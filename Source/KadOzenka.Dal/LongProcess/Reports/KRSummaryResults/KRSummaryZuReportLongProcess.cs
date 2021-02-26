using System;
using System.Collections.Generic;
using System.IO;
using Core.Register;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.LongProcess.Reports.KRSummaryResults.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.KRSummaryResults
{
	public class KRSummaryZuReportLongProcess : ALinearReportsLongProcessTemplate<KRSummaryZuReportLongProcess.KRSummaryResultsZuDto, ZuReportLongProcessInputParameters>
	{
		protected override string ReportName => "Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (ОКС)";
		protected override string ProcessName => nameof(KRSummaryZuReportLongProcess);
		protected StatisticalDataService StatisticalDataService { get; set; }
		protected RosreestrRegisterService RosreestrRegisterService { get; set; }
		protected GbuCodRegisterService GbuCodRegisterService { get; set; }
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }

		private readonly string _reportZuSqlFileName = "KRSummaryResultsZU";

		public KRSummaryZuReportLongProcess() : base(Log.ForContext<KRSummaryZuReportLongProcess>())
		{
			StatisticalDataService = new StatisticalDataService();
			RosreestrRegisterService = new RosreestrRegisterService();
			GbuCodRegisterService = new GbuCodRegisterService();
		}


		protected override bool AreInputParametersValid(ZuReportLongProcessInputParameters inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0;
		}

		protected override void PrepareVariables(ZuReportLongProcessInputParameters inputParameters)
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

			return GetProcessConfigFromSettings("KRSummaryResultsOKS", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ZuReportLongProcessInputParameters inputParameters)
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

		protected override Func<KRSummaryResultsZuDto, string> GetSortingCondition()
		{
			return x => x.CadastralNumber;
		}

		protected override string GenerateReportTitle()
		{
			return "Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (ЗУ)";
		}

		protected override List<Column> GenerateReportHeaders()
		{
			var columns = new List<Column>
			{
				new Column{Header = "№ п/п",Width = 2},
				new Column{Header = "КН",Width = ColumnWidthForCadastralNumber},
				new Column{Header = "Тип",Width = 5},
				new Column{Header = "Площадь",Width = 3},
				new Column{Header = "Разрешенное использование",Width = 6},
				new Column{Header = "Адрес",Width = 9},
				new Column{Header = "КЛАДР",Width = 9},
				new Column{Header = "Местоположение",Width = 9},
				new Column{Header = "Кадастровый квартал",Width = 5},
				new Column{Header = "Категория земель",Width = 3},
				new Column{Header = "УПКС объекта недвижимости, руб./кв.м.",Width = 7},
				new Column{Header = "Кадастровая стоимость объекта недвижимости, руб.",Width = 7},
			};

			var counter = 0;
			columns.ForEach(x => x.Index = counter++);

			return columns;
		}

		protected override List<object> GenerateReportReportRow(int index, KRSummaryResultsZuDto item)
		{
			return new List<object>
			{
				(index + 1).ToString(),
				item.CadastralNumber,
				item.PropertyType,
				item.Square,
				item.PermittedUsing,
				item.Address,
				item.Kladr,
				item.Location,
				item.CadastralQuarter,
				item.LandCategory,
				item.Upks,
				item.CadastralCost,
			};
		}

		#region Support Methods

		private string GetBaseSql(ZuReportLongProcessInputParameters parameters)
		{
			string contents;
			using (var sr = new StreamReader(StatisticalDataService.GetSqlQueryFileStream(_reportZuSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", parameters.TaskIds),
				RegisterCache.GetAttributeData(parameters.KladrAttributeId).Id,
				RosreestrRegisterService.GetTypeOfUseByDocumentsAttribute().Id,
				RosreestrRegisterService.GetAddressAttribute().Id,
				RosreestrRegisterService.GetLocationAttribute().Id,
				RosreestrRegisterService.GetParcelCategoryAttribute().Id,
				GbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id
			);
			return sql;
		}

		#endregion


		#region Entities

		public class KRSummaryResultsZuDto
		{
			public string CadastralNumber { get; set; }
			public string PropertyType { get; set; }
			public decimal? Square { get; set; }
			public string PermittedUsing { get; set; }
			public string Address { get; set; }
			public string Kladr { get; set; }
			public string Location { get; set; }
			public string CadastralQuarter { get; set; }
			public string LandCategory { get; set; }
			public decimal? Upks { get; set; }
			public decimal? CadastralCost { get; set; }
		}

		#endregion
	}
}
