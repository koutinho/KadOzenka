using System;
using System.Threading;
using Core.ErrorManagment;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.LongProcess;
using ObjectModel.Gbu.ExportAttribute;
using System.Threading.Tasks;

namespace KadOzenka.Dal.LongProcess
{
	public class ExportAttributeToKoProcess : ILongProcess
	{
		public const string LongProcessName = "ExportAttributeToKoProcess";

		public static void AddProcessToQueue(GbuExportAttributeSettings settings)
		{
			LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
            var cancelProgressCounterSource = new CancellationTokenSource();
            var cancelProgressCounterToken = cancelProgressCounterSource.Token;
            try
			{
                WorkerCommon.SetProgress(processQueue, 0);
                WorkerCommon.LogState(processQueue, "Начата обработка процесса переноса атрибутов из ГБУ в КО.");

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
                ExportAttributeToKO.Run(settings, processQueue);
                //TestLongRunningProcess(settings);

                cancelProgressCounterSource.Cancel();
                progressCounterTask.Wait(cancellationToken);
                cancelProgressCounterSource.Dispose();

                WorkerCommon.LogState(processQueue, "Отправка уведомления о завершении операции.");
                SendSuccessNotification(processQueue);
                WorkerCommon.SetProgress(processQueue, 100);
            }
			catch (Exception ex)
			{
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

        private static void LogProgress(OMQueue processQueue)
        {
            if (ExportAttributeToKO.MaxCount <= 0 || ExportAttributeToKO.CurrentCount <= 0)
                return;

            var newProgress = (long)Math.Round(((double)ExportAttributeToKO.CurrentCount / ExportAttributeToKO.MaxCount) * 100);
            if (newProgress != processQueue.Progress)
                WorkerCommon.SetProgress(processQueue, newProgress);
        }

        private void SendSuccessNotification(OMQueue processQueue)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto{UserIds = processQueue.UserId.HasValue ? new[] { processQueue.UserId.Value } : new long[] { } },
				Subject = $"Результат Операции переноса атрибутов из ГБУ в КО",
				Message = $"Операция переноса атрибутов из ГБУ в КО успешно завершена",
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
            });
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
