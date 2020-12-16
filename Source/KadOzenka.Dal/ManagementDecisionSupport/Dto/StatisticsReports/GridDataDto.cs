using System.Collections.Generic;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports
{
	public class GridDataDto<T> where T : UnitObjectDto
	{
		public List<T> Data { get; set; }
		public long? Total { get; set; }
	}
}
