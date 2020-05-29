using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public interface IResultsByCadastralDistrictReport
    {
        string GetTemplateName(NameValueCollection query);
        DataSet GetData(NameValueCollection query, HashSet<long> objectList = null);
    }
}
