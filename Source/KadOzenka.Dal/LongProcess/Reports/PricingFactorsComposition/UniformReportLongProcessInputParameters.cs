using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	public class UniformReportLongProcessInputParameters
	{
		public List<long> TaskIds { get; set; }
		public int? PackageSize { get; set; }
	}
}
