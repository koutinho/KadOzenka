using System.Collections.Generic;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports
{
	public class ExportedObjectDto : UnitObjectDto
	{
		public string Status { get; set; }

		public override List<object> ToRowExportObjects()
		{
			var list = base.ToRowExportObjects();
			list.Add(Status);

			return list;
		}
	}
}
