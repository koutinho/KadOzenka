﻿using System;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Serilog;
using Core.Messages;
using KadOzenka.Dal.Logger;

namespace KadOzenka.Dal.LongProcess
{
	public abstract class LongProcess : ILongProcess
	{
		private readonly ILogger _log = Log.ForContext<LongProcess>();
		protected const int PercentageInterval = 10;
		protected const int MaxMessageSize = 4000;
        protected NotificationSender NotificationSender { get; set; }
		protected LongProcessProgressLogger LongProcessProgressLogger { get; set; }

		protected LongProcess()
		{
			NotificationSender = new NotificationSender();
			LongProcessProgressLogger = new LongProcessProgressLogger();
		}

		public abstract void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken);

		public virtual void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			_log.ForContext("ErrorId", errorId).Error(ex, "Ошибка фонового процесса. ID объекта {objectId}", objectId);
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

        protected void SendMessage(OMQueue processQueue, string message, string subject)
        {
	        var resultMessage = message;
	        if (message?.Length >= MaxMessageSize)
	        {
		        var ending = " */далее обрезано/*";
		        resultMessage = message.Substring(0, MaxMessageSize - ending.Length) + ending;
	        }

	        new MessageService().SendMessages(new MessageDto
	        {
		        Addressers = new MessageAddressersDto { UserIds = processQueue.UserId.HasValue ? new[] { processQueue.UserId.Value } : new long[] { } },
		        Subject = subject,
		        Message = resultMessage,
		        IsUrgent = true,
		        IsEmail = true,
		        ExpireDate = DateTime.Now.AddHours(2)
	        });
        }
	}
}
