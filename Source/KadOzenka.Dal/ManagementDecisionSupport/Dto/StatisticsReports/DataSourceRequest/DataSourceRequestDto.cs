using System.Collections.Generic;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest.Filter;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest
{
	public class DataSourceRequestDto
	{
		public List<FilterDto> Filters { get; set; }
		public List<SortDto> Sorts { get; set; }
		public int PageSize { get; set; }
		public int Page { get; set; }
	}
}
