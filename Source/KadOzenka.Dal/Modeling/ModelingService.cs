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
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.Extentions;
using KadOzenka.Dal.Modeling.Exceptions;
using KadOzenka.Dal.Modeling.Repositories;
using KadOzenka.Dal.Modeling.Resources;
using Serilog;
using System.Linq.Expressions;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingService
	{
		private readonly ILogger _log = Log.ForContext<ModelingService>();
        private IModelingRepository ModelingRepository { get; set; }
        public ModelFactorsService ModelFactorsService { get; set; }
        public RecycleBinService RecycleBinService { get; }

		#region Информация для выгрузки/загрузки объектов моделирования

		private const int IdColumnIndex = 0;
		private const int IsForTrainingColumnIndex = 1;
		private const int IsForControlColumnIndex = 2;
		private const int IsExcludedColumnIndex = 3;
		private const int CadastralNumberColumnIndex = 4;
		private const int ObjectTypeColumnIndex = 5;
		private const int PriceColumnIndex = 6;
		private const int PredictedPriceColumnIndex = 7;
		private const int DeviationFromPredictablePriceColumnIndex = 8;
		private int _maxNumberOfColumns = 9;

		#endregion

		public ModelingService(IModelingRepository modelingRepository = null)
		{
			ModelFactorsService = new ModelFactorsService();
			ModelingRepository = modelingRepository ?? new ModelingRepository();
			RecycleBinService = new RecycleBinService();
		}


		#region CRUD General Model

		public OMModel GetActiveModelEntityByGroupId(long? groupId)
        {
	        if (groupId == null)
		        throw new Exception("Не передан идентификатор Группы для поиска модели");

			var model = ModelingRepository.GetEntityByCondition(x => x.GroupId == groupId && x.IsActive.Coalesce(false) == true, null);

			return model;
        }

        public OMModel GetModelEntityById(long? modelId)
        {
	        if (modelId.GetValueOrDefault() == 0)
		        throw new Exception(Messages.EmptyModelId);

	        var model = ModelingRepository.GetById(modelId.Value, null);
	        if (model == null)
		        throw new ModelNotFoundByIdException($"Не найдена Модель с id='{modelId}'");

	        return model;
        }

        public ModelingModelDto GetModelById(long modelId)
        {
	        Expression<Func<OMModel, object>> selectExpression = x => new
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
	        };
	        
	        var model = ModelingRepository.GetById(modelId, selectExpression);

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

        public bool IsModelGroupExist(long modelId)
        {
	        var model = ModelingRepository.GetById(modelId, x => new {x.GroupId});
	        if (model == null)
		        return false;

	        return OMGroup.Where(x => x.Id == model.GroupId).ExecuteExists();
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

	        ModelingRepository.Save(model);
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

			ModelingRepository.Save(model);
		}

        public void UpdateAutomaticModel(ModelingModelDto modelDto)
		{
			ValidateBaseModel(modelDto);

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
	        ValidateBaseModel(modelDto);

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
	        var model = ModelingRepository.GetById(modelId, x => new
	        {
		        x.GroupId,
		        x.Type_Code,
		        x.LinearTrainingResult,
		        x.ExponentialTrainingResult,
		        x.MultiplicativeTrainingResult,
		        x.IsActive
	        });

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
				var otherModelsForGroup = ModelingRepository.GetEntitiesByCondition(x => x.GroupId == model.GroupId, x => new {x.IsActive});
				otherModelsForGroup.ForEach(x =>
				{
					if (!x.IsActive.GetValueOrDefault())
						return;

			        x.IsActive = false;
			        ModelingRepository.Save(x);
				});

		        if (!model.IsActive.GetValueOrDefault())
		        {
			        model.IsActive = true;
			        ModelingRepository.Save(model);
		        }

		        ts.Complete();
	        }
        }

        public void DeleteModel(long modelId)
        {
			var model = GetModelEntityById(modelId);

			var factors = ModelFactorsService.GetFactors(modelId, KoAlgoritmType.None);
			factors.ForEach(factor => factor.Destroy());

			if (model.Type_Code == KoModelType.Automatic)
			{
				var modelToObjectsRelation = OMModelToMarketObjects.Where(x => x.ModelId == modelId).Execute();
				modelToObjectsRelation.ForEach(x => x.Destroy());
			}

			model.Destroy();
		}

        public void DeleteModelLogically(long modelId, long eventId)
        {
	        var model = GetModelEntityById(modelId);

	        var factors = ModelFactorsService.GetFactors(modelId, KoAlgoritmType.None);
	        RecycleBinService.MoveObjectsToRecycleBin(factors.Select(x => x.Id).ToList(), OMModelFactor.GetRegisterId(), eventId);

	        if (model.Type_Code == KoModelType.Automatic)
	        {
		        var modelToObjectsRelation = OMModelToMarketObjects.Where(x => x.ModelId == modelId).Execute();
		        RecycleBinService.MoveObjectsToRecycleBin(modelToObjectsRelation.Select(x => x.Id).ToList(), OMModelToMarketObjects.GetRegisterId(), eventId);
	        }

			RecycleBinService.MoveObjectToRecycleBin(model.Id, OMModel.GetRegisterId(), eventId);
		}

		#region Support Methods

		private void ValidateBaseModel(ModelingModelDto modelDto)
        {
	        var message = new StringBuilder();

	        if (string.IsNullOrWhiteSpace(modelDto.Name))
		        message.AppendLine(Messages.EmptyName);
	        if (string.IsNullOrWhiteSpace(modelDto.Description))
		        message.AppendLine("У модели не заполнено Описание");

	        if (modelDto.Type == KoModelType.Manual && modelDto.AlgorithmTypeForCadastralPriceCalculation == KoAlgoritmType.None)
		        message.AppendLine($"Для модели типа '{KoModelType.Manual.GetEnumDescription()}' нужно указать Тип алгоритма");

	        if (message.Length != 0)
		        throw new ModelCrudException(message.ToString());
        }

        private void ValidateModelDuringAddition(ModelingModelDto modelDto)
        {
	        ValidateBaseModel(modelDto);

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

        public Stream ExportMarketObjectsToExcel(long modelId)
        {
	        //var model = OMModel.Where(x => x.Id == modelId).Select(x => x.A0ForExponential).ExecuteFirstOrDefault();
	        //if (model == null)
	        //    throw new Exception($"Не найдена модель с ИД '{modelId}'");
	        var excelTemplate = new ExcelFile();
            var mainWorkSheet = excelTemplate.Worksheets.Add("Объекты модели");

            var factors = GetFactorColumnsForModelObjectsInFile(modelId);
			//если есть нормализация - показывать два столбца (с коэфициентом и со значением)
            var groupedFactors = factors.OrderBy(x => x.ColumnIndex).GroupBy(x => x.AttributeId).ToList();
			var columnHeaders = new object[_maxNumberOfColumns];
            columnHeaders[IdColumnIndex] = "ИД";
            columnHeaders[IsForTrainingColumnIndex] = "Признак выбора аналога в обучающую модель";
            columnHeaders[IsForControlColumnIndex] = "Признак выбора аналога в контрольную модель";
            columnHeaders[IsExcludedColumnIndex] = "Исключен из расчета";
            columnHeaders[CadastralNumberColumnIndex] = "Кадастровый номер";
            columnHeaders[ObjectTypeColumnIndex] = "Тип объекта";
            columnHeaders[PriceColumnIndex] = "Цена";
            columnHeaders[PredictedPriceColumnIndex] = "Спрогнозированная цена";
            columnHeaders[DeviationFromPredictablePriceColumnIndex] = "Отклонение цены от прогнозной, %";
            factors.ForEach(x => columnHeaders[x.ColumnIndex] = x.Name);
            //TODO код закомментирован по просьбе заказчиков, в дальнейшем он будет использоваться
            //columnHeaders.AddRange(new List<string>{ "МС", "%" });

            AddRowToExcel(mainWorkSheet, 0, columnHeaders);

			var rowCounter = 1;
			var marketObjects = GetModelObjects(modelId);
			marketObjects.ForEach(obj =>
			{
				var values = new object[_maxNumberOfColumns];
				values[IdColumnIndex] = obj.Id;
				values[IsForTrainingColumnIndex] = obj.IsForTraining.GetValueOrDefault();
				values[IsForControlColumnIndex] = obj.IsForControl.GetValueOrDefault();
				values[IsExcludedColumnIndex] = obj.IsExcluded.GetValueOrDefault();
				values[CadastralNumberColumnIndex] = obj.CadastralNumber;
				values[ObjectTypeColumnIndex] = obj.UnitPropertyType;
				values[PriceColumnIndex] = obj.Price;
				values[PredictedPriceColumnIndex] = obj.PriceFromModel;
				values[DeviationFromPredictablePriceColumnIndex] = CalculatePercent(obj.PriceFromModel, obj.Price);

				var coefficients = obj.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
				groupedFactors.ForEach(factor =>
				{
					var coefficient = coefficients.FirstOrDefault(x => x.AttributeId == factor.Key);
					var factorColumns = factor.ToList();
					factorColumns.ForEach(x =>
					{
						if (x.IsColumnWithValue)
						{
							values[x.ColumnIndex] = coefficient?.Value;
						}
						else
						{
							values[x.ColumnIndex] = coefficient?.Coefficient;
						}
					});
				});

				//var calculationParameters = GetModelCalculationParameters(model.A0ForExponentialTypeInPreviousTour, obj.Price,
				// factors, coefficients, obj.CadastralNumber); 

				//values.Add(calculationParameters.ModelingPrice); 
				//values.Add(calculationParameters.Percent);

				AddRowToExcel(mainWorkSheet, rowCounter++, values);
			});

			var stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

		public UpdateModelObjectsResult UpdateModelObjects(long modelId, ExcelFile file)
        {
	        var factors = GetFactorColumnsForModelObjectsInFile(modelId).Where(x => !x.IsColumnWithValue).ToList();

            var rows = file.Worksheets[0].Rows;
            var modelObjectsFromExcel = new List<ModelObjectsFromExcelData>();
            var rowsCount = DataExportCommon.GetLastUsedRowIndex(file.Worksheets[0]);
            for (var i = 1; i <= rowsCount; i++)
            {
	            var cells = rows[i].Cells;
	            var factorsFromFile = new List<CoefficientForObject>();
                factors.ForEach(factor =>
                {
	                factorsFromFile.Add(new CoefficientForObject(factor.AttributeId)
					{
						Coefficient = cells[factor.ColumnIndex].Value.ParseToDecimalNullable()
					});
                });

                modelObjectsFromExcel.Add(new ModelObjectsFromExcelData
                {
                    Id = cells[IdColumnIndex].Value.ParseToLongNullable(),
					IsForTraining = cells[IsForTrainingColumnIndex].Value.ParseToBooleanFromString(),
					IsForControl = cells[IsForControlColumnIndex].Value.ParseToBooleanFromString(),
                    IsExcluded = cells[IsExcludedColumnIndex].Value.ParseToBooleanFromString(),
                    RowIndexInFile = i,
					Factors = factorsFromFile
                });
            }

            var modelObjectsIds = modelObjectsFromExcel.Select(x => x.Id).ToList();
            if (modelObjectsIds.Count == 0)
            {
	            return new UpdateModelObjectsResult();
            }

            var result = new UpdateModelObjectsResult
            {
                TotalCount = modelObjectsFromExcel.Count
            };

            var modelObjects = OMModelToMarketObjects.Where(x => modelObjectsIds.Contains(x.Id))
	            .Select(x => new
	            {
		            x.IsForTraining,
		            x.IsForControl,
		            x.IsExcluded,
					x.Coefficients
	            }).Execute();

            foreach (var modelObjectFromExcel in modelObjectsFromExcel)
            {
	            var modelObject = modelObjects.FirstOrDefault(x => x.Id == modelObjectFromExcel.Id);
	            if (modelObject == null)
	            {
		            result.ErrorRowIndexes.Add(modelObjectFromExcel.RowIndexInFile);
		            continue;
	            }

				var coefficientsWereChanged = false;
	            var coefficientsFromDb = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
	            factors.ForEach(attribute =>
	            {
		            var coefficientFromDb = coefficientsFromDb.FirstOrDefault(x => x.AttributeId == attribute?.AttributeId);
		            var coefficientFromFile = modelObjectFromExcel.Factors.FirstOrDefault(x => x.AttributeId == attribute?.AttributeId);
		            if (coefficientFromDb != null && coefficientFromDb.Coefficient != coefficientFromFile?.Coefficient)
		            {
			            coefficientsWereChanged = true;
			            coefficientFromDb.Coefficient = coefficientFromFile?.Coefficient;
		            }
	            });

	            if (modelObject.IsForTraining.GetValueOrDefault() == modelObjectFromExcel.IsForTraining &&
	                modelObject.IsForControl.GetValueOrDefault() == modelObjectFromExcel.IsForControl &&
	                modelObject.IsExcluded.GetValueOrDefault() == modelObjectFromExcel.IsExcluded &&
	                !coefficientsWereChanged)
	            {
		            result.UnchangedObjectsCount++;
		            continue;
	            }

	            modelObject.IsForTraining = modelObjectFromExcel.IsForTraining;
	            modelObject.IsForControl = modelObjectFromExcel.IsForControl;
	            modelObject.IsExcluded = modelObjectFromExcel.IsExcluded;
	            modelObject.Coefficients = coefficientsFromDb.SerializeToXml();
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

		private List<FactorInFileInfo> GetFactorColumnsForModelObjectsInFile(long modelId)
		{
			//пока работаем только с Exp (был расчет МС и процента)
			var factors = ModelFactorsService.GetFactors(modelId, KoAlgoritmType.Exp);
			var result = new List<FactorInFileInfo>();
			factors.ForEach(x =>
			{
				var factorId = x.FactorId.GetValueOrDefault();
				var factorName = RegisterCache.GetAttributeData((int) x.FactorId.GetValueOrDefault()).Name;
				if (x.DictionaryId == null)
				{
					result.Add(new FactorInFileInfo
					{
						AttributeId = factorId,
						Name = factorName
					});
				}
				else
				{
					result.Add(new FactorInFileInfo
					{
						AttributeId = factorId,
						Name = $"{factorName} (Коэффициент)"
					});
					result.Add(new FactorInFileInfo
					{
						AttributeId = factorId,
						Name = $"{factorName} (Значение)",
						IsColumnWithValue = true
					});
				}
			});

			result = result.OrderBy(x => x.Name).ToList();
			result.ForEach(x => x.ColumnIndex = _maxNumberOfColumns++);
			
			return result;
		}

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

			var factors = ModelFactorsService.GetFactors(generalModel.Id, type);
			factors.ForEach(x =>
			{
				x.Weight = 0;
				x.Save();
			});

			generalModel.Save();
		}

		#endregion


		#region Entities

		private class FactorInFileInfo
		{
			public long AttributeId { get; set; }
			public string Name { get; set; }
			public int ColumnIndex { get; set; }
			public bool IsColumnWithValue { get; set; }
		}

		public class ModelObjectsCalculationParameters
        {
	        public decimal? ModelingPrice { get; set; }
	        public decimal? Percent { get; set; }
        }

        private class ModelObjectsFromExcelData
        {
	        public long? Id { get; set; }
	        public bool IsForTraining { get; set; }
	        public bool IsForControl { get; set; }
	        public bool IsExcluded { get; set; }
	        public List<CoefficientForObject> Factors { get; set; }
	        public int RowIndexInFile { get; set; }

	        public ModelObjectsFromExcelData()
	        {
		        Factors = new List<CoefficientForObject>();
	        }
		}

        public class UpdateModelObjectsResult
		{
			public int TotalCount { get; set; }
			public int UpdatedObjectsCount { get; set; }
			public int UnchangedObjectsCount { get; set; }
			public int ErrorObjectsCount => ErrorRowIndexes.Count;
			public List<int> ErrorRowIndexes { get; set; }
			public MemoryStream File { get; set; }

			public UpdateModelObjectsResult()
			{
				ErrorRowIndexes = new List<int>();
			}
		}

        #endregion
    }
}
