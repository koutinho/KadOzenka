using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Gbu.CodSelection;
using Serilog;

namespace KadOzenka.Dal.LongProcess.GbuLongProcesses
{
	public class SelectionCodLongProcess : LongProcess
	{
		private readonly ILogger _log = Log.ForContext<SelectionCodLongProcess>();
		public const string LongProcessName = "SelectionCodLongProcess";

		public static long AddProcessToQueue(CodSelectionSettings settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken)
		{
			try
			{
				_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<CodSelectionSettings>();
				_log.Information("{ProcessType}. Настройки: {Settings}", processType.Description, JsonConvert.SerializeObject(settings));

				LongProcessProgressLogger.StartLogProgress(processQueue, () => SelectionCOD.MaxCount, () => SelectionCOD.CurrentCount);
				long reportId = SelectionCOD.Run(settings);
				LongProcessProgressLogger.StopLogProgress();

				WorkerCommon.SetProgress(processQueue, 100);
				_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);

				string message = "Операция успешно завершена." +
				                 $@"<a href=""/DataExport/DownloadExportResult?exportId={reportId}"">Скачать результат</a>";

				NotificationSender.SendNotification(processQueue, "Результат Операции выборки из справочника ЦОД", message);
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Операция выборки из справочника ЦОД завершена с ошибкой");
				LongProcessProgressLogger.StopLogProgress();
				NotificationSender.SendNotification(processQueue, "Результат Операции выборки из справочника ЦОД", $"Операция была прервана: {ex.Message}");
				throw;
			}
		}
	}
}