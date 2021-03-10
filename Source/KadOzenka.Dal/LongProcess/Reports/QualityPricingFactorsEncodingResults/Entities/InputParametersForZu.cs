using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults.Entities
{
	public class InputParametersForZu
	{
		public List<long> TaskIds { get; set; }
		public long LinkedObjectsInfoAttributeId { get; set; }
		public long LinkedObjectsInfoSourceAttributeId { get; set; }
		public long TypeOfUsingNameAttributeId { get; set; }
		public long TypeOfUsingCodeAttributeId { get; set; }
		public long TypeOfUsingCodeSourceAttributeId { get; set; }
		public long SegmentAttributeId { get; set; }
	}
}
