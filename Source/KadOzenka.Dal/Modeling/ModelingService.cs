﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.ScoreCommon;
using KadOzenka.Dal.ScoreCommon.Dto;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;

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
            query.AddColumn(OMModelingModel.GetColumn(x => x.WasTrained, nameof(ModelingModelDto.WasTrained)));

            var table = query.ExecuteQuery();
			ModelingModelDto model = null;
			if (table.Rows.Count != 0)
			{
				var row = table.Rows[0];

				var modelName = row[nameof(ModelingModelDto.Name)].ParseToString();
				var tourId = row[nameof(ModelingModelDto.TourId)].ParseToLong();
				var tourYear = row[nameof(ModelingModelDto.TourYear)].ParseToLong();
				var segment = (MarketSegment)row[nameof(ModelingModelDto.MarketSegment)];
                var wasTrained = row[nameof(ModelingModelDto.WasTrained)].ParseToBooleanNullable();

                model = new ModelingModelDto
				{
					ModelId = modelId,
					Name = modelName,
					TourId = tourId,
					TourYear = tourYear,
					MarketSegment = segment,
                    WasTrained = wasTrained.GetValueOrDefault()
                };
			}

			if (model == null)
				throw new Exception($"Не найдена модель с Id='{modelId}'");

			return model;
		}

		public List<ModelAttributeDto> GetModelAttributes(long modelId)
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
			query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelAttributeDto.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelAttributeDto.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelAttributeDto.AttributeName)));
			query.AddColumn(OMEsReference.GetColumn(x => x.Id, nameof(ModelAttributeDto.DictionaryId)));
			query.AddColumn(OMEsReference.GetColumn(x => x.Name, nameof(ModelAttributeDto.DictionaryName)));

			var attributes = new List<ModelAttributeDto>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];

				var registerId = row[nameof(ModelAttributeDto.RegisterId)].ParseToLong();

				var attributeId = row[nameof(ModelAttributeDto.AttributeId)].ParseToLong();
				var attributeName = row[nameof(ModelAttributeDto.AttributeName)].ParseToString();

				var dictionaryId = row[nameof(ModelAttributeDto.DictionaryId)].ParseToLongNullable();
				var dictionaryName = row[nameof(ModelAttributeDto.DictionaryName)].ParseToString();

				attributes.Add(new ModelAttributeDto
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

		public bool UpdateModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			var existedModel = GetModelByIdInternal(modelDto.ModelId);

			var newAttributes = modelDto.Attributes ?? new List<ModelAttributeDto>();
			var existedModelAttributes = OMModelAttributesRelation.Where(x => x.ModelId == modelDto.ModelId).SelectAll().Execute();

            var isModelChanged = IsModelChanged(existedModel, modelDto, existedModelAttributes, newAttributes);

            using (var ts = new TransactionScope())
            {
                existedModel.Name = modelDto.Name;
                existedModel.TourId = modelDto.TourId;
                existedModel.MarketSegment_Code = modelDto.MarketSegment;
                if (isModelChanged)
                    existedModel.WasTrained = false;
                existedModel.Save();

                existedModelAttributes.ForEach(x => x.Destroy());
                newAttributes.ForEach(newAttribute =>
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

            return isModelChanged;
        }

        private bool IsModelChanged(OMModelingModel existedModel, ModelingModelDto newModel, List<OMModelAttributesRelation> existedAttributes, List<ModelAttributeDto> newAttributes)
        {
            var oldAttributeIds = existedAttributes.Select(x => x.AttributeId).OrderBy(x => x);
            var newAttributeIds = newAttributes.Select(x => x.AttributeId).OrderBy(x => x);
            var areAttributeIdsEqual = oldAttributeIds.SequenceEqual(newAttributeIds);

            var oldDictionaryIds = existedAttributes.Select(x => x.DictionaryId).OrderBy(x => x);
            var newDictionaryIIds = newAttributes.Select(x => x.DictionaryId).OrderBy(x => x);
            var areDictionaryIdsSequenceEqualEqual = oldDictionaryIds.SequenceEqual(newDictionaryIIds);

            return !(existedModel.TourId == newModel.TourId &&
                   existedModel.MarketSegment_Code == newModel.MarketSegment &&
                   areAttributeIdsEqual &&
                   areDictionaryIdsSequenceEqualEqual);
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
			var ids = objects.Select(x => x.Id).ToList();
            if (ids.Count == 0)
                return;

            var objectsFromDb = OMModelToMarketObjects.Where(x => ids.Contains(x.Id)).SelectAll().Execute();
            objects.ForEach(obj =>
            {
                var objFromDb = objectsFromDb.FirstOrDefault(x => x.Id == obj.Id);
                if (objFromDb == null)
                    return;

                objFromDb.IsExcluded = obj.IsExcluded;
                objFromDb.Coefficients = null;
                objFromDb.Save();
            });
        }

        #endregion


        #region Modeling Process

        public void CreateObjectsForModel(long modelId)
        {
            var model = GetModelByIdInternal(modelId);

			var groupedObjects = OMCoreObject.Where(x =>
					x.PropertyMarketSegment_Code == model.MarketSegment_Code &&
					x.CadastralNumber != null && x.CadastralNumber != string.Empty && 
                    x.ProcessType_Code != ProcessStep.Excluded)
				.Select(x => x.CadastralNumber)
				.Select(x => x.Price)
				.Execute()
				.GroupBy(x => new
				{
					x.CadastralNumber,
					x.Price
				}).ToList();

			var existedModelObjects = GetAllModelMarketObjects(modelId);
            var modelAttributes = GetModelAttributes(modelId);

            for (var i = 0; i < groupedObjects.Count; i++)
            {
                var isForTraining = i < groupedObjects.Count / 2;

                var groupedObj = groupedObjects[i];
                var existedModelObject = existedModelObjects.FirstOrDefault(x =>
                    x.CadastralNumber == groupedObj.Key.CadastralNumber && x.Price == groupedObj.Key.Price);

                if (existedModelObject == null)
                {
                    existedModelObject = new OMModelToMarketObjects
                    {
                        ModelId = modelId,
                        CadastralNumber = groupedObj.Key.CadastralNumber,
                        Price = groupedObj.Key.Price ?? 0,
                        IsForTraining = isForTraining
                    };
                }
                else
                {
                    existedModelObject.IsForTraining = isForTraining;
                }

                CalculateCoefficientsForModelObject(model.TourId, existedModelObject, modelAttributes);

                existedModelObject.Coefficients = modelAttributes.SerializeToXml();
                existedModelObject.Save();
            }
        }

		public void CalculateCoefficientsForModelObject(long tourId, OMModelToMarketObjects modelObject, List<ModelAttributeDto> modelAttributes)
        { 
            modelAttributes.ForEach(modelAttribute =>
            {
                modelAttribute.Coefficient = null;
                modelAttribute.Message = null;

                var objectParameterData = GetParameterDataByCadastralNumber(modelObject.CadastralNumber, (int) tourId,
                    (int) modelAttribute.AttributeId, (int) modelAttribute.RegisterId);
                if (objectParameterData == null)
                {
                    modelAttribute.Message =
                        $"Не найдено значение. Аттрибут '{modelAttribute.AttributeName}' для объекта '{modelObject.CadastralNumber}'.";
                }
                else
                {
                    CalculateCoefficients(objectParameterData, modelAttribute, modelObject);
                }
            });
        }

        #endregion


        #region Support Methods

        private OMModelingModel GetModelByIdInternal(long modelId)
        {
            var model = OMModelingModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
                throw new Exception($"Не найдена модель с Id='{modelId}'");

            return model;
        }

        public ModelMarketObjectRelationDto ToDto(OMModelToMarketObjects entity)
		{
			return new ModelMarketObjectRelationDto
			{
				Id = entity.Id,
				CadastralNumber = entity.CadastralNumber,
				Price = entity.Price,
                PriceFromModel = entity.PriceFromModel,
				IsExcluded = entity.IsExcluded.GetValueOrDefault(), 
                IsForTraining = entity.IsForTraining.GetValueOrDefault(),
                Coefficients = entity.Coefficients.DeserializeFromXml<List<ModelAttributeDto>>()
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

		private List<OMModelToMarketObjects> GetAllModelMarketObjects(long modelId)
        {
            return OMModelToMarketObjects.Where(x => x.ModelId == modelId).SelectAll().Execute();
        }

        private ParameterDataDto GetParameterDataByCadastralNumber(string kn, int tourId, int attributeId, int registerId)
		{
			var unitsIds = ScoreCommonService.GetUnitsIdsByCadastralNumber(kn, tourId);
			return unitsIds.Count > 0 ? ScoreCommonService.GetParameters(unitsIds, attributeId, registerId) : null;
		}

		private void CalculateCoefficients(ParameterDataDto objectParameterData, ModelAttributeDto modelModelAttribute, OMModelToMarketObjects modelMarketObject)
		{
            switch (objectParameterData.Type)
			{
				case ParameterType.String:
				{
					if (modelModelAttribute.DictionaryId == null)
					{
						modelModelAttribute.Message = GetErrorMessage(modelModelAttribute.AttributeName,
							modelMarketObject.CadastralNumber, "строка");
					}
					else
					{
						modelModelAttribute.Coefficient = ScoreCommonService.GetCoefficientFromStringFactor(objectParameterData,
							(int)modelModelAttribute.DictionaryId.GetValueOrDefault());
					}

					break;
				}
				case ParameterType.Date:
				{
					if (modelModelAttribute.DictionaryId == null)
					{
						modelModelAttribute.Message =
							GetErrorMessage(modelModelAttribute.AttributeName, modelMarketObject.CadastralNumber, "дата");
					}
					else
					{
						modelModelAttribute.Coefficient = ScoreCommonService.GetCoefficientFromDateFactor(objectParameterData,
							(int)modelModelAttribute.DictionaryId.GetValueOrDefault());
					}

					break;
				}
				case ParameterType.Number:
				{
					modelModelAttribute.Coefficient = ScoreCommonService.GetCoefficientFromNumberFactor(objectParameterData,
						(int)modelModelAttribute.DictionaryId.GetValueOrDefault());

					break;
				}
				default:
				{
					modelModelAttribute.Message = GetErrorMessage(modelModelAttribute.AttributeName, modelMarketObject.CadastralNumber,
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
