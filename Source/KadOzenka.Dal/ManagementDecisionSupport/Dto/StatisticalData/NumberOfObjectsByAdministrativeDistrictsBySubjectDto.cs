namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class NumberOfObjectsByAdministrativeDistrictsBySubjectDto : PropertyTypeWithPurposeDto
	{
		public string PropertyType { get; set; }
		public string Group { get; set; }
		public long Count { get; set; }
		public bool HasGroup { get; set; }
	}
}