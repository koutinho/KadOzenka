using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks.Excel;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports.ResultComposition
{
	public abstract class BaseReportLongProcess<T> : LongProcessForReportsBase where T : class, new()
	{
		private int _defaultPackageSize = 125000;
		private int _defaultThreadsCount = 20;
		protected abstract string ReportName { get; }
		protected abstract string ProcessName { get; }
		private string MessageSubject => $"Отчет '{ReportName}'";
		private DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; }
		private readonly QueryManager _queryManager;
		private object _locker;

		protected BaseReportLongProcess(ILogger logger) : base(logger)
		{
			_locker = new object();
			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
			_queryManager = new QueryManager();
		}

		protected abstract long ReportCode { get; }
		protected abstract List<string> GenerateReportReportRow(int index, T item);
		protected abstract List<GbuReportService.Column> GenerateReportHeaders(List<T> info);


		public override void AddToQueue(object input)
		{
			var parameters = input as ReportLongProcessOnlyTasksInputParameters;
			if (parameters?.TaskIds == null || parameters.TaskIds.Count == 0)
				throw new Exception("Не переданы ИД задач");

			LongProcessManager.AddTaskToQueue(ProcessName, parameters: parameters.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			DataCompositionByCharacteristicsService.QueryManager.SetBaseToken(cancellationToken);
			_queryManager.SetBaseToken(cancellationToken);
			Logger.Debug("Начат фоновый процесс.");
			WorkerCommon.SetProgress(processQueue, 0);

			var parameters = processQueue?.Parameters?.DeserializeFromXml<ReportLongProcessOnlyTasksInputParameters>();
			if (parameters?.TaskIds == null || parameters.TaskIds.Count == 0)
			{
				NotificationSender.SendNotification(processQueue, MessageSubject, "Не переданы ИД задач для построения отчета");
				return;
			}
			Logger.ForContext("InputParameters", parameters.TaskIds, destructureObjects: true).Debug("Входные параметры");

			if (!DataCompositionByCharacteristicsService.IsCacheTableExists())
				throw new Exception("Не найдена таблица с данными для отчета");

			var message = string.Empty;
			try
			{
				var config = GetProcessConfigFromSettings("UniformAndNonUniform", _defaultPackageSize, _defaultThreadsCount);

				var unitsCount = OMUnit.Where(x => parameters.TaskIds.Contains((long) x.TaskId) &&
				                                   x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
				Logger.Debug("Всего в БД {UnitsCount} ЕО.", unitsCount);
				if (unitsCount == 0)
				{
					message = "У заданий на оценку нет единиц оценки";
					return;
				}
				WorkerCommon.SetProgress(processQueue, 1);

				var localCancelTokenSource = new CancellationTokenSource();
				var options = new ParallelOptions
				{
					CancellationToken = localCancelTokenSource.Token,
					MaxDegreeOfParallelism = config.ThreadsCount
				};

				var processedPackageCount = 0;
				var numberOfPackages = unitsCount / config.PackageSize + 1;
				var processedItemsCount = 0;
				using (Logger.TimeOperation("Полная обработка всех пакетов"))
				{
					Parallel.For(0, numberOfPackages, options, (i, s) =>
					{
						CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
						Logger.Debug("Начата работа с пакетом №{i}", i);

						using (Logger.TimeOperation("Полная обработка пакета №{i} (сбор данных + генерация файла)", i))
						{
							var objectIdsSubQuerySql = $@"select object_id from ko_unit 
								where task_id in ({string.Join(',', parameters.TaskIds)}) and PROPERTY_TYPE_CODE <> 2190 
								order by object_id 
								limit {config.PackageSize} offset {i * config.PackageSize} ";

							var sql = $@"select cadastral_number as CadastralNumber, attributes 
								from {DataCompositionByCharacteristicsService.TableName} 
								where object_id in ({objectIdsSubQuerySql})";

							CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
							List<T> currentPage;
							using (Logger.TimeOperation("Сбор данных для пакета №{i}", i))
							{
								Logger.Debug(new Exception(sql), "Начат сбор данных для пакета №{i}. До этого было обработано {ProcessedItemsCount} записей", i, processedItemsCount);

									currentPage = _queryManager.ExecuteSql<T>(sql);
									processedItemsCount += currentPage.Count;
							}

							CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
							using (Logger.TimeOperation("Формирование файла для пакета №{i}", i))
							{
								GenerateReport(currentPage);
							}
						}

						lock (_locker)
						{
							processedPackageCount++;
							LongProcessProgressLogger.LogProgress(numberOfPackages, processedPackageCount, processQueue);
						}
					});
				}

				message = "Операция успешно завершена.";
			}
			catch (OperationCanceledException)
			{
				message = "Формирование отчета было отменено пользователем";
				Logger.Error(message);
			}
			catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				Logger.Fatal(exception, "Ошибка построения отчета");

				message = $"Операция завершена с ошибкой: {exception.Message} (Подробнее в журнале: {errorId})";
			}
			finally
			{
				string urlToDownload;
				using (Logger.TimeOperation("Сохранение zip-файла"))
				{
					urlToDownload = CustomReportsService.SaveReport(ReportName, ReportCode, parameters);
				}

				SendMessageInternal(processQueue, message, urlToDownload);
			}

			WorkerCommon.SetProgress(processQueue, 100);
			Logger.Debug("Закончен фоновый процесс.");
		}


		#region Support Methods

		private void SendMessageInternal(OMQueue processQueue, string mainMessage, string urlToDownload)
		{
			var fullMessage = string.IsNullOrWhiteSpace(urlToDownload) 
				? mainMessage 
				: mainMessage + "<br>" + $@"<a href=""{urlToDownload}"">Скачать результат</a>";

			NotificationSender.SendNotification(processQueue, MessageSubject, fullMessage);
		}

		private void GenerateReport(List<T> reportItems)
		{
			var headerColumns = GenerateReportHeaders(reportItems);

			var encoding = Encoding.GetEncoding(1251);
			MemoryStream stream;
			using (stream = new MemoryStream())
			{
				WriteToStream(headerColumns.Select(x => x.Header).ToList(), encoding, stream);

				for (var i = 0; i < reportItems.Count; i++)
				{
					var values = GenerateReportReportRow(i, reportItems[i]);
					WriteToStream(values, encoding, stream);

					if (i % 100000 == 0)
						Logger.Debug("Обрабатывается строка №{i} из {reportItemsCount}.", i, reportItems.Count);
				}

				lock (_locker)
				{
					using (Logger.TimeOperation("Добавление zip-файла"))
					{
						CustomReportsService.AddZipFileToGeneralZipArchive(stream, ReportName, "csv");
					}
				}
			}
		}

		protected void WriteToStream(List<string> str, Encoding encoding, MemoryStream stream)
		{
			str.Add("\n");
			var headers = string.Join(',', str);
			byte[] firstString = encoding.GetBytes(headers);
			stream.Write(firstString, 0, firstString.Length);
		}

		#endregion


		#region Entities

		public class Attribute
		{
			public string Name { get; set; }
			public string RegisterName { get; set; }
			public long RegisterId { get; set; }
		}

		#endregion
	}
}
