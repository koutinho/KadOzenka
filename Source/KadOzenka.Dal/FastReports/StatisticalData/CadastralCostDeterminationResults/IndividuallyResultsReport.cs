using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public class IndividuallyResultsReport : ICadastralCostDeterminationResultsReport
    {
        public string GetTemplateName(NameValueCollection query)
        {
            return "CadastralCostDeterminationIndividuallyResultsReport";
        }

        public List<long?> GetAvailableGroupIds()
        {
	        return OMGroup
		        .Where(x => x.GroupName.ToLower()
			        .Contains(CadastralCostDeterminationResultsMainReport.IndividuallyResultsGroupNamePhrase)).Execute()
		        .Select(x => (long?)x.Id).ToList();
        }
    }
}
