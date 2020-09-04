namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData.Dto.MinMaxAverage
{
	public class MinMaxAverageByGroupsUpksOksDto : MinMaxAverageCalculationInfoDto
    {
		public string ParentGroup { get; set; }
		public string PropertyType { get; set; }
		public string Purpose { get; set; }
		public bool HasPurpose { get; set; }
		public int ObjectsCount { get; set; }
    }
}
