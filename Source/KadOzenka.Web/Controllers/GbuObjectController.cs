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
using Core.Shared.Extensions;
using Core.SRD;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using ObjectModel.Directory.Common;
using ObjectModel.Gbu.GroupingAlgoritm;
using ObjectModel.KO;
using ObjectModel.Gbu.Harmonization;

namespace KadOzenka.Web.Controllers
{
	public class GbuObjectController : BaseController
	{
		private readonly GbuObjectService _service;

		public GbuObjectController(GbuObjectService service)
		{
			_service = service;
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
				sources = new List<long> {registerId.Value};
			}

			List<long> attributes = null;

			if (attributeId != null)
			{
				attributes = new List<long> {attributeId.Value};
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

				return View();
		}

		public List<SelectListItem> GetTemplatesGrouping()
		{
			return OMDataFormStorage.Where(x =>
					x.UserId == SRDSession.GetCurrentUserId().Value && x.FormType_Code == DataFormStorege.Normalisation)
				.SelectAll().Execute().Select(x => new SelectListItem(x.TemplateName ?? "", x.Id.ToString())).ToList();
		}

		public JsonResult GetTemplatesOneGroup(int id)
		{
			if (id == 0)
			{
				return Json(new {error = "Ид равен 0"});

			}

			GroupingObject obj;

			try
			{
				var tmp = OMDataFormStorage.Where(x =>
						x.Id == id)
					.SelectAll().ExecuteFirstOrDefault();

				obj = OMDataFormStorage.Where(x =>
						x.Id == id)
					.SelectAll().ExecuteFirstOrDefault().Data.DeserializeFromXml<GroupingObject>();
			}

			catch (Exception e)
			{
				return Json(new {error = $"Ошибка: {e.Message}"});
			}

			if (obj == null)
			{
				return Json(new { error = "Данные об объекте не найдены" });
			}

			return Json(new
			{
				data = JsonConvert.SerializeObject(obj)

			});
		}

		public List<SelectListItem> GetTemplatesHarmonization()
		{
			return OMDataFormStorage.Where(x =>
					x.UserId == SRDSession.GetCurrentUserId().Value && x.FormType_Code == DataFormStorege.Harmonization)
				.SelectAll().Execute().Select(x => new SelectListItem(x.TemplateName ?? "", x.Id.ToString())).ToList();
		}


		[HttpPost]
		public JsonResult GroupingObject(GroupingObject model)
		{
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

		[HttpGet]
		public ActionResult Harmonization()
		{
			ViewData["Attributes"] = OMAttribute.Where(x => x.RegisterId >= 2 && x.RegisterId <= 23)
				.Select(x => new { x.Name, x.Id })
				.Execute()
				.Select(x => new { Text = x.Name, Value = x.Id })
				.ToList();

			var viewModel = new HarmonizationViewModel();
			var formStorage = OMDataFormStorage
				.Where(x => x.UserId == SRDSession.GetCurrentUserId().Value &&
				            x.FormType_Code == DataFormStorege.Harmonization).SelectAll().ExecuteFirstOrDefault();
			if (formStorage != null)
			{
				viewModel = formStorage.Data.DeserializeFromXml<HarmonizationViewModel>();
			}

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

			var formStorage = OMDataFormStorage
				.Where(x => x.UserId == SRDSession.GetCurrentUserId().Value &&
				            x.FormType_Code == DataFormStorege.Harmonization).SelectAll().ExecuteFirstOrDefault();
			try
			{
				if (formStorage != null)
				{
					formStorage.Data = viewModel.SerializeToXml();
				}
				else
				{
					formStorage = new OMDataFormStorage()
					{
						UserId = SRDSession.GetCurrentUserId().Value,
						FormType_Code = DataFormStorege.Harmonization,
						Data = viewModel.SerializeToXml()

					};
				}
				formStorage.Save();

				ObjectModel.Gbu.Harmonization.Harmonization.Run(viewModel.ToHarmonizationSettings());
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new { Success = "Процедура Гармонизации успешно выполнена" });
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
