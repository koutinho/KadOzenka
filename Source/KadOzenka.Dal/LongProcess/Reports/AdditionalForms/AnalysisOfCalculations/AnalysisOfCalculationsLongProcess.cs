using System;
using System.Collections.Generic;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.AdditionalForms.AnalysisOfCalculations
{
	public class AnalysisOfCalculationsLongProcess: ALinearReportsLongProcessTemplate<AnalysisOfCalculationsLongProcess.ReportItem, ReportLongProcessOnlyTasksInputParameters>
	{
		public class ReportItem
		{
			public ReportItem()
			{

			}
		}

		public AnalysisOfCalculationsLongProcess() : base(Log.ForContext<AnalysisOfCalculationsLongProcess>())
		{
		}

		protected override bool AreInputParametersValid(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return true;
		}

		protected override string ReportName  => "Анализ расчетов";
		protected override string ProcessName => nameof(AnalysisOfCalculationsLongProcess);

		protected override ReportsConfig GetProcessConfig()
		{
			throw new NotImplementedException();
		}

		protected override int GetMaxItemsCount(ReportLongProcessOnlyTasksInputParameters inputParameters, QueryManager queryManager)
		{
			throw new NotImplementedException();
		}

		protected override Func<ReportItem, string> GetSortingCondition()
		{
			throw new NotImplementedException();
		}

		protected override List<GbuReportService.Column> GenerateReportHeaders()
		{
			throw new NotImplementedException();
		}

		protected override List<object> GenerateReportReportRow(int index, ReportItem item)
		{
			throw new NotImplementedException();
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			throw new NotImplementedException();
		}
	}
}