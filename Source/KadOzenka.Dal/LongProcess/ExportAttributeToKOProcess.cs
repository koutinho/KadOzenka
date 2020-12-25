using System;
using System.Threading;
using Core.ErrorManagment;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.LongProcess;
using System.Threading.Tasks;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Logger;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
	public class ExportAttributeToKoProcess : ILongProcess
	{
		public const string LongProcessName = "ExportAttributeToKoProcess";
        private static readonly ILogger _log = Log.ForContext<ExportAttributeToKoProcess>();
        private LongProcessProgressLogger LongProcessProgressLogger { get; }

        public ExportAttributeToKoProcess()
        {
	        LongProcessProgressLogger = new LongProcessProgressLogger();
        }

        public static void AddProcessToQueue(GbuExportAttributeSettings settings)
		{
            _log.Information(new Exception(settings.SerializeToXml()), "Добавление в очередь фонового процесса {LongProcessName}", LongProcessName);
            
            LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
            _log.Information("Старт фонового процесса {LongProcessName} {cancellationToken} {QueueId} {processType}", LongProcessName, cancellationToken, processQueue.Id, processType.Parameters);
            try
			{
                WorkerCommon.SetProgress(processQueue, 0);
                WorkerCommon.LogState(processQueue, "Начата обработка процесса переноса атрибутов из ГБУ в КО.");
                _log.Information("Начата обработка процесса переноса атрибутов из ГБУ в КО.");

               LongProcessProgressLogger.StartLogProgress(processQueue, () => ExportAttributeToKO.MaxCount, () => ExportAttributeToKO.CurrentCount);

                var settings = processQueue.Parameters.DeserializeFromXml<GbuExportAttributeSettings>();

                var urlToDownload = new ExportAttributeToKO().Run(settings, processQueue);
                //TestLongRunningProcess(settings);

                LongProcessProgressLogger.StopLogProgress();

                WorkerCommon.LogState(processQueue, "Отправка уведомления о завершении операции.");
				SendSuccessNotification(processQueue, urlToDownload);
                WorkerCommon.SetProgress(processQueue, 100);
            }
			catch (Exception ex)
			{
				_log.Fatal(ex, "Ошибка запуска фонового процесса {LongProcessName}", LongProcessName);
				LongProcessProgressLogger.StopLogProgress();
                var errorId = ErrorManager.LogError(ex);
				var message = $"{ex.Message} (Подробнее в журнале: {errorId})";
				SendFailureNotification(processQueue, message);
				throw;
			}
		}


		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			throw new NotImplementedException();
		}

		public bool Test()
		{
			return true;
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
