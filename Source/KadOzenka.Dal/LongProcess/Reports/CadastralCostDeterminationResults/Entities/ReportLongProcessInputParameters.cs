using System;
using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities
{
	public class ReportLongProcessInputParameters
	{
		public List<long> TaskIds { get; set; }
		public Type Type { get; set; }
	}
}
