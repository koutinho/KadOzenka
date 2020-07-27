using ObjectModel.Market;

namespace KadOzenka.Web.Models.MarketObject
{
	public class MapObjectDto
	{
		public long Id { get; set; }
		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }

		public static MapObjectDto OMMap(OMCoreObject entity)
		{
			var dto = new MapObjectDto
			{
				Id = entity.Id,
				Latitude = entity.Lat,
				Longitude = entity.Lng
			};

			return dto;
		}
	}
}
