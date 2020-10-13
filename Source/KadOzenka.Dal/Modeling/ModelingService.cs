using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using GemBox.Spreadsheet;
using Kendo.Mvc.Extensions;
using GroupDto = KadOzenka.Dal.Modeling.Dto.GroupDto;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingService
	{
		public DictionaryService DictionaryService { get; set; }

		public ModelingService(DictionaryService dictionaryService)
		{
			DictionaryService = dictionaryService;
		}

        #region CRUD Model

        public OMModel GetModelEntityByGroupId(long? groupId)
        {
	        if (groupId == null)
		        throw new Exception("Не передан идентификатор Группы для поиска модели");

	        var model = OMModel.Where(x => x.GroupId == groupId).SelectAll().ExecuteFirstOrDefault();
	        if (model == null)
		        throw new Exception($"Не найдена модель для Группы с id='{groupId}'");

	        return model;
        }

        public OMModel GetModelEntityById(long? modelId)
        {
	        if (modelId == null)
		        throw new Exception("Не передан идентификатор Модели для поиска");

	        var model = OMModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
	        if (model == null)
		        throw new Exception($"Не найдена Модель с id='{modelId}'");

	        return model;
        }

        public ModelingModelDto GetModelById(long modelId)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMModel.GetRegisterId(),
				Condition = new QSConditionSimple(OMModel.GetColumn(x => x.Id), QSConditionType.Equal, modelId),
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMTour.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMModel.GetColumn(x => x.TourId),
							RightOperand = OMTour.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Left
					},
                    new QSJoin
                    {
                        RegisterId = OMGroup.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMModel.GetColumn(x => x.GroupId),
                            RightOperand = OMGroup.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Left
                    }
                }
			};
			query.AddColumn(OMModel.GetColumn(x => x.Description, nameof(ModelingModelDto.Description)));
			query.AddColumn(OMModel.GetColumn(x => x.Name, nameof(ModelingModelDto.Name)));
            query.AddColumn(OMModel.GetColumn(x => x.LinearTrainingResult, nameof(ModelingModelDto.LinearTrainingResult)));
            query.AddColumn(OMModel.GetColumn(x => x.ExponentialTrainingResult, nameof(ModelingModelDto.ExponentialTrainingResult)));
            query.AddColumn(OMModel.GetColumn(x => x.MultiplicativeTrainingResult, nameof(ModelingModelDto.MultiplicativeTrainingResult)));
            query.AddColumn(OMModel.GetColumn(x => x.TourId, nameof(ModelingModelDto.TourId)));
            query.AddColumn(OMTour.GetColumn(x => x.Year, nameof(ModelingModelDto.TourYear)));
			query.AddColumn(OMModel.GetColumn(x => x.GroupId, nameof(ModelingModelDto.GroupId)));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(ModelingModelDto.GroupName)));
            query.AddColumn(OMModel.GetColumn(x => x.IsOksObjectType, nameof(ModelingModelDto.IsOksObjectType)));

            var table = query.ExecuteQuery();
			ModelingModelDto model = null;
			if (table.Rows.Count != 0)
			{
				var row = table.Rows[0];

				var modelDescription = row[nameof(ModelingModelDto.Description)].ParseToString();
				var modelName = row[nameof(ModelingModelDto.Name)].ParseToString();
				var linearTrainingResult = row[nameof(ModelingModelDto.LinearTrainingResult)].ParseToString();
				var exponentialTrainingResult = row[nameof(ModelingModelDto.ExponentialTrainingResult)].ParseToString();
				var multiplicativeTrainingResult = row[nameof(ModelingModelDto.MultiplicativeTrainingResult)].ParseToString();
				var tourId = row[nameof(ModelingModelDto.TourId)].ParseToLong();
				var tourYear = row[nameof(ModelingModelDto.TourYear)].ParseToLong();
				var groupId = row[nameof(ModelingModelDto.GroupId)].ParseToLong();
                var groupName = row[nameof(ModelingModelDto.GroupName)].ParseToString();
                var isOksObjectType = row[nameof(ModelingModelDto.IsOksObjectType)].ParseToBooleanNullable();

                model = new ModelingModelDto
				{
					ModelId = modelId,
					Name = modelName,
                    Description = modelDescription,
                    LinearTrainingResult = linearTrainingResult,
                    ExponentialTrainingResult = exponentialTrainingResult,
                    MultiplicativeTrainingResult = multiplicativeTrainingResult,
					TourId = tourId,
					TourYear = tourYear,
                    GroupId = groupId,
                    GroupName = groupName,
                    IsOksObjectType = isOksObjectType.GetValueOrDefault()
                };
			}

			if (model == null)
				throw new Exception($"Не найдена модель с Id='{modelId}'");

			return model;
		}

        public List<GroupDto> GetGroups(long tourId)
        {
            var groupsInTour = OMTourGroup.Where(x => x.TourId == tourId).Select(x => x.GroupId).Execute()
                .Select(x => x.GroupId).ToList();
            if (groupsInTour.Count == 0)
                return new List<GroupDto>();

            var groupsToMarketSegmentInTour = ObjectModel.Ko.OMGroupToMarketSegmentRelation.Where(x => groupsInTour.Contains(x.GroupId))
                .Select(x => x.GroupId)
                .Select(x => x.ParentGroup.GroupName)
                .Execute().Select(
                    x => new GroupDto
                    {
                        GroupId = x.GroupId,
                        Name = x.ParentGroup?.GroupName
                    }).OrderBy(x => x.Name).ToList();

            return groupsToMarketSegmentInTour;
        }

        public int AddModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			return new OMModel
			{
				Name = modelDto.Name,
                Description = modelDto.Description,
				TourId = modelDto.TourId,
				GroupId = modelDto.GroupId,
				Formula = "-",
                AlgoritmType_Code = KoAlgoritmType.None
			}.Save();
		}

		public bool UpdateModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			var existedModel = GetModelEntityById(modelDto.ModelId);

			var newAttributes = modelDto.Attributes ?? new List<ModelAttributeRelationDto>();
			var existedModelAttributes = OMModelFactor.Where(x => x.ModelId == modelDto.ModelId).SelectAll().Execute();

            var isModelChanged = IsModelChanged(existedModel, modelDto, existedModelAttributes, newAttributes);

            using (var ts = new TransactionScope())
            {
                existedModel.Name = modelDto.Name;
                existedModel.Description = modelDto.Description;
                existedModel.TourId = modelDto.TourId;
                existedModel.GroupId = modelDto.GroupId;
                existedModel.IsOksObjectType = modelDto.IsOksObjectType;
                if (isModelChanged)
                {
                    existedModel.LinearTrainingResult = null;
                    existedModel.ExponentialTrainingResult = null;
                    existedModel.MultiplicativeTrainingResult = null;
                }
                existedModel.Save();

                existedModelAttributes.ForEach(x => x.Destroy());
                newAttributes.ForEach(newAttribute =>
                {
                    new OMModelFactor
                    {
                        ModelId = modelDto.ModelId,
                        FactorId = newAttribute.AttributeId,
                        DictionaryId = newAttribute.DictionaryId,
                        MarkerId = -1
                    }.Save();
                });

                ts.Complete();
            }

            return isModelChanged;
        }

        private bool IsModelChanged(OMModel existedModel, ModelingModelDto newModel, List<OMModelFactor> existedAttributes, List<ModelAttributeRelationDto> newAttributes)
        {
            var oldAttributeIds = existedAttributes.Select(x => x.FactorId).OrderBy(x => x);
            var newAttributeIds = newAttributes.Select(x => (long?)x.AttributeId).OrderBy(x => x);
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


        #region Model Attributes

        public List<ModelAttributeRelationDto> GetModelFactors(long modelId)
        {
	        var dictionaryJoin = new QSJoin
	        {
		        RegisterId = OMModelingDictionary.GetRegisterId(),
		        JoinCondition = new QSConditionSimple
		        {
			        ConditionType = QSConditionType.Equal,
			        LeftOperand = OMModelFactor.GetColumn(x => x.DictionaryId),
			        RightOperand = OMModelingDictionary.GetColumn(x => x.Id)
		        },
		        JoinType = QSJoinType.Left
	        };
	        var query = GetModelFactorsQuery(modelId, dictionaryJoin);
            
            query.AddColumn(OMModelFactor.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.Id)));
            query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelAttributeRelationDto.RegisterId)));
            query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.AttributeId)));
            query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.AttributeName)));
            query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelAttributeRelationDto.AttributeType)));
            query.AddColumn(OMModelingDictionary.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.DictionaryId)));
            query.AddColumn(OMModelingDictionary.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.DictionaryName)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.Weight, nameof(ModelAttributeRelationDto.Coefficient)));

            var attributes = new List<ModelAttributeRelationDto>();
            var table = query.ExecuteQuery();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                var id = row[nameof(ModelAttributeRelationDto.Id)].ParseToLong();

                var registerId = row[nameof(ModelAttributeRelationDto.RegisterId)].ParseToLong();

                var attributeId = row[nameof(ModelAttributeRelationDto.AttributeId)].ParseToLong();
                var attributeName = row[nameof(ModelAttributeRelationDto.AttributeName)].ParseToString();
                var attributeType = row[nameof(ModelAttributeRelationDto.AttributeType)].ParseToInt();

                var dictionaryId = row[nameof(ModelAttributeRelationDto.DictionaryId)].ParseToLongNullable();
                var dictionaryName = row[nameof(ModelAttributeRelationDto.DictionaryName)].ParseToString();

                var coefficientFromModel = row[nameof(ModelAttributeRelationDto.Coefficient)].ParseToDecimalNullable();

                attributes.Add(new ModelAttributeRelationDto
                {
                    Id = id,
                    RegisterId = registerId,
                    AttributeId = attributeId,
                    AttributeName = attributeName,
                    AttributeType = attributeType,
                    DictionaryId = dictionaryId,
                    DictionaryName = dictionaryName,
                    Coefficient = coefficientFromModel
                });
            }

            return attributes;
        }

        public QSQuery GetModelFactorsQuery(long modelId, QSJoin additionalJoin = null)
        {
	        var query = new QSQuery
	        {
		        MainRegisterID = OMModelFactor.GetRegisterId(),
		        Condition = new QSConditionGroup
		        {
			        Type = QSConditionGroupType.And,
			        Conditions = new List<QSCondition>
			        {
				        new QSConditionSimple(OMModelFactor.GetColumn(x => x.ModelId), QSConditionType.Equal, modelId)
			        }
		        },
		        Joins = new List<QSJoin>
		        {
			        new QSJoin
			        {
				        RegisterId = OMAttribute.GetRegisterId(),
				        JoinCondition = new QSConditionSimple
				        {
					        ConditionType = QSConditionType.Equal,
					        LeftOperand = OMModelFactor.GetColumn(x => x.FactorId),
					        RightOperand = OMAttribute.GetColumn(x => x.Id)
				        },
				        JoinType = QSJoinType.Inner
			        }
		        },
		        OrderBy = new List<QSOrder>
		        {
			        new QSOrder
			        {
				        Column = OMAttribute.GetColumn(x => x.Name),
				        Order = QSOrderType.ASC
			        }
		        }
	        };

            if(additionalJoin != null)
                query.Joins.Add(additionalJoin);

	        return query;
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
            var modelAttributes = GetModelFactors(modelId);

            var modelMarketObjects = OMModelToMarketObjects
                .Where(x => x.ModelId == modelId && x.Coefficients != null && x.IsExcluded.Coalesce(false) == false)
                .Select(x => x.CadastralNumber)
                .Select(x => x.Coefficients)
                .Execute();

            var excelTemplate = new ExcelFile();
            var mainWorkSheet = excelTemplate.Worksheets.Add("Логи");

            var columnHeaders = new List<object> {"Кадастровый номер"};
            columnHeaders.AddRange(modelAttributes.Select(x => x.AttributeName).ToList());

            AddRowToExcel(mainWorkSheet, 0, columnHeaders.ToArray());

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

                AddRowToExcel(mainWorkSheet, rowCounter++, values.ToArray());
            });

            var stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public Stream ExportMarketObjectsToExcel(List<long> marketObjectIds, long modelId)
        {
            var modelAttributes = GetModelFactors(modelId);

            var excelTemplate = new ExcelFile();
            var mainWorkSheet = excelTemplate.Worksheets.Add("Объекты модели");

            var columnHeaders = new List<object>
            {
                "Id", "Исключен из расчета", "Кадастровый номер", "Цена", "Спрогнозированная цена",
                "Объект для обучения"
            };
            columnHeaders.AddRange(modelAttributes.Select(x => x.AttributeName).ToList());

            AddRowToExcel(mainWorkSheet, 0, columnHeaders.ToArray());

            if (marketObjectIds != null && marketObjectIds.Count > 0)
            {
                var rowCounter = 1;
                var marketObjects = OMModelToMarketObjects.Where(x => marketObjectIds.Contains(x.Id)).SelectAll().Execute();
                marketObjectIds.ForEach(id =>
                {
                    var obj = marketObjects.FirstOrDefault(x => x.Id == id);
                    if (obj == null)
                        return;

                    var values = new List<object>
                    {
                        obj.Id, obj.IsExcluded.GetValueOrDefault(), obj.CadastralNumber, obj.Price, obj.PriceFromModel,
                        obj.IsForTraining.GetValueOrDefault()
                    };

                    modelAttributes.ForEach(attribute =>
                    {
                        var coefficients = obj.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
                        var coefficient = coefficients.FirstOrDefault(x => x.AttributeId == attribute?.AttributeId)?.Coefficient;
                        values.Add(coefficient);
                    });

                    AddRowToExcel(mainWorkSheet, rowCounter++, values.ToArray());
                });
            }

            var stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public void ImportModelObjectsFromExcel(ExcelFile file)
        {
            const int idColumnIndex = 0;
            const int isExcludedColumnIndex = 1;

            var rows = file.Worksheets[0].Rows;
            var dictionary = new Dictionary<long, bool>();
            for (var i = 1; i < rows.Count; i++)
            {
                var id = rows[i].Cells[idColumnIndex].Value.ParseToLongNullable();
                if (id == null || id == 0)
                    continue;

                var isExcludedStr = rows[i].Cells[isExcludedColumnIndex].Value.ToString();
                var isExcluded = isExcludedStr?.ToLower() == "да";

                dictionary[id.Value] = isExcluded;
            }

            var modelObjects = OMModelToMarketObjects.Where(x => dictionary.Keys.Contains(x.Id))
                .Select(x => x.IsExcluded)
                .Execute();

            foreach (var keyValuePair in dictionary)
            {
                var modelObject = modelObjects.FirstOrDefault(x => x.Id == keyValuePair.Key);
                if (modelObject == null || modelObject.IsExcluded.GetValueOrDefault() == keyValuePair.Value)
                    continue;

                modelObject.IsExcluded = keyValuePair.Value;
                modelObject.Save();
            }
        }


        #region Support Methods

        private void AddRowToExcel(ExcelWorksheet sheet, int row, object[] values)
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
                    case bool _:
                        var res = Convert.ToBoolean(value) ? "Да" : "Нет";
                        sheet.Rows[row].Cells[col].SetValue(res);
                        break;
                    default:
                        var defaultValue = value?.ToString();
                        sheet.Rows[row].Cells[col].SetValue(defaultValue);
                        break;
                }

                sheet.Rows[row].Cells[col].Style.Borders.SetBorders(MultipleBorders.All,
                    SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

                col++;
            }
        }

        #endregion

        #endregion


        #region Modeling Process

        public Expression<Func<OMCoreObject, bool>> GetConditionForTerritoryType(TerritoryType territoryType)
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

        public List<OMModelingDictionary> GetDictionaries(List<ModelAttributeRelationDto> modelAttributes)
        {
	        var dictionaryIds = modelAttributes?.Where(x => x.DictionaryId != null).Select(x => x.DictionaryId.Value)
		        .Distinct().ToList();
           
            return DictionaryService.GetDictionaries(dictionaryIds);
        }

        public Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromMarketObject(List<long> objectIds, List<OMModelingDictionary> dictionaries,
	        List<ModelAttributeRelationDto> modelAttributes)
        {
            if(modelAttributes == null || modelAttributes.Count == 0)
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

        public Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromTourFactors(List<long> unitIds, List<OMModelingDictionary> dictionaries,
	        List<GroupedModelAttributes> modelAttributes)
        {
	        var coefficients = new Dictionary<long, List<CoefficientForObject>>();

	        modelAttributes.ForEach(modelAttribute =>
	        {
		        var idAttribute = RegisterCache.RegisterAttributes.Values
			        .FirstOrDefault(x => x.RegisterId == modelAttribute.RegisterId && x.IsPrimaryKey)?.Id;

		        var query = new QSQuery
		        {
			        MainRegisterID = modelAttribute.RegisterId,
			        Condition = new QSConditionSimple
			        {
				        ConditionType = QSConditionType.In,
				        LeftOperand = new QSColumnSimple((int)idAttribute),
				        RightOperand = new QSColumnConstant(unitIds)
			        }
		        };

		        var currentCoefficients = GetCoefficients(query, dictionaries, modelAttribute.Attributes);
		        coefficients.AddRange(currentCoefficients);
	        });

            return coefficients;
        }

        public List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, bool isForTraining)
        {
            return OMModelToMarketObjects
                .Where(x => x.ModelId == modelId && x.IsExcluded.Coalesce(false) == false &&
                            x.IsForTraining.Coalesce(false) == isForTraining).SelectAll().Execute();
        }

        public void DestroyModelMarketObjects(long modelId)
        {
            var existedModelObjects = OMModelToMarketObjects.Where(x => x.ModelId == modelId).Execute();
            existedModelObjects.ForEach(x => x.Destroy());
        }

        #endregion


        #region Support Methods

        private ModelMarketObjectRelationDto ToDto(OMModelToMarketObjects entity)
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
				message.AppendLine("У модели не заполнено Имя");
			if (string.IsNullOrWhiteSpace(modelDto.Description))
				message.AppendLine("У модели не заполнено Описание");

			var isTourExists = OMTour.Where(x => x.Id == modelDto.TourId).ExecuteExists();
			if(!isTourExists)
				message.AppendLine($"Не найден Тур с Id='{modelDto.TourId}'");

			if(modelDto.GroupId == 0)
				message.AppendLine("Для модели не выбрана группа");

            var isGroupBelongToTour = OMTourGroup.Where(x => x.TourId == modelDto.TourId && x.GroupId == modelDto.GroupId).ExecuteExists();
            if (!isGroupBelongToTour)
                message.AppendLine($"Группа c Id='{modelDto.GroupId}'не принадлежит туру с Id='{modelDto.TourId}'");

            if (message.Length != 0)
				throw new Exception(message.ToString());
		}

		private Dictionary<long, List<CoefficientForObject>> GetCoefficients(QSQuery query, List<OMModelingDictionary> dictionaries, List<ModelAttributeRelationDto> attributes)
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

        private CoefficientForObject CalculateCoefficientViaDictionary(object value, ModelAttributeRelationDto modelAttribute, OMModelingDictionary dictionary)
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

        public class GroupedModelAttributes
        {
	        public int RegisterId { get; set; }
	        public List<ModelAttributeRelationDto> Attributes { get; set; }

	        public GroupedModelAttributes()
	        {
		        Attributes = new List<ModelAttributeRelationDto>();
	        }
        }

        #endregion
    }
}
