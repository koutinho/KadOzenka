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
using KadOzenka.Web.Models.DataImporterLayout;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class DataExportController : KoBaseController
	{
		private readonly int _dataCountForBackgroundLoading = 1000;

		[HttpGet]
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
		public JsonResult AddExportToQueue(int mainRegisterId, string registerViewId, IFormFile file, List<DataColumnDto> columns)
		{
            ValidateColumns(columns);

            long exportByTemplateId;
            using (var stream = file.OpenReadStream())
			{
                exportByTemplateId = DataExporterCommon.AddExportToQueue(mainRegisterId, registerViewId, file.FileName, stream,
					columns.Select(x => new DataExportColumn
					{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList());
			}

            return new JsonResult(new {ExportByTemplateId = exportByTemplateId});
        }

		[HttpPost]
		public ActionResult ExportDataToExcel(int mainRegisterId, IFormFile file, List<DataColumnDto> columns)
		{
			ValidateColumns(columns);

			ExcelFile excelFile;
			using (var stream = file.OpenReadStream())
			{
				excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
			}
			var resultStream = (MemoryStream)DataExporterCommon.ExportDataToExcel(mainRegisterId, excelFile,
				columns.Select(x => new DataExportColumn
				{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList());

			HttpContext.Session.Set(file.FileName, resultStream.ToArray());
			return Content(JsonConvert.SerializeObject(new { success = true, fileName = file.FileName }), "application/json");
		}

        [HttpGet]
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
		public ActionResult DownloadExcelTemplate(long objectId, string fileType)
		{
			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == objectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				throw new Exception($"В журнале выгрузок не найдена запись с ИД {objectId}");
			}

			string FileStorageName = "DataExporterByTemplate";
			string TemplateName = $"{export.Id}_{fileType}";
			FileStream templateFile = FileStorageManager.GetFileStream(FileStorageName, export.DateCreated, TemplateName);

			byte[] bytes = new byte[templateFile.Length];
			templateFile.Read(bytes);

			StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtensiton, out string contentType);

			return File(bytes, contentType, $"{Path.GetFileNameWithoutExtension(export.TemplateFileName)}_{fileType}.{fileExtensiton}");			
		}

		[HttpGet]
		public ActionResult RepeatFormation(long objectId)
		{
			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == objectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				throw new Exception($"В журнале выгрузок не найдена запись с ИД {objectId}");
			}

			string FileStorageName = "DataExporterByTemplate";
			string TemplateName = $"{export.Id}_Template";
			FileStream fs = FileStorageManager.GetFileStream(FileStorageName, export.DateCreated, TemplateName);
			
			List<DataExportColumn> columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(export.ColumnsMapping);
			DataExporterCommon.AddExportToQueue(export.MainRegisterId, export.RegisterViewId, export.TemplateFileName, fs, columns);

			return Content($"Выполнено повторное формирование файла по шаблону {export.TemplateFileName}");				
		}

		[HttpGet]
		public IActionResult Details(long objectId)
		{
			OMExportByTemplates export = OMExportByTemplates.Where(x => x.Id == objectId).SelectAll().Execute().FirstOrDefault();

			ViewBag.User = SRDCache.Users[(int)export.UserId].FullName;
			ViewBag.StatusDescription = ((RegistersExportStatus)export.Status).GetEnumDescription();

			string FileStorageName = "DataExporterByTemplate";
			string fileLocation = FileStorageManager.GetPathForFileFolder(FileStorageName, export.DateCreated);

			fileLocation = Path.Combine(fileLocation, $"{export.Id}_Template");
	
			if (!string.IsNullOrEmpty(fileLocation))
				if (System.IO.File.Exists(fileLocation))
				{
					long fileSize = new FileInfo(fileLocation).Length;
					ViewBag.FileSizeKb = Convert.ToString(fileSize / 1024);
					ViewBag.FileSizeMb = Convert.ToString(fileSize / (1024 * 1024));
				}

			var dbColumns = JsonConvert.DeserializeObject<List<DataExportColumn>>(export.ColumnsMapping);
			var columnsMappingDtoList = dbColumns.Select(x => new ColumnsMappingDto
			{
				ColumnName = x.ColumnName,
				AttributeName = RegisterCache.GetAttributeData((int)x.AttributrId).Name,
				IsKey = x.IsKey
			}).ToList();
			ViewBag.ColumnsMappingDtoListJson = JsonConvert.SerializeObject(columnsMappingDtoList);

			return View(export);
		}

        public FileResult DownloadExportData(long exportId)
        {
            var file = DataExporterCommon.GetExportResultFileStream(exportId);

            return File(file, Helpers.Consts.ExcelContentType, "Результат выгрузки данных по списку" + ".xlsx");
        }

		public ActionResult UnloadSettings()
		{
			KOUnloadSettings settings = new KOUnloadSettings();
			return View(settings);
		}

		[HttpPost]
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

		public FileResult DownloadKoExportResult(long reportId, bool isXml)
		{
			var export = OMExportByTemplates.Where(x => x.Id == reportId).SelectAll().ExecuteFirstOrDefault();

			if (export == null)
			{
				throw new Exception($"В журнале выгрузок не найдена запись с ИД {reportId}");
			}

			var templateFile = FileStorageManager.GetFileStream(SaveReportDownload.StorageName, export.DateCreated,
				export.Id.ToString());


			StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtension, out string contentType);

			string defaultEx = ".xlsx";
			string reportName = export.TemplateFileName;

			if (string.IsNullOrEmpty(Path.GetExtension(reportName)) && !isXml)
			{
				reportName += defaultEx;
			}

			if (isXml)
			{
				contentType = "application/xml";

				if (string.IsNullOrEmpty(Path.GetExtension(reportName)))
				{
					reportName += ".xml";
				}
			}

			return File(templateFile, contentType, reportName);
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