using System;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks;
using Serilog;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.LongProcess._Common;
using Microsoft.Extensions.Configuration;

namespace KadOzenka.Dal.LongProcess
{
	public abstract class LongProcess : ILongProcess
	{
		private readonly ILogger _log = Log.ForContext<LongProcess>();
		protected const int PercentageInterval = 10;
		protected INotificationSender NotificationSender { get; set; }
		protected ILongProcessProgressLogger LongProcessProgressLogger { get; set; }


		protected LongProcess()
		{
			NotificationSender = new NotificationSender();
			LongProcessProgressLogger = new LongProcessProgressLogger();
		}

		protected LongProcess(INotificationSender notificationSender, ILongProcessProgressLogger logger)
		{
			NotificationSender = notificationSender ?? new NotificationSender();
			LongProcessProgressLogger = logger ?? new LongProcessProgressLogger();
		}


		public abstract void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken);

		public virtual void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			_log.ForContext("ErrorId", errorId).Error(ex, "Ошибка фонового процесса. ID объекта {objectId}", objectId);
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

        protected void CheckCancellationToken(CancellationToken processCancellationToken,
	        CancellationTokenSource localCancellationToken, ParallelOptions options)
        {
	        if (!processCancellationToken.IsCancellationRequested)
		        return;

	        localCancellationToken.Cancel();
	        options.CancellationToken.ThrowIfCancellationRequested();
        }

        protected ParallelThreadsConfig GetParallelThreadsConfig(string sectionName, int defaultPackageSize, int defaultThreadsCount)
        {
	        var fileName = "appsettings.json";
	        _log.Debug("Поиск настроек конфигурации из файла {FileName}", fileName);

	        var configuration = new ConfigurationBuilder()
		        .AddJsonFile(path: fileName, optional: false, reloadOnChange: true)
		        .Build();

	        var config = new ParallelThreadsConfig();
	        var fullSectionName = $"MainOperations:{sectionName}";
	        configuration.GetSection(fullSectionName).Bind(config);
	        _log.ForContext("Configs", config, true).Debug("Полученные настройки конфигурации для секции {SectionName}", fullSectionName);

	        var packageSize = config.PackageSize == 0 ? defaultPackageSize : config.PackageSize;
	        var threadsCountForObjects = config.ThreadsCount == 0 ? defaultThreadsCount : config.ThreadsCount;

	        config.PackageSize = packageSize;
	        config.ThreadsCount = threadsCountForObjects;

	        _log.ForContext("ResultConfigs", config, true)
		        .Debug("Итоговые настройки конфигурации для секции {SectionName}", fullSectionName);

	        return config;
        }
	}
}
