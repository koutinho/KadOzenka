using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
    public class UpksService : MinMaxAverageUpksAndUprsByGroupsBaseService
    {
        #region Entities

        private class GroupingOks
        {
            public string PropertyType { get; set; }
            public bool HasPurpose { get; set; }
            public string Purpose { get; set; }
        }

        private class GroupingOksByGroupsDictionary
        {
            public GroupingOks Key { get; set; }
            public List<UpksByGroupsOksDto> Values { get; set; }
        }

        private class GroupingOksByGroupsAndSubGroupsDictionary
        {
            public GroupingOks Key { get; set; }
            public List<UpksByGroupsAndSubGroupsOksDto> Values { get; set; }
        }

        #endregion


        private readonly StatisticalDataService _statisticalDataService;

        public UpksService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
        }


        
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

        public List<UpksByGroupsOksDto> GetDataByGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, false);
            var result = QSQuery.ExecuteSql<UpksByGroupsOksDto>(sql);

            var groupingOksDictionaries = result.GroupBy(x => new
            {
                x.Purpose,
                x.HasPurpose,
                x.PropertyType
            }, (k, g) => new GroupingOksByGroupsDictionary
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
                var summary = GetSummaryByGroupsForOks(x);

                result.Add(summary);
            });

            return result;
        }

        public List<UpksByGroupsAndSubGroupsOksDto> GetDataByGroupsAndSubGroupsForOks(long[] taskIdList)
        {
            var sql = GetSqlForOks(taskIdList, true);
            var result = QSQuery.ExecuteSql<UpksByGroupsAndSubGroupsOksDto>(sql);

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


        #region Support Methods

        private string GetSqlForOks(long[] taskIdList, bool withSubGroups)
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

        private static UpksByGroupsOksDto GetSummaryByGroupsForOks(GroupingOksByGroupsDictionary groupingOksByGroupsDictionary)
        {
            var objectCount = groupingOksByGroupsDictionary.Values.GroupBy(x => x.ParentGroup).Sum(x => x.FirstOrDefault()?.ObjectsCount ?? 0);

            var upksValues = groupingOksByGroupsDictionary.Values.ToList();

            return new UpksByGroupsOksDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
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

        private static UpksByGroupsAndSubGroupsOksDto GetSummaryByGroupsAndSubGroupsForOks(GroupingOksByGroupsAndSubGroupsDictionary groupingOksByGroupsDictionary)
        {
            var objectCount = groupingOksByGroupsDictionary.Values.GroupBy(x => x.ParentGroup).Sum(x => x.Sum(y => y.ObjectsCount));

            var upksValues = groupingOksByGroupsDictionary.Values.ToList();

            return new UpksByGroupsAndSubGroupsOksDto
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
