using System;
using Core.UI.Registers.Controllers;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.GbuObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using Core.SRD;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using ObjectModel.Directory.Common;
using ObjectModel.Gbu.GroupingAlgoritm;
using ObjectModel.KO;

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

		[HttpPost]
		public JsonResult GroupingObject(GroupingObject model)
		{
			PriorityGrouping.SetPriorityGroup(model.CovertToGroupingSettings());
			return Json(new { success = true });
		}

		[HttpPost]
		public JsonResult SaveTemplateGroupingObject(GroupingObject model)
		{
		
			try
			{
				OMDataFormStorage formStorage = OMDataFormStorage
					.Where(x => x.UserId == SRDSession.GetCurrentUserId().Value &&
					            x.FormType_Code == DataFormStorege.Normalisation).SelectAll().ExecuteFirstOrDefault();
				if (formStorage != null)
				{
					formStorage.Data = model.SerializeToXml();
					formStorage.Save();
					return Json(new { success = true });
				}

				new OMDataFormStorage()
				{
					UserId = SRDSession.GetCurrentUserId().Value,
					FormType_Code = DataFormStorege.Normalisation,
					Data = model.SerializeToXml()

				}.Save();
			}
			catch (Exception e)
			{
				return Json(new { Error = $"Сохранение не выполнено. Подробности в журнале ошибок. Ошибка: {e.Message}" });
			}
			
			return Json(new { success = true });
		}
	}
}
