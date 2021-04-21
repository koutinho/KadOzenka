using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.Tasks;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.KO;
using Platform.Web.Services;
using RsmCloudService.Web.Models.CoreAttachment;

namespace KadOzenka.Web.Controllers
{
	public class GknDataImportController : KoBaseController
	{
		public TaskService TaskService { get; set; }
		public DocumentService DocumentService { get; set; }

		public GknDataImportController(TaskService taskService)
		{
			TaskService = taskService;
            DocumentService = new DocumentService();
        }


		[HttpGet]
		[SRDFunction(Tag = "")]
		public ActionResult ImportGkn()
		{
			var model = new TaskCreationModel();

            ViewData["Documents"] = GetDocumentsForPartialView();

            return View(model);
		}

		[HttpPost]
		[RequestSizeLimit(2000000000)]
		[SRDFunction(Tag = "")]
		public ActionResult ImportGkn(TaskCreationModel dto, List<IFormFile> images)
		{
			//внутри TaskCreationModel есть модель для документа, поэтому ModelState.IsValid использовать нельзя
			dto.Validate();

			dto.Document.ProcessDocument();

            if (dto.Document.IdDocument.GetValueOrDefault() == 0)
                throw new Exception("Не выбран документ");

            var taskId = new OMTask
			{
				TourId = dto.TourId,
				DocumentId = dto.Document.IdDocument,
				CreationDate = DateTime.Now,
				EstimationDate = dto.EstimationDate,
				NoteType_Code = dto.NoteType ?? ObjectModel.Directory.KoNoteType.None,
				Status_Code = ObjectModel.Directory.KoTaskStatus.InWork
			}.Save();

			try
			{
				var attSvc = new CoreAttachmentService();
				var attDto = new AttachmentUploadDto();
				attDto.AttachmentRegisterId = 203;
				attDto.AttachmentObjectId = taskId;
				attSvc.AttachmentUpload(attDto,images.ToArray());

				ProcessDocuments(dto, taskId);
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			string Msg = "Задание на оценку успешно создано. ";
			if (dto.XmlFiles.Any())
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
						DataImporterGknLongProcess.AddImportToQueue(file.FileName, stream, taskId);
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


		#region Support Methods

		private void ProcessDocuments(TaskCreationModel dto, long taskId)
		{
			if (dto.DocumentType == DocumentType.Xml)
			{
				foreach (var file in dto.XmlFiles)
				{
					using (var stream = file.OpenReadStream())
					{
						DataImporterGknLongProcess.AddImportToQueue(file.FileName, stream, taskId);
					}
				}
			}
			else
			{
				using (var stream = dto.ExcelFile.OpenReadStream())
				{
					var attributes = dto.ExcelColumnsMapping.Select(x => new DataExportColumn
					{
						AttributrId = x.AttributeId, 
						ColumnName = x.ColumnName
					}).ToList();

					DataImporterGknLongProcess.AddImportToQueue(dto.ExcelFile.FileName, stream, taskId, attributes);
				}
			}
		}

		#endregion
	}
}
