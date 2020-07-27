using System.Collections.Generic;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports
{
	public class FactorStatisticDto : UnitObjectDto
	{
		public string ChangedFactors { get; set; }

		public override List<object> ToRowExportObjects()
		{
			var list = base.ToRowExportObjects();
			list.Add(ChangedFactors);

			return list;
		}
	}
}
