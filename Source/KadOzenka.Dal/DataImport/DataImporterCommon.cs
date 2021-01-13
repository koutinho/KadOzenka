using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using Core.Messages;
using Core.Register;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.LongProcess.DataImport;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Declarations;
using ObjectModel.Directory.Common;
using ObjectModel.Market;

namespace KadOzenka.Dal.DataImport
{
	public class DataImporterCommon
	{
		public const string FileStorageName = "DataImporterFromTemplate";

		public static string GetStorageDataFileName(long importDataId) => $"{importDataId}_Template";
		public static string GetStorageResultFileName(long importDataId) => $"{importDataId}_Result";

		public static string GetDataFileTitle(string fileName) => Path.GetFileNameWithoutExtension(fileName);
		public static string GetFileResultTitleFromDataTitle(OMImportDataLog import) => $"{import.DataFileTitle}_Result";

		public static string GetDownloadDataFileName(OMImportDataLog import) => !string.IsNullOrEmpty(import.FileExtension) ? $"{import.DataFileTitle}.{import.FileExtension}" : $"{import.DataFileTitle}";
		public static string GetDownloadResultFileName(OMImportDataLog import) => !string.IsNullOrEmpty(import.FileExtension) ? $"{import.ResultFileTitle}.{import.FileExtension}" : $"{import.ResultFileTitle}";

		public static FileStream GetImportDataFileStream(long importId)
		{
			var import = OMImportDataLog
				.Where(x => x.Id == importId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			return FileStorageManager.GetFileStream(FileStorageName, import.DateCreated, import.DataFileName);
		}

		public static FileStream GetImportResultFileStream(long importId)
		{
			var import = OMImportDataLog
				.Where(x => x.Id == importId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			return FileStorageManager.GetFileStream(FileStorageName, import.DateFinished.Value, import.ResultFileName);
		}

		public static void RepeatFormation(OMImportDataLog import)
		{
			if (string.IsNullOrEmpty(import.DataFileName))
			{
				throw new Exception("Не задан шаблон файла");
			}

			var fileStream = GetImportDataFileStream(import.Id);
			List<DataExportColumn> columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(import.ColumnsMapping);
			if (import.MainRegisterId == OMDeclaration.GetRegisterId())
			{
				DataImporterDeclarations.AddImportToQueue(OMDeclaration.GetRegisterId(), "DeclarationsDeclaration", import.DataFileTitle, fileStream,
					columns);
			}
			else
			{
				DataImporterByTemplateLongProcess.AddImportToQueue(import.MainRegisterId, import.RegisterViewId, import.DataFileTitle, fileStream, columns, import.DocumentId);
			}
		}

		public static void SendResultNotification(OMImportDataLog import, string subject = null, string importStatus = null)
		{
			if (string.IsNullOrEmpty(subject))
				subject =
					$"Результат загрузки данных в реестр: {RegisterCache.GetRegisterData((int) import.MainRegisterId).Description} от ({import.DateCreated.GetString()})";

			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto { UserIds = new long[] { import.UserId } },
				Subject = subject,
				Message = $@"Загрузка файла ""{import.DataFileName}"" была завершена.
Статус загрузки: {importStatus ?? import.Status_Code.GetEnumDescription()}
<a href=""/DataImport/DownloadImportResultFile?importId={import.Id}"">Скачать результат</a>
<a href=""/DataImport/DownloadImportDataFile?importId={import.Id}"">Скачать исходный файл</a>
<a href=""/RegistersView/DataImporter?Transition=1&80100100={import.Id}"">Перейти в журнал загрузки</a>",
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
			});
		}

		public static string GetFileExtension(string fileName)
		{
			var extension = Path.GetExtension(fileName);
			if (extension != null && extension.StartsWith("."))
			{
				extension = extension.Substring(1);
			}

			return extension;
		}

		public static OMImportDataLog CreateDataFileImport(Stream fileStream, string fileName, long mainRegisterId, string registerViewId)
		{
			var import = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ImportStatus.Added,
				DataFileTitle = GetDataFileTitle(fileName),
				FileExtension = GetFileExtension(fileName),
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId
			};
			import.Save();

			import.DataFileName = GetStorageDataFileName(import.Id);
			FileStorageManager.Save(fileStream, FileStorageName, import.DateCreated, import.DataFileName);
			import.Save();

			return import;
		}

		public static void SaveResultFile(OMImportDataLog import, Stream streamResult)
		{
			import.ResultFileTitle = GetFileResultTitleFromDataTitle(import);
			import.ResultFileName = GetStorageResultFileName(import.Id);
			import.DateFinished = DateTime.Now;
			FileStorageManager.Save(streamResult, FileStorageName, import.DateFinished.Value, import.ResultFileName);
			import.Save();
		}
	}
}
