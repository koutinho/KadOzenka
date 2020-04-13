using System;
using System.Linq;
using CIPJS.Models.ExpressScore;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Web.Models.ExpressScore;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.ES;
using ObjectModel.Market;

namespace KadOzenka.Web.Controllers
{
	public class ExpressScoreController : KoBaseController
	{
		private ExpressScoreService _service;

		public ExpressScoreController(ExpressScoreService service)
		{
			_service = service;
		}
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
				out var yearRange, out var squareRange);

			if (!string.IsNullOrEmpty(resMessage))
			{
				return SendErrorMessage(resMessage);
			}


			var objects = OMCoreObject.Where(x => x.PropertyMarketSegment_Code == param.Segment
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
					 Lat = y.Lat,
					 Lng = y.Lng
				 });

			var coordinates = _service.GetCoordinatesPointAtSelectedDistance(objects, param.SelectedLat.GetValueOrDefault(), param.SelectedLng.GetValueOrDefault(), param.Quality.GetValueOrDefault());

			if (coordinates.Count == 0)
			{
				return SendErrorMessage("Объекты аналоги не найдены");
			}

			return Json(new {response = new { coordinates } });
		}

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
    }
}

