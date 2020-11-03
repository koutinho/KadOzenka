using System.Collections.Generic;
using System.Collections.Specialized;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public interface ICadastralCostDeterminationResultsReport
    {
        string GetTemplateName(NameValueCollection query);
        List<long?> GetAvailableGroupIds();
    }
}
