using System;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.GbuObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Core.ErrorManagment;
using Core.Register;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.Tasks;
using KadOzenka.Web.Models.GbuObject.ObjectAttributes;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Common;
using ObjectModel.Core.TD;
using ObjectModel.Directory.Common;
using ObjectModel.Gbu;
using ObjectModel.Gbu.CodSelection;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class GbuObjectController : KoBaseController
	{
		#region initialization
		private readonly GbuObjectService _service;
		private readonly TaskService _taskService;

		public GbuObjectController(GbuObjectService service, TaskService taskService)
		{
			_service = service;
			_taskService = taskService;
		}
		#endregion

		#region Object Card

		public ActionResult AllDataTree(long objectId)
		{
			return View(objectId);
		}

		public ActionResult TreeList(long objectId, string parentNodeId, long nodeLevel)
		{
			List<AllDataTreeDto> treeList = _service.GetAllDataTree(objectId, parentNodeId, nodeLevel);

			return Content(JsonConvert.SerializeObject(treeList), "application/json");
		}

		public ActionResult AllDetails(long objectId, long? registerId = null, long? attributeId = null)
		{
			List<long> sources = null;

			if (registerId != null)
			{
				sources = new List<long> { registerId.Value };
			}

			List<long> attributes = null;

			if (attributeId != null)
			{
				attributes = new List<long> { attributeId.Value };
			}

			var sttributesValues = _service.GetAllAttributes(objectId, sources, attributes);

			return View(sttributesValues);
		}

		[HttpGet]
		public ActionResult GbuObjectCard(long objectId)
		{
			var obj = OMMainObject.Where(x => x.Id == objectId).SelectAll().ExecuteFirstOrDefault();
			return View(GbuObjectViewModel.FromEntity(obj, DateTime.Now));
		}

		public ActionResult GetGbuObjectAttributes(long objectId, DateTime? actualDate)
		{
			var viewModel = new List<RegisterDto>();

			var mainRegister = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());
			var getSources = RegisterCache.Registers.Values.Where(x => x.QuantTable == mainRegister.QuantTable && x.Id != mainRegister.Id && x.Id != 1).ToList();
			foreach (var source in getSources)
			{
				var objAttributes = _service
					.GetAllAttributes(objectId, new List<long> { source.Id }, null, actualDate ?? DateTime.Now)
					.ToList();
				if (objAttributes.Count > 0)
				{
					viewModel.Add(new RegisterDto(source.Id, objectId, source.Description, objAttributes));
				}
			}

			return PartialView("~/Views/GbuObject/_gbuObjectAttributes.cshtml", viewModel);
		}

	    public ActionResult GetAttributeHistory(long objectId, long registerId, long attrId)
	    {
	        var attributeValues = _service
	            .GetAllAttributes(objectId, new List<long> { registerId }, new List<long> { attrId })
	            .OrderByDescending(x => x.Id)
	            .ToList();
	        var model = new List<AttributeHistoryRecordDto>();
	        foreach (var attributeValue in attributeValues)
	        {
	            model.Add(new AttributeHistoryRecordDto(attributeValue));
	        }

            return View("~/Views/GbuObject/AttributeHistory.cshtml", model);
        }

        #endregion

        #region GroupingObject
        [HttpGet]
		public ActionResult GroupingObject()
		{
			ViewData["CodJob"] = OMCodJob.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Value = x.Id,
				Text = x.NameJob
			}).AsEnumerable();

			ViewData["TreeAttributes"] = _service.GetGbuAttributesTree()
				.Select(x => new DropDownTreeItemModel
				{
					Value = Guid.NewGuid().ToString(),
					Text = x.Text,
					Items = x.Items.Select(y => new DropDownTreeItemModel
					{
						Value = y.Value,
						Text = y.Text
					}).ToList()
				}).AsEnumerable();

		    ViewData["Documents"] = OMInstance.Where(x => x).SelectAll().Execute().Select(x => new
		    {
		        Text = x.Description,
		        Value = x.Id,
		    }).ToList();

            return View(new GroupingObject());
		}

		[HttpPost]
		public JsonResult GroupingObject(GroupingObject model)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			if (model.IsNewAttribute)
			{
				int idAttr = _service.AddNewVirtualAttribute(model.NameNewAttribute, model.RegistryId.GetValueOrDefault(), model.TypeNewAttribute ?? RegisterAttributeType.INTEGER);
				if (idAttr == 0)
				{
					SendErrorMessage("Не корректные данные для создания нового атрибута");
				}

				model.IdAttributeResult = idAttr;
			}

		    if (model.Document.IsNewDocument)
		    {
		        var idDocument = _taskService.CreateDocument(model.Document.NewDocumentRegNumber,
		            model.Document.NewDocumentName, model.Document.NewDocumentDate);
                if (idDocument == 0)
		        {
		            SendErrorMessage("Не корректные данные для создания нового документа");
		        }

		        model.Document.IdDocument = idDocument;
		    }

		    try
			{
				SetPriorityGroupProcess.AddProcessToQueue(model.CovertToGroupingSettings());
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}

		    return Json(new
		    {
		        success = true,
		        idResultAttribute = model.IsNewAttribute ? model.IdAttributeResult : null,
		        idDocument = model.Document.IsNewDocument ? model.Document.IdDocument : null
		    });
		}
		#endregion

		#region Getting Templates
		public List<SelectListItem> GetTemplatesGrouping()
		{
			return OMDataFormStorage.Where(x =>
					x.UserId == SRDSession.GetCurrentUserId().Value && x.FormType_Code == DataFormStorege.Normalisation)
				.SelectAll().Execute().Select(x => new SelectListItem(x.TemplateName ?? "", x.Id.ToString())).ToList();
		}

		public List<SelectListItem> GetTemplatesUnloadingForm()
		{
			return OMDataFormStorage.Where(x =>
					x.UserId == SRDSession.GetCurrentUserId().Value && x.FormType_Code == DataFormStorege.UnloadingFromDict)
				.SelectAll().Execute().Select(x => new SelectListItem(x.TemplateName ?? "", x.Id.ToString())).ToList();
		}
		public List<SelectListItem> GetTemplatesHarmonization()
		{
			return OMDataFormStorage.Where(x =>
					x.UserId == SRDSession.GetCurrentUserId().Value && x.FormType_Code == DataFormStorege.Harmonization)
				.SelectAll().Execute().Select(x => new SelectListItem(x.TemplateName ?? "", x.Id.ToString())).ToList();
		}

		public List<SelectListItem> GetTemplatesHarmonizationCOD()
		{
			return OMDataFormStorage.Where(x =>
					x.UserId == SRDSession.GetCurrentUserId().Value && x.FormType_Code == DataFormStorege.HarmonizationCOD)
				.SelectAll().Execute().Select(x => new SelectListItem(x.TemplateName ?? "", x.Id.ToString())).ToList();
		}


		public JsonResult GetTemplatesOneGroup(int id)
		{
			if (id == 0)
			{
				return Json(new { error = "Ид равен 0" });

			}

			try
			{
				var storage = OMDataFormStorage.Where(x =>
						x.Id == id)
					.SelectAll().ExecuteFirstOrDefault();

				if (storage != null && storage.FormType_Code == DataFormStorege.Normalisation)
				{
					var nObj = storage.Data.DeserializeFromXml<GroupingObject>();
					return Json(new { data = JsonConvert.SerializeObject(nObj) });
				}

				if (storage != null && storage.FormType_Code == DataFormStorege.Harmonization)
				{
					var hObj = storage.Data.DeserializeFromXml<HarmonizationViewModel>();
					return Json(new { data = JsonConvert.SerializeObject(hObj) });
				}

				if (storage != null && storage.FormType_Code == DataFormStorege.HarmonizationCOD)
				{
					var hcObj = storage.Data.DeserializeFromXml<HarmonizationCODViewModel>();
					return Json(new { data = JsonConvert.SerializeObject(hcObj) });
				}

				if (storage != null && storage.FormType_Code == DataFormStorege.UnloadingFromDict)
				{
					var unObj = storage.Data.DeserializeFromXml<UnloadingFromDicViewModel>();
					return Json(new {data = JsonConvert.SerializeObject(unObj) });
				}

			}

			catch (Exception e)
			{
				return Json(new { error = $"Ошибка: {e.Message}" });
			}

			return Json(new { error = "Не найдено соответсвующего типа формы" });
		}

		#endregion

		#region Save Template

		[HttpPost]
		public JsonResult SaveTemplateGroupingObject(string nameTemplate, [FromForm]GroupingObject model)
		{
			return SaveTemplate(nameTemplate, DataFormStorege.Normalisation, model.SerializeToXml());
		}

		[HttpPost]
		public JsonResult SaveTemplateHarmonizationObject(string nameTemplate, [FromForm]HarmonizationViewModel viewModel)
		{
			return SaveTemplate(nameTemplate, DataFormStorege.Harmonization, viewModel.SerializeToXml());
		}

		[HttpPost]
		public JsonResult SaveTemplateHarmonizationCODObject(string nameTemplate, [FromForm]HarmonizationCODViewModel viewModel)
		{
			return SaveTemplate(nameTemplate, DataFormStorege.HarmonizationCOD, viewModel.SerializeToXml());
		}

		[HttpPost]
		public JsonResult SaveTemplateUnloading(string nameTemplate, [FromForm]UnloadingFromDicViewModel viewModel)
		{
			return SaveTemplate(nameTemplate, DataFormStorege.UnloadingFromDict, viewModel.SerializeToXml());
		}

		#endregion

		#region Harmonization

		[HttpGet]
		public ActionResult Harmonization()
		{
			ViewData["TreeAttributes"] = _service.GetGbuAttributesTree()
				.Select(x => new DropDownTreeItemModel
				{
					Value = Guid.NewGuid().ToString(),
					Text = x.Text,
					Items = x.Items.Select(y => new DropDownTreeItemModel
					{
						Value = y.Value,
						Text = y.Text
					}).ToList()
				}).AsEnumerable();

			var viewModel = new HarmonizationViewModel();
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Harmonization(HarmonizationViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			if (viewModel.IsNewAttribute)
			{
				int idAttr = _service.AddNewVirtualAttribute(viewModel.NameNewAttribute, viewModel.RegistryId.GetValueOrDefault(), viewModel.TypeNewAttribute ?? RegisterAttributeType.INTEGER);
				if (idAttr == 0)
				{
					SendErrorMessage("Не корректные данные для создания нового атрибута");
				}

				viewModel.IdAttributeResult = idAttr;
			}

			try
			{
				HarmonizationProcess.AddProcessToQueue(viewModel.ToHarmonizationSettings());
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new { Success = "Процедура Гармонизации успешно добавлена в очередь, по результатам операции будет отправлено сообщение", idResultAttribute = viewModel.IsNewAttribute ? viewModel.IdAttributeResult : null });
		}

		#endregion

		#region HarmonizationCOD
		[HttpGet]
		public ActionResult HarmonizationCOD()
		{
			ViewData["TreeAttributes"] = _service.GetGbuAttributesTree()
				.Select(x => new DropDownTreeItemModel
				{
					Value = Guid.NewGuid().ToString(),
					Text = x.Text,
					Items = x.Items.Select(y => new DropDownTreeItemModel
					{
						Value = y.Value,
						Text = y.Text
					}).ToList()
				}).AsEnumerable();

			ViewData["CodJobs"] = OMCodJob.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Text = x.NameJob,
				Value = x.Id,
			}).ToList();
			ViewData["Documents"] = OMInstance.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Text = x.Description,
				Value = x.Id,
			}).ToList();

			return View(new HarmonizationCODViewModel());
		}

		[HttpPost]
		public ActionResult HarmonizationCOD(HarmonizationCODViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			if (viewModel.IsNewAttribute)
			{
				int idAttr = _service.AddNewVirtualAttribute(viewModel.NameNewAttribute, viewModel.RegistryId.GetValueOrDefault(), viewModel.TypeNewAttribute ?? RegisterAttributeType.INTEGER);
				if (idAttr == 0)
				{
					SendErrorMessage("Не корректные данные для создания нового атрибута");
				}

				viewModel.IdAttributeResult = idAttr;
			}

		    if (viewModel.Document.IsNewDocument)
		    {
		        var idDocument = _taskService.CreateDocument(viewModel.Document.NewDocumentRegNumber,
		            viewModel.Document.NewDocumentName, viewModel.Document.NewDocumentDate);
		        if (idDocument == 0)
		        {
		            SendErrorMessage("Не корректные данные для создания нового документа");
		        }

		        viewModel.Document.IdDocument = idDocument;
		    }

            try
			{
				HarmonizationCodProcess.AddProcessToQueue(viewModel.ToHarmonizationCODSettings());
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

		    return Json(new
		    {
		        Success =
		            "Процедура Гармонизации по классификатору ЦОД успешно добавлена в очередь, по результатам операции будет отправлено сообщение",
		        idResultAttribute = viewModel.IsNewAttribute ? viewModel.IdAttributeResult : null,
		        idDocument = viewModel.Document.IsNewDocument ? viewModel.Document.IdDocument : null
		    });
		}

		#endregion

		#region Unloading From Dict

		[HttpGet]
		public ActionResult UnloadingFromDict()
		{
			ViewData["CodJob"] = OMCodJob.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Value = x.Id,
				Text = x.NameJob
			}).AsEnumerable();

			ViewData["TreeAttributes"] = _service.GetGbuAttributesTree()
				.Select(x => new DropDownTreeItemModel
				{
					Value = Guid.NewGuid().ToString(),
					Text = x.Text,
					Items = x.Items.Select(y => new DropDownTreeItemModel
					{
						Value = y.Value,
						Text = y.Text
					}).ToList()
				}).AsEnumerable();

			ViewData["Documents"] = OMInstance.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Value = x.Id,
				Text = x.Description
			}).AsEnumerable();

			return View(new UnloadingFromDicViewModel());
		}

		[HttpPost]
		public JsonResult UnloadingFromDict(UnloadingFromDicViewModel viewModel)
		{

			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			if (viewModel.IsNewAttribute)
			{
				int idAttr = _service.AddNewVirtualAttribute(viewModel.NameNewAttribute, viewModel.RegistryId.GetValueOrDefault(), viewModel.TypeNewAttribute ?? RegisterAttributeType.INTEGER);
				if (idAttr == 0)
				{
					SendErrorMessage("Не корректные данные для создания нового атрибута");
				}

				viewModel.IdAttributeResult = idAttr;
			}

		    if (viewModel.Document.IsNewDocument)
		    {
		        var idDocument = _taskService.CreateDocument(viewModel.Document.NewDocumentRegNumber,
		            viewModel.Document.NewDocumentName, viewModel.Document.NewDocumentDate);
		        if (idDocument == 0)
		        {
		            SendErrorMessage("Не корректные данные для создания нового документа");
		        }

		        viewModel.Document.IdDocument = idDocument;
		    }

            CodSelection.SelectByCadastralNumber(viewModel.ToCodSelectionSettings());

		    return Json(new
		    {
		        success = "Успешно выполнено",
		        idResultAttribute = viewModel.IsNewAttribute ? viewModel.IdAttributeResult : null,
		        idDocument = viewModel.Document.IsNewDocument ? viewModel.Document.IdDocument : null
		    });
		}

		#endregion

		#region Inheritance

		#region load data

		public JsonResult GetRatingTours()
		{
			var tours = OMTour.Where(x => true).SelectAll().Execute()
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Year.ToString()
				});

			return Json(tours);
		}

		public ActionResult GetRow([FromForm] int rowNumber)
		{
			ViewData["TreeAttributes"] = _service.GetGbuAttributesTree()
				.Select(x => new DropDownTreeItemModel
				{
					Value = Guid.NewGuid().ToString(),
					Text = x.Text,
					Items = x.Items.Select(y => new DropDownTreeItemModel
					{
						Value = y.Value,
						Text = y.Text
					}).ToList()
				}).AsEnumerable();

			ViewData["RowNumber"] = rowNumber.ToString();
			return PartialView("/Views/GbuObject/Partials/PartialNewRow.cshtml", new PartialAttribute());
		}

		#endregion
		[HttpGet]
		public ActionResult Inheritance()
		{
			ViewData["TreeAttributes"] = _service.GetGbuAttributesTree()
				.Select(x => new DropDownTreeItemModel
				{
					Value = Guid.NewGuid().ToString(),
					Text = x.Text,
					Items = x.Items.Select(y => new DropDownTreeItemModel
					{
						Value = y.Value,
						Text = y.Text
					}).ToList()
				}).AsEnumerable();

			long[] arr = new long[5];
			return View(new InheritanceViewModel{Attributes = new List<long>(arr.ToList())});
		}

		[HttpPost]
		public JsonResult Inheritance(InheritanceViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			try
			{
				GbuObjectInheritanceAttribute.Run(viewModel.ToAttributeSettings());
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}
			

			return Json(new { Success = "Выполнено успешно!"});
		}

		#endregion
		public List<SelectListItem> GetTasksData()
		{
			var documentInfoList = _taskService.GetTaskDocumentInfoList().OrderByDescending(x => x.DocumentCreateDate);
			return documentInfoList
				.Select(x => new SelectListItem(_taskService.GetTemplateForTaskName(x), x.TaskId.ToString()))
				.ToList();
		}

		#region Helper

		public IEnumerable<SelectListItem> GetAllGbuRegisters()
		{
			return RegisterCache.Registers.Values.Where(x => _service.GetGbuRegistersIds().Contains(x.Id)).Select(x => new SelectListItem(x.Description, x.Id.ToString()));
		}

		#endregion
	}
}
