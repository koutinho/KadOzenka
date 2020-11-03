using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public class NumberOfObjectsByGroupsReport : StatisticalDataReport
	{
		private readonly NumberOfObjectsByGroupsService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public NumberOfObjectsByGroupsReport()
		{
			_service = new NumberOfObjectsByGroupsService(new StatisticalDataService());
			_logger = Log.ForContext<NumberOfObjectsByGroupsReport>();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(NumberOfObjectsByGroupsReport);
		}

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);
			var reportType = GetQueryParam<string>("ReportType", query);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Количество объектов в разрезе подгрупп");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Group", typeof(string));
			dataTable.Columns.Add("ParentGroup", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));

			var isOksReportType = reportType == "Статистика по группам с количеством ОКС";
			var data = _service.GetNumberOfObjectsByGroups(taskIdList, isOksReportType);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.PropertyType, unitDto.Group, unitDto.ParentGroup, unitDto.Count);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}
	}
}
