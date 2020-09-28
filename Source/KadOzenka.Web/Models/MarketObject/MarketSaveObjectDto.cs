using ObjectModel.Directory;
using ObjectModel.Market;

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

		public static void ToEntity(MarketSaveObjectDto dto, ref OMCoreObject obj)
		{
			obj.Lng = dto.Lng;
			obj.Lat = dto.Lat;
			obj.PropertyTypesCIPJS_Code = dto.PropertyTypeCode.GetValueOrDefault();
			obj.PropertyMarketSegment_Code = dto.MarketSegmentCode.GetValueOrDefault();
			obj.ProcessType_Code = dto.StatusCode.GetValueOrDefault();
			obj.EntranceType = dto.EntranceType;
			obj.QualityClass_Code = dto.QualityClassCode.GetValueOrDefault();
			obj.Renovation = dto.Renovation;
			obj.BuildingLine = dto.BuildingLine;
			obj.FloorNumber = dto.FloorNumber;
		}
	}
}
