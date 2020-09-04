using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms
{
	public class ChangesUploadingReport : StatisticalDataReport
	{
		private readonly AdditionalFormsService _service;

		public ChangesUploadingReport()
		{
			_service = new AdditionalFormsService(StatisticalDataService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return "AdditionalFormsChangesUploadingReport";
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);
			var reportType = GetQueryParam<string>("ReportType", query);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Выгрузка изменений");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("Kn", typeof(string));
			dataTable.Columns.Add("ChangedDate", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Status", typeof(string));
			dataTable.Columns.Add("OldValue", typeof(string));
			dataTable.Columns.Add("NewValue", typeof(string));
			dataTable.Columns.Add("Changing", typeof(string));

			var isOksReportType = reportType == "Статистика по группам с количеством ОКС";
			var data = _service.GetChangesUploadingData(taskIdList);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.CadastralNumber, unitDto.ChangedDate?.ToString(DateFormat),
					unitDto.PropertyType, unitDto.Status, unitDto.OldValue, unitDto.NewValue, unitDto.Changing);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}
	}
}
