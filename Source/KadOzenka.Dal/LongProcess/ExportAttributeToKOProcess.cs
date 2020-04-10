using System;
using System.Threading;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.LongProcess;
using ObjectModel.Gbu.ExportAttribute;

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
			try
			{
                WorkerCommon.SetProgress(processQueue, 0);

                var settings = processQueue.Parameters.DeserializeFromXml<GbuExportAttributeSettings>();
				ExportAttributeToKO.Run(settings);
				SendSuccessNotification(processQueue);

                WorkerCommon.SetProgress(processQueue, 100);
            }
			catch (Exception ex)
			{
				SendFailureNotification(processQueue, ex.Message);
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

		private void SendSuccessNotification(OMQueue processQueue)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto{UserIds = processQueue.UserId.HasValue ? new[] { processQueue.UserId.Value } : new long[] { } },
				Subject = $"Результат Операции переноса атрибутов из ГБУ в КО",
				Message = $"Операция переноса атрибутов из ГБУ в КО успешно завершена",
				IsUrgent = true,
				IsEmail = true
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
				IsEmail = true
			});
		}
	}
}
