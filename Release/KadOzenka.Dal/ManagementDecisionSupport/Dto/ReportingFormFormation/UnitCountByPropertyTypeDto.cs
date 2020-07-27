using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.ReportingFormFormation
{
	public class UnitCountByPropertyTypeDto
	{
		public PropertyTypes PropertyType { get; set; }
		public long ObjectsCount { get; set; }
	}
}
