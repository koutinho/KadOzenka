using System;
using System.Collections.Generic;
using System.Linq;

namespace KadOzenka.Dal.ExpressScore
{
	public class ExpressScoreService
	{
		/// <summary>
		/// coordinates - все координаты аналогов
		/// selectedLat - широта исходной точки
		/// selectedLng - долгота исходной точки
		/// quality - количество координат которое нужно вернуть
		/// </summary>
		/// <returns>
		/// Возвращает ближайшие координаты к выбраной точке
		/// </returns>
		public List<CoordinatesDto> GetNearestCoordinates(Dictionary<long, CoordinatesDto> coordinates, decimal selectedLat, decimal selectedLng, int quality)
		{
			var distances = new Dictionary<long, double>();

			foreach (var item in coordinates)
			{
				var coordinate = item.Value;
				var deltaLat = selectedLat - coordinate.Lat;
				var deltaLng = selectedLng - coordinate.Lng;
				var distance = Math.Round(Math.Sqrt(Math.Pow((double)deltaLat, 2) + Math.Pow((double)deltaLng, 2)), 2) ;

				if (!distances.Values.Any(x => x.Equals(distance)))
				{
					distances.Add(item.Key, distance);
				}
				
			}

			List<KeyValuePair<long, double>> myList = distances.ToList();

			myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

			var ids = myList.Take(quality).Select(x => x.Key);

			return coordinates.Where(x => ids.Contains(x.Key)).Select(x => x.Value).ToList();
		}
	}
}