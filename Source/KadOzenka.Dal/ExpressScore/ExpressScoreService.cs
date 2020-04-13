using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ExpressScore
{
	public class ExpressScoreService
	{
		public string GetSearchParamForNearestObject(string address, decimal square, out OMYearConstruction yearRange, out OMSquare squareRange)
		{
			yearRange = null;
			squareRange = null;

			var yandexAddress = OMYandexAddress.Where(x => x.FormalizedAddress.Contains(address)).Select(x => x.CadastralNumber).ExecuteFirstOrDefault();

			if (yandexAddress == null)
			{
				return "Адрес для объекта не найден";
			}

			//TODO Маг числа: 2018 - ид тура в котором ищем, 25000100 - ид атрибута ID для KO_UNIT_PARAMS_OKS_2018, 25018400 - ид атрибута год постройки для KO_UNIT_PARAMS_OKS_2018
			// в двльнейшем будет какой то GUI для настройки этих параметров

			var unitsIds = OMUnit.Where(x => x.CadastralNumber == yandexAddress.CadastralNumber && x.TourId == 2018)
				.Select(x => x.Id).Execute().Select(x => x.Id).OrderByDescending(x => x).ToList();


			QSQuery query = GetQsQuery(OMUnitParamsOks2018.GetRegisterId(), 25000100, unitsIds);
			query.AddColumn(new QSColumnSimple(25018400, nameof(YearDto.Year)));
			List<YearDto> years = query.ExecuteQuery<YearDto>().Where(x => x.Year.HasValue).OrderByDescending(x => x.Id).ToList();

			if (years.Count == 0)
			{
				return "Не найдены данные для выбраного объекта";
			}

			var year = years[0].Year;

			yearRange = OMYearConstruction.Where(x => x.YearFrom < year.Value && year.Value < x.YearTo)
				.SelectAll().ExecuteFirstOrDefault();

			squareRange = OMSquare.Where(x => x.SquareFrom <= square && square < x.SquareTo).SelectAll()
				.ExecuteFirstOrDefault();

			return "";
		}

		private QSQuery GetQsQuery(int registerId,int filterId, List<long> filterValues)
		{
			return new QSQuery
			{
				MainRegisterID = registerId,
				Condition = new QSConditionSimple
				{
					ConditionType = QSConditionType.In,
					LeftOperand = new QSColumnSimple(filterId),
					RightOperand = new QSColumnConstant(filterValues)
				}
			};
		}

		
		public List<CoordinatesDto> GetNearestCoordinates(Dictionary<long, CoordinatesDto> coordinates, Dictionary<long, double> distances, int quality)
		{
			List<KeyValuePair<long, double>> myList = distances.ToList();

			myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

			var ids = myList.Take(quality).Select(x => x.Key);

			return coordinates.Where(x => ids.Contains(x.Key)).Select(x => x.Value).ToList();
		}

		/// <summary>
		/// Считаем длинну дуги зная широту и долготу исходной точки
		/// </summary>
		/// <returns>
		/// возвращаем нужное количество точек в радиусе 1 км или 3 ближайшие точки
		/// </returns>
		public List<CoordinatesDto> GetCoordinatesPointAtSelectedDistance(Dictionary<long, CoordinatesDto> coordinates, decimal selectedLat, decimal selectedLng, int quality)
		{
			var selectedCoordinates = new Dictionary<long, CoordinatesDto>();
			var distances = new Dictionary<long, double>();
			int r = 6371; //км — средний радиус земного шара.
			int searchRad = 1; //км - радиус поиска объектов;
			int reservedCount = 3; //Количество объектов которое показываем если не нашли в пределах нужного радиуса

			foreach (var item in coordinates.Where(x => x.Value.Lng != selectedLng && x.Value.Lat != selectedLat))
			{
				// 1 градус = 0.0174533 рад
				var coordinate = item.Value;
				var lat = (double)coordinate.Lat * 0.0174533;
				var lng = (double)coordinate.Lng * 0.0174533;
				var sLat = (double)selectedLat * 0.0174533;
				var sLng = (double)selectedLng * 0.0174533;

				var delLng = Math.Pow(Math.Sin((lng - sLng) / 2),2);
				var delLat = Math.Pow(Math.Sin((lat - sLat) / 2),2);

				var d1 = 2 *r * Math.Asin(Math.Sqrt(delLat + Math.Cos(lat)* Math.Cos(sLat)* delLng)); // км - расстояние от исходной точки до одного из найденных объектов

				distances.Add(item.Key, d1);
				if (d1 <= searchRad)
				{
					selectedCoordinates.Add(item.Key, item.Value);
				}
			}
			if (selectedCoordinates.Count > 0 && selectedCoordinates.Count <= quality)
			{
				return selectedCoordinates.Values.ToList();
			}

			if (selectedCoordinates.Count == 0)
			{
				return  GetNearestCoordinates(coordinates, distances, reservedCount);
			}

			if (selectedCoordinates.Count > quality)
			{
				return GetNearestCoordinates(coordinates, distances, quality);
			}

			return new List<CoordinatesDto>();
		}
	}
}