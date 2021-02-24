using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.AdditionalForms.AnalysisOfCalculations
{
	public class AnalysisOfCalculationsLongProcess: ALinearReportsLongProcessTemplate<AnalysisOfCalculationsLongProcess.ReportItem, ReportLongProcessOnlyTasksInputParameters>
	{
		private readonly string _reportSqlFileName = "AdditionalForms_CalculationAnalysis";
		private readonly GbuCodRegisterService _gbuCodRegisterService;
		private readonly RosreestrRegisterService _rosreestrRegisterService;
		private List<long> _taskIdList;
		private string UnitsConditionToCount { get; set; }

		public class ReportItem
		{
			public string Address { get; set; }
			public string Location { get; set; }
			public string EvaluationSubgroup2018 { get; set; }
			public string Upks2018 { get; set; }
			public string CadastralCost2018 { get; set; }
			public string CadastralQuartal2018 { get; set; }
			public string TaskType { get; set; }
			public string EvaluationSubgroup { get; set; }
			public string Upks { get; set; }
			public string CadastralCost { get; set; }
			public string CadastralQuartal { get; set; }
			public string EGRNChangeDate { get; set; }
			public string Status { get; set; }
			public string Changes { get; set; }
			public string MinUpksByCadastralQuartal { get; set; }
			public string AverageUpksByCadastralQuartal { get; set; }
			public string MaxUpksByCadastralQuartal { get; set; }
			public string MinUpksByZone { get; set; }
			public string AverageUpksByZone { get; set; }
			public string MaxUpksByZone { get; set; }
			public string ParticipatingCount { get; set; }
			public string CountInYear { get; set; }
			public string CountInDays { get; set; }
		}

		public AnalysisOfCalculationsLongProcess() : base(Log.ForContext<AnalysisOfCalculationsLongProcess>())
		{
			_gbuCodRegisterService = new GbuCodRegisterService();
			_rosreestrRegisterService = new RosreestrRegisterService();
		}

		protected override bool AreInputParametersValid(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return inputParameters?.TaskIds?.Count > 0;
		}

		protected override string ReportName  => "Анализ расчетов";
		protected override string ProcessName => nameof(AnalysisOfCalculationsLongProcess);

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 4;

			return GetProcessConfigFromSettings("AnalysisOfCalculationsReport", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportLongProcessOnlyTasksInputParameters inputParameters, QueryManager queryManager)
		{
			if(inputParameters != null && inputParameters.TaskIds?.Count > 0)
			{
				return GetMaxUnitsCount(UnitsConditionToCount, queryManager);
			}

			return 0;
		}

		protected override Func<ReportItem, string> GetSortingCondition()
		{
			throw new NotImplementedException();
		}

		protected override List<GbuReportService.Column> GenerateReportHeaders()
		{
			var columns = new List<GbuReportService.Column>
			{
				new GbuReportService.Column
				{
					Header = "№ п/п",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "1",
					Width = 6
				},
				new GbuReportService.Column
				{
					Header = "2",
					Width = 5
				},
				new GbuReportService.Column
				{
					Header = "3",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "4",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "5",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "6",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "7",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "8",
					Width = 4
				},

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
				item.Address,
				item.Location,
				item.EvaluationSubgroup2018,
				item.Upks2018,
				item.CadastralCost2018,
				item.TaskType,
				item.EvaluationSubgroup,
				item.Upks,
				item.CadastralCost,
				item.CadastralQuartal,
				item.EGRNChangeDate,
				item.Status,
				item.Changes,
				item.MinUpksByCadastralQuartal,
				item.AverageUpksByCadastralQuartal,
				item.MaxUpksByCadastralQuartal,
				item.MinUpksByZone,
				item.AverageUpksByZone,
				item.MaxUpksByZone,
				item.ParticipatingCount,
				item.CountInYear,
				item.CountInDays
			};
		}

		protected override void PrepareVariables(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			_taskIdList = inputParameters.TaskIds;

			UnitsConditionToCount = $"where TASK_ID in {string.Join(", ", _taskIdList)} and property_type_code<>2190";
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			string contents;
			using (var sr = new StreamReader(StatisticalDataService.GetSqlQueryFileStream(_reportSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", _taskIdList),
				_gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id,
				_rosreestrRegisterService.GetSquareAttribute().Id,
				_rosreestrRegisterService.GetObjectNameAttribute().Id,
				_rosreestrRegisterService.GetTypeOfUseByDocumentsAttribute().Id,
				_rosreestrRegisterService.GetBuildingPurposeAttribute().Id,
				_rosreestrRegisterService.GetPlacementPurposeAttribute().Id,
				_rosreestrRegisterService.GetConstructionPurposeAttribute().Id,
				_rosreestrRegisterService.GetAddressAttribute().Id,
				_rosreestrRegisterService.GetLocationAttribute().Id,
				packageSize,
				packageIndex
			);

			return sql;
		}
	}
}