using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.MarketObjects.Settings;
using KadOzenka.Dal.OutliersChecking;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Market;
using Serilog;

namespace KadOzenka.Dal.LongProcess.MarketObjects
{
	public class OutliersCheckingLongProcess : LongProcess
	{
		public const string LongProcessName = nameof(OutliersCheckingLongProcess);

		private static readonly ILogger Log = Serilog.Log.ForContext<OutliersCheckingLongProcess>();

		public static long AddProcessToQueue(OutliersCheckingProcessSettings settings)
		{
			var history = new OMOutliersCheckingHistory
			{
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
				PropertyTypesMapping = !settings.AllPropertyTypes 
					? JsonConvert.SerializeObject(settings.PropertyTypes) 
					: null,
			};
			if (settings.Segment.HasValue)
				history.MarketSegment_Code = settings.Segment.Value;
			history.Save();

			return LongProcessManager.AddTaskToQueue(LongProcessName, OMCoreObject.GetRegisterId(), history.Id, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Log.Information("Старт фонового процесса: {Description}.", processType.Description);

			if (!processQueue.ObjectId.HasValue)
			{
				WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				return;
			}

			var history = OMOutliersCheckingHistory.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();
			if (history == null)
			{
				WorkerCommon.SetMessage(processQueue, Consts.Consts.GetMessageForProcessInterruptedBecauseOfNoUnloadResultQueue(processQueue.ObjectId.Value));
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoUnloadResultQueue);
				return;
			}

			var cancelSource = new CancellationTokenSource();
			var cancelToken = cancelSource.Token;
			try
			{
				WorkerCommon.SetProgress(processQueue, 0);
				history.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
				history.DateStarted = DateTime.Now;
				history.Save();

				var settings = processQueue.Parameters.DeserializeFromXml<OutliersCheckingProcessSettings>();
				Log.Information("{ProcessType}. Настройки: {Settings}", processType.Description,
					JsonConvert.SerializeObject(settings));
				var outliersCheckingProcess = new OutliersCheckingProcess();

				var t = Task.Run(() =>
				{
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}

						if (outliersCheckingProcess.TotalObjectsCount > 0 &&
						    outliersCheckingProcess.CurrentHandledObjectsCount > 0)
						{
							var newProgress =
								(long) Math.Round(((double) outliersCheckingProcess.CurrentHandledObjectsCount /
								                   outliersCheckingProcess.TotalObjectsCount) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
								history.TotalObjectsCount = outliersCheckingProcess.TotalObjectsCount;
								history.ExcludedObjectsCount = outliersCheckingProcess.ExcludedObjectsCount;
								history.CurrentHandledObjectsCount = outliersCheckingProcess.CurrentHandledObjectsCount;
								history.Save();
							}
						}

						Thread.Sleep(1000);
					}
				}, cancelToken);

				var reportId = outliersCheckingProcess.PerformOutliersChecking(settings.Segment, settings.PropertyTypes);
				cancelSource.Cancel();
				t.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);
				history.DateFinished = DateTime.Now;
				history.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
				history.TotalObjectsCount = outliersCheckingProcess.TotalObjectsCount;
				history.ExcludedObjectsCount = outliersCheckingProcess.ExcludedObjectsCount;
				history.CurrentHandledObjectsCount = outliersCheckingProcess.CurrentHandledObjectsCount;
				history.ExportId = reportId;
				history.Save();

				SendSuccessEmail(processQueue, reportId);
				Log.Information("Завершение фонового процесса: {Description}.", processType.Description);
			}
			catch (Exception ex)
			{
				cancelSource.Cancel();
				history.DateFinished = DateTime.Now;
				history.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
				history.Save();
				NotificationSender.SendNotification(processQueue, "Результат Процедуры проверки на вылеты", $"Операция была прервана: {ex.Message}");
				throw;
			}
		}

		private static void SendSuccessEmail(OMQueue processQueue, long reportId)
		{
			string message = "Операция успешно завершена." +
			                 $@"<a href=""/DataExport/DownloadExportResult?exportId={reportId}"">Скачать результат</a>";

			NotificationSender.SendNotification(processQueue,
				"Результат Процедуры проверки на вылеты", message);
		}
	}
}
