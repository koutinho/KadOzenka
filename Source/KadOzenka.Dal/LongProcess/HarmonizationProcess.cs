using System;
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
				var t = Task.Run(() => {
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}
						if (Harmonization.MaxCount > 0 && Harmonization.CurrentCount > 0)
						{
							var newProgress = (long)Math.Round(((double)Harmonization.CurrentCount / Harmonization.MaxCount) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
							}
						}
					}
				}, cancelToken);

				Harmonization.Run(settings);
				//TestLongRunningProcess(settings);
				cancelSource.Cancel();
				t.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);

				NotificationSender.SendNotification(processQueue, "Результат Операции Гармонизации", "Операция успешно завершена");
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
			Harmonization.MaxCount = 900;
			Harmonization.CurrentCount = 0;
			for (int i = 0; i < 900; i++)
			{
				Harmonization.CurrentCount++;
				Thread.Sleep(1000);
			}
		}
	}
}
