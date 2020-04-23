using System;
using System.Collections.Generic;
using System.Linq;
using CIPJS.Models.ExpressScore;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Web.Models.ExpressScore;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Directory;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Web.Controllers
{
	public class ExpressScoreController : KoBaseController
	{


		#region init
		private ExpressScoreService _service;
		public ExpressScoreController(ExpressScoreService service)
		{
			_service = service;
		}
		#endregion

		public ActionResult Index()
		{
			return View();
		}

		public JsonResult GetKadNumber(string address)
		{
			var yandexAddress = OMYandexAddress.Where(x => x.FormalizedAddress.Contains(address)).Select(x => x.CadastralNumber).ExecuteFirstOrDefault();

			if (yandexAddress == null)
			{
				return SendErrorMessage("Кадастровый номер для объекта не найден");
			}
			return Json(new { response = new {kadNumber = yandexAddress.CadastralNumber} });
		}

		public JsonResult GetAddressByKadNumber(string kadNumber)
		{
			var yandexAddress = OMYandexAddress.Where(x => x.CadastralNumber == kadNumber).Select(x => x.FormalizedAddress).ExecuteFirstOrDefault();

			if (yandexAddress == null)
			{
				return SendErrorMessage("Адрес для объекта не найден");
			}
			return Json(new { response = new { address = yandexAddress.FormalizedAddress } });
		}


		public JsonResult GetNearestObjects([FromQuery] NearestObjectViewModel param)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			string resMessage = _service.GetSearchParamForNearestObject(param.Address, param.Square.GetValueOrDefault(),
				out var yearRange, out var squareRange, out int targetObjectId);

			if (!string.IsNullOrEmpty(resMessage))
			{
				return SendErrorMessage(resMessage);
			}


			var objects = OMCoreObject.Where(x => x.ProcessType_Code != ProcessStep.Excluded && x.PropertyMarketSegment_Code == param.Segment
				&& x.BuildingYear!= null && x.BuildingYear < yearRange.YearTo && yearRange.YearFrom < x.BuildingYear
				&& x.Area != null && x.Area < squareRange.SquareTo && squareRange.SquareFrom < x.Area)
				.Select(x => new {
				x.Id,
				x.Lat,
				x.Lng
				}).Execute().Select(x => new CoordinatesDto
				 {
					Id = x.Id,
					Lat = x.Lat.GetValueOrDefault(),
					Lng = x.Lng.GetValueOrDefault(),
				 }).Distinct().ToDictionary(x => x.Id.GetValueOrDefault(), y => new CoordinatesDto
				 {
					 Id = y.Id,
					 Lat = y.Lat,
					 Lng = y.Lng
				 });

			var coordinates = _service.GetCoordinatesPointAtSelectedDistance(objects, param.SelectedLat.GetValueOrDefault(), param.SelectedLng.GetValueOrDefault(), param.Quality.GetValueOrDefault());

			if (coordinates.Count == 0)
			{
				return SendErrorMessage("Объекты аналоги не найдены");
			}

			return Json(new {response = new { coordinates, targetObjectId } });
		}

		#region WallMaterial

		[HttpGet]
		public ActionResult WallMaterial(long id)
		{
			var wallMaterial = OMWallMaterial.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			return View(WallMaterialViewModel.FromEntity(wallMaterial));
		}

		[HttpPost]
		public ActionResult WallMaterial(WallMaterialViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			long id;
			try
			{
				id = model.Id == -1
					? _service.AddWallMaterial(model.WallMaterial, model.Mark.Value)
					: _service.UpdateEWallMaterial(model.Id, model.WallMaterial, model.Mark.Value);
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}

			return Json(new { Success = "Сохранено успешно", Id = id });
		}

		#endregion

		[HttpPost]
		public ActionResult CalculateCostTargetObject(CalculateCostTargetObjectViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			string resMsg = _service.CalculateExpressScore(_service.GetAnalogsByIds(viewModel.SelectedPoints),
				viewModel.TargetObjectId.GetValueOrDefault(), viewModel.Floor.GetValueOrDefault(), viewModel.Square.GetValueOrDefault(),
				out ResultCalculateDto resultCalculate);

			if (!string.IsNullOrEmpty(resMsg))
			{
				return SendErrorMessage(resMsg);
			}

			ViewBag.Filter = $"10002000={string.Join(',', resultCalculate.Analogs.Select(x => x.Id).ToList())}";
			ViewBag.EsId = resultCalculate.Id;
			return PartialView("Partials/PartialGridResultExpressScore", resultCalculate);
		}

		public ActionResult AnalogObjectsCard(int objectId)
		{
			var marketIds = OMEsToMarketCoreObject.Where(x => x.EsId == objectId).SelectAll().Execute().Select(x => x.MarketObjectId).ToList();

			ViewBag.Filter = $"10002000={string.Join(',', marketIds)}";
			ViewBag.EsId = objectId;

			return View();
		}

		#region Delete Analog
		[HttpGet]
		public ActionResult RecalculateAnalog(int esId)
		{
			var analogIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();
			ViewBag.DeleteAnalogIds = analogIds;
			ViewBag.EsId = esId;
			return View();
		}

		[HttpPost]
		public JsonResult RecalculateAnalog([FromForm]List<int> deleteAnalogIds, [FromForm]int expressScoreId)
		{
			if (deleteAnalogIds.Count == 0)
			{
				return SendErrorMessage("Выберите аналоги");
			}
			var obj = OMExpressScore.Where(x => x.Id == expressScoreId).SelectAll().ExecuteFirstOrDefault();
			var analogIds = OMEsToMarketCoreObject.Where(x => x.EsId == expressScoreId).SelectAll().Execute()
				.Select(x => (int)x.MarketObjectId).ToList();

			string resMsg = _service.RemoveAnalogAndRecalculateExpressScore(_service.GetAnalogsByIds(analogIds), deleteAnalogIds,
				(int)obj.Objectid, (int)obj.Floor, obj.Square, expressScoreId, out decimal cost, out decimal squareCost);

			if (!string.IsNullOrEmpty(resMsg))
			{
				return SendErrorMessage(resMsg);
			}

			return Json( new {success = new { cost, squareCost } });
		}

		#endregion

		#region Setting ExpressScore

		public JsonResult GetDictionaries()
		{
			var dictionaries = OMEsReference.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Text = x.Name,
				Value = (int)x.Id
			}).ToList();

			dictionaries.Insert(0, new { Text = "",  Value = 0});

			return Json(dictionaries);
		}

		public JsonResult GetAttributes(int registerId)
		{
			var attributes =	RegisterCache.RegisterAttributes.Values.Where(x => x.RegisterId == registerId).Select(x => new
			{
				Text = x.Description,
				Value = x.Id
			}).ToList();

			return Json(attributes);
		}

		public JsonResult GetFactorRegisters(int tourId)
		{
			var registerFactors = OMTourFactorRegister.Where(x => x.TourId == tourId).SelectAll().Execute().Select(x => new
			{
				Text = RegisterCache.Registers.Values.FirstOrDefault(y => y.Id == x.RegisterId)?.Description,
				Value = x.RegisterId
			}).ToList();
			return Json(registerFactors);
		}

		public ActionResult SettingsExpressScore()
		{
			var model = new SettingsExpressScoreViewModel {CostFactors = new List<CostFactor>()};
			return View(model);
		}

		#endregion
	}
}

