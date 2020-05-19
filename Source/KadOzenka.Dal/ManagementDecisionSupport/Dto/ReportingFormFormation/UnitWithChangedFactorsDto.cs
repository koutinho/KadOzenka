using System.Collections.Generic;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.ReportingFormFormation
{
	public class UnitWithChangedFactorsDto : UnitDto
	{
		public long Id { get; set; }
		public string ChangedFactor { get; set; }
		public List<string> ChangedFactors { get; set; }
	}
}
