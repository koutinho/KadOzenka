using System.IO;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalReportsExport
{
	public class StatisticsReportsExportResult
	{
		public bool UseExportSavingToStorage { get; set; }
		public string UrlToDownloadReport { get; set; }
		public  GbuReportService.ReportFile ReportFile { get; set; }
	}
}
