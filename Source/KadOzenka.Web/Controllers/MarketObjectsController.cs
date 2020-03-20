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
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace KadOzenka.Web.Controllers
{
    public class MarketObjectsController : KoBaseController
	{
        public CorrectionByDateService CorrectionByDateService { get; set; }

        public MarketObjectsController()
        {
            CorrectionByDateService = new CorrectionByDateService();
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
            var lastCorrection = CorrectionByDateService.GetNextCorrection();

            ViewBag.LastDate = lastCorrection?.Date.ToString("dd.MM.yyyy");

            return View();
        }

        [HttpGet]
        public JsonResult GetConsumerPriceIndexes([DataSourceRequest]DataSourceRequest request)
        {
            var corrections = CorrectionByDateService.GetCorrections();

            var models = new List<CorrectionByDateModel>();
            corrections.ForEach(x => models.Add(CorrectionByDateModel.Map(x)));

            return Json(models.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult AddConsumerPriceIndexRosstat()
        {
            var nextCorrection = CorrectionByDateService.GetNextCorrection();

            var model = new CorrectionByDateModel
            {
                Id = nextCorrection.Id,
                IsDateReadOnly = true,
                IndexDate = nextCorrection.Date
            };

            return View("~/Views/MarketObjects/EditConsumerPriceIndexRosstat.cshtml", model);
        }

        [HttpGet]
        public ActionResult EditConsumerPriceIndexRosstat()
        {
            var model = new CorrectionByDateModel();
            return View(model);
        }

        [HttpGet]
        public JsonResult GetCorrectionByDate(DateTime? date)
        {
            if (date == null)
                throw new ArgumentException("Дата не может быть пустой");

            var correction = CorrectionByDateService.GetCorrectionByDate(date.Value);
            var model = CorrectionByDateModel.Map(correction);

            return Json(new { Correction = model });
        }

        [HttpPost]
        public JsonResult EditConsumerPriceIndexRosstat(CorrectionByDateModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            CorrectionByDateService.EditCorrection(CorrectionByDateModel.UnMap(model));
            var message = "Индекс успешно обновлен";

            return Json(new { Message = message });
        }

        #endregion
    }

}