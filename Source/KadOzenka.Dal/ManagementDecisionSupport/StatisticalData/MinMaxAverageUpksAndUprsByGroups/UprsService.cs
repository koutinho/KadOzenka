using System.Collections.Generic;
using System.Linq;
using CommonSdks;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Registers.GbuRegistersServices;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
	public class UprsService : MinMaxAverageUpksAndUprsByGroupsBaseService
	{
		public QueryManager QueryManager;
		private readonly StatisticalDataService _statisticalDataService;
		private readonly RosreestrRegisterService _rosreestrRegisterService;

        public UprsService(StatisticalDataService statisticalDataService)
		{
            QueryManager = new QueryManager();
			_statisticalDataService = statisticalDataService;
            _rosreestrRegisterService = new RosreestrRegisterService();
        }


        #region ZU

        public List<UprsByGroupsZuDto> GetDataByGroupsForZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, false);
            var result = QueryManager.ExecuteSql<UprsByGroupsZuDto>(sql);

            var summary = new UprsByGroupsZuDto
            {
                ParentGroup = SummaryTitle,
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Min = result.Min(x => x.Min),
                Max = result.Max(x => x.Max),
                Avg = result.Average(x => x.Avg),
                AvgWeight = result.Average(x => x.AvgWeight)
            };
            result.Add(summary);

            return result;
        }

        public List<UprsByGroupsAndSubGroupsZuDto> GetDataByGroupsAndSubgroupsForZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, true);

            var result = QueryManager.ExecuteSql<UprsByGroupsAndSubGroupsZuDto>(sql);

            var summary = new UprsByGroupsAndSubGroupsZuDto
            {
                ParentGroup = SummaryTitle,
                SubGroup = SummaryTitle,
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
            var subGroupForSorting = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromGrouping = @"temp.""SubGroup"", ";
                subGroupSelectionFromQuery = @"case when L1_R205.NUMBER is null then L1_R205.GROUP_NAME else CONCAT(L1_R205.NUMBER, '. ', L1_R205.GROUP_NAME) end as ""SubGroup"", L1_R205.NUMBER as SubGroupNumber, ";
                subGroupForGrouping = @", temp.""SubGroup""";
                subGroupForSorting = @", min(nullif(split_part(temp.SubGroupNumber, '.', 2), '')::numeric)";
            }

            return string.Format(contents, subGroupSelectionFromGrouping, subGroupSelectionFromQuery,
                string.Join(",", taskIdList), subGroupForGrouping, subGroupForSorting);
        }

        #endregion


        #region Oks

        public List<ByGroupsOksDto> GetDataByGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, false);
            var result = QueryManager.ExecuteSql<ByGroupsOksDto>(sql);

            AddSummaryByGroupsOks(result);

            return result;
        }

        public List<ByGroupsAndSubGroupsOksDto> GetDataByGroupsAndSubGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, true);
            var result = QueryManager.ExecuteSql<ByGroupsAndSubGroupsOksDto>(sql);

            AddSummaryByGroupsAndSubGroupsOks(result);

            return result;
        }

        public string GetSqlForOks(long[] taskIdList, bool withSubGroups)
        {
            var contents = GetSqlFileContent("UprsForOks");

            var buildingPurposeAttr = _rosreestrRegisterService.GetBuildingPurposeAttribute();
            var placementPurposeAttr = _rosreestrRegisterService.GetPlacementPurposeAttribute();

            string subGroupSelectionFromQuery = string.Empty,
	            subGroupForGrouping = string.Empty,
	            subGroupNumberForGrouping = string.Empty,
	            subGroupForResultData = string.Empty,
	            subGroupForSorting = string.Empty,
	            subGroupForResultGrouping = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromQuery = @"(case when L1_R205.NUMBER is null then L1_R205.GROUP_NAME else CONCAT(L1_R205.NUMBER, '. ', L1_R205.GROUP_NAME) end) SubGroup, L1_R205.NUMBER AS SubGroupNumber, ";
                subGroupForGrouping = @"d.SubGroup, ";
                subGroupNumberForGrouping = @"min(nullif(split_part(d.SubGroupNumber, '.', 2), '')::numeric) as SubGroupNumber, ";
                subGroupForResultData = @"dg.SubGroup, dg.SubGroupNumber, ";
                subGroupForSorting = @"SubGroupNumber, ";
                subGroupForResultGrouping = @"SubGroup, ";
            }

            return string.Format(contents, string.Join(",", taskIdList), buildingPurposeAttr.Id,
                placementPurposeAttr.Id, subGroupSelectionFromQuery, subGroupForGrouping, subGroupNumberForGrouping, subGroupForResultData,
                 subGroupForSorting, subGroupForResultGrouping);
        }

        #endregion

    }
}
