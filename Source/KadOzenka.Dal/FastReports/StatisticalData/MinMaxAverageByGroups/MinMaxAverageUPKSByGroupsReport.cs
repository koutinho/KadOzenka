using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;

namespace KadOzenka.Dal.FastReports.StatisticalData.MinMaxAverageByGroups
{
	public class MinMaxAverageUPKSByGroupsReport : BaseMinMaxAverageByGroupsReport
    {
        protected override string GetReportTitle()
		{
			return "Статистика по минимальным, максимальным и средним УПКС в разрезе групп";
		}

		protected override string GetDataNameColumnText()
		{
			return "УПКС";
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
                dataTable.Columns.Add("CalcValue", typeof(decimal));

                var calcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();

                if (isOks)
                {
	                Logger.Debug("Тип ОКС");

                    var data = UpksService.GetDataByGroupsForOks(taskIdList);
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
                                GetCalcValue(calcType, unitDto));
                        }
                    }
                    Logger.Debug("Закончено формирование таблиц");
                }
                else
                {
	                Logger.Debug("Тип ЗУ");

                    var data = UpksService.GetDataByGroupsForZu(taskIdList);
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
                                GetCalcValue(calcType, unitDto));
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
                dataTable.Columns.Add("CalcValue", typeof(decimal));

                var calcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();

                if (isOks)
                {
	                Logger.Debug("Тип ОКС");

                    var data = UpksService.GetDataByGroupsAndSubGroupsForOks(taskIdList);
                    Logger.Debug("Найдено {Count} объектов", data?.Count);

                    var objectCountInGroupAndSubGroup = data
                        .GroupBy(x => new {x.ParentGroup, x.SubGroup})
                        .ToDictionary(k => PreprocessGroupName(k.Key.ParentGroup) + PreprocessGroupName(k.Key.SubGroup),
                            v => v.Sum(x => x.ObjectsCount));

                    Logger.Debug("Начато формирование таблиц");
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
                                GetCalcValue(calcType, unitDto));
                        }
                    }
                    Logger.Debug("Закончено формирование таблиц");
                }
                else
                {
	                Logger.Debug("Тип ЗУ");

                    var data = UpksService.GetDataByGroupsAndSubgroupsForZu(taskIdList);
                    Logger.Debug("Найдено {Count} объектов", data?.Count);

                    Logger.Debug("Начато формирование таблиц");
                    foreach (var unitDto in data)
                    {
                        foreach (var calcType in calcTypes)
                        {
                            dataTable.Rows.Add(PreprocessGroupName(unitDto.ParentGroup),
                                PreprocessGroupName(unitDto.SubGroup), 
                                PropertyTypes.Stead.GetEnumDescription(), 
                                string.Empty, 
                                false, 
                                unitDto.ObjectsCount,
                                calcType.GetEnumDescription(),
                                GetCalcValue(calcType, unitDto));
                        }
                    }
                    Logger.Debug("Закончено формирование таблиц");
                }

                return dataTable;
            }
        }
    }
}
