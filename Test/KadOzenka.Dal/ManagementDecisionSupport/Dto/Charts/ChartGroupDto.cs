using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using KadOzenka.Dal.Oks;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.Charts
{
	public class ChartGroupDto
	{
		public string GroupName { get; set; }
		public long GroupId { get; set; }
		//public long ParentGroupId { get; set; }
		public ObjectType ObjectType { get; set; }
		public long ObjectCount { get; set; }

		public List<ChartChildGroupDto> ChildChartGroupDtoList { get; set; }
	}
}
