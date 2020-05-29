using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.NumberOfObjectsByAdministrativeDistricts
{
	public class NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesReportHandler : INumberOfObjectsByAdministrativeDistrictsReportHandler
	{
		private readonly NumberOfObjectsByAdministrativeDistrictsService _service;

		public NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesReportHandler()
		{
			_service = new NumberOfObjectsByAdministrativeDistrictsService();
		}

		public string GetTemplateName(NameValueCollection query, IGetQueryPAramFunc getQueryParam)
		{
			return "NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesReport";
		}

		public DataSet GetData(long[] taskList, NameValueCollection query, IGetQueryPAramFunc getQueryParam)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Количество объектов недвижимости (по административным округам), в разрезе групп и видов объектов недвижимости на территории города Москвы");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("Distrinct", typeof(string));
			dataTable.Columns.Add("RegionNumber", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes(taskList);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.Distrinct, unitDto.RegionNumber, unitDto.PropertyType, unitDto.Group, unitDto.Count);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}
	}
}
