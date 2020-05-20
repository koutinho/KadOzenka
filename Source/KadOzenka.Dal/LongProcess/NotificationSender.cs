using Core.Messages;
using Core.Shared.Extensions;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess
{
	public class NotificationSender
	{
		public static void SendNotification(OMQueue processQueue, string subject, string message)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto{UserIds = processQueue.UserId.HasValue ? new long[] { processQueue.UserId.Value } : new long[] { } },
				Subject = subject,
				Message = message,
				IsUrgent = true,
				IsEmail = true
			});
		}

		public void SendExportResultNotificationWithAttachment(OMExportByTemplates export, string subject, bool withXmlExtension = false)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto{UserIds = new long[] { export.UserId } },
				Subject = subject,
				Message = $@"Статус операции: {((ObjectModel.Directory.Common.ImportStatus)export.Status).GetEnumDescription()}
					<a href=""/Sud/DownloadExportResult?exportId={export.Id}&withXmlExtension={withXmlExtension}"">Скачать результат</a>",
				IsUrgent = true,
				IsEmail = true
			});
		}
	}
}
