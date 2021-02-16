using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
	public class HarmonizationProcess : LongProcess
	{
		public const string LongProcessName = "HarmonizationProcess";
		private readonly ILogger _log = Log.ForContext<HarmonizationProcess>();

        public static long AddProcessToQueue(HarmonizationSettings settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
				_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
                WorkerCommon.SetProgress(processQueue, 0);

                var settings = processQueue.Parameters.DeserializeFromXml<HarmonizationSettings>();
                _log.Information("{ProcessType}. Настройки: {Settings}", processType.Description, JsonConvert.SerializeObject(settings));
                
                var harmonization = new Harmonization(settings, _log);
                LongProcessProgressLogger.StartLogProgress(processQueue, () => harmonization.MaxObjectsCount, () => harmonization.CurrentCount);

                var urlToDownload = harmonization.Run();

                //TestLongRunningProcess(settings);
                LongProcessProgressLogger.StopLogProgress();

                WorkerCommon.SetProgress(processQueue, 100);
                _log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);

                string message = "Операция успешно завершена." +
                                 $@"<a href=""{urlToDownload}"">Скачать результат</a>";

                NotificationSender.SendNotification(processQueue, "Результат Операции Гармонизации", message);
            }
			catch (Exception ex)
			{
				_log.Error(ex, "Операция гармонизации завершена с ошибкой");
                LongProcessProgressLogger.StopLogProgress();
                NotificationSender.SendNotification(processQueue, "Результат Операции Гармонизации", $"Операция была прервана: {ex.Message}");
				throw;
			}
		}

		protected void TestLongRunningProcess(HarmonizationSettings setting)
		{
            var harmonization = new Harmonization(setting, _log)
            {
                MaxObjectsCount = 500,
                CurrentCount = 0
            };

            for (int i = 0; i < 500; i++)
            {
                harmonization.CurrentCount++;
                Thread.Sleep(1000);
            }
        }
	}
}
