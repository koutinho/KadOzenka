using System;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Serilog;
using Core.Messages;

namespace KadOzenka.Dal.LongProcess
{
	public abstract class LongProcess : ILongProcess
	{
		private readonly ILogger _log = Log.ForContext<LongProcess>();
		protected const int PercentageInterval = 10;
        protected NotificationSender NotificationSender { get; set; }

		protected LongProcess()
		{
			NotificationSender = new NotificationSender();
		}

		public abstract void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken);

		public virtual void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			_log.ForContext("ErrorId", errorId).Error(ex, "Ошибка фонового процесса. ID объекта {objectId}", objectId);
			throw new NotImplementedException();
		}

		public virtual void AddToQueue(object input)
		{

		}

		public bool Test()
		{
			return true;
		}

        protected void AddLog(OMQueue processQueue, string message, bool withNewLine = true, ILogger logger = null)
        {
            var previousLog = string.IsNullOrWhiteSpace(processQueue.Log) ? string.Empty : processQueue.Log;

            var newLog = withNewLine && !string.IsNullOrWhiteSpace(previousLog)
                ? previousLog + Environment.NewLine + message
                : previousLog + message;

            processQueue.Log = newLog;
            processQueue.Save();

            if (logger == null)
            {
	            _log.Debug(message);
            }
            else
            {
				logger.Debug(message);
			}
        }

        protected void LogProgress(int maxCount, int currentCount, OMQueue processQueue)
        {
	        if (maxCount <= 0 || currentCount <= 0)
		        return;

	        var newProgress = (long)Math.Round(((double)currentCount / maxCount) * 100);
	        if (newProgress != processQueue.Progress)
		        WorkerCommon.SetProgress(processQueue, newProgress);
        }

		protected void SendMessage(OMQueue processQueue, string message, string subject)
        {
	        new MessageService().SendMessages(new MessageDto
	        {
		        Addressers = new MessageAddressersDto { UserIds = processQueue.UserId.HasValue ? new[] { processQueue.UserId.Value } : new long[] { } },
		        Subject = subject,
		        Message = message,
		        IsUrgent = true,
		        IsEmail = true,
		        ExpireDate = DateTime.Now.AddHours(2)
	        });
        }
	}
}
