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

		public void UpdateModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);
			var existedModel = GetModelByIdInternal(modelDto.ModelId);

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
					x.CadastralNumber != null && x.CadastralNumber != string.Empty)
				.Select(x => x.CadastralNumber)
				.Select(x => x.Price)
				.Execute()
				.GroupBy(x => new
				{
					x.CadastralNumber,
					x.Price
				}).ToList();

			var existedModelObjects = GetModelMarketObjects(modelId);
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

        public TrainingSet GetCoefficientsToTrainModel(long modelId)
        {
            var trainingSet = new TrainingSet();

            var modelMarketObjects = GetModelMarketObjects(modelId).Where(x => x.IsForTraining == true).ToList();

            modelMarketObjects.ForEach(modelObject =>
            {
                var coefficients = modelObject.Coefficients.DeserializeFromXml<List<ModelAttributeDto>>()
                    ?.Where(x => x.Coefficient != null).ToList();
                if (coefficients == null || coefficients.Count == 0)
                    return;

                trainingSet.Prices.Add(modelObject.Price);
                trainingSet.AttributeNames.AddRange(coefficients.Select(x => x.AttributeName).ToList());
                trainingSet.Coefficients.AddRange(coefficients.Select(x => x.Coefficient.Value).ToList());
            });

            return trainingSet;
        }

        public CalculationSet GetCoefficientsToCalculateModel(long modelId)
        {
            var calculationSet = new CalculationSet();

            var modelMarketObjects = GetModelMarketObjects(modelId)
                .Where(x => x.IsForTraining == null || x.IsForTraining == false).ToList();

            modelMarketObjects.ForEach(modelObject =>
            {
                var coefficients = modelObject.Coefficients.DeserializeFromXml<List<ModelAttributeDto>>()
                    ?.Where(x => x.Coefficient != null).ToList();
                if (coefficients == null || coefficients.Count == 0)
                    return;

                calculationSet.AttributeNames.AddRange(coefficients.Select(x => x.AttributeName).ToList());
                calculationSet.Coefficients.AddRange(coefficients.Select(x => x.Coefficient.Value).ToList());
            });

            return calculationSet;
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

		private List<OMModelToMarketObjects> GetModelMarketObjects(long modelId)
        {
            return OMModelToMarketObjects
                .Where(x => x.ModelId == modelId && (x.IsExcluded == null || x.IsExcluded == false)).SelectAll()
                .Execute();
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
