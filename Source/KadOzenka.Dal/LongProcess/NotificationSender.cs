using System;
using Core.Messages;
using Core.Shared.Extensions;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess
{
	public class NotificationSender
	{
		public static void SendNotification(OMQueue processQueue, string subject, string message, long? roleId = null)
        {
            var addressers = roleId == null
                ? new MessageAddressersDto
                {
                    UserIds = processQueue.UserId.HasValue ? new[] {processQueue.UserId.Value} : new long[] { }
                }
                : new MessageAddressersDto
                {
                    RoleIds = new[] { roleId.Value }
                };

            new MessageService().SendMessages(new MessageDto
			{
				Addressers = addressers,
				Subject = subject,
				Message = message,
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
            });
		}

        public static void SendNotification(OMQueue processQueue, string subject, string message, long roleId)
        {
            new MessageService().SendMessages(new MessageDto
            {
                Addressers = new MessageAddressersDto { UserIds = processQueue.UserId.HasValue ? new long[] { processQueue.UserId.Value } : new long[] { } },
                Subject = subject,
                Message = message,
                IsUrgent = true,
                IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
            });
        }

        public void SendExportResultNotificationWithAttachment(OMExportByTemplates export, string subject)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto{UserIds = new long[] { export.UserId } },
				Subject = subject,
				Message = $@"Статус операции: {((ObjectModel.Directory.Common.ImportStatus)export.Status).GetEnumDescription()}
					<a href=""/DataExport/DownloadExportResult?exportId={export.Id}"">Скачать результат</a>",
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
            });
		}
	}
}
