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
	public class NumberOfObjectsByAdministrativeDistrictsByGroupsReport : StatisticalDataReport
	{
		private readonly NumberOfObjectsByAdministrativeDistrictsService _service;

		public NumberOfObjectsByAdministrativeDistrictsByGroupsReport()
		{
			_service = new NumberOfObjectsByAdministrativeDistrictsService(new StatisticalDataService(), new GbuObjectService());
		}

		protected override string TemplateName(NameValueCollection query)
		{
			var divisionType = GetAreaDivisionType(GetQueryParam<string>("DivisionType", query));
			switch (divisionType)
			{
				case StatisticDataAreaDivisionType.RegionNumbers:
					return "NumberOfObjectsByAdministrativeDistrictsByGroupsReport";
				case StatisticDataAreaDivisionType.Districts:
					return "NumberOfObjectsByAdministrativeDistrictsByGroupsReport";
				case StatisticDataAreaDivisionType.Regions:
					return "NumberOfObjectsByAdministrativeDistrictsByGroupsForRegionsReport";
				case StatisticDataAreaDivisionType.Quarters:
					return "NumberOfObjectsByAdministrativeDistrictsByGroupsForQuartersReport";
				default:
					throw new InvalidDataException($"Неизвестный тип деления: {divisionType}");
			}
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskList = GetTaskIdList(query);
			var divisionType = GetAreaDivisionType(GetQueryParam<string>("DivisionType", query));

			DataSet dataset;
			switch (divisionType)
			{
				case StatisticDataAreaDivisionType.RegionNumbers:
					dataset = GetDataForCadastralRegions(taskList);
					break;
				case StatisticDataAreaDivisionType.Districts:
					dataset = GetDataForDistricts(taskList);
					break;
				case StatisticDataAreaDivisionType.Regions:
					dataset = GetDataForRegions(taskList);
					break;
				case StatisticDataAreaDivisionType.Quarters:
					dataset = GetDataForQuarters(taskList);
					break;
				default:
					throw new InvalidDataException($"Неизвестный тип деления для количества объектов по группам: {divisionType}");
			}

			return dataset;
		}

		private DataSet GetDataForCadastralRegions(long[] taskList)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Columns.Add("DataNameColumnText");
			var title = "Количество объектов недвижимости по кадастровым районам города Москвы";
			var dataTypeHeader = "Номер кадастрового района";
			dataTitleTable.Rows.Add(title, dataTypeHeader);

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsByGroups(taskList, StatisticDataAreaDivisionType.RegionNumbers);
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.Name, unitDto.Group, unitDto.ObjectsCount);
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
			var title = "Количество объектов недвижимости по административным округам города Москвы";
			var dataTypeHeader = "Наименование административного округа";
			dataTitleTable.Rows.Add(title, dataTypeHeader);

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsByGroups(taskList, StatisticDataAreaDivisionType.Districts);
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.Name, unitDto.Group, unitDto.ObjectsCount);
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
			var title = "Количество объектов недвижимости по муниципальным районам города Москвы";
			dataTitleTable.Rows.Add(title);

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("FirstParentName", typeof(string));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsByGroups(taskList, StatisticDataAreaDivisionType.Regions);
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.FirstParentName, unitDto.Name, unitDto.Group, unitDto.ObjectsCount);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;

		}

		private DataSet GetDataForQuarters(long[] taskList)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Columns.Add("DataNameColumnText");
			var title = "Количество объектов недвижимости по кадастровым кварталам города Москвы";
			dataTitleTable.Rows.Add(title);

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("FirstParentName", typeof(string));
			dataTable.Columns.Add("SecondParentName", typeof(string));
			dataTable.Columns.Add("ThirdParentName", typeof(string));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));
			

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsByGroups(taskList, StatisticDataAreaDivisionType.Quarters);
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.FirstParentName, unitDto.SecondParentName, unitDto.ThirdParentName, unitDto.Name, unitDto.Group, unitDto.ObjectsCount);
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
				case "Кадастровый квартал":
					return StatisticDataAreaDivisionType.Quarters;
				default:
					throw new InvalidDataException($"Тип деления для количества объектов по группам: {divisionType}");
			}
		}
	}
}
