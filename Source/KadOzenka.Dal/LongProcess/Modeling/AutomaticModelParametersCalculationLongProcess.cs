using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonSdks;
using CommonSdks.Excel;
using CommonSdks.PlatformWrappers;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.Common;
using Microsoft.Practices.ObjectBuilder2;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Factors;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Factors.Exceptions.AutomaticModelParametersCalculation;
using ModelingBusiness.Factors.Repositories;
using ModelingBusiness.Model;
using ModelingBusiness.Modeling.Exceptions;
using ModelingBusiness.Objects.Entities;
using ModelingBusiness.Objects.Repositories;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;
using ObjectModel.Modeling;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Modeling
{
    public class AutomaticModelParametersCalculationLongProcess : LongProcess
    {
	    private string _messageSubject = "Результат Операции Расчета параметров для автоматической модели";
	    private int _maxFactorsCount;
	    private int _processedFactorsCount;
	    private readonly ILogger _logger = Log.ForContext<MarksCalculationLongProcess>();
	    private IModelService ModelService { get; }
		private IModelFactorsService ModelFactorsService { get; }
		private IModelObjectsRepository ModelObjectsRepository { get; }
		private IModelFactorsRepository ModelFactorsRepository { get; }
		private IModelDictionaryService ModelDictionaryService { get; }
		private IRegisterCacheWrapper RegisterCacheWrapper { get; }



		public AutomaticModelParametersCalculationLongProcess(IModelService modelService = null,
			IModelFactorsService modelFactorsService = null,
			IModelObjectsRepository modelObjectsRepository = null,
			IModelFactorsRepository modelFactorsRepository = null,
			IModelDictionaryService modelDictionaryService = null,
			IRegisterCacheWrapper registerCacheWrapper = null)
		{
			ModelService = modelService ?? new ModelService();
			ModelFactorsService = modelFactorsService ?? new ModelFactorsService();
			ModelObjectsRepository = modelObjectsRepository ?? new ModelObjectsRepository();
			ModelFactorsRepository = modelFactorsRepository ?? new ModelFactorsRepository();
			ModelDictionaryService = modelDictionaryService ?? new ModelDictionaryService();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
		}



		//public static void AddProcessToQueue(long modelId)
		//{
		//	ValidateModelId(modelId);

		//	CheckActiveProcessInQueue(modelId);

		//	LongProcessManager.AddTaskToQueue(nameof(MarksCalculationLongProcess), objectId: modelId, registerId: OMModel.GetRegisterId());
  //      }

		//public static void CheckActiveProcessInQueue(long modelId)
		//{
		//	var processId = 100;
		//	var isProcessExists = new LongProcessService().HasActiveProcessInQueue(processId, modelId);
		//	if (isProcessExists)
		//		throw new Exception("В очереди есть процесс расчета меток для этой модели");
		//}


		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_logger.Debug("Старт процесса расчета параметров для автоматической модели");

			var modelId = processQueue.ObjectId.GetValueOrDefault();
			ValidateModelId(modelId);

			try
			{
				LongProcessProgressLogger.StartLogProgress(processQueue, () => _maxFactorsCount,
					() => _processedFactorsCount);

				var urlToDownloadReport = CalculateParameters(modelId, cancellationToken);

				var downloadReportElement = string.IsNullOrWhiteSpace(urlToDownloadReport)
					? string.Empty
					: $@"<a href=""{urlToDownloadReport}"">Скачать отчет с ошибками</a>";

				var message = "Операция завершена. " + downloadReportElement;
				NotificationSender.SendNotification(processQueue, _messageSubject, message);
			}
			catch (OperationCanceledException ex)
			{
				_logger.Error(ex, "Операция остановлена пользователем");
				NotificationSender.SendNotification(processQueue, _messageSubject, "Операция была остановлена пользователем");
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Ошибка в ходе расчета параметров для автоматической модели");
				var errorId = ErrorManager.LogError(ex); 
				NotificationSender.SendNotification(processQueue, _messageSubject, $"Операция завершена с ошибкой: {ex.Message} (Подробнее в журнале: {errorId})");
			}

			LongProcessProgressLogger.StopLogProgress();

			_logger.Debug("Финиш процесса расчета параметров для автоматической модели");
			WorkerCommon.SetProgress(processQueue, 100);
		}

		public string CalculateParameters(long modelId, CancellationToken cancellationToken)
        {
	        var model = ModelService.GetModelEntityById(modelId);
	        if (!model.IsAutomatic)
		        throw new CanNotCalculateParametersForNonAutomaticModelException();

	        var factors = GetModelFactors(modelId);

			var modelObjects = GetModelObjects(modelId, factors, cancellationToken);

	        var urlToDownloadReport = ProcessModelObjectsWithEmptyValues(modelObjects, factors, cancellationToken);

			factors.ForEach(factor =>
			{
				cancellationToken.ThrowIfCancellationRequested();

				ProcessUnCodedFactor(factor, modelObjects);

				_processedFactorsCount++;
			});
			_logger.Debug("Расчет всех факторов закончен. Начато обновление коэффициентов в объектах моделирования");

			return urlToDownloadReport;
        }

		
		#region Support Methods

		private static void ValidateModelId(long? modelId)
		{
			if (modelId.GetValueOrDefault() == 0)
				throw new Exception("Не передан ИД модели");
		}

		private List<OMModelToMarketObjects> GetModelObjects(long modelId,
			List<ModelFactorRelationPure> factors, CancellationToken cancellationToken)
		{
			using (_logger.TimeOperation("Получение объектов моделирования для модели с ИД '{ModelId}'", modelId))
			{
				var modelObjects = ModelObjectsRepository.GetIncludedModelObjects(modelId, IncludedObjectsMode.Training,
					cancellationToken,
					select => new { select.MarketObjectInfo, select.Coefficients, select.Price });
				if (modelObjects.IsEmpty())
					throw new CanNotCalculateParametersBecauseNoMarketObjectsException();

				_logger.Debug("Всего найдено {ModelObjectsCount} объектов модели для модели с ИД '{ModelId}'", modelObjects.Count, modelId);

				var factorIds = factors.Select(x => x.AttributeId).ToList();
				var modelObjectsWithSelectedFactors = modelObjects.Where(x => x.DeserializedCoefficients.Any(c => factorIds.Contains(c.AttributeId))).ToList();
				if (modelObjectsWithSelectedFactors.Count == 0)
					throw new CanNotCreateMarksBecauseNoMarketObjectsWithSelectedFactorsException();

				return modelObjectsWithSelectedFactors;
			}
		}

		private List<ModelFactorRelationPure> GetModelFactors(long modelId)
		{
			var factors = ModelFactorsService.GetGeneralModelFactors(modelId)
				.Where(x => (x.MarkType == MarkType.Reverse || x.MarkType == MarkType.Straight) && x.IsActive)
				.ToList();
			if (factors.IsEmpty())
				throw new CanNotCalculateParametersBecauseNoFactorsException();

			_maxFactorsCount = factors.Count;
			_logger.Debug("Найдено {FactorsCount} активных факторов с прямой или обратной меткой для модели с ИД '{ModelId}'", factors.Count, modelId);

			return factors;
		}

		private string ProcessModelObjectsWithEmptyValues(List<OMModelToMarketObjects> modelObjects,
			List<ModelFactorRelationPure> factors, CancellationToken cancellationToken)
		{
			using (_logger.TimeOperation("Обработка объектов моделирования, у которых есть пустые факторы"))
			{
				var factorIds = factors.Select(x => x.AttributeId).ToList();

				var modelObjectsWithEmptyFactors = modelObjects.Where(x =>
					x.DeserializedCoefficients.Any(c =>
						string.IsNullOrWhiteSpace(c.Value) && factorIds.Contains(c.AttributeId))).ToList();

				_logger.Debug("Найдено {ModelObjectsWithEmptyFactorsCount} объектов модели с пустыми факторами'", modelObjectsWithEmptyFactors.Count);

				if(modelObjectsWithEmptyFactors.Count == 0)
					return string.Empty;

				var reportService = new GbuReportService("Объекты, не участвующие в расчете параметров для автоматической модели");

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
						Header = "Незаполненные факторы",
						Index = factorsColumnIndex,
						Width = 12
					}
				};

				reportService.AddHeaders(headers);
				reportService.SetIndividualWidth(headers);

				modelObjectsWithEmptyFactors.ForEach(obj =>
				{
					cancellationToken.ThrowIfCancellationRequested();

					var factorNames = string.Empty;
					factors.ForEach(factor =>
					{
						var coefficient = obj.DeserializedCoefficients.FirstOrDefault(c => c.AttributeId == factor.AttributeId);
						if (coefficient != null && string.IsNullOrWhiteSpace(coefficient.Value))
							factorNames += $"{factor.AttributeName}{Environment.NewLine}";
					});

					var row = reportService.GetCurrentRow();
					reportService.AddValue(obj.MarketObjectInfo, descriptionColumnIndex, row);
					reportService.AddValue(factorNames, factorsColumnIndex, row);

					ExcludeInvalidModelObject(modelObjects, obj);
				});

				var reportId = reportService.SaveReport();

				return reportService.GetUrlToDownloadFile(reportId);
			}
		}

		private void ExcludeInvalidModelObject(List<OMModelToMarketObjects> modelObjects, OMModelToMarketObjects invalidObject)
		{
			modelObjects.Remove(invalidObject);
			
			invalidObject.IsExcluded = true;
			invalidObject.Save();
		}

		public void ProcessUnCodedFactor(ModelFactorRelationPure factor, long modelId, List<OMModelToMarketObjects> modelObjects)
		{
			using (_logger.TimeOperation("Полная обработка фактора '{FactorName}'", factor.AttributeName))
			{
				var coefficients = modelObjects.SelectMany(x => x.DeserializedCoefficients)
					.Where(x => x.AttributeId == factor.AttributeId && x.Coefficient != null)
					.Select(x => x.Coefficient.GetValueOrDefault()).ToList();

				var k = CalculateK(coefficients);
				var correctionTerm = CalculateCorrectionTerm(coefficients);

				var omFactors = ModelFactorsService.GetFactors(modelId, KoAlgoritmType.None);
				omFactors.ForEach(x =>
				{
					x.K = k;
					x.CorrectingTerm = correctionTerm;
					ModelFactorsRepository.Save(x);
				});
			}
		}

		public decimal CalculateK(List<decimal> coefficients)
		{
			var average = coefficients.Average();
			var median = CalculateMedian(coefficients);

			return (average + median) / 2m;
		}

		public decimal CalculateCorrectionTerm(List<decimal> coefficients)
		{
			var maxCoefficient = coefficients.Max();
			var minCoefficient = coefficients.Min();

			return 0.2m * (maxCoefficient - minCoefficient);
		}

		private decimal CalculateMedian(List<decimal> prices)
		{
			using (_logger.TimeOperation("Расчет медианного значения по {PricesCount} ценам", prices.Count))
			{
				return MathExtended.CalculateMedian(prices);
			}
		}

		#endregion
    }
}
