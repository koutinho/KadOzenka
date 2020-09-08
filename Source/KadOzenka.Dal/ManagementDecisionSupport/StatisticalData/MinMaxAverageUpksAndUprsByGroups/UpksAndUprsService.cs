using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
    public class UpksAndUprsService
    {
        #region Entities

        protected class GroupingOksByGroupsDictionary
        {
            public GroupingOks Key { get; set; }
            public List<UpksAndUprsByGroupsOksDto> Values { get; set; }
        }

        protected class GroupingOksByGroupsAndSubGroupsDictionary
        {
            public GroupingOks Key { get; set; }
            public List<UpksAndUprsByGroupsAndSubGroupsOksDto> Values { get; set; }
        }

        #endregion

        private readonly UpksService _upksService;
		private readonly UprsService _uprsService;

        public UpksAndUprsService(UpksService upksService, UprsService uprsService)
		{
			_upksService = upksService;
            _uprsService = uprsService;
		}


        #region Zu

        public List<UpksAndUprsByGroupsZuDto> GetDataByGroupsForZu(long[] taskIdList)
        {
            var upksSql = _upksService.GetSqlForZu(taskIdList, false);
            var upksResults = QSQuery.ExecuteSql<UpksByGroupsZuDto>(upksSql);

            var uprsSql = _uprsService.GetSqlForZu(taskIdList, false);
            var uprsResults = QSQuery.ExecuteSql<UprsByGroupsZuDto>(uprsSql);

            var result = new List<UpksAndUprsByGroupsZuDto>();
            foreach (var upks in upksResults)
            {
                var uprs = uprsResults.FirstOrDefault(x => x.ParentGroup == upks.ParentGroup);
                var map = MapZu(uprs, upks);
                result.Add(map);
            }

            result.Add(new UpksAndUprsByGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Upks = new CalculationInfoDto
                {
                    Min = result.Min(x => x.Upks.Min),
                    Avg = result.Average(x => x.Upks.Avg),
                    AvgWeight = result.Average(x => x.Upks.AvgWeight),
                    Max = result.Max(x => x.Upks.Max)
                },
                Uprs = new CalculationInfoDto
                {
                    Min = result.Min(x => x.Uprs.Min),
                    Avg = result.Average(x => x.Uprs.Avg),
                    AvgWeight = result.Average(x => x.Uprs.AvgWeight),
                    Max = result.Max(x => x.Uprs.Max)
                }
            });

            return result;
        }

        public List<ByGroupsAndSubGroupsZuDto> GetDataByGroupsAndSubGroupsForZu(long[] taskIdList)
        {
            var upksSql = _upksService.GetSqlForZu(taskIdList, true);
            var upksResults = QSQuery.ExecuteSql<UpksByGroupsAndSubGroupsZuDto>(upksSql);

            var uprsSql = _uprsService.GetSqlForZu(taskIdList, true);
            var uprsResults = QSQuery.ExecuteSql<UprsByGroupsAndSubGroupsZuDto>(uprsSql);

            var result = new List<ByGroupsAndSubGroupsZuDto>();
            foreach (var upks in upksResults)
            {
                var uprs = uprsResults.FirstOrDefault(x => x.ParentGroup == upks.ParentGroup && x.SubGroup == upks.SubGroup);
                var map = MapZu(uprs, upks);
                map.SubGroup = uprs?.SubGroup;
                result.Add(map);
            }

            result.Add(new ByGroupsAndSubGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                SubGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Upks = new CalculationInfoDto
                {
                    Min = result.Min(x => x.Upks.Min),
                    Avg = result.Average(x => x.Upks.Avg),
                    AvgWeight = result.Average(x => x.Upks.AvgWeight),
                    Max = result.Max(x => x.Upks.Max)
                },
                Uprs = new CalculationInfoDto
                {
                    Min = result.Min(x => x.Uprs.Min),
                    Avg = result.Average(x => x.Uprs.Avg),
                    AvgWeight = result.Average(x => x.Uprs.AvgWeight),
                    Max = result.Max(x => x.Uprs.Max)
                }
            });

            return result;
        }

        #endregion


        #region Oks

        public List<UpksAndUprsByGroupsOksDto> GetDataByGroupsForOks(long[] taskIdList)
        {
            var upksSql = _upksService.GetSqlForOks(taskIdList, false);
            var upksResults = QSQuery.ExecuteSql<ByGroupsOksDto>(upksSql);

            var uprsSql = _uprsService.GetSqlForOks(taskIdList, false);
            var uprsResults = QSQuery.ExecuteSql<ByGroupsOksDto>(uprsSql);

            var result = new List<UpksAndUprsByGroupsOksDto>();
            foreach (var upks in upksResults)
            {
                var uprs = uprsResults.FirstOrDefault(x =>
                    x.ParentGroup == upks.ParentGroup && x.Purpose == upks.Purpose && x.HasPurpose == upks.HasPurpose &&
                    x.PropertyType == upks.PropertyType);

                var map = MapOks(uprs, upks);

                result.Add(map);
            }

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

            groupingOksDictionaries.ForEach(group =>
            {
                var objectsCount = group.Values.GroupBy(y => y.ParentGroup)
                    .Sum(y => y.FirstOrDefault()?.ObjectsCount ?? 0);

                var calculationInfo = group.Values.ToList();
                var upksCalculationInfo = calculationInfo.Select(x => x.Upks).ToList();
                var uprsCalculationInfo = calculationInfo.Select(x => x.Uprs).ToList();

                var summary = GetSummary(group.Key, objectsCount, upksCalculationInfo, uprsCalculationInfo);

                result.Add(summary);
            });

            return result;
        }

        public List<UpksAndUprsByGroupsAndSubGroupsOksDto> GetDataByGroupsAndSubGroupsForOks(long[] taskIdList)
        {
            var upksSql = _upksService.GetSqlForOks(taskIdList, true);
            var upksResults = QSQuery.ExecuteSql<ByGroupsAndSubGroupsOksDto>(upksSql);

            var uprsSql = _uprsService.GetSqlForOks(taskIdList, true);
            var uprsResults = QSQuery.ExecuteSql<ByGroupsAndSubGroupsOksDto>(uprsSql);

            var result = new List<UpksAndUprsByGroupsAndSubGroupsOksDto>();
            foreach (var upks in upksResults)
            {
                var uprs = uprsResults.FirstOrDefault(x =>
                    x.ParentGroup == upks.ParentGroup && 
                    x.SubGroup == upks.SubGroup && 
                    x.Purpose == upks.Purpose && x.HasPurpose == upks.HasPurpose &&
                    x.PropertyType == upks.PropertyType);

                var map = MapOks(uprs, upks);
                map.SubGroup = upks?.SubGroup;

                result.Add(map);
            }

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

            groupingOksDictionaries.ForEach(group =>
            {
                var objectsCount = group.Values.GroupBy(y => y.ParentGroup)
                    .Sum(x => x.Sum(y => y.ObjectsCount));

                var calculationInfo = group.Values.ToList();
                var upksCalculationInfo = calculationInfo.Select(x => x.Upks).ToList();
                var uprsCalculationInfo = calculationInfo.Select(x => x.Uprs).ToList();

                var summary = GetSummary(group.Key, objectsCount, upksCalculationInfo, uprsCalculationInfo);

                result.Add(summary);
            });

            return result;
        }

        #endregion


        #region Support Methods

        private ByGroupsAndSubGroupsZuDto MapZu(UprsByGroupsZuDto uprs, UpksByGroupsZuDto upks)
        {
            return new ByGroupsAndSubGroupsZuDto
            {
                ParentGroup = uprs?.ParentGroup,
                ObjectsCount = uprs?.ObjectsCount ?? 0,
                Upks = new CalculationInfoDto
                {
                    Min = upks.Min,
                    Avg = upks.Avg,
                    AvgWeight = upks.AvgWeight,
                    Max = upks.Max
                },
                Uprs = new CalculationInfoDto
                {
                    Min = uprs?.Min,
                    Avg = uprs?.Avg,
                    AvgWeight = uprs?.AvgWeight,
                    Max = uprs?.Max
                }
            };
        }

        private UpksAndUprsByGroupsAndSubGroupsOksDto MapOks(ByGroupsOksDto uprs, ByGroupsOksDto upks)
        {
            return new UpksAndUprsByGroupsAndSubGroupsOksDto
            {
                ParentGroup = uprs?.ParentGroup,
                PropertyType = uprs?.PropertyType,
                Purpose = uprs?.Purpose,
                HasPurpose = uprs?.HasPurpose ?? false,
                ObjectsCount = uprs?.ObjectsCount ?? 0,
                Upks = new CalculationInfoDto
                {
                    Min = upks.Min,
                    Avg = upks.Avg,
                    AvgWeight = upks.AvgWeight,
                    Max = upks.Max
                },
                Uprs = new CalculationInfoDto
                {
                    Min = uprs?.Min,
                    Avg = uprs?.Avg,
                    AvgWeight = uprs?.AvgWeight,
                    Max = uprs?.Max
                }
            };
        }

        private static UpksAndUprsByGroupsAndSubGroupsOksDto GetSummary(GroupingOks groupKey, int objectsCount,
            List<CalculationInfoDto> upksCalculationInfo, List<CalculationInfoDto> uprsCalculationInfo)
        {
            return new UpksAndUprsByGroupsAndSubGroupsOksDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                SubGroup = "Итого по субъекту РФ г Москва",
                PropertyType = groupKey.PropertyType,
                Purpose = groupKey.Purpose,
                HasPurpose = groupKey.HasPurpose,
                ObjectsCount = objectsCount,
                Upks = new CalculationInfoDto
                {
                    Min = upksCalculationInfo.Min(y => y.Min),
                    Avg = upksCalculationInfo.Average(y => y.Avg),
                    AvgWeight = upksCalculationInfo.Average(y => y.AvgWeight),
                    Max = upksCalculationInfo.Max(y => y.Max)
                },
                Uprs = new CalculationInfoDto
                {
                    Min = uprsCalculationInfo.Min(y => y.Min),
                    Avg = uprsCalculationInfo.Average(y => y.Avg),
                    AvgWeight = uprsCalculationInfo.Average(y => y.AvgWeight),
                    Max = uprsCalculationInfo.Max(y => y.Max)
                }
            };
        }

        #endregion
    }
}
