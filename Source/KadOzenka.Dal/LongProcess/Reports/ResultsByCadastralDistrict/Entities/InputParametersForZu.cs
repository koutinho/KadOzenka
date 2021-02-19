using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.Reports.ResultsByCadastralDistrict.Entities
{
	public class InputParametersForZu
	{
		public List<long> TaskIds { get; set; }
		public long InfoAboutExistenceOfOtherObjectsAttributeId { get; set; }
		public long InfoSourceAttributeId { get; set; }
		public long SegmentAttributeId { get; set; }
		public long UsageTypeCodeAttributeId { get; set; }
		public long UsageTypeNameAttributeId { get; set; }
		public long UsageTypeCodeSourceAttributeId { get; set; }
	}
}
