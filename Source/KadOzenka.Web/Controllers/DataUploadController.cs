using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GemBox.Spreadsheet;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Dal.DataExport;

namespace KadOzenka.Web.Controllers
{
	public class DataUploadController : BaseController
	{
		private const string RegistersList = "RegistersList";

		[HttpGet]
		public IActionResult DataUpload(string registerViewId, long? mainRegisterId)
		{
			if (!string.IsNullOrEmpty(registerViewId))
			{
				ViewBag.RegisterViewId = registerViewId;
			}
			if (mainRegisterId.HasValue)
			{
				ViewBag.MainRegisterId = mainRegisterId;
			}

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
				using (var stream = file.OpenReadStream())
				{
					var excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
					var ws = excelFile.Worksheets[0];
					var headerRow = ws.Rows[0];
					columnsNames
						.AddRange(headerRow.AllocatedCells.Where(x => x.Value != null)
							.Select((x, index) => new { Id = index, Name = x.Value.ToString() }));
					if (columnsNames.Count == 0)
					{
						throw new Exception("Выбран пустой файл");
					}
				}
				return Content(JsonConvert.SerializeObject(columnsNames), "application/json");
			}
			catch (Exception ex)
			{
				return Content(ex.Message);
			}
		}

		[HttpPost]
		public IActionResult AddExportToQueue(int mainRegisterId, string registerViewId, IFormFile file, List<DataColumnDto> columns)
		{
			using (var stream = file.OpenReadStream())
			{
				DataExporter.AddExportToQueue(mainRegisterId, registerViewId, file.FileName, stream,
					columns.Select(x => new DataExportColumn
					{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList());
			}

			return NoContent();
		}

		[HttpPost]
		public ActionResult ExportDataToExcel(int mainRegisterId, IFormFile file, List<DataColumnDto> columns)
		{
			ExcelFile excelFile;
			using (var stream = file.OpenReadStream())
			{
				excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
			}
			var resultStream = (MemoryStream)DataExporter.ExportDataToExcel(mainRegisterId, excelFile,
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
	}
}
