using System;
using System.Collections.Generic;
using Core.SRD;
using KadOzenka.Dal.DataImport;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Common;
using System.Linq;
using CommonSdks;
using CommonSdks.PlatformWrappers;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.DataImport;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.DataImport;
using Newtonsoft.Json;
using ObjectModel.Declarations;
using ObjectModel.Directory.Common;

namespace KadOzenka.Web.Controllers
{
	public class DataImportController : KoBaseController
	{
		public DataImportController(IGbuObjectService gbuObjectService, IRegisterCacheWrapper registerCacheWrapper)
			: base(gbuObjectService, registerCacheWrapper)
		{
			
		}

		[HttpGet]
        [SRDFunction(Tag = "")]
		public ActionResult MainData(long importId)
		{
			var import = OMImportDataLog
				.Where(x => x.Id == importId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var userName = import != null ? SRDCache.Users[(int)import.UserId]?.FullName : string.Empty;
			var status = import != null ? import.Status_Code : (ImportStatus?) null;

			return View(DataImporterLayoutDto.OMMap(import, userName, status));
		}

		[HttpGet]
		[SRDFunction(Tag = "")]
		public ActionResult DownloadImportDataFile(long importId)
		{
			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == importId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (import == null)
			{
				throw new Exception($"В журнале загрузок не найдена запись с ИД {importId}");
			}
			var file = DataImporterCommon.GetImportDataFileStream(importId);

			return File(file, GetContentTypeByExtension(import.FileExtension), DataImporterCommon.GetDownloadDataFileName(import));
		}

		[SRDFunction(Tag = "")]
		public FileResult DownloadImportResultFile(long importId)
		{
			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == importId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (import == null)
			{
				throw new Exception($"В журнале загрузок не найдена запись с ИД {importId}");
			}

			var file = DataImporterCommon.GetImportResultFileStream(importId);

			return File(file, GetContentTypeByExtension(import.FileExtension), DataImporterCommon.GetDownloadResultFileName(import));
		}


	    [HttpGet]
        [SRDFunction(Tag = "")]
		public ActionResult ImportReStart(long importId)
		{
			var import = OMImportDataLog
				.Where(x => x.Id == importId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (import == null)
			{
				throw new Exception($"В журнале загрузок не найдена запись с ИД {importId}");
			}

			RepeatFormation(import);

			return Content($"Выполнено повторное формирование файла по шаблону {import.DataFileTitle}");
		}


		#region Support Methods

		public static void RepeatFormation(OMImportDataLog import)
		{
			if (string.IsNullOrEmpty(import.DataFileName))
			{
				throw new Exception("Не задан шаблон файла");
			}

			var fileStream = DataImporterCommon.GetImportDataFileStream(import.Id);
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

		#endregion
	}
}
