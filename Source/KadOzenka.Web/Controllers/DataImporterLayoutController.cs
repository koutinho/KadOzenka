﻿using System;
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

namespace KadOzenka.Web.Controllers
{
	public class DataImporterLayoutController : BaseController
	{
		[HttpGet]
		public ActionResult MainData(long importId)
		{
			var import = OMImportFromTemplates
				.Where(x => x.Id == importId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var userName = import != null ? SRDCache.Users[(int)import.UserId]?.FullName : string.Empty;
			var status = import != null ? (RegistersExportStatus)import.Status : (RegistersExportStatus?) null;

			return View(DataImporterLayoutDto.OMMap(import, userName, status));
		}

		[HttpGet]
		public FileContentResult Download(long importId)
		{
			var import = OMImportFromTemplates
				.Where(x => x.Id == importId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (import == null)
			{
				throw new Exception($"В журнале загрузок не найдена запись с ИД {importId}");
			}

			var templateFileName = DataImporter.GetTemplateName(importId);
			var templateFile = FileStorageManager.GetFileStream(DataImporter.FileStorageName, import.DateCreated,
				templateFileName);
			var bytes = new byte[templateFile.Length];
			templateFile.Read(bytes);
			StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtension, out string contentType);

			return File(bytes, contentType, templateFileName + "." + fileExtension);
		}

		[HttpGet]
		public ActionResult ImportReStart(long importId)
		{
			var import = OMImportFromTemplates
				.Where(x => x.Id == importId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (import == null)
			{
				throw new Exception($"В журнале загрузок не найдена запись с ИД {importId}");
			}

			var fileStream = FileStorageManager.GetFileStream(DataImporter.FileStorageName, import.DateCreated,
				DataImporter.GetTemplateName(importId));
			var columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(import.ColumnsMapping);
			DataImporter.AddImportToQueue(import.MainRegisterId, import.RegisterViewId, import.TemplateFileName, fileStream, columns);

			return Content($"Выполнено повторное формирование файла по шаблону {import.TemplateFileName}");
		}
	}
}
