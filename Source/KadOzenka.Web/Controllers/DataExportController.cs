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
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.DataImport;

namespace KadOzenka.Web.Controllers
{
	public class DataExportController : KoBaseController
	{
		public DataExportController(IGbuObjectService gbuObjectService, IRegisterCacheWrapper registerCacheWrapper)
			: base(gbuObjectService, registerCacheWrapper)
		{
			
		}

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

			var fileSizeFormat = "n2";
			var templateFileInfo = CalculateFileSize(DataExporterCommon.FileStorageName, export.DateCreated,
				DataExporterCommon.GetStorageTemplateFileName(export.Id));
			ViewBag.TemplateFileSizeKb = templateFileInfo.Kb.ToString(fileSizeFormat);
			ViewBag.TemplateFileSizeMb = templateFileInfo.Mb.ToString(fileSizeFormat);

			var resultFileInfo = CalculateFileSize(DataExporterCommon.FileStorageName, export.DateFinished.GetValueOrDefault(),
				DataExporterCommon.GetStorageResultFileName(export.Id));
			ViewBag.ResultFileSizeKb = resultFileInfo.Kb.ToString(fileSizeFormat);
			ViewBag.ResultFileSizeMb = resultFileInfo.Mb.ToString(fileSizeFormat);

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