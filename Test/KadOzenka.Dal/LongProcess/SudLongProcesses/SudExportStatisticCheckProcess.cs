using System;
using System.Linq;
using System.Threading;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.SRD;
using KadOzenka.Dal.DataExport;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Sud;


namespace KadOzenka.Dal.LongProcess.SudLongProcesses
{
	public class SudExportStatisticCheckProcess : SudExportLongProcess
	{
		public const string LongProcessName = "SudExportStatisticCheckProcess";

		public static void AddImportToQueue()
		{
			var export = new OMExportByTemplates
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status = 0,
				TemplateFileName =  null,
				ColumnsMapping = null,
				MainRegisterId = OMObject.GetRegisterId(),
				RegisterViewId = "SudObjects"
			};
			export.Save();

			LongProcessManager.AddTaskToQueue(LongProcessName, OMObject.GetRegisterId(), export.Id);
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
			{
                WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
                return;
			}

			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
                WorkerCommon.SetMessage(processQueue, Consts.Consts.GetMessageForProcessInterruptedBecauseOfNoDataLog(processQueue.ObjectId.Value));
                WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoDataLog);
                return;
			}

			WorkerCommon.SetProgress(processQueue, 0);

			export.Status = (long)ObjectModel.Directory.Common.ImportStatus.Running;
			export.DateStarted = DateTime.Now;
			export.Save();

			export.FileResultTitle = $"Статистика по положительным судебным решениям {export.Id}";
			export.FileExtension = "xlsx";
			export.Save();

			var file = DataExporterSud.ExportStatisticCheck();

            WorkerCommon.SetProgress(processQueue, 75);

            export.DateFinished = DateTime.Now;
            export.ResultFileName = DataExporterCommon.GetStorageResultFileName(export.Id);
            export.Status = (long)ObjectModel.Directory.Common.ImportStatus.Completed;
            FileStorageManager.Save(file, DataExporterCommon.FileStorageName, export.DateFinished.Value, export.ResultFileName);
            export.Save();

            WorkerCommon.SetProgress(processQueue, 100);

			NotificationSender.SendExportResultNotificationWithAttachment(export, "Результат выгрузки Статистики по положительным судебным решениям");
		}
	}
}
