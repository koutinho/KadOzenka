﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Modeling.Dto.Factors;
using Kendo.Mvc.Extensions;
using ObjectModel.Directory.ES;

namespace KadOzenka.Dal.Modeling
{
	public class AutomaticModelingService : BaseModelingService
    {
	    #region CRUD Model

	    public override void AddModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			var model = new OMModel
			{
				Name = modelDto.Name,
				Description = modelDto.Description,
				GroupId = modelDto.GroupId,
                CalculationType_Code = KoCalculationType.Comparative, 
				AlgoritmType_Code = KoAlgoritmType.None,
                Type_Code = KoModelType.Automatic
			};

			model.Formula = model.GetFormulaFull(true);

            model.Save();
        }

		public override bool UpdateModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			var existedModel = GetModelEntityById(modelDto.ModelId);

			var newAttributes = modelDto.Attributes ?? new List<ModelAttributeRelationDto>();
			var existedModelAttributes = ModelFactorsService.GetFactors(existedModel.Id, existedModel.AlgoritmType_Code);

            var isModelChanged = IsModelChanged(existedModel, modelDto, existedModelAttributes, newAttributes);

            using (var ts = new TransactionScope())
            {
	            existedModel.Name = modelDto.Name;
                existedModel.Description = modelDto.Description;
                existedModel.GroupId = modelDto.GroupId;
                existedModel.IsOksObjectType = modelDto.IsOksObjectType;
                existedModel.Formula = "-";

                if (isModelChanged)
                {
	                ResetTrainingResults(existedModel, KoAlgoritmType.None);

	                existedModelAttributes.ForEach(x =>
	                {
		                x.Weight = 0;
		                x.Save();
	                });
                }

                existedModel.Save();

                AddModelAttributes(existedModel.Id, existedModel.AlgoritmType_Code, newAttributes);

                ts.Complete();
            }

