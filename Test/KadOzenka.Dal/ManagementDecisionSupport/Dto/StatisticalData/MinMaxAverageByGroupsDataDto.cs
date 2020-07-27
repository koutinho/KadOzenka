using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class MinMaxAverageByGroupsDataDto
	{
		public string GroupName { get; set; }
		public string PropertyType { get; set; }
		public string Purpose { get; set; }
		public bool HasPurpose { get; set; }
		public int ObjectsCount { get; set; }
		public UpksCalcType CalcType { get; set; }
		public decimal? UpksCalcValue { get; set; }
		public decimal? UprsCalcValue { get; set; }
	}
}
