namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class NumberOfObjectsByGroupsDto
	{
		public string PropertyType { get; set; }
		public string Group { get; set; }
		public bool HasGroup { get; set; }
		public string ParentGroup { get; set; }
		public bool HasParentGroup { get; set; }
		public long Count { get; set; }
	}
}
