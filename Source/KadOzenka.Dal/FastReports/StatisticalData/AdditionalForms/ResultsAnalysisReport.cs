using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms
{
	public class ResultsAnalysisReport : StatisticalDataReport
	{
		private readonly AdditionalFormsService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public ResultsAnalysisReport()
		{
			_service = new AdditionalFormsService(StatisticalDataService);
			_logger = Log.ForContext<ResultsAnalysisReport>();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return "AdditionalFormsResultsAnalysisReport";
		}

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			_service.QueryManager.SetBaseToken(CancellationToken);
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
			Logger.Debug("Найдено {Count} объектов", data?.Count);

			Logger.Debug("Начато формирование таблиц");
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
			Logger.Debug("Закончено формирование таблиц");

			return dataSet;
		}
	}
}
