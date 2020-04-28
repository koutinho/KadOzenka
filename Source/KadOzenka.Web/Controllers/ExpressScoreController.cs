﻿using System;
using System.Collections.Generic;
using System.Linq;
using CIPJS.Models.ExpressScore;
using Core.Register;
using Core.UI.Registers.CoreUI.Registers;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Web.Helpers;
using KadOzenka.Web.Models.ExpressScore;
using KadOzenka.Web.Models.MarketObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Directory;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Web.Controllers
{
	public class ExpressScoreController : KoBaseController
	{
		#region Init

		private ExpressScoreService _service;
		private ViewRenderService _viewRenderService;

		public ExpressScoreController(ExpressScoreService service, ViewRenderService viewRenderService)
		{
			_service = service;
			_viewRenderService = viewRenderService;
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

			var objects = OMCoreObject.Where(x =>
					x.ProcessType_Code != ProcessStep.Excluded && x.PropertyMarketSegment_Code == param.Segment
															   && x.BuildingYear != null &&
															   x.BuildingYear < yearRange.YearTo &&
															   yearRange.YearFrom < x.BuildingYear
															   && x.Area != null && x.Area < squareRange.SquareTo &&
															   squareRange.SquareFrom <
															   x.Area && param.DealType.Contains(x.DealType_Code))
				.Select(x => new
				{
					x.Id,
					x.Lat,
					x.Lng
				}).Execute().Select(x => new CoordinatesDto
				{
					Id = x.Id,
					Lat = x.Lat.GetValueOrDefault(),
					Lng = x.Lng.GetValueOrDefault(),
				}).Distinct().ToList();
				

			//Проверяем дату актуальности
			List<CoordinatesDto> searchedAnalogs = new List<CoordinatesDto>();

			var actualDate = new DateTime(param.ActualDate.Value.Year, param.ActualDate.Value.Month, param.ActualDate.Value.Day) + new TimeSpan(23, 59, 59);
			foreach (var obj in objects)
			{
				var historyPrices = OMPriceHistory.Where(x => x.InitialId == obj.Id && x.ChangingDate <= actualDate).SelectAll().Execute();

				if (historyPrices.Count > 0)
				{
					searchedAnalogs.Add(obj);
					continue;
				}

				var analog = OMCoreObject.Where(x => x.Id == obj.Id && x.ParserTime <= actualDate)
					.ExecuteFirstOrDefault();
				if (analog != null)
				{
					searchedAnalogs.Add(obj);
				}
			}

			var coordinatesInput = searchedAnalogs.ToDictionary(x => x.Id.GetValueOrDefault(), y => new CoordinatesDto
			{
				Id = y.Id,
				Lat = y.Lat,
				Lng = y.Lng
			});

			var coordinates = _service.GetCoordinatesPointAtSelectedDistance(coordinatesInput, param.SelectedLat.GetValueOrDefault(), param.SelectedLng.GetValueOrDefault(), param.Quality.GetValueOrDefault());

			if (coordinates.Count == 0)
			{
				return SendErrorMessage("Объекты аналоги не найдены");
			}

			BuildObjectCards(coordinates);

			return Json(new { response = new { coordinates, targetObjectId } });
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
				out ResultCalculateDto resultCalculate, viewModel.ScenarioType);

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

		#region Recalculate Analog
		[HttpGet]
		public ActionResult RecalculateAnalog(int esId)
		{
			var analogIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();
			ViewBag.DeleteAnalogIds = analogIds;
			ViewBag.EsId = esId;
			return View();
		}

		[HttpPost]
		public JsonResult RecalculateAnalog([FromForm]List<int> analogIds, [FromForm]int expressScoreId)
		{
			if (analogIds.Count == 0)
			{
				return SendErrorMessage("Выберите аналоги");
			}
			var obj = OMExpressScore.Where(x => x.Id == expressScoreId).SelectAll().ExecuteFirstOrDefault();

			string resMsg = _service.RecalculateExpressScore(_service.GetAnalogsByIds(analogIds), analogIds,
				(int)obj.Objectid, (int)obj.Floor, obj.Square, expressScoreId, obj.ScenarioType_Code, out decimal cost, out decimal squareCost);

			if (!string.IsNullOrEmpty(resMsg))
			{
				return SendErrorMessage(resMsg);
			}

			return Json( new {success = new { cost, squareCost } });
		}

		#endregion

		#region Setting ExpressScore

		public ActionResult ConstructorExpressScore()
		{
			return View();
		}

		public JsonResult GetListSegments()
		{
			List<SelectListItem> segments = new List<SelectListItem>();

			foreach (var segment in  Enum.GetNames(typeof(MarketSegment)))
			{
				
			}
			return Json(segments);
		}

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

		public ActionResult SettingsExpressScore(int segmentId)
		{
			var model = new SettingsExpressScoreViewModel();
			return View(model);
		}

		#endregion

		#region Support Methods

		private void BuildObjectCards(List<CoordinatesDto> coordinates)
		{
			var resultObjectIds = coordinates.Select(x => x.Id).ToList();
			var resultObjects = OMCoreObject.Where(x => resultObjectIds.Contains(x.Id))
				.Select(x => new
				{
					x.Id,
					x.Images,
					x.Price,
					x.PricePerMeter,
					x.Area,
					x.Address,
					x.CadastralNumber,
					x.PropertyMarketSegment,
					x.DealType,
					x.Market_Code,
					x.PropertyTypesCIPJS_Code
				}).Execute();

			coordinates.ForEach(x =>
			{
				var resultObject = resultObjects.FirstOrDefault(y => y.Id == x.Id);
				x.ObjectMiniCard = _viewRenderService.ToString("MarketObjects/ObjectMiniCard",
					CoreObjectDto.MapToMiniCard(resultObject));
			});
		}

		#endregion
	}
}

