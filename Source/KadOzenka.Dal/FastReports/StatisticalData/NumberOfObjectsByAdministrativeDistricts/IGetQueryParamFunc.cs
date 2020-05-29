using System.Collections.Specialized;

namespace KadOzenka.Dal.FastReports.StatisticalData.NumberOfObjectsByAdministrativeDistricts
{
	public interface IGetQueryPAramFunc
	{
		T Call<T>(string paramName, NameValueCollection query);
	}
}
