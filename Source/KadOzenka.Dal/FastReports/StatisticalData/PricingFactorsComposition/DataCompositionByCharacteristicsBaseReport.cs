using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public abstract class DataCompositionByCharacteristicsBaseReport : StatisticalDataReport
    {
	    protected DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; set; }
	    private readonly ILogger _logger;
	    protected override ILogger Logger => _logger;

	    public DataCompositionByCharacteristicsBaseReport()
	    {
		    DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
		    _logger = Log.ForContext<DataCompositionByCharacteristicsBaseReport>();
		}

	    protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
	    {
		    return GetDataCompositionByCharacteristicsReportData(query, objectList);
	    }

	    protected abstract DataSet GetDataCompositionByCharacteristicsReportData(NameValueCollection query, HashSet<long> objectList = null);


		protected List<T> GetOperations<T>(List<long> taskIds) where T : new()
		{
			var runtimeResults = new List<T>();

			if (DataCompositionByCharacteristicsService.IsCacheTableExists())
			{
				var longPerformanceTaskIds = DataCompositionByCharacteristicsService.GetCachedTaskIds();
				var tasIdsForRuntime = taskIds.Except(longPerformanceTaskIds).ToList();
				var tasIdsForCache = longPerformanceTaskIds.Intersect(taskIds).ToList();

				if (tasIdsForRuntime.Count != 0)
				{
					var runtimeSql = DataCompositionByCharacteristicsService.GetSqlForRuntime(tasIdsForRuntime);
					runtimeResults = QSQuery.ExecuteSql<T>(runtimeSql);
				}

				if (tasIdsForCache.Count != 0)
				{
					var cacheSql = DataCompositionByCharacteristicsService.GetSqlForCache(tasIdsForCache);
					var cacheResults = QSQuery.ExecuteSql<T>(cacheSql);
					runtimeResults.AddRange(cacheResults);
				}
			}
			else
			{
				var runtimeSql = DataCompositionByCharacteristicsService.GetSqlForRuntime(taskIds);
				runtimeResults = QSQuery.ExecuteSql<T>(runtimeSql);
			}

			return runtimeResults;
		}


		#region Entites

		protected class Attribute
		{
			public string Name { get; set; }
			public string RegisterName { get; set; }
			public long RegisterId { get; set; }
		}

		#endregion
	}
}
