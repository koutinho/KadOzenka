using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public class NumberOfObjectsByZoneAndSubgroupsReport : StatisticalDataReport
	{
		private readonly StatisticalDataService _statisticalDataService;

		public NumberOfObjectsByZoneAndSubgroupsReport()
		{
			_statisticalDataService = new StatisticalDataService();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(NumberOfObjectsByZoneAndSubgroupsReport);
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			throw new NotImplementedException();
		}
	}
}
