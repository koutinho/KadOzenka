namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData.Dto.MinMaxAverage
{
	public class MinMaxAverageByGroupsUpksAndUprsZuDto
	{
		public string ParentGroup { get; set; }
        public int ObjectsCount { get; set; }
        public MinMaxAverageCalculationInfoDto Upks { get; set; }
        public MinMaxAverageCalculationInfoDto Uprs { get; set; }

        public MinMaxAverageByGroupsUpksAndUprsZuDto()
        {
            Upks = new MinMaxAverageCalculationInfoDto();
            Uprs = new MinMaxAverageCalculationInfoDto();
        }
    }
}
