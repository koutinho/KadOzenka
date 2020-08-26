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
                    var objectCountInGroup = GetObjectCountInGroup(data);

                    foreach (var unitDto in data)
                    {
                        var parentGroup = PreprocessGroupName(unitDto.ParentGroup);
                        var objectsCountInGroup = objectCountInGroup[parentGroup];

                        dataTable.Rows.Add(
                            parentGroup, 
                            unitDto.PropertyType, 
                            unitDto.Purpose, 
                            unitDto.HasPurpose,
                            objectsCountInGroup,
                            unitDto.UpksCalcTypeEnum.GetEnumDescription(),
                            (unitDto.UpksCalcValue.HasValue
                                ? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues)
                                : (decimal?)null));
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
                    var data = _service.GetDataByGroupsAndSubgroups(taskIdList, isOks, MinMaxAverageByGroupsCalcType.Upks);
                    foreach (var unitDto in data)
                    {
                        dataTable.Rows.Add(unitDto.GroupName, unitDto.SubgroupName, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.ObjectsCount,
                            unitDto.CalcType.GetEnumDescription(),
                            (unitDto.UpksCalcValue.HasValue
                                ? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues)
                                : (decimal?)null));
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

        #region Support Methods

        private Dictionary<string, int> GetObjectCountInGroup(List<MinMaxAverageByGroupsUpksOksDto> data)
        {
            var objectCountInGroup = new Dictionary<string, int>();

            data.GroupBy(x => new { x.ParentGroup, x.PropertyType, x.Purpose }).ToList().ForEach(x =>
            {
                var parentGroup = PreprocessGroupName(x.Key.ParentGroup);
                var currentObjectCount = x.FirstOrDefault()?.ObjectsCount ?? 0;

                if (objectCountInGroup.TryGetValue(parentGroup, out var n))
                {
                    objectCountInGroup[parentGroup] = n + currentObjectCount;
                }
                else
                {
                    objectCountInGroup[parentGroup] = currentObjectCount;
                }
            });

            return objectCountInGroup;
        }

        #endregion
    }
}
