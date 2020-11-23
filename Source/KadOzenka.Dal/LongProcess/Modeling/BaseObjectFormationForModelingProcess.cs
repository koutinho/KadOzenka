using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Modeling
{
	public class BaseObjectFormationForModelingProcess : LongProcess
	{
		protected readonly ILogger Logger;

		protected DictionaryService DictionaryService { get; set; }
		protected ModelingService ModelingService { get; set; }
		protected ModelFactorsService ModelFactorsService { get; set; }

		public BaseObjectFormationForModelingProcess(ILogger logger)
		{
			Logger = logger;
			ModelingService = new ModelingService();
			DictionaryService = new DictionaryService();
			ModelFactorsService = new ModelFactorsService();
		}



		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		protected Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromMarketObject(List<long> objectIds, List<OMModelingDictionary> dictionaries,
		  List<ModelAttributePure> modelAttributes)
		{
			if (modelAttributes == null || modelAttributes.Count == 0 || objectIds == null || objectIds.Count == 0)
				return new Dictionary<long, List<CoefficientForObject>>();

			var query = new QSQuery
			{
				MainRegisterID = OMCoreObject.GetRegisterId(),
				Condition = new QSConditionSimple
				{
					ConditionType = QSConditionType.In,
					LeftOperand = OMCoreObject.GetColumn(x => x.Id),
					RightOperand = new QSColumnConstant(objectIds)
				}
			};

			return GetCoefficients(query, dictionaries, modelAttributes);
		}

		protected Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromTourFactors(List<long> unitIds, List<OMModelingDictionary> dictionaries,
			List<ModelAttributePure> modelAttributes)
		{
			if (modelAttributes == null || modelAttributes.Count == 0 || unitIds == null || unitIds.Count == 0)
				return new Dictionary<long, List<CoefficientForObject>>();

			var registerId = modelAttributes[0].RegisterId;
			var idAttribute = RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == registerId && x.IsPrimaryKey).Id;

			var query = new QSQuery
			{
				MainRegisterID = (int)registerId,
				Condition = new QSConditionSimple
				{
					ConditionType = QSConditionType.In,
					LeftOperand = new QSColumnSimple((int)idAttribute),
					RightOperand = new QSColumnConstant(unitIds)
				}
			};

			return GetCoefficients(query, dictionaries, modelAttributes);
		}

		protected void CreateMarkCatalog(long? groupId, List<OMModelToMarketObjects> modelObjects, List<ModelAttributePure> attributes, OMQueue queue)
		{
			attributes.ForEach(attribute =>
			{
				var numberOfMarks = ModelFactorsService.DeleteMarks(groupId, attribute.AttributeId);
				AddLog(queue, $"Удалено {numberOfMarks} предыдущих меток для фактора '{attribute.AttributeName}' (ИД {attribute.AttributeId})", logger: Logger);
			});

			for (var i = 0; i < modelObjects.Count; i++)
			{
				var modelObject = modelObjects[i];

				foreach (var attribute in attributes)
				{
					var objectCoefficients = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
					var objectCoefficient = objectCoefficients.FirstOrDefault(x => x.AttributeId == attribute.AttributeId && !string.IsNullOrWhiteSpace(x.Value));
					if (objectCoefficient == null || !string.IsNullOrWhiteSpace(objectCoefficient.Message))
						return;

					var value = objectCoefficient.Value;
					var metka = objectCoefficient.Coefficient;

					if (metka != null)
					{
						ModelFactorsService.CreateMark(value, metka, attribute.AttributeId, groupId);
					}
				}
			}
		}


		#region Support Methods

		private Dictionary<long, List<CoefficientForObject>> GetCoefficients(QSQuery query, List<OMModelingDictionary> dictionaries, List<ModelAttributePure> attributes)
		{
			attributes.ForEach(attribute =>
			{
				query.AddColumn(attribute.AttributeId, attribute.AttributeId.ToString());
			});

			var sql = query.GetSql();

			var coefficients = new Dictionary<long, List<CoefficientForObject>>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];
				var id = row["id"].ParseToLong();
				var currentCoefficients = new List<CoefficientForObject>();
				attributes.ForEach(attribute =>
				{
					var value = row[attribute.AttributeId.ToString()].ParseToStringNullable();

					CoefficientForObject coefficient;
					if (string.IsNullOrWhiteSpace(value))
					{
						coefficient = new CoefficientForObject(attribute.AttributeId)
						{
							Message = "Не найдено значение."
						};
					}
					else
					{
						var dictionary = attribute.DictionaryId == null
							? null
							: dictionaries.FirstOrDefault(x => x.Id == attribute.DictionaryId);

						coefficient = CalculateCoefficientViaDictionary(value, attribute, dictionary);
					}
					currentCoefficients.Add(coefficient);
				});

				coefficients[id] = currentCoefficients;
			}

			return coefficients;
		}

		private CoefficientForObject CalculateCoefficientViaDictionary(object value, ModelAttributePure modelAttribute, OMModelingDictionary dictionary)
		{
			var coefficient = new CoefficientForObject(modelAttribute.AttributeId);

			switch (modelAttribute.AttributeTypeCode)
			{
				case RegisterAttributeType.STRING:
					{
						if (dictionary == null)
						{
							coefficient.Message = GetErrorMessage("строка");
						}
						else
						{
							var stringValue = value?.ParseToString();
							coefficient.Value = stringValue;
							coefficient.Coefficient = DictionaryService.GetCoefficientFromStringFactor(stringValue, dictionary);
						}

						break;
					}
				case RegisterAttributeType.DATE:
					{
						if (dictionary == null)
						{
							coefficient.Message = GetErrorMessage("дата");
						}
						else
						{
							var dateValue = value?.ParseToDateTimeNullable();
							coefficient.Value = dateValue?.ToShortDateString();
							coefficient.Coefficient = DictionaryService.GetCoefficientFromDateFactor(dateValue, dictionary);
						}

						break;
					}
				//число
				case RegisterAttributeType.INTEGER:
				case RegisterAttributeType.DECIMAL:
					{
						var numberValue = value?.ParseToDecimalNullable();

						var number = DictionaryService.GetCoefficientFromNumberFactor(numberValue, dictionary);

						coefficient.Value = number.ToString();
						coefficient.Coefficient = number;
						break;
					}
				default:
					{
						coefficient.Message = "Ошибка: атрибут относится к типу 'неизвестный тип'.";
						break;
					}
			}

			return coefficient;
		}

		private string GetErrorMessage(string type)
		{
			return $"Ошибка: нет справочника. Атрибут относится к типу '{type}', но к нему не выбран справочник.";
		}

		#endregion


		#region Entities

		protected class ModelAttributePure
		{
			public long AttributeId { get; }
			public string AttributeName { get; }
			public long RegisterId { get; }
			public long? DictionaryId { get; }
			public RegisterAttributeType AttributeTypeCode { get; }

			public ModelAttributePure(long attributeId, long? dictionaryId)
			{
				var attribute = RegisterCache.GetAttributeData(attributeId);

				AttributeId = attribute.Id;
				AttributeName = attribute.Name;
				AttributeTypeCode = attribute.Type;
				RegisterId = attribute.RegisterId;
				DictionaryId = dictionaryId;
			}
		}

		#endregion
	}
}
