using System;
using System.Collections.Generic;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults
{
	public class GroupingReportLongProcess : ALinearReportsLongProcessTemplate<GroupingReportLongProcess.ReportItem, ReportLongProcessOnlyTasksInputParameters>
	{
		protected override string ReportName => "Группировка объектов недвижимости";
		protected override string ProcessName => nameof(GroupingReportLongProcess);
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }


		public GroupingReportLongProcess() : base(Log.ForContext<GroupingReportLongProcess>())
		{
		}


		protected override bool AreInputParametersValid(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0;
		}

		protected override void PrepareVariables(ReportLongProcessOnlyTasksInputParameters inputParameters)
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

			return GetProcessConfigFromSettings("QualityPricingFactorsEncodingResultsForGrouping", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return GetMaxUnitsCount(BaseUnitsCondition);
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

		protected override string GenerateReportTitle()
		{
			return "Группировка объектов недвижимости";
		}

		protected override List<Column> GenerateReportHeaders()
		{
			var columns = new List<Column>
			{
				new Column {Header = "№ п/п", Width = 3},
				new Column {Header = "Тип", Width = 3},
				new Column {Header = "Кадастровый номер", Width = ColumnWidthForCadastralNumber},
				new Column {Header = "Номер подгруппы", Width = 4},
				new Column {Header = "Метод оценки", Width = 4}
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
				item.PropertyType,
				item.CadastralNumber,
				item.GroupNumber,
				item.ModelCalculationMethod
			};
		}



		#region Support Methods

		private string GetBaseSql(ReportLongProcessOnlyTasksInputParameters parameters)
		{
			var tourId = GetTourFromTasks(parameters.TaskIds);

			var baseFolderWithSql = "QualityPricingFactorsEncodingResults";
			var sql = StatisticalDataService.GetSqlFileContent(baseFolderWithSql, "GroupingForLongProcess");

			var codeGroupAttributeId = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId).Id;

			var sqlWithParameters = string.Format(sql, "{0}", codeGroupAttributeId);

			return sqlWithParameters;
		}

		#endregion


		#region Entities

		public class ReportItem
		{
			public string PropertyType { get; set; }
			public string CadastralNumber { get; set; }
			public string GroupNumber { get; set; }
			public string ModelCalculationMethod { get; set; }
		}

		#endregion
	}
}
