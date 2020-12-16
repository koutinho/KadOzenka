using System;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Core.LongProcess;
using ObjectModel.Gbu.GroupingAlgoritm;

namespace KadOzenka.Dal.LongProcess
{
	public class SetPriorityGroupProcess : LongProcess
	{
		public const string LongProcessName = "SetPriorityGroupProcess";

		public static long AddProcessToQueue(GroupingSettings settings)
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

				var settings = processQueue.Parameters.DeserializeFromXml<GroupingSettings>();
				var t = Task.Run(() => {
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}
						if (PriorityGrouping.MaxCount > 0 && PriorityGrouping.CurrentCount > 0)
						{
							var newProgress = (long)Math.Round(((double)PriorityGrouping.CurrentCount / PriorityGrouping.MaxCount) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
							}
						}

						Thread.Sleep(1000);
					}
				}, cancellationToken);

				long reportId = PriorityGrouping.SetPriorityGroup(settings);
				//TestLongRunningProcess(settings);

				cancelSource.Cancel();
				t.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);

				string message = "Операция успешно завершена. " +
				                 $@"<a href=""/DataExport/DownloadExportResult?exportId={reportId}"">Скачать результат</a>";


				NotificationSender.SendNotification(processQueue, "Результат Операции группировки", message);
			}
			catch (Exception ex)
			{
				cancelSource.Cancel();
				NotificationSender.SendNotification(processQueue, "Результат Операции группировки", $"Операция была прервана: {ex.Message}");
				ErrorManager.LogError(ex);
				throw;
			}
		}

		protected void TestLongRunningProcess(GroupingSettings setting)
		{
			PriorityGrouping.MaxCount = 200;
			PriorityGrouping.CurrentCount = 0;
			for (int i = 0; i < 200; i++)
			{
				PriorityGrouping.CurrentCount++;
				Thread.Sleep(1000);
			}
		}
	}
}
