using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using Ionic.Zip;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using ObjectModel.KO;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	public abstract class BaseReportLongProcess<T> : LongProcess where T : new()
	{
		private int _packageSize = 125000;
		protected abstract string ReportName { get; }
		protected abstract string ProcessName { get; }
		private string MessageSubject => $"Отчет '{ReportName}'";
		protected ILogger Logger { get; }
		private DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; }
		private CustomReportsService CustomReportsService { get; }

		protected BaseReportLongProcess(ILogger logger)
		{
			Logger = logger;
			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
			CustomReportsService = new CustomReportsService();
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
			DataCompositionByCharacteristicsService.CancellationManager.SetBaseToken(cancellationToken);
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

			var urls = new List<string>();
			try
			{
				var unitsCount = OMUnit.Where(x => parameters.TaskIds.Contains((long)x.TaskId) && x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
				Logger.Debug($"Всего в БД {unitsCount} ЕО.");

				var cancelTokenSource = new CancellationTokenSource();
				var options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 20
				};

				var numberOfPackages = unitsCount / _packageSize + 1;
				var processedItemsCount = 0;
				using (Logger.TimeOperation("Полная обработка всех пакетов"))
				{
					try
					{
						Parallel.For(0, numberOfPackages, options, (i, s) =>
						{
							if (processedItemsCount >= unitsCount)
							{
								cancelTokenSource.Cancel();
								options.CancellationToken.ThrowIfCancellationRequested();
							}

							CheckCancellationToken(cancellationToken);
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

								CheckCancellationToken(cancellationToken);
								List<T> currentPage;
								using (Logger.TimeOperation($"Сбор данных для пакета №{i}"))
								{
									Logger.Debug(new Exception(sql), $"Начат сбор данных для пакета №{i}. До этого было обработано {processedItemsCount} записей");

									currentPage = QSQuery.ExecuteSql<T>(sql);
									processedItemsCount += currentPage.Count;
								}

								CheckCancellationToken(cancellationToken);
								using (Logger.TimeOperation($"Формирование файла для пакета №{i}"))
								{
									Logger.Debug(new Exception(sql), $"Начата запись в файл для пакета №{i}.");

									var urlToDownloadReport = GenerateReport(currentPage);
									urls.Add(urlToDownloadReport);

									Logger.ForContext("UrlToDownloadFile", urlToDownloadReport)
										.Debug(new Exception(sql), $"Закончена запись в файл для пакета №{i}. Сформирована ссылка для скачивания");
								}
							}
						});
					}
					catch (OperationCanceledException)
					{
						Logger.Error("Закончена обработка, вызвана отмена токена");
					}
				}

				SendMessageInternal(processQueue, "Операция успешно завершена.", urls);

				Logger.Debug("Закончен фоновый процесс.");
			}
			catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				Logger.Fatal(exception, "Ошибка построения отчета");
				var message = $"Операция завершена с ошибкой: {exception.Message} (Подробнее в журнале: {errorId})";
				SendMessageInternal(processQueue, message, urls);
			}

			WorkerCommon.SetProgress(processQueue, 100);
		}


		#region Support Methods

		private void SendMessageInternal(OMQueue processQueue, string mainMessage, List<string> urls)
		{
			var linksToUrls = new StringBuilder();
			linksToUrls.AppendLine("Созданные файлы:");
			urls.ForEach(url => { linksToUrls.AppendLine($@"<a href=""{url}"">Скачать результат</a>"); });

			SendMessage(processQueue, mainMessage + "<br>" + linksToUrls, MessageSubject);
		}

		private void CheckCancellationToken(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
				return;

			var message = "Формирование отчета было отменено пользователем";
			Logger.Error(message);
			throw new Exception(message);
		}

		private string GenerateReport(List<T> reportItems)
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

				using (Logger.TimeOperation("Сохранение zip-файла"))
				{
					return CustomReportsService.SaveReportZip(stream, ReportName, "csv");
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
