using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core.Main.FileStorages;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Core.SRD;
using Ionic.Zip;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using ObjectModel.Common;
using ObjectModel.Core.Reports;
using Platform.Reports;

namespace KadOzenka.Dal.LongProcess.Reports
{
	public class CustomReportsService
	{
		public static string FileStorageKey => "SaveReportPath";
		private readonly ZipFile _generalZipFile;


		public CustomReportsService()
		{
			_generalZipFile = new ZipFile
			{
				AlternateEncoding = Encoding.UTF8,
				AlternateEncodingUsage = ZipOption.AsNecessary
			};
		}



		public void AddZipFileToGeneralZipArchive(MemoryStream stream, string fileName, string fileExtension)
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

		public void AddExcelFileToGeneralZipArchive(MemoryStream stream, string fileName)
		{
			var counter = _generalZipFile.Entries.Count + 1;
			var fullFileName = $"{fileName} №{counter}.xlsx";

			_generalZipFile.AddEntry(fullFileName, stream);
		}

		public string SaveReport(string fileName, long reportCode, object parameters)
		{
			if (_generalZipFile.Entries.Count == 0)
				return string.Empty;

			//внутри zip-архива может быть как другой архив, так и файл напрямую
			var fileContainsOnlyZip = _generalZipFile.All(x => x.FileName.EndsWith(".zip"));
			MemoryStream stream;
			if (_generalZipFile.Entries.Count == 1 && fileContainsOnlyZip)
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

			return SaveFile(stream, fileName, reportCode, parameters);
		}

		public string SaveFile(MemoryStream stream, string fileName, long reportCode, object parameters)
		{
			var currentDate = DateTime.Now;
			var report = new OMSavedReport
			{
				Code = reportCode,
				Parameters = parameters.SerializeToXml(),
				UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				CreateDate = currentDate,
				Title = fileName,
				//из-за особенностей плаформы приходится сохрянять с типом pdf
				FileType = RegistersExportType.Pdf.GetEnumDescription(),
				Status = (long?) RegistersExportStatus.Created
			};
			report.Save();

			var path = FileStorageManager.GetPathForStorage(FileStorageKey);
			var fullFileName = Path.Combine(path, $"{report.Id}.zip");
			using (var fs = File.Create(fullFileName))
			{
				fs.Seek(0, SeekOrigin.Begin);
				stream.CopyTo(fs);
			}

			report.EndDate = DateTime.Now;
			report.Status = (long?) RegistersExportStatus.Finished;
			report.Save();

			//TODO Dal не должен знать о контроллере
			return $"/GeneralReports/Download?reportId={report.Id}";
		}
	}
}
