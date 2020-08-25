using System;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData.Dto.MinMaxAverage;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using ObjectModel.Directory;

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

                if (isOks)
                {
                    var data = _service.GetDataByGroups(taskIdList, isOks, MinMaxAverageByGroupsCalcType.Uprs);

                    foreach (var unitDto in data)
                    {
                        dataTable.Rows.Add(unitDto.GroupName, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.ObjectsCount,
                            unitDto.CalcType.GetEnumDescription(),
                            (unitDto.UprsCalcValue.HasValue
                                ? Math.Round(unitDto.UprsCalcValue.Value, PrecisionForDecimalValues)
                                : (decimal?)null));
                    }
                }
                else
                {
                    var data = _service.GetDataByGroupsUprsZu(taskIdList);
                    var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();
                    foreach (var unitDto in data)
                    {
                        foreach (var upksCalcType in upksCalcTypes)
                        {
                            dataTable.Rows.Add(
                                GetGroupName(unitDto.ParentGroup),
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

                var data = _service.GetDataByGroupsAndSubgroups(taskIdList, isOks, MinMaxAverageByGroupsCalcType.Uprs);
                foreach (var unitDto in data)
                {
                    dataTable.Rows.Add(unitDto.GroupName, unitDto.SubgroupName, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.ObjectsCount,
                        unitDto.CalcType.GetEnumDescription(),
                        (unitDto.UprsCalcValue.HasValue
                            ? Math.Round(unitDto.UprsCalcValue.Value, PrecisionForDecimalValues)
                            : (decimal?)null));
                }

                return dataTable;
            }
        }

        //TODO move to base class
        #region SupportMethods

        private static decimal? GetCalcValue(UpksCalcType upksCalcType, MinMaxAverageByGroupsUprsZuDto unitDto)
        {
            decimal? value = null;
            switch (upksCalcType)
            {
                case UpksCalcType.Min:
                    value = unitDto.UprsMin;
                    break;
                case UpksCalcType.Average:
                    value = unitDto.UprsAvg;
                    break;
                case UpksCalcType.AverageWeight:
                    value = unitDto.UprsAvgWeight;
                    break;
                case UpksCalcType.Max:
                    value = unitDto.UprsMax;
                    break;
            }

            return value.HasValue
                ? Math.Round(value.Value, PrecisionForDecimalValues)
                : (decimal?)null;
        }

        private string GetGroupName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? "Без группы" : name;
        }

        #endregion
    }
}
