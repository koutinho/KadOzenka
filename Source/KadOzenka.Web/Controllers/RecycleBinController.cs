﻿using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.LongProcess.RecycleBin;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.RecycleBin;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Web.Controllers
{
	public class RecycleBinController : KoBaseController
	{
		public RecycleBinService RecycleBinService { get; }

		public RecycleBinController(RecycleBinService recycleBinService)
		{
			RecycleBinService = recycleBinService;
		}

		[HttpGet]
		[SRDFunction(Tag = "ADMIN")]
		public IActionResult RestoreRecycleBinRecord(long id)
		{
			var record = RecycleBinService.GetRecycleBinRecord(id);
			return View(RecycleBinModel.FromDto(record));
		}

		[HttpPost]
		[SRDFunction(Tag = "ADMIN")]
		public IActionResult Restore(RecycleBinModel model)
		{
			string responseText;
			if (RecycleBinService.ShouldUseLongProcessForRestoringObject(model.ObjectRegisterId))
			{
				RestoreObjectFromRecycleBinLongProcess.AddProcessToQueue(model.ToSettings());
				responseText = "Запрос на восстановление данных добавлен в очередь";
			}
			else
			{
				RecycleBinService.RestoreObject(model.Id);
				responseText = "Данные восстановлены";
			}

			return Ok(responseText);
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.CORE_LONG_PROCESS_PARAM_EDIT)]
		public ActionResult FlushOldDataFromRecycleBinLongProcessParams(long processTypeId)
		{
			OMProcessType processType = OMProcessType.Where(w => w.Id == processTypeId).SelectAll().ExecuteFirstOrDefault();

			var userConfig = processType.Parameters.DeserializeFromXml<FlushOldDataFromRecycleBinLongProcessParams>() ?? new FlushOldDataFromRecycleBinLongProcessParams();
			ViewBag.processTypeId = processTypeId;

			return View(userConfig);
		}
	}
}
