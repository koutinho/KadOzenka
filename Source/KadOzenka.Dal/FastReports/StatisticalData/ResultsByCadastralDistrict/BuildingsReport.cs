using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class BuildingsReport : IResultsByCadastralDistrictReport
    {
        public string GetTemplateName(NameValueCollection query)
        {
            return "ResultsByCadastralDistrictForBuildingsReport";
        }

        public DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            return new DataSet();
        }
    }
}
