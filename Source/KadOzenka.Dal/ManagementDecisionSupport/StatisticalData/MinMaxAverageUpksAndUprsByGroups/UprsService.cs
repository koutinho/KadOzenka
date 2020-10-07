using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Registers.GbuRegistersServices;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
	public class UprsService : MinMaxAverageUpksAndUprsByGroupsBaseService
    {
		private readonly StatisticalDataService _statisticalDataService;
		private readonly RosreestrRegisterService _rosreestrRegisterService;

        public UprsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
            _rosreestrRegisterService = new RosreestrRegisterService();
        }


        #region ZU

        public List<UprsByGroupsZuDto> GetDataByGroupsForZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, false);
            var result = QSQuery.ExecuteSql<UprsByGroupsZuDto>(sql);

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

            var result = QSQuery.ExecuteSql<UprsByGroupsAndSubGroupsZuDto>(sql);

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
            if (withSubGroups)
            {
                subGroupSelectionFromGrouping = @"temp.""SubGroup"",";
                subGroupSelectionFromQuery = @"(COALESCE(L1_R205.NUMBER, '') || '. ' || L1_R205.GROUP_NAME) as ""SubGroup"",";
                subGroupForGrouping = @", temp.""SubGroup""";
            }

            return string.Format(contents, subGroupSelectionFromGrouping, subGroupSelectionFromQuery,
                string.Join(",", taskIdList), subGroupForGrouping);
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
            var contents = GetSqlFileContent("UprsForOks");

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

            return string.Format(contents, string.Join(",", taskIdList), buildingPurposeAttr.Id,
                placementPurposeAttr.Id, subGroupSelectionFromQuery, subGroupForGrouping, subGroupForResultData,
                subGroupForResult, subGroupForSorting);
        }

        #endregion

    }
}
