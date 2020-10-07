using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.LongProcess;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject.Dto;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
	public class ExportAttributeToKoProcess : ILongProcess
	{
		public const string LongProcessName = "ExportAttributeToKoProcess";
        private static readonly ILogger _log = Log.ForContext<ExportAttributeToKoProcess>();
        private GbuReportService ReportService { get; }
        private string ReportName => "Отчет по переносу атрибутов";
        private int ColumnWidth => 8;

        public ExportAttributeToKoProcess()
        {
	        ReportService = new GbuReportService();
        }



        public static void AddProcessToQueue(GbuExportAttributeSettings settings)
		{
            _log.Information(new Exception(settings.SerializeToXml()), "Добавление в очередь фонового процесса {LongProcessName}", LongProcessName);
            
            LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
            _log.Information("Старт фонового процесса {LongProcessName} {cancellationToken} {QueueId} {processType}", LongProcessName, cancellationToken, processQueue.Id, processType.Parameters);
            var cancelProgressCounterSource = new CancellationTokenSource();
            var cancelProgressCounterToken = cancelProgressCounterSource.Token;
            try
			{
                WorkerCommon.SetProgress(processQueue, 0);
                WorkerCommon.LogState(processQueue, "Начата обработка процесса переноса атрибутов из ГБУ в КО.");
                _log.Information("Начата обработка процесса переноса атрибутов из ГБУ в КО.");

               var progressCounterTask = Task.Run(() => {
                    while (true)
                    {
                        if (cancelProgressCounterToken.IsCancellationRequested)
                        {
                            break;
                        }

                        LogProgress(processQueue);
                    }
                }, cancelProgressCounterToken);


                var settings = processQueue.Parameters.DeserializeFromXml<GbuExportAttributeSettings>();
                var exportResult = new ExportAttributeToKO().Run(settings, processQueue);
                //TestLongRunningProcess(settings);

                cancelProgressCounterSource.Cancel();
                progressCounterTask.Wait(cancellationToken);
                cancelProgressCounterSource.Dispose();

                WorkerCommon.LogState(processQueue, "Отправка уведомления о завершении операции.");
                _log.Information("Отправка уведомления о завершении операции.");

                var urlToDownloadReport = GenerateReport(settings, exportResult);
				SendSuccessNotification(processQueue, urlToDownloadReport);
                WorkerCommon.SetProgress(processQueue, 100);
            }
			catch (Exception ex)
			{
				var errorId = ErrorManager.LogError(ex);
				var message = $"{ex.Message} (Подробнее в журнале: {errorId})";
                _log.Fatal(ex, "Ошибка запуска фонового процесса {LongProcessName}", LongProcessName);
                SendFailureNotification(processQueue, message);
				throw;
			}
		}

		private string GenerateReport(GbuExportAttributeSettings settings, List<ExportAttributeToKO.OperationResult> exportResults)
		{
			_log.Information("Начато формирование отчета.");

			var cadastralNumberColumn = new GbuReportService.Column
			{
				Index = 0,
				Header = "КН",
				Width = 4
			};
			ReportService.SetIndividualWidth(cadastralNumberColumn.Index, cadastralNumberColumn.Width);

			var numberOfAttributes = settings.Attributes?.Count ?? 0;
			var copiedColumns = Enumerable.Range(1, numberOfAttributes).Select(x =>
			{
				ReportService.SetIndividualWidth(x, ColumnWidth);
				return new GbuReportService.Column
				{
					Header = $"Скопированное значение №{x}",
					Index = x,
					Width = ColumnWidth
				};
			});

			var headers = copiedColumns.Select(x => x.Header).ToList();
			headers.Insert(0, cadastralNumberColumn.Header);
			ReportService.AddHeaders(0, headers);

			var copiedColumnsStartIndex = cadastralNumberColumn.Index + 1;
			exportResults.ForEach(exportResult =>
			{
				var currentReportRowIndex = ReportService.GetCurrentRow();
				ReportService.AddValue(exportResult.CadastralNumber, cadastralNumberColumn.Index, currentReportRowIndex);

				exportResult.Atributes.ForEach(attribute =>
				{
					var color = SpreadsheetColor.FromName(ColorName.White);
					var columnValue = $"{attribute.GbuAttributeName} ({attribute.GbuRegisterName}) -> {attribute.KoAttributeName} = '{attribute.Value}'";

					if (!string.IsNullOrWhiteSpace(attribute.Warning))
					{
						color = SpreadsheetColor.FromName(ColorName.Yellow);
						columnValue = $"{columnValue}\n{attribute.Warning}";
					}
					if (!string.IsNullOrWhiteSpace(attribute.Error))
					{
						color = SpreadsheetColor.FromName(ColorName.Red);
						columnValue = $"{columnValue}\n{attribute.Error}";
					}

					var cellStyle = new CellStyle();
					cellStyle.FillPattern.SetPattern(FillPatternStyle.Solid, color, SpreadsheetColor.FromName(ColorName.Black));

					ReportService.AddValue(columnValue, attribute.Index + copiedColumnsStartIndex, currentReportRowIndex, cellStyle);
				});
			});

			ReportService.SetStyle();
			ReportService.SaveReport(ReportName);

			_log.Information("Закончено формирование отчета.");

			return ReportService.UrlToDownload;
		}


		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			throw new NotImplementedException();
		}

		public bool Test()
		{
			return true;
		}

        private static void LogProgress(OMQueue processQueue)
        {
            if (ExportAttributeToKO.MaxCount <= 0 || ExportAttributeToKO.CurrentCount <= 0)
                return;

            var newProgress = (long)Math.Round(((double)ExportAttributeToKO.CurrentCount / ExportAttributeToKO.MaxCount) * 100);
            if (newProgress != processQueue.Progress)
                WorkerCommon.SetProgress(processQueue, newProgress);
        }

        private void SendSuccessNotification(OMQueue processQueue, string urlToDownloadReport)
		{
			var message = "Операция переноса атрибутов из ГБУ в КО успешно завершена." +
			                 $@"<a href=""{urlToDownloadReport}"">Скачать результат</a>";

			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto{UserIds = processQueue.UserId.HasValue ? new[] { processQueue.UserId.Value } : new long[] { } },
				Subject = $"Результат Операции переноса атрибутов из ГБУ в КО",
				Message = message,
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
            });
            _log.Information("Операция переноса атрибутов из ГБУ в КО успешно завершена {QueueId}", processQueue.Id);
        }

		private void SendFailureNotification(OMQueue processQueue, string message)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto { UserIds = processQueue.UserId.HasValue ? new[] { processQueue.UserId.Value } : new long[] { } },
				Subject = $"Результат Операции переноса атрибутов из ГБУ в КО",
				Message = $"Операция переноса атрибутов из ГБУ в КО была прервана: {message}",
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
            });
            _log.Error(new Exception(message), "Операция переноса атрибутов из ГБУ в КО была прервана {QueueId}", processQueue.Id);
        }

        protected void TestLongRunningProcess(GbuExportAttributeSettings setting)
        {
            ExportAttributeToKO.MaxCount = 900;
            ExportAttributeToKO.CurrentCount = 0;
            for (int i = 0; i < 900; i++)
            {
                ExportAttributeToKO.CurrentCount++;
                Thread.Sleep(1000);
            }
        }
    }
}
