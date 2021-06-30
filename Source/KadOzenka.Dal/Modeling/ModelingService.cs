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
        private IModelingRepository ModelingRepository { get; set; }
        private IModelObjectsRepository ModelObjectsRepository { get; set; }
        private IModelFactorsService ModelFactorsService { get; set; }
        private RecycleBinService RecycleBinService { get; }
        public IRegisterCacheWrapper RegisterCacheWrapper { get; }
        

        public ModelingService(IModelingRepository modelingRepository = null,
			IModelObjectsRepository modelObjectsRepository = null,
			IModelFactorsService modelFactorsService = null,
			IRegisterCacheWrapper registerCacheWrapper = null)
		{
			ModelFactorsService = modelFactorsService ?? new ModelFactorsService();
			ModelingRepository = modelingRepository ?? new ModelingRepository();
			RecycleBinService = new RecycleBinService();
			ModelObjectsRepository = modelObjectsRepository ?? new ModelObjectsRepository();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
		}


		#region CRUD General Model

		public OMModel GetActiveModelEntityByGroupId(long? groupId)
        {
	        if (groupId == null)
		        throw new Exception("Не передан идентификатор Группы для поиска модели");

			var model = ModelingRepository.GetEntityByCondition(x => x.GroupId == groupId && x.IsActive.Coalesce(false) == true, null);

			return model;
        }

		public List<OMModel> GetActiveModelsEntityByGroupId(long? groupId)
		{
			if (groupId == null)
				throw new Exception("Не передан идентификатор Группы для поиска модели");

			return OMModel.Where(x => x.GroupId == groupId)
				.OrderByDescending(x => x.IsActive.Coalesce(false)).OrderBy(x => x.Name)
				.Select(x => new
				{
					x.Id,
					x.Name
				})
				.Execute();
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
		        Type_Code = KoModelType.Manual,
		        Formula = "-"
			};

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
	            //existedModel.Formula = GetFormula(existedModel);

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
		        var hasFormedObjectArray = ModelObjectsRepository.AreIncludedModelObjectsExist(modelId, IncludedObjectsMode.Training);
		        var hasTrainingResult = !string.IsNullOrWhiteSpace(model.LinearTrainingResult) ||
		                                !string.IsNullOrWhiteSpace(model.ExponentialTrainingResult) ||
		                                !string.IsNullOrWhiteSpace(model.MultiplicativeTrainingResult);
		        if (!hasFormedObjectArray || !hasTrainingResult)
			        throw new Exception(Messages.CanNotActivateNotPreparedAutomaticModel);
			}
	        
			using (var ts = new TransactionScope())
			{
				var otherModelsForGroup = ModelingRepository.GetEntitiesByCondition(
					x => x.GroupId == model.GroupId && x.IsActive.Coalesce(false) == true, x => new {x.IsActive});
				otherModelsForGroup.ForEach(x =>
				{
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
				throw new ModelCrudException(message.ToString());
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


		#region Training Result

		public TrainingDetailsDto GetTrainingResult(long modelId, KoAlgoritmType type)
		{
			var model = GetModelEntityById(modelId);

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

		public void UpdateTrainingQualityInfo(long modelId, KoAlgoritmType type, QualityControlInfo newQualityControlInfo)
		{
			var model = GetModelEntityById(modelId);

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
			var model = GetModelEntityById(modelId);

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

		#endregion


		#region Formulas

		public string GetFormula(OMModel model, KoAlgoritmType algorithmType)
		{
			//для ручной модели существует один набор факторов под алгоритм самой модели
			//для автоматической модели набор факторов разный под тип алгоритма
			var factors = ModelFactorsService.GetFactors(model.Id, model.AlgoritmType_Code);
			if (factors.Count == 0)
				throw new FormulaCreationException("Невозможно сформировать формулу, т.к. у модели нет факторов");

			var formulaCreator = GetFormulaCreator(algorithmType);

			var factorsInFormula = new StringBuilder();

			factors.ForEach(x =>
			{
				var attributeName = $"\"{RegisterCacheWrapper.GetAttributeData(x.FactorId.GetValueOrDefault()).Name}\"";
				var weightInFormula = ProcessNumber(x.WeightInFormula);
				var b0InFormula = ProcessNumber(x.B0InFormula);
				var correctingTermInFormula = ProcessNumber(x.CorrectingTermInFormula);
				var kInFormula = ProcessNumber(x.KInFormula);
				var modelInfo = new ModelInfoForFormula(attributeName, weightInFormula, b0InFormula,
					correctingTermInFormula, kInFormula);

				switch (x.MarkType_Code)
				{
					case MarkType.None:
						factorsInFormula.Append(formulaCreator.GetPartForNoneMarkType(modelInfo));
						break;
					case MarkType.Default:
						factorsInFormula.Append(formulaCreator.GetPartForDefaultMarkType(modelInfo));
						break;
					case MarkType.Straight:
						factorsInFormula.Append(formulaCreator.GetPartForStraightMarkType(modelInfo));
						break;
					case MarkType.Reverse:
						factorsInFormula.Append(formulaCreator.GetPartForReverseMarkType(modelInfo));
						break;
					default:
						throw new FormulaCreationException(
							$"Передан неизвестный тип метки: '{x.MarkType_Code.GetEnumDescription()}'");
				}

				factorsInFormula.Append(formulaCreator.FactorsSeparator);
			});

			var processedFactors = factorsInFormula.ToString().TrimEnd(formulaCreator.FactorsSeparator);
			return formulaCreator.GetBaseFormulaPart(model, processedFactors);
		}


		#region Support Methods

		private BaseFormula GetFormulaCreator(KoAlgoritmType algorithmType)
		{
			switch (algorithmType)
			{
				case KoAlgoritmType.None:
					throw new FormulaCreationException("Не передан тип алгоритма расчета модели");
				case KoAlgoritmType.Exp:
					return new ExponentialFormula();
				case KoAlgoritmType.Line:
					return new LinearFormula();
				case KoAlgoritmType.Multi:
					return new MultiplicativeFormula();
				default:
					throw new FormulaCreationException($"Передан неизвестный тип алгоритма расчета модели: '{algorithmType.GetEnumDescription()}'");
			}
		}

		private string ProcessNumber(decimal number)
		{
			var numberInFormula = $"{number}";
			if (number < 0)
			{
				numberInFormula = $"({numberInFormula})";
			}

			//все стронние библиотеки (для отрисовки формулы и расчета) работают с ".",
			//но разделить для культуры в приложении - ","
			return numberInFormula.Replace(",", ".");
		}

		#endregion

		#endregion
	}
}
