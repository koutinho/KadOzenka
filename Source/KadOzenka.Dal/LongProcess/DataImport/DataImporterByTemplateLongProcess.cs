using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.DataImport.DataImporterByTemplate;
using KadOzenka.Dal.LongProcess.Common;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.DataImport
{
	public class DataImporterByTemplateLongProcess : ILongProcess
	{
		public const string LongProcessName = "DataImporterFromTemplate";
		private readonly ILogger _log = Log.ForContext<DataImporterByTemplateLongProcess>();

		public static long AddImportToQueue(long mainRegisterId, string registerViewId, string templateFileName,
			Stream templateFile, List<DataExportColumn> columns, long? documentId)
		{
			string jsonstring = JsonConvert.SerializeObject(columns);
			var import = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added, // TODO: доработать платформу, чтоб формировался Enum
				DataFileTitle = DataImporterCommon.GetDataFileTitle(templateFileName),
				FileExtension = DataImporterCommon.GetFileExtension(templateFileName),
				ColumnsMapping = jsonstring,
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId,
				DocumentId = documentId
			};
			import.Save();

			import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
			FileStorageManager.Save(templateFile, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			import.Save();

			LongProcessManager.AddTaskToQueue(LongProcessName, OMImportDataLog.GetRegisterId(), import.Id);

			return import.Id;
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
			{
				WorkerCommon.SetMessage(processQueue, Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				return;
			}

			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();
			if (import == null)
			{
				WorkerCommon.SetMessage(processQueue, Consts.GetMessageForProcessInterruptedBecauseOfNoDataLog(processQueue.ObjectId.Value));
				WorkerCommon.SetProgress(processQueue, Consts.ProgressForProcessInterruptedBecauseOfNoDataLog);
				return;
			}

			WorkerCommon.SetProgress(processQueue, 0);

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
			import.DateStarted = DateTime.Now;
			import.Save();

			// Запустить формирование файла
			var templateFile = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			ExcelFile excelTemplate = ExcelFile.Load(templateFile, LoadOptions.XlsxDefault);

			WorkerCommon.SetProgress(processQueue, 25);

			List<DataExportColumn> columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(import.ColumnsMapping);
			var dataImporter =
				DataImporterByTemplateFactory.CreateDataImporterByTemplate((int) import.MainRegisterId);
			var result = dataImporter.ImportDataFromExcel(excelTemplate, columns, import.DocumentId);

			WorkerCommon.SetProgress(processQueue, 75);

			// Сохранение файла
			import.DateFinished = DateTime.Now;
			import.ResultFileTitle = DataImporterCommon.GetFileResultTitleFromDataTitle(import);
			import.ResultFileName = DataImporterCommon.GetStorageResultFileName(import.Id);
			FileStorageManager.Save(result.ResultFile, DataImporterCommon.FileStorageName, import.DateFinished.Value, import.ResultFileName);

			if (result.Status == DataImportStatus.Failed)
			{
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
				import.Save();
				DataImporterCommon.SendResultNotification(import);
				throw new Exception("Файл содержит некорректные данные");
			}

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
			import.Save();

			DataImporterCommon.SendResultNotification(import, importStatus: result.Status.GetShortTitle());
			WorkerCommon.SetProgress(processQueue, 100);
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			_log.ForContext("ErrorId", errorId).Error(ex, "Ошибка фонового процесса. ID объекта {objectId}", objectId);
			OMImportDataLog import = OMImportDataLog.Where(x => x.Id == objectId).SelectAll().Execute().FirstOrDefault();
			if (import == null) return;
			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
			import.DateFinished = DateTime.Now;
			import.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : String.Empty)}";
			import.Save();
		}

		public bool Test()
		{
			return true;
		}
	}
}
