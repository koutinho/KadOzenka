using System.Collections.Generic;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports
{
	public class GroupStatisticDto : UnitObjectDto
	{
		public string Group { get; set; }
		public long? SubGroupId { get; set; }
		public string SubGroup { get; set; }

		public override List<object> ToRowExportObjects()
		{
			var list = base.ToRowExportObjects();
			list.Add(Group);
			list.Add(SubGroup);

			return list;
		}
	}
}
