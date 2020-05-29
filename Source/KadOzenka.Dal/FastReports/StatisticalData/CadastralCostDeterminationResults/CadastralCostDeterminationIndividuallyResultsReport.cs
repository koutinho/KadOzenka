using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public class CadastralCostDeterminationIndividuallyResultsReport : ICadastralCostDeterminationResultsReport
    {
        public string GetTemplateName(NameValueCollection query)
        {
            return "CadastralCostDeterminationIndividuallyResultsReport";
        }

        public DataSet GetData(NameValueCollection query, List<long> taskIds)
        {
            return new DataSet();
        }
    }
}
