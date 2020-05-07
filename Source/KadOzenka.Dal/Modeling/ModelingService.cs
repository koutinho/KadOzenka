using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using DevExpress.CodeParser;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.ScoreCommon;
using KadOzenka.Dal.ScoreCommon.Dto;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using AttributeDto = KadOzenka.Dal.Modeling.Dto.AttributeDto;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingService
	{
		public ScoreCommonService ScoreCommonService { get; set; }

		public ModelingService(ScoreCommonService scoreCommonService)
		{
			ScoreCommonService = scoreCommonService;
		}

		#region CRUD Model

		public ModelingModelDto GetModelById(long modelId)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMModelingModel.GetRegisterId(),
				Condition = new QSConditionSimple(OMModelingModel.GetColumn(x => x.Id), QSConditionType.Equal, modelId),
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMTour.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMModelingModel.GetColumn(x => x.TourId),
							RightOperand = OMTour.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					}
				}
			};
			query.AddColumn(OMModelingModel.GetColumn(x => x.Name, nameof(ModelingModelDto.Name)));
			query.AddColumn(OMModelingModel.GetColumn(x => x.TourId, nameof(ModelingModelDto.TourId)));
			query.AddColumn(OMTour.GetColumn(x => x.Year, nameof(ModelingModelDto.TourYear)));
			query.AddColumn(OMModelingModel.GetColumn(x => x.MarketSegment_Code, nameof(ModelingModelDto.MarketSegment)));

			var table = query.ExecuteQuery();
			ModelingModelDto model = null;
			if (table.Rows.Count != 0)
			{
				var row = table.Rows[0];

				var modelName = row[nameof(ModelingModelDto.Name)].ParseToString();
				var tourId = row[nameof(ModelingModelDto.TourId)].ParseToLong();
				var tourYear = row[nameof(ModelingModelDto.TourYear)].ParseToLong();
				var segment = (MarketSegment)row[nameof(ModelingModelDto.MarketSegment)];

				model = new ModelingModelDto
				{
					ModelId = modelId,
					Name = modelName,
					TourId = tourId,
					TourYear = tourYear,
					MarketSegment = segment
				};
			}

			if (model == null)
				throw new Exception($"Не найдена модель с Id='{modelId}'");

			return model;
		}

		public List<AttributeDto> GetModelAttributes(long modelId)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMModelAttributesRelation.GetRegisterId(),
				Condition = new QSConditionSimple(OMModelAttributesRelation.GetColumn(x => x.ModelId), QSConditionType.Equal, modelId),
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMAttribute.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMModelAttributesRelation.GetColumn(x => x.AttributeId),
							RightOperand = OMAttribute.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					},
					new QSJoin
					{
						RegisterId = OMEsReference.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMModelAttributesRelation.GetColumn(x => x.DictionaryId),
							RightOperand = OMEsReference.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Left
					}
				}
			};
			query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(AttributeDto.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(AttributeDto.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(AttributeDto.AttributeName)));
			query.AddColumn(OMEsReference.GetColumn(x => x.Id, nameof(AttributeDto.DictionaryId)));
			query.AddColumn(OMEsReference.GetColumn(x => x.Name, nameof(AttributeDto.DictionaryName)));

			var attributes = new List<AttributeDto>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];

				var registerId = row[nameof(AttributeDto.RegisterId)].ParseToLong();

				var attributeId = row[nameof(AttributeDto.AttributeId)].ParseToLong();
				var attributeName = row[nameof(AttributeDto.AttributeName)].ParseToString();

				var dictionaryId = row[nameof(AttributeDto.DictionaryId)].ParseToLongNullable();
				var dictionaryName = row[nameof(AttributeDto.DictionaryName)].ParseToString();

				attributes.Add(new AttributeDto
				{
					RegisterId = registerId,
					AttributeId = attributeId,
					AttributeName = attributeName,
					DictionaryId = dictionaryId,
					DictionaryName = dictionaryName
				});
			}

			return attributes;
		}

		public int AddModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			return new OMModelingModel
			{
				Name = modelDto.Name,
				TourId = modelDto.TourId,
				MarketSegment_Code = modelDto.MarketSegment
			}.Save();
		}

		public void UpdateModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);
			var existedModel = OMModelingModel.Where(x => x.Id == modelDto.ModelId).SelectAll().ExecuteFirstOrDefault();
			if (existedModel == null)
				throw new Exception($"Не найдена модель с Id='{modelDto.ModelId}'");

			var newAttributes = modelDto.Attributes;
			var existedModelAttributes = OMModelAttributesRelation.Where(x => x.ModelId == modelDto.ModelId).SelectAll().Execute();
			using (var ts = new TransactionScope())
			{
				existedModel.Name = modelDto.Name;
				existedModel.TourId = modelDto.TourId;
				existedModel.MarketSegment_Code = modelDto.MarketSegment;
				existedModel.Save();

				existedModelAttributes.ForEach(x => x.Destroy());
				newAttributes?.ForEach(newAttribute =>
				{
					new OMModelAttributesRelation
					{
						ModelId = modelDto.ModelId,
						AttributeId = newAttribute.AttributeId,
						DictionaryId = newAttribute.DictionaryId
					}.Save();
				});

				ts.Complete();
			}
		}

		#endregion


		#region Cofficeints

		public List<ModelMarketObjectRelationDto> GetMarketObjectsForModel(long modelId)
		{
			var models = OMModelToMarketObjects.Where(x => x.ModelId == modelId)
				.OrderBy(x => x.CadastralNumber)
				.SelectAll()
				.Execute();

			return models.Select(ToDto).ToList();
		}

		public void ChangeObjectsStatusInCalculation(List<ModelMarketObjectRelationDto> objects)
		{
			var ids = objects.Select(x => x.Id);
			var objectsFromDb = OMModelToMarketObjects.Where(x => ids.Contains(x.Id)).SelectAll().Execute();
			objects.ForEach(obj =>
			{
				var objFromDb = objectsFromDb.FirstOrDefault(x => x.Id == obj.Id);
				if (objFromDb == null)
					return;

				objFromDb.IsExcluded = obj.IsExcluded;
				objFromDb.Save();
			});
		}

		#endregion


		#region Calculation for Process

		public void CreateObjectsForModel(ModelingModelDto modelDto)
		{
			var groupedObjects = OMCoreObject.Where(x =>
					x.PropertyMarketSegment_Code == modelDto.MarketSegment &&
					x.CadastralNumber != null && x.CadastralNumber != string.Empty)
				.Select(x => x.CadastralNumber)
				.Select(x => x.Price)
				.Execute()
				.GroupBy(x => new
				{
					x.CadastralNumber,
					x.Price
				}).ToList();

			var objectsFromPreviousCalculation = OMModelToMarketObjects.Where(x => x.ModelId == modelDto.ModelId).SelectAll().Execute();
			groupedObjects.ForEach(groupedObj =>
			{
				var existedObject = objectsFromPreviousCalculation.FirstOrDefault(x =>
					x.CadastralNumber == groupedObj.Key.CadastralNumber && x.Price == groupedObj.Key.Price);
				if (existedObject == null)
				{
					new OMModelToMarketObjects
					{
						ModelId = modelDto.ModelId,
						CadastralNumber = groupedObj.Key.CadastralNumber,
						Price = groupedObj.Key.Price ?? 0
					}.Save();
				}
			});
		}

		public void CreateCoefficientsForObjects(long modelId)
		{
			var model = GetModelById(modelId);

			var modelAttributes = GetModelAttributes(model.ModelId);

			var modelMarketObjects = GetModelMarketObjects(model.ModelId);

			modelMarketObjects.ForEach(modelMarketObject =>
			{
				modelAttributes.ForEach(modelAttribute =>
				{
					modelAttribute.Coefficient = null;
					modelAttribute.Message = null;

					var objectParameterData = GetParameterDataByCadastralNumber(modelMarketObject.CadastralNumber, (int)model.TourId,
						(int)modelAttribute.AttributeId, (int)modelAttribute.RegisterId);
					if (objectParameterData == null)
					{
						modelAttribute.Message =
							$"Не найдено значение. Аттрибут '{modelAttribute.AttributeName}' для объекта '{modelMarketObject.CadastralNumber}'.";
					}
					else
					{
						CalculateCoefficients(objectParameterData, modelAttribute, modelMarketObject);
					}
				});

				modelMarketObject.Coefficients = modelAttributes.SerializeToXml();
				modelMarketObject.Save();
			});
		}

		

		#endregion


		#region Support Methods

		public ModelMarketObjectRelationDto ToDto(OMModelToMarketObjects entity)
		{
			return new ModelMarketObjectRelationDto
			{
				Id = entity.Id,
				CadastralNumber = entity.CadastralNumber,
				Price = entity.Price,
				IsExcluded = entity.IsExcluded.GetValueOrDefault()
			};
		}

		private void ValidateModel(ModelingModelDto modelDto)
		{
			var message = new StringBuilder();

			if (string.IsNullOrWhiteSpace(modelDto.Name))
				message.AppendLine("У модели не заполнено имя");

			var isTourExists = OMTour.Where(x => x.Id == modelDto.TourId).ExecuteExists();
			if(!isTourExists)
				message.AppendLine($"Не найден Тур с Id='{modelDto.TourId}'");

			if(modelDto.MarketSegment == MarketSegment.None)
				message.AppendLine("Для модели не выбран сегмент");

			if (message.Length != 0)
				throw new Exception(message.ToString());
		}

		private List<OMModelToMarketObjects> GetModelMarketObjects(long modelId)
		{
			return OMModelToMarketObjects.Where(x =>
				x.ModelId == modelId && (x.IsExcluded == null || x.IsExcluded == false)).SelectAll().Execute();
		}

		private ParameterDataDto GetParameterDataByCadastralNumber(string kn, int tourId, int attributeId, int registerId)
		{
			var unitsIds = ScoreCommonService.GetUnitsIdsByCadastralNumber(kn, tourId);
			return unitsIds.Count > 0 ? ScoreCommonService.GetParameters(unitsIds, attributeId, registerId) : null;
		}

		private void CalculateCoefficients(ParameterDataDto objectParameterData, AttributeDto modelAttribute, OMModelToMarketObjects modelMarketObject)
		{
			switch (objectParameterData.Type)
			{
				case ParameterType.String:
				{
					if (modelAttribute.DictionaryId == null)
					{
						modelAttribute.Message = GetErrorMessage(modelAttribute.AttributeName,
							modelMarketObject.CadastralNumber, "строка");
					}
					else
					{
						modelAttribute.Coefficient = ScoreCommonService.GetCoefficientFromStringFactor(objectParameterData,
							(int)modelAttribute.DictionaryId.GetValueOrDefault());
					}

					break;
				}
				case ParameterType.Date:
				{
					if (modelAttribute.DictionaryId == null)
					{
						modelAttribute.Message =
							GetErrorMessage(modelAttribute.AttributeName, modelMarketObject.CadastralNumber, "дата");
					}
					else
					{
						modelAttribute.Coefficient = ScoreCommonService.GetCoefficientFromDateFactor(objectParameterData,
							(int)modelAttribute.DictionaryId.GetValueOrDefault());
					}

					break;
				}
				case ParameterType.Number:
				{
					modelAttribute.Coefficient = ScoreCommonService.GetCoefficientFromNumberFactor(objectParameterData,
						(int)modelAttribute.DictionaryId.GetValueOrDefault());

					break;
				}
				default:
				{
					modelAttribute.Message = GetErrorMessage(modelAttribute.AttributeName, modelMarketObject.CadastralNumber,
						"неизвестный тип");
					break;
				}
			}
		}

		//TODO remove
		private string GetErrorMessage(string attributeName, string cadastralNumber, string type)
		{
			return $"Ошибка: нет справочника. Аттрибут '{attributeName}' для объекта '{cadastralNumber}' относится к типу '{type}', но к нему не выбран справочник.";
		}

		#endregion
	}
}
