using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public interface ICadastralCostDeterminationResultsReport
    {
        string GetTemplateName(NameValueCollection query);
        DataSet GetData(NameValueCollection query, List<long> taskIds);
    }
}
