using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports
{
	public abstract class BaseReportLongProcess<T> : LongProcess where T : class, new()
	{
		private int _packageSize = 125000;
		protected abstract string ReportName { get; }
		protected abstract string ProcessName { get; }
		private string MessageSubject => $"Отчет '{ReportName}'";
		protected ILogger Logger { get; }
		private DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; }
		private CustomReportsService CustomReportsService { get; }
		private readonly QueryManager _queryManager;
		private object _locker;

		protected BaseReportLongProcess(ILogger logger)
		{
			Logger = logger;
			_locker = new object();
			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
			CustomReportsService = new CustomReportsService();
			_queryManager = new QueryManager();
		}

		protected abstract List<string> GenerateReportReportRow(int index, T item);
		protected abstract List<GbuReportService.Column> GenerateReportHeaders(List<T> info);


		public override void AddToQueue(object input)
		{
			var parameters = input as ReportLongProcessInputParameters;
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

			ReportLongProcessInputParameters parameters = null;
			if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
			{
				parameters = processQueue.Parameters.DeserializeFromXml<ReportLongProcessInputParameters>();
				if (parameters.PackageSize.GetValueOrDefault() != 0)
				{
					_packageSize = parameters.PackageSize.GetValueOrDefault();
				}
			}
			if (parameters?.TaskIds == null || parameters.TaskIds.Count == 0)
			{
				SendMessage(processQueue, "Не переданы ИД задач для построения отчета", MessageSubject);
				return;
			}

			if (!DataCompositionByCharacteristicsService.IsCacheTableExists())
				throw new Exception("Не найдена таблица с данными для отчета");

			var message = string.Empty;
			try
			{
				var unitsCount = OMUnit.Where(x => parameters.TaskIds.Contains((long) x.TaskId) &&
				                                   x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
				Logger.Debug($"Всего в БД {unitsCount} ЕО.");

				var localCancelTokenSource = new CancellationTokenSource();
				var options = new ParallelOptions
				{
					CancellationToken = localCancelTokenSource.Token,
					MaxDegreeOfParallelism = 20
				};

				var numberOfPackages = unitsCount / _packageSize + 1;
				var processedItemsCount = 0;
				using (Logger.TimeOperation("Полная обработка всех пакетов"))
				{
					Parallel.For(0, numberOfPackages, options, (i, s) =>
					{
						if (processedItemsCount >= unitsCount)
						{
							localCancelTokenSource.Cancel();
							options.CancellationToken.ThrowIfCancellationRequested();
						}

						CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
						Logger.Debug($"Начата работа с пакетом №{i}");

						using (Logger.TimeOperation($"Полная обработка пакета №{i} (сбор данных + генерация файла)"))
						{
							var objectIdsSubQuerySql = $@"select object_id from ko_unit 
								where task_id in ({string.Join(',', parameters.TaskIds)}) and PROPERTY_TYPE_CODE <> 2190 
								order by object_id 
								limit {_packageSize} offset {i * _packageSize} ";

							var sql = $@"select cadastral_number as CadastralNumber, attributes 
								from {DataCompositionByCharacteristicsService.TableName} 
								where object_id in ({objectIdsSubQuerySql})";

							CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
							List<T> currentPage;
							using (Logger.TimeOperation($"Сбор данных для пакета №{i}"))
							{
								Logger.Debug(new Exception(sql), $"Начат сбор данных для пакета №{i}. До этого было обработано {processedItemsCount} записей");

									currentPage = _queryManager.ExecuteSql<T>(sql);
									processedItemsCount += currentPage.Count;
								}

							CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
							using (Logger.TimeOperation($"Формирование файла для пакета №{i}"))
							{
								GenerateReport(currentPage);
							}
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
					urlToDownload = CustomReportsService.SaveReport(ReportName);
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

			SendMessage(processQueue, fullMessage, MessageSubject);
		}

		private void CheckCancellationToken(CancellationToken processCancellationToken,
			CancellationTokenSource localCancellationToken, ParallelOptions options)
		{
			if (!processCancellationToken.IsCancellationRequested)
				return;

			localCancellationToken.Cancel();
			options.CancellationToken.ThrowIfCancellationRequested();
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
						Logger.Debug($"Обрабатывается строка №{i} из {reportItems.Count}.");
				}

				lock (_locker)
				{
					using (Logger.TimeOperation("Добавление zip-файла"))
					{
						CustomReportsService.AddFileToZip(stream, ReportName, "csv");
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
