using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.SRD;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using Core.Shared.Extensions;
using System.Linq;
using System.Text;
using System.Transactions;
using Core.ErrorManagment;
using Core.Messages;
using Core.Register;
using GemBox.Spreadsheet;
using Ionic.Zip;
using ObjectModel.Directory;
using SharpCompress.Archives.Rar;

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
					   
			var templateFileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, DataImporterCommon.GetTemplateName(import.Id));
			try
			{
                ObjectModel.KO.OMTask omTask = ObjectModel.KO.OMTask.Where(x => x.Id == import.ObjectId).SelectAll().ExecuteFirstOrDefault();
                if (omTask != null && omTask.NoteType_Code == KoNoteType.Petition)
                {
                    if (import.DataFileName.EndsWith(".zip"))
                    {
                        ImportGknFromZipForPetition(import, templateFileStream);
                    }
                    else if (import.DataFileName.EndsWith(".rar"))
                    {
                        ImportGknFromRarForPetition(import, templateFileStream);
                    }
                    else if (import.DataFileName.EndsWith(".xlsx"))
                    {
                        ImportGknFromXlsx(templateFileStream, import.ObjectId, import);
                    }
                    else
                    {
                        throw new NotSupportedException($"Неподдерживаемое расширение файла {import.DataFileName}, поддерживаемые расширения: .zip, .rar, .xlsx.");
                    }
                }
                else
                {
                    if (import.DataFileName.EndsWith(".zip"))
                    {
                        ImportGknFromZip(import, templateFileStream);
                    }
                    else if (import.DataFileName.EndsWith(".rar"))
                    {
                        ImportGknFromRar(import, templateFileStream);
                    }
                    else if (import.DataFileName.EndsWith(".xml"))
                    {
                        ImportGknFromXml(templateFileStream, import.ObjectId, import);
                    }
                    else
                    {
                        throw new NotSupportedException($"Неподдерживаемое расширение файла {import.DataFileName}");
                    }
                }

				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
				import.DateFinished = DateTime.Now;
				import.Save();
			}
			catch (Exception ex)
			{
				long errorId = ErrorManager.LogError(ex);
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
				import.DateFinished = DateTime.Now;
				import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
				import.Save();
			}			

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

		public static void ImportGknFromXml(FileStream fileStream, long? objectId, OMImportDataLog dataLog)
		{
			ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == objectId).SelectAll().ExecuteFirstOrDefault();
			string schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");

		    DataImporterGkn.ImportDataGknFromXml(fileStream, schemaPath, task);
		    CollectStatistic(dataLog, GetFileTotalNumberOfObjects(), GetFileTotalNumberOfImportedObjects());
        }

        public static void ImportGknFromXlsx(FileStream fileStream, long? objectId, OMImportDataLog dataLog)
        {
            ObjectModel.KO.OMTask task = ObjectModel.KO.OMTask.Where(x => x.Id == objectId).SelectAll().ExecuteFirstOrDefault();
            string schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");

            var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
            DataImporterGkn.ImportDataGknFromExcel(excelFile, schemaPath, task);
            CollectStatistic(dataLog, GetFileTotalNumberOfObjects(), GetFileTotalNumberOfImportedObjects());
        }

        private static void ImportGknFromZip(OMImportDataLog import, FileStream templateFileStream)
	    {
	        ObjectModel.KO.OMTask omTask = ObjectModel.KO.OMTask.Where(x => x.Id == import.ObjectId).SelectAll()
	            .ExecuteFirstOrDefault();
	        string schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");

	        var totalNumberOfObjects = 0;
	        var numberOfImportedObjects = 0;
	        using (ZipArchive archive = new ZipArchive(templateFileStream, ZipArchiveMode.Read))
	        {
	            var entries = archive.Entries.Where(x => x.Name.EndsWith(".xml")).ToList();
	            foreach (var zipArchiveEntry in entries)
	            {
	                DataImporterGkn.ImportDataGknFromXml(zipArchiveEntry.Open(), schemaPath, omTask);

	                totalNumberOfObjects += GetFileTotalNumberOfObjects();
	                numberOfImportedObjects += GetFileTotalNumberOfImportedObjects();

					CollectStatistic(import, totalNumberOfObjects, numberOfImportedObjects);
				}
	        }

	        CollectStatistic(import, totalNumberOfObjects, numberOfImportedObjects);
	    }

        private static void ImportGknFromZipForPetition(OMImportDataLog import, FileStream templateFileStream)
        {
            ObjectModel.KO.OMTask omTask = ObjectModel.KO.OMTask.Where(x => x.Id == import.ObjectId).SelectAll()
                .ExecuteFirstOrDefault();
            string schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");

            var totalNumberOfObjects = 0;
            var numberOfImportedObjects = 0;
           
            using (var filesFromZip = Ionic.Zip.ZipFile.Read(templateFileStream, new ReadOptions { Encoding = Encoding.GetEncoding("cp866") }))
            {
                if (filesFromZip.Count == 0)
                    throw new Exception("Передан пустой zip-файл");

                foreach (var file in filesFromZip)
                {
                    if (file.Attributes == FileAttributes.Directory)
                        continue;

                    var fileExtension = Path.GetExtension(file.FileName);
                    if (fileExtension != ".xlsx")
                    {
                        throw new Exception(
                            $"Внутри архива есть файл с неправильным форматом '{file.FileName}' (поддерживаемые форматы: .xlsx).");
                    }
                    var stream = new MemoryStream();
                    file.Extract(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    var excelFile = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
                    DataImporterGkn.ImportDataGknFromExcel(excelFile, schemaPath, omTask);

                    totalNumberOfObjects += GetFileTotalNumberOfObjects();
                    numberOfImportedObjects += GetFileTotalNumberOfImportedObjects();
                }
            }

            CollectStatistic(import, totalNumberOfObjects, numberOfImportedObjects);
        }

        private static void ImportGknFromRar(OMImportDataLog import, FileStream templateFileStream)
	    {
	        ObjectModel.KO.OMTask omTask = ObjectModel.KO.OMTask.Where(x => x.Id == import.ObjectId).SelectAll()
	            .ExecuteFirstOrDefault();
	        string schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");

	        var totalNumberOfObjects = 0;
	        var numberOfImportedObjects = 0;

            using (var archive = RarArchive.Open(templateFileStream))
	        {
	            var entries = archive.Entries.Where(entry => entry.Key.EndsWith(".xml")).ToList();
                foreach (var entry in entries)
	            {
	                DataImporterGkn.ImportDataGknFromXml(entry.OpenEntryStream(), schemaPath, omTask);

	                totalNumberOfObjects += GetFileTotalNumberOfObjects();
	                numberOfImportedObjects += GetFileTotalNumberOfImportedObjects();
	            }
	        }

	        CollectStatistic(import, totalNumberOfObjects, numberOfImportedObjects);
	    }

        private static void ImportGknFromRarForPetition(OMImportDataLog import, FileStream templateFileStream)
        {
            ObjectModel.KO.OMTask omTask = ObjectModel.KO.OMTask.Where(x => x.Id == import.ObjectId).SelectAll()
                .ExecuteFirstOrDefault();
            string schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");

            var totalNumberOfObjects = 0;
            var numberOfImportedObjects = 0;

            using (var archive = RarArchive.Open(templateFileStream))
            {
                var entries = archive.Entries.Where(entry => entry.Key.EndsWith(".xlsx")).ToList();
                foreach (var entry in entries)
                {
                    var excelFile = ExcelFile.Load(entry.OpenEntryStream(), LoadOptions.XlsxDefault);
                    DataImporterGkn.ImportDataGknFromExcel(excelFile, schemaPath, omTask);

                    totalNumberOfObjects += GetFileTotalNumberOfObjects();
                    numberOfImportedObjects += GetFileTotalNumberOfImportedObjects();
                }
            }

            CollectStatistic(import, totalNumberOfObjects, numberOfImportedObjects);
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

        private static void CollectStatistic(OMImportDataLog dataLog, int totalNumberOfObjects, int totalNumberOfImportedObjects)
        {
            using (var ts = new TransactionScope())
            {
                dataLog.TotalNumberOfObjects = totalNumberOfObjects;
                dataLog.NumberOfImportedObjects = totalNumberOfImportedObjects;
                dataLog.Save();

                ts.Complete();
            }
        }

	    private static int GetFileTotalNumberOfImportedObjects()
	    {
	        return DataImporterGkn.CountImportBuildings + DataImporterGkn.CountImportParcels +
	               DataImporterGkn.CountImportConstructions + DataImporterGkn.CountImportUncompliteds +
	               DataImporterGkn.CountImportFlats + DataImporterGkn.CountImportCarPlaces;
	    }

	    private static int GetFileTotalNumberOfObjects()
	    {
	        return DataImporterGkn.CountXmlBuildings + DataImporterGkn.CountXmlParcels +
	               DataImporterGkn.CountXmlConstructions + DataImporterGkn.CountXmlUncompliteds +
	               DataImporterGkn.CountXmlFlats + DataImporterGkn.CountXmlCarPlaces;
	    }
	}
}
