using System.IO;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalReportsExport
{
	public class StatisticsReportsExportResult
	{
		public bool UseExportSavingToStorage { get; set; }
		public long? ReportId { get; set; }
		public  GbuReportService.ReportFile ReportFile { get; set; }
	}
}
