using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonSdks;
using CommonSdks.Excel;
using CommonSdks.PlatformWrappers;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using Microsoft.Practices.ObjectBuilder2;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Dictionaries.Entities;
using ModelingBusiness.Factors;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Model;
using ModelingBusiness.Modeling.Entities;
using ModelingBusiness.Modeling.Exceptions;
using ModelingBusiness.Modeling.Responses;
using ModelingBusiness.Objects;
using ModelingBusiness.Objects.Entities;
using ModelingBusiness.Objects.Repositories;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;
using ObjectModel.Modeling;
using Serilog;

namespace ModelingBusiness.Modeling
{
	public class ModelingService : IModelingService
	{
		private readonly ILogger _log = Log.ForContext<ModelingService>();
        private IModelService ModelService { get; }
        private IModelFactorsService ModelFactorsService { get; }
        private IModelObjectsRepository ModelObjectsRepository { get; }
        private IModelDictionaryService ModelDictionaryService { get; }
        public IRegisterCacheWrapper RegisterCacheWrapper { get; }
        

        public ModelingService(IModelService modelService = null,
			IModelFactorsService modelFactorsService = null,
			IModelObjectsRepository modelObjectsRepository = null,
			IModelDictionaryService modelDictionaryService = null,
			IRegisterCacheWrapper registerCacheWrapper = null)
        {
	        ModelService = modelService ?? new ModelService();
			ModelFactorsService = modelFactorsService ?? new ModelFactorsService();
			ModelObjectsRepository = modelObjectsRepository ?? new ModelObjectsRepository();
			ModelDictionaryService = modelDictionaryService ?? new ModelDictionaryService();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
		}


        #region Результаты обучения модели

        public TrainingDetailsDto GetTrainingResult(long modelId, KoAlgoritmType type)
        {
	        var model = ModelService.GetModelEntityById(modelId);

	        var trainingResultStr = model.GetTrainingResult(type);
	        if (string.IsNullOrWhiteSpace(trainingResultStr))
		        throw new Exception($"Не найдет результат обучения модели типа '{type.GetEnumDescription()}'");

	        var images = GetModelImages(modelId, type);
	        var trainingResult = JsonConvert.DeserializeObject<TrainingResponse>(trainingResultStr);

	        return new TrainingDetailsDto
	        {
		        ModelId = model.Id,
		        ModelName = model.Name,
		        Type = type,
		        MeanSquaredErrorTrain = trainingResult?.AccuracyScore?.MeanSquaredError?.Train,
		        MeanSquaredErrorTest = trainingResult?.AccuracyScore?.MeanSquaredError?.Test,
		        FisherCriterionTrain = trainingResult?.AccuracyScore?.FisherCriterion?.Estimated,
		        FisherCriterionTest = trainingResult?.AccuracyScore?.FisherCriterion?.Tabular,
		        R2Train = trainingResult?.AccuracyScore?.R2?.Train,
		        R2Test = trainingResult?.AccuracyScore?.R2?.Test,
		        ScatterImage = images?.Scatter,
		        CorrelationImage = images?.Correlation,
		        QualityControlInfo = trainingResult?.QualityControlInfo
	        };
        }

        public void ResetTrainingResults(long? modelId, KoAlgoritmType type)
        {
	        var model = ModelService.GetModelEntityById(modelId);
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
		        x.Correction = 0;
		        x.Save();
	        });