            return isModelChanged;
        }

		public void ResetTrainingResults(OMModel generalModel, KoAlgoritmType type)
		{
			switch (type)
			{
				case KoAlgoritmType.None:
					generalModel.LinearTrainingResult = null;
					generalModel.ExponentialTrainingResult = null;
					generalModel.MultiplicativeTrainingResult = null;
					break;
				case KoAlgoritmType.Exp:
					generalModel.ExponentialTrainingResult = null;
					break;
				case KoAlgoritmType.Line:
					generalModel.LinearTrainingResult = null;
					break;
				case KoAlgoritmType.Multi:
					generalModel.MultiplicativeTrainingResult = null;
					break;
			}
		}


		#region Support

        private bool IsModelChanged(OMModel existedModel, ModelingModelDto newModel, List<OMModelFactor> existedAttributes, List<ModelAttributeRelationDto> newAttributes)
		{
			var oldAttributeIds = existedAttributes.Select(x => x.FactorId).OrderBy(x => x);
			var newAttributeIds = newAttributes.Select(x => (long?)x.AttributeId).OrderBy(x => x);
			var areAttributeIdsEqual = oldAttributeIds.SequenceEqual(newAttributeIds);

			var oldDictionaryIds = existedAttributes.Select(x => x.DictionaryId).OrderBy(x => x);
			var newDictionaryIIds = newAttributes.Select(x => x.DictionaryId).OrderBy(x => x);
			var areDictionaryIdsSequenceEqualEqual = oldDictionaryIds.SequenceEqual(newDictionaryIIds);

			return !(existedModel.GroupId == newModel.GroupId &&
			         existedModel.IsOksObjectType == newModel.IsOksObjectType &&
			         areAttributeIdsEqual && areDictionaryIdsSequenceEqualEqual);
		}

		public void AddModelAttributes(long? generalModelId, KoAlgoritmType type, List<ModelAttributeRelationDto> attributes)
		{
			if (attributes == null || attributes.Count == 0)
				return;

			var model = OMModel.Where(x => x.Id == generalModelId).Select(x => new
			{
				x.ParentGroup.Id,
				x.ParentGroup.GroupName
			}).ExecuteFirstOrDefault();
			if (model == null)
				throw new Exception($"Не найдена модель с ИД '{generalModelId}'");
			if (model.ParentGroup == null)
				throw new Exception($"Для модели '{model.Name}' (ИД='{generalModelId}') не найдена группа");

			var tour = OMTourGroup.Where(x => x.GroupId == model.ParentGroup.Id).Select(x => new
			{
				x.ParentTour.Id,
				x.ParentTour.Year
			}).ExecuteFirstOrDefault()?.ParentTour;
			if (tour == null)
				throw new Exception($"Не найден тур для группы '{model.ParentGroup?.GroupName}' (ИД='{model.ParentGroup.Id}')");
			//в 2016 почти все атрибуты забиты как строки, поэтому его не валидируем
			if (tour.Year == 2016)
				return;

			ValidateAttributes(attributes, model.Id, type);

			attributes.ForEach(x =>
			{
				ModelFactorsService.AddFactor(new ModelFactorDto
				{
					GeneralModelId = generalModelId,
					FactorId = x.AttributeId,
					DictionaryId = x.DictionaryId,
					Type = x.Type
                });
			});
		}

		private void ValidateAttributes(List<ModelAttributeRelationDto> attributes, long generalModelId, KoAlgoritmType type)
		{
			var errors = new List<string>();

			var attributeIds = attributes.Select(x => x.AttributeId).ToList();
			var dictionaryIds = attributes.Select(x => x.DictionaryId).ToList();

			var omAttributes = RegisterCache.RegisterAttributes.Values.Where(x => attributeIds.Contains(x.Id)).ToList();
			var omDictionaries = OMModelingDictionary.Where(x => dictionaryIds.Contains(x.Id)).Select(x => x.Type_Code).Execute();

			foreach (var modelAttribute in attributes)
			{
				var attribute = omAttributes.FirstOrDefault(y => y.Id == modelAttribute.AttributeId);

				var isTheSameAttributeExists = OMModelFactor.Where(x =>
						x.FactorId == attribute.Id && x.ModelId == generalModelId && x.AlgorithmType_Code == type)
					.ExecuteExists();
				if (isTheSameAttributeExists)
					throw new Exception($"Атрибут '{attribute?.Name}' уже был добавлен");

				if ((attribute?.Type == RegisterAttributeType.STRING || attribute?.Type == RegisterAttributeType.DATE) &&
				    modelAttribute.DictionaryId.GetValueOrDefault() == 0)
				{
					errors.Add($"Для атрибута '{attribute.Name}' нужно выбрать словарь");
				}
				if (modelAttribute.DictionaryId.GetValueOrDefault() != 0)
				{
					var dictionaryType = omDictionaries.FirstOrDefault(x => x.Id == modelAttribute.DictionaryId)?.Type_Code;
					switch (attribute?.Type)
					{
						case RegisterAttributeType.STRING:
						{
							if (dictionaryType != ReferenceItemCodeType.String)
								errors.Add(GenerateMessage(attribute.Name, ReferenceItemCodeType.String));
							break;
						}
						case RegisterAttributeType.DATE:
						{
							if (dictionaryType != ReferenceItemCodeType.Date)
								errors.Add(GenerateMessage(attribute.Name, ReferenceItemCodeType.Date));
							break;
						}
					}
				}
			}

			if (errors.Count > 0)
				throw new Exception(string.Join("<br>", errors));
		}

		private string GenerateMessage(string attributeName, ReferenceItemCodeType dictionaryType)
		{
			return $"Выберите словарь типа '{dictionaryType.GetEnumDescription()}' для атрибута '{attributeName}'";
		}

        #endregion

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
            var modelAttributes = ModelFactorsService.GetGeneralModelAttributes(modelId);

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
            var modelAttributes = ModelFactorsService.GetGeneralModelAttributes(modelId);

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

        public Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromMarketObject(List<long> objectIds, List<OMModelingDictionary> dictionaries,
	        List<ModelAttributeRelationDto> modelAttributes)
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

        public Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromTourFactors(List<long> unitIds, List<OMModelingDictionary> dictionaries,
	        List<GroupedModelAttributes> modelAttributes)
        {
	        if (modelAttributes == null || modelAttributes.Count == 0 || unitIds == null || unitIds.Count == 0)
		        return new Dictionary<long, List<CoefficientForObject>>();

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
