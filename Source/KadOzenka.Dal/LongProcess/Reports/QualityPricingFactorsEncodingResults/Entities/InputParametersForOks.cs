using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults.Entities
{
	public class InputParametersForOks
	{
		public List<long> TaskIds { get; set; }
		public long ParentKnAttributeId { get; set; }
		public long TypeOfUsingNameAttributeId { get; set; }
		public long TypeOfUsingCodeAttributeId { get; set; }
		public long TypeOfUsingCodeSourceAttributeId { get; set; }
		public long TypeOfUsingGroupCodeAttributeId { get; set; }
		public long SegmentAttributeId { get; set; }
		public long FunctionalGroupNameAttributeId { get; set; }
	}
}
