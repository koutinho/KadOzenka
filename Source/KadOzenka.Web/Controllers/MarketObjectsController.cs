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
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Attributes;
using MarketPlaceBusiness.Interfaces;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
    public class MarketObjectsController : KoBaseController
	{
		public IMarketObjectService MarketObjectsService { get; set; }

        public MarketObjectsController(IMarketObjectService marketObjectsService, IRegisterCacheWrapper registerCacheWrapper,
	        IGbuObjectService gbuObjectService)
	        : base(gbuObjectService, registerCacheWrapper)
        {
	        MarketObjectsService = marketObjectsService;
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        public IActionResult ObjectCard(long id)
        {
	        List<OMPriceHistory> priceHistory = null;
            var analogItem = MarketObjectsService.GetMappedObjectById(id);
            if (analogItem != null)
            {
                var screenList = OMScreenshots.Where(x => x.InitialId == id).SelectAll().Execute().Select(x => (x.Id, x.CreationDate)).ToList();
                if(screenList.IsNotEmpty()) ViewBag.ScreenShots = screenList;
                priceHistory = ObjectModel.Market.OMPriceHistory.Where(x => x.InitialId == id).SelectAll().Execute();
            }
            return View(CoreObjectDto.OMMap(analogItem, priceHistory));
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
			var ids = RegistersVariables.CurrentList != null && RegistersVariables.CurrentList.Count > 0 ? RegistersVariables.CurrentList?.Cast<long>()?.ToList() : new List<long> { objectId };
			var screenList = OMScreenshots.Where(x => ids.Contains((long) x.InitialId)).SelectAll().Execute().ToList();
			var analogItems = MarketObjectsService.GetByIds(ids, x => x.CadastralNumber);
			using (MemoryStream zipStream = new MemoryStream())
			{
				using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
				{
					int n = 1;
					foreach (OMScreenshots screenshot in screenList)
					{
						var analogItem = analogItems.Where(x => x.Id == screenshot.InitialId).FirstOrDefault();
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

        //[HttpGet]
        //[SRDFunction(Tag = SRDCoreFunctions.MARKET_ACTIVATE_PROCESS)]
        //public ActionResult ActivateProcess()
        //{
        //    return View("~/Views/AnalogCheck/ActivateDistrictsRegionsZones.cshtml");
        //}

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_ACTIVATE_COORDINATES)]
        public ActionResult ActivateCoordinates()
        {
            return View("~/Views/AnalogCheck/ActivateCoordinates.cshtml");
        }


        #region Outliers Checking

        //[SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        //public ActionResult GetMarketSegmentList()
        //{
	       // var exceptions = new List<long> { (long)MarketSegment.None, (long)MarketSegment.NoSegment };
	       // var segments = Helpers.EnumExtensions.GetSelectList(typeof(MarketSegment), exceptions: exceptions);

	       // return Content(JsonConvert.SerializeObject(segments), "application/json");
        //}

        //[SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        //public ActionResult GetMarketPropertyTypeDivisionsList()
        //{
	       // var propertyTypes = Helpers.EnumExtensions.GetSelectList(typeof(ObjectPropertyTypeDivision));
	       // return Content(JsonConvert.SerializeObject(propertyTypes), "application/json");
        //}

        //[HttpGet]
        //[SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        //public ActionResult OutliersSettings(bool isPartialView = false)
        //{
	       // ViewBag.isPartialView = isPartialView;
        //    return View("~/Views/MarketObjects/OutliersCheckingSettings.cshtml");
        //}

        //[HttpGet]
        //[SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        //public JsonResult GetOutliersSettingsCoefficients()
        //{
	       // var settingsDto = OutliersCheckingSettingsService.GetOutliersCheckingSettings();
	       // var models = OutliersSettingsModel.FromDto(settingsDto);

        //    return Json(models);
        //}

        //[SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        //public JsonResult UpdateOutliersSettingsCoefficients(string modelJson)
        //{
	       // var model = JsonConvert.DeserializeObject<OutliersSettingsModel> (modelJson);
	       // OutliersCheckingSettingsService.UpdateOutliersCheckingSettings(model.ToDto());

	       // return Json(new[] { model });
        //}

        //[HttpGet]
        //[SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        //public IActionResult OutliersCheckingSettingsImport()
        //{
	       // return View(new OutliersSettingsImportModel());
        //}

        //[HttpPost]
        //[SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        //public IActionResult OutliersCheckingSettingsImport(IFormFile file, OutliersSettingsImportModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return GenerateMessageNonValidModel();
        //    }

        //    object returnedData;
        //    try
        //    {
	       //     using (var stream = file.OpenReadStream())
	       //     {
		      //      var settingDto = viewModel.ToDto(file);
		      //      var importId = OutliersCheckingSettingsService.ImportOutliersCheckingSettingsFromExcel(stream, settingDto);
		      //      returnedData = new
		      //      {
			     //       importId
		      //      };
	       //     }
        //    }
        //    catch (Exception ex)
        //    {
	       //     ErrorManager.LogError(ex);
	       //     return BadRequest();
        //    }

        //    return Content(JsonConvert.SerializeObject(returnedData), "application/json");
        //}

        //[SRDFunction(Tag = SRDCoreFunctions.MARKET)]
        //public ActionResult PerformOutliersChecking(OutliersCheckingPerformModel model)
        //{
	       // if (!ModelState.IsValid)
	       // {
		      //  return GenerateMessageNonValidModel();
	       // }
        //    ////For testing
        //    //var settings = model.ToSettings();
        //    //var history = new OMOutliersCheckingHistory
        //    //{
        //    //    DateCreated = DateTime.Now,
        //    //    Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
        //    //    PropertyTypesMapping = !settings.AllPropertyTypes
	       //    //     ? JsonConvert.SerializeObject(settings.PropertyTypes)
	       //    //     : null,
        //    //};
        //    //if (settings.Segment.HasValue)
        //    //    history.MarketSegment_Code = settings.Segment.Value;
        //    //history.Save();
        //    //new OutliersCheckingLongProcess().StartProcess(new OMProcessType(), new OMQueue
        //    //{
        //    //    Status_Code = Status.Added,
        //    //    UserId = SRDSession.GetCurrentUserId(),
        //    //    Parameters = settings.SerializeToXml(),
        //    //    ObjectId = history.Id
        //    //}, new CancellationToken());

        //    OutliersCheckingLongProcess.AddProcessToQueue(model.ToSettings());

        //    return Ok();
        //}

        #endregion Outliers Checking
    }
}