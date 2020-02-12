using System;
using Core.UI.Registers.Controllers;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.GbuObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.ErrorManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using KadOzenka.Dal.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using ObjectModel.Core.TD;
using ObjectModel.Directory.Common;
using ObjectModel.Gbu.GroupingAlgoritm;
using ObjectModel.KO;
using ObjectModel.Gbu.Harmonization;

namespace KadOzenka.Web.Controllers
{
	public class GbuObjectController : BaseController
	{
		private readonly GbuObjectService _service;
		private readonly TaskService _taskService;

		public GbuObjectController(GbuObjectService service, TaskService taskService)
		{
			_service = service;
			_taskService = taskService;
		}

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
		public ActionResult GroupingObject()
		{
			ViewData["CodJob"] = OMCodJob.Where(x => x).SelectAll().Execute().Select(x => new
			{
				x.Id,
				Text = x.NameJob
			}).AsEnumerable();

			ViewData["Attribute"] = OMAttribute.Where(x => x.RegisterId >= 2 && x.RegisterId <= 23).SelectAll()
				.Execute().Select(x => new
				{
					x.Id,
					Text = x.Name
				}).AsEnumerable();

			return View(new GroupingObject());
		}

		public List<SelectListItem> GetTemplatesGrouping()
		{
			return OMDataFormStorage.Where(x =>
					x.UserId == SRDSession.GetCurrentUserId().Value && x.FormType_Code == DataFormStorege.Normalisation)
				.SelectAll().Execute().Select(x => new SelectListItem(x.TemplateName ?? "", x.Id.ToString())).ToList();
		}

		public List<SelectListItem> GetTasksData()
		{
			var documentInfoList =_taskService.GetTaskDocumentInfoList();
			return documentInfoList
				.Select(x => new SelectListItem(x.DocumentRegNumber, x.TaskId.ToString()))
				.ToList();
		}

		public JsonResult GetTemplatesOneGroup(int id)
		{
			if (id == 0)
			{
				return Json(new { error = "Ид равен 0" });

			}

			GroupingObject nObj = null;
			HarmonizationViewModel hObj = null;
			HarmonizationCODViewModel hcObj = null;
			try
			{
				var storage = OMDataFormStorage.Where(x =>
						x.Id == id)
					.SelectAll().ExecuteFirstOrDefault();
				if (storage != null && storage.FormType_Code == DataFormStorege.Normalisation)
				{
					nObj = storage.Data.DeserializeFromXml<GroupingObject>();

				}

				if (storage != null && storage.FormType_Code == DataFormStorege.Harmonization)
				{
					hObj = storage.Data.DeserializeFromXml<HarmonizationViewModel>();
				}

				if (storage != null && storage.FormType_Code == DataFormStorege.HarmonizationCOD)
				{
					hcObj = storage.Data.DeserializeFromXml<HarmonizationCODViewModel>();
				}

			}

			catch (Exception e)
			{
				return Json(new { error = $"Ошибка: {e.Message}" });
			}

			return Json(new
			{
				data = nObj != null ?
					JsonConvert.SerializeObject(nObj) :
					hObj != null ?
						JsonConvert.SerializeObject(hObj) : JsonConvert.SerializeObject(hcObj)
			});
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


		[HttpPost]
		public JsonResult GroupingObject(GroupingObject model)
		{
			if (!ModelState.IsValid)
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
			List<string> errorsList = new List<string>();
			if (model.IsDataActualUsed && model.DataActual.IsNullOrDbNull())
			{
				errorsList.Add("Дата актулизации обязательна");
			}
			PriorityGrouping.SetPriorityGroup(model.CovertToGroupingSettings());
			return Json(new { success = true });
		}

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

			try
			{
				KadOzenka.Dal.GbuObject.Harmonization.Run(viewModel.ToHarmonizationSettings());
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new { Success = "Процедура Гармонизации успешно выполнена" });
		}

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

			try
			{
				KadOzenka.Dal.GbuObject.HarmonizationCOD.Run(viewModel.ToHarmonizationCODSettings());
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new { Success = "Процедура Гармонизации по классификатору ЦОД успешно выполнена" });
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

		#endregion
	}
}
