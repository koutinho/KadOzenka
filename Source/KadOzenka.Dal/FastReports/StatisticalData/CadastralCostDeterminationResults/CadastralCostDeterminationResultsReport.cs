using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public class CadastralCostDeterminationResultsReport : ICadastralCostDeterminationResultsReport
    {
        public string GetTemplateName(NameValueCollection query)
        {
            return "CadastralCostDeterminationResultsReport";
        }

        public DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            return new DataSet();
        }
    }
}
