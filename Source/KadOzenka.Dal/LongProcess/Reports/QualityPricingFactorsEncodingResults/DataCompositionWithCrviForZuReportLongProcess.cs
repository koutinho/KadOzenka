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
	public class DataCompositionWithCrviForZuReportLongProcess : ALinearReportsLongProcessTemplate<DataCompositionWithCrviForZuReportLongProcess.ReportItem, InputParametersForZu>
	{
		protected override string ReportName => "Состав данных объектов недвижимости с присвоенными крви (ЗУ)";
		protected override string ProcessName => nameof(DataCompositionWithCrviForZuReportLongProcess);
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }


		public DataCompositionWithCrviForZuReportLongProcess() : base(Log.ForContext<DataCompositionWithCrviForZuReportLongProcess>())
		{
		}


		protected override bool AreInputParametersValid(InputParametersForZu inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0 &&
			       inputParameters.LinkedObjectsInfoAttributeId != 0 &&
			       inputParameters.LinkedObjectsInfoSourceAttributeId != 0 &&
			       inputParameters.TypeOfUsingNameAttributeId != 0 &&
			       inputParameters.TypeOfUsingCodeAttributeId != 0 &&
			       inputParameters.TypeOfUsingCodeSourceAttributeId != 0 &&
			       inputParameters.SegmentAttributeId != 0;
		}

		protected override void PrepareVariables(InputParametersForZu inputParameters)
		{
			BaseSql = GetBaseSql(inputParameters);
			TaskIdsStr = string.Join(',', inputParameters.TaskIds);

			BaseUnitsCondition = $@" where unit.TASK_ID IN ({TaskIdsStr}) AND 
										unit.PROPERTY_TYPE_CODE = 4";
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 3;

			return GetProcessConfigFromSettings("DataCompositionWithCrviForZu", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(InputParametersForZu inputParameters)
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
				new GbuReportService.Column {Header = "Разрешенное использование", Width = 3},
				new GbuReportService.Column {Header = "Адрес", Width = ColumnWidthForAddresses},
				new GbuReportService.Column {Header = "Местоположение", Width = ColumnWidthForAddresses},
				new GbuReportService.Column {Header = "Кадастровый квартал", Width = 3},
				new GbuReportService.Column {Header = "Сведения о нахождении на земельном участке других связанных с ним объектов недвижимости", Width = 3},
				new GbuReportService.Column {Header = "Источник информации о нахождении на земельном участке других связанных с ним объектов недвижимости", Width = 3},
				new GbuReportService.Column {Header = "Сегмент", Width = 3},
				new GbuReportService.Column {Header = "Наименование вида использования", Width = 3},
				new GbuReportService.Column {Header = "Код вида использования", Width = 3},
				new GbuReportService.Column {Header = "Источник информации кода вида использования", Width = 3}
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
				item.PermittedUsing,
				item.Address,
				item.Location,
				item.CadastralQuarter,
				item.LinkedObjectsInfo,
				item.LinkedObjectsInfoSource,
				item.Segment,
				item.TypeOfUsingName,
				item.TypeOfUsingCode,
				item.TypeOfUsingCodeSource
			};
		}



		#region Support Methods

		private string GetBaseSql(InputParametersForZu inputParameters)
		{
			var baseFolderWithSql = "QualityPricingFactorsEncodingResults";
			var sql = StatisticalDataService.GetSqlFileContent(baseFolderWithSql, "CrviForZu");

			var rosreestrRegisterService = new RosreestrRegisterService();
			var gbuCodRegisterService = new GbuCodRegisterService();

			var linkedObjectsInfoAttributeId = RegisterCache.GetAttributeData(inputParameters.LinkedObjectsInfoAttributeId).Id;
			var linkedObjectsInfoSourceAttributeId = RegisterCache.GetAttributeData(inputParameters.LinkedObjectsInfoSourceAttributeId).Id;
			var segmentAttributeId = RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId).Id;
			var typeOfUsingNameAttributeId = RegisterCache.GetAttributeData(inputParameters.TypeOfUsingNameAttributeId).Id;
			var typeOfUsingCodeAttributeId = RegisterCache.GetAttributeData(inputParameters.TypeOfUsingCodeAttributeId).Id;
			var typeOfUsingCodeSourceAttributeId = RegisterCache.GetAttributeData(inputParameters.TypeOfUsingCodeSourceAttributeId).Id;

			var sqlWithParameters = string.Format(sql, "{0}",
				linkedObjectsInfoAttributeId, linkedObjectsInfoSourceAttributeId, segmentAttributeId, 
				typeOfUsingNameAttributeId, typeOfUsingCodeAttributeId, typeOfUsingCodeSourceAttributeId,
				rosreestrRegisterService.GetParcelNameAttribute().Id,
				rosreestrRegisterService.GetTypeOfUseByDocumentsAttribute().Id,
				rosreestrRegisterService.GetAddressAttribute().Id,
				rosreestrRegisterService.GetLocationAttribute().Id,
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
			public string PermittedUsing { get; set; }
			public string Address { get; set; }
			public string Location { get; set; }
			public string CadastralQuarter { get; set; }
			public string LinkedObjectsInfo { get; set; }
			public string LinkedObjectsInfoSource { get; set; }
			public string Segment { get; set; }
			public string TypeOfUsingName { get; set; }
			public string TypeOfUsingCode { get; set; }
			public string TypeOfUsingCodeSource { get; set; }
		}

		#endregion
	}
}
