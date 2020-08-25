namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData.Dto.MinMaxAverage
{
	public class MinMaxAverageByGroupsUpksZuDto : MinMaxAverageCalculationInfoDto
	{
		public string ParentGroup { get; set; }
        public int ObjectsCount { get; set; }
    }
}
