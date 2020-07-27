using System;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class MarketDataDto
	{
		public long UniqueNumber { get; set; }
		public string Kn { get; set; }
		public string SegmentGroup { get; set; }
		public string TypeOfUseCode { get; set; }
		public string OksGroup { get; set; }
		public string SubjectCode { get; set; }
		public string OKTMO { get; set; }
		public string AddressReferencePoint { get; set; }
		public string Metro { get; set; }
		public string Market { get; set; }
		public string Link { get; set; }
		public string Phone { get; set; }
		public DateTime? Date { get; set; }
		public string AdText { get; set; }
		public string TypeOfProperty { get; set; }
		public string TypeOfUse { get; set; }
		public string TypeOfRight { get; set; }
		public long? RoomCount { get; set; }
		public string DealSuggestion { get; set; }
		public decimal? Square { get; set; }
		public decimal? Price { get; set; }
		public decimal? Upks { get; set; }
		public decimal? AnnualRateOfRent { get; set; }
	}
}
