using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.NumberOfObjectsByAdministrativeDistricts
{
	public class NumberOfObjectsByAdministrativeDistrictsBySubjectReportHandler : INumberOfObjectsByAdministrativeDistrictsReportHandler
	{
		private readonly NumberOfObjectsByAdministrativeDistrictsService _service;

		public NumberOfObjectsByAdministrativeDistrictsBySubjectReportHandler()
		{
			_service = new NumberOfObjectsByAdministrativeDistrictsService(new StatisticalDataService(), new GbuObjectService());
		}

		public string GetTemplateName(NameValueCollection query, IGetQueryPAramFunc getQueryParam)
		{
			return "NumberOfObjectsByAdministrativeDistrictsBySubjectReport";
		}

		public DataSet GetData(long[] taskList, NameValueCollection query, IGetQueryPAramFunc getQueryParam)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Количество объектов недвижимости (по субъекту), в разрезе групп и видов объектов недвижимости на территории города Москвы");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("HasPurpose", typeof(bool));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsBySubject(taskList);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.Group, unitDto.Count);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}
	}
}
