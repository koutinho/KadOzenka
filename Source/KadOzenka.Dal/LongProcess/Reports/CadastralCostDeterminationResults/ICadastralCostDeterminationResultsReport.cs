using System.Collections.Generic;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities;
using KadOzenka.Dal.LongProcess.Reports.Entities;

namespace KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults
{
    public interface ICadastralCostDeterminationResultsReport
    {
	    string ReportName { get; }

	    List<long?> GetAvailableGroupIds();
	    List<object> GenerateReportReportRow(int index, ReportItem item);
	    List<Column> GenerateReportHeaders();
    }
}
