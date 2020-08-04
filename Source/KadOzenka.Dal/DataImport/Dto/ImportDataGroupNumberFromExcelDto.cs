using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.DataImport.Dto
{
	public class ImportDataGroupNumberFromExcelDto
	{
		public string FileName { get; set; }
		public long? TourId { get; set; }
		public bool IsUnitStatusUsed { get; set; }
		public KoUnitStatus? UnitStatus { get; set; }
		public List<long> TaskFilter { get; set; }
		public string RegisterViewId { get; set; }
		public int MainRegisterId { get; set; }
	}
}
