using System;
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
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using ObjectModel.Directory;

namespace KadOzenka.Web.Controllers
{
    public class MarketObjectsController : KoBaseController
	{
        public CorrectionByDateService CorrectionByDateService { get; set; }
        public CorrectionByRoomService CorrectionByRoomService { get; set; }
		public CorrectionByStageService CorrectionByStageService { get; set; }
        public CorrectionForFirstFloorService CorrectionForFirstFloorService { get; set; }

		public MarketObjectsController()
        {
            CorrectionByDateService = new CorrectionByDateService();
            CorrectionByRoomService = new CorrectionByRoomService();
			CorrectionByStageService = new CorrectionByStageService();
            CorrectionForFirstFloorService = new CorrectionForFirstFloorService();
        }

        [HttpGet]
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
        public FileResult ShowFile(long id)
        {
            var screen = OMScreenshots.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            var fileStream = FileStorageManager.GetFileStream("MarketObjectScreenShot", screen.CreationDate.Value, id.ToString());
            return File(fileStream, screen.Type, id.ToString());
        }

		[HttpGet]
		public ActionResult GetChartData(long? id)
		{
			if (!id.HasValue) throw new Exception("Не передан идентификатор объекта.");
			var priceHistories = ObjectModel.Market.OMPriceHistory.Where(x => x.InitialId == id.Value).SelectAll().Execute();
			var screenshots = OMScreenshots.Where(x => x.InitialId == id.Value).SelectAll().Execute().ToList();
			return Content(JsonConvert.SerializeObject(PriceHistoryChartModel.FromEntities(priceHistories, screenshots)), "application/json");
		}

		[HttpGet]
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


        #region Correction By Date

        [HttpGet]
        public ActionResult ConsumerPriceIndexes()
        {
            var nextIndex = CorrectionByDateService.GetNextConsumerIndex();

            ViewBag.LastDate = nextIndex?.Date.ToString("dd.MM.yyyy");

            return View();
        }

