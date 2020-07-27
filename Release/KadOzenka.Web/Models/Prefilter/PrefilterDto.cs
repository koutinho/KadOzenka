using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Prefilter
{
	public class PrefilterDto
	{
		public long[] MarketSegmentItemIds { get; set; }
		public long[] DealTypeItemIds { get; set; }
		public long[] PropertyTypeItemIds { get; set; }
		public int? PriceFrom { get; set; }
		public int? PriceTo { get; set; }
		public string Metro { get; set; }
	}
}
