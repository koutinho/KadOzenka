using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;
using KadOzenka.Dal.Registers;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
    public class UpksService : MinMaxAverageUpksAndUprsByGroupsBaseService
    {
        private readonly StatisticalDataService _statisticalDataService;
        private readonly RosreestrRegisterService _rosreestrRegisterService;

        public UpksService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
            _rosreestrRegisterService = new RosreestrRegisterService();
        }



        #region Zu

        public List<UpksByGroupsZuDto> GetDataByGroupsForZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, false);
            var result = QSQuery.ExecuteSql<UpksByGroupsZuDto>(sql);

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
            var result = QSQuery.ExecuteSql<UpksByGroupsAndSubGroupsZuDto>(sql);

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
            if (withSubGroups)
            {
                subGroupSelectionFromQuery = @"(COALESCE(L1_R205.NUMBER, '') || '. ' || L1_R205.GROUP_NAME) as ""SubGroup"",";
                subGroupForGrouping = @", ""SubGroup""";
            }

            return string.Format(contents, subGroupSelectionFromQuery, string.Join(",", taskIdList), subGroupForGrouping);
        }

        #endregion


        #region Oks

        public List<ByGroupsOksDto> GetDataByGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, false);

            var result = QSQuery.ExecuteSql<ByGroupsOksDto>(sql);

            AddSummaryByGroupsOks(result);

            return result;
        }

        public List<ByGroupsAndSubGroupsOksDto> GetDataByGroupsAndSubGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, true);
            var result = QSQuery.ExecuteSql<ByGroupsAndSubGroupsOksDto>(sql);

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
                subGroupForResultData = string.Empty,
                subGroupForResult = string.Empty,
                subGroupForSorting = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromQuery = @"(COALESCE(L1_R205.NUMBER, '') || '. ' || L1_R205.GROUP_NAME) SubGroup, ";
                subGroupForGrouping = @"d.SubGroup, ";
                subGroupForResultData = @"dg.SubGroup, ";
                subGroupForResult = @"rd.SubGroup, ";
                subGroupForSorting = @"SubGroup, ";
            }

            return string.Format(contents, string.Join(", ", taskIdList), buildingPurposeAttr.Id,
                placementPurposeAttr.Id, subGroupSelectionFromQuery, subGroupForGrouping, subGroupForResultData,
                subGroupForResult, subGroupForSorting);
        }

        #endregion
    }
}
