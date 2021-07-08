using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Modeling;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.Modeling.Entities;
using KadOzenka.Dal.Modeling.Exceptions;
using KadOzenka.Dal.Modeling.Repositories;
using KadOzenka.Dal.Modeling.Resources;
using Serilog;
using System.Linq.Expressions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Modeling.Formulas;
using KadOzenka.Dal.RecycleBin;
using Newtonsoft.Json;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingService : IModelingService
	{
		private readonly ILogger _log = Log.ForContext<ModelingService>();
        private IModelService ModelService { get; set; }
        private IModelingRepository ModelingRepository { get; set; }
        private IModelObjectsRepository ModelObjectsRepository { get; set; }
        private IModelFactorsService ModelFactorsService { get; set; }
        private RecycleBinService RecycleBinService { get; }
        public IRegisterCacheWrapper RegisterCacheWrapper { get; }
        

        public ModelingService(IModelingRepository modelingRepository = null,
	        IModelService modelService = null,
			IModelObjectsRepository modelObjectsRepository = null,
			IModelFactorsService modelFactorsService = null,
			IRegisterCacheWrapper registerCacheWrapper = null)
        {
	        ModelService = modelService ?? new ModelService();
			ModelFactorsService = modelFactorsService ?? new ModelFactorsService();
			ModelingRepository = modelingRepository ?? new ModelingRepository();
			RecycleBinService = new RecycleBinService();
			ModelObjectsRepository = modelObjectsRepository ?? new ModelObjectsRepository();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
		}



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
				x.Weight = 0;
				x.Save();
			});

			generalModel.Save();
		}

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
			DataExportCommon.SetIndividualWidth(mainWorkSheet, rowHeaderColumnIndex, 5);
			DataExportCommon.SetIndividualWidth(mainWorkSheet, studentColumnIndex, 4);
			DataExportCommon.SetIndividualWidth(mainWorkSheet, mseColumnIndex, 4);
			DataExportCommon.SetIndividualWidth(mainWorkSheet, r2ColumnIndex, 4);
			DataExportCommon.SetIndividualWidth(mainWorkSheet, fisherColumnIndex, 4);
			mainWorkSheet.Rows[0].Cells[0].SetValue("Анализ качества статической модели");
			var cells = mainWorkSheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnHeaders.Length - 1);
			cells.Merged = true;

			DataExportCommon.AddRow(mainWorkSheet, columnHeadersRowIndex, columnHeaders.ToArray());

			var studentInfo = qualityInfo.Student;
			var mseInfo = qualityInfo.MeanSquaredError;
			var r2Info = qualityInfo.R2;
			var fisherInfo = qualityInfo.Fisher;

			var firstRow = new object[]
			{
				"Расчетное", studentInfo.Estimated, mseInfo.Estimated, r2Info.Estimated, fisherInfo.Estimated
			};
			DataExportCommon.AddRow(mainWorkSheet, calculationRowIndex, firstRow);

			var secondRow = new object[]
			{
				"Табличное", studentInfo.Tabular, mseInfo.Tabular, r2Info.Tabular, fisherInfo.Tabular
			};
			DataExportCommon.AddRow(mainWorkSheet, tableRowIndex, secondRow);

			var thirdRow = new object[]
			{
				"Критерий", studentInfo.Criterion, mseInfo.Criterion, r2Info.Criterion, fisherInfo.Criterion
			};
			DataExportCommon.AddRow(mainWorkSheet, criterionRowIndex, thirdRow);

			var fifthRow = new object[]
			{
				"Вывод", studentInfo.Conclusion, mseInfo.Conclusion, r2Info.Conclusion, fisherInfo.Conclusion
			};
			DataExportCommon.AddRow(mainWorkSheet, conclusionRowIndex, fifthRow);

			cells = mainWorkSheet.Cells.GetSubrangeAbsolute(calculationRowIndex, mseColumnIndex, tableRowIndex, mseColumnIndex);
			cells.Merged = true;
			cells = mainWorkSheet.Cells.GetSubrangeAbsolute(calculationRowIndex, r2ColumnIndex, tableRowIndex, r2ColumnIndex);
			cells.Merged = true;

			var stream = new MemoryStream();
			excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);
			return stream;
		}

		public OMModelTrainingResultImages GetModelImages(long modelId, KoAlgoritmType type)
		{
			return OMModelTrainingResultImages.Where(x => x.ModelId == modelId && x.AlgorithmType_Code == type)
				.SelectAll()
				.ExecuteFirstOrDefault();
		}
	}
}
