using System.IO;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalReportsExport
{
	public class StatisticsReportsExportResult
	{
		public bool UseExportSavingToStorage { get; set; }
		public long? ReportId { get; set; }
		public FileStream ReportFile { get; set; }
	}
}
