using System.Collections.Generic;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports
{
	public class ZoneStatisticDto : UnitObjectDto
	{
		public string Zone { get; set; }

		public override List<object> ToRowExportObjects()
		{
			var list = base.ToRowExportObjects();
			list.Add(Zone);

			return list;
		}
	}
}
