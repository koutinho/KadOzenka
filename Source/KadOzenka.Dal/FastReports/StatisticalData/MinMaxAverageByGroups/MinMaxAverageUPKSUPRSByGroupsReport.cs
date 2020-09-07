using System;
using System.Collections.Specialized;
using System.IO;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using MinMaxAverageByGroupsCalcType = KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageByGroupsCalcType;

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

                if (isOks)
                {
                    var data = _service.GetDataByGroups(taskIdList, isOks, MinMaxAverageByGroupsCalcType.UpksAndUprs);
                    foreach (var unitDto in data)
                    {
                        dataTable.Rows.Add(unitDto.GroupName, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.ObjectsCount,
                            unitDto.CalcType.GetEnumDescription(),
                            (unitDto.UpksCalcValue.HasValue
                                ? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues)
                                : (decimal?)null),
                            (unitDto.UprsCalcValue.HasValue
                                ? Math.Round(unitDto.UprsCalcValue.Value, PrecisionForDecimalValues)
                                : (decimal?)null));
                    }
                }
                else
                {
                    var data = UpksAndUprsService.GetDataByGroupsForZu(taskIdList);
                    var calcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();
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

                if (isOks)
                {
                    var data = _service.GetDataByGroupsAndSubgroups(taskIdList, isOks, MinMaxAverageByGroupsCalcType.UpksAndUprs);
                    foreach (var unitDto in data)
                    {
                        dataTable.Rows.Add(unitDto.GroupName, unitDto.SubgroupName, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.ObjectsCount,
                            unitDto.CalcType.GetEnumDescription(),
                            (unitDto.UpksCalcValue.HasValue
                                ? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues)
                                : (decimal?)null),
                            (unitDto.UprsCalcValue.HasValue
                                ? Math.Round(unitDto.UprsCalcValue.Value, PrecisionForDecimalValues)
                                : (decimal?)null));
                    }
                }
                else
                {
                    var data = UpksAndUprsService.GetDataByGroupsAndSubGroupsForZu(taskIdList);
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
