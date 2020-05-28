using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class NumberOfObjectsByAdministrativeDistrictsBySubjectDto
	{
		public string PropertyType { get; set; }
		public string Group { get; set; }
		public long Count { get; set; }
		public bool HasGroup { get; set; }
	}
}