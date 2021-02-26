using System;
using System.Collections.Generic;
using Core.Register;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults.Entities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults
{
	public class DataCompositionWithCrviForOksReportLongProcess : ALinearReportsLongProcessTemplate<DataCompositionWithCrviForOksReportLongProcess.ReportItem, InputParametersForOks>
	{
		protected override string ReportName => "Состав данных объектов недвижимости с присвоенными крви (ОКС)";
		protected override string ProcessName => nameof(DataCompositionWithCrviForOksReportLongProcess);
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }


		public DataCompositionWithCrviForOksReportLongProcess() : base(Log.ForContext<DataCompositionWithCrviForOksReportLongProcess>())
		{
		}


		protected override bool AreInputParametersValid(InputParametersForOks inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0 &&
				inputParameters.ParentKnAttributeId != 0 &&
				inputParameters.TypeOfUsingNameAttributeId != 0 &&
				inputParameters.TypeOfUsingCodeAttributeId != 0 &&
				inputParameters.TypeOfUsingCodeSourceAttributeId != 0 &&
				inputParameters.TypeOfUsingGroupCodeAttributeId != 0 &&
				inputParameters.SegmentAttributeId != 0 &&
				inputParameters.FunctionalGroupNameAttributeId != 0;
		}

		protected override void PrepareVariables(InputParametersForOks inputParameters)
		{
			BaseSql = GetBaseSql(inputParameters);
			TaskIdsStr = string.Join(',', inputParameters.TaskIds);

			BaseUnitsCondition = $@" where unit.TASK_ID IN ({TaskIdsStr}) AND 
										unit.PROPERTY_TYPE_CODE <> 2190 AND
										unit.PROPERTY_TYPE_CODE <> 4";
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 3;

			return GetProcessConfigFromSettings("DataCompositionWithCrviForOks", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(InputParametersForOks inputParameters)
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
			return "Состав данных объектов недвижимости с присвоенными видами использования";
		}

		protected override List<GbuReportService.Column> GenerateReportHeaders()
		{
			var columns = new List<GbuReportService.Column>
			{
				new GbuReportService.Column {Header = "№ п/п", Width = 3},
				new GbuReportService.Column {Header = "Тип объекта", Width = 3},
				new GbuReportService.Column {Header = "Кадастровый номер", Width = ColumnWidthForCadastralNumber},
				new GbuReportService.Column {Header = "Площадь", Width = ColumnWidthForDecimals},
				new GbuReportService.Column {Header = "Наименование", Width = 3},
				new GbuReportService.Column {Header = "Назначение", Width = 3},
				new GbuReportService.Column {Header = "Адрес", Width = ColumnWidthForAddresses},
				new GbuReportService.Column {Header = "Местоположение", Width = ColumnWidthForAddresses},
				new GbuReportService.Column {Header = "Кадастровый квартал", Width = 3},
				new GbuReportService.Column {Header = "КН родителя (для помещения)", Width = 3},
				new GbuReportService.Column {Header = "Кадастровый номер земельного участка", Width = ColumnWidthForCadastralNumber},
				new GbuReportService.Column {Header = "Год постройки", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Год ввода в эксплуатацию", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Кол-во этажей", Width = 3},
				new GbuReportService.Column {Header = "Подземных этажей", Width = 3},
				new GbuReportService.Column {Header = "Этаж (для помещения)", Width = 3},
				new GbuReportService.Column {Header = "Материал стен", Width = 3},
				new GbuReportService.Column {Header = "Наименование вида использования", Width = 3},
				new GbuReportService.Column {Header = "Код вида использования", Width = 3},
				new GbuReportService.Column {Header = "Источник информации кода вида использования", Width = 3},
				new GbuReportService.Column {Header = "Код подгруппы вида использования", Width = 3},
				new GbuReportService.Column {Header = "Наименование функциональной подгруппы", Width = 3},
				new GbuReportService.Column {Header = "Сегмент", Width = 3}
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
				item.Square,
				item.Name,
				item.Purpose,
				item.Address,
				item.Location,
				item.CadastralQuarter,
				item.ParentKn,
				item.ZuCadastralNumber,
				item.BuildingYear,
				item.CommissioningYear,
				item.FloorCount,
				item.UndergroundFloorCount,
				item.FloorNumber,
				item.WallMaterial,
				item.TypeOfUsingName,
				item.TypeOfUsingCode,
				item.TypeOfUsingCodeSource,
				item.TypeOfUsingGroupCode,
				item.FunctionalGroupName,
				item.Segment
			};
		}



		#region Support Methods

		private string GetBaseSql(InputParametersForOks inputParameters)
		{
			var baseFolderWithSql = "QualityPricingFactorsEncodingResults";
			var sql = StatisticalDataService.GetSqlFileContent(baseFolderWithSql, "CrviForOks");

			var rosreestrRegisterService = new RosreestrRegisterService();
			var gbuCodRegisterService = new GbuCodRegisterService();

			var parentKnAttributeId = RegisterCache.GetAttributeData(inputParameters.ParentKnAttributeId);
			var typeOfUsingNameAttributeId = RegisterCache.GetAttributeData(inputParameters.TypeOfUsingNameAttributeId);
			var typeOfUsingCodeAttributeId = RegisterCache.GetAttributeData(inputParameters.TypeOfUsingCodeAttributeId);
			var typeOfUsingCodeSourceAttributeId = RegisterCache.GetAttributeData(inputParameters.TypeOfUsingCodeSourceAttributeId);
			var typeOfUsingGroupCodeAttributeId = RegisterCache.GetAttributeData(inputParameters.TypeOfUsingGroupCodeAttributeId);
			var segmentAttributeId = RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId);
			var functionalGroupNameAttributeId = RegisterCache.GetAttributeData(inputParameters.FunctionalGroupNameAttributeId);

			var sqlWithParameters = string.Format(sql, "{0}", parentKnAttributeId.Id, typeOfUsingNameAttributeId.Id,
				typeOfUsingCodeAttributeId.Id, typeOfUsingCodeSourceAttributeId.Id, typeOfUsingGroupCodeAttributeId.Id,
				segmentAttributeId.Id, functionalGroupNameAttributeId.Id,
				rosreestrRegisterService.GetObjectNameAttribute().Id,
				rosreestrRegisterService.GetConstructionPurposeAttribute().Id,
				rosreestrRegisterService.GetAddressAttribute().Id,
				rosreestrRegisterService.GetLocationAttribute().Id,
				rosreestrRegisterService.GetParcelAttribute().Id,
				rosreestrRegisterService.GetBuildYearAttribute().Id,
				rosreestrRegisterService.GetCommissioningYearAttribute().Id,
				rosreestrRegisterService.GetFloorsNumberAttribute().Id,
				rosreestrRegisterService.GetUndergroundFloorsNumberAttribute().Id,
				rosreestrRegisterService.GetFloorAttribute().Id,
				rosreestrRegisterService.GetWallMaterialAttribute().Id,
				gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id
			);

			return sqlWithParameters;
		}

		#endregion


		#region Entities

		public class ReportItem
		{
			public string PropertyType { get; set; }
			public string CadastralNumber { get; set; }
			public decimal? Square { get; set; }
			public string Name { get; set; }
			public string Purpose { get; set; }
			public string Address { get; set; }
			public string Location { get; set; }
			public string CadastralQuarter { get; set; }
			public string ParentKn { get; set; }
			public string ZuCadastralNumber { get; set; }
			public string BuildingYear { get; set; }
			public string CommissioningYear { get; set; }
			public string FloorCount { get; set; }
			public string UndergroundFloorCount { get; set; }
			public string FloorNumber { get; set; }
			public string WallMaterial { get; set; }
			public string TypeOfUsingName { get; set; }
			public string TypeOfUsingCode { get; set; }
			public string TypeOfUsingCodeSource { get; set; }
			public string Segment { get; set; }
			public string TypeOfUsingGroupCode { get; set; }
			public string FunctionalGroupName { get; set; }
		}

		#endregion
	}
}
