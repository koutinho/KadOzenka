using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KadOzenka.Dal.DataExport;
using Core.Main.FileStorages;
using Core.Register;
using ObjectModel.Common;
using Core.SRD;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.DataImporterLayout;

namespace KadOzenka.Web.Controllers
{
	public class DataExportController : KoBaseController
	{
		[HttpGet]
		[SRDFunction(Tag = "")]
		public ActionResult DownloadExportTemplate(long exportId)
		{
			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == exportId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				throw new Exception($"В журнале выгрузок не найдена запись с ИД {exportId}");
			}
			var file = DataExporterCommon.GetExportTemplateFileStream(exportId);

			return File(file, GetContentTypeByExtension(export.FileExtension), DataExporterCommon.GetDownloadTemplateFileName(export));
		}

		[SRDFunction(Tag = "")]
		public FileResult DownloadExportResult(long exportId)
		{
			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == exportId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				throw new Exception($"В журнале выгрузок не найдена запись с ИД {exportId}");
			}

			var file = DataExporterCommon.GetExportResultFileStream(exportId);

			return File(file, GetContentTypeByExtension(export.FileExtension), DataExporterCommon.GetDownloadResultFileName(export));
		}

		[HttpGet]
		[SRDFunction(Tag = "")]
		public ActionResult RepeatFormation(long exportId)
		{
			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == exportId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				throw new Exception($"В журнале выгрузок не найдена запись с ИД {exportId}");
			}

			var resultMessage = DataExporterCommon.RepeatFormation(export);

			return Content(resultMessage);
		}

		[HttpGet]
		[SRDFunction(Tag = "")]
		public IActionResult Details(long objectId)
		{
			OMExportByTemplates export = OMExportByTemplates.Where(x => x.Id == objectId).SelectAll().Execute().FirstOrDefault();

			ViewBag.User = SRDCache.Users[(int)export.UserId].FullName;
			ViewBag.StatusDescription = ((RegistersExportStatus)export.Status).GetEnumDescription();

			string templateFileLocation = FileStorageManager.GetPathForFileFolder(DataExporterCommon.FileStorageName, export.DateCreated);
			templateFileLocation = Path.Combine(templateFileLocation, DataExporterCommon.GetStorageTemplateFileName(export.Id));
			if (!string.IsNullOrEmpty(templateFileLocation))
				if (System.IO.File.Exists(templateFileLocation))
				{
					long templateFileSize = new FileInfo(templateFileLocation).Length;
					ViewBag.TemplateFileSizeKb = Convert.ToString(templateFileSize / 1024);
					ViewBag.TemplateFileSizeMb = Convert.ToString(templateFileSize / (1024 * 1024));
				}

			string resultFileLocation = FileStorageManager.GetPathForFileFolder(DataExporterCommon.FileStorageName, export.DateFinished.GetValueOrDefault());
			resultFileLocation = Path.Combine(resultFileLocation, DataExporterCommon.GetStorageResultFileName(export.Id));
			if (!string.IsNullOrEmpty(resultFileLocation))
				if (System.IO.File.Exists(resultFileLocation))
				{
					long resultFileSize = new FileInfo(resultFileLocation).Length;
					ViewBag.ResultFileSizeKb = Convert.ToString(resultFileSize / 1024);
					ViewBag.ResultFileSizeMb = Convert.ToString(resultFileSize / (1024 * 1024));
				}

			List<ColumnsMappingDto> columnsMappingDtoList = null;
			if (!string.IsNullOrEmpty(export.ColumnsMapping))
			{
				var dbColumns = JsonConvert.DeserializeObject<List<DataExportColumn>>(export.ColumnsMapping);
				columnsMappingDtoList = dbColumns.Select(x => new ColumnsMappingDto
				{
					ColumnName = x.ColumnName,
					AttributeName = RegisterCache.GetAttributeData((int)x.AttributrId).Name,
					IsKey = x.IsKey
				}).ToList();
			}
			ViewBag.ColumnsMappingDtoListJson = JsonConvert.SerializeObject(columnsMappingDtoList);

			return View(export);
		}
	}
}