namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class NumberOfObjectsByGroupsDto
	{
		public string PropertyType { get; set; }
		public string Group { get; set; }
		public bool HasGroup { get; set; } = true;
		public string ParentGroup { get; set; }
		public bool HasParentGroup { get; set; } = true;
		public long Count { get; set; }
	}
}
