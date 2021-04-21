using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.SRD;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Core.Shared.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Core.ErrorManagment;
using Core.Messages;
using Core.Register;
using GemBox.Spreadsheet;
using Ionic.Zip;
using KadOzenka.Dal.DataComparing;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.Tasks;
using ObjectModel.Directory;
using ObjectModel.Directory.Common;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.KO;
using Serilog;
using SharpCompress.Archives.Rar;

namespace KadOzenka.Dal.DataImport
{
    public class DataImporterGknLongProcess : ILongProcess
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<DataImporterGknLongProcess>();
		public const string LongProcessName = "DataImporterGkn";
		private DataImporterGknLongProcessProgressLogger DataImporterGknLongProcessProgressLogger { get; }

		public DataImporterGknLongProcess()
		{
			DataImporterGknLongProcessProgressLogger = new DataImporterGknLongProcessProgressLogger();
		}

		public static void AddImportToQueue(long mainRegisterId, string registerViewId, string templateFileName, Stream templateFile, long registerId, long objectId)
		{
			var import = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added, // TODO: доработать платформу, чтоб формировался Enum
				DataFileTitle = DataImporterCommon.GetDataFileTitle(templateFileName),
				FileExtension = DataImporterCommon.GetFileExtension(templateFileName),
				RegisterId = registerId,
				ObjectId = objectId,
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId
			};
			import.Save();

			import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
			FileStorageManager.Save(templateFile, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			import.Save();

			////TODO код для отладки
			//var cancelSource = new CancellationTokenSource();
			//var cancelToken = cancelSource.Token;
			//////TODO для тестирования отмены процеса
			////Task.Factory.StartNew(() =>
			////{
			////	Thread.Sleep(7000);
			////	cancelSource.Cancel();
			////});
			//new DataImporterGknLongProcess().StartProcess(new OMProcessType
			//{
			//	Description = "debug test"
			//},
			//	new OMQueue
			//	{
			//		Status_Code = Status.Added,
			//		UserId = SRDSession.GetCurrentUserId(),
			//		ObjectId = import.Id
			//	}, cancelToken);

			LongProcessManager.AddTaskToQueue(LongProcessName, OMImportDataLog.GetRegisterId(), import.Id);
		}

	    public static void RestartImport(long? importId)
	    {
            var import = OMImportDataLog
                .Where(x => x.Id == importId)
                .SelectAll()
                .ExecuteFirstOrDefault();

	        if (import == null)
	        {
	            throw new Exception($"Не найдена запись {importId} в журнале загрузки данных");
            }

	        if (import.Status_Code == ImportStatus.Added || import.Status_Code == ImportStatus.Running)
	        {
	            throw new Exception($"Невозможно перезапустить записть импорта {import} со статусом '{import.Status_Code.GetEnumDescription()}'");
	        }

            using (var ts = new TransactionScope())
	        {
	            import.DateStarted = null;
	            import.DateFinished = null;
	            import.ResultMessage = null;
	            import.TotalNumberOfObjects = null;
	            import.NumberOfImportedObjects = null;
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Added;
	            import.Save();

	            LongProcessManager.AddTaskToQueue(LongProcessName, OMImportDataLog.GetRegisterId(), import.Id);

                ts.Complete();
	        }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			Log.ForContext("ErrorId", errorId).Error(ex, "Ошибка фонового процесса. ID объекта {objectId}", objectId);
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
			Log.Information("Старт фонового процесса: {Description}.", processType.Description);

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
					   
			var templateFileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			try
			{
                ObjectModel.KO.OMTask omTask = ObjectModel.KO.OMTask.Where(x => x.Id == import.ObjectId).SelectAll().ExecuteFirstOrDefault();
				Log.Information("Начата обработка задачи с Id {TaskId}, типа '{type}', расширение файла {FileExtension}.",
					omTask?.Id, omTask?.NoteType_Code.GetEnumDescription(), import.FileExtension);
				if (omTask == null)
					throw new Exception($"Не найдено задание на оценку с ИД '{import.ObjectId}'");

				var resultFileExtension = omTask.NoteType_Code == KoNoteType.Petition ? ".xlsx" : ".xml";

				IDataImporterGkn importer = null;
				if (import.FileExtension == "zip")
				{
					ImportGknFromZip(import, templateFileStream, resultFileExtension);
				}
				else if (import.FileExtension == "rar")
				{
					ImportGknFromRar(import, templateFileStream, resultFileExtension);
				}
				else
				{
					importer = GetImporter(import.FileExtension, omTask.NoteType_Code);
				}

				importer?.Import(templateFileStream, omTask, import, cancellationToken);

				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
				import.DateFinished = DateTime.Now;
				import.Save();

				WorkerCommon.SetProgress(processQueue, 100);
            }
			catch (Exception ex)
			{
				long errorId = ErrorManager.LogError(ex);
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
				import.DateFinished = DateTime.Now;
				import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
				import.Save();

                throw;
            }

			ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == import.ObjectId).SelectAll().ExecuteFirstOrDefault();
			task.Status_Code = ObjectModel.Directory.KoTaskStatus.Ready;
			task.Save();

