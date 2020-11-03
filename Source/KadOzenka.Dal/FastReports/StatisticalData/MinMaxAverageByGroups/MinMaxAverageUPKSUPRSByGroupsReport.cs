﻿using System.Collections.Specialized;
using System.IO;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;

namespace KadOzenka.Dal.FastReports.StatisticalData.MinMaxAverageByGroups
{
	public class MinMaxAverageUPKSUPRSByGroupsReport : BaseMinMaxAverageByGroupsReport
	{
        protected override string TemplateName(NameValueCollection query)
		{
			var reportType = GetQueryParam<string>("ReportType", query);
			var zuOksObjectType = GetQueryParam<string>("ZuOksObjectType", query);

			switch (reportType)
			{
				case "По группам":
					return zuOksObjectType == "ОКС"
						? "MinMaxAverageByGroupsOksUPKSUPRSReport"
						: "MinMaxAverageByGroupsZuUPKSUPRSReport";
				case "По группам и подгруппам":
					return zuOksObjectType == "ОКС"
						? "MinMaxAverageByGroupsAndSubgroupsOksUPKSUPRSReport"
						: "MinMaxAverageByGroupsAndSubgroupsZuUPKSUPRSReport";
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}
		}

		protected override string GetReportTitle()
		{
			return "Статистика по минимальным, максимальным и средним УПКС/УПРС в разрезе групп";
		}

		protected override string GetDataNameColumnText()
		{
			return null;
		}

		protected override DataTable GetDataByGroups(long[] taskIdList, bool isOks)
		{
            using (var dataTable = new DataTable("Data"))
            {
                dataTable.Columns.Add("GroupName", typeof(string));
                dataTable.Columns.Add("PropertyType", typeof(string));
                dataTable.Columns.Add("Purpose", typeof(string));
                dataTable.Columns.Add("HasPurpose", typeof(bool));
                dataTable.Columns.Add("ObjectsCount", typeof(decimal));
                dataTable.Columns.Add("CalcType", typeof(string));
                dataTable.Columns.Add("UpksCalcValue", typeof(decimal));
                dataTable.Columns.Add("UprsCalcValue", typeof(decimal));

                var calcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();

                if (isOks)
                {
	                Logger.Debug("Тип ОКС");

                    var data = UpksAndUprsService.GetDataByGroupsForOks(taskIdList);
                    Logger.Debug("Найдено {Count} объектов", data?.Count);

                    var objectCountInGroup = data
                        .GroupBy(x => x.ParentGroup)
                        .ToDictionary(k => PreprocessGroupName(k.Key), v => v.Sum(x => x.ObjectsCount));

                    Logger.Debug("Начато формирование таблиц");
                    foreach (var unitDto in data)
                    {
                        var parentGroup = PreprocessGroupName(unitDto.ParentGroup);

                        foreach (var calcType in calcTypes)
                        {
                            dataTable.Rows.Add(
                                parentGroup,
                                unitDto.PropertyType,
                                unitDto.Purpose,
                                unitDto.HasPurpose,
                                objectCountInGroup[parentGroup],
                                calcType.GetEnumDescription(),
                                GetCalcValue(calcType, unitDto.Upks),
                                GetCalcValue(calcType, unitDto.Uprs));
                        }
                    }
                    Logger.Debug("Закончено формирование таблиц");
                }
                else
                {
	                Logger.Debug("Тип ЗУ");

                    var data = UpksAndUprsService.GetDataByGroupsForZu(taskIdList);
                    Logger.Debug("Найдено {Count} объектов", data?.Count);

                    Logger.Debug("Начато формирование таблиц");
                    foreach (var unitDto in data)
                    {
                        foreach (var calcType in calcTypes)
                        {
                            dataTable.Rows.Add(
                                PreprocessGroupName(unitDto.ParentGroup), 
                                PropertyTypes.Stead.GetEnumDescription(), 
                                string.Empty, 
                                false, 
                                unitDto.ObjectsCount,
                                calcType.GetEnumDescription(),
                                GetCalcValue(calcType, unitDto.Upks),
                                GetCalcValue(calcType, unitDto.Uprs));
                        }
                    }
                    Logger.Debug("Закончено формирование таблиц");
                }
                
                return dataTable;
            }
        }

		protected override DataTable GetDataByGroupsAndSubgroups(long[] taskIdList, bool isOks)
		{
            using (var dataTable = new DataTable("Data"))
            {
                dataTable.Columns.Add("GroupName", typeof(string));
                dataTable.Columns.Add("SubgroupName", typeof(string));
                dataTable.Columns.Add("PropertyType", typeof(string));
                dataTable.Columns.Add("Purpose", typeof(string));
                dataTable.Columns.Add("HasPurpose", typeof(bool));
                dataTable.Columns.Add("ObjectsCount", typeof(decimal));
                dataTable.Columns.Add("CalcType", typeof(string));
                dataTable.Columns.Add("UpksCalcValue", typeof(decimal));
                dataTable.Columns.Add("UprsCalcValue", typeof(decimal));

                var calcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();

                if (isOks)
                {
	                Logger.Debug("Тип ОКС");

                    var data = UpksAndUprsService.GetDataByGroupsAndSubGroupsForOks(taskIdList);
                    Logger.Debug("Найдено {Count} объектов", data?.Count);

                    var objectCountInGroupAndSubGroup = data.GroupBy(x => new { x.ParentGroup, x.SubGroup })
                        .ToDictionary(k => PreprocessGroupName(k.Key.ParentGroup) + PreprocessGroupName(k.Key.SubGroup),
                            v => v.Sum(x => x.ObjectsCount));

                    foreach (var unitDto in data)
                    {
                        var key = PreprocessGroupName(unitDto.ParentGroup) + PreprocessGroupName(unitDto.SubGroup);

                        foreach (var calcType in calcTypes)
                        {
                            dataTable.Rows.Add(
                                PreprocessGroupName(unitDto.ParentGroup),
                                PreprocessGroupName(unitDto.SubGroup),
                                unitDto.PropertyType,
                                unitDto.Purpose,
                                unitDto.HasPurpose,
                                objectCountInGroupAndSubGroup[key],
                                calcType.GetEnumDescription(),
                                GetCalcValue(calcType, unitDto.Upks),
                                GetCalcValue(calcType, unitDto.Uprs));
                        }
                    }
                }
                else
                {
	                Logger.Debug("Тип ЗУ");

                    var data = UpksAndUprsService.GetDataByGroupsAndSubGroupsForZu(taskIdList);
                    Logger.Debug("Найдено {Count} объектов", data?.Count);

                    foreach (var unitDto in data)
                    {
                        foreach (var calcType in calcTypes)
                        {
                            dataTable.Rows.Add(
                                PreprocessGroupName(unitDto.ParentGroup), 
                                PreprocessGroupName(unitDto.SubGroup), 
                                PropertyTypes.Stead.GetEnumDescription(), 
                                string.Empty, 
                                false, 
                                unitDto.ObjectsCount,
                                calcType.GetEnumDescription(),
                                GetCalcValue(calcType, unitDto.Upks),
                                GetCalcValue(calcType, unitDto.Uprs));
                        }
                    }
                }

                return dataTable;
            }
        }
	}
}
