using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class BuildingsReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "ResultsByCadastralDistrictForBuildingsReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            return new DataSet();
        }
    }
}
