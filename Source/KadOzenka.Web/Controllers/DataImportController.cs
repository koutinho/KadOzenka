using System;
using System.Collections.Generic;
using System.Linq;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GemBox.Spreadsheet;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using Core.Main.FileStorages;
using Core.ErrorManagment;
using System.IO;
using Core.SRD;
using ObjectModel.KO;
using KadOzenka.Web.Models.Task;
using ObjectModel.Core.TD;

namespace KadOzenka.Web.Controllers
{
	public class DataImportController : BaseController
	{
		private readonly int _dataCountForBackgroundLoading = 1000;

		[HttpGet]
		public IActionResult DataImport(string registerViewId, long? mainRegisterId)
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

				return Content(JsonConvert.SerializeObject(new {DataCount = dataRowCount, ColumnsNames = columnsNames}),
					"application/json");
			}
			catch (Exception ex)
			{
				return Content(ex.Message);
			}
		}

		[HttpPost]
		public IActionResult ImportDataFromExcel(int mainRegisterId, IFormFile file, List<DataColumnDto> columns)
		{
			if (columns.All(x => x.IsKey == false))
			{
				throw new Exception("Должен быть выбран хотя бы один ключевой параметр");
			}

			ExcelFile excelFile;
			using (var stream = file.OpenReadStream())
			{
				excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
			}

			DataImporterCommon.ImportDataFromExcel(mainRegisterId, excelFile, columns.Select(x => new DataExportColumn
				{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList());

			return NoContent();
		}

		[HttpPost]
		public IActionResult AddImportToQueue(int mainRegisterId, string registerViewId, IFormFile file, List<DataColumnDto> columns)
		{
			if (columns.All(x => x.IsKey == false))
			{
				throw new Exception("Должен быть выбран хотя бы один ключевой параметр");
			}

			using (var stream = file.OpenReadStream())
			{
				DataImporterCommon.AddImportToQueue(mainRegisterId, registerViewId, file.FileName, stream,
					columns.Select(x => new DataExportColumn
						{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList());
			}

			return NoContent();
		}

		[HttpGet]
		public ActionResult ImportGkn()
		{
			TaskModel dto = new TaskModel();
			return View(dto);
		}

		[HttpPost]
		public ActionResult ImportGkn(List<IFormFile> files, TaskModel dto)
		{
			//SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_IMPORT, true, false, true);

			OMInstance instance = new OMInstance
			{
				RegNumber = dto.IncomingDocumentRegNumber,
				Description = dto.IncomingDocumentDescription,
				CreateDate = dto.IncomingDocumentDate ?? DateTime.Now
			};
			instance.Save();

			OMTask task = new OMTask
			{
				TourId = dto.TourYear,
				DocumentId = instance.Id,
				CreationDate = DateTime.Now,
				EstimationDate = dto.EstimationDate,
				NoteType_Code = dto.NoteType ?? ObjectModel.Directory.KoNoteType.None,
				Status_Code = ObjectModel.Directory.KoTaskStatus.InWork
			};
			task.Save();

			try
			{
				foreach (var file in files)
				{
					using (var stream = file.OpenReadStream())
					{
						DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", file.FileName, stream, OMTask.GetRegisterId(), task.Id);
					}
				}
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			string Msg = "Задание на оценку успешно создано. ";
			if (files.Any())
			{
				Msg += "Загрузка добавлена в очередь, по результатам загрузки будет отправлено сообщение";
			}

			return Json(new { Msg });
		}

		[HttpPost]
		public ActionResult ImportGknFromTask(List<IFormFile> files, long taskId)
		{
			try
			{
				foreach (var file in files)
				{
					using (var stream = file.OpenReadStream())
					{
						DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", file.FileName, stream, OMTask.GetRegisterId(), taskId);
					}
				}
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return NoContent();
		}
	}
}
