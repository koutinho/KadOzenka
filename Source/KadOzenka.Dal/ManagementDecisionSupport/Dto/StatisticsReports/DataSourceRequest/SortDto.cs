using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest
{
	public class SortDto
	{
		public string Member { get; set; }
		public SortDirectionType SortDirection { get; set; }
	}
}
