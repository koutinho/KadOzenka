using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public class StateResultsReport : ICadastralCostDeterminationResultsReport
    {
        string ICadastralCostDeterminationResultsReport.GetTemplateName(NameValueCollection query)
        {
            return "CadastralCostDeterminationResultsReport";
        }

        public List<long?> GetAvailableGroupIds()
        {
            return OMGroup
	            .Where(x => !x.GroupName.ToLower()
		            .Contains(CadastralCostDeterminationResultsMainReport.IndividuallyResultsGroupNamePhrase)).Execute()
	            .Select(x => (long?) x.Id).ToList();
        }
    }
}
