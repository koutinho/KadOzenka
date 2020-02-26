using System;
using System.Linq;
using System.Threading;
using Core.Main.FileStorages;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.DataExport;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Sud;


namespace KadOzenka.Dal.LongProcess.SudLongProcesses
{
	public class SudExportStatisticObjectProcess : ILongProcess
	{
		public const string LongProcessName = "SudExportStatisticObjectProcess";
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

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
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

			var file = DataExporterSud.ExportStatisticObject();
			FileStorageManager.Save(file, StorageName, DateTime.Now, $"Статистика по объектам недвижимости {export.Id}");

			export.Status = (long)ObjectModel.Directory.Common.ImportStatus.Completed;
			export.DateFinished = DateTime.Now;
			export.TemplateFileName = $"Статистика по объектам недвижимости {export.Id}";
			export.Save();
			WorkerCommon.SetProgress(processQueue, 100);
			SendResultNotification(export);
		}

		internal static void SendResultNotification(OMExportByTemplates export)
		{
			new MessageService().SendMessages(new MessageDto
			{
				UserIds = new long[] { export.UserId },
				Subject = $"Результат выгрузки Статистики по объектам недвижимости",
				Message = $@"Статус операции: {((ObjectModel.Directory.Common.ImportStatus)export.Status).GetEnumDescription()}
					<a href=""/Sud/DownloadExportResult?exportId={export.Id}"">Скачать результат</a>",
				IsUrgent = true,
				IsEmail = true
			});
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == objectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				return;
			}

			export.Status = (long)ObjectModel.Directory.Common.ImportStatus.Faulted;
			export.DateFinished = DateTime.Now;
			export.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : String.Empty)}";
			export.Save();
		}

		public bool Test()
		{
			return true;
		}
	}
}
