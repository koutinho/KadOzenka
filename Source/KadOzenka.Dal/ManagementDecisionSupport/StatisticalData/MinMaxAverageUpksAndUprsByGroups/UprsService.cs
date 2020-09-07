using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
	public class UprsService : MinMaxAverageUpksAndUprsByGroupsBaseService
    {
		private readonly StatisticalDataService _statisticalDataService;

        public UprsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
        }


        public List<UprsByGroupsZuDto> GetDataByGroupsUprsZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, false);
            var result = QSQuery.ExecuteSql<UprsByGroupsZuDto>(sql);

            var summary = new UprsByGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Min = result.Min(x => x.Min),
                Max = result.Max(x => x.Max),
                Avg = result.Average(x => x.Avg),
                AvgWeight = result.Average(x => x.AvgWeight)
            };
            result.Add(summary);

            return result;
        }

        public List<UprsByGroupsAndSubGroupsZuDto> GetDataByGroupsAndSubgroupsUprsZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, true);

            var result = QSQuery.ExecuteSql<UprsByGroupsAndSubGroupsZuDto>(sql);

            var summary = new UprsByGroupsAndSubGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                SubGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Min = result.Min(x => x.Min),
                Max = result.Max(x => x.Max),
                Avg = result.Average(x => x.Avg),
                AvgWeight = result.Average(x => x.AvgWeight)
            };
            result.Add(summary);

            return result;
        }

        public string GetSqlForZu(long[] taskIdList, bool withSubGroups)
        {
            var contents = GetSqlFileContent("UprsForZu");

            var subGroupSelectionFromGrouping = string.Empty;
            var subGroupSelectionFromQuery = string.Empty;
            var subGroupForGrouping = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromGrouping = @"temp.""SubGroup"",";
                subGroupSelectionFromQuery = @"L1_R205.GROUP_NAME as ""SubGroup"",";
                subGroupForGrouping = @", temp.""SubGroup""";
            }

            return string.Format(contents, subGroupSelectionFromGrouping, subGroupSelectionFromQuery,
                string.Join(",", taskIdList), subGroupForGrouping);
        }
    }
}
