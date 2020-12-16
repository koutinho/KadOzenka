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
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.LongProcess.Modeling.Entities;
using KadOzenka.Dal.Oks;
using ObjectModel.Ko;
using ObjectModel.Market;
using Serilog;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingService
	{
		private readonly ILogger _log = Log.ForContext<ModelingService>();
        public ModelingRepository ModelingRepository { get; set; }
        public ModelFactorsService ModelFactorsService { get; set; }

		public ModelingService()
		{
			ModelFactorsService = new ModelFactorsService();
			ModelingRepository = new ModelingRepository();
		}

        #region CRUD General Model

        public OMModel GetActiveModelEntityByGroupId(long? groupId)
        {
	        var model = ModelingRepository.GetActiveModelEntityByGroupId(groupId);
	        if (model == null)
		        throw new Exception($"Не найдена активная модель для Группы с id='{groupId}'");

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
                    x.A0,
					x.A0ForExponential,
					x.A0ForMultiplicative,
                    x.A0ForLinearTypeInPreviousTour,
                    x.A0ForExponentialTypeInPreviousTour,
					x.A0ForMultiplicativeTypeInPreviousTour,
                    x.Formula,
                    x.CalculationMethod_Code,
					x.IsActive
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
                AlgorithmTypeForCadastralPriceCalculation = model.AlgoritmType_Code,
                CalculationType = model.CalculationType_Code,
                A0 = model.GetA0(),
                A0ForPreviousTour = model.GetA0ForPreviousTour(),
                Formula = model.Formula,
                CalculationMethod = model.CalculationMethod_Code,
                IsActive = model.IsActive.GetValueOrDefault()
			};
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
	        ValidateModelDuringAddition(modelDto);

	        var model = new OMModel
	        {
		        Name = modelDto.Name,
		        Description = modelDto.Description,
		        GroupId = modelDto.GroupId,
                IsOksObjectType = modelDto.IsOksObjectType,
		        CalculationType_Code = KoCalculationType.Comparative,
		        AlgoritmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation,
		        Type_Code = KoModelType.Automatic,
                Formula = "-"
	        };

	        model.Save();
        }

        public void AddManualModel(ModelingModelDto modelDto)
        {
	        ValidateModelDuringAddition(modelDto);

	        var model = new OMModel
	        {
		        Name = modelDto.Name,
		        Description = modelDto.Description,
		        GroupId = modelDto.GroupId,
		        IsOksObjectType = modelDto.IsOksObjectType,
                AlgoritmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation,
		        Type_Code = KoModelType.Manual
	        };

	        model.Formula = model.GetFormulaFull(true);

	        model.Save();
        }

        public void UpdateAutomaticModel(ModelingModelDto modelDto)
		{
			ValidateModelDuringUpdating(modelDto);

            var existedModel = GetModelEntityById(modelDto.ModelId);

            existedModel.Name = modelDto.Name;
            existedModel.Description = modelDto.Description;
            existedModel.AlgoritmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation;
            switch (modelDto.AlgorithmType)
            {
	            case KoAlgoritmType.None:
	            case KoAlgoritmType.Line:
		            existedModel.A0 = modelDto.A0;
		            existedModel.A0ForLinearTypeInPreviousTour = modelDto.A0ForPreviousTour;
		            break;
	            case KoAlgoritmType.Exp:
		            existedModel.A0ForExponential = modelDto.A0;
		            existedModel.A0ForExponentialTypeInPreviousTour = modelDto.A0ForPreviousTour;
		            break;
	            case KoAlgoritmType.Multi:
		            existedModel.A0ForMultiplicative = modelDto.A0;
		            existedModel.A0ForMultiplicativeTypeInPreviousTour = modelDto.A0ForPreviousTour;
		            break;
            }

            existedModel.Save();
        }

        public void UpdateManualModel(ModelingModelDto modelDto)
        {
	        ValidateModelDuringUpdating(modelDto);

            var existedModel = GetModelEntityById(modelDto.ModelId);

            using (var ts = new TransactionScope())
            {
	            if (existedModel.AlgoritmType_Code != modelDto.AlgorithmTypeForCadastralPriceCalculation)
	            {
		            var factors = ModelFactorsService.GetFactors(modelDto.ModelId, existedModel.AlgoritmType_Code);
		            factors.ForEach(x =>
		            {
			            x.AlgorithmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation;
			            x.Save();
		            });
	            }

	            existedModel.Name = modelDto.Name;
	            existedModel.Description = modelDto.Description;
	            existedModel.AlgoritmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation;
	            switch (modelDto.AlgorithmTypeForCadastralPriceCalculation)
	            {
		            case KoAlgoritmType.Exp:
			            existedModel.A0ForExponential = modelDto.A0;
						break;
		            case KoAlgoritmType.Line:
			            existedModel.A0 = modelDto.A0;
						break;
		            case KoAlgoritmType.Multi:
			            existedModel.A0ForMultiplicative = modelDto.A0;
						break;
	            }

	            existedModel.CalculationMethod_Code = modelDto.CalculationType == KoCalculationType.Comparative
		            ? modelDto.CalculationMethod
		            : KoCalculationMethod.None;

	            existedModel.CalculationType_Code = modelDto.CalculationType;
	            existedModel.Formula = existedModel.GetFormulaFull(true);

	            existedModel.Save();

                ts.Complete();
            }
        }

        public void MakeModelActive(long modelId)
        {
	        var model = OMModel.Where(x => x.Id == modelId)
		        .Select(x => new
		        {
			        x.GroupId,
					x.Type_Code,
					x.LinearTrainingResult,
					x.ExponentialTrainingResult,
					x.MultiplicativeTrainingResult,
					x.IsActive
		        }).ExecuteFirstOrDefault();
	        if (model == null)
		        throw new Exception($"Не найдена модель с ИД '{modelId}'");

	        if (model.Type_Code == KoModelType.Automatic)
	        {
		        var hasFormedObjectArray = GetIncludedModelObjectsQuery(modelId, true).ExecuteExists();
		        var hasTrainingResult = !string.IsNullOrWhiteSpace(model.LinearTrainingResult) ||
		                                !string.IsNullOrWhiteSpace(model.ExponentialTrainingResult) ||
		                                !string.IsNullOrWhiteSpace(model.MultiplicativeTrainingResult);
		        if (!hasFormedObjectArray || !hasTrainingResult)
			        throw new Exception("Невозможно активировать необученную модель");
			}
	        
			using (var ts = new TransactionScope())
			{
				var otherModelsForGroup = OMModel.Where(x => x.GroupId == model.GroupId).Select(x => x.IsActive).Execute();
				otherModelsForGroup.ForEach(x =>
				{
					if (!x.IsActive.GetValueOrDefault())
						return;

			        x.IsActive = false;
			        x.Save();
		        });

		        if (!model.IsActive.GetValueOrDefault())
		        {
			        model.IsActive = true;
			        model.Save();
				}

		        ts.Complete();
	        }
        }

        public void DeleteModel(long modelId)
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
		}

        #region Support Methods

        private void ValidateModelDuringUpdating(ModelingModelDto modelDto)
        {
	        var message = new StringBuilder();

	        if (string.IsNullOrWhiteSpace(modelDto.Name))
		        message.AppendLine("У модели не заполнено Имя");
	        if (string.IsNullOrWhiteSpace(modelDto.Description))
		        message.AppendLine("У модели не заполнено Описание");

	        if (modelDto.Type == KoModelType.Manual && modelDto.AlgorithmTypeForCadastralPriceCalculation == KoAlgoritmType.None)
		        message.AppendLine($"Для модели типа '{KoModelType.Manual.GetEnumDescription()}' нужно указать Тип алгоритма");

	        if (message.Length != 0)
		        throw new Exception(message.ToString());
        }

        private void ValidateModelDuringAddition(ModelingModelDto modelDto)
        {
	        ValidateModelDuringUpdating(modelDto);

	        var message = new StringBuilder();

	        if (modelDto.GroupId == 0)
		        message.AppendLine("Для модели не выбрана группа");

	        var isGroupBelongToTour = OMTourGroup.Where(x => x.TourId == modelDto.TourId && x.GroupId == modelDto.GroupId).ExecuteExists();
	        if (!isGroupBelongToTour)
		        message.AppendLine($"Группа c Id='{modelDto.GroupId}'не принадлежит туру с Id='{modelDto.TourId}'");

	        if (message.Length != 0)
		        throw new Exception(message.ToString());
        }

        #endregion

        #endregion


        #region Model Object Relations

        public List<OMModelToMarketObjects> GetModelObjects(long modelId)
		{
			return OMModelToMarketObjects.Where(x => x.ModelId == modelId)
                .OrderBy(x => x.CadastralNumber)
                .SelectAll()
                .Execute();
		}

        public QSQuery<OMModelToMarketObjects> GetIncludedModelObjectsQuery(long? modelId, bool isForTraining)
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

        public int DestroyModelMarketObjects(OMModel model)
        {
	        var existedModelObjects = OMModelToMarketObjects.Where(x => x.ModelId == model.Id).Execute();
	        existedModelObjects.ForEach(x => x.Destroy());
	        
	        model.ObjectsStatistic = null;
	        model.Save();

	        return existedModelObjects.Count;
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
                x.IsForControl
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
	                objFromDb.Save();
                }
            });
        }

        public Stream ExportMarketObjectsToExcel(long modelId, List<long> marketObjectsIds)
        {
	        //var model = OMModel.Where(x => x.Id == modelId).Select(x => x.A0ForExponential).ExecuteFirstOrDefault();
	        //if (model == null)
	        //    throw new Exception($"Не найдена модель с ИД '{modelId}'");
            //пока работаем только с Exp
            var factors = ModelFactorsService.GetFactors(modelId, KoAlgoritmType.Exp).Select(x => new
            {
	            FactorId = x.FactorId.GetValueOrDefault(),
                Name = RegisterCache.GetAttributeData((int)x.FactorId.GetValueOrDefault()).Name
            }).OrderBy(x => x.Name).ToList();

            var excelTemplate = new ExcelFile();
            var mainWorkSheet = excelTemplate.Worksheets.Add("Объекты модели");

            var columnHeaders = new List<object>
            {
                "Id", "Исключен из расчета", "Кадастровый номер", "Цена", "Спрогнозированная цена"
            };
            columnHeaders.AddRange(factors.Select(x => x.Name).ToList());
            columnHeaders.AddRange(new List<string>{ "Признак выбора аналога в обучающую модель", "Признак выбора аналога в контрольную модель" });
            //TODO код закомментирован по просьбе заказчиков, в дальнейшем он будет использоваться
            //columnHeaders.AddRange(new List<string>{ "МС", "%" });

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
                        obj.Id, obj.IsExcluded.GetValueOrDefault(), obj.CadastralNumber, obj.Price, obj.PriceFromModel
                    };

	                var coefficients = obj.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
	                factors.ForEach(attribute =>
	                {
		                var coefficient = coefficients.FirstOrDefault(x => x.AttributeId == attribute?.FactorId)?.Coefficient;
		                values.Add(coefficient);
	                });

	                values.Add(obj.IsForTraining.GetValueOrDefault());
	                values.Add(obj.IsForControl.GetValueOrDefault());

                    //var calculationParameters = GetModelCalculationParameters(model.A0ForExponentialTypeInPreviousTour, obj.Price,
                    // factors, coefficients, obj.CadastralNumber); 

                    //values.Add(calculationParameters.ModelingPrice); 
                    //values.Add(calculationParameters.Percent);

                    AddRowToExcel(mainWorkSheet, rowCounter++, values.ToArray());
                });
            }

            var stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public ExcludeModelObjectsFromCalculationResult ExcludeModelObjectsFromCalculation(ExcelFile file)
        {
            const int idColumnIndex = 0;
            const int isExcludedColumnIndex = 1;

            var rows = file.Worksheets[0].Rows;
            var modelObjectsFromExcel = new List<ModelObjectsFromExcelData>();
            var rowsCount = DataExportCommon.GetLastUsedRowIndex(file.Worksheets[0]);
            for (var i = 1; i < rowsCount; i++)
            {
                var id = rows[i].Cells[idColumnIndex].Value.ParseToLongNullable();
                var isExcludedStr = rows[i].Cells[isExcludedColumnIndex].Value.ParseToStringNullable();

                modelObjectsFromExcel.Add(new ModelObjectsFromExcelData
                {
                    Id = id,
                    IsExcluded = isExcludedStr?.ToLower() == "да",
                    RowIndexInFile = i
                });
            }

            var modelObjectsIds = modelObjectsFromExcel.Select(x => x.Id).ToList();
            if (modelObjectsIds.Count == 0)
            {
	            return new ExcludeModelObjectsFromCalculationResult();
            }

            var result = new ExcludeModelObjectsFromCalculationResult
            {
                TotalCount = modelObjectsFromExcel.Count
            };

            var modelObjects = OMModelToMarketObjects.Where(x => modelObjectsIds.Contains(x.Id))
	            .Select(x => x.IsExcluded)
	            .Execute();
            foreach (var modelObjectFromExcel in modelObjectsFromExcel)
            {
	            var modelObject = modelObjects.FirstOrDefault(x => x.Id == modelObjectFromExcel.Id);
	            if (modelObject == null)
	            {
		            result.ErrorRowIndexes.Add(modelObjectFromExcel.RowIndexInFile);
		            continue;
	            }

	            if (modelObject.IsExcluded.GetValueOrDefault() == modelObjectFromExcel.IsExcluded)
	            {
		            result.UnchangedObjectsCount++;
		            continue;
	            }
	            
	            modelObject.IsExcluded = modelObjectFromExcel.IsExcluded;
	            modelObject.Save();
	            result.UpdatedObjectsCount++;
            }

            if (result.ErrorObjectsCount > 0)
            {
	            var stream = new MemoryStream();
				//исключительный случай - когда не найден ни один объект из файла
				//тогда не генерируем файл заново, а возвращаем тот же самый
	            if (rowsCount - 1 == result.ErrorRowIndexes?.Count)
	            {
		            file.Save(stream, SaveOptions.XlsxDefault);
		            stream.Seek(0, SeekOrigin.Begin);
				}
	            else
	            {
		            stream = GenerateReportWithErrors(file, result.ErrorRowIndexes);
	            }
	            result.File = stream;
            }

            return result;
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
			        percent = CalculatePercent(modelingPrice, objectPrice);
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

        public static decimal? CalculatePercent(decimal? calculatedPrice, decimal? initialPrice)
        {
	        decimal? percent = null;
	        if (calculatedPrice != null && initialPrice.GetValueOrDefault() != 0)
	        {
		        percent = (calculatedPrice / initialPrice.GetValueOrDefault() - 1) * 100;
	        }

	        return percent;
        }


        #region Support Methods

        private MemoryStream GenerateReportWithErrors(ExcelFile initialFile, List<int> errorRowIndexes)
        {
			var resultFile = new ExcelFile();
	        var sheet = resultFile.Worksheets.Add("Не найденные объекты");
	        sheet.Cells.Style.Font.Name = "Times New Roman";

            //копируем заголовки
            var generalRowCounter = 0;
            sheet.Rows.InsertCopy(generalRowCounter, initialFile.Worksheets[0].Rows[0]);

            for (var i = 0; i < errorRowIndexes.Count; i++)
            {
	            generalRowCounter++;
	            sheet.Rows.InsertCopy(generalRowCounter, initialFile.Worksheets[0].Rows[errorRowIndexes[i]]);
	            errorRowIndexes[i]++;
            }

            var stream = new MemoryStream();
	        resultFile.Save(stream, SaveOptions.XlsxDefault);
	        stream.Seek(0, SeekOrigin.Begin);

	        return stream;
        }

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

		#endregion


        #region Entities

        public class ModelObjectsCalculationParameters
        {
	        public decimal? ModelingPrice { get; set; }
	        public decimal? Percent { get; set; }
        }

        public class ModelObjectsFromExcelData
        {
	        public long? Id { get; set; }
	        public bool IsExcluded { get; set; }
	        public int RowIndexInFile { get; set; }
        }

        public class ExcludeModelObjectsFromCalculationResult
		{
			public int TotalCount { get; set; }
			public int UpdatedObjectsCount { get; set; }
			public int UnchangedObjectsCount { get; set; }
			public int ErrorObjectsCount => ErrorRowIndexes.Count;
			public List<int> ErrorRowIndexes { get; set; }
			public MemoryStream File { get; set; }

			public ExcludeModelObjectsFromCalculationResult()
			{
				ErrorRowIndexes = new List<int>();
			}
		}

        #endregion
    }
}
