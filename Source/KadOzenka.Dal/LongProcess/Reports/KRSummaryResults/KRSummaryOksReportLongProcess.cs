using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.LongProcess.Reports.KRSummaryResults.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.KRSummaryResults
{
	public class KRSummaryOksReportLongProcess : ALinearReportsLongProcessTemplate<KRSummaryOksReportLongProcess.KRSummaryResultsOksDto, OksReportLongProcessInputParameters>
	{
		protected override string ReportName => "Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (ОКС)";
		protected override string ProcessName => nameof(KRSummaryOksReportLongProcess);
		protected StatisticalDataService StatisticalDataService { get; set; }
		protected RosreestrRegisterService RosreestrRegisterService { get; set; }
		protected GbuCodRegisterService GbuCodRegisterService { get; set; }
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }

		private readonly string _reportOksSqlFileName = "KRSummaryResultsOKS";
		protected override long ReportCode => (long)StatisticalDataType.KRSummaryResultsOks;


		public KRSummaryOksReportLongProcess() : base(Log.ForContext<KRSummaryOksReportLongProcess>())
		{
			StatisticalDataService = new StatisticalDataService();
			RosreestrRegisterService = new RosreestrRegisterService();
			GbuCodRegisterService = new GbuCodRegisterService();
		}


		protected override bool AreInputParametersValid(OksReportLongProcessInputParameters inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0;
		}

		protected override void PrepareVariables(OksReportLongProcessInputParameters inputParameters)
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

		protected override int GetMaxItemsCount(OksReportLongProcessInputParameters inputParameters)
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

		protected override Func<IEnumerable<KRSummaryResultsOksDto>, IEnumerable<KRSummaryResultsOksDto>> FuncForDownloadedItems()
		{
			return x => x.OrderBy(y => y.CadastralNumber);
		}

		protected override string GenerateReportTitle()
		{
			return "Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (ОКС)";
		}

		protected override List<Column> GenerateReportHeaders()
		{
			var columns = new List<Column>
			{
				new Column{Header = "№ п/п",Width = 2},
				new Column{Header = "КН",Width = ColumnWidthForCadastralNumber},
				new Column{Header = "Тип",Width = 5},
				new Column{Header = "Площадь"},
				new Column{Header = "Наименование",Width = 6},
				new Column{Header = "Назначение",Width = 6},
				new Column{Header = "Адрес",Width = 9},
				new Column{Header = "КЛАДР",Width = 9},
				new Column{Header = "КН родителя",Width = 5},
				new Column{Header = "Местоположение",Width = 9},
				new Column{Header = "Кадастровый квартал",Width = 5},
				new Column{Header = "Кадастровый номер земельного участка",Width = 5},
				new Column{Header = "Год постройки"},
				new Column{Header = "Год ввода в эксплуатацию"},
				new Column{Header = "Кол-во этажей"},
				new Column{Header = "Подземных этажей"},
				new Column{Header = "Этаж (для помещения)"},
				new Column{Header = "Материал стен",Width = 5},
				new Column{Header = "Процент готовности"},
				new Column{Header = "УПКС объекта недвижимости, руб./кв.м.",Width = 7},
				new Column{Header = "Кадастровая стоимость объекта недвижимости, руб.",Width = 7},
			};

			var counter = 0;
			columns.ForEach(x => x.Index = counter++);

			return columns;
		}

		protected override List<object> GenerateReportReportRow(int index, KRSummaryResultsOksDto item)
		{
			return new List<object>
			{
				(index + 1).ToString(),
				item.CadastralNumber,
				item.PropertyType,
				item.Square,
				item.Name,
				item.Purpose,
				item.Address,
				item.Kladr,
				item.ParentKn,
				item.Location,
				item.CadastralQuarter,
				item.ZuCadastralNumber,
				item.BuildingYear,
				item.CommissioningYear,
				item.FloorCount,
				item.UndergroundFloorCount,
				item.FloorNumber,
				item.WallMaterial,
				item.AvailabilityPercentage,
				item.Upks,
				item.CadastralCost
			};
		}



		#region Support Methods

		private string GetBaseSql(OksReportLongProcessInputParameters parameters)
		{
			string contents;
			using (var sr = new StreamReader(StatisticalDataService.GetSqlQueryFileStream(_reportOksSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", parameters.TaskIds),
				RegisterCache.GetAttributeData(parameters.KladrAttributeId).Id,
				RegisterCache.GetAttributeData(parameters.ParentKnAttributeId).Id,
				RosreestrRegisterService.GetObjectNameAttribute().Id,
				RosreestrRegisterService.GetConstructionPurposeAttribute().Id,
				RosreestrRegisterService.GetAddressAttribute().Id,
				RosreestrRegisterService.GetLocationAttribute().Id,
				RosreestrRegisterService.GetParcelAttribute().Id,
				RosreestrRegisterService.GetBuildYearAttribute().Id,
				RosreestrRegisterService.GetCommissioningYearAttribute().Id,
				RosreestrRegisterService.GetFloorsNumberAttribute().Id,
				RosreestrRegisterService.GetUndergroundFloorsNumberAttribute().Id,
				RosreestrRegisterService.GetFloorAttribute().Id,
				RosreestrRegisterService.GetWallMaterialAttribute().Id,
				GbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id
			);
			return sql;
		}

		#endregion


		#region Entities

		public class KRSummaryResultsOksDto
		{
			public string CadastralNumber { get; set; }
			public string PropertyType { get; set; }
			public decimal? Square { get; set; }
			public string Name { get; set; }
			public string Purpose { get; set; }
			public string Address { get; set; }
			public string Kladr { get; set; }
			public string ParentKn { get; set; }
			public string Location { get; set; }
			public string CadastralQuarter { get; set; }
			public string ZuCadastralNumber { get; set; }
			public string BuildingYear { get; set; }
			public string CommissioningYear { get; set; }
			public string FloorCount { get; set; }
			public string UndergroundFloorCount { get; set; }
			public string FloorNumber { get; set; }
			public string WallMaterial { get; set; }
			public long? AvailabilityPercentage { get; set; }
			public decimal? Upks { get; set; }
			public decimal? CadastralCost { get; set; }
		}

		#endregion
	}
}
