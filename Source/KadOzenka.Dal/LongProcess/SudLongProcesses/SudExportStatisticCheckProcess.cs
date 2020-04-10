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
		public const string StorageName = "SudExportFiles";

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
				return;
			}

			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				return;
			}
			WorkerCommon.SetProgress(processQueue, 0);

			export.Status = (long)ObjectModel.Directory.Common.ImportStatus.Running;
			export.DateStarted = DateTime.Now;
			export.Save();

			var file = DataExporterSud.ExportStatisticCheck();

            WorkerCommon.SetProgress(processQueue, 75);

            FileStorageManager.Save(file, StorageName, DateTime.Now, $"Статистика по положительным судебным решениям {export.Id}");

			export.Status = (long)ObjectModel.Directory.Common.ImportStatus.Completed;
			export.DateFinished = DateTime.Now;
			export.TemplateFileName = $"Статистика по положительным судебным решениям {export.Id}";
			export.Save();

			WorkerCommon.SetProgress(processQueue, 100);

			NotificationSender.SendExportResultNotificationWithAttachment(export, "Результат выгрузки Статистики по положительным судебным решениям");
		}
	}
}
