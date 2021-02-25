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
	public class OksReportLongProcess : ALinearReportsLongProcessTemplate<OksReportLongProcess.KRSummaryResultsOksDto, OksReportLongProcessInputParameters>
	{
		protected override string ReportName => "Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (ОКС)";
		protected override string ProcessName => nameof(OksReportLongProcess);
		protected StatisticalDataService StatisticalDataService { get; set; }
		protected RosreestrRegisterService RosreestrRegisterService { get; set; }
		protected GbuCodRegisterService GbuCodRegisterService { get; set; }
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }

		private readonly string _reportOksSqlFileName = "KRSummaryResultsOKS";

		public OksReportLongProcess() : base(Log.ForContext<OksReportLongProcess>())
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

		protected override int GetMaxItemsCount(OksReportLongProcessInputParameters inputParameters,
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

		protected override Func<KRSummaryResultsOksDto, string> GetSortingCondition()
		{
			return x => x.CadastralNumber;
		}

		protected override string GenerateReportTitle()
		{
			return "Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (ОКС)";
		}

		protected override List<GbuReportService.Column> GenerateReportHeaders()
		{
			var columns = new List<GbuReportService.Column>
			{
				new GbuReportService.Column{Header = "№ п/п",Width = 3},
				new GbuReportService.Column{Header = "КН",Width = 5},
				new GbuReportService.Column{Header = "Тип",Width = 5},
				new GbuReportService.Column{Header = "Площадь",Width = 3},
				new GbuReportService.Column{Header = "Наименование",Width = 6},
				new GbuReportService.Column{Header = "Назначение",Width = 6},
				new GbuReportService.Column{Header = "Адрес",Width = 9},
				new GbuReportService.Column{Header = "КЛАДР",Width = 9},
				new GbuReportService.Column{Header = "КН родителя",Width = 5},
				new GbuReportService.Column{Header = "Местоположение",Width = 9},
				new GbuReportService.Column{Header = "Кадастровый квартал",Width = 5},
				new GbuReportService.Column{Header = "Кадастровый номер земельного участка",Width = 5},
				new GbuReportService.Column{Header = "Год постройки",Width = 3},
				new GbuReportService.Column{Header = "Год ввода в эксплуатацию",Width = 3},
				new GbuReportService.Column{Header = "Кол-во этажей",Width = 3},
				new GbuReportService.Column{Header = "Подземных этажей",Width = 3},
				new GbuReportService.Column{Header = "Этаж (для помещения)",Width = 3},
				new GbuReportService.Column{Header = "Материал стен",Width = 5},
				new GbuReportService.Column{Header = "Процент готовности",Width = 3},
				new GbuReportService.Column{Header = "УПКС объекта недвижимости, руб./кв.м.",Width = 7},
				new GbuReportService.Column{Header = "Кадастровая стоимость объекта недвижимости, руб.",Width = 7},
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
