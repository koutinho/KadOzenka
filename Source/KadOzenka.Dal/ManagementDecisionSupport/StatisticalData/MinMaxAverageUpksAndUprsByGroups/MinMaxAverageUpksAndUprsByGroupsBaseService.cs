using System.IO;
using Core.ConfigParam;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
    public class MinMaxAverageUpksAndUprsByGroupsBaseService
    {
        protected string GetSqlFileContent(string fileName)
        {
            string contents;
            using (var sr =
                new StreamReader(Configuration.GetFileStream(
                    $"\\StatisticalData\\MinMaxAverageUpksAndUprsByGroups\\{fileName}", "sql", "SqlQueries")))
            {
                contents = sr.ReadToEnd();
            }

            return contents;
        }
    }
}
