using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Modeling;
using GemBox.Spreadsheet;
using ObjectModel.Ko;
using Serilog;
using GroupDto = KadOzenka.Dal.Modeling.Dto.GroupDto;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingService
	{
		private readonly ILogger _log = Log.ForContext<ModelingService>();
        public ModelFactorsService ModelFactorsService { get; set; }

		public ModelingService()
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
                    x.AlgoritmType_Code,
                    x.CalculationType_Code,
                    x.A0
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
                AlgorithmType = model.AlgoritmType_Code,
                CalculationType = model.CalculationType_Code,
                A0 = model.A0
	        };
        }

        public List<GroupDto> GetGroups(long tourId)
        {
            var groupsInTour = OMTourGroup.Where(x => x.TourId == tourId).Select(x => x.GroupId).Execute()
                .Select(x => x.GroupId).ToList();
            if (groupsInTour.Count == 0)
                return new List<GroupDto>();

            var groupsToMarketSegmentInTour = OMGroupToMarketSegmentRelation
	            .Where(x => groupsInTour.Contains(x.GroupId))
	            .Select(x => new
	            {
		            x.GroupId,
		            x.ParentGroup.GroupName,
		            x.ParentGroup.Number
	            })
	            .Execute()
	            .Select(x => new GroupDto
	            {
		            GroupId = x.GroupId,
		            Name = $"{x.ParentGroup?.Number}.{x.ParentGroup?.GroupName}"
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
	        ValidateAutomaticModel(modelDto);

	        var model = new OMModel
	        {
		        Name = modelDto.Name,
		        Description = modelDto.Description,
		        GroupId = modelDto.GroupId,
		        CalculationType_Code = KoCalculationType.Comparative,
		        AlgoritmType_Code = modelDto.AlgorithmType,
		        Type_Code = KoModelType.Automatic,
                Formula = "-"
	        };

	        model.Save();
        }

        public void AddManualModel(ModelingModelDto modelDto)
        {
	        ValidateBaseModel(modelDto);

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

        public bool UpdateAutomaticModel(ModelingModelDto modelDto)
		{
			ValidateAutomaticModel(modelDto);

            var existedModel = GetModelEntityById(modelDto.ModelId);

            var isModelChanged = IsModelChanged(existedModel, modelDto);

            using (var ts = new TransactionScope())
            {
	            existedModel.Name = modelDto.Name;
                existedModel.Description = modelDto.Description;
                existedModel.GroupId = modelDto.GroupId;
                existedModel.IsOksObjectType = modelDto.IsOksObjectType;
                existedModel.AlgoritmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation;
                switch (modelDto.AlgorithmType)
                {
                    case KoAlgoritmType.None:
	                case KoAlgoritmType.Line:
	                    existedModel.A0 = modelDto.A0;
                        break;
	                case KoAlgoritmType.Exp:
		                existedModel.A0ForExponential = modelDto.A0;
                        break;
	                case KoAlgoritmType.Multi:
		                existedModel.A0ForMultiplicative = modelDto.A0;
                        break;
                }

                if (isModelChanged)
                {
	                ResetTrainingResults(existedModel, KoAlgoritmType.None);
	                var factors = ModelFactorsService.GetFactors(existedModel.Id, KoAlgoritmType.None);
	                factors.ForEach(x => x.Destroy());
                    DestroyModelMarketObjects(existedModel.Id);
                }

                existedModel.Save();

                ts.Complete();
            }

            return isModelChanged;
        }

        public void UpdateManualModel(ModelingModelDto modelDto)
        {
	        ValidateBaseModel(modelDto);

            var existedModel = GetModelEntityById(modelDto.ModelId);

            using (var ts = new TransactionScope())
            {
	            if (existedModel.AlgoritmType_Code != modelDto.AlgorithmType)
	            {
		            var factors = ModelFactorsService.GetFactors(modelDto.ModelId, existedModel.AlgoritmType_Code);
		            factors.ForEach(x =>
		            {
			            x.AlgorithmType_Code = modelDto.AlgorithmType;
			            x.Save();
		            });
	            }

	            existedModel.Name = modelDto.Name;
	            existedModel.Description = modelDto.Description;
	            existedModel.GroupId = modelDto.GroupId;
	            existedModel.AlgoritmType_Code = modelDto.AlgorithmType;
	            existedModel.A0 = modelDto.A0;

	            existedModel.CalculationMethod_Code = modelDto.CalculationType == KoCalculationType.Comparative
		            ? modelDto.CalculationMethod
		            : KoCalculationMethod.None;

	            existedModel.CalculationType_Code = modelDto.CalculationType;
	            existedModel.Formula = existedModel.GetFormulaFull(true);

	            existedModel.Save();

                ts.Complete();
            }
        }

        public void DeleteModel(long modelId)
        {
	        using (var ts = new TransactionScope())
	        {
		        var model = GetModelEntityById(modelId);

		        var factors = ModelFactorsService.GetFactors(modelId, KoAlgoritmType.None);
		        factors.ForEach(factor =>
		        {
                    ModelFactorsService.DeleteMarks(model.GroupId, factor.FactorId);

                    factor.Destroy();
		        });

		        if (model.Type_Code == KoModelType.Automatic)
		        {
			        var modelToObjectsRelation = OMModelToMarketObjects.Where(x => x.ModelId == modelId).Execute();
			        modelToObjectsRelation.ForEach(x => x.Destroy());
		        }

		        model.Destroy();

                ts.Complete();
	        }
        }

        public void ResetTrainingResults(long? modelId, KoAlgoritmType type)
        {
	        var model = GetModelEntityById(modelId);
            ResetTrainingResults(model, type);
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

        public bool IsModelChanged(long modelId, ModelingModelDto newModel)
        {
	        var existedModel = GetModelEntityById(modelId);

	        return IsModelChanged(existedModel, newModel);
        }


        #region Support

        private bool IsModelChanged(OMModel existedModel, ModelingModelDto newModel)
		{
			return !(existedModel.GroupId == newModel.GroupId &&
			         existedModel.IsOksObjectType == newModel.IsOksObjectType);
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

            var objectsFromDb = OMModelToMarketObjects.Where(x => ids.Contains(x.Id)).Select(x => new
            {
                x.IsExcluded,
                x.IsForTraining,
                x.IsForControl,
                x.Coefficients
            }).Execute();
            objects.ForEach(obj =>
            {
                var objFromDb = objectsFromDb.FirstOrDefault(x => x.Id == obj.Id);
                if (objFromDb == null)
                    return;

                if (objFromDb.IsExcluded.GetValueOrDefault() != obj.IsExcluded ||
                    objFromDb.IsForTraining.GetValueOrDefault() != obj.IsForTraining ||
                    objFromDb.IsForControl.GetValueOrDefault() != obj.IsForControl)
                {
	                if (obj.IsForTraining && obj.IsForControl)
		                throw new Exception($"Объект с КН '{obj.CadastralNumber}' не может одновременно быть и в обучающей, и в контрольной выборках");

	                objFromDb.IsExcluded = obj.IsExcluded;
	                objFromDb.IsForTraining = obj.IsForTraining;
	                objFromDb.IsForControl = obj.IsForControl;
	                if (obj.IsExcluded)
	                {
		                objFromDb.Coefficients = null;
	                }
	                objFromDb.Save();
                }
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

        public Stream ExportMarketObjectsToExcel(long modelId, List<long> marketObjectsIds)
        {
	        var model = OMModel.Where(x => x.Id == modelId).Select(x => x.A0ForExponential).ExecuteFirstOrDefault();
            if (model == null)
	            throw new Exception($"Не найдена модель с ИД '{modelId}'");
            //пока работаем только с Exp
            var factors = ModelFactorsService.GetFactors(model.Id, KoAlgoritmType.Exp);

            var excelTemplate = new ExcelFile();
            var mainWorkSheet = excelTemplate.Worksheets.Add("Объекты модели");

            var columnHeaders = new List<object>
            {
                "Id", "Исключен из расчета", "Кадастровый номер", "Цена", "Спрогнозированная цена",
                "Признак выбора аналога в обучающую модель",
                "Признак выбора аналога в контрольную модель"
            };
            columnHeaders.AddRange(factors.Select(x => RegisterCache.GetAttributeData((int)x.FactorId.GetValueOrDefault()).Name).ToList());
            columnHeaders.AddRange(new List<string>{ "МС", "%" });

            AddRowToExcel(mainWorkSheet, 0, columnHeaders.ToArray());

            if (marketObjectsIds != null && marketObjectsIds.Count > 0)
            {
                var rowCounter = 1;
                var marketObjects = OMModelToMarketObjects.Where(x => marketObjectsIds.Contains(x.Id)).SelectAll().Execute();
                marketObjectsIds.ForEach(id =>
                {
	                var obj = marketObjects.FirstOrDefault(x => x.Id == id);
                    if(obj == null)
                        return;
                    
	                var values = new List<object>
                    {
                        obj.Id, obj.IsExcluded.GetValueOrDefault(), obj.CadastralNumber, obj.Price, obj.PriceFromModel,
                        obj.IsForTraining.GetValueOrDefault(), obj.IsForControl.GetValueOrDefault()
                    };

	                var coefficients = obj.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
	                factors.ForEach(attribute =>
	                {
		                var coefficient = coefficients.FirstOrDefault(x => x.AttributeId == attribute?.FactorId)?.Coefficient;
		                values.Add(coefficient);
	                });

	                var calculationParameters = GetModelCalculationParameters(model.A0ForExponential, obj.Price,
		                factors, coefficients, obj.CadastralNumber);

                    values.Add(calculationParameters.ModelingPrice);
                    values.Add(calculationParameters.Percent);

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

        public ModelObjectsCalculationParameters GetModelCalculationParameters(decimal? a0, decimal? objectPrice,
	        List<OMModelFactor> factors, List<CoefficientForObject> objectCoefficients, string cadastralNumber)
        {
	        try
	        {
		        decimal modelingPriceCounter = 0;
		        foreach (var factor in factors)
		        {
			        var objectCoefficient = objectCoefficients?.FirstOrDefault(x =>
				        x.AttributeId == factor.FactorId && !string.IsNullOrWhiteSpace(x.Value));

			        var metka = objectCoefficient?.Coefficient;

			        modelingPriceCounter = modelingPriceCounter +
			                               (metka.GetValueOrDefault(1) * factor.PreviousWeight.GetValueOrDefault(1));
		        }

		        var resultModelingPrice = (decimal?) Math.Exp((double) (a0.GetValueOrDefault() + modelingPriceCounter));
		        var modelingPrice = Math.Round(resultModelingPrice.GetValueOrDefault(), 2);
		        decimal? percent = null;
		        if (objectPrice.GetValueOrDefault() != 1)
		        {
			        percent = (modelingPrice / objectPrice.GetValueOrDefault() - 1) * 100;
		        }

		        return new ModelObjectsCalculationParameters
		        {
			        ModelingPrice = modelingPrice,
			        Percent = percent
		        };
	        }
	        catch (Exception exception)
	        {
		        _log.ForContext("CadastralNumber", cadastralNumber)
			        .ForContext("A0", a0)
			        .ForContext("ObjectPrice", objectPrice)
			        .Error(exception, "Ошибка во время расчета МС и % для объекта моделирования");
		        return new ModelObjectsCalculationParameters();
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

        public QSQuery<OMModelToMarketObjects> GetIncludedModelObjectsQuery(long modelId, bool isForTraining)
        {
	        if (isForTraining)
	        {
		        return OMModelToMarketObjects
			        .Where(x => x.ModelId == modelId && x.IsExcluded.Coalesce(false) == false &&
			                    (x.IsForTraining.Coalesce(false) == true || x.IsForControl.Coalesce(false) == true));
            }

	        return OMModelToMarketObjects
		        .Where(x => x.ModelId == modelId && x.IsExcluded.Coalesce(false) == false &&
		                    x.IsForTraining.Coalesce(false) == false && x.IsForControl.Coalesce(false) == false);
        }

        public List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, bool isForTraining)
        {
            return GetIncludedModelObjectsQuery(modelId, isForTraining).SelectAll().Execute();
        }

        public void DestroyModelMarketObjects(long? modelId)
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
                IsForControl = entity.IsForControl.GetValueOrDefault(),
                Coefficients = entity.Coefficients.DeserializeFromXml<List<CoefficientForObject>>()
            };
		}

        private void ValidateBaseModel(ModelingModelDto modelDto)
        {
	        var message = new StringBuilder();

	        if (string.IsNullOrWhiteSpace(modelDto.Name))
		        message.AppendLine("У модели не заполнено Имя");
	        if (string.IsNullOrWhiteSpace(modelDto.Description))
		        message.AppendLine("У модели не заполнено Описание");

	        var isModelExists = OMModel.Where(x => x.Id != modelDto.ModelId && x.GroupId == modelDto.GroupId).ExecuteExists();
	        if (isModelExists)
		        message.AppendLine("Модель для данной группы уже существует");

	        if (modelDto.Type == KoModelType.Manual && modelDto.AlgorithmType == KoAlgoritmType.None)
		        message.AppendLine($"Для модели типа '{KoModelType.Manual.GetEnumDescription()}' нужно указать Тип алгоритма");

	        if (message.Length != 0)
		        throw new Exception(message.ToString());
        }

        private void ValidateAutomaticModel(ModelingModelDto modelDto)
        {
	        ValidateBaseModel(modelDto);

            var message = new StringBuilder();

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

        #endregion

        #region Entities

        public class ModelObjectsCalculationParameters
        {
	        public decimal? ModelingPrice { get; set; }
	        public decimal? Percent { get; set; }
        }

        #endregion
    }
}
