using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms
{
	public class ResultsAnalysisReport : StatisticalDataReport
	{
		private readonly AdditionalFormsService _service;

		public ResultsAnalysisReport()
		{
			_service = new AdditionalFormsService(StatisticalDataService, GbuObjectService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return "AdditionalFormsResultsAnalysisReport";
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Анализ результатов");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("Kn", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Square", typeof(string));
			dataTable.Columns.Add("PrevUpks", typeof(string));
			dataTable.Columns.Add("PrevKs", typeof(string));
			dataTable.Columns.Add("Upks", typeof(string));
			dataTable.Columns.Add("Ks", typeof(string));
			dataTable.Columns.Add("Status", typeof(string));

			var data = _service.GetResultsAnalysisData(taskIdList);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.CadastralNumber,
					unitDto.PropertyType,
					unitDto.Square,
					unitDto.PreviousUpks.HasValue
						? Math.Round(unitDto.PreviousUpks.Value, PrecisionForDecimalValues)
						: (decimal?) null,
					unitDto.PreviousCadastralCost.HasValue
						? Math.Round(unitDto.PreviousCadastralCost.Value, PrecisionForDecimalValues)
						: (decimal?) null,
					unitDto.Upks.HasValue
						? Math.Round(unitDto.Upks.Value, PrecisionForDecimalValues)
						: (decimal?) null,
					unitDto.CadastralCost.HasValue
						? Math.Round(unitDto.CadastralCost.Value, PrecisionForDecimalValues)
						: (decimal?) null,
					unitDto.Status);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}
	}
}
