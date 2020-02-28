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
	public class HarmonizationCodProcess : LongProcess
	{
		public const string LongProcessName = "HarmonizationCodProcess";

		public static void AddProcessToQueue(HarmonizationCODSettings settings)
		{
			LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			var cancelSource = new CancellationTokenSource();
			var cancelToken = cancelSource.Token;
			try
			{
				WorkerCommon.SetProgress(processQueue, 0);
				var settings = processQueue.Parameters.DeserializeFromXml<HarmonizationCODSettings>();
				var t = Task.Run(() => {
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}
						if (HarmonizationCOD.MaxCount > 0 && HarmonizationCOD.CurrentCount > 0)
						{
							var newProgress = (long)Math.Round(((double)HarmonizationCOD.CurrentCount / HarmonizationCOD.MaxCount) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
							}
						}
					}
				}, cancelToken);

				HarmonizationCOD.Run(settings);
				//TestLongRunningProcess(settings);
				cancelSource.Cancel();
				t.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);
				NotificationSender.SendNotification(processQueue, "Результат Операции Гармонизации с использованием справочника ЦОД", "Операция успешно завершена");
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
			HarmonizationCOD.MaxCount = 500;
			HarmonizationCOD.CurrentCount = 0;
			for (int i = 0; i < 500; i++)
			{
				HarmonizationCOD.CurrentCount++;
				Thread.Sleep(1000);
			}
		}
	}
}
