using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class ParcelsReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "ResultsByCadastralDistrictForParcelsReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            return new DataSet();
        }
    }
}
