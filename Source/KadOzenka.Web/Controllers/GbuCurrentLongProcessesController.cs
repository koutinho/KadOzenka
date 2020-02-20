﻿using System;
using System.Linq;
using Core.UI.Registers.Controllers;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.GbuProgressBar;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KadOzenka.Web.Controllers
{
	public class GbuCurrentLongProcessesController : BaseController
	{
		private readonly GbuLongProcessesService _service;

		public GbuCurrentLongProcessesController(GbuLongProcessesService service)
		{
			_service = service ?? throw new ArgumentNullException(nameof(service));
		}

		[HttpGet]
		public ActionResult CurrentLongProcessesList()
		{
			var processesList = _service.GetCurrentLongProcessesList();
			return Content(JsonConvert.SerializeObject(processesList.Select(LongProcessViewModel.ToModel).ToList()), "application/json");
		}
	}
}
