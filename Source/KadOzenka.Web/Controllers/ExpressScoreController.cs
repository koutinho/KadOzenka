using System.Collections.Generic;
using System.Linq;
using CIPJS.Models.ExpressScore;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ExpressScore;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Directory;
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

			List<QSCondition> conditions = new List<QSCondition>
			{
				new QSConditionSimple()
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMCoreObject.GetColumn(x => x.PropertyMarketSegment_Code),
					RightOperand = new QSColumnConstant(param.Segment)
				}
			};

			var objects = OMCoreObject.Where(conditions.ToArray()).Select(x => new
			{
				x.Id,
				x.Lat,
				x.Lng
			}).Execute()
				.Select(x => new
				 {
					 x.Id,
					 x.Lat,
					 x.Lng,
				 }).ToDictionary(x => x.Id, y => new CoordinatesDto
				 {
					 Lat = y.Lat.GetValueOrDefault(),
					 Lng = y.Lng.GetValueOrDefault()
				 });

			var coordinates = _service.GetNearestCoordinates(objects, param.SelectedLat.GetValueOrDefault(), param.SelectedLng.GetValueOrDefault(), param.Quality.GetValueOrDefault());

			return Json(new {response = new { coordinates } });
		}
	}
}

