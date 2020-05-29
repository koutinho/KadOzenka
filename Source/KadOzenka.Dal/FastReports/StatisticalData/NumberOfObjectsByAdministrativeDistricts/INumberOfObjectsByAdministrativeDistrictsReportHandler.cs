using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports.StatisticalData.NumberOfObjectsByAdministrativeDistricts
{
	public interface INumberOfObjectsByAdministrativeDistrictsReportHandler
	{
		string GetTemplateName(NameValueCollection query, IGetQueryPAramFunc getQueryParam);
		DataSet GetData(long[] taskList, NameValueCollection query, IGetQueryPAramFunc getQueryParam);
	}
}
