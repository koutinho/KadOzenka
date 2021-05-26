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
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Attributes;
using MarketPlaceBusiness.Common;
using ObjectModel.Gbu;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Web.Controllers
{
	public class DataExportByTemplateController : KoBaseController
	{
		private readonly int _dataCountForBackgroundLoading = 1000;

		public DataExportByTemplateController(IGbuObjectService gbuObjectService, IRegisterCacheWrapper registerCacheWrapper)
			: base(gbuObjectService, registerCacheWrapper)
		{

		}

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
            var exporter = GetExporter(mainRegisterId);

            var mappedColumns = MapColumns(columns);
            exporter.ValidateColumns(mappedColumns);

            long exportByTemplateId;
			using (var stream = file.OpenReadStream())
			{
                exportByTemplateId = exporter.AddExportToQueue(mainRegisterId, registerViewId, file.FileName, stream, mappedColumns);
			}

			return new JsonResult(new { ExportByTemplateId = exportByTemplateId });
		}

        [HttpPost]
		[SRDFunction(Tag = "")]
		public ActionResult ExportDataToExcel(int mainRegisterId, IFormFile file, List<DataColumnDto> columns)
        {
            var exporter = GetExporter(mainRegisterId);

            var mappedColumns = MapColumns(columns);
            exporter.ValidateColumns(mappedColumns);

			ExcelFile excelFile;
			using (var stream = file.OpenReadStream())
			{
				excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
			}

            var resultStream = (MemoryStream)exporter.ExportDataToExcel(mainRegisterId, excelFile, mappedColumns);

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

        private DataExporterByTemplate GetExporter(int mainRegisterId)
        {
            if (mainRegisterId == Consts.RegisterId)
                return new DataExporterByTemplate();

            if (mainRegisterId == OMMainObject.GetRegisterId())
	            return new GbuObjectExporterByTemplate();

			if (mainRegisterId == OMUnit.GetRegisterId())
                return new UnitExporterByTemplate();

            throw new Exception($"Не известный тип экспорта: {mainRegisterId}");
        }

        private static List<DataExportColumn> MapColumns(List<DataColumnDto> columns)
        {
            return columns.Select(x => new DataExportColumn
                { AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList();
        }

        #endregion
    }
}