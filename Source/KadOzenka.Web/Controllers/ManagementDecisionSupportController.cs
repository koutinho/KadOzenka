using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Directory;

namespace KadOzenka.Web.Controllers
{
	public class ManagementDecisionSupportController : KoBaseController
	{
		private readonly ManagementDecisionSupportService _service;

		public ManagementDecisionSupportController(ManagementDecisionSupportService service)
		{
			_service = service;
		}

		public ActionResult Map()
		{
			var exceptions = new List<long> { (long)PropertyTypes.None };
			var segments = Helpers.EnumExtensions.GetSelectList(typeof(PropertyTypes), exceptions: exceptions);
			ViewBag.Segments = segments;

			return View();
		}

		public JsonResult HeatMapData(long toutId, PropertyTypes objectType, DivisionType divisionType, string colors)
		{
			var result = _service.GetHeatMapData(toutId, objectType, divisionType, colors.Split(","));
			return Json(result);
		}

	}
}
