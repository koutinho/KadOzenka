using System;
using System.IO;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.DataImport.Dto;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess
{
	public class ImportDataFromExcelLongProcess : LongProcess
	{
		public const string LongProcessName = "ImportDataFromExcelLongProcess";

		public static void AddProcessToQueue(Stream stream, ImportDataFromExcelDto settings)
		{
			var import = DataImporterKO.CreateDataFileImport(stream, settings);
			LongProcessManager.AddTaskToQueue(LongProcessName, settings.MainRegisterId, import.Id, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
			{
				WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				return;
			}

			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();
			if (import == null)
			{
				WorkerCommon.SetMessage(processQueue, Consts.Consts.GetMessageForProcessInterruptedBecauseOfNoDataLog(processQueue.ObjectId.Value));
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoDataLog);
				return;
			}

			try
			{
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<ImportDataFromExcelDto>();
				var dataFile = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
				ExcelFile excelFile = ExcelFile.Load(dataFile, LoadOptions.XlsxDefault);

				new ImportKoFactoryCreator().ExecuteImport(excelFile, settings, import);

				var message = GetMessage(import);
				NotificationSender.SendNotification(processQueue, "Импорт группы из Excel", message);
				WorkerCommon.SetProgress(processQueue, 100);
			}
			catch (Exception e)
			{
				int numberError = ErrorManager.LogError(e);
				var message = $"В результате выполнения возникли ошибки (журнал № {numberError})";

				WorkerCommon.SetMessage(processQueue, message);
				WorkerCommon.SetProgress(processQueue, 100);

				NotificationSender.SendNotification(processQueue, "Импорт группы из Excel", message);

				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
				import.DateFinished = DateTime.Now;
				import.ResultMessage = message;
				import.Save();

				throw;
			}
		}

		private string GetMessage(OMImportDataLog import)
		{
			return $@"Операция успешно завершена.
<a href=""/DataImport/DownloadImportResultFile?importId={import.Id}"">Скачать результат</a>
<a href=""/DataImport/DownloadImportDataFile?importId={import.Id}"">Скачать исходный файл</a>";
		}
	}
}
