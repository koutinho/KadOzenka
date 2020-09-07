namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto
{
    public class UpksAndUprsByGroupsOksDto
    {
        public string ParentGroup { get; set; }
        public string PropertyType { get; set; }
        public string Purpose { get; set; }
        public bool HasPurpose { get; set; }
        public int ObjectsCount { get; set; }
        public CalculationInfoDto Upks { get; set; }
        public CalculationInfoDto Uprs { get; set; }

        public UpksAndUprsByGroupsOksDto()
        {
            Upks = new CalculationInfoDto();
            Uprs = new CalculationInfoDto();
        }
    }
}
