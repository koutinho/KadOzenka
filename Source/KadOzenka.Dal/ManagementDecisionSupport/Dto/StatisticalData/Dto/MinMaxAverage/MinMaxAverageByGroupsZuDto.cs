namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData.Dto.MinMaxAverage
{
	public class MinMaxAverageByGroupsZuDto
	{
		public string ParentGroup { get; set; }
        public int ObjectsCount { get; set; }
        public decimal? ObjectUpksMin { get; set; }
        public decimal? ObjectUpksAvg { get; set; }
        public decimal? ObjectUpksAvgWeight { get; set; }
        public decimal? ObjectUpksMax { get; set; }
    }
}
