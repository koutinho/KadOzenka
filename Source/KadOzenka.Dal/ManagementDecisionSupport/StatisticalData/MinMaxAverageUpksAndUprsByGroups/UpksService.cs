using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
    public class UpksService : MinMaxAverageUpksAndUprsByGroupsBaseService
    {
        private readonly StatisticalDataService _statisticalDataService;

        public UpksService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
        }



        #region Zu

        public List<UpksByGroupsZuDto> GetDataByGroupsForZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, false);
            var result = QSQuery.ExecuteSql<UpksByGroupsZuDto>(sql);

            var summary = new UpksByGroupsZuDto
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

        public List<UpksByGroupsAndSubGroupsZuDto> GetDataByGroupsAndSubgroupsForZu(long[] taskIdList)
        {
            var sql = GetSqlForZu(taskIdList, true);
            var result = QSQuery.ExecuteSql<UpksByGroupsAndSubGroupsZuDto>(sql);

            var summary = new UpksByGroupsAndSubGroupsZuDto
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
            var contents = GetSqlFileContent("UpksForZu");

            var subGroupSelectionFromQuery = string.Empty;
            var subGroupForGrouping = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromQuery = @"L1_R205.GROUP_NAME as ""SubGroup"",";
                subGroupForGrouping = @", ""SubGroup""";
            }

            return string.Format(contents, subGroupSelectionFromQuery, string.Join(",", taskIdList), subGroupForGrouping);
        }

        #endregion

        #region Oks

        public List<OksByGroupsDto> GetDataByGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, false);

            var result = QSQuery.ExecuteSql<OksByGroupsDto>(sql);

            AddSummaryByGroupsOks(result);

            return result;
        }

        public List<UpksOksByGroupsAndSubGroupsDto> GetDataByGroupsAndSubGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, true);
            var result = QSQuery.ExecuteSql<UpksOksByGroupsAndSubGroupsDto>(sql);

            var groupingOksDictionaries = result.GroupBy(x => new
            {
                x.Purpose,
                x.HasPurpose,
                x.PropertyType
            }, (k, g) => new GroupingOksByGroupsAndSubGroupsDictionary
            {
                Key = new GroupingOks
                {
                    HasPurpose = k.HasPurpose,
                    Purpose = k.Purpose,
                    PropertyType = k.PropertyType
                },
                Values = g.ToList()
            }).ToList();

            groupingOksDictionaries.ForEach(x =>
            {
                var summary = GetSummaryByGroupsAndSubGroupsForOks(x);

                result.Add(summary);
            });

            return result;
        }

        public string GetSqlForOks(long[] taskIdList, bool withSubGroups)
        {
            var contents = GetSqlFileContent("UpksForOks");

            var buildingPurposeAttr = _statisticalDataService.GetRosreestrBuildingPurposeAttribute();
            var placementPurposeAttr = _statisticalDataService.GetRosreestrPlacementPurposeAttribute();

            string subGroupSelectionFromQuery = string.Empty,
                subGroupForGrouping = string.Empty,
                subGroupForResultData = string.Empty,
                subGroupForResult = string.Empty,
                subGroupForSorting = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromQuery = @"L1_R205.GROUP_NAME SubGroup, ";
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


        #region Support Methods

        private static UpksOksByGroupsAndSubGroupsDto GetSummaryByGroupsAndSubGroupsForOks(GroupingOksByGroupsAndSubGroupsDictionary groupingOksByGroupsDictionary)
        {
            var objectCount = groupingOksByGroupsDictionary.Values.GroupBy(x => x.ParentGroup).Sum(x => x.Sum(y => y.ObjectsCount));

            var upksValues = groupingOksByGroupsDictionary.Values.ToList();

            return new UpksOksByGroupsAndSubGroupsDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                SubGroup = "Итого по субъекту РФ г Москва",
                PropertyType = groupingOksByGroupsDictionary.Key.PropertyType,
                Purpose = groupingOksByGroupsDictionary.Key.Purpose,
                HasPurpose = groupingOksByGroupsDictionary.Key.HasPurpose,
                ObjectsCount = objectCount,
                Min = upksValues.Min(x => x.Min),
                Avg = upksValues.Average(x => x.Avg),
                AvgWeight = upksValues.Average(x => x.AvgWeight),
                Max = upksValues.Max(x => x.Max)
            };
        }

        #endregion
    }
}
