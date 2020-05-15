using System;
using System.Collections.Generic;
using System.Linq;
using CIPJS.Models.ExpressScore;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Web.Helpers;
using KadOzenka.Web.Models.ExpressScore;
using KadOzenka.Web.Models.MarketObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Directory;
using ObjectModel.Es;
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
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRES_SSCORE_CALCULATE, true, false, true);
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
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRES_SSCORE_CALCULATE, true,
				false, true);
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			string resMessage = _service.GetSearchParamForNearestObject(param.Address, param.Square.GetValueOrDefault(), param.Segment.GetValueOrDefault(),
				out var yearRange, out var squareRange, out int targetObjectId);

			if (!string.IsNullOrEmpty(resMessage))
			{
				return SendErrorMessage(resMessage);
			}

			if (squareRange == null && param.UseSquare || yearRange == null && param.UseYearBuild)
			{
				return SendErrorMessage("Не найден дипозон даты постройки или площади.");
			}

			var condition = _service.GetSearchCondition(yearRange, squareRange, param.UseYearBuild, param.UseSquare, param.Segment.GetValueOrDefault(), param.DealType );
			var objects = OMCoreObject.Where(condition)
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
			var idsObjects = objects.Select(y => y.Id).ToList();
			var cachePriceHistory = OMPriceHistory.Where(x => idsObjects.Contains(x.InitialId)).SelectAll().Execute();

			foreach (var obj in objects)
			{
				var historyPrices = cachePriceHistory.Where(x => x.InitialId == obj.Id && x.ChangingDate <= actualDate)
					.ToList();

				if (historyPrices.Count > 0)
				{
					searchedAnalogs.Add(obj);
					continue;
				}

				var analog = OMCoreObject.Where(x => x.Id == obj.Id && (x.ParserTime <= actualDate || x.LastDateUpdate <= actualDate))
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
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRES_SSCORE_CALCULATE, true,
				false, true);
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			string resMsg = _service.CalculateExpressScore(_service.GetAnalogsByIds(viewModel.SelectedPoints),
				viewModel.TargetObjectId.GetValueOrDefault(), viewModel.Floor.GetValueOrDefault(), viewModel.Square.GetValueOrDefault(),
				out ResultCalculateDto resultCalculate, viewModel.ScenarioType, viewModel.Segment);

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
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRES_SSCORE_HISTORY, true,
				false, true);
			var marketIds = OMEsToMarketCoreObject.Where(x => x.EsId == objectId).SelectAll().Execute().Select(x => x.MarketObjectId).ToList();

			ViewBag.Filter = $"10002000={string.Join(',', marketIds)}";
			ViewBag.EsId = objectId;

			return View();
		}

		#region Recalculate Analog
		[HttpGet]
		public ActionResult RecalculateAnalog(int esId)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRES_SSCORE_CALCULATE, true,
				false, true);
			var analogIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();
			ViewBag.DeleteAnalogIds = analogIds;
			ViewBag.EsId = esId;
			return View();
		}

		[HttpPost]
		public JsonResult RecalculateAnalog([FromForm]List<int> analogIds, [FromForm]int expressScoreId)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRES_SSCORE_CALCULATE, true,
				false, true);
			if (analogIds.Count == 0)
			{
				return SendErrorMessage("Выберите аналоги");
			}
			var obj = OMExpressScore.Where(x => x.Id == expressScoreId).SelectAll().ExecuteFirstOrDefault();

			if (obj == null)
			{
				return SendErrorMessage("Не найдена оценка");

			}

			string resMsg = _service.RecalculateExpressScore(_service.GetAnalogsByIds(analogIds), analogIds,
				(int)obj.Objectid, (int)obj.Floor, obj.Square, expressScoreId, obj.ScenarioType_Code, obj.SegmentType_Code, out decimal cost, out decimal squareCost);

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
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRES_SSCORE_CONSTRUCTOR, true,
				false, true);
			return View();
		}

		public ActionResult SettingsExpressScore(int segmentId)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRES_SSCORE_CONSTRUCTOR, true,
				false, true);
			var model = new SettingsExpressScoreViewModel();
			var esSetting = OMSettingsParams.Where(x => x.SegmentType_Code == (MarketSegment)segmentId).SelectAll()
				.ExecuteFirstOrDefault();

			if (esSetting == null)
			{
				esSetting = new OMSettingsParams();
			}
			model.TourId = esSetting.TourId;
			model.FactorRegisterId = esSetting.Registerid;
			model.CostFactors = esSetting.CostFacrors?.DeserializeFromXml<CostFactorsDto>() ?? new CostFactorsDto
			{
				ComplexCostFactors = new List<ComplexCostFactor>(),
				SimpleCostFactors = new List<SimpleCostFactor>()
			};
			model.SegmentType = (MarketSegment) segmentId;
			return PartialView("Partials/SettingsExpressScore", model);
		}

		[HttpPost]
		public JsonResult SettingsExpressScore(SettingsExpressScoreViewModel viewModel)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRES_SSCORE_CONSTRUCTOR, true,
				false, true);
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}
			var setting = OMSettingsParams.Where(x => x.SegmentType_Code == viewModel.SegmentType.GetValueOrDefault()).SelectAll()
				.ExecuteFirstOrDefault();

			if (setting == null)
			{
				setting = new OMSettingsParams
				{
					SegmentType_Code = viewModel.SegmentType.GetValueOrDefault(),
					CostFacrors = viewModel.CostFactors.SerializeToXml(),
					Registerid = viewModel.FactorRegisterId.GetValueOrDefault(),
					TourId = viewModel.TourId.GetValueOrDefault()
				};
			}
			else
			{
				setting.CostFacrors = viewModel.CostFactors.SerializeToXml();
				setting.Registerid = viewModel.FactorRegisterId.GetValueOrDefault();
				setting.TourId = viewModel.TourId.GetValueOrDefault();
			}

			

			setting.Save();

			return Json(new {success = true});
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
				Text = x.Name,
				Value = x.Id
			}).ToList();

			return Json(attributes);
		}

		public JsonResult GetFactorRegisters(int tourId)
		{
			var registerFactors = OMTourFactorRegister.Where(x => x.TourId == tourId).SelectAll().Execute().Select(x => new SelectListItem
			{
				Text = RegisterCache.Registers.Values.FirstOrDefault(y => y.Id == x.RegisterId)?.Description,
				Value = x.RegisterId.ToString()
			}).GroupBy(x => x.Value).Select(x => new SelectListItem
			{
				Value = x.Key,
				Text = x.Select(y => y.Text).FirstOrDefault()
			}).ToList();
			return Json(registerFactors);
		}

		public ActionResult AddNewComplexCard(int count)
		{
			ViewBag.Count = count;
			return PartialView("Partials/PartialComplexFactorCard", new ComplexCostFactor());
		}

		public ActionResult AddNewSimpleCard(int count)
		{
			ViewBag.Count = count;
			return PartialView("Partials/PartialSimpleFactorCard", new ComplexCostFactor());
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

