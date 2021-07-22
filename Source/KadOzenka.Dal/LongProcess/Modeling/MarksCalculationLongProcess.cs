using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
using ModelingBusiness.Model;
using ModelingBusiness.Modeling.Exceptions;
using ModelingBusiness.Objects.Entities;
using ModelingBusiness.Objects.Repositories;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;
using ObjectModel.Modeling;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Modeling
{
    public class MarksCalculationLongProcess : LongProcess
    {
	    private static long ProcessId => 100;
	    private string _messageSubject = "Результат Операции Расчета меток";
	    private int MaxFactorsCount = 0;
	    private int ProcessedFactorsCount = 0;
	    private readonly ILogger _logger = Log.ForContext<MarksCalculationLongProcess>();
	    private IModelService ModelService { get; }
		private IModelFactorsService ModelFactorsService { get; }
		private IModelObjectsRepository ModelObjectsRepository { get; }
		private IModelDictionaryService ModelDictionaryService { get; }
		public IRegisterCacheWrapper RegisterCacheWrapper { get; }



		public MarksCalculationLongProcess(IModelService modelService = null,
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



		public static void AddProcessToQueue(long modelId)
		{
			ValidateModelId(modelId);

			var isProcessExists = new LongProcessService().HasActiveProcessInQueue(ProcessId, modelId);
	        if (isProcessExists)
		        throw new Exception("Процесс расчета меток для этой модели уже находится в очереди");

			LongProcessManager.AddTaskToQueue(nameof(MarksCalculationLongProcess), objectId: modelId, registerId: OMModel.GetRegisterId());
        }


        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			var modelId = processQueue.ObjectId.GetValueOrDefault();
			ValidateModelId(modelId);

			try
			{
				LongProcessProgressLogger.StartLogProgress(processQueue, () => MaxFactorsCount, () => ProcessedFactorsCount);

				var urlToDownloadReport = CalculateMarks(modelId);
				
				var downloadReportElement = string.IsNullOrWhiteSpace(urlToDownloadReport) 
					? string.Empty 
					: $@"<a href=""{urlToDownloadReport}"">Скачать отчет с ошибками</a>";

				var message = "Операция успешно завершена." + downloadReportElement;
				NotificationSender.SendNotification(processQueue, _messageSubject, message);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Ошибка в ходе расчета меток");
				var errorId = ErrorManager.LogError(ex); 
				NotificationSender.SendNotification(processQueue, _messageSubject, $"Операция завершена с ошибкой: {ex.Message} (Подробнее в журнале: {errorId})");
			}

			LongProcessProgressLogger.StopLogProgress();
		}

        public string CalculateMarks(long modelId)
        {
	        var model = ModelService.GetModelEntityById(modelId);
	        if (!model.IsAutomatic)
		        throw new CanNotCreateMarksForNonAutomaticModelException();

	        var factors = GetModelFactors(modelId);

			var modelObjects = GetModelObjects(modelId);

	        var urlToDownloadReport = ProcessModelObjectsWithEmptyFactors(modelObjects, factors);

	        factors.ForEach(factor =>
	        {
		        if (factor.DictionaryId == null)
			        throw new CanNotCreateMarksBecauseNoDictionaryException(factor.AttributeName);

		        ProcessCodedFactor(factor, modelObjects);
		        
		        ProcessedFactorsCount++;
	        });

	        return urlToDownloadReport;
        }

		
		#region Support Methods

		private static void ValidateModelId(long? modelId)
		{
			if (modelId.GetValueOrDefault() == 0)
				throw new Exception("Не передан ИД модели");
		}

		private List<OMModelToMarketObjects> GetModelObjects(long modelId)
		{
			using (_logger.TimeOperation("Получение объектов моделирования для модели с ИД '{ModelId}'", modelId))
			{
				var modelObjects = ModelObjectsRepository.GetIncludedModelObjects(modelId, IncludedObjectsMode.Training,
					select => new { select.CadastralNumber, select.Coefficients, select.Price });
				if (modelObjects.IsEmpty())
					throw new CanNotCreateMarksBecauseNoMarketObjectsException();

				_logger.Debug("Найдено {ModelObjectsCount} объектов модели для модели с ИД '{ModelId}'", modelObjects.Count, modelId);

				return modelObjects;
			}
		}

		private List<ModelFactorRelationPure> GetModelFactors(long modelId)
		{
			var factors = ModelFactorsService.GetGeneralModelFactors(modelId)
				.Where(x => x.MarkType == MarkType.Default && x.IsActive).ToList();
			if (factors.IsEmpty())
				throw new CanNotCreateMarksBecauseNoFactorsException();

			MaxFactorsCount = factors.Count;
			_logger.Debug("Найдено {FactorsCount} активных факторов с меткой по умолчанию для модели с ИД '{ModelId}'", factors.Count, modelId);

			return factors;
		}

		private string ProcessModelObjectsWithEmptyFactors(List<OMModelToMarketObjects> modelObjects,
			List<ModelFactorRelationPure> factors)
		{
			var factorIds = factors.Select(x => x.AttributeId).ToList();
			var modelObjectsWithEmptyFactors = modelObjects.Where(x =>
				x.DeserializedCoefficients.Any(c => string.IsNullOrWhiteSpace(c.Value) && factorIds.Contains(c.AttributeId))).ToList();
			if (modelObjectsWithEmptyFactors.Count == 0)
				return string.Empty;

			_logger.Debug("Найдено {ModelObjectsWithEmptyFactorsCount} объектов модели с пустыми факторами'", modelObjectsWithEmptyFactors.Count);


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
					Header = "Незаполненные факторы",
					Index = factorsColumnIndex,
					Width = 12
				}
			};

			reportService.AddHeaders(headers);
			reportService.SetIndividualWidth(headers);

			using (_logger.TimeOperation("Формирование отчета с объектами моделирования, у которых есть пустые факторы"))
			{
				modelObjectsWithEmptyFactors.ForEach(obj =>
				{
					var factorNames = string.Empty;
					factors.ForEach(factor =>
					{
						var coefficient = obj.DeserializedCoefficients.FirstOrDefault(c => c.AttributeId == factor.AttributeId);
						if (coefficient != null && string.IsNullOrWhiteSpace(coefficient.Value))
							factorNames += $"{factor.AttributeName}{Environment.NewLine}";
					});

					var row = reportService.GetCurrentRow();
					reportService.AddValue(obj.CadastralNumber, descriptionColumnIndex, row);
					reportService.AddValue(factorNames, factorsColumnIndex, row);

					modelObjects.Remove(obj);
				});

				var reportId = reportService.SaveReport();
				return reportService.GetUrlToDownloadFile(reportId);
			}
		}

		private void ProcessCodedFactor(ModelFactorRelationPure factor, List<OMModelToMarketObjects> modelObjects)
		{
			using (_logger.TimeOperation("Полная обработка фактора '{FactorName}'", factor.AttributeName))
			{
				var uniqueFactorValues = GetUniqueFactorValues(factor, modelObjects);
				if (uniqueFactorValues.Count == 0)
					return;

				var uniqueFactorValuesInfo = CalculateUniqueValuesAveragePrices(factor.AttributeId, uniqueFactorValues, modelObjects);
				if (uniqueFactorValuesInfo.Count == 0)
					return;

				var allValuesAveragePrices = uniqueFactorValuesInfo.Values.Select(x => x.AveragePrice);
				var divider = uniqueFactorValues.Count % 2 == 0 ? allValuesAveragePrices.Average() : CalculateMedian(allValuesAveragePrices.ToList());
				_logger.Debug("Делитель для фактора '{FactorName}' = {Divider}", factor.AttributeName, divider);

				CreateMarks(factor, uniqueFactorValuesInfo, divider);
			}
		}

		private HashSet<string> GetUniqueFactorValues(ModelFactorRelationPure factor, List<OMModelToMarketObjects> modelObjects)
		{
			using (_logger.TimeOperation("Получение уникальных значений фактора '{FactorName}'", factor.AttributeName))
			{
				var uniqueFactorValues = modelObjects
					.SelectMany(x => x.DeserializedCoefficients)
					.Where(x => x.AttributeId == factor.AttributeId && !string.IsNullOrWhiteSpace(x.Value))
					.Select(x => x.Value)
					.ToHashSet();

				_logger.Debug("Найдено {uniqueFactorValuesCount} уникальных значений фактора '{FactorName}'", uniqueFactorValues.Count, factor.AttributeName);

				return uniqueFactorValues;
			}
		}

		private Dictionary<string, UniqueFactorValueInfo> CalculateUniqueValuesAveragePrices(long attributeId,
			HashSet<string> uniqueFactorValues, List<OMModelToMarketObjects> modelObjects)
		{
			using (_logger.TimeOperation("Расчет средних цен по {UniqueFactorValuesCount} уникальным факторам", uniqueFactorValues.Count))
			{
				var uniqueValuesAveragePrices = new Dictionary<string, UniqueFactorValueInfo>();

				uniqueFactorValues.ForEach(uniqueValue =>
				{
					var modelObjectsWithCurrentUniqueValue = modelObjects.Where(obj => obj.DeserializedCoefficients.Exists(coef =>
						coef.AttributeId == attributeId && coef.Value == uniqueValue)).ToList();

					uniqueValuesAveragePrices[uniqueValue] = new UniqueFactorValueInfo
					{
						ModelObjects = modelObjectsWithCurrentUniqueValue,
						AveragePrice = modelObjectsWithCurrentUniqueValue.Average(x => x.Price)
					};
				});

				return uniqueValuesAveragePrices;
			}
		}

		private void CreateMarks(ModelFactorRelationPure factor, Dictionary<string, UniqueFactorValueInfo> uniqueFactorValuesInfo, decimal divider)
		{
			if (divider == 0)
				throw new Exception($"Средняя цена объектов с фактором '{factor.AttributeName}' равна нулю");

			using (_logger.TimeOperation("Полное создание меток для фактора '{FactorName}'", factor.AttributeName))
			{
				ModelDictionaryService.DeleteMarks(factor.DictionaryId);

				foreach (var uniqueFactorPair in uniqueFactorValuesInfo)
				{
					var uniqueFactorInfo = uniqueFactorPair.Value;

					using (_logger.TimeOperation("Обновление {ModelObjectsCount} объектов со значением фактора '{FactorValue}'", uniqueFactorInfo.ModelObjects.Count, uniqueFactorPair.Key))
					{
						uniqueFactorInfo.ModelObjects.ForEach(obj =>
						{
							var oldCoefficient = obj.DeserializedCoefficients.FirstOrDefault(x => x.AttributeId == factor.AttributeId);
							if (oldCoefficient == null)
								return;

							oldCoefficient.Coefficient = uniqueFactorInfo.AveragePrice / divider;
							obj.Coefficients = obj.DeserializedCoefficients.SerializeCoefficient();
							obj.Save();
						});
					}

					var allObjectsCoefficients = uniqueFactorInfo.ModelObjects.SelectMany(x => x.DeserializedCoefficients);
					ModelDictionaryService.CreateMarks(factor.AttributeId, factor.DictionaryId.Value, allObjectsCoefficients);
				}
			}
		}

		private decimal CalculateMedian(List<decimal> prices)
		{
			using (_logger.TimeOperation("Расчет медианного значения по {PricesCount} ценам", prices.Count))
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
		}

		#endregion

		private class UniqueFactorValueInfo
		{
			public List<OMModelToMarketObjects> ModelObjects { get; init; }
			public decimal AveragePrice { get; init; }
		}
	}
}
