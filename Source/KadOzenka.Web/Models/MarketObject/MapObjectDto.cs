using MarketPlaceBusiness.Dto;
using ObjectModel.Market;

namespace KadOzenka.Web.Models.MarketObject
{
	public class MapObjectDto
	{
		public long Id { get; set; }
		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }

		public static MapObjectDto OMMap(MarketObjectDto entity)
		{
			var dto = new MapObjectDto
			{
				Id = entity.Id,
				Latitude = null,
				Longitude = null
			};

			return dto;
		}
	}
}
