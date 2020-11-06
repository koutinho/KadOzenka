using System;
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

	    public DataCompositionByCharacteristicsBaseReport()
	    {
		    DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
	    }


	    protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
	    {
		    return GetDataCompositionByCharacteristicsReportData(query, objectList);
	    }

	    protected abstract DataSet GetDataCompositionByCharacteristicsReportData(NameValueCollection query, HashSet<long> objectList = null);


		protected List<T> GetOperations<T>(List<long> taskIds, ILogger logger) where T : new()
		{
			var runtimeResults = new List<T>();

			var isCacheTableExists = DataCompositionByCharacteristicsService.IsCacheTableExists();
			logger.ForContext("IsCacheTableExists", isCacheTableExists).Debug($"Поиск кеш-таблицы. Таблица существует {isCacheTableExists}");

			if (isCacheTableExists)
			{
				var longPerformanceTaskIds = DataCompositionByCharacteristicsService.GetCachedTaskIds();
				logger.ForContext("AllCachedTaskIds", longPerformanceTaskIds, true).Debug("ИД задач из кеша");
				
				var tasIdsForRuntime = taskIds.Except(longPerformanceTaskIds).ToList();
				logger.ForContext("RuntimeTaskIds", tasIdsForRuntime, true).Debug("ИД задач для рантайма");

				var tasIdsForCache = longPerformanceTaskIds.Intersect(taskIds).ToList();
				
				if (tasIdsForRuntime.Count != 0)
				{
					var runtimeSql = DataCompositionByCharacteristicsService.GetSqlForRuntime(tasIdsForRuntime);
					logger.Debug(new Exception(runtimeSql), "Sql запрос для рантайма");
					runtimeResults = QSQuery.ExecuteSql<T>(runtimeSql);
				}

				if (tasIdsForCache.Count != 0)
				{
					var cacheSql = DataCompositionByCharacteristicsService.GetSqlForCache(tasIdsForCache);
					logger.Debug(new Exception(cacheSql), "Sql запрос для кеша");
					var cacheResults = QSQuery.ExecuteSql<T>(cacheSql);
					runtimeResults.AddRange(cacheResults);
				}
			}
			else
			{
				var runtimeSql = DataCompositionByCharacteristicsService.GetSqlForRuntime(taskIds);
				logger.Debug(new Exception(runtimeSql), "Sql запрос для рантайма");
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
