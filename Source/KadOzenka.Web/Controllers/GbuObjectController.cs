using System;
using Core.UI.Registers.Controllers;
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
using KadOzenka.Dal.Tasks;
using KadOzenka.Web.Models.GbuObject.ObjectAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using ObjectModel.Core.TD;
using ObjectModel.Directory.Common;
using ObjectModel.Gbu;
using ObjectModel.Gbu.CodSelection;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class GbuObjectController : BaseController
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
					.Where(x =>
						x.NumValue.HasValue || x.DtValue.HasValue || !string.IsNullOrEmpty(x.StringValue)).ToList();
				if (objAttributes.Count > 0)
				{
					viewModel.Add(new RegisterDto(source.Id, source.Description, objAttributes));
				}
			}

			return PartialView("~/Views/GbuObject/_gbuObjectAttributes.cshtml", viewModel);
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

			ViewData["Attributes"] = _service.GetGbuAttributes()
				.Select(x => new
				{
					Value = x.Id,
					Text = x.Name
				}).AsEnumerable();

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

			try
			{
				PriorityGrouping.SetPriorityGroup(model.CovertToGroupingSettings());
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}

			return Json(new { success = true , idResultAttribute = model.IsNewAttribute ? model.IdAttributeResult : null });
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
			ViewData["Attributes"] = OMAttribute.Where(x => x.RegisterId >= 2 && x.RegisterId <= 23)
				.Select(x => new { x.Name, x.Id })
				.Execute()
				.Select(x => new { Text = x.Name, Value = x.Id })
				.ToList();

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
				KadOzenka.Dal.GbuObject.Harmonization.Run(viewModel.ToHarmonizationSettings());
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new { Success = "Процедура Гармонизации успешно выполнена", idResultAttribute = viewModel.IsNewAttribute ? viewModel.IdAttributeResult : null });
		}

		#endregion

		#region HarmonizationCOD
		[HttpGet]
		public ActionResult HarmonizationCOD()
		{
			ViewData["Attributes"] = OMAttribute.Where(x => x.RegisterId >= 2 && x.RegisterId <= 23)
				.Select(x => new { x.Name, x.Id })
				.Execute()
				.Select(x => new { Text = x.Name, Value = x.Id })
				.ToList();
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

			try
			{
				KadOzenka.Dal.GbuObject.HarmonizationCOD.Run(viewModel.ToHarmonizationCODSettings());
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new { Success = "Процедура Гармонизации по классификатору ЦОД успешно выполнена", idResultAttribute = viewModel.IsNewAttribute ? viewModel.IdAttributeResult : null });
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

			ViewData["Attributes"] = _service.GetGbuAttributes()
				.Select(x => new
				{
					Value = x.Id,
					Text = x.Name
				}).AsEnumerable();

			ViewData["Document"] = OMInstance.Where(x => x).SelectAll().Execute().Select(x => new
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

			CodSelection.SelectByCadastralNumber(viewModel.ToCodSelectionSettings());

			return Json(new {success = "Успешно выполнено", idResultAttribute = viewModel.IsNewAttribute ? viewModel.IdAttributeResult : null });
		}

		#endregion
		public List<SelectListItem> GetTasksData()
		{
			var documentInfoList = _taskService.GetTaskDocumentInfoList();
			return documentInfoList
				.Select(x => new SelectListItem(x.DocumentRegNumber, x.TaskId.ToString()))
				.ToList();
		}

		#region Helper

		public JsonResult SaveTemplate(string nameTemplate, DataFormStorege formType, string serializeData)
		{
			if (string.IsNullOrEmpty(nameTemplate))
			{
				return Json(new { Error = "Сохранение не выполнено. Имя шаблона обязательное поле" });
			}

			try
			{
				new OMDataFormStorage()
				{
					UserId = SRDSession.GetCurrentUserId().Value,
					FormType_Code = formType,
					Data = serializeData,
					TemplateName = nameTemplate,

				}.Save();
			}
			catch (Exception e)
			{
				return Json(new { Error = $"Сохранение не выполнено. Подробности в журнале ошибок. Ошибка: {e.Message}" });
			}

			return Json(new { success = true });
		}

		public IEnumerable<SelectListItem> GetAllGbuRegisters()
		{
			return RegisterCache.Registers.Values.Where(x => x.Id > 2 && x.Id < 23).Select(x => new SelectListItem(x.Description, x.Id.ToString()));
		}

		public JsonResult SendErrorMessage(string errorMessage)
		{
			return Json(new
			{
				Errors = new
				{
					Control = 0,
					Message = errorMessage
				}
			});
		}

		public JsonResult GenerateMessageNonValidModel()
		{
			return Json(new
				{
					Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
					{
						Control = x.Key,
						Message = string.Join("\n", x.Value.Errors.Select(e =>
						{
							if (e.ErrorMessage == "The value '' is invalid.")
							{
								return $"{e.ErrorMessage} Поле {x.Key}";
							}

							return e.ErrorMessage;
						}))
					})
				});

		}

		#endregion
	}
}
