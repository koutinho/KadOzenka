using KadOzenka.Dal.FastReports.StatisticalData.Common;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class PricingFactorsCompositionBaseReport : StatisticalDataReport
    {
        protected readonly string BaseFolderWithSql = "PricingFactorsComposition";

        protected string GetSqlFileContent(string fileName)
        {
            return StatisticalDataService.GetSqlFileContent(BaseFolderWithSql, fileName);
        }
    }
}
