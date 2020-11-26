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
	public class HarmonizationCodProcess : LongProcess
	{
		public const string LongProcessName = "HarmonizationCodProcess";
		private readonly ILogger _log = Log.ForContext<HarmonizationCodProcess>();

		public static long AddProcessToQueue(HarmonizationCODSettings settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			var cancelSource = new CancellationTokenSource();
			var cancelToken = cancelSource.Token;
			try
			{
				_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<HarmonizationCODSettings>();
				_log.Information("{ProcessType}. Настройки: {Settings}", processType.Description, JsonConvert.SerializeObject(settings));
				var harmonizationCOD = new HarmonizationCOD(settings, _log);

                var t = Task.Run(() => {
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}
						if (harmonizationCOD.MaxObjectsCount > 0 && harmonizationCOD.CurrentCount > 0)
						{
							var newProgress = (long)Math.Round(((double)harmonizationCOD.CurrentCount / harmonizationCOD.MaxObjectsCount) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
							}
						}
					}
				}, cancelToken);

                var baseItemsGetter = new HarmonizationItemsGetter(settings, _log);
                var objectChangeStatusDecorator = new GbuObjectStatusFilterDecorator<Item>(baseItemsGetter, _log, settings.UnitChangeStatus);
				var reportId = harmonizationCOD.Run(objectChangeStatusDecorator);
				//TestLongRunningProcess(settings);
				cancelSource.Cancel();
				t.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);
				_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);

				string message = "Операция успешно завершена." +
				                 $@"<a href=""/DataExport/DownloadExportResult?exportId={reportId}"">Скачать результат</a>";

				NotificationSender.SendNotification(processQueue, "Результат Операции Гармонизации с использованием справочника ЦОД", message);
			}
			catch (Exception ex)
			{
				cancelSource.Cancel();
				NotificationSender.SendNotification(processQueue, "Результат Операции Гармонизации с использованием справочника ЦОД", $"Операция была прервана: {ex.Message}");
				throw;
			}
		}

		protected void TestLongRunningProcess(HarmonizationCODSettings setting)
		{
            var harmonizationCOD = new HarmonizationCOD(setting, _log)
            {
                MaxObjectsCount = 500,
                CurrentCount = 0
            };

            for (int i = 0; i < 500; i++)
			{
                harmonizationCOD.CurrentCount++;
				Thread.Sleep(1000);
			}
		}
	}
}