				// Отправка уведомления о завершении загрузки
			SendResultNotification(import, cancellationToken.IsCancellationRequested);

			Log.Information("Финиш фонового процесса: {Description}.", processType.Description);
		}


	    public bool Test()
		{
			return true;
		}


	    #region Support Methods

	    private IDataImporterGkn GetImporter(string fileExtension, KoNoteType taskType)
	    {
		    IDataImporterGkn importer;
		    if (fileExtension == "xlsx" && taskType == KoNoteType.Petition)
		    {
			    importer = new PetitionImporter(DataImporterGknLongProcessProgressLogger);
		    }
		    else if (fileExtension == "xlsx" && taskType != KoNoteType.Petition)
		    {
			    importer = new PetitionImporter(DataImporterGknLongProcessProgressLogger);
		    }
		    else if (fileExtension == "xml")
		    {
			    importer = new NotPetitionExcelImporter(DataImporterGknLongProcessProgressLogger);
		    }
		    else
		    {
			    throw new NotSupportedException($"Неподдерживаемое расширение файла '{fileExtension}'. Для заданий типа '{KoNoteType.Petition.GetEnumDescription()}' поддерживаемые расширения: .zip, .rar, .xlsx. Для остальных типов - .zip, .rar, .html");
		    }

			return importer;
		}

	    private static void ImportGknFromZip(OMImportDataLog import, FileStream templateFileStream, string usedFileExtension)
        {
	        Log.Information("Начат импорт из zip, новое расширение {UsedFileExtension}.", usedFileExtension);

			using (var filesFromZip = Ionic.Zip.ZipFile.Read(templateFileStream, new ReadOptions { Encoding = Encoding.GetEncoding("cp866") }))
            {
                if (filesFromZip.Count == 0)
                    throw new Exception("Передан пустой zip-файл");

                Log.Information($"Внутри zip архива {filesFromZip.Count} файлов.");

				foreach (var file in filesFromZip)
                {
                    if (file.Attributes == FileAttributes.Directory)
                        continue;

                    var fileExtension = Path.GetExtension(file.FileName);
                    if (fileExtension != usedFileExtension)
                    {
                        continue;
                    }
                    var stream = new MemoryStream();
                    file.Extract(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    Log.Information("Файл {InsideFileName} поставлен в очередь на обработку.", file.FileName);

					DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", file.FileName, stream, OMTask.GetRegisterId(), import.ObjectId.Value);
                }
            }
        }

        private static void ImportGknFromRar(OMImportDataLog import, FileStream templateFileStream, string usedFileExtension)
	    {
		    Log.Information("Начат импорт из rar, новое расширение {UsedFileExtension}.", usedFileExtension);

			using (var archive = RarArchive.Open(templateFileStream))
	        {
	            var entries = archive.Entries.Where(entry => entry.Key.EndsWith(usedFileExtension)).ToList();
	            Log.Information($"Внутри rar архива {entries.Count} файлов.");

				foreach (var entry in entries)
	            {
		            Log.Information("Файл {InsideFileName} поставлен в очередь на обработку.", entry.Key);

					DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", entry.Key, entry.OpenEntryStream(), OMTask.GetRegisterId(), import.ObjectId.Value);
	            }
	        }
	    }

        internal static void SendResultNotification(OMImportDataLog import, bool processWasStopped)
        {
	        new MessageService().SendMessages(dto: new MessageDto
			{
				Addressers = new MessageAddressersDto{UserIds = new [] { import.UserId } },
				Subject = $"Результат загрузки данных в реестр: {RegisterCache.GetRegisterData((int)import.MainRegisterId).Description} от ({import.DateCreated.GetString()})",
				Message = $@"Загрузка файла ""{import.DataFileName}"" была {(processWasStopped ? "остановлена" : "завершена")}.
Статус загрузки: {import.Status_Code.GetEnumDescription()}
<a href=""/Task/TaskCard?TaskId={import.ObjectId}"">Перейти к заданию на оценку</a>
<a href=""/RegistersView/DataImporter?Transition=1&80100100={import.Id}"">Перейти в журнал загрузки</a>",
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
			});
		}

        #endregion
	}
}
