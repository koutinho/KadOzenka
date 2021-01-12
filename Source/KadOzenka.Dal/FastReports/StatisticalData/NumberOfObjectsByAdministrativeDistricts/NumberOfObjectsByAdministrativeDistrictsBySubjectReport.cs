using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.NumberOfObjectsByAdministrativeDistricts
{
	public class NumberOfObjectsByAdministrativeDistrictsBySubjectReport : StatisticalDataReport
	{
		private readonly NumberOfObjectsByAdministrativeDistrictsService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public NumberOfObjectsByAdministrativeDistrictsBySubjectReport()
		{
			_service = new NumberOfObjectsByAdministrativeDistrictsService(new GbuCodRegisterService());
			_logger = Log.ForContext<NumberOfObjectsByAdministrativeDistrictsBySubjectReport>();
		}


		protected override string TemplateName(NameValueCollection query)
		{
			return "NumberOfObjectsByAdministrativeDistrictsBySubjectReport";
		}

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			_service.CancellationManager.SetBaseToken(CancellationToken);

			var taskIdList = GetTaskIdList(query);
			var zuOksObjectType = GetQueryParam<string>("ZuOksObjectType", query);
			Logger.Debug("Тип объекта {ObjectType}", zuOksObjectType);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Количество объектов недвижимости (по субъекту), в разрезе групп и видов объектов недвижимости на территории города Москвы");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("HasPurpose", typeof(bool));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var data = _service.GetNumberOfObjectsByAdministrativeDistrictsBySubject(taskIdList, zuOksObjectType == "ОКС");
			Logger.Debug("Найдено {Count} объектов", data?.Count);

			Logger.Debug("Начато формирование таблиц");
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.Group, unitDto.Count);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);
			Logger.Debug("Закончено формирование таблиц");

			return dataSet;
		}
	}
}
