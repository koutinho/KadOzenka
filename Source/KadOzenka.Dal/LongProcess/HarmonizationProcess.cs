﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.LongProcess;
using ObjectModel.Gbu.Harmonization;

namespace KadOzenka.Dal.LongProcess
{
	public class HarmonizationProcess : LongProcess
	{
		public const string LongProcessName = "HarmonizationProcess";

		public static long AddProcessToQueue(HarmonizationSettings settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			var cancelSource = new CancellationTokenSource();
			var cancelToken = cancelSource.Token;
			try
			{
                WorkerCommon.SetProgress(processQueue, 0);

                var settings = processQueue.Parameters.DeserializeFromXml<HarmonizationSettings>();
                var harmonization = new Harmonization(settings);

                var t = Task.Run(() =>
                {
                    while (true)
                    {
                        if (cancelToken.IsCancellationRequested)
                        {
                            break;
                        }
                        if (harmonization.MaxObjectsCount > 0 && harmonization.CurrentCount > 0)
                        {
                            var newProgress = (long)Math.Round(((double)harmonization.CurrentCount / harmonization.MaxObjectsCount) * 100);
                            if (newProgress != processQueue.Progress)
                            {
                                WorkerCommon.SetProgress(processQueue, newProgress);
                            }
                        }
                    }
                }, cancelToken);

                var reportId = harmonization.Run();
                //TestLongRunningProcess(settings);
                cancelSource.Cancel();
                t.Wait(cancellationToken);
                cancelSource.Dispose();

                WorkerCommon.SetProgress(processQueue, 100);

                string message = "Операция успешно завершена." +
                                 $@"<a href=""/GbuObject/GetFileResult?reportId={reportId}"">Скачать результат</a>";

                NotificationSender.SendNotification(processQueue, "Результат Операции Гармонизации", message);
            }
			catch (Exception ex)
			{
				cancelSource.Cancel();
				NotificationSender.SendNotification(processQueue, "Результат Операции Гармонизации", $"Операция была прервана: {ex.Message}");
				throw;
			}
		}

		protected void TestLongRunningProcess(HarmonizationSettings setting)
		{
            var harmonization = new Harmonization(setting)
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
