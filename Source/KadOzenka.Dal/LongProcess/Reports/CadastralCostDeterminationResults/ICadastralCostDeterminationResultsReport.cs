using System.Collections.Generic;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities;

namespace KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults
{
    public interface ICadastralCostDeterminationResultsReport
    {
	    string ReportName { get; }

	    List<long?> GetAvailableGroupIds();
	    List<object> GenerateReportReportRow(int index, ReportItem item);
	    List<GbuReportService.Column> GenerateReportHeaders(List<ReportItem> info);
    }
}
