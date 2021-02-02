using System;
using Core.Messages;
using Core.Shared.Extensions;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess
{
	public interface INotificationSender
	{
		void SendNotification(OMQueue processQueue, string subject, string message);
		void SendNotification(OMQueue processQueue, string subject, string message, long? roleId);
		void SendNotification(string subject, string message, MessageAddressersDto messageAddresses);
		void SendExportResultNotificationWithAttachment(OMExportByTemplates export, string subject);
	}


	public class NotificationSender : INotificationSender
	{
		protected const int MaxMessageSize = 4000;

		public void SendNotification(OMQueue processQueue, string subject, string message)
		{
			var resultMessage = message;
			if (message?.Length >= MaxMessageSize)
			{
				var ending = " */далее обрезано/*";
				resultMessage = message.Substring(0, MaxMessageSize - ending.Length) + ending;
			}

			SendNotification(processQueue, subject, resultMessage, null);
		}

		public void SendNotification(OMQueue processQueue, string subject, string message, long? roleId)
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

		public void SendNotification(string subject, string message, MessageAddressersDto messageAddresses)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = messageAddresses,
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
