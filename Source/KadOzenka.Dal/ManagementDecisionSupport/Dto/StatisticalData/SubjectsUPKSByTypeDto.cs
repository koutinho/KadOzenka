using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class SubjectsUPKSByTypeDto
	{
		public string PropertyType { get; set; }
		public int ObjectsCount { get; set; }
		public UpksCalcType UpksCalcType { get; set; }
		public decimal? UpksCalcValue { get; set; }
	}
}
