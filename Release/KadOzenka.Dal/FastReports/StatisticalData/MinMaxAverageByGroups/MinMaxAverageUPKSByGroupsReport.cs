using System;
using System.Data;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

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
			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("GroupName", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("HasPurpose", typeof(bool));
			dataTable.Columns.Add("ObjectsCount", typeof(decimal));
			dataTable.Columns.Add("CalcType", typeof(string));
			dataTable.Columns.Add("CalcValue", typeof(decimal));


			var data = _service.GetDataByGroups(taskIdList, isOks, MinMaxAverageByGroupsCalcType.Upks);
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.GroupName, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.ObjectsCount,
					unitDto.CalcType.GetEnumDescription(),
					(unitDto.UpksCalcValue.HasValue
						? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues)
						: (decimal?)null));
			}

			return dataTable;

		}

		protected override DataTable GetDataByGroupsAndSubgroups(long[] taskIdList, bool isOks)
		{
			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("GroupName", typeof(string));
			dataTable.Columns.Add("SubgroupName", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("HasPurpose", typeof(bool));
			dataTable.Columns.Add("ObjectsCount", typeof(decimal));
			dataTable.Columns.Add("CalcType", typeof(string));
			dataTable.Columns.Add("CalcValue", typeof(decimal));


			var data = _service.GetDataByGroupsAndSubgroups(taskIdList, isOks, MinMaxAverageByGroupsCalcType.Upks);
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.GroupName, unitDto.SubgroupName, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.ObjectsCount,
					unitDto.CalcType.GetEnumDescription(),
					(unitDto.UpksCalcValue.HasValue
						? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues)
						: (decimal?)null));
			}

			return dataTable;
		}
	}
}
