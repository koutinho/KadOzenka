﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
using GemBox.Spreadsheet;
using GroupDto = KadOzenka.Dal.Modeling.Dto.GroupDto;

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
					},
                    new QSJoin
                    {
                        RegisterId = OMGroup.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMModelingModel.GetColumn(x => x.GroupId),
                            RightOperand = OMGroup.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
			};
			query.AddColumn(OMModelingModel.GetColumn(x => x.Name, nameof(ModelingModelDto.Name)));
			query.AddColumn(OMModelingModel.GetColumn(x => x.TourId, nameof(ModelingModelDto.TourId)));
			query.AddColumn(OMTour.GetColumn(x => x.Year, nameof(ModelingModelDto.TourYear)));
			query.AddColumn(OMModelingModel.GetColumn(x => x.GroupId, nameof(ModelingModelDto.GroupId)));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(ModelingModelDto.GroupName)));
            query.AddColumn(OMModelingModel.GetColumn(x => x.WasTrained, nameof(ModelingModelDto.WasTrained)));
            query.AddColumn(OMModelingModel.GetColumn(x => x.IsOksObjectType, nameof(ModelingModelDto.IsOksObjectType)));

            var table = query.ExecuteQuery();
			ModelingModelDto model = null;
			if (table.Rows.Count != 0)
			{
				var row = table.Rows[0];

				var modelName = row[nameof(ModelingModelDto.Name)].ParseToString();
				var tourId = row[nameof(ModelingModelDto.TourId)].ParseToLong();
				var tourYear = row[nameof(ModelingModelDto.TourYear)].ParseToLong();
				var groupId = row[nameof(ModelingModelDto.GroupId)].ParseToLong();
                var groupName = row[nameof(ModelingModelDto.GroupName)].ParseToString();
                var wasTrained = row[nameof(ModelingModelDto.WasTrained)].ParseToBooleanNullable();
                var isOksObjectType = row[nameof(ModelingModelDto.IsOksObjectType)].ParseToBooleanNullable();

                model = new ModelingModelDto
				{
					ModelId = modelId,
					Name = modelName,
					TourId = tourId,
					TourYear = tourYear,
                    GroupId = groupId,
                    GroupName = groupName,
                    WasTrained = wasTrained.GetValueOrDefault(),
                    IsOksObjectType = isOksObjectType.GetValueOrDefault()
                };
			}

			if (model == null)
				throw new Exception($"Не найдена модель с Id='{modelId}'");

			return model;
		}

        public List<GroupDto> GetGroups()
        {
            var query = new QSQuery
            {
                MainRegisterID = ObjectModel.Ko.OMGroupToMarketSegmentRelation.GetRegisterId(),
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMGroup.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMGroup.GetColumn(x => x.Id),
                            RightOperand = ObjectModel.Ko.OMGroupToMarketSegmentRelation.GetColumn(x => x.GroupId)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };
            query.AddColumn(ObjectModel.Ko.OMGroupToMarketSegmentRelation.GetColumn(x => x.GroupId, nameof(GroupDto.GroupId)));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(GroupDto.Name)));
            query.OrderBy.Add(new QSOrder
            {
                Column = OMGroup.GetColumn(x => x.GroupName),
                Order = QSOrderType.ASC
            });

            return query.ExecuteQuery<GroupDto>();
        }

        public List<ModelAttributeRelationDto> GetModelAttributes(long modelId)
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
            query.AddColumn(OMModelAttributesRelation.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.Id)));
            query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelAttributeRelationDto.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.AttributeName)));
			query.AddColumn(OMEsReference.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.DictionaryId)));
			query.AddColumn(OMEsReference.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.DictionaryName)));
            query.AddColumn(OMModelAttributesRelation.GetColumn(x => x.Coefficient, nameof(ModelAttributeRelationDto.Coefficient)));

            var attributes = new List<ModelAttributeRelationDto>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];

                var id = row[nameof(ModelAttributeRelationDto.Id)].ParseToLong();

                var registerId = row[nameof(ModelAttributeRelationDto.RegisterId)].ParseToLong();

				var attributeId = row[nameof(ModelAttributeRelationDto.AttributeId)].ParseToLong();
				var attributeName = row[nameof(ModelAttributeRelationDto.AttributeName)].ParseToString();

				var dictionaryId = row[nameof(ModelAttributeRelationDto.DictionaryId)].ParseToLongNullable();
				var dictionaryName = row[nameof(ModelAttributeRelationDto.DictionaryName)].ParseToString();

                var coefficientFromModel = row[nameof(ModelAttributeRelationDto.Coefficient)].ParseToDecimalNullable();

                attributes.Add(new ModelAttributeRelationDto
				{
                    Id = id,
					RegisterId = registerId,
					AttributeId = attributeId,
					AttributeName = attributeName,
					DictionaryId = dictionaryId,
					DictionaryName = dictionaryName,
                    Coefficient = coefficientFromModel
                });
			}

			return attributes;
		}

		public int AddModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

            using (var ts = new TransactionScope())
            {
                var model = new OMModelingModel
                {
                    Name = modelDto.Name,
                    TourId = modelDto.TourId,
                    GroupId = modelDto.GroupId
                };

                var id = model.Save();
                model.InternalName = $"model_{id}";
                model.Save();

                ts.Complete();

                return id;
            }
        }

		public bool UpdateModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			var existedModel = GetModelByIdInternal(modelDto.ModelId);

			var newAttributes = modelDto.Attributes ?? new List<ModelAttributeRelationDto>();
			var existedModelAttributes = OMModelAttributesRelation.Where(x => x.ModelId == modelDto.ModelId).SelectAll().Execute();

            var isModelChanged = IsModelChanged(existedModel, modelDto, existedModelAttributes, newAttributes);

            using (var ts = new TransactionScope())
            {
                existedModel.Name = modelDto.Name;
                existedModel.TourId = modelDto.TourId;
                existedModel.GroupId = modelDto.GroupId;
                existedModel.IsOksObjectType = modelDto.IsOksObjectType;
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

        private bool IsModelChanged(OMModelingModel existedModel, ModelingModelDto newModel, List<OMModelAttributesRelation> existedAttributes, List<ModelAttributeRelationDto> newAttributes)
        {
            var oldAttributeIds = existedAttributes.Select(x => x.AttributeId).OrderBy(x => x);
            var newAttributeIds = newAttributes.Select(x => x.AttributeId).OrderBy(x => x);
            var areAttributeIdsEqual = oldAttributeIds.SequenceEqual(newAttributeIds);

            var oldDictionaryIds = existedAttributes.Select(x => x.DictionaryId).OrderBy(x => x);
            var newDictionaryIIds = newAttributes.Select(x => x.DictionaryId).OrderBy(x => x);
            var areDictionaryIdsSequenceEqualEqual = oldDictionaryIds.SequenceEqual(newDictionaryIIds);

            return !(existedModel.TourId == newModel.TourId &&
                   existedModel.GroupId == newModel.GroupId &&
                   existedModel.IsOksObjectType == newModel.IsOksObjectType &&
                   areAttributeIdsEqual &&
                   areDictionaryIdsSequenceEqualEqual);
        }

		#endregion


		#region Model Object Relations

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

        public Stream GetLogs(long modelId)
        {
            var modelAttributes = GetModelAttributes(modelId);

            var modelMarketObjects = OMModelToMarketObjects
                .Where(x => x.ModelId == modelId && x.Coefficients != null && x.IsExcluded.Coalesce(false) == false)
                .Select(x => x.CadastralNumber)
                .Select(x => x.Coefficients)
                .Execute();

            var excelTemplate = new ExcelFile();
            var mainWorkSheet = excelTemplate.Worksheets.Add("Логи");

            var columnHeaders = new List<object> {"Кадастровый номер"};
            columnHeaders.AddRange(modelAttributes.Select(x => x.AttributeName).ToList());

            AddRow(mainWorkSheet, 0, columnHeaders.ToArray());

            var rowCounter = 1;
            modelMarketObjects.ForEach(modelMarketObject =>
            {
                var coefficients = modelMarketObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
                if (coefficients.All(x => string.IsNullOrWhiteSpace(x.Message)))
                    return;

                var values = new List<object> {modelMarketObject.CadastralNumber};

                modelAttributes.ForEach(attribute =>
                {
                    var message = coefficients.FirstOrDefault(x => x.AttributeId == attribute.AttributeId)?.Message;
                    values.Add(message);
                });

                AddRow(mainWorkSheet, rowCounter++, values.ToArray());
            });


            var stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }


        private void AddRow(ExcelWorksheet sheet, int row, object[] values)
        {
            var col = 0;
            foreach (object value in values)
            {
                switch (value)
                {
                    case decimal _:
                    case double _:
                        sheet.Rows[row].Cells[col].SetValue(Convert.ToDouble(value));
                        sheet.Rows[row].Cells[col].Style.NumberFormat = "#,##0.00";
                        break;
                    case DateTime _:
                        sheet.Rows[row].Cells[col].SetValue(Convert.ToDateTime(value));
                        sheet.Rows[row].Cells[col].Style.NumberFormat = "mm/dd/yyyy";
                        break;
                    default:
                        sheet.Rows[row].Cells[col].SetValue(value as string);
                        break;
                }

                sheet.Rows[row].Cells[col].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                col++;
            }
        }

        #endregion


        #region Modeling Process

        public void CreateObjectsForModel(long modelId)
        {
            var model = GetModelByIdInternal(modelId);

            var groupToMarketSegmentRelation = ObjectModel.Ko.OMGroupToMarketSegmentRelation
                .Where(x => x.GroupId == model.GroupId)
                .Select(x => x.MarketSegment_Code)
                .Select(x => x.TerritoryType_Code)
                .ExecuteFirstOrDefault();

            //var territoryCondition = GetConditionForTerritoryType(groupToMarketSegmentRelation.TerritoryType_Code);

            var groupedObjects = OMCoreObject.Where(x =>
					x.PropertyMarketSegment_Code == groupToMarketSegmentRelation.MarketSegment_Code &&
                    x.CadastralNumber != null && 
                    x.ProcessType_Code != ProcessStep.Excluded)
                //TODO PART
                //.And(territoryCondition)
				.Select(x => x.CadastralNumber)
				.Select(x => x.Price)
                .GroupBy(x => new
                {
                    x.CadastralNumber,
                    x.Price
                })
                .ExecuteSelect(x => new
                {
                    x.CadastralNumber,
                    x.Price
                });

            DestroyModelMarketObjects(modelId);

            var modelAttributes = GetModelAttributes(modelId);

            var i = 0;
            Parallel.ForEach(groupedObjects, groupedObj =>
            {
                var isForTraining = i < groupedObjects.Count / 2;
                i++;

                var modelObject = new OMModelToMarketObjects
                {
                    ModelId = modelId,
                    CadastralNumber = groupedObj.CadastralNumber,
                    Price = groupedObj.Price ?? 0,
                    IsForTraining = isForTraining
                };

                var objectCoefficients = GetCoefficientsForObject(model.TourId,
                    modelObject.CadastralNumber, modelAttributes);

                modelObject.Coefficients = objectCoefficients.SerializeToXml();
                modelObject.Save();
            });
        }

        private Expression<Func<OMCoreObject, bool>> GetConditionForTerritoryType(TerritoryType territoryType)
        {
            switch (territoryType)
            {
                case TerritoryType.Main:
                    Expression<Func<OMCoreObject, bool>> mainTerritoryCondition = x => x.Address == "Main";
                    return mainTerritoryCondition;
                case TerritoryType.Additional:
                    Expression<Func<OMCoreObject, bool>> additionalTerritoryCondition = x => x.Address == "Additional";
                    return additionalTerritoryCondition;
                case TerritoryType.MainAndAdditional:
                    Expression<Func<OMCoreObject, bool>> bothTerritoryCondition = x => x.Address == "MainAndAdditional";
                    return bothTerritoryCondition;
                default:
                    Expression<Func<OMCoreObject, bool>> unknownTerritoryCondition = x => x.Address == "default";
                    return unknownTerritoryCondition;
            }
        }

		private List<CoefficientForObject> GetCoefficientsForObject(long tourId, string cadastralNumber, List<ModelAttributeRelationDto> modelAttributes)
        {
            var coefficients = new List<CoefficientForObject>();
            modelAttributes.ForEach(modelAttribute =>
            {
                CoefficientForObject coefficient;
                var objectParameterData = GetParameterDataByCadastralNumber(cadastralNumber, (int) tourId,
                    (int) modelAttribute.AttributeId, (int) modelAttribute.RegisterId);
                if (objectParameterData == null)
                {
                    coefficient = new CoefficientForObject(modelAttribute.AttributeId)
                    {
                        Message = "Не найдено значение."
                    };
                }
                else
                {
                    coefficient = CalculateCoefficient(objectParameterData, modelAttribute);
                }

                coefficients.Add(coefficient);
            });

            return coefficients;
        }

        public List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, bool isForTraining)
        {
            return OMModelToMarketObjects
                .Where(x => x.ModelId == modelId && x.IsExcluded.Coalesce(false) == false &&
                            x.IsForTraining.Coalesce(false) == isForTraining).SelectAll().Execute();
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
                Coefficients = entity.Coefficients.DeserializeFromXml<List<CoefficientForObject>>()
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

			if(modelDto.GroupId == 0)
				message.AppendLine("Для модели не выбрана группа");

			if (message.Length != 0)
				throw new Exception(message.ToString());
		}

		private void DestroyModelMarketObjects(long modelId)
        {
            var existedModelObjects = OMModelToMarketObjects.Where(x => x.ModelId == modelId).Execute();
            existedModelObjects.ForEach(x => x.Destroy());
        }

        private ParameterDataDto GetParameterDataByCadastralNumber(string kn, int tourId, int attributeId, int registerId)
		{
			var unitsIds = ScoreCommonService.GetUnitsIdsByCadastralNumber(kn, tourId);
			return unitsIds.Count > 0 ? ScoreCommonService.GetParameters(unitsIds, attributeId, registerId) : null;
		}

		private CoefficientForObject CalculateCoefficient(ParameterDataDto objectParameterData, ModelAttributeRelationDto modelAttribute)
        {
            var coefficient = new CoefficientForObject(modelAttribute.AttributeId);

            switch (objectParameterData.Type)
			{
				case ParameterType.String:
				{
					if (modelAttribute.DictionaryId == null)
					{
                        coefficient.Message = GetErrorMessage("строка");
					}
					else
                    {
                        coefficient.Value = objectParameterData.StringValue;
                        coefficient.Coefficient = ScoreCommonService.GetCoefficientFromStringFactor(objectParameterData,
							(int)modelAttribute.DictionaryId.GetValueOrDefault());
					}

					break;
				}
				case ParameterType.Date:
				{
					if (modelAttribute.DictionaryId == null)
					{
                        coefficient.Message = GetErrorMessage("дата");
					}
					else
					{
                        coefficient.Value = objectParameterData.DateValue.ToShortDateString();
                        coefficient.Coefficient = ScoreCommonService.GetCoefficientFromDateFactor(objectParameterData,
							(int)modelAttribute.DictionaryId.GetValueOrDefault());
					}

					break;
				}
				case ParameterType.Number:
				{
                    var number = ScoreCommonService.GetCoefficientFromNumberFactor(objectParameterData,
                        (int)modelAttribute.DictionaryId.GetValueOrDefault());

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
	}
}
