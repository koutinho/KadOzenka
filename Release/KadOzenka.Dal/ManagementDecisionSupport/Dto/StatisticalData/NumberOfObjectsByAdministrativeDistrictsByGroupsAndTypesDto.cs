namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto : PropertyTypeWithPurposeDto
	{
		public string ParentName { get; set; }
		public string Name { get; set; }
		public string PropertyType { get; set; }
		public string Group { get; set; }
		public long Count { get; set; }
		public bool HasGroup { get; set; } = true;
	}
}
