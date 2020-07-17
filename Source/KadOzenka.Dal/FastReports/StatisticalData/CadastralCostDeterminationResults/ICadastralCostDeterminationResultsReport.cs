using System.Collections.Generic;
using System.Collections.Specialized;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public interface ICadastralCostDeterminationResultsReport
    {
        string GetTemplateName(NameValueCollection query);
        List<OMUnit> GetUnitsForCadastralCostDetermination(List<long> taskIds);
    }
}
