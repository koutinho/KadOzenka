using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using Core.Messages;
using Newtonsoft.Json;
using ObjectModel.Common;

namespace KadOzenka.Dal.DataExport
{
	public class DataExporterCommon
	{
		public const string FileStorageName = "DataExporterByTemplate";

		public static string GetStorageTemplateFileName(long exportDataId) => $"{exportDataId}_Template";
		public static string GetStorageResultFileName(long exportDataId) => $"{exportDataId}_Result";

		public static string GetDownloadTemplateFileName(OMExportByTemplates export) => $"{export.FileTemplateTitle}.{export.FileExtension}";
		public static string GetDownloadResultFileName(OMExportByTemplates export) => $"{export.FileResultTitle}.{export.FileExtension}";

		public static FileStream GetExportTemplateFileStream(long exportId)
		{
			var export = OMExportByTemplates
				.Where(x => x.Id == exportId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			return FileStorageManager.GetFileStream(FileStorageName, export.DateCreated, export.TemplateFileName);
		}

		public static FileStream GetExportResultFileStream(long exportId)
		{
			var export = OMExportByTemplates
				.Where(x => x.Id == exportId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			return FileStorageManager.GetFileStream(FileStorageName, export.DateFinished.Value, export.ResultFileName);
		}

		public static void SendResultNotification(OMExportByTemplates export)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto { UserIds = new[] { export.UserId } },
				Subject = $"Результаты Выгрузки данных по списку от: {export.DateCreated})",
				Message = $@"Процесс Выгрузки данных по списку завершен. <a href=""/DataExport/DownloadExportResult?exportId={export.Id}"">Скачать результаты</a>",
				IsUrgent = true,
				IsEmail = true,
				ExpireDate = DateTime.Now.AddHours(2)
            });
		}

		public static string RepeatFormation(OMExportByTemplates export)
		{
			if (string.IsNullOrEmpty(export.TemplateFileName))
			{
				throw new Exception("Не задан шаблон файла");
			}

			var fileStream = GetExportTemplateFileStream(export.Id);
			List<DataExportColumn> columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(export.ColumnsMapping);
			DataExporterByTemplate.AddExportToQueue(export.MainRegisterId, export.RegisterViewId, export.FileTemplateTitle, fileStream, columns);
			return $"Выполнено повторное формирование файла по шаблону {export.FileTemplateTitle}";
		}
	}
}
