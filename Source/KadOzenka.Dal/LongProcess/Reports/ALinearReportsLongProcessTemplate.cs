using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.KO;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports
{
	/// <summary>
	/// Базовый класс для линейных отчетов.
	/// (линейные отчеты - это отчеты без группировки с большим объемом данных)
	/// </summary>
	public abstract class ALinearReportsLongProcessTemplate<TReportItem, TInputParameters> : LongProcessForReportsBase 
		where TReportItem : class, new()
		where TInputParameters : class
	{
		private readonly object _locker;
		protected readonly QueryManager QueryManager;
		private string MessageSubject => $"Отчет '{ReportName}'";

		protected int ColumnWidthForDates = 3;
		protected int ColumnWidthForDecimals = 3;
		protected int ColumnWidthForCadastralNumber = 6;
		protected int ColumnWidthForAddresses = 6;
		protected StatisticalDataService StatisticalDataService { get; set; }

		protected ALinearReportsLongProcessTemplate(ILogger logger) : base(logger)
		{
			_locker = new object();
			QueryManager = new QueryManager();
			StatisticalDataService = new StatisticalDataService();
		}


		protected abstract bool AreInputParametersValid(TInputParameters inputParameters);
		protected abstract string ReportName { get; }
		protected abstract string ProcessName { get; }
		protected abstract ReportsConfig GetProcessConfig();
		//TODO remove queryManager as input parameter
		protected abstract int GetMaxItemsCount(TInputParameters inputParameters);
		protected abstract Func<TReportItem, string> GetSortingCondition();
		protected abstract List<Column> GenerateReportHeaders();
		protected abstract List<object> GenerateReportReportRow(int index, TReportItem item);
		protected abstract string GetSql(int packageIndex, int packageSize);

		protected virtual void PrepareVariables(TInputParameters inputParameters)
		{

		}

		protected virtual Func<TReportItem, object> GetSecondSortingCondition()
		{
			return null;
		}

		protected virtual List<TReportItem> GetReportItems(string sql)
		{
			return QueryManager.ExecuteSql<TReportItem>(sql);
		}

		protected virtual string GenerateReportTitle()
		{
			return string.Empty;
		}

		protected virtual List<MergedColumns> GenerateReportMergedHeaders()
		{
			return new List<MergedColumns>();
		}

		protected virtual string GetMessageForReportsWithoutUnits(TInputParameters inputParameters)
		{
			return "У заданий на оценку нет единиц оценки";
		}


		public override void AddToQueue(object input)
		{
			var parameters = input as TInputParameters;
			if (!AreInputParametersValid(parameters))
				throw new Exception("Не переданы параметры для построения отчета");

			LongProcessManager.AddTaskToQueue(ProcessName, parameters: input.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			QueryManager.SetBaseToken(cancellationToken);
			Logger.ForContext("InputParameters", processQueue.Parameters).Debug("Начат фоновый процесс {ProcessDescription}. Входные параметры", processType.Description);
			WorkerCommon.SetProgress(processQueue, 0);

			var parameters = processQueue.Parameters?.DeserializeFromXml<TInputParameters>();
			if (!AreInputParametersValid(parameters))
			{
				NotificationSender.SendNotification(processQueue, MessageSubject,
					$"Неверные параметры для построения отчета '{processQueue.Parameters}'.");
				return;
			}
			PrepareVariables(parameters);

			var message = string.Empty;
			try
			{
				var config = GetProcessConfig();
				using (Logger.TimeOperation("Общее время обработки всех пакетов"))
				{
					var maxItemsCount = GetMaxItemsCount(parameters);
					Logger.Debug("Всего в БД {UnitsCount} ЕО.", maxItemsCount);
					if (maxItemsCount == 0)
					{
						message = GetMessageForReportsWithoutUnits(parameters);
						return;
					}

					var localCancelTokenSource = new CancellationTokenSource();
					var options = new ParallelOptions
					{
						CancellationToken = localCancelTokenSource.Token,
						MaxDegreeOfParallelism = config.ThreadsCount
					};
					var numberOfPackages = maxItemsCount / config.PackageSize + 1;
					var processedPackageCount = 0;
					var processedItemsCount = 0;
					Parallel.For(0, numberOfPackages, options, (i, s) =>
					{
						var sql = GetSql(i, config.PackageSize);
						Logger.Debug(new Exception(sql), "Начата работа с пакетом №{PackageNumber} из {MaxPackagesCount}", i, numberOfPackages);

						CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
						List<TReportItem> currentOperations;
						using (Logger.TimeOperation("Сбор данных для пакета №{i}", i))
						{
							//todo убрать условие
							var orderedItems = GetReportItems(sql).OrderBy(GetSortingCondition());
							
							currentOperations = GetSecondSortingCondition() != null 
								? orderedItems.ThenBy(GetSecondSortingCondition()).ToList() 
								: GetReportItems(sql).OrderBy(GetSortingCondition()).ToList();

							processedItemsCount += currentOperations.Count;
							Logger.Debug("Выкачено {ProcessedItemsCount} записей из {MaxItemsCount}", processedItemsCount, maxItemsCount);
						}

						CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
						using (Logger.TimeOperation("Формирование файла для пакета №{i}", i))
						{
							GenerateReport(currentOperations);
						}

						lock (_locker)
						{
							processedPackageCount++;
							LongProcessProgressLogger.LogProgress(numberOfPackages, processedPackageCount, processQueue);
						}

						Logger.Debug("Закончена работа с пакетом №{PackageNumber} из {MaxPackagesCount}", i, numberOfPackages);
					});
				}
			}
			catch (OperationCanceledException)
			{
				message = "Формирование отчета было отменено пользователем";
				SaveQueueFaultedStatus(processQueue);
				Logger.Error(message);
			}
			catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				Logger.Fatal(exception, "Ошибка построения отчета");
				SaveQueueFaultedStatus(processQueue);

				message = $"Операция завершена с ошибкой: {exception.Message} (Подробнее в журнале: {errorId})";
			}
			finally
			{
				string urlToDownload;
				using (Logger.TimeOperation("Сохранение zip-файла"))
				{
					urlToDownload = CustomReportsService.SaveReport(ReportName);
				}

				SendMessageInternal(processQueue, message, urlToDownload);
			}

			WorkerCommon.SetProgress(processQueue, 100);
			Logger.Debug("Закончен фоновый процесс.");
		}


		#region Support Methods

		//пока неизвестно - все ли линейные отчеты связаны с юнитами
		protected int GetMaxUnitsCount(string baseUnitsCondition)
		{
			var columnName = "count";
			var countSql = $@"select count(*) as {columnName} from ko_unit unit {baseUnitsCondition}";
			var dataSet = QueryManager.ExecuteSqlStringToDataSet(countSql);

			var unitCount = 0;
			var row = dataSet.Tables[0]?.Rows[0];
			if (row != null)
			{
				unitCount = row[columnName].ParseToInt();
			}

			return unitCount;
		}

		protected long GetTourFromTasks(List<long> taskIds)
		{
			var tourId = OMTask.Where(x => x.Id == taskIds[0]).Select(x => x.TourId).ExecuteFirstOrDefault().TourId.GetValueOrDefault();
			Logger.Debug("ИД тура '{TourId}'", tourId);

			return tourId;
		}

		private void SendMessageInternal(OMQueue processQueue, string mainMessage, string urlToDownload)
		{
			var fullMessage = string.IsNullOrWhiteSpace(urlToDownload)
				? mainMessage
				: mainMessage + "<br>" + $@"<a href=""{urlToDownload}"">Скачать результат</a>";

			NotificationSender.SendNotification(processQueue, MessageSubject, fullMessage);
		}

		private void GenerateReport(List<TReportItem> reportItems)
		{
			var stream = GetReportStream(reportItems);

			lock (_locker)
			{
				using (Logger.TimeOperation("Добавление zip-файла"))
				{
					CustomReportsService.AddExcelFileToGeneralZipArchive(stream, ReportName);
				}
			}
		}

		protected virtual MemoryStream GetReportStream(List<TReportItem> reportItems)
		{
			var excelFileGenerator = new GemBoxExcelFileGenerator();

			var reportTitle = GenerateReportTitle();
			var mergedColumnsHeaders = GenerateReportMergedHeaders();
			var generalColumnsHeaders = GenerateReportHeaders();

			excelFileGenerator.AddTitle(reportTitle, generalColumnsHeaders.Count);
			excelFileGenerator.AddMergedHeaders(mergedColumnsHeaders);
			excelFileGenerator.AddSeparateColumnsHeaders(generalColumnsHeaders);

			for (var i = 0; i < reportItems.Count; i++)
			{
				var values = GenerateReportReportRow(i, reportItems[i]);
				excelFileGenerator.AddRow(values);

				if (i % 100000 == 0)
					Logger.Debug("Обрабатывается строка №{i} из {ReportItemsCount}.", i, reportItems.Count);
			}

			excelFileGenerator.SetIndividualWidth(generalColumnsHeaders);
			excelFileGenerator.SetStyle();

			//попытка принудительно освободить память
			reportItems = null;
			GC.Collect();

			return excelFileGenerator.GetStream();
		}

		private void SaveQueueFaultedStatus(OMQueue processQueue)
		{
			processQueue.Status_Code = Status.Faulted;
			processQueue.Save();
		}

		#endregion
	}
}
