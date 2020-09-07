namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto
{
	public class UpksByGroupsZuDto : CalculationInfoDto
	{
		public string ParentGroup { get; set; }
        public int ObjectsCount { get; set; }
    }
}
