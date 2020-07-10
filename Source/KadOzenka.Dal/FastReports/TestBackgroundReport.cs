using System.Collections.Generic;
using Platform.Reports;
using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports
{
    public class TestBackgroundReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "TestBackgroundReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var dataSet = new DataSet();
            

            return dataSet;
        }
    }
}
