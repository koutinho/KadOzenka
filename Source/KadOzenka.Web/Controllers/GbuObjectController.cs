using Core.UI.Registers.Controllers;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.GbuObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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

			var sttributesValues = _service.GetAllAttributes(objectId, sources);

			return View(sttributesValues);
		}
	}
}
