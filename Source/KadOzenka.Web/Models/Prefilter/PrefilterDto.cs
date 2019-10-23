using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Prefilter
{
	public class PrefilterDto
	{
		public string PropertyMarketSegmentValue { get; set; }
		public long? PropertyMarketSegmentItemId { get; set; }
		public string DealTypeValue { get; set; }
		public long? DealTypeItemId { get; set; }
		public string PropertyTypeValue { get; set; }
		public long? PropertyTypeItemId { get; set; }
		public int? PriceFrom { get; set; }
		public int? PriceTo { get; set; }
		public string Metro { get; set; }
	}
}
