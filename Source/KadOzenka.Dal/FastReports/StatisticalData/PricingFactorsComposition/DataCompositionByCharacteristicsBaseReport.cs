using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class DataCompositionByCharacteristicsBaseReport : StatisticalDataReport
    {
	    protected DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; set; }

	    public DataCompositionByCharacteristicsBaseReport()
	    {
		    DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
	    }


		protected List<T> GetOperations<T>(List<long> taskIds) where T : new()
		{
			var longPerformanceTaskIds = DataCompositionByCharacteristicsService.GetCachedTaskIds();
			var tasIdsForRuntime = taskIds.Except(longPerformanceTaskIds).ToList();
			var tasIdsForCache = longPerformanceTaskIds.Intersect(taskIds).ToList();

			var runtimeResults = new List<T>();
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
