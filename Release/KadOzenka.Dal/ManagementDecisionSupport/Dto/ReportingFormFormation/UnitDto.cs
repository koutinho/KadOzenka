using System;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.ReportingFormFormation
{
	public class UnitDto
	{
		public string CadastralNumber { get; set; }
		public DateTime? CreationDate { get; set; }
		public KoStatusRepeatCalc StatusRepeatCalc { get; set; }
		public decimal? Square { get; set; }
		public PropertyTypes PropertyType { get; set; }
		public KoUnitStatus Status { get; set; }
	}
}
