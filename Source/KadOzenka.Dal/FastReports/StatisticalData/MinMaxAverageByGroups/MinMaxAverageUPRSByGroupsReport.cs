using System;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using MinMaxAverageByGroupsCalcType = KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageByGroupsCalcType;

namespace KadOzenka.Dal.FastReports.StatisticalData.MinMaxAverageByGroups
{
	public class MinMaxAverageUPRSByGroupsReport : BaseMinMaxAverageByGroupsReport
	{
        protected override string GetReportTitle()
		{
			return "Статистика по минимальным, максимальным и средним УПРС в разрезе групп";
		}

		protected override string GetDataNameColumnText()
		{
			return "УПРС";
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
                    var data = UprsService.GetDataByGroupsForOks(taskIdList);
                    var objectCountInGroup = data
                        .GroupBy(x => x.ParentGroup)
                        .ToDictionary(k => PreprocessGroupName(k.Key), v => v.Sum(x => x.ObjectsCount));

                    foreach (var unitDto in data)
                    {
                        var parentGroup = PreprocessGroupName(unitDto.ParentGroup);
                        var objectsCountInGroup = objectCountInGroup[parentGroup];

                        foreach (var calcType in calcTypes)
                        {
                            dataTable.Rows.Add(
                                PreprocessGroupName(unitDto.ParentGroup),
                                unitDto.PropertyType,
                                unitDto.Purpose,
                                unitDto.HasPurpose,
                                objectsCountInGroup,
                                calcType.GetEnumDescription(),
                                GetCalcValue(calcType, unitDto));
                        }
                    }
                }
                else
                {
                    var data = UprsService.GetDataByGroupsForZu(taskIdList);
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

                if (isOks)
                {
                    var data = _service.GetDataByGroupsAndSubgroups(taskIdList, isOks, MinMaxAverageByGroupsCalcType.Uprs);
                    foreach (var unitDto in data)
                    {
                        dataTable.Rows.Add(unitDto.GroupName, unitDto.SubgroupName, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.ObjectsCount,
                            unitDto.CalcType.GetEnumDescription(),
                            (unitDto.UprsCalcValue.HasValue
                                ? Math.Round(unitDto.UprsCalcValue.Value, PrecisionForDecimalValues)
                                : (decimal?)null));
                    }
                }
                else
                {
                    var data = UprsService.GetDataByGroupsAndSubgroupsForZu(taskIdList);
                    var calcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();
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
                                GetCalcValue(calcType, unitDto));
                        }
                    }
                }

                return dataTable;
            }
        }
    }
}