	        generalModel.Save();
        }

        public OMModelTrainingResultImages GetModelImages(long modelId, KoAlgoritmType type)
        {
	        return OMModelTrainingResultImages.Where(x => x.ModelId == modelId && x.AlgorithmType_Code == type)
		        .SelectAll()
		        .ExecuteFirstOrDefault();
        }

		#endregion


		#region Качество обученной модели

		public void UpdateTrainingQualityInfo(long modelId, KoAlgoritmType type, QualityControlInfo newQualityControlInfo)
		{
			var model = ModelService.GetModelEntityById(modelId);

			var trainingResultStr = model.GetTrainingResult(type);
			if (string.IsNullOrWhiteSpace(trainingResultStr))
				throw new Exception($"Не найдет результат обучения модели типа '{type.GetEnumDescription()}'");

			var trainingResult = JsonConvert.DeserializeObject<TrainingResponse>(trainingResultStr);
			trainingResult.QualityControlInfo.UpdateStudent(newQualityControlInfo.Student.Criterion, newQualityControlInfo.Student.Conclusion);
			trainingResult.QualityControlInfo.UpdateMse(newQualityControlInfo.MeanSquaredError.Criterion, newQualityControlInfo.MeanSquaredError.Conclusion);
			trainingResult.QualityControlInfo.UpdateR2(newQualityControlInfo.R2.Criterion, newQualityControlInfo.R2.Conclusion);
			trainingResult.QualityControlInfo.UpdateFisher(newQualityControlInfo.Fisher.Criterion, newQualityControlInfo.Fisher.Conclusion);

			var updatedTrainingResult = JsonConvert.SerializeObject(trainingResult);
			switch (type)
			{
				case KoAlgoritmType.Exp:
					model.ExponentialTrainingResult = updatedTrainingResult;
					break;
				case KoAlgoritmType.Line:
					model.LinearTrainingResult = updatedTrainingResult;
					break;
				case KoAlgoritmType.Multi:
					model.MultiplicativeTrainingResult = updatedTrainingResult;
					break;
				default:
					throw new Exception($"Передан неизвестный тип модели {type.GetEnumDescription()}");
			}

			model.Save();
		}

		public Stream ExportQualityInfoToExcel(long modelId, KoAlgoritmType type)
		{
			var model = ModelService.GetModelEntityById(modelId);

			var trainingResultStr = model.GetTrainingResult(type);
			if (string.IsNullOrWhiteSpace(trainingResultStr))
				throw new Exception($"Не найдет результат обучения модели типа '{type.GetEnumDescription()}'");

			var trainingResult = JsonConvert.DeserializeObject<TrainingResponse>(trainingResultStr);
			var qualityInfo = trainingResult.QualityControlInfo;

			var excelTemplate = new ExcelFile();
			var mainWorkSheet = excelTemplate.Worksheets.Add("Результаты");

			var columnHeadersRowIndex = 1;
			var calculationRowIndex = 2;
			var tableRowIndex = 3;
			var criterionRowIndex = 4;
			var conclusionRowIndex = 5;
			var rowHeaderColumnIndex = 0;
			var studentColumnIndex = 1;
			var mseColumnIndex = 2;
			var r2ColumnIndex = 3;
			var fisherColumnIndex = 4;

			var columnHeaders = new object[]
			{
				"", "t-критерий Стьюдента", "Средняя ошибка аппроксимации",
				"Коэффициент детерминации (R²)", "F-критерий Фишера"
			};
			ExcelFileHelper.SetIndividualWidth(mainWorkSheet, rowHeaderColumnIndex, 5);
			ExcelFileHelper.SetIndividualWidth(mainWorkSheet, studentColumnIndex, 4);
			ExcelFileHelper.SetIndividualWidth(mainWorkSheet, mseColumnIndex, 4);
			ExcelFileHelper.SetIndividualWidth(mainWorkSheet, r2ColumnIndex, 4);
			ExcelFileHelper.SetIndividualWidth(mainWorkSheet, fisherColumnIndex, 4);
			mainWorkSheet.Rows[0].Cells[0].SetValue("Анализ качества статической модели");
			var cells = mainWorkSheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnHeaders.Length - 1);
			cells.Merged = true;

			ExcelFileHelper.AddRow(mainWorkSheet, columnHeadersRowIndex, columnHeaders.ToArray());

			var studentInfo = qualityInfo.Student;
			var mseInfo = qualityInfo.MeanSquaredError;
			var r2Info = qualityInfo.R2;
			var fisherInfo = qualityInfo.Fisher;

			var firstRow = new object[]
			{
				"Расчетное", studentInfo.Estimated, mseInfo.Estimated, r2Info.Estimated, fisherInfo.Estimated
			};
			ExcelFileHelper.AddRow(mainWorkSheet, calculationRowIndex, firstRow);

			var secondRow = new object[]
			{
				"Табличное", studentInfo.Tabular, mseInfo.Tabular, r2Info.Tabular, fisherInfo.Tabular
			};
			ExcelFileHelper.AddRow(mainWorkSheet, tableRowIndex, secondRow);

			var thirdRow = new object[]
			{
				"Критерий", studentInfo.Criterion, mseInfo.Criterion, r2Info.Criterion, fisherInfo.Criterion
			};
			ExcelFileHelper.AddRow(mainWorkSheet, criterionRowIndex, thirdRow);

			var fifthRow = new object[]
			{
				"Вывод", studentInfo.Conclusion, mseInfo.Conclusion, r2Info.Conclusion, fisherInfo.Conclusion
			};
			ExcelFileHelper.AddRow(mainWorkSheet, conclusionRowIndex, fifthRow);

			cells = mainWorkSheet.Cells.GetSubrangeAbsolute(calculationRowIndex, mseColumnIndex, tableRowIndex, mseColumnIndex);
			cells.Merged = true;
			cells = mainWorkSheet.Cells.GetSubrangeAbsolute(calculationRowIndex, r2ColumnIndex, tableRowIndex, r2ColumnIndex);
			cells.Merged = true;

			var stream = new MemoryStream();
			excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);
			return stream;
		}

		#endregion


		#region Словари и метки

		public long? GetDictionaryId(long? groupId, long? factorId)
		{
			if (groupId.GetValueOrDefault() == 0 || factorId.GetValueOrDefault() == 0)
				return null;

			var activeModel = ModelService.GetActiveModelEntityByGroupId(groupId);
			if (activeModel == null)
				return null;

			var modelFactors = ModelFactorsService.GetGeneralModelFactors(activeModel.Id);
			var dictionaryId = modelFactors.FirstOrDefault(x => x.AttributeId == factorId)?.DictionaryId;

			return dictionaryId;
		}

		public string CalculateMarks(long modelId)
		{
			var model = ModelService.GetModelEntityById(modelId);
			if (!model.IsAutomatic)
				throw new CanNotCreateMarksForNonAutomaticModelException();

			var modelObjects = ModelObjectsRepository.GetIncludedModelObjects(modelId, IncludedObjectsMode.Training,
				select => new {select.CadastralNumber, select.Coefficients, select.Price});
			if (modelObjects.IsEmpty())
				throw new CanNotCreateMarksBecauseNoMarketObjectsException();

			var factors = ModelFactorsService.GetGeneralModelFactors(modelId)
				.Where(x => x.MarkType == MarkType.Default && x.IsActive).ToList();
			if (factors.IsEmpty())
				throw new CanNotCreateMarksBecauseNoFactorsException();

			var urlToDownloadReport = ProcessModelObjectsWithEmptyFactors(modelObjects, factors);

			factors.ForEach(factor =>
			{
				if (factor.DictionaryId == null)
					throw new CanNotCreateMarksBecauseNoDictionaryException(factor.AttributeName);

				ProcessCodedFactor(factor, modelObjects);
			});

			return urlToDownloadReport;
		}


		#region Support Methods

		private string ProcessModelObjectsWithEmptyFactors(List<OMModelToMarketObjects> modelObjects,
			List<ModelFactorRelationPure> factors)
		{
			var factorIds = factors.Select(x => x.AttributeId).ToList();
			var modelObjectsWithEmptyFactors = modelObjects.Where(x => 
				x.DeserializedCoefficients.Any(c => string.IsNullOrWhiteSpace(c.Value) && factorIds.Contains(c.AttributeId))).ToList();
			if (modelObjectsWithEmptyFactors.Count == 0)
				return string.Empty;

			var reportService = new GbuReportService("Объекты, не участвующие в формировании меток");

			var descriptionColumnIndex = 0;
			var factorsColumnIndex = 1;
			var headers = new List<GbuReportService.Column>
			{
				new()
				{
					Header = "Описание объекта аналога",
					Index = descriptionColumnIndex,
					Width = 8
				},
				new()
				{
					Header = "Незаполненный факторы",
					Index = factorsColumnIndex,
					Width = 12
				}
			};

			reportService.AddHeaders(headers);
			reportService.SetIndividualWidth(headers);

			modelObjectsWithEmptyFactors.ForEach(obj =>
			{
				var factorNames = string.Empty;
				factors.ForEach(factor =>
				{
					var coefficient = obj.DeserializedCoefficients.FirstOrDefault(c => c.AttributeId == factor.AttributeId);
					if (coefficient != null && string.IsNullOrWhiteSpace(coefficient.Value))
						factorNames += $"{factor.AttributeName} {Environment.NewLine}";
				});

				var row = reportService.GetCurrentRow();
				reportService.AddValue(obj.CadastralNumber, descriptionColumnIndex, row);
				reportService.AddValue(factorNames, factorsColumnIndex, row);
				
				modelObjects.Remove(obj);
			});

			var reportId = reportService.SaveReport();
			return reportService.GetUrlToDownloadFile(reportId);
		}

		private void ProcessCodedFactor(ModelFactorRelationPure factor, List<OMModelToMarketObjects> modelObjects)
		{
			var uniqueFactorValues = GetUniqueFactorValues(factor, modelObjects);
			if (uniqueFactorValues.Count == 0)
				return;

			var uniqueValuesAveragePrices = CalculateUniqueValuesAveragePrices(factor.AttributeId, uniqueFactorValues, modelObjects);
			if (uniqueValuesAveragePrices.Count == 0)
				return;

			var allValuesAveragePrices = uniqueValuesAveragePrices.Values;
			var divider = uniqueFactorValues.Count % 2 == 0 ? allValuesAveragePrices.Average() : CalculateMedian(allValuesAveragePrices.ToList());

			CreateMarks(factor, uniqueValuesAveragePrices, divider);
		}

		private HashSet<string> GetUniqueFactorValues(ModelFactorRelationPure factor, List<OMModelToMarketObjects> modelObjects)
		{
			return modelObjects
				.SelectMany(x => x.DeserializedCoefficients)
				.Where(x => x.AttributeId == factor.AttributeId && !string.IsNullOrWhiteSpace(x.Value))
				.Select(x => x.Value)
				.ToHashSet();
		}

		private Dictionary<string, decimal> CalculateUniqueValuesAveragePrices(long attributeId,
			HashSet<string> uniqueFactorValues, List<OMModelToMarketObjects> modelObjects)
		{
			var uniqueValuesAveragePrice = new Dictionary<string, decimal>();

			uniqueFactorValues.ForEach(uniqueValue =>
			{
				var objectsPriceSumWithUniqueValue = 0m;
				var objectsCountWithUniqueValue = 0;
				modelObjects.ForEach(obj =>
				{
					var coefficient = obj.DeserializedCoefficients.FirstOrDefault(x => x.AttributeId == attributeId && x.Value == uniqueValue);
					if (coefficient == null)
						return;

					objectsPriceSumWithUniqueValue += obj.Price;
					objectsCountWithUniqueValue++;
				});
				//todo average?
				uniqueValuesAveragePrice[uniqueValue] = objectsPriceSumWithUniqueValue / objectsCountWithUniqueValue;
			});

			return uniqueValuesAveragePrice;
		}

		private void CreateMarks(ModelFactorRelationPure factor, Dictionary<string, decimal> uniqueValuesAveragePrice, decimal divider)
		{
			if (divider == 0)
				throw new Exception($"Средняя цена объектов с фактором '{factor.AttributeName}' равна нулю");

			foreach (var valuePrice in uniqueValuesAveragePrice)
			{
				var mark = new DictionaryMarkDto
				{
					DictionaryId = factor.DictionaryId.Value,
					Value = valuePrice.Key,
					CalculationValue = valuePrice.Value / divider
				};

				//todo если буду проблемы с производительностью, формировать sql-запрос через строку
				ModelDictionaryService.CreateMark(mark);
			}
		}

		private decimal CalculateMedian(List<decimal> prices)
		{
			var count = prices.Count;
			var halfIndex = prices.Count / 2;
			var sortedPrices = prices.OrderBy(n => n).ToList();
			decimal median;
			if (count % 2 == 0)
			{
				median = (sortedPrices.ElementAt(halfIndex) + sortedPrices.ElementAt(halfIndex - 1)) / 2;
			}
			else
			{
				median = sortedPrices.ElementAt(halfIndex);
			}

			return median;
		}

		#endregion

		#endregion
	}
}
