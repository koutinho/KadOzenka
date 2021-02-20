using System;
using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.ResultsForApproval
{
	public class ResultsForApprovalLongProcess : ALinearReportsLongProcessTemplate<ResultsForApprovalLongProcess.ReportItem, ReportLongProcessInputParameters>
	{
		protected override string ReportName => "Результаты на утверждение";
		protected override string ProcessName => nameof(ResultsForApprovalLongProcess);
		protected StatisticalDataService StatisticalDataService { get; set; }
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }


		public ResultsForApprovalLongProcess() : base(Log.ForContext<ResultsForApprovalLongProcess>())
		{
			StatisticalDataService = new StatisticalDataService();
		}


		protected override bool AreInputParametersValid(ReportLongProcessInputParameters inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0;
		}

		protected override void PrepareVariables(ReportLongProcessInputParameters inputParameters)
		{
			BaseSql = GetBaseSql(inputParameters);
			TaskIdsStr = string.Join(',', inputParameters.TaskIds);

			BaseUnitsCondition = $@" where unit.TASK_ID IN ({TaskIdsStr}) AND 
										unit.PROPERTY_TYPE_CODE<>2190 ";
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 400000;
			var defaultThreadsCount = 4;

			return GetProcessConfigFromSettings("ResultsForApproval", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportLongProcessInputParameters inputParameters,
			QueryManager queryManager)
		{
			return GetMaxUnitsCount(BaseUnitsCondition, queryManager);
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			var unitsCondition = $@"{BaseUnitsCondition}
										order by unit.id 
										limit {packageSize} offset {packageIndex * packageSize}";

			return string.Format(BaseSql, unitsCondition);
		}

		protected override Func<ReportItem, string> GetSortingCondition()
		{
			return x => x.CadastralNumber;
		}

		protected override List<GbuReportService.Column> GenerateReportHeaders()
		{
			var columns = new List<GbuReportService.Column>
			{
				new GbuReportService.Column
				{
					Header = "№ п/п",
					Width = 3
				},
				new GbuReportService.Column
				{
					Header = "Кадастровый номер",
					Width = 5
				},
				new GbuReportService.Column
				{
					Header = "Кадастровая стоимость, рублей",
					Width = 7
				}
			};

			var counter = 0;
			columns.ForEach(x => x.Index = counter++);

			return columns;
		}

		protected override List<object> GenerateReportReportRow(int index, ReportItem item)
		{
			return new List<object>
			{
				(index + 1).ToString(),
				item.CadastralNumber,
				item.CadastralCost
			};
		}



		#region Support Methods

		private string GetBaseSql(ReportLongProcessInputParameters parameters)
		{
			var notCadastralQuarterType = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
				QSConditionType.NotEqual, (long) PropertyTypes.CadastralQuartal);
			var query = StatisticalDataService.GetQueryForUnitsByTasks(parameters.TaskIds.ToArray(), new List<QSCondition>{ notCadastralQuarterType });
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "CadastralCost"));

			return query.GetSql();
		}

		#endregion


		#region Entities

		public class ReportItem
		{
			public string CadastralNumber { get; set; }
			public decimal CadastralCost { get; set; }
		}

		#endregion
	}
}
