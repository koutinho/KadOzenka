using System;
using System.Collections.Generic;
using Core.Main.FileStorages;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.Controllers;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Web.Models.DataImporterLayout;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Common;
using System.IO;
using ObjectModel.Directory.Common;

namespace KadOzenka.Web.Controllers
{
	public class DataImporterLayoutController : BaseController
	{
		[HttpGet]
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
		public FileContentResult Download(long importId, bool downloadResult, string fileNameWithExtension = null)
		{
			var import = OMImportDataLog
				.Where(x => x.Id == importId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (import == null)
			{
				throw new Exception($"В журнале загрузок не найдена запись с ИД {importId}");
			}

			var fileName = downloadResult
				? DataImporterCommon.GetResultFileName(importId)
				: DataImporterCommon.GetTemplateName(importId);
			var templateFile = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated,
				fileName);
			var bytes = new byte[templateFile.Length];
			templateFile.Read(bytes);
			StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtension, out string contentType);

		    if (fileNameWithExtension != null)
		    {
		        GetFileExtensionByName(fileNameWithExtension, ref fileExtension, ref contentType);
		    }

            return File(bytes, contentType, fileName.Replace(importId.ToString(), Path.GetFileNameWithoutExtension(import.DataFileName)) + "." + fileExtension);
		}

	    [HttpGet]
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

			var fileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated,
				DataImporterCommon.GetTemplateName(importId));
			var columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(import.ColumnsMapping);
			DataImporterCommon.AddImportToQueue(import.MainRegisterId, import.RegisterViewId, import.DataFileName, fileStream, columns, import.DocumentId);

			return Content($"Выполнено повторное формирование файла по шаблону {import.DataFileName}");
		}

	    #region Helpers

        private static void GetFileExtensionByName(string fileNameWithExtension, ref string fileExtension, ref string contentType)
	    {
	        if (fileNameWithExtension.EndsWith(".xml"))
	        {
	            fileExtension = "xml";
	            contentType = "application/xml";
	        }
	        else if (fileNameWithExtension.EndsWith(".zip"))
	        {
	            fileExtension = "zip";
	            contentType = "application/zip";
	        }
	        else if (fileNameWithExtension.EndsWith(".rar"))
	        {
	            fileExtension = "rar";
	            contentType = "application/octet-stream";
	        }
	    }

	    #endregion
    }
}
