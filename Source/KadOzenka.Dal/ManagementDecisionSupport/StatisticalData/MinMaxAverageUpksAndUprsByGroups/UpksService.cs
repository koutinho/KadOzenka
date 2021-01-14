using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Registers.GbuRegistersServices;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
    public class UpksService : MinMaxAverageUpksAndUprsByGroupsBaseService
    {
	    public readonly CancellationManager CancellationManager;
        private readonly StatisticalDataService _statisticalDataService;
        private readonly RosreestrRegisterService _rosreestrRegisterService;
        
        public UpksService(StatisticalDataService statisticalDataService)
		{
			CancellationManager = new CancellationManager();
            _statisticalDataService = statisticalDataService;
            _rosreestrRegisterService = new RosreestrRegisterService();
        }



        #region Zu

        public List<UpksByGroupsZuDto> GetDataByGroupsForZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, false);
            var result = CancellationManager.ExecuteSql<UpksByGroupsZuDto>(sql);

            var summary = new UpksByGroupsZuDto
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

        public List<UpksByGroupsAndSubGroupsZuDto> GetDataByGroupsAndSubgroupsForZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, true);
            var result = CancellationManager.ExecuteSql<UpksByGroupsAndSubGroupsZuDto>(sql);

            var summary = new UpksByGroupsAndSubGroupsZuDto
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
            var contents = GetSqlFileContent("UpksForZu");

            var subGroupSelectionFromQuery = string.Empty;
            var subGroupForGrouping = string.Empty;
            var subGroupForOrdering = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromQuery = @"case when L1_R205.NUMBER is null then L1_R205.GROUP_NAME else CONCAT(L1_R205.NUMBER, '. ', L1_R205.GROUP_NAME) end as ""SubGroup"", min(nullif(split_part(L1_R205.NUMBER, '.', 2), '')::numeric) AS SubGroupNumber,";
                subGroupForGrouping = @", ""SubGroup""";
                subGroupForOrdering = @", SubGroupNumber";
            }

            return string.Format(contents, subGroupSelectionFromQuery, string.Join(",", taskIdList), subGroupForGrouping, subGroupForOrdering);
        }

        #endregion


        #region Oks

        public List<ByGroupsOksDto> GetDataByGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, false);

            var result = CancellationManager.ExecuteSql<ByGroupsOksDto>(sql);

            AddSummaryByGroupsOks(result);

            return result;
        }

        public List<ByGroupsAndSubGroupsOksDto> GetDataByGroupsAndSubGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, true);
            var result = CancellationManager.ExecuteSql<ByGroupsAndSubGroupsOksDto>(sql);

            AddSummaryByGroupsAndSubGroupsOks(result);

            return result;
        }

        public string GetSqlForOks(long[] taskIdList, bool withSubGroups)
        {
            var contents = GetSqlFileContent("UpksForOks");

            var buildingPurposeAttr = _rosreestrRegisterService.GetBuildingPurposeAttribute();
            var placementPurposeAttr = _rosreestrRegisterService.GetPlacementPurposeAttribute();

            string subGroupSelectionFromQuery = string.Empty,
                subGroupForGrouping = string.Empty,
                subGroupNumber = string.Empty,
                subGroupForResultData = string.Empty,
                subGroupForResult = string.Empty,
                subGroupForResultGrouping = string.Empty,
                subGroupForSorting = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromQuery = @"(case when L1_R205.NUMBER is null then L1_R205.GROUP_NAME else CONCAT(L1_R205.NUMBER, '. ', L1_R205.GROUP_NAME) end) SubGroup, L1_R205.NUMBER AS SubGroupNumber,";
                subGroupForGrouping = @"d.SubGroup, ";
                subGroupNumber = @"min(nullif(split_part(d.SubGroupNumber, '.', 2), '')::numeric) as SubGroupNumber, ";
                subGroupForResultData = @"dg.SubGroup, dg.SubGroupNumber, ";
                subGroupForResult = @"rd.SubGroup, rd.SubGroupNumber, ";
                subGroupForSorting = @"SubGroupNumber, ";
                subGroupForResultGrouping = @"SubGroup, ";
            }

            return string.Format(contents, string.Join(", ", taskIdList), buildingPurposeAttr.Id,
                placementPurposeAttr.Id, subGroupSelectionFromQuery, subGroupForGrouping, subGroupNumber, subGroupForResultData,
                subGroupForResult, subGroupForSorting, subGroupForResultGrouping);
        }

        #endregion
    }
}
