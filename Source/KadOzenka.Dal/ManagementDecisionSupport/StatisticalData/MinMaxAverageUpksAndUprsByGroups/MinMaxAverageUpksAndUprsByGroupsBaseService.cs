﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.ConfigParam;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups
{
    public class MinMaxAverageUpksAndUprsByGroupsBaseService
    {
        #region Entities

        protected class GroupingOks
        {
            public string PropertyType { get; set; }
            public bool HasPurpose { get; set; }
            public string Purpose { get; set; }
        }

        protected class GroupingOksByGroupsDictionary
        {
            public GroupingOks Key { get; set; }
            public List<OksByGroupsDto> Values { get; set; }
        }

        protected class GroupingOksByGroupsAndSubGroupsDictionary
        {
            public GroupingOks Key { get; set; }
            public List<UpksOksByGroupsAndSubGroupsDto> Values { get; set; }
        }

        #endregion

        protected string GetSqlFileContent(string fileName)
        {
            string contents;
            using (var sr =
                new StreamReader(Configuration.GetFileStream(
                    $"\\StatisticalData\\MinMaxAverageUpksAndUprsByGroups\\{fileName}", "sql", "SqlQueries")))
            {
                contents = sr.ReadToEnd();
            }

            return contents;
        }

        protected void AddSummaryByGroupsOks(List<OksByGroupsDto> result)
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


        #region Support Methods

        private OksByGroupsDto GetSummaryByGroupsForOks(GroupingOksByGroupsDictionary groupingOksByGroupsDictionary)
        {
            var objectsCount = groupingOksByGroupsDictionary.Values.GroupBy(x => x.ParentGroup)
                .Sum(x => x.FirstOrDefault()?.ObjectsCount ?? 0);

            var calculationInfo = groupingOksByGroupsDictionary.Values.ToList();

            return new OksByGroupsDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
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

        #endregion
    }
}
