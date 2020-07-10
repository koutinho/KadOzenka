using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GemBox.Spreadsheet;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Dal.DataExport;
using KadOzenka.Web.Attributes;

namespace KadOzenka.Web.Controllers
{
	public class DataExportByTemplateController : KoBaseController
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

			return new JsonResult(new { ExportByTemplateId = exportByTemplateId });
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