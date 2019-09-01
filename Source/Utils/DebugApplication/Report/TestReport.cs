using Core.Shared.Extensions;
using System;
using System.Collections.Specialized;
using System.Data;

namespace DebugApplication.Report
{
    public class TestReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query) { { return "TestReport"; } }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        protected override DataSet GetData(NameValueCollection query)
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

        public override long? ObjectId(NameValueCollection query)
        {
            return query["testId"] != null ? query["testId"].ParseToLong() : (long?)null;
        }
    }
}
