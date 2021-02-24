using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.Reports.KRSummaryResults.Entities
{
	public class ZuReportLongProcessInputParameters
	{
		public List<long> TaskIds { get; set; }
		public long KladrAttributeId { get; set; }
	}
}
