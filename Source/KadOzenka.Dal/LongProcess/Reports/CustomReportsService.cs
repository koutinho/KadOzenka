using System;
using System.IO;
using System.Linq;
using System.Text;
using Core.Main.FileStorages;
using Core.SRD;
using Ionic.Zip;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using ObjectModel.Common;
using ObjectModel.Directory.Common;

namespace KadOzenka.Dal.LongProcess.Reports
{
	public class CustomReportsService
	{
		public static string FileStorageKey => "SaveReportPath";
		private ZipFile _generalZipFile;


		public CustomReportsService()
		{
			_generalZipFile = new ZipFile
			{
				AlternateEncoding = Encoding.UTF8,
				AlternateEncodingUsage = ZipOption.AsNecessary
			};
		}


		public OMReportFiles GetFileInfo(long reportId)
		{
			var report = OMReportFiles.Where(x => x.Id == reportId).SelectAll().ExecuteFirstOrDefault();
			if (report == null)
				throw new Exception($"В журнале с сохраненными отчетами не найдена запись с ИД '{reportId}'");

			return report;
		}


		public void AddFileToZip(MemoryStream stream, string fileName, string fileExtension)
		{
			using (var currentZipFile = new ZipFile())
			{
				currentZipFile.AlternateEncoding = Encoding.UTF8;
				currentZipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

				stream.Seek(0, SeekOrigin.Begin);
				currentZipFile.AddEntry($"{fileName}.{fileExtension}", stream);

				var currentZipStream = new MemoryStream();
				currentZipFile.Save(currentZipStream);
				currentZipStream.Seek(0, SeekOrigin.Begin);

				var counter = _generalZipFile.Entries.Count + 1;
				var zipFileName = $"{fileName} №{counter} (архив).zip";

				_generalZipFile.AddEntry(zipFileName, currentZipStream);
			}
		}

		public string SaveReport(string fileName)
		{
			MemoryStream stream;
			if (_generalZipFile.Entries.Count == 1)
			{
				var entry = _generalZipFile.Entries.ElementAt(0);
				stream = (MemoryStream) entry.InputStream;
			}
			else
			{
				stream = new MemoryStream();
				_generalZipFile.Save(stream);
			}

			stream.Seek(0, SeekOrigin.Begin);

			return SaveFile(stream, fileName, "zip");
		}

		public string SaveFile(MemoryStream stream, string fileName, string fileExtension)
		{
			var currentDate = DateTime.Now;
			var report = new OMReportFiles
			{
				UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				CreationDate = currentDate,
				Status_Code = ExportStatus.Added,
				FileName = fileName,
				FileExtension = string.IsNullOrWhiteSpace(fileExtension) ? "csv" : fileExtension
			};
			report.Save();

			FileStorageManager.Save(stream, FileStorageKey, report.DateOnServer, report.FileNameOnServer);

			report.FinishDate = DateTime.Now;
			report.Status_Code = ExportStatus.Completed;
			report.Save();

			//TODO Dal не должен знать о контроллере
			return $"/CustomReports/Download?reportId={report.Id}";
		}

		public CustomReportInfo GetFile(long reportId)
		{
			var report = GetFileInfo(reportId);

			var file = FileStorageManager.GetFileStream(FileStorageKey, report.DateOnServer, report.FileNameOnServer);

			return new CustomReportInfo
			{
				Stream = file,
				FullFileName = $"{report.FileName}.{report.FileExtension}",
				FileExtension = report.FileExtension
			};
		}
	}
}
