using System;
using System.Collections.Generic;
using System.Linq;
using CIPJS.Models.ExpressScore;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Web.Models.ExpressScore;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Directory;
using ObjectModel.ES;
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
		public JsonResult CalculateCostTargetObject(CalculateCostTargetObjectViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			double cTorg = 0.9231;

			var analogs = OMCoreObject.Where(x => viewModel.SelectedPoints.Contains((int)x.Id))
				.Select(x => new
				{
					x.Id,
					x.CadastralNumber,
					x.Price,
					x.Area,
					x.LastDateUpdate,
					x.FloorsCount,
					x.FloorNumber
				}).Execute();


			List<decimal> res = new List<decimal>();
			foreach (var analog in analogs)
			{
				decimal cost = 0;
				decimal yPrice = 0; // Удельный показатель стоимости


				if (analog.Price != null && analog.Area != null && analog.Area.GetValueOrDefault() != 0)
				{
					yPrice = analog.Price.GetValueOrDefault() / analog.Area.GetValueOrDefault();
				}

				//Корректировка на дату 
				var dateEstimate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

				var indexDateEstimate = OMIdexDate.Where(x => x.Date == dateEstimate).SelectAll().ExecuteFirstOrDefault();
				if (indexDateEstimate == null)
				{
					indexDateEstimate = OMIdexDate.Where(x => x).SelectAll().Execute().OrderByDescending(x => x.Date).First();
				}

				OMIdexDate indexAnalogDate = null;
				if (analog.LastDateUpdate != null)
				{
					var analogDate = new DateTime(analog.LastDateUpdate.Value.Year, analog.LastDateUpdate.Value.Month,
						1);

					indexAnalogDate = OMIdexDate.Where(x => x.Date == analogDate).SelectAll().ExecuteFirstOrDefault();
				}

				if (indexAnalogDate == null)
				{
					continue;
				}

				decimal kDate = indexAnalogDate.Index / indexDateEstimate.Index; // Корректировка на дату

				cost = kDate * yPrice;
				cost = cost * (decimal)cTorg;

				OMLandShare landShare = null;
				if (analog.FloorsCount != null)
				{
					landShare = OMLandShare.Where(x => x.Floor == analog.FloorsCount && x.SegmentType_Code == MarketSegment.Office).SelectAll().ExecuteFirstOrDefault();
					if (landShare == null)
					{
						var tmpLandShares = OMLandShare.Where(x => x.SegmentType_Code == MarketSegment.Office)
							.SelectAll().Execute().OrderByDescending(x => x.Floor).ToList();
						landShare = tmpLandShares.Count > 1 ? tmpLandShares[1] : null;
						if (tmpLandShares.Count > 0 && tmpLandShares[1].Floor < analog.FloorsCount)
						{
							landShare = tmpLandShares[0];

						}
					}
				}

				if (landShare != null)
				{
					cost = cost * landShare.Factor;
				}


				var estimatedParameters = _service.GetEstimateParametersByKn(analog.CadastralNumber);

				var estimatedParametersTargetObject = _service.GetEstimateParametersById(viewModel.TargetObjectId.GetValueOrDefault());

				if (estimatedParametersTargetObject == null)
				{
					continue;
				}

				// Начинаются оценочные факторы
				var wallMaterial = OMWallMaterial.Where(x => x.WallMaterial.Contains(estimatedParameters.WallMaterial) ).SelectAll()
					.ExecuteFirstOrDefault();

				var wallMaterialTargetObject = OMWallMaterial.Where(x => x.WallMaterial.Contains(estimatedParametersTargetObject.WallMaterial)).SelectAll()
					.ExecuteFirstOrDefault();

				if (wallMaterial != null && wallMaterialTargetObject != null)
				{
					var costFactor = OMCostFactor.Where(x => x.Id == 1).SelectAll().ExecuteFirstOrDefault();
					var factor = Math.Exp((double)(wallMaterialTargetObject.Mark * costFactor.Factor)) / Math.Exp((double)(wallMaterial.Mark * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 2).SelectAll().ExecuteFirstOrDefault();
					var factor = Math.Exp((double)(estimatedParametersTargetObject.DistanceToMetro * costFactor.Factor)) 
					             / Math.Exp((double)(estimatedParameters.DistanceToMetro * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 3).SelectAll().ExecuteFirstOrDefault();
					var factor = Math.Exp((double)(estimatedParametersTargetObject.DistanceToHistoryCityCenter * costFactor.Factor)) 
					             / Math.Exp((double)(estimatedParameters.DistanceToHistoryCityCenter * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 4).SelectAll().ExecuteFirstOrDefault();
					decimal distA = estimatedParametersTargetObject.DistanceToHighway > 500
						? 500
						: estimatedParametersTargetObject.DistanceToHighway; // нормируем расстояние по условию

					decimal distB = estimatedParameters.DistanceToHighway > 500
						? 500
						: estimatedParameters.DistanceToHighway; // нормируем расстояние по условию

					var factor = Math.Exp((double)(distA * costFactor.Factor)) / Math.Exp((double)(distB * costFactor.Factor));
					cost = cost * (decimal)factor;

				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 5).SelectAll().ExecuteFirstOrDefault();
					var isIndustrialZoneTargetObject =
						estimatedParametersTargetObject.IndustrialZone == IndustrialZoneEnum.Yes.GetEnumDescription()
							? 1
							: 0;

					var isIndustrialZone =
						estimatedParameters.IndustrialZone == IndustrialZoneEnum.Yes.GetEnumDescription()
							? 1
							: 0;

					var factor = Math.Exp((double)(isIndustrialZoneTargetObject * costFactor.Factor)) / Math.Exp((double)(isIndustrialZone * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 6).SelectAll().ExecuteFirstOrDefault();
					var factor = Math.Exp((double)(estimatedParametersTargetObject.CoefficientTerritoryValue * costFactor.Factor)) 
					             / Math.Exp((double)(estimatedParameters.CoefficientTerritoryValue * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var floor = analog.FloorNumber ?? estimatedParameters.Floor;
					var floors = OMFloor.Where(x => x).SelectAll().Execute().OrderByDescending(x => x.Floor).ToList();

					var floorFactor = floor != 0
						? floor > floors[0].Floor ? floors[0].Factor :
						floors?.FirstOrDefault(x => x.Floor == floor).Factor
						: 0;

					var targetObjectFloorFactor =
						viewModel.Floor > floors[0].Floor ? floors[0].Factor :
						floors?.FirstOrDefault(x => x.Floor == viewModel.Floor).Factor;

					if (floorFactor != 0)
					{
						var factor = targetObjectFloorFactor / floorFactor;
						cost = cost * (decimal)factor;
					}
				
				}
				res.Add(cost);

			}

			var costSquareMeter = res.Sum(x => x) / res.Count;

			return Json(new { });
		}

	}
}

