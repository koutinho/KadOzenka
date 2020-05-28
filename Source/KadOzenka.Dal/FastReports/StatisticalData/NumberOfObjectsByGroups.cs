using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.ManagementDecisionSupport;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public class NumberOfObjectsByGroups : StatisticalDataReport
	{
		private readonly StatisticalDataService _statisticalDataService;

		public NumberOfObjectsByGroups()
		{
			_statisticalDataService = new StatisticalDataService();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(NumberOfObjectsByGroups);
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
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
			var data = _statisticalDataService.GetNumberOfObjectsByGroups(taskIdList, isOksReportType);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.PropertyType, unitDto.Group, unitDto.ParentGroup, unitDto.Count);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return HadleData(dataSet);
		}
	}
}
