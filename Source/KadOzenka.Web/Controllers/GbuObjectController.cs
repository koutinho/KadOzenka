using Core.UI.Registers.Controllers;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.GbuObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Core.Register;

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

			if(registerId != null)
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
			return View();
		}

		[HttpGet]
		public ActionResult Harmonization()
		{
			ViewData["Attributes"] = OMAttribute.Where(x => x.RegisterId >= 2 && x.RegisterId <= 23)
				.Select(x => new { x.Name, x.Id })
				.Execute()
				.Select(x => new { Text = x.Name, Value = x.Id })
				.ToList();
		
			return View(new HarmonizationViewModel{ IdAttributeResult = 8 });
		}

		[HttpPost]
		public ActionResult Harmonization(HarmonizationViewModel viewModel)
		{
			return NoContent();
		}
	}
}
