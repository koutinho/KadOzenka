﻿namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto
	{
		public string Distrinct { get; set; }
		public string RegionNumber { get; set; }
		public string PropertyType { get; set; }
		public string Group { get; set; }
		public long Count { get; set; }
		public bool HasGroup { get; set; } = true;
	}
}
