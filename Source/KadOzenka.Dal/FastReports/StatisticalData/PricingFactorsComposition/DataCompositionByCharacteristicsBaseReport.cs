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
			var result = new List<T>();

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
					logger.Debug("Начата работа в рантайме");

					var runtimeSql = DataCompositionByCharacteristicsService.GetSqlForRuntime(tasIdsForRuntime);
					var runtimeResults = GetRuntimeResults<T>(runtimeSql, logger);
					result.AddRange(runtimeResults);

					logger.Debug("Закончена работа в рантайме");
				}

				if (tasIdsForCache.Count != 0)
				{
					logger.Debug("Начата работа через кеш");

					var cacheSql = DataCompositionByCharacteristicsService.GetSqlForCache(tasIdsForCache);
					var cacheResults = GetRuntimeResults<T>(cacheSql, logger);
					result.AddRange(cacheResults);

					logger.Debug("Закончена работа через кеш");
				}
			}
			else
			{
				var runtimeSql = DataCompositionByCharacteristicsService.GetSqlForRuntime(taskIds);
				var runtimeResults = GetRuntimeResults<T>(runtimeSql, logger);
				result.AddRange(runtimeResults);
			}

			return result;
		}


		#region Support Methods

		private List<T> GetRuntimeResults<T>(string sql, ILogger logger) where T : new()
		{
			logger.Debug(new Exception(sql), "Sql запрос");

			var result = QSQuery.ExecuteSql<T>(sql);

			return result;
		}

		#endregion


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
