using ObjectModel.Directory;

namespace KadOzenka.Web.Models.MarketObject
{
	public class MarketSaveObjectDto
	{
		public long? Id { get; set; }
		public decimal? Lng { get; set; }
		public decimal? Lat { get; set; }
		public PropertyTypesCIPJS? PropertyTypeCode { get; set; }
		public MarketSegment? MarketSegmentCode { get; set; }
		public ProcessStep? StatusCode { get; set; }
		public string EntranceType { get; set; }
		public QualityClass? QualityClassCode { get; set; }
		public string Renovation { get; set; }
		public string BuildingLine { get; set; }
		public long? FloorNumber { get; set; }
	}
}
