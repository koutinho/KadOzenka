using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.ConfigParam;
using KadOzenka.Dal.Helpers;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
    public class MinMaxAverageUpksAndUprsByGroupsBaseService
    {
        protected readonly string SummaryTitle = "Итого по субъекту РФ г Москва";

        #region Entities

        public class GroupingOks
        {
            public string PropertyType { get; set; }
            public bool HasPurpose { get; set; }
            public string Purpose { get; set; }
        }

        protected class GroupingOksByGroupsDictionary
        {
            public GroupingOks Key { get; set; }
            public List<ByGroupsOksDto> Values { get; set; }
        }

        protected class GroupingOksByGroupsAndSubGroupsDictionary
        {
            public GroupingOks Key { get; set; }
            public List<ByGroupsAndSubGroupsOksDto> Values { get; set; }
        }

        #endregion


        protected string GetSqlFileContent(string fileName)
        {
	        var path = PathCombiner.GetFullPath("StatisticalData", "MinMaxAverageUpksAndUprsByGroups", fileName);
            string contents;
            using (var sr = new StreamReader(Configuration.GetFileStream(path, "sql", "SqlQueries")))
            {
                contents = sr.ReadToEnd();
            }

            return contents;
        }

        protected void AddSummaryByGroupsOks(List<ByGroupsOksDto> result)
        {
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
        }

        protected void AddSummaryByGroupsAndSubGroupsOks(List<ByGroupsAndSubGroupsOksDto> result)
        {
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
        }


        #region Support Methods

        private ByGroupsOksDto GetSummaryByGroupsForOks(GroupingOksByGroupsDictionary groupingOksByGroupsDictionary)
        {
            var objectsCount = groupingOksByGroupsDictionary.Values.GroupBy(x => x.ParentGroup)
                .Sum(x => x.FirstOrDefault()?.ObjectsCount ?? 0);

            var calculationInfo = groupingOksByGroupsDictionary.Values.ToList();

            return new ByGroupsOksDto
            {
                ParentGroup = SummaryTitle,
                PropertyType = groupingOksByGroupsDictionary.Key.PropertyType,
                Purpose = groupingOksByGroupsDictionary.Key.Purpose,
                HasPurpose = groupingOksByGroupsDictionary.Key.HasPurpose,
                ObjectsCount = objectsCount,
                Min = calculationInfo.Min(x => x.Min),
                Avg = calculationInfo.Average(x => x.Avg),
                AvgWeight = calculationInfo.Average(x => x.AvgWeight),
                Max = calculationInfo.Max(x => x.Max)
            };
        }

        private ByGroupsAndSubGroupsOksDto GetSummaryByGroupsAndSubGroupsForOks(GroupingOksByGroupsAndSubGroupsDictionary groupingOksByGroupsDictionary)
        {
            var objectCount = groupingOksByGroupsDictionary.Values.GroupBy(x => x.ParentGroup)
                .Sum(x => x.Sum(y => y.ObjectsCount));

            var upksValues = groupingOksByGroupsDictionary.Values.ToList();

            return new ByGroupsAndSubGroupsOksDto
            {
                ParentGroup = SummaryTitle,
                SubGroup = SummaryTitle,
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
