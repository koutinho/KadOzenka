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
	    private const int PackageSize = 5000;
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

			var unitsCount = DataCompositionByCharacteristicsService.GetUnitsCount(taskIds);
			logger.Debug($"Количество Единиц оценки для отчета {unitsCount}");

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
					var runtimeResults = GetResults<T>(runtimeSql, logger);
					result.AddRange(runtimeResults);

					logger.Debug("Закончена работа в рантайме");
				}

				if (tasIdsForCache.Count != 0)
				{
					logger.Debug("Начата работа через кеш");

					var cacheSql = DataCompositionByCharacteristicsService.GetSqlForCache(tasIdsForCache);
					var cacheResults = GetResults<T>(cacheSql, logger);
					result.AddRange(cacheResults);

					logger.Debug("Закончена работа через кеш");
				}
			}
			else
			{
				var runtimeSql = DataCompositionByCharacteristicsService.GetSqlForRuntime(taskIds);
				var runtimeResults = GetResults<T>(runtimeSql, logger);
				result.AddRange(runtimeResults);
			}

			return result;
		}


		#region Support Methods

		private List<T> GetResults<T>(string sql, ILogger logger) where T : new()
		{
			logger.Debug(new Exception(sql), "Общий Sql запрос (без пагинации)");

			var packageIndex = 0;
			var result = new List<T>();
			for (var i = packageIndex * PackageSize; i < (packageIndex + 1) * PackageSize; i++)
			{
				var offset = packageIndex * PackageSize;
				var sqlWithPackage = $"{sql} \nlimit {PackageSize} offset {offset}";
				logger.Debug(new Exception(sqlWithPackage), $"Начата обработка пакета с индексом {i}, до этого было выгружено {offset} записей");

				var package = QSQuery.ExecuteSql<T>(sqlWithPackage);
				if (package.Count == 0)
					break;

				result.AddRange(package);

				packageIndex++;
			}

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
