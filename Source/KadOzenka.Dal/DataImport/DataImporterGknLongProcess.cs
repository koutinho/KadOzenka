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
using System.Transactions;
using Core.ErrorManagment;
using Core.Messages;
using Core.Register;
using Ionic.Zip;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.LongProcess.Common;
using Newtonsoft.Json;
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

		public static OMImportDataLog SaveImportDataLog(string templateFileName, Stream templateFile, long taskId,
			List<ColumnToAttributeMapping> columnsMapping = null)
		{
			var mainRegisterId = OMTask.GetRegisterId();
			var registerViewId = "Tasks";
			var serializedColumnsMapping = columnsMapping == null ? null : JsonConvert.SerializeObject(columnsMapping);

			var import = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added, // TODO: доработать платформу, чтоб формировался Enum
				DataFileTitle = DataImporterCommon.GetDataFileTitle(templateFileName),
				FileExtension = DataImporterCommon.GetFileExtension(templateFileName),
				ColumnsMapping = serializedColumnsMapping,
				RegisterId = mainRegisterId,
				ObjectId = taskId,
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId
			};
			import.Save();

			import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
			FileStorageManager.Save(templateFile, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			import.Save();

			return import;
		}

		public static void AddImportToQueue(string templateFileName, Stream templateFile, long taskId,
			List<ColumnToAttributeMapping> columnsMapping = null)
		{
			var import = SaveImportDataLog(templateFileName, templateFile, taskId, columnsMapping);

			//TODO код для отладки
			var cancelSource = new CancellationTokenSource();
			var cancelToken = cancelSource.Token;
			////TODO для тестирования отмены процеса
			//Task.Factory.StartNew(() =>
			//{
			//	Thread.Sleep(7000);
			//	cancelSource.Cancel();
			//});
			new DataImporterGknLongProcess().StartProcess(new OMProcessType
			{
				Description = "debug test"
			},
				new OMQueue
				{
					Status_Code = Status.Added,
					UserId = SRDSession.GetCurrentUserId(),
					ObjectId = import.Id
				}, cancelToken);

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

			string urlToDownloadReportWithErrors = null;
			var templateFileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			try
			{
                ObjectModel.KO.OMTask omTask = ObjectModel.KO.OMTask.Where(x => x.Id == import.ObjectId).SelectAll().ExecuteFirstOrDefault();
				Log.Information("Начата обработка задачи с Id {TaskId}, типа '{type}', расширение файла {FileExtension}.",
					omTask?.Id, omTask?.NoteType_Code.GetEnumDescription(), import.FileExtension);
				ValidateTask(omTask);

				var resultFileExtensionForArchives = ".xml";

				BaseImporter importer = null;
				if (import.FileExtension == "zip")
				{
					ImportGknFromZip(import, templateFileStream, resultFileExtensionForArchives);
				}
				else if (import.FileExtension == "rar")
				{
					ImportGknFromRar(import, templateFileStream, resultFileExtensionForArchives);
				}
				else
				{
					importer = GetImporter(import.FileExtension);
				}

				urlToDownloadReportWithErrors = importer?.Import(templateFileStream, omTask, import, cancellationToken,
					import.ColumnsMapping);

				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
				import.DateFinished = DateTime.Now;
				import.Save();

				var task = OMTask.Where(x => x.Id == import.ObjectId).SelectAll().ExecuteFirstOrDefault();
				task.Status_Code = KoTaskStatus.Ready;
				task.Save();

				WorkerCommon.SetProgress(processQueue, 100);
            }
			catch (Exception ex)
			{
				Log.Error(ex, "Ошибка при выполнение длительного процесса для Импорта документов для задания на оценку");
				long errorId = ErrorManager.LogError(ex);
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
				import.DateFinished = DateTime.Now;
				import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
				import.Save();
			}

			SendResultNotification(import, urlToDownloadReportWithErrors, cancellationToken.IsCancellationRequested);

			Log.Information("Финиш фонового процесса: {Description}.", processType.Description);
		}


	    public bool Test()
		{
			return true;
		}


	    #region Support Methods

	    private void ValidateTask(OMTask task)
	    {
		    if (task == null)
			    throw new Exception("Не найдено задание на оценку");

			var messages = new List<string>();

			if (task.TourId == null)
				messages.Add("Тур");
			if (task.EstimationDate == null)
				messages.Add("Дата оценки");
			if (task.DocumentId == null)
				messages.Add("Входящий документ");

			if (messages.Count != 0)
				throw new Exception($"В задании на оценку не указаны: {string.Join(',', messages)}");
	    }

	    private BaseImporter GetImporter(string fileExtension)
	    {
		    BaseImporter importer;
		    if (fileExtension == "xlsx")
		    {
			    importer = new ExcelImporter(DataImporterGknLongProcessProgressLogger);
		    }
			else if (fileExtension == "xml")
		    {
			    importer = new XmlImporter(DataImporterGknLongProcessProgressLogger);
		    }
		    else
		    {
			    throw new NotSupportedException($"Неподдерживаемое расширение файла '{fileExtension}'. Поддерживаемые расширения: .zip, .rar, .xlsx, .xml.");
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

					DataImporterGknLongProcess.AddImportToQueue(file.FileName, stream, import.ObjectId.Value);
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

					AddImportToQueue(entry.Key, entry.OpenEntryStream(), import.ObjectId.Value);
	            }
	        }
	    }

        private void SendResultNotification(OMImportDataLog import, string urlToDownloadReportWithErrors, bool processWasStopped)
        {
	        var reportWithErrorsMessage = string.IsNullOrWhiteSpace(urlToDownloadReportWithErrors) 
		        ? null 
		        : $@"<a href=""{urlToDownloadReportWithErrors}"">Скачать отчет с ошибками</a>";

	        new MessageService().SendMessages(dto: new MessageDto
			{
				Addressers = new MessageAddressersDto{UserIds = new [] { import.UserId } },
				Subject = $"Результат загрузки данных в реестр: {RegisterCache.GetRegisterData((int)import.MainRegisterId).Description} от ({import.DateCreated.GetString()})",
				Message = $@"Загрузка файла ""{import.DataFileName}"" была {(processWasStopped ? "остановлена" : "завершена")}.
Статус загрузки: {import.Status_Code.GetEnumDescription()}
<a href=""/Task/TaskCard?TaskId={import.ObjectId}"">Перейти к заданию на оценку</a>
<a href=""/RegistersView/DataImporter?Transition=1&80100100={import.Id}"">Перейти в журнал загрузки</a>
{reportWithErrorsMessage}",
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
			});
		}

        #endregion
	}
}
