using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.Reports.ResultsByCadastralDistrict.Entities
{
	public class InputParametersForBuildings
	{
		public List<long> TaskIds { get; set; }
		public long SegmentAttributeId { get; set; }
		public long UsageTypeNameAttributeId { get; set; }
		public long UsageTypeCodeAttributeId { get; set; }
		public long UsageTypeCodeSourceAttributeId { get; set; }
		public long SubGroupUsageTypeCodeAttributeId { get; set; }
		public long FunctionalSubGroupNameAttributeId { get; set; }
	}
}
