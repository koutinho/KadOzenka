using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
    public class UpksAndUprsService
    {
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
                var map = Map(uprs, upks);
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
                var map = Map(uprs, upks);
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


        #region Support Methods

        private ByGroupsAndSubGroupsZuDto Map(UprsByGroupsZuDto uprs, UpksByGroupsZuDto upks)
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

        #endregion
    }
}
