using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Tasks;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class GknDataImportController : KoBaseController
	{
		public TaskService TaskService { get; set; }

		public GknDataImportController(TaskService taskService)
		{
			TaskService = taskService;
		}


		[HttpGet]
		[SRDFunction(Tag = "")]
		public ActionResult ImportGkn()
		{
			TaskModel dto = new TaskModel();
			return View(dto);
		}

		[HttpPost]
		[RequestSizeLimit(2000000000)]
		[SRDFunction(Tag = "")]
		public ActionResult ImportGkn(List<IFormFile> files, TaskModel dto)
		{
			//SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_IMPORT, true, false, true);

			var documentId = TaskService.CreateDocument(dto.IncomingDocumentRegNumber, dto.IncomingDocumentDescription,
				dto.IncomingDocumentDate);

			OMTask task = new OMTask
			{
				TourId = dto.TourYear,
				DocumentId = documentId,
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
		[RequestSizeLimit(2000000000)]
		[SRDFunction(Tag = "")]
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

		[HttpGet]
		[ActionName("RestartGknImports")]
		[SRDFunction(Tag = "")]
		public ActionResult RestartGknImportsConfirm(long? importId)
		{
			var currentImportsId = RegistersVariables.CurrentList?.ToList();
			if (currentImportsId == null || currentImportsId.Count == 0)
			{
				currentImportsId = importId.HasValue
					? new List<long> { importId.Value }
					: new List<long>();
			}

			if (currentImportsId.Count == 0)
			{
				return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
				{
					Message = "Не выбраны записи для перезапуска.",
					Icon = ModalDialogDetails.IconType.Warning,
					Buttons = ModalDialogDetails.ButtonType.Ok,
					Action = ModalDialogDetails.ActionType.Reload
				});
			}

			var addedOrRunningSelectedImports = OMImportDataLog
				.Where(x => currentImportsId.Contains(x.Id) && (x.Status_Code == ImportStatus.Added || x.Status_Code == ImportStatus.Running))
				.Select(x => x.Status)
				.Select(x => x.Status_Code)
				.Execute();
			if (addedOrRunningSelectedImports.Count > 0)
			{
				return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
				{
					Message = $"Выбраны записи со статусом '{ImportStatus.Added.GetEnumDescription()}' или '{ImportStatus.Running.GetEnumDescription()}'",
					Icon = ModalDialogDetails.IconType.Warning,
					Buttons = ModalDialogDetails.ButtonType.Ok,
					Action = ModalDialogDetails.ActionType.Reload
				});
			}

			var model = new ModalDialogDetails
			{
				Message = "Вы уверены, что хотите перезапустить выбранные записи импорта?",
			};

			return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
		}

		[HttpPost]
		[ActionName("RestartGknImports")]
		[SRDFunction(Tag = "")]
		public ActionResult RestartGknImports(long? importId)
		{
			var currentImportsId = RegistersVariables.CurrentList?.ToList();
			if (currentImportsId == null || currentImportsId.Count == 0)
			{
				currentImportsId = importId.HasValue
					? new List<long> { importId.Value }
					: new List<long>();
			}
			try
			{
				if (currentImportsId.Count == 0)
				{
					throw new Exception("Не выбраны записи для перезапуска.");
				}

				using (var ts = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					foreach (var id in currentImportsId)
					{
						DataImporterGknLongProcess.RestartImport(id);
					}

					ts.Complete();
				}
			}
			catch (Exception e)
			{
				long errorId = ErrorManager.LogError(e);
				return Json(new
				{
					type = "Error",
					message = $"{e.Message} (Подробнее в журнале № {errorId})"
				});
			}

			return Json(new
			{
				type = "Success",
				message = "Выбранные записи импорта успешно перезапущены",
				reload = true
			});
		}
	}
}
