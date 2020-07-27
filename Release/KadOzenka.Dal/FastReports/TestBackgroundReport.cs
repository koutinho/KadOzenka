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
            var testInputParameter = GetQueryParam<string>("TestInputParameter", query);

            var dataTable = new DataTable("COMMON");
            dataTable.Columns.Add("TestValue");
            dataTable.Columns.Add("InputParameter");

            dataTable.Rows.Add("WORKS!", testInputParameter);

            var dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);

            return dataSet;
        }
    }
}
