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
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Modeling.Dto.Factors;
using Kendo.Mvc.Extensions;
using ObjectModel.Directory.ES;
using ObjectModel.Ko;
using GroupDto = KadOzenka.Dal.Modeling.Dto.GroupDto;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingService
	{
		public ModelFactorsService ModelFactorsService { get; set; }

		public ModelingService(DictionaryService dictionaryService)
		{
			ModelFactorsService = new ModelFactorsService();
		}

        #region CRUD General Model

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
	        var model = OMModel.Where(x => x.Id == modelId)
		        .Select(x => new
		        {
			        x.Description,
			        x.Name,
			        x.LinearTrainingResult,
			        x.ExponentialTrainingResult,
			        x.MultiplicativeTrainingResult,
                    x.GroupId,
			        x.ParentGroup.Id,
			        x.ParentGroup.GroupName,
			        x.IsOksObjectType,
                    x.Type_Code,
                    x.AlgoritmType_Code
		        }).ExecuteFirstOrDefault();

	        if (model == null)
				throw new Exception($"Не найдена модель с Id='{modelId}'");

	        var tour = GetModelTour(model.GroupId);

            return new ModelingModelDto
	        {
		        ModelId = model.Id,
		        Name = model.Name,
		        Description = model.Description,
		        LinearTrainingResult = model.LinearTrainingResult,
		        ExponentialTrainingResult = model.ExponentialTrainingResult,
		        MultiplicativeTrainingResult = model.MultiplicativeTrainingResult,
				TourId = tour.Id,
				TourYear = tour.Year.GetValueOrDefault(),
				GroupId = model.ParentGroup.Id,
		        GroupName = model.ParentGroup.GroupName,
		        IsOksObjectType = model.IsOksObjectType.GetValueOrDefault(),
                Type = model.Type_Code,
                AlgorithmType = model.AlgoritmType_Code
	        };
        }

        public List<GroupDto> GetGroups(long tourId)
        {
            var groupsInTour = OMTourGroup.Where(x => x.TourId == tourId).Select(x => x.GroupId).Execute()
                .Select(x => x.GroupId).ToList();
            if (groupsInTour.Count == 0)
                return new List<GroupDto>();

            var groupsToMarketSegmentInTour = OMGroupToMarketSegmentRelation.Where(x => groupsInTour.Contains(x.GroupId))
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

        public OMTour GetModelTour(long? groupId)
        {
	        var tourToGroupRelation = OMTourGroup.Where(x => x.GroupId == groupId).Select(x => new
	        {
		        x.ParentTour.Id,
		        x.ParentTour.Year
	        }).ExecuteFirstOrDefault();
	        if (tourToGroupRelation?.ParentTour == null)
		        throw new Exception($"Для группы {groupId} не найдено Тура");

	        return tourToGroupRelation.ParentTour;
        }

        public void AddAutomaticModel(ModelingModelDto modelDto)
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

        public void AddManualModel(ModelingModelDto modelDto)
        {
	        ValidateModel(modelDto);

	        var model = new OMModel
	        {
		        Name = modelDto.Name,
		        Description = modelDto.Description,
		        GroupId = modelDto.GroupId,
		        AlgoritmType_Code = modelDto.AlgorithmType,
		        Type_Code = KoModelType.Manual
	        };

	        model.Formula = model.GetFormulaFull(true);

	        model.Save();
        }

        public bool UpdateModel(ModelingModelDto modelDto)
		{
			var newAttributes = modelDto.Attributes ?? new List<ModelAttributeRelationDto>();

            ValidateModel(modelDto);
            ValidateAttributes(newAttributes);

			var existedModel = GetModelEntityById(modelDto.ModelId);
			var existedModelAttributes = ModelFactorsService.GetFactors(existedModel.Id, existedModel.AlgoritmType_Code);

            var isModelChanged = IsModelChanged(existedModel, modelDto, existedModelAttributes, newAttributes);

            using (var ts = new TransactionScope())
            {
	            existedModel.Name = modelDto.Name;
                existedModel.Description = modelDto.Description;
                existedModel.GroupId = modelDto.GroupId;
                existedModel.IsOksObjectType = modelDto.IsOksObjectType;
                if (isModelChanged)
                {
	                ResetTrainingResults(existedModel, KoAlgoritmType.None);
	                existedModelAttributes.ForEach(x => x.Destroy());
	                existedModelAttributes = new List<OMModelFactor>();
                }

                existedModel.Save();

                AddModelAttributes(existedModel.Id, modelDto.TourId, newAttributes, existedModelAttributes);

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

			generalModel.Save();
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

		public void AddModelAttributes(long? generalModelId, long tourId, List<ModelAttributeRelationDto> attributes,
			List<OMModelFactor> existedAttributes)
		{
			if (attributes == null || attributes.Count == 0)
				return;

			var tour = OMTour.Where(x => x.Id == tourId).Select(x => x.Year).ExecuteFirstOrDefault();
			//в 2016 почти все атрибуты забиты как строки, поэтому его не валидируем
			if (tour?.Year == 2016)
				return;

			var existedAttributeIds = existedAttributes.Select(x => x.FactorId);
			var newAttributes = attributes.Where(x => !existedAttributeIds.Contains(x.AttributeId)).ToList();
			var types = ModelFactorsService.GetPossibleTypes(KoAlgoritmType.None);

			newAttributes.ForEach(attribute =>
			{
				types.ForEach(type =>
				{
					ModelFactorsService.AddFactor(new ModelFactorDto
					{
						GeneralModelId = generalModelId,
						FactorId = attribute.AttributeId,
						DictionaryId = attribute.DictionaryId,
						Type = type
					});
                });
			});
		}

		private void ValidateAttributes(List<ModelAttributeRelationDto> attributes)
		{
			var errors = new List<string>();

			var attributeIds = attributes.Select(x => x.AttributeId).ToList();
			var dictionaryIds = attributes.Where(x => x.DictionaryId != null).Select(x => x.DictionaryId).ToList();

			var omAttributes = RegisterCache.RegisterAttributes.Values.Where(x => attributeIds.Contains(x.Id)).ToList();
			var omDictionaries = dictionaryIds.Count > 0
				? OMModelingDictionary.Where(x => dictionaryIds.Contains(x.Id)).Select(x => x.Type_Code).Execute()
				: new List<OMModelingDictionary>();

			foreach (var modelAttribute in attributes)
			{
				var attribute = omAttributes.FirstOrDefault(y => y.Id == modelAttribute.AttributeId);

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

            var isModelExists = OMModel.Where(x => x.Id != modelDto.ModelId && x.GroupId == modelDto.GroupId).ExecuteExists();
            if (isModelExists)
	            message.AppendLine("Модель для данной группы уже существует");

            if(modelDto.Type == KoModelType.Manual && modelDto.AlgorithmType == KoAlgoritmType.None)
	            message.AppendLine($"Для модели типа '{KoModelType.Manual.GetEnumDescription()}' нужно указать Тип алгоритма");

            if (message.Length != 0)
				throw new Exception(message.ToString());
		}

		#endregion
	}
}