        [HttpGet]
        public JsonResult GetConsumerPriceIndexes([DataSourceRequest]DataSourceRequest request)
        {
            var indexes = CorrectionByDateService.GetAllConsumerIndexes();

            var models = new List<CorrectionByDateModel>();
            indexes.ForEach(x => models.Add(CorrectionByDateModel.Map(x)));

            return Json(models.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult AddConsumerPriceIndexRosstat()
        {
            var nexIndex = CorrectionByDateService.GetNextConsumerIndex();

            var model = new CorrectionByDateModel
            {
                Id = nexIndex.Id,
                IsDateReadOnly = true,
                IndexDate = nexIndex.Date
            };

            return View("~/Views/MarketObjects/EditConsumerPriceIndexRosstat.cshtml", model);
        }

        [HttpGet]
        public ActionResult EditConsumerPriceIndexRosstat()
        {
            var nexIndex = CorrectionByDateService.GetNextConsumerIndex();
            var model = new CorrectionByDateModel
            {
                MaxIndexDate = nexIndex.Date.AddMonths(-1)
            };
            return View(model);
        }

        [HttpGet]
        public JsonResult GetCorrectionByDate(DateTime? date)
        {
            if (date == null)
                throw new ArgumentException("Дата не может быть пустой");

            var index = CorrectionByDateService.GetConsumerIndexByDate(date.Value);
            var model = CorrectionByDateModel.Map(index);

            return Json(new { Correction = model });
        }

        [HttpPost]
        public JsonResult EditConsumerPriceIndexRosstat(CorrectionByDateModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            CorrectionByDateService.EditConsumerIndex(CorrectionByDateModel.UnMap(model));
            var message = "Индекс успешно обновлен. Процесс перерассчета стоимости объектов добавлен в очередь.";

            return Json(new { Message = message });
        }

        #endregion

        
        #region Correction By Bargain

        [HttpGet]
        public ActionResult CorrectionByBargain()
        {
            var segments = System.Enum.GetValues(typeof(MarketSegment)).Cast<MarketSegment>();
            ViewData["Segments"] = segments.Select(x => new SelectListItem(x.GetEnumDescription(),
                    x.GetEnumCode())).ToList();

            return View(new CorrectionByBargainModel());
        }

	    [HttpPost]
	    public ActionResult CorrectionByBargain(CorrectionByBargainModel model)
	    {
	        if (!ModelState.IsValid)
	        {
	            return GenerateMessageNonValidModel();
	        }

	        var request = model.ToCorrectionByBargainRequest();
	        var correctionByBargainProc = new CorrectionByBargainProc();
	        correctionByBargainProc.PerformBargainCorrectionProc(request);

            return Json(new { Success = "Процедура Корректировки на торг успешно выполнена" });
        }

        #endregion Correction By Bargain


        #region Correction By Rooms

        [HttpGet]
        public ActionResult CorrectionByRoomGeneralCoefficients()
        {
            var segments = Helpers.EnumExtensions.GetSelectList(typeof(MarketSegment));
            ViewBag.Segments = segments;

            return View();
        }

        public JsonResult GetCorrectionByRoomGeneralCoefficients(long marketSegmentCode)
        {
            var history = CorrectionByRoomService.GetAverageCoefficients(marketSegmentCode);

            return Json(history.Select(CorrectionByRoomModel.Map).ToList());
        }

        [HttpGet]
        public ActionResult CorrectionByRoomDetailedCoefficients(long marketSegmentCode, DateTime date)
        {
            var marketSegment = (MarketSegment)marketSegmentCode;

            ViewBag.Date = date;
            ViewBag.MarketSegmentCode = marketSegmentCode;
            ViewBag.MarketSegment = marketSegment.GetEnumDescription();

            return View();
        }

        public JsonResult GetCorrectionByRoomDetailedCoefficients(long marketSegmentCode, DateTime date)
        {
            var historyRecords = CorrectionByRoomService.GetDetailedCoefficients(marketSegmentCode, date);
            var models = historyRecords.Select(CorrectionByRoomModel.Map).ToList();

            return Json(models);
        }

        [HttpPost]
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
                message = "Данные успешно обновлены, процедура перерасчета цены с учетом корректировки на дату добавлена в очередь";
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
		public ActionResult CorrectionByStageGeneralHistory()
		{
			var segments = Helpers.EnumExtensions.GetSelectList(typeof(MarketSegment));
			ViewBag.Segments = segments;

			return View();
		}

		public JsonResult GetCorrectionByStageGeneralHistory(long marketSegmentCode)
		{
			var history = CorrectionByStageService.GetGeneralHistory(marketSegmentCode);

			return Json(history);
		}

		[HttpGet]
		public ActionResult CorrectionByStageDetailedHistory(long marketSegmentCode, DateTime date)
		{
			var marketSegment = (MarketSegment)marketSegmentCode;

			ViewBag.Date = date;
			ViewBag.MarketSegmentCode = marketSegmentCode;
			ViewBag.MarketSegment = marketSegment.GetEnumDescription();

			return View();
		}

		public JsonResult GetCorrectionByStageDetailedHistory(long marketSegmentCode, DateTime date)
		{
			var historyRecords = CorrectionByStageService.GetDetailedHistory(marketSegmentCode, date);
			
			return Json(historyRecords);
		}

		[HttpPost]		
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
        public IActionResult CorrectionForFirstFloorDetailed(long marketSegmentCode, DateTime date)
        {
            var marketSegment = (MarketSegment)marketSegmentCode;

            ViewBag.Date = date;
            ViewBag.MarketSegmentCode = marketSegmentCode;
            ViewBag.MarketSegment = marketSegment.GetEnumDescription();

            return View();
        }

        public JsonResult GetCorrectionForFirstFloorDetailed(long marketSegmentCode, DateTime date)
        {
            var details = CorrectionForFirstFloorService.GetDetailsForSegmentAtDate(marketSegmentCode, date);
            var models = details.Select(CorrectionForFirstFloorModel.Map).ToList();

            return Json(models);
        }

        public IActionResult CorrectionForFirstFloorGeneral()
        {
            var segments = Helpers.EnumExtensions.GetSelectList(typeof(MarketSegment));
            ViewBag.Segments = segments;

            return View();
        }
        public JsonResult GetCorrectionForFirstFloorGeneral(long marketSegmentCode)
        {
            var stats = CorrectionForFirstFloorService.GetRatesBySegment(marketSegmentCode);

            return Json(stats);
        }

        [HttpPost]
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
    }
}