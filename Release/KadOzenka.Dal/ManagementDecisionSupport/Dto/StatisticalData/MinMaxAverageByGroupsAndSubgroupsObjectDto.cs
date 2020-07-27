namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class MinMaxAverageByGroupsAndSubgroupsObjectDto : MinMaxAverageByGroupsObjectDto
	{
		public string SubgroupName { get; set; }
		public bool HasSubgroup { get; set; }
	}
}
