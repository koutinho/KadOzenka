﻿using System;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Market;
using KadOzenka.Web.Models.MarketObject;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Web.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using ObjectModel.Directory;
using ObjectModel.Directory.MarketObjects;
using ObjectModel.SRD;

namespace KadOzenka.Web.Controllers
{
    public class MarketObjectsController : KoBaseController
	{
        public CorrectionByDateService CorrectionByDateService { get; set; }
        public CorrectionByRoomService CorrectionByRoomService { get; set; }
		public CorrectionByStageService CorrectionByStageService { get; set; }
        public CorrectionForFirstFloorService CorrectionForFirstFloorService { get; set; }
        public CorrectionSettingsService CorrectionSettingsService { get; set; }

		public MarketObjectsController()
        {
            CorrectionByDateService = new CorrectionByDateService();
            CorrectionByRoomService = new CorrectionByRoomService();
			CorrectionByStageService = new CorrectionByStageService();
            CorrectionForFirstFloorService = new CorrectionForFirstFloorService();
            CorrectionSettingsService = new CorrectionSettingsService();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        public IActionResult ObjectCard(long id)
		{
			var analogItem = OMCoreObject.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (analogItem != null)
            {
                var screenList = OMScreenshots.Where(x => x.InitialId == id).SelectAll().Execute().Select(x => (x.Id, x.CreationDate)).ToList();
                if(screenList.IsNotEmpty()) ViewBag.ScreenShots = screenList;
	            analogItem.PriceHistory = ObjectModel.Market.OMPriceHistory.Where(x => x.InitialId == id).SelectAll().Execute();
            }
            return View(CoreObjectDto.OMMap(analogItem));
		}

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        public FileResult ShowFile(long id)
        {
            var screen = OMScreenshots.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            var fileStream = FileStorageManager.GetFileStream("MarketObjectScreenShot", screen.CreationDate.Value, id.ToString());
            return File(fileStream, screen.Type, id.ToString());
        }

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        public ActionResult GetChartData(long? id)
		{
			if (!id.HasValue) throw new Exception("Не передан идентификатор объекта.");
			var priceHistories = ObjectModel.Market.OMPriceHistory.Where(x => x.InitialId == id.Value).SelectAll().Execute();
			var screenshots = OMScreenshots.Where(x => x.InitialId == id.Value).SelectAll().Execute().ToList();
			return Content(JsonConvert.SerializeObject(PriceHistoryChartModel.FromEntities(priceHistories, screenshots)), "application/json");
		}

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_UNLOAD_SCREENSHOTS)]
        public FileResult UnloadScreenshots(long objectId)
		{
			List<long?> ids = RegistersVariables.CurrentList != null && RegistersVariables.CurrentList.Count > 0 ? RegistersVariables.CurrentList?.Cast<long?>()?.ToList() : new List<long?> { objectId };
			var screenList = OMScreenshots.Where(x => ids.Contains(x.InitialId)).SelectAll().Execute().ToList();
			List<OMCoreObject> analogItems = OMCoreObject.Where(x => ids.Contains(x.Id)).Select(x => x.CadastralNumber).Execute().ToList();
			using (MemoryStream zipStream = new MemoryStream())
			{
				using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
				{
					int n = 1;
					foreach (OMScreenshots screenshot in screenList)
					{
						OMCoreObject analogItem = analogItems.Where(x => x.Id == screenshot.InitialId).FirstOrDefault();
						if (analogItem == null) continue;
						FileStream fileStream = FileStorageManager.GetFileStream("MarketObjectScreenShot", screenshot.CreationDate.Value, screenshot.Id.ToString());
						string folderName = !analogItem.CadastralNumber.IsNullOrEmpty() ? analogItem.CadastralNumber : analogItem.Id.ToString();
						string entryName = $"Скриншот_{n++}_{screenshot.CreationDate?.ToShortDateString().Replace(".", "")}";
						string ext = screenshot.Type.Replace("image/", ".");
						entryName = folderName + "/" + entryName + ext;
						ZipArchiveEntry zipEntry = zip.CreateEntry(entryName);
						using (Stream sWriter = zipEntry.Open())
						{							
							byte[] bytes = new byte[fileStream.Length];							
							int count = fileStream.Read(bytes);
							sWriter.Write(bytes);
						}
					}
				}
				int objCount = (ids?.Count == 0) ? 1 : ids.Count;
				return File(zipStream.ToArray(), "application/zip",	$"Скриншоты по объектам ({objCount})" + ".zip");				
			}
		}

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_ACTIVATE_PROCESS)]
        public ActionResult ActivateProcess()
        {
            if (AddressesDetector.IsProcessAdditable())
            {
                AddressesDetector.AddProcessToQueue();
                return Content("Присвоение зон, районов, округов");
            }
            else return Content("Процесс уже добавлен в очередь или выполняется");
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_ACTIVATE_COORDINATES)]
        public ActionResult ActivateCoordinates()
        {
            if (CoordinatesJoiner.IsProcessAdditable())
            {
                CoordinatesJoiner.AddProcessToQueue();
                return Content("Присвоение координат");
            }
            else return Content("Процесс уже добавлен в очередь или выполняется");
        }

        #region Correction By Date

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public ActionResult CorrectionByDateGeneralCoefficients()
        {
            var exceptions = new List<long> { (long)MarketSegment.None, (long)MarketSegment.NoSegment };

            var segments = Helpers.EnumExtensions.GetSelectList(typeof(MarketSegment), exceptions: exceptions);

            ViewBag.Segments = segments;

            return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public JsonResult GetCorrectionByDateGeneralCoefficients(long marketSegmentCode)
        {
            var history = CorrectionByDateService.GetAverageCoefficientsBySegments(marketSegmentCode);
            return Json(history.Select(x => CorrectionByDateModel.Map(x, CorrectionByDateService.IsCoefIncludedInCalculationLimit)).ToList());
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public ActionResult CorrectionByDateDetailedCoefficients(long marketSegmentCode, DateTime date)
        {
            var marketSegment = (MarketSegment)marketSegmentCode;

            ViewBag.Date = date;
            ViewBag.MarketSegmentCode = marketSegmentCode;
            ViewBag.MarketSegment = marketSegment.GetEnumDescription();
            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByDate);
            if (settings.LowerLimitForCoefficient.HasValue)
            {
                ViewBag.LowerLimitForCoefficient = settings.LowerLimitForCoefficient;
            }
            if (settings.UpperLimitForCoefficient.HasValue)
            {
                ViewBag.UpperLimitForCoefficient = settings.UpperLimitForCoefficient;
            }

            return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public JsonResult GetCorrectionByDateDetailedCoefficients(long marketSegmentCode, DateTime date)
        {
            var historyRecords = CorrectionByDateService.GetDetailedCoefficients(marketSegmentCode, date);
            var models = historyRecords.Select(x => CorrectionByDateModel.Map(x, CorrectionByDateService.IsCoefIncludedInCalculationLimit)).ToList();

            return Json(models);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION_EDIT)]
        public JsonResult ChangeBuildingsStatusInCalculationForCorrectionByDate(string models, DateTime date)
        {
            var historyJson = JObject.Parse(models).SelectToken("models").ToString();

            var allRecords = JsonConvert.DeserializeObject<List<CorrectionByDateModel>>(historyJson);
            var changedRecords = allRecords.Where(x => x.IsDirty).Select(CorrectionByDateModel.UnMap).ToList();

            var isDataUpdated = CorrectionByDateService.ChangeBuildingsStatusInCalculation(changedRecords);

            string message;
            if (isDataUpdated)
            {
                CorrectionByDateForMarketObjectsLongProcess.AddProcessToQueue();
                message = "Данные успешно обновлены, процедура перерасчета цены с учетом корректировки на дату добавлена в очередь";
            }
            else
            {
                message = "Не найдено данных для изменения";
            }

            return Json(new { Message = message });
        }

        #endregion

        #region Correction By Bargain

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public ActionResult CorrectionByBargain()
        {
            var segments = System.Enum.GetValues(typeof(MarketSegment)).Cast<MarketSegment>();
            ViewData["Segments"] = segments.Select(x => new SelectListItem(x.GetEnumDescription(),
                    x.GetEnumCode())).ToList();

            return View(new CorrectionByBargainModel());
        }

	    [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public ActionResult CorrectionByBargain(CorrectionByBargainModel model)
	    {
	        if (!ModelState.IsValid)
	        {
	            return GenerateMessageNonValidModel();
	        }

	        var request = model.ToCorrectionByBargainRequest();
	        var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByBargain);
            var correctionByBargainProc = new CorrectionByBargainProc(settings);
	        correctionByBargainProc.PerformBargainCorrectionProc(request);

            return Json(new { Success = "Процедура Корректировки на торг успешно выполнена" });
        }

        #endregion Correction By Bargain

        #region Correction By Rooms

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public ActionResult CorrectionByRoomGeneralCoefficients()
        {
            var filters = CorrectionByRoomService.CalculatedMarketSegments.Select(x => (long) x).ToArray();
            var segments = Helpers.EnumExtensions.GetSelectList(typeof(MarketSegment), filterValues: filters);

            ViewBag.Segments = segments;

            return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public JsonResult GetCorrectionByRoomGeneralCoefficients(long marketSegmentCode)
        {
            var history = CorrectionByRoomService.GetAverageCoefficients(marketSegmentCode);
            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByRoom);
            var models = history.Select(x => CorrectionByRoomModel.Map(x,
                CorrectionByRoomService.IsOneRoomCoefIncludedInCalculationLimit,
                CorrectionByRoomService.IsThreeRoomsCoefIncludedInCalculationLimit)).ToList();

            return Json(models);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public ActionResult CorrectionByRoomDetailedCoefficients(long marketSegmentCode, DateTime date)
        {
            var marketSegment = (MarketSegment)marketSegmentCode;

            ViewBag.Date = date;
            ViewBag.MarketSegmentCode = marketSegmentCode;
            ViewBag.MarketSegment = marketSegment.GetEnumDescription();

            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByRoom);
            if (settings.LowerLimitForCoefficient.HasValue)
            {
                ViewBag.LowerLimitForCoefficient = settings.LowerLimitForCoefficient;
            }
            if (settings.UpperLimitForCoefficient.HasValue)
            {
                ViewBag.UpperLimitForCoefficient = settings.UpperLimitForCoefficient;
            }
            if (settings.LowerLimitForTheSecondCoefficient.HasValue)
            {
                ViewBag.LowerLimitForTheSecondCoefficient = settings.LowerLimitForTheSecondCoefficient;
            }
            if (settings.UpperLimitForTheSecondCoefficient.HasValue)
            {
                ViewBag.UpperLimitForTheSecondCoefficient = settings.UpperLimitForTheSecondCoefficient;
            }


            return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public JsonResult GetCorrectionByRoomDetailedCoefficients(long marketSegmentCode, DateTime date)
        {
            var historyRecords = CorrectionByRoomService.GetDetailedCoefficients(marketSegmentCode, date);
            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByRoom);
            var models = historyRecords.Select(x => CorrectionByRoomModel.Map(x,
                CorrectionByRoomService.IsOneRoomCoefIncludedInCalculationLimit,
                CorrectionByRoomService.IsThreeRoomsCoefIncludedInCalculationLimit)).ToList();

            return Json(models);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION_EDIT)]
        public JsonResult ChangeBuildingsStatusInCalculation(string models, DateTime date)
        {
            var historyJson = JObject.Parse(models).SelectToken("models").ToString();

            var allRecords = JsonConvert.DeserializeObject<List<CorrectionByRoomModel>>(historyJson);
            var changedRecords = allRecords.Where(x => x.IsDirty).Select(CorrectionByRoomModel.UnMap).ToList();

            var isDataUpdated = CorrectionByRoomService.ChangeBuildingsStatusInCalculation(changedRecords);

            string message;
            if (isDataUpdated)
            {
                CorrectionByRoomForMarketObjectsLongProcess.AddProcessToQueue(new CorrectionByRoomRequest { Date = date });
                message = "Данные успешно обновлены, процедура перерасчета цены с учетом корректировки на комнатность добавлена в очередь";
            }
            else
            {
                message = "Не найдено данных для изменения";
            }

            return Json(new {Message = message });
        }

        #endregion

        #region Correction By Stage

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public ActionResult CorrectionByStageGeneralHistory()
		{
		    var filters = CorrectionByStageService.CalculatedMarketSegments.Select(x => (long)x).ToArray();
		    var segments = Helpers.EnumExtensions.GetSelectList(typeof(MarketSegment), filterValues: filters);
			ViewBag.Segments = segments;

			return View();
		}

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public JsonResult GetCorrectionByStageGeneralHistory(long marketSegmentCode)
		{
            var history = CorrectionByStageService.GetGeneralHistory(marketSegmentCode);

            return Json(history);
		}

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public ActionResult CorrectionByStageDetailedHistory(long marketSegmentCode, DateTime date)
		{
			var marketSegment = (MarketSegment)marketSegmentCode;

			ViewBag.Date = date;
			ViewBag.MarketSegmentCode = marketSegmentCode;
			ViewBag.MarketSegment = marketSegment.GetEnumDescription();
		    var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByStage);
		    if (settings.LowerLimitForCoefficient.HasValue)
		    {
		        ViewBag.LowerLimitForCoefficient = settings.LowerLimitForCoefficient;
		    }
		    if (settings.UpperLimitForCoefficient.HasValue)
		    {
		        ViewBag.UpperLimitForCoefficient = settings.UpperLimitForCoefficient;
		    }

            return View();
		}

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public JsonResult GetCorrectionByStageDetailedHistory(long marketSegmentCode, DateTime date)
		{
			var historyRecords = CorrectionByStageService.GetDetailedHistory(marketSegmentCode, date);
		    var models = historyRecords.Select(x => CorrectionByStageModel.Map(x, CorrectionByStageService.IsCoefIncludedInCalculationLimit)).ToList();

            return Json(models);
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION_EDIT)]
        public JsonResult ChangeBuildingsStatusInCalculationByStage(string models, DateTime date)
		{
			var historyJson = JObject.Parse(models).SelectToken("models").ToString();

			var history = JsonConvert.DeserializeObject<List<CorrectionByStageModel>>(historyJson);
			var records = history.Select(CorrectionByStageModel.UnMap).ToList();

			var isDataUpdated = CorrectionByStageService.ChangeBuildingsStatusInCalculation(records);

			string message;
			if (isDataUpdated)
			{
				CorrectionByStageForMarketObjectsLongProcess.AddProcessToQueue(new CorrectionByRoomRequest { Date = date });
				message = "Данные успешно обновлены, процедура перерасчета цены с учетом корректировки на дату добавлена в очередь";
			}
			else
			{
				message = "Не найдено данных для изменения";
			}

			return Json(new { Message = message });
		}

        #endregion

        #region Correction For First Floor

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public IActionResult CorrectionForFirstFloorDetailed(long marketSegmentCode, DateTime date)
        {
            var marketSegment = (MarketSegment)marketSegmentCode;

            ViewBag.Date = date;
            ViewBag.MarketSegmentCode = marketSegmentCode;
            ViewBag.MarketSegment = marketSegment.GetEnumDescription();
            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByStage);
            if (settings.LowerLimitForCoefficient.HasValue)
            {
                ViewBag.LowerLimitForCoefficient = settings.LowerLimitForCoefficient;
            }
            if (settings.UpperLimitForCoefficient.HasValue)
            {
                ViewBag.UpperLimitForCoefficient = settings.UpperLimitForCoefficient;
            }

            return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public JsonResult GetCorrectionForFirstFloorDetailed(long marketSegmentCode, DateTime date)
        {
            var details = CorrectionForFirstFloorService.GetDetailsForSegmentAtDate(marketSegmentCode, date);
            var models = details.Select(x => CorrectionForFirstFloorModel.Map(x, CorrectionForFirstFloorService.IsCoefIncludedInCalculationLimit)).ToList();

            return Json(models);
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public IActionResult CorrectionForFirstFloorGeneral()
        {
            var filters = CorrectionForFirstFloorService.CalculatedMarketSegments.Select(x => (long)x).ToArray();
            var segments = Helpers.EnumExtensions.GetSelectList(typeof(MarketSegment), filterValues: filters);
            ViewBag.Segments = segments;

            return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION)]
        public JsonResult GetCorrectionForFirstFloorGeneral(long marketSegmentCode)
        {
            var stats = CorrectionForFirstFloorService.GetRatesBySegment(marketSegmentCode);

            return Json(stats);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION_EDIT)]
        public JsonResult ChangeFirstFloorStatusInCalculation(string models, DateTime date, MarketSegment code)
        {
            var historyJson = JObject.Parse(models).SelectToken("models").ToString();

            var allRecords = JsonConvert.DeserializeObject<List<CorrectionForFirstFloorModel>>(historyJson);
            var changedRecords = allRecords.Where(x => x.IsDirty).Select(CorrectionForFirstFloorModel.UnMap).ToList();

            var isDataUpdated = CorrectionForFirstFloorService.ChangeBuildingsStatusInCalculation(changedRecords);

            string message;
            if (isDataUpdated)
            {
                CorrectionForFirstFloorForMarketObjectsLongProcess.AddProcessToQueue(new CorrectionForFirstFloorRequest { Date = date, Segment = code });
                message = "Данные успешно обновлены, процедура перерасчета цены с учетом корректировки на первый этаж добавлена в очередь";
            }
            else
            {
                message = "Не найдено данных для изменения";
            }

            return Json(new { Message = message });
        }
        #endregion

        #region Correction Settings

	    [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION_EDIT)]
        public IActionResult CorrectionSettings()
	    {
	        var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByDate);
	        var model = new CorrectionSettingsModel(settings, CorrectionTypes.CorrectionByDate);

            return View(model);
	    }

	    [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION_EDIT)]
        public IActionResult GetCorrectionSettingsInfo(CorrectionTypes correctionType)
	    {
	        var settings = CorrectionSettingsService.GetCorrectionSettings(correctionType);
	        return Content(JsonConvert.SerializeObject(new CorrectionSettingsModel(settings, correctionType)), "application/json");
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRECTION_EDIT)]
        public IActionResult CorrectionSettings(CorrectionSettingsModel model)
	    {
	        if (!ModelState.IsValid)
	        {
	            return GenerateMessageNonValidModel();
	        }

	        try
	        {
	            CorrectionSettingsService.SaveCorrectionSettings(model.ToModel(), model.CorrectionType.GetValueOrDefault());
	        }
	        catch (Exception e)
	        {
	            return SendErrorMessage(e.Message);
	        }

	        return Json(new
	        {
	            success = true
	        });
        }

        #endregion Correction Settings

    }
}