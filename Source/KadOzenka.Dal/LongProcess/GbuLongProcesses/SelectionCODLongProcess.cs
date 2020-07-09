using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.LongProcess;
using ObjectModel.Gbu.CodSelection;

namespace KadOzenka.Dal.LongProcess.GbuLongProcesses
{
	public class SelectionCodLongProcess : LongProcess
	{
		public const string LongProcessName = "SelectionCodLongProcess";

		public static long AddProcessToQueue(CodSelectionSettings settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken)
		{
			var cancelSource = new CancellationTokenSource();
			var cancelToken = cancelSource.Token;
			try
			{
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<CodSelectionSettings>();
				var t = Task.Run(() =>
				{
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}

						if (SelectionCOD.MaxCount > 0 && SelectionCOD.CurrentCount > 0)
						{
							var newProgress =
								(long) Math.Round(((double) SelectionCOD.CurrentCount / SelectionCOD.MaxCount) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
							}
						}
					}
				}, cancelToken);


				long reportId = SelectionCOD.Run(settings);
				cancelSource.Cancel();
				t.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);


				string message = "Операция успешно завершена." +
				                 $@"<a href=""/DataExport/DownloadExportResult?exportId={reportId}"">Скачать результат</a>";

				NotificationSender.SendNotification(processQueue, "Результат Операции выборки из справочника ЦОД", message);
			}
			catch (Exception ex)
			{
				cancelSource.Cancel();
				NotificationSender.SendNotification(processQueue, "Результат Операции выборки из справочника ЦОД", $"Операция была прервана: {ex.Message}");
				throw;
			}
		}
	}
}