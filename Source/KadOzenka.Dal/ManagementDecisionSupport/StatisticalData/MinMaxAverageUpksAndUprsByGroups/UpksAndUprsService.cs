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

        #endregion

        private readonly UpksService _upksService;
		private readonly UprsService _uprsService;

        public UpksAndUprsService(UpksService upksService, UprsService uprsService)
		{
			_upksService = upksService;
            _uprsService = uprsService;
		}


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

        public List<UpksAndUprsByGroupsOksDto> GetDataByGroupsForOks(long[] taskIdList)
        {
            var upksSql = _upksService.GetSqlForOks(taskIdList, false);
            var upksResults = QSQuery.ExecuteSql<OksByGroupsDto>(upksSql);

            var uprsSql = _uprsService.GetSqlForOks(taskIdList, false);
            var uprsResults = QSQuery.ExecuteSql<OksByGroupsDto>(uprsSql);

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

            groupingOksDictionaries.ForEach(x =>
            {
                var objectsCount = x.Values.GroupBy(y => y.ParentGroup)
                    .Sum(y => y.FirstOrDefault()?.ObjectsCount ?? 0);

                var calculationInfo = x.Values.ToList();

                var summary = new UpksAndUprsByGroupsOksDto
                {
                    ParentGroup = "Итого по субъекту РФ г Москва",
                    PropertyType = x.Key.PropertyType,
                    Purpose = x.Key.Purpose,
                    HasPurpose = x.Key.HasPurpose,
                    ObjectsCount = objectsCount,
                    Upks = new CalculationInfoDto
                    {
                        Min = calculationInfo.Min(y => y.Upks.Min),
                        Avg = calculationInfo.Average(y => y.Upks.Avg),
                        AvgWeight = calculationInfo.Average(y => y.Upks.AvgWeight),
                        Max = calculationInfo.Max(y => y.Upks.Max)
                    },
                    Uprs = new CalculationInfoDto
                    {
                        Min = calculationInfo.Min(y => y.Uprs.Min),
                        Avg = calculationInfo.Average(y => y.Uprs.Avg),
                        AvgWeight = calculationInfo.Average(y => y.Uprs.AvgWeight),
                        Max = calculationInfo.Max(y => y.Uprs.Max)
                    }
                };

                result.Add(summary);
            });

            return result;
        }


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

        private UpksAndUprsByGroupsOksDto MapOks(OksByGroupsDto uprs, OksByGroupsDto upks)
        {
            return new UpksAndUprsByGroupsOksDto
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

        #endregion
    }
}
