using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest.Filter
{
	public class FilterSimpleDto : FilterDto
	{
		public string Member { get; set; }
		public object Value { get; set; }
		public FilterOperatorType Operator { get; set; }
	}
}
