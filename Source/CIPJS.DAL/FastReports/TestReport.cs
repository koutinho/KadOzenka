using Platform.Reports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace CIPJS.DAL.FastReports
{
    public class TestReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query) { { return "TestReport"; } }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("TestPrint");
            dataSet.Tables[0].Columns.Add("Title");
            dataSet.Tables[0].Columns.Add("DatePeriod");
            dataSet.Tables[0].Columns.Add("Author");

            dataSet.Tables[0].Rows.Add("Заголовок тестового отчета",
                DateTime.Now.ToString("MMMM yyyy", CultureRu).ToLower(),
                "Тестер");

            return dataSet;
        }
    }
}