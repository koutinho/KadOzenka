using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.NumberOfObjectsByAdministrativeDistricts
{
	public class NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesReport : StatisticalDataReport
	{
		private readonly NumberOfObjectsByAdministrativeDistrictsService _service;

		public NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesReport()
		{
			_service = new NumberOfObjectsByAdministrativeDistrictsService(new StatisticalDataService(), new GbuObjectService());
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return "NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesReport";
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskList = GetTaskIdList(query);
			var divisionType = GetAreaDivisionType(GetQueryParam<string>("DivisionType", query));

			DataSet dataset;
			switch (divisionType)
			{
				case StatisticDataAreaDivisionType.RegionNumbers:
					dataset = GetDataForRegionNumbers(taskList);
					break;
				case StatisticDataAreaDivisionType.Districts:
					dataset = GetDataForDistricts(taskList);
					break;
				case StatisticDataAreaDivisionType.Regions:
					dataset = GetDataForRegions(taskList);
					break;
				default:
					throw new InvalidDataException($"Неизвестный тип деления для количества объектов по группам: {divisionType}");
			}

			return dataset;
		}

		private DataSet GetDataForRegionNumbers(long[] taskList)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Columns.Add("DataNameColumnText");
			dataTitleTable.Columns.Add("DataParentNameColumnText");
			dataTitleTable.Rows.Add("Количество объектов недвижимости (по кадастровым районам), в разрезе групп и видов объектов недвижимости на территории города Москвы",
				"Номер кадастрового района", "Административный округ");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("ParentName", typeof(string));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("HasPurpose", typeof(bool));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes(taskList, StatisticDataAreaDivisionType.RegionNumbers);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.ParentName, unitDto.Name, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.Group, unitDto.Count);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}

		private DataSet GetDataForDistricts(long[] taskList)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Columns.Add("DataNameColumnText");
			dataTitleTable.Columns.Add("DataParentNameColumnText");
			dataTitleTable.Rows.Add("Количество объектов недвижимости (по административным округам), в разрезе групп и видов объектов недвижимости на территории города Москвы",
				"Административный округ", "Номер кадастрового района");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("ParentName", typeof(string));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("HasPurpose", typeof(bool));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes(taskList, StatisticDataAreaDivisionType.Districts);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.ParentName, unitDto.Name, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.Group, unitDto.Count);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}

		private DataSet GetDataForRegions(long[] taskList)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Columns.Add("DataNameColumnText");
			dataTitleTable.Columns.Add("DataParentNameColumnText");
			dataTitleTable.Rows.Add("Количество объектов недвижимости (по муниципальным районам), в разрезе групп и видов объектов недвижимости на территории города Москвы",
				"Муниципальный район / поселение", "Административный округ");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("ParentName", typeof(string));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("HasPurpose", typeof(bool));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes(taskList, StatisticDataAreaDivisionType.Regions);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.ParentName, unitDto.Name, unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.Group, unitDto.Count);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}

		private StatisticDataAreaDivisionType GetAreaDivisionType(string divisionType)
		{
			switch (divisionType)
			{
				case "Номер кадастрового района":
					return StatisticDataAreaDivisionType.RegionNumbers;
				case "Административный округ":
					return StatisticDataAreaDivisionType.Districts;
				case "Муниципальный район":
					return StatisticDataAreaDivisionType.Regions;
				default:
					throw new InvalidDataException($"Тип деления для количества объектов по группам: {divisionType}");
			}
		}
	}
}
