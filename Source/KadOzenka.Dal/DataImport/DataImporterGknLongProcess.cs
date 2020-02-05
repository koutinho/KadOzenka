using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.SRD;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Core.Shared.Extensions;
using System.Linq;
using Core.ErrorManagment;
using Core.Messages;
using Core.Register;

namespace KadOzenka.Dal.DataImport
{
	public class DataImporterGknLongProcess : ILongProcess
	{
		public const string LongProcessName = "DataImporterGkn";

		public static void AddImportToQueue(long mainRegisterId, string registerViewId, string templateFileName, Stream templateFile, long registerId, long objectId)
		{
			var export = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added, // TODO: доработать платформу, чтоб формировался Enum
				DataFileName = templateFileName,
				RegisterId = registerId,
				ObjectId = objectId,
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId
			};
			export.Save();

			FileStorageManager.Save(templateFile, DataImporterCommon.FileStorageName, export.DateCreated, DataImporterCommon.GetTemplateName(export.Id));

			LongProcessManager.AddTaskToQueue(LongProcessName, OMExportByTemplates.GetRegisterId(), export.Id);
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == objectId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (import == null)
			{
				return;
			}

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
			import.DateFinished = DateTime.Now;
			import.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : String.Empty)}";
			import.Save();
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
			{
				return;
			}

			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (import == null)
			{
				return;
			}

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
			import.DateStarted = DateTime.Now;
			import.Save();
					   
			// Запустить формирование файла
			var templateFile = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, DataImporterCommon.GetTemplateName(import.Id));

			ImportGknFromXml(templateFile, import.ObjectId);
			
			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
			import.DateFinished = DateTime.Now;
			import.Save();

			ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == import.ObjectId).SelectAll().ExecuteFirstOrDefault();
			task.Status_Code = ObjectModel.Directory.KoTaskStatus.Ready;
			task.Save();

			// Отправка уведомления о завершении загрузки
			SendResultNotification(import);
		}

		public bool Test()
		{
			return true;
		}

		public static void ImportGknFromXml(FileStream fileStream, long? objectId)
		{
			ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == objectId).SelectAll().ExecuteFirstOrDefault();
			string schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");
					
			DataImporterGkn.ImportDataGknFromXml(fileStream, schemaPath, task);			
		}

		internal static void SendResultNotification(OMImportDataLog import)
		{
			new MessageService().SendMessages(new MessageDto
			{
				UserIds = new long[] { import.UserId },
				Subject = $"Результат загрузки данных в реестр: {RegisterCache.GetRegisterData((int)import.MainRegisterId).Description} от ({import.DateCreated.GetString()})",
				Message = $@"Загрузка файла ""{import.DataFileName}"" была завершена.
Статус загрузки: {import.Status_Code.GetEnumDescription()}
<a href=""/Task/TaskCard?TaskId={import.ObjectId}"">Перейти к заданию на оценку</a>
<a href=""/RegistersView/DataImporter?Transition=1&80100100={import.Id}"">Перейти в журнал загрузки</a>",
				IsUrgent = true,
				IsEmail = true
			});
		}
	}
}
