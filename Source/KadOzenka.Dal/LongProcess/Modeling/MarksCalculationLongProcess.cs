﻿using System;
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
	    private string _messageSubject = "Результат Операции Расчета меток";
	    private int _maxFactorsCount;
	    private int _processedFactorsCount;
	    private int _descriptionColumnIndex = 0;
	    private int _factorsColumnIndex = 1;
	    private int _errorColumnIndex = 2;
		private readonly ILogger _logger = Log.ForContext<MarksCalculationLongProcess>();
	    private IModelService ModelService { get; }
		private IModelFactorsService ModelFactorsService { get; }
		private IModelObjectsRepository ModelObjectsRepository { get; }
		private IModelDictionaryService ModelDictionaryService { get; }
		private IRegisterCacheWrapper RegisterCacheWrapper { get; }
		private GbuReportService GbuReportService { get; set; }



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

		//для фоновых процессов
		public MarksCalculationLongProcess()
		{
			ModelService = new ModelService();
			ModelFactorsService = new ModelFactorsService();
			ModelObjectsRepository = new ModelObjectsRepository();
			ModelDictionaryService = new ModelDictionaryService();
			RegisterCacheWrapper = new RegisterCacheWrapper();
		}



		public static void AddProcessToQueue(long modelId)
		{
			ValidateModelId(modelId);

			CheckActiveProcessInQueue(modelId);

			LongProcessManager.AddTaskToQueue(nameof(MarksCalculationLongProcess), objectId: modelId, registerId: OMModel.GetRegisterId());
        }

		public static void CheckActiveProcessInQueue(long modelId)
		{
			var processId = 100;
			var isProcessExists = new LongProcessService().HasActiveProcessInQueue(processId, modelId);
			if (isProcessExists)
				throw new Exception("В очереди есть процесс расчета меток для этой модели");
		}


		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_logger.Debug("Старт процесса расчета меток");

			var modelId = processQueue.ObjectId.GetValueOrDefault();
			ValidateModelId(modelId);

			try
			{
				LongProcessProgressLogger.StartLogProgress(processQueue, () => _maxFactorsCount,
					() => _processedFactorsCount);

				var urlToDownloadReport = CalculateMarks(modelId, cancellationToken);

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
				_logger.Error(ex, "Ошибка в ходе расчета меток");
				var errorId = ErrorManager.LogError(ex); 
				NotificationSender.SendNotification(processQueue, _messageSubject, $"Операция завершена с ошибкой: {ex.Message} (Подробнее в журнале: {errorId})");
			}

			LongProcessProgressLogger.StopLogProgress();

			_logger.Debug("Финиш процесса расчета меток");
			WorkerCommon.SetProgress(processQueue, 100);
		}

		public string CalculateMarks(long modelId, CancellationToken cancellationToken)
        {
	        var model = ModelService.GetModelEntityById(modelId);
	        if (!model.IsAutomatic)
		        throw new CanNotCreateMarksForNonAutomaticModelException();

	        var factors = GetModelFactors(modelId);

			var modelObjects = GetModelObjects(modelId, factors, cancellationToken);

	        var urlToDownloadReport = ProcessInValidModelObjects(modelObjects, factors, cancellationToken);

	        factors.ForEach(factor =>
	        {
		        cancellationToken.ThrowIfCancellationRequested();

		        ProcessCodedFactor(factor, modelObjects, cancellationToken);

		        _processedFactorsCount++;
	        });
	        _logger.Debug("Расчет всех факторов закончен. Начато обновление коэффициентов в объектах моделирования");

	        using (_logger.TimeOperation("Обновление коэффициентов в объектах моделирования"))
	        {
		        modelObjects.Where(x => x.IsCoefficientsChanged).ForEach(x => x.Save());
	        }

	        return urlToDownloadReport;
        }

		
		#region Support Methods

		private static void ValidateModelId(long? modelId)
		{
			if (modelId.GetValueOrDefault() == 0)
				throw new Exception("Не передан ИД модели");
		}

		private List<OMModelToMarketObjects> GetModelObjects(long modelId,
			List<FactorInfo> factors, CancellationToken cancellationToken)
		{
			using (_logger.TimeOperation("Получение объектов моделирования для модели с ИД '{ModelId}'", modelId))
			{
				var modelObjects = ModelObjectsRepository.GetIncludedModelObjects(modelId, IncludedObjectsMode.Training,
					cancellationToken,
					select => new { CadastralNumber = @select.MarketObjectInfo, select.Coefficients, select.Price });
				if (modelObjects.IsEmpty())
					throw new CanNotCreateMarksBecauseNoMarketObjectsException();

				_logger.Debug("Всего найдено {ModelObjectsCount} объектов модели для модели с ИД '{ModelId}'", modelObjects.Count, modelId);

				var factorIds = factors.Select(x => x.AttributeId).ToList();
				var modelObjectsSelectedFactors = modelObjects.Where(x => x.DeserializedCoefficients.Any(c => factorIds.Contains(c.AttributeId))).ToList();
				if (modelObjectsSelectedFactors.Count == 0)
					throw new CanNotCreateMarksBecauseNoMarketObjectsWithSelectedFactorsException();

				return modelObjectsSelectedFactors;
			}
		}

		private List<FactorInfo> GetModelFactors(long modelId)
		{
			var factors = ModelFactorsService.GetGeneralModelFactors(modelId)
				.Where(x => x.MarkType == MarkType.Default && x.IsActive).ToList();
			if (factors.IsEmpty())
				throw new CanNotCreateMarksBecauseNoFactorsException();

			var factorDtos = new List<FactorInfo>();
			factors.ForEach(x =>
			{
				if (x.DictionaryId == null)
					throw new CanNotCreateMarksBecauseNoDictionaryException(x.AttributeName);
				
				factorDtos.Add(new FactorInfo
				{
					AttributeId = x.AttributeId,
					AttributeName = x.AttributeName,
					Dictionary = ModelDictionaryService.GetDictionaryById(x.DictionaryId.Value)
				});
			});

			_maxFactorsCount = factorDtos.Count;
			_logger.Debug("Найдено {FactorsCount} активных факторов с меткой по умолчанию для модели с ИД '{ModelId}'", factors.Count, modelId);

			return factorDtos;
		}

		private string ProcessInValidModelObjects(List<OMModelToMarketObjects> modelObjects,
			List<FactorInfo> factors, CancellationToken cancellationToken)
		{
			ProcessModelObjectsWithEmptyValues(modelObjects, factors, cancellationToken);

			ProcessModelObjectsWithInvalidMarks(modelObjects, factors, cancellationToken);

			if (GbuReportService == null)
				return string.Empty;
			
			var reportId = GbuReportService.SaveReport();
			
			return GbuReportService.GetUrlToDownloadFile(reportId);
		}

		private void ProcessModelObjectsWithEmptyValues(List<OMModelToMarketObjects> modelObjects,
			List<FactorInfo> factors, CancellationToken cancellationToken)
		{
			using (_logger.TimeOperation("Обработка объектов моделирования, у которых есть пустые факторы"))
			{
				var factorIds = factors.Select(x => x.AttributeId).ToList();

				var modelObjectsWithEmptyFactors = modelObjects.Where(x =>
					x.DeserializedCoefficients.Any(c =>
						string.IsNullOrWhiteSpace(c.Value) && factorIds.Contains(c.AttributeId))).ToList();

				_logger.Debug("Найдено {ModelObjectsWithEmptyFactorsCount} объектов модели с пустыми факторами'", modelObjectsWithEmptyFactors.Count);

				if(modelObjectsWithEmptyFactors.Count == 0)
					return;

				InitReport();
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

					var row = GbuReportService.GetCurrentRow();
					GbuReportService.AddValue(obj.MarketObjectInfo, _descriptionColumnIndex, row);
					GbuReportService.AddValue(factorNames, _factorsColumnIndex, row);

					ExcludeInvalidModelObject(modelObjects, obj);
				});
			}
		}

		private void ProcessModelObjectsWithInvalidMarks(List<OMModelToMarketObjects> modelObjects,
			List<FactorInfo> factors, CancellationToken cancellationToken)
		{
			using (_logger.TimeOperation("Обработка объектов моделирования, у которых есть невалидные значения меток"))
			{
				for (var i = 0; i < modelObjects.Count; i++)
				{
					cancellationToken.ThrowIfCancellationRequested();

					var errors = string.Empty;
					var iLocal = i;
					factors.ForEach(factor =>
					{
						var factorCoefficient = modelObjects[iLocal].DeserializedCoefficients.FirstOrDefault(x => x.AttributeId == factor.AttributeId);
						if (factorCoefficient == null)
							return;

						try
						{
							ModelDictionaryService.ValidateMark(factor.Dictionary, factorCoefficient.Value, 0);
						}
						catch (Exception e)
						{
							_logger.Error(e, "Невалидная метка");
							errors += $"{e.Message}{Environment.NewLine}";
						}
					});

					if (!string.IsNullOrWhiteSpace(errors))
					{
						InitReport();
						var row = GbuReportService.GetCurrentRow();
						GbuReportService.AddValue(modelObjects[i].MarketObjectInfo, _descriptionColumnIndex, row);
						GbuReportService.AddValue(errors, _factorsColumnIndex, row);

						ExcludeInvalidModelObject(modelObjects, modelObjects[i]);
					}
				}
			}
		}

		private void InitReport()
		{
			if(GbuReportService != null)
				return;

			GbuReportService = new GbuReportService("Объекты, не участвующие в формировании меток");

			var headers = new List<GbuReportService.Column>
			{
				new()
				{
					Header = "Описание объекта аналога",
					Index = _descriptionColumnIndex,
					Width = 8
				},
				new()
				{
					Header = "Незаполненные факторы",
					Index = _factorsColumnIndex,
					Width = 12
				},
				new()
				{
					Header = "Ошибки",
					Index = _errorColumnIndex,
					Width = 12
				}
			};

			GbuReportService.AddHeaders(headers);
			GbuReportService.SetIndividualWidth(headers);
		}

		private void ExcludeInvalidModelObject(List<OMModelToMarketObjects> modelObjects, OMModelToMarketObjects invalidObject)
		{
			modelObjects.Remove(invalidObject);
			
			invalidObject.IsExcluded = true;
			invalidObject.Save();
		}

		private void ProcessCodedFactor(FactorInfo factor, List<OMModelToMarketObjects> modelObjects,
			CancellationToken cancellationToken)
		{
			using (_logger.TimeOperation("Полная обработка фактора '{FactorName}'", factor.AttributeName))
			{
				var uniqueFactorValues = GetUniqueFactorValues(factor, modelObjects);
				if (uniqueFactorValues.Count == 0)
					return;

				var uniqueFactorValuesInfo = CalculateUniqueValuesAveragePrices(factor.AttributeId, uniqueFactorValues,
					modelObjects, cancellationToken);
				if (uniqueFactorValuesInfo.Count == 0)
					return;

				var allValuesAveragePrices = uniqueFactorValuesInfo.Values.Select(x => x.AveragePrice);
				var divider = uniqueFactorValues.Count % 2 == 0 ? allValuesAveragePrices.Average() : CalculateMedian(allValuesAveragePrices.ToList());
				_logger.Debug("Делитель для фактора '{FactorName}' = {Divider}", factor.AttributeName, divider);

				CreateMarks(factor, uniqueFactorValuesInfo, divider, cancellationToken);
			}
		}

		private HashSet<string> GetUniqueFactorValues(FactorInfo factor, List<OMModelToMarketObjects> modelObjects)
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
			HashSet<string> uniqueFactorValues, List<OMModelToMarketObjects> modelObjects,
			CancellationToken cancellationToken)
		{
			using (_logger.TimeOperation("Расчет средних цен по {UniqueFactorValuesCount} уникальным факторам", uniqueFactorValues.Count))
			{
				var uniqueValuesAveragePrices = new Dictionary<string, UniqueFactorValueInfo>();

				uniqueFactorValues.ForEach(uniqueValue =>
				{
					cancellationToken.ThrowIfCancellationRequested();

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

		private void CreateMarks(FactorInfo factor,
			Dictionary<string, UniqueFactorValueInfo> uniqueFactorValuesInfo, decimal divider,
			CancellationToken cancellationToken)
		{
			if (divider == 0)
				throw new Exception($"Средняя цена объектов с фактором '{factor.AttributeName}' равна нулю");

			using (_logger.TimeOperation("Cоздание меток для фактора '{FactorName}'", factor.AttributeName))
			{
				ModelDictionaryService.DeleteMarks(factor.Dictionary.Id);

				foreach (var uniqueFactorPair in uniqueFactorValuesInfo)
				{
					var uniqueFactorInfo = uniqueFactorPair.Value;

					using (_logger.TimeOperation("Обновление {ModelObjectsCount} объектов со значением фактора '{FactorValue}'", uniqueFactorInfo.ModelObjects.Count, uniqueFactorPair.Key))
					{
						uniqueFactorInfo.ModelObjects.ForEach(obj =>
						{
							cancellationToken.ThrowIfCancellationRequested();

							var oldCoefficient = obj.DeserializedCoefficients.FirstOrDefault(x => x.AttributeId == factor.AttributeId);
							if (oldCoefficient == null)
								return;

							oldCoefficient.Coefficient = uniqueFactorInfo.AveragePrice / divider;
							obj.Coefficients = obj.DeserializedCoefficients.SerializeCoefficient();
							obj.IsCoefficientsChanged = true;
							////сделано через одноразовое обновление
							//obj.Save();
						});
					}

					var allObjectsCoefficients = uniqueFactorInfo.ModelObjects.SelectMany(x => x.DeserializedCoefficients);
					ModelDictionaryService.CreateMarks(factor.AttributeId, factor.Dictionary.Id, allObjectsCoefficients);
				}
			}
		}

		private decimal CalculateMedian(List<decimal> prices)
		{
			using (_logger.TimeOperation("Расчет медианного значения по {PricesCount} ценам", prices.Count))
			{
				return MathExtended.CalculateMedian(prices);
			}
		}

		#endregion

		private class UniqueFactorValueInfo
		{
			public List<OMModelToMarketObjects> ModelObjects { get; init; }
			public decimal AveragePrice { get; init; }
		}

		private class FactorInfo
		{
			public long AttributeId { get; set; }
			public string AttributeName { get; set; }
			public OMModelingDictionary Dictionary { get; init; }
		}
    }
}
