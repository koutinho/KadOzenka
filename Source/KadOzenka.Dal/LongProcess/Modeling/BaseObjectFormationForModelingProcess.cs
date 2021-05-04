using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using ExCSS.Model.Extensions;
using KadOzenka.Dal.LongProcess.Modeling.Entities;
using KadOzenka.Dal.Modeling;
using MarketPlaceBusiness;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Modeling
{
	public class BaseObjectFormationForModelingProcess : LongProcess
	{
		protected readonly ILogger Logger;

		protected DictionaryService DictionaryService { get; set; }
		protected ModelingService ModelingService { get; set; }
		protected ModelFactorsService ModelFactorsService { get; set; }
		protected IModelObjectsService ModelObjectsService { get; set; }

		public BaseObjectFormationForModelingProcess(ILogger logger)
		{
			Logger = logger;
			ModelingService = new ModelingService();
			DictionaryService = new DictionaryService();
			ModelFactorsService = new ModelFactorsService();
			ModelObjectsService = new ModelObjectsService();
		}



		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		protected List<ModelAttributePure> GetGeneralModelAttributes(long modelId)
		{
			return ModelFactorsService.GetGeneralModelAttributes(modelId)
				.Select(x => new ModelAttributePure(x.AttributeId, x.DictionaryId)).ToList();
		}

		protected Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromMarketObject(List<long> objectIds, List<OMModelingDictionary> dictionaries,
		  List<ModelAttributePure> modelAttributes)
		{
			if (modelAttributes == null || modelAttributes.Count == 0 || objectIds == null || objectIds.Count == 0)
				return new Dictionary<long, List<CoefficientForObject>>();

			return GetCoefficients(MarketObjectsForModelingService.RegisterId, MarketObjectsForModelingService.PrimaryKeyColumn, objectIds, dictionaries, modelAttributes);
		}

		protected Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromTourFactors(List<long> unitIds, List<OMModelingDictionary> dictionaries,
			List<ModelAttributePure> modelAttributes)
		{
			if (modelAttributes == null || modelAttributes.Count == 0 || unitIds == null || unitIds.Count == 0)
				return new Dictionary<long, List<CoefficientForObject>>();

			var registerId = modelAttributes[0].RegisterId;
			var idAttribute = RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == registerId && x.IsPrimaryKey).Id;
			var registerPrimaryKeyColumn = new QSColumnSimple((int)idAttribute);

			return GetCoefficients(registerId, registerPrimaryKeyColumn, unitIds, dictionaries, modelAttributes);
		}

		protected void CreateMarkCatalog(long? groupId, List<OMModelToMarketObjects> modelObjects, List<ModelAttributePure> attributes, OMQueue queue)
		{
			using (Logger.TimeOperation("Формирование каталога меток"))
			{
				AddLog(queue, "Начато формирование каталога меток", logger: Logger);

				using (Logger.TimeOperation("Удаление всех предыдущих меток"))
				{
					attributes.ForEach(attribute =>
					{
						var deletedMarksCount = ModelFactorsService.DeleteMarks(groupId, attribute.AttributeId);
						Logger.Debug("Удалено {DeletedMarksCount} предыдущих меток для фактора '{AttributeName}' (ИД {AttributeId})", deletedMarksCount, attribute.AttributeName, attribute.AttributeId);
					});
				}

				using (Logger.TimeOperation("Создание новых меток"))
				{
					var rowsToInsertSql = new StringBuilder();
					var marksCounter = 0;
					for (var i = 0; i < modelObjects.Count; i++)
					{
						var modelObject = modelObjects[i];
						var objectCoefficients = modelObject.DeserializeCoefficient();

						foreach (var attribute in attributes)
						{
							var objectCoefficient = objectCoefficients.FirstOrDefault(x => x.AttributeId == attribute.AttributeId);
							if (objectCoefficient == null || string.IsNullOrWhiteSpace(objectCoefficient.Value) ||
							    objectCoefficient.Coefficient.GetValueOrDefault() == 0)
								continue;

							var value = objectCoefficient.Value.Replace(',', '.');
							var metka = objectCoefficient.Coefficient.ToString().Replace(',', '.');
							rowsToInsertSql.AppendLine($"((select nextval('REG_OBJECT_SEQ')), {groupId}, {attribute.AttributeId}, {value}, {metka}),");

							//добавляем метки пакетом по 1000 записей
							marksCounter++;
							if (marksCounter % 2500 == 0)
							{
								InsertMarks(rowsToInsertSql);
								rowsToInsertSql = new StringBuilder();
							}
						}
					}

					if (rowsToInsertSql.Length > 0)
					{
						InsertMarks(rowsToInsertSql);
					}

					Logger.Debug("Всего в БД добавлено {numberOfMarks} меток", marksCounter);
				}
				
				AddLog(queue, "Закончено формирование каталога меток", logger: Logger);
			}
		}

		private void InsertMarks(StringBuilder rowsToInsertSql)
		{
			//убираем последний перевод строки и знак ','
			rowsToInsertSql.TrimLastLine().Length--;

			var sql = @$"INSERT INTO ko_mark_catalog (id, group_id, factor_id, value_factor, metka_factor)
							VALUES
							{rowsToInsertSql}";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			var insertedMarksCount = DBMngr.Main.ExecuteNonQuery(command);

			Logger.Debug("В БД добавлено {numberOfMarks} меток", insertedMarksCount);
		}

		protected void SaveStatistic(List<OMModelToMarketObjects> objects, List<ModelAttributePure> attributes, OMModel model, OMQueue queue)
		{
			AddLog(queue, $"Начато формирование статистики для модели '{model.Name}'.", logger: Logger);

			var attributesDictionary = new Dictionary<long, ObjectsByAttributeStatistic>();
			attributes.ForEach(x =>
			{
				attributesDictionary[x.AttributeId] = new ObjectsByAttributeStatistic
				{
					AttributeId = x.AttributeId,
					AttributeName = x.AttributeName
				};
			});

			objects.ForEach(obj =>
			{
				var coefficients = obj.DeserializeCoefficient();
				attributes.ForEach(attribute =>
				{
					var attributeCoefficient = coefficients.FirstOrDefault(x => x.AttributeId == attribute.AttributeId)?.Coefficient;
					if (attributeCoefficient == null)
						return;

					attributesDictionary[attribute.AttributeId].Count++;
				});
			});

			decimal totalCount = objects.Count;
			if (totalCount != 0)
			{
				attributesDictionary.Values.ForEach(x =>
				{
					x.Percent = x.Count / totalCount * 100;
				});
			}

			var result = new ModelingObjectsStatistic
			{
				TotalCount = (int) totalCount,
				ObjectsByAttributeStatistics = attributesDictionary.Values.OrderByDescending(x => x.Count).ToList()
			};

			model.ObjectsStatistic = result.SerializeToXml();
			model.Save();

			AddLog(queue, $"Закончено формирование статистики для модели '{model.Name}'.", logger: Logger);
		}


		#region Support Methods

		private Dictionary<long, List<CoefficientForObject>> GetCoefficients(long registerId, QSColumn registerPrimaryKeyColumn, 
			List<long> objectIds, List<OMModelingDictionary> dictionaries, List<ModelAttributePure> attributes)
		{
			var query = new QSQuery
			{
				MainRegisterID = (int)registerId,
				Condition = new QSConditionSimple
				{
					ConditionType = QSConditionType.In,
					LeftOperand = registerPrimaryKeyColumn,
					RightOperand = new QSColumnConstant(objectIds)
				}
			};

			attributes.ForEach(attribute =>
			{
				query.AddColumn(attribute.AttributeId, attribute.AttributeId.ToString());
			});

			//var sql = query.GetSql();

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

					var dictionary = attribute.DictionaryId == null
						? null
						: dictionaries.FirstOrDefault(x => x.Id == attribute.DictionaryId);

					var coefficient = CalculateCoefficient(value, attribute, dictionary);

					currentCoefficients.Add(coefficient);
				});

				coefficients[id] = currentCoefficients;
			}

			return coefficients;
		}

		private CoefficientForObject CalculateCoefficient(object value, ModelAttributePure modelAttribute, OMModelingDictionary dictionary)
		{
			string resultValue;
			decimal? resultCoefficient;

			switch (modelAttribute.AttributeTypeCode)
			{
				case RegisterAttributeType.STRING:
					{
						if (dictionary == null)
							throw new Exception(GetErrorMessage("строка"));

						var stringValue = value?.ParseToString();
						resultValue = stringValue;
						resultCoefficient = DictionaryService.GetCoefficientFromStringFactor(stringValue, dictionary);
						break;
					}
				case RegisterAttributeType.DATE:
					{
						if (dictionary == null)
							throw new Exception(GetErrorMessage("дата"));

						var dateValue = value?.ParseToDateTimeNullable();
						resultValue = dateValue?.ToShortDateString();
						resultCoefficient = DictionaryService.GetCoefficientFromDateFactor(dateValue, dictionary);
						break;
					}
				case RegisterAttributeType.INTEGER:
				case RegisterAttributeType.DECIMAL:
					{
						var numberValue = value?.ParseToDecimalNullable();

						var number = DictionaryService.GetCoefficientFromNumberFactor(numberValue, dictionary);
						resultValue = number.ToString();
						resultCoefficient = number;
						break;
					}
				default:
					{
						throw new Exception("Ошибка: атрибут относится к типу 'неизвестный тип'.");
					}
			}

			var coefficientObj = new CoefficientForObject(modelAttribute.AttributeId)
			{
				Value = resultValue, 
				Coefficient = resultCoefficient
			};

			return coefficientObj;
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
