namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto
{
	public class UpksAndUprsByGroupsZuDto
	{
		public string ParentGroup { get; set; }
        public int ObjectsCount { get; set; }
        public CalculationInfoDto Upks { get; set; }
        public CalculationInfoDto Uprs { get; set; }

        public UpksAndUprsByGroupsZuDto()
        {
            Upks = new CalculationInfoDto();
            Uprs = new CalculationInfoDto();
        }
    }
}
