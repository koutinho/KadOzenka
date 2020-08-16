namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class NumberOfObjectsByAdministrativeDistrictsByGroupsDto
	{
		public string Name { get; set; }
		public string FirstParentName { get; set; }
		public string SecondParentName { get; set; }
		public string ThirdParentName { get; set; }
		public string Group { get; set; }
		public bool HasGroup { get; set; }
		public long ObjectsCount { get; set; }
	}
}
