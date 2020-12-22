using System;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.LongProcess.ManagementDecisionSupport.Settings
{
	public class StatisticsReportWidgetExportLongProcessSettings
	{
		public DataSourceRequestDto DataSourceRequest { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }
		public StatisticsReportExportType StatisticsReportExportType { get; set; }
	}
}
