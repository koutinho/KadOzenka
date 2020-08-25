namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData.Dto.MinMaxAverage
{
	public class MinMaxAverageByGroupsUprsZuDto
    {
		public string ParentGroup { get; set; }
        public int ObjectsCount { get; set; }
        public decimal? UprsMin { get; set; }
        public decimal? UprsAvg { get; set; }
        public decimal? UprsAvgWeight { get; set; }
        public decimal? UprsMax { get; set; }
    }
}
