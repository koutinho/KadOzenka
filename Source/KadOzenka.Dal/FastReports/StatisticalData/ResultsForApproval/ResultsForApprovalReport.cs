using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsForApproval
{
	public class ResultsForApprovalReport : StatisticalDataReport
	{
		private readonly ResultsForApprovalService _service;
		public ResultsForApprovalReport()
		{
			_service = new ResultsForApprovalService(StatisticalDataService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(ResultsForApprovalReport);
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Результаты определения кадастровой стоимости объектов недвижимости на территории субъекта Российской Федерации город Москва");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("DataNum");
			dataTable.Columns.Add("CadastralNumber");
			dataTable.Columns.Add("CadastralCost");

			var data = _service.GetResultsForApprovalData(taskIdList);

			var i = 1;
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(i,
					unitDto.CadastralNumber,
					unitDto.CadastralCost
				);
				i++;
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}
	}
}
