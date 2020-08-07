using System;
using System.Collections.Generic;
using System.Linq;
using CIPJS.Models.ExpressScore;
using Core.Register;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Dal.ScoreCommon;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Helpers;
using KadOzenka.Web.Models.ExpressScore;
using KadOzenka.Web.Models.MarketObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Es;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Market;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
	public class ExpressScoreController : KoBaseController
	{
		#region Init

		private ExpressScoreService _service;
		private ScoreCommonService ScoreCommonService { get; set; }
		private ViewRenderService _viewRenderService;
		private TourFactorService TourFactorService { get; set; }

		public ExpressScoreController(ExpressScoreService service, ViewRenderService viewRenderService, ScoreCommonService scoreCommonService, TourFactorService tourFactorService)
		{
			_service = service;
			_viewRenderService = viewRenderService;
            ScoreCommonService = scoreCommonService;
            TourFactorService = tourFactorService;

		}

		#endregion

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public ActionResult Index()
		{
			return View();
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public JsonResult GetKadNumber(string address)
		{
			var yandexAddress = OMYandexAddress.Where(x => x.FormalizedAddress.Contains(address)).Select(x => x.CadastralNumber).ExecuteFirstOrDefault();

			if (yandexAddress == null)
			{
				return SendErrorMessage("Кадастровый номер для объекта не найден");
			}
			return Json(new { response = new {kadNumber = yandexAddress.CadastralNumber} });
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public JsonResult GetAddressByKadNumber(string kadNumber)
		{
			var yandexAddress = OMYandexAddress.Where(x => x.CadastralNumber == kadNumber).Select(x => x.FormalizedAddress).ExecuteFirstOrDefault();

			if (yandexAddress == null)
			{
				return SendErrorMessage("Адрес для объекта не найден");
			}
			return Json(new { response = new { address = yandexAddress.FormalizedAddress } });
		}

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
        public ActionResult TargetObjectSubCard(string targetObjectStr)
        {
            var targetObject = JsonConvert.DeserializeObject<TargetObjectDto>(targetObjectStr);

            var model = TargetObjectModel.ToModel(targetObject);
       
            return PartialView("~/Views/ExpressScore/Partials/TargetObjectSubCard.cshtml", model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
        public JsonResult TargetObjectSubCard(TargetObjectModel targetObject)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

			TourFactorService.SetTourFactorAttributesValue(targetObject.UnitId, targetObject.Attributes.Select(x => x.ToDto()).ToList());

			return Json(new {success = true, message = "Значения атрибутов успешно сохранены"});
        }

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public ActionResult GetNearestObjects([FromQuery] NearestObjectViewModel param)
		{
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var yandexAddress = OMYandexAddress.Where(x => x.FormalizedAddress.Contains(param.Address)).Select(x => x.CadastralNumber).ExecuteFirstOrDefault();
            if (yandexAddress == null)
                return SendErrorMessage("Адрес для объекта не найден");

            var setting = OMSettingsParams.Where(x => x.SegmentType_Code == param.Segment.GetValueOrDefault()).SelectAll().ExecuteFirstOrDefault();
            if (setting == null)
                return SendErrorMessage("Не найдены настройки для выбранного сегмента");

            var unitsIds = ScoreCommonService.GetUnitsIdsByCadastralNumber(yandexAddress.CadastralNumber, (int)setting.TourId);
            if (unitsIds.Count == 0)
                return SendErrorMessage("Выбранный объект не входит в тур или его параметры оценки не заполнены");

            var costFactor = setting.CostFacrors.DeserializeFromXml<CostFactorsDto>();
            if (costFactor.YearBuildId == null && costFactor.YearBuildId == 0)
                return SendErrorMessage("В настройках не задан атрибут для года постройки.");

            var targetObject = _service.GetTargetObject(setting, costFactor, unitsIds);
            if(targetObject == null)
                return SendErrorMessage("Не найдены данные для выбранного объекта.");

            if (targetObject.Attributes.Any(x => string.IsNullOrWhiteSpace(x.Value)))
	            return Json(new
				{
					updateTargetObjectUrl = Url.Action("TargetObjectSubCard", new { targetObjectStr = JsonConvert.SerializeObject(targetObject) })
				});

			var targetObjectBuildYear = Convert.ToInt32(targetObject.Attributes.FirstOrDefault(x => x.Id == costFactor.YearBuildId)?.Value);
            var yearRange = OMYearConstruction.Where(x => x.YearFrom <= targetObjectBuildYear && targetObjectBuildYear <= x.YearTo)
                .SelectAll().ExecuteFirstOrDefault();

            var square = param.Square.GetValueOrDefault();
            var squareRange = OMSquare.Where(x => x.SquareFrom <= square && square <= x.SquareTo).SelectAll()
                .ExecuteFirstOrDefault();

            if (squareRange == null && param.UseSquare || yearRange == null && param.UseYearBuild)
			{
				return SendErrorMessage("Не найден диапазон даты постройки или площади.");
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

            if (objects.Count == 0)
			{
				return SendErrorMessage("Объекты аналоги не найдены");
			}

            List<CoordinatesDto> searchedAnalogs = new List<CoordinatesDto>();
			//Проверяем дату актуальности
			{
				var actualDate = new DateTime(param.ActualDate.Value.Year, param.ActualDate.Value.Month, param.ActualDate.Value.Day) + new TimeSpan(23, 59, 59);
				var idsObjects = objects.Select(y => y.Id).ToList();
				var cachePriceHistory = OMPriceHistory.Where(x => idsObjects.Contains(x.InitialId)).SelectAll().Execute();

				var successIdsObjects = new List<long>();
				foreach (var obj in objects)
				{
					var historyPrices = cachePriceHistory.Where(x => x.InitialId == obj.Id && x.ChangingDate <= actualDate)
						.ToList();

					if (historyPrices.Count > 0)
					{
						searchedAnalogs.Add(obj);
						successIdsObjects.Add(obj.Id.GetValueOrDefault());
					}
				}

				List<CoordinatesDto> leftoversObjects =
					objects.Where(x => !successIdsObjects.Contains(x.Id.GetValueOrDefault())).ToList();

				if (leftoversObjects.Count > 0)
				{
					var idsLeftOversObjects = leftoversObjects.Select(x => x.Id).ToList();
					var cacheAnalogs = OMCoreObject.Where(x => idsLeftOversObjects.Contains(x.Id)).SelectAll().Execute();
					foreach (var obj in leftoversObjects)
					{
						var analogs = cacheAnalogs.Where(x =>
							x.Id == obj.Id && (x.ParserTime <= actualDate || x.LastDateUpdate <= actualDate));

						if (analogs.Any())
						{
							searchedAnalogs.Add(obj);
						}
					}
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

			return Json(new { response = new { coordinates, targetObjectId = targetObject.UnitId } });
		}

		#region WallMaterial

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public ActionResult WallMaterial(long id)
		{
			var wallMaterial = OMWallMaterial.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			return View(WallMaterialViewModel.FromEntity(wallMaterial));
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
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
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public ActionResult CalculateCostTargetObject(CalculateCostTargetObjectViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var inputParam = new InputCalculateDto
			{
				Address = viewModel.Address,
				Analogs = _service.GetAnalogsByIds(viewModel.SelectedPoints),
				DealType = viewModel.DealType,
				Floor = viewModel.Floor.GetValueOrDefault(),
				Kn = viewModel.Kn,
				ScenarioType = viewModel.ScenarioType,
				Square = viewModel.Square.GetValueOrDefault(),
				Segment = viewModel.Segment,
				TargetObjectId = viewModel.TargetObjectId.GetValueOrDefault()
			};

			string resMsg = _service.CalculateExpressScore(inputParam, out ResultCalculateDto resultCalculate);

			if (!string.IsNullOrEmpty(resMsg))
			{
				return SendErrorMessage(resMsg);
			}

			ViewBag.Filter = $"10002000={string.Join(',', resultCalculate.Analogs.Select(x => x.Id).ToList())}";
			ViewBag.EsId = resultCalculate.Id;
			return PartialView("Partials/PartialGridResultExpressScore", resultCalculate);
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_HISTORY)]
		public ActionResult AnalogObjectsCard(int objectId)
		{
			var marketIds = OMEsToMarketCoreObject.Where(x => x.EsId == objectId).SelectAll().Execute().Select(x => x.MarketObjectId).ToList();

			ViewBag.Filter = $"10002000={string.Join(',', marketIds)}";
			ViewBag.EsId = objectId;

			return View();
		}

		#region Recalculate Analog
		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public ActionResult RecalculateAnalog(int esId)
		{
			var analogIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();
			ViewBag.DeleteAnalogIds = analogIds;
			ViewBag.EsId = esId;
			return View();
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public JsonResult RecalculateAnalog([FromForm]List<int> analogIds, [FromForm]int expressScoreId)
		{
			if (analogIds.Count == 0)
			{
				return SendErrorMessage("Выберите аналоги");
			}
			var obj = OMExpressScore.Where(x => x.Id == expressScoreId).SelectAll().ExecuteFirstOrDefault();

			if (obj == null)
			{
				return SendErrorMessage("Не найдена оценка");

			}

			var inputParam = new InputCalculateDto
			{
				Address = obj.Address,
				Kn = obj.KadastralNumber,
				Analogs = _service.GetAnalogsByIds(analogIds),
				DealType = obj.DealType_Code == DealType.RentDeal ? DealTypeShort.Rent : DealTypeShort.Sale,
				Floor = (int) obj.Floor,
				ScenarioType = obj.ScenarioType_Code,
				Square = obj.Square,
				Segment = obj.SegmentType_Code,
				TargetObjectId = (int) obj.Objectid
			};

			string resMsg = _service.RecalculateExpressScore(inputParam, analogIds, expressScoreId, out decimal cost, out decimal squareCost, out long reportId);

			if (!string.IsNullOrEmpty(resMsg))
			{
				return SendErrorMessage(resMsg);
			}

			if (inputParam.DealType == DealTypeShort.Rent)
			{
				squareCost *= 12;
				cost *= 12;
			}

			return Json( new {success = new { cost, squareCost, reportId } });
		}

		#endregion

		#region Setting ExpressScore

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR)]
		public ActionResult ConstructorExpressScore()
		{
            return View();
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR)]
		public ActionResult SettingsExpressScore(int segmentId)
		{
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
			model.IsEdit =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR_EDIT);
			return PartialView("Partials/SettingsExpressScore", model);
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR_EDIT)]
		public JsonResult SettingsExpressScore(SettingsExpressScoreViewModel viewModel)
		{
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

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
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

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public JsonResult GetAttributes(int registerId)
		{
			var attributes =	RegisterCache.RegisterAttributes.Values.Where(x => x.RegisterId == registerId && !x.IsPrimaryKey).Select(x => new
			{
				Text = x.Name,
				Value = x.Id
			}).ToList();

			return Json(attributes);
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
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

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public ActionResult AddNewComplexCard(int count)
		{
			ViewBag.Count = count;
			return PartialView("Partials/PartialComplexFactorCard", new ComplexCostFactor());
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
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

