using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData.Dto.MinMaxAverage;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
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

                if (isOks)
                {
                    var data = _service.GetDataByGroupsUpksOks(taskIdList);
                    var objectCountInGroup = data
                        .GroupBy(x => x.ParentGroup)
                        .ToDictionary(k => PreprocessGroupName(k.Key), v => v.Sum(x => x.ObjectsCount));

                    var calcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();
                    foreach (var unitDto in data)
                    {
                        var parentGroup = PreprocessGroupName(unitDto.ParentGroup);
                        var objectsCountInGroup = objectCountInGroup[parentGroup];

                        foreach (var calcType in calcTypes)
                        {
                            dataTable.Rows.Add(
                                parentGroup,
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
                    var data = _service.GetDataByGroupsUpksZu(taskIdList);
                    var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();
                    foreach (var unitDto in data)
                    {
                        foreach (var upksCalcType in upksCalcTypes)
                        {
                            dataTable.Rows.Add(
                                PreprocessGroupName(unitDto.ParentGroup),
                                PropertyTypes.Stead.GetEnumDescription(),
                                string.Empty,
                                false,
                                unitDto.ObjectsCount,
                                upksCalcType.GetEnumDescription(),
                                GetCalcValue(upksCalcType, unitDto));
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
                    var data = _service.GetDataByGroupsAndSubGroupsUpksOks(taskIdList);
                    var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();

                    var objectCountInGroupAndSubGroup = data
                        .GroupBy(x => new {x.ParentGroup, x.SubGroup})
                        .ToDictionary(k => PreprocessGroupName(k.Key.ParentGroup) + PreprocessGroupName(k.Key.SubGroup),
                            v => v.Sum(x => x.ObjectsCount));

                    foreach (var unitDto in data)
                    {
                        var key = PreprocessGroupName(unitDto.ParentGroup) + PreprocessGroupName(unitDto.SubGroup);

                        var objectsCountInGroup = objectCountInGroupAndSubGroup[key];

                        foreach (var upksCalcType in upksCalcTypes)
                        {
                            dataTable.Rows.Add(
                                PreprocessGroupName(unitDto.ParentGroup),
                                PreprocessGroupName(unitDto.SubGroup),
                                unitDto.PropertyType,
                                unitDto.Purpose,
                                unitDto.HasPurpose,
                                objectsCountInGroup,
                                upksCalcType.GetEnumDescription(),
                                GetCalcValue(upksCalcType, unitDto));
                        }
                    }
                }
                else
                {
                    var data = _service.GetDataByGroupsAndSubgroupsUpksZu(taskIdList);
                    var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();

                    foreach (var unitDto in data)
                    {
                        foreach (var upksCalcType in upksCalcTypes)
                        {
                            dataTable.Rows.Add(PreprocessGroupName(unitDto.ParentGroup),
                                PreprocessGroupName(unitDto.SubGroup), 
                                PropertyTypes.Stead.GetEnumDescription(), 
                                string.Empty, 
                                false, 
                                unitDto.ObjectsCount,
                                upksCalcType.GetEnumDescription(),
                                GetCalcValue(upksCalcType, unitDto));
                        }
                    }
                }

                return dataTable;
            }
        }
    }
}
