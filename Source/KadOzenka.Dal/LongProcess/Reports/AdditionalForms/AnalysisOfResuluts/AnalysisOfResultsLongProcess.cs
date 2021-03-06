using System;
using System.Collections.Generic;
using System.IO;
using KadOzenka.Dal.LongProcess.Reports.AdditionalForms.AnalysisOfResuluts.Entities;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.AdditionalForms.AnalysisOfResuluts
{
	public class AnalysisOfResultsLongProcess: ALinearReportsLongProcessTemplate<ReportItem, ReportLongProcessOnlyTasksInputParameters>
	{
		private readonly string _reportSqlFileName = "AdditionalForms_ResultsAnalysis";
		private string UnitsConditionToCount { get; set; }
		private List<long> _taskIdList;
		private readonly int PrecisionForDecimalValues = 2;
		protected override long ReportCode => (long)StatisticalDataType.AdditionalFormsResultsAnalysis;



		public AnalysisOfResultsLongProcess() : base(Log.ForContext<AnalysisOfResultsLongProcess>())
		{
		}

		protected override bool AreInputParametersValid(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return inputParameters?.TaskIds?.Count > 0;
		}

		protected override string ReportName => "Анализ результатов";
		protected override string ProcessName => nameof(AnalysisOfResultsLongProcess);

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 4;

			return GetProcessConfigFromSettings("AnalysisOfResultsReport", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			if (inputParameters != null && inputParameters.TaskIds?.Count > 0)
			{
				return GetMaxUnitsCount(UnitsConditionToCount);
			}

			return 0;
		}

		protected override string GenerateReportTitle()
		{
			return "Анализ результатов";
		}

		protected override List<Column> GenerateReportHeaders()
		{
			var counter = 0;
			var columns  = new List<Column>{
				new Column {Header = "№ п/п", Width = 2},
				new Column {Header = "КН", Width = 4},
				new Column {Header = "Тип", Width = 5},
				new Column {Header = "Площадь"},
				new Column {Header = "Прош. УПКС", Width = 4},
				new Column {Header = "Прош. КС", Width = 4},
				new Column {Header = "окон. УПКС"},
				new Column {Header = "окон. КС"}, 
				new Column {Header = "Статус", Width = 5}
			};
			columns.ForEach(item => item.Index = counter++);

			return columns;
		}

		protected override List<object> GenerateReportReportRow(int index, ReportItem item)
		{
			return new List<object>
			{
				(index + 1).ToString(),
				item.CadastralNumber,
				item.PropertyType,
				item.Square,
				item.PreviousUpks.HasValue
					? Math.Round(item.PreviousUpks.Value, PrecisionForDecimalValues)
					: (decimal?) null,
				item.PreviousCadastralCost.HasValue
					? Math.Round(item.PreviousCadastralCost.Value, PrecisionForDecimalValues)
					: (decimal?) null,
				item.Upks.HasValue
					? Math.Round(item.Upks.Value, PrecisionForDecimalValues)
					: (decimal?) null,
				item.CadastralCost.HasValue
					? Math.Round(item.CadastralCost.Value, PrecisionForDecimalValues)
					: (decimal?) null,
				item.Status
			};
		}

		protected override void PrepareVariables(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			_taskIdList = inputParameters.TaskIds;

			UnitsConditionToCount = $"where TASK_ID in ({string.Join(", ", _taskIdList)}) and property_type_code<>2190";
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			string contents;
			using (var sr = new StreamReader(StatisticalDataService.GetSqlQueryFileStream(_reportSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", _taskIdList), packageSize, packageIndex);
			return sql;
		}
	}
}