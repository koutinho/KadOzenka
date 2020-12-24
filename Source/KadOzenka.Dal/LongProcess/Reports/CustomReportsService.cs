using System;
using System.IO;
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

		public string SaveReportZip(MemoryStream stream, string fileName, string fileExtension)
		{
			using (var zipFile = new ZipFile())
			{
				zipFile.AlternateEncoding = Encoding.UTF8;
				zipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

				stream.Seek(0, SeekOrigin.Begin);
				zipFile.AddEntry($"{fileName}.{fileExtension}", stream);

				var zipStream = new MemoryStream();
				zipFile.Save(zipStream);
				zipStream.Seek(0, SeekOrigin.Begin);
				var zipFileName = $"{fileName} (архив)";

				return SaveReport(zipStream, zipFileName, "zip");
			}
		}

		public string SaveReport(MemoryStream stream, string fileName, string fileExtension)
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

			return $"/CustomReports/Download?reportId={report.Id}";
		}

		public CustomReportInfo GerReportInfo(long reportId)
		{
			var report = OMReportFiles.Where(x => x.Id == reportId).SelectAll().ExecuteFirstOrDefault();
			if (report == null)
				throw new Exception($"В журнале с сохраненными отчетами не найдена запись с ИД '{reportId}'");

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
