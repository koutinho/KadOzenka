using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GemBox.Spreadsheet;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Dal.DataExport;
using Core.Main.FileStorages;
using Core.Register;
using ObjectModel.Common;
using Core.SRD;
using KadOzenka.Dal.LongProcess.CalculateSystem;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.DataImporterLayout;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class DataExportController : KoBaseController
	{
		private readonly int _dataCountForBackgroundLoading = 1000;

		[HttpGet]
        [SRDFunction(Tag = "")]
		public IActionResult DataExport(string registerViewId, long? mainRegisterId)
		{
			if (!string.IsNullOrEmpty(registerViewId))
			{
				ViewBag.RegisterViewId = registerViewId;
			}
			if (mainRegisterId.HasValue)
			{
				ViewBag.MainRegisterId = mainRegisterId;
			}
			ViewBag.DataCountForBackgroundLoading = _dataCountForBackgroundLoading;

			return View();
		}

		[HttpPost]
        [SRDFunction(Tag = "")]
		public ActionResult ParseFileColumns(List<IFormFile> files)
		{
			if (files == null || files.Count == 0)
			{
				return EmptyResponse();
			}

			try
			{
				var file = files.FirstOrDefault();
				var columnsNames = new List<object>();
				int dataRowCount;
				using (var stream = file.OpenReadStream())
				{
					var excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
					var ws = excelFile.Worksheets[0];
					var headerRow = ws.Rows[0];
					dataRowCount = ws.Rows.Count - 1;
					columnsNames
						.AddRange(headerRow.AllocatedCells.Where(x => x.Value != null)
							.Select((x, index) => new { Id = index, Name = x.Value.ToString() }));
					if (columnsNames.Count == 0)
					{
						throw new Exception("Выбран пустой файл");
					}
				}

				return Content(JsonConvert.SerializeObject(new { DataCount = dataRowCount, ColumnsNames = columnsNames }),
					"application/json");
			}
			catch (Exception ex)
			{
				return Content(ex.Message);
			}
		}

		[HttpPost]
        [SRDFunction(Tag = "")]
		public JsonResult AddExportToQueue(int mainRegisterId, string registerViewId, IFormFile file, List<DataColumnDto> columns)
		{
            ValidateColumns(columns);

            long exportByTemplateId;
            using (var stream = file.OpenReadStream())
			{
                exportByTemplateId = DataExporterByTemplate.AddExportToQueue(mainRegisterId, registerViewId, file.FileName, stream,
					columns.Select(x => new DataExportColumn
					{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList());
			}

            return new JsonResult(new {ExportByTemplateId = exportByTemplateId});
        }

		[HttpPost]
        [SRDFunction(Tag = "")]
		public ActionResult ExportDataToExcel(int mainRegisterId, IFormFile file, List<DataColumnDto> columns)
		{
			ValidateColumns(columns);

			ExcelFile excelFile;
			using (var stream = file.OpenReadStream())
			{
				excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
			}
			var resultStream = (MemoryStream)DataExporterByTemplate.ExportDataToExcel(mainRegisterId, excelFile,
				columns.Select(x => new DataExportColumn
				{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList());

			HttpContext.Session.Set(file.FileName, resultStream.ToArray());
			return Content(JsonConvert.SerializeObject(new { success = true, fileName = file.FileName }), "application/json");
		}

        [HttpGet]
        [SRDFunction(Tag = "")]
		public ActionResult DownloadExcelFile(string fileName)
		{
			var fileContent = HttpContext.Session.Get(fileName);
			if (fileContent == null)
			{
				return new EmptyResult();
			}

			HttpContext.Session.Remove(fileName);
			StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtensiton, out string contentType);

			return File(fileContent, contentType, fileName);
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

		[SRDFunction(Tag = "")]
		public ActionResult UnloadSettings()
		{
			KOUnloadSettings settings = new KOUnloadSettings();
			return View(settings);
		}

		[HttpPost]
        [SRDFunction(Tag = "")]
		public ActionResult UnloadSettings(UnloadSettingsDto settings)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			KOUnloadSettings settingsUnload = UnloadSettingsDto.Map(settings);
			KoDownloadResultProcess.AddImportToQueue(settingsUnload.IdTour, settingsUnload);
			return Ok();
		}

		#region Support Methods

        private void ValidateColumns(List<DataColumnDto> columns)
        {
            if (columns.All(x => x.IsKey == false))
            {
                throw new Exception("Должен быть выбран хотя бы один ключевой параметр");
            }

            if (columns.Count(x => x.IsKey) > 1)
            {
                throw new Exception("Должен быть выбран только один ключевой параметр");
            }
        }

        #endregion
    }
}