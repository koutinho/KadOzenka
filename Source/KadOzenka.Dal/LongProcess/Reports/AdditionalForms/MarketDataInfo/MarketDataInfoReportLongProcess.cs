using System;
using System.Collections.Generic;
using System.IO;
using Core.Register;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.AdditionalForms.MarketDataInfo.Entities;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.AdditionalForms.MarketDataInfo
{
	public class MarketDataInfoReportLongProcess: ALinearReportsLongProcessTemplate<ReportItem, ReportInputParams>
	{
		private string CommonConditionToCount { get; set; }
		private string _reportMarketFileName = "AdditionalForms_Market";
		private ReportInputParams _inputReportData = new ReportInputParams();
		public static readonly int PrecisionForDecimalValues = 2;
		public static readonly string DateFormat = "dd.MM.yyyy";

		public MarketDataInfoReportLongProcess() : base(Log.ForContext<MarketDataInfoReportLongProcess>())
		{
		}

		protected override bool AreInputParametersValid(ReportInputParams inputParameters)
		{
			return inputParameters.DateFrom.HasValue && inputParameters.DateTo.HasValue &&
			       inputParameters.DateTo > inputParameters.DateFrom;
		}

		protected override string ReportName => "Состав данных о рыночной информации";
		protected override string ProcessName => nameof(MarketDataInfoReportLongProcess);
		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 4;

			return GetProcessConfigFromSettings(" MarketDataInfoReport", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportInputParams inputParameters)
		{
			return GetMaxAnalogCount(CommonConditionToCount);
		}

		protected override string GenerateReportTitle()
		{
			return $"Состав данных о рыночной информации (с {_inputReportData.DateFrom?.ToString(DateFormat)} по {_inputReportData.DateTo?.ToString(DateFormat)})";
		}


		protected override List<Column> GenerateReportHeaders()
		{
			var columns = new List<Column>
			{
				new Column {Header = "№ п/п"},
				new Column {Header = "Уникальный номер"},
				new Column {Header = "Кадастровый номер", Width = 4},
				new Column {Header = "Группа сегмента рынка"},
				new Column {Header = "Код вида использования"},
				new Column {Header = "Группа ОКС"},
				new Column {Header = "Код субъекта РФ"},
				new Column {Header = "Код муниципального образования (ОКТМО)"},
				new Column {Header = "Адресный ориентир", Width = 6},
				new Column {Header = "Метро"},
				new Column {Header = "Источник информации"},
				new Column {Header = "Адрес ссылки"},
				new Column {Header = "Номер телефона"},
				new Column {Header = "Дата предложения (сделки)"},
				new Column {Header = "Вид объекта недвижимости"},
				new Column {Header = "Вид использования (функциональное назначение)"},
				new Column {Header = "Вид права"},
				new Column {Header = "Количество комнат"},
				new Column {Header = "Факт сделки (сделка, предложения)"},
				new Column {Header = "Площадь"},
				new Column {Header = "Цена сделки/предложения"},
				new Column {Header = "Удельная цена сделки/предложения"},
				new Column {Header = "Удельная годовая ставка аренды"}
			};

			var counter = 0;
			columns.ForEach(item => item.Index = counter++);

			return columns;
		}

		protected override List<object> GenerateReportReportRow(int index, ReportItem item)
		{
			return new List<object>
			{
				(index + 1).ToString(),
				item.UniqueNumber,
				item.Kn,
				item.SegmentGroup,
				item.TypeOfUseCode,
				item.OksGroup,
				item.SubjectCode,
				item.OKTMO,
				item.AddressReferencePoint,
				item.Metro,
				item.Market,
				item.Link,
				item.Phone,
				item.Date?.ToString(DateFormat),
				item.TypeOfProperty,
				item.TypeOfUseCode,
				item.TypeOfRight,
				item.RoomCount,
				item.DealSuggestion,
				item.Square,
				item.Price,
				(item.Upks.HasValue
					? Math.Round(item.Upks.Value, PrecisionForDecimalValues)
					: (decimal?)null),
				(item.AnnualRateOfRent.HasValue
					? Math.Round(item.AnnualRateOfRent.Value, PrecisionForDecimalValues)
					: (decimal?)null)
			};
		}

		protected override void PrepareVariables(ReportInputParams inputParameters)
		{
			_inputReportData = inputParameters;
			var dateFrom = _inputReportData.DateFrom.HasValue
				? CrossDBSQL.ToDate(_inputReportData.DateFrom.Value)
				: "null";

			var dateTo = _inputReportData.DateTo.HasValue ? CrossDBSQL.ToDate(_inputReportData.DateTo.Value) : "null";
			CommonConditionToCount = "where (PROCESS_TYPE_CODE = 742 " +
			                         $"and (gbu.IS_ACTIVE <> 0 or gbu.IS_ACTIVE is null) and (case when {dateFrom} is not null" +
			                         $" then((LAST_DATE_UPDATE IS NOT NULL AND LAST_DATE_UPDATE >= {dateFrom})" +
			                         $"or (LAST_DATE_UPDATE IS NULL AND PARSER_TIME >= {dateFrom})) else true end)" +
			                         $"and (case when {dateTo} is not null then ((LAST_DATE_UPDATE IS NOT NULL AND LAST_DATE_UPDATE <= {dateTo})" +
			                         $"or (LAST_DATE_UPDATE IS NULL AND PARSER_TIME <= {dateTo}))else true end))";
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			string contents;
			using (var sr = new StreamReader(StatisticalDataService.GetSqlQueryFileStream(_reportMarketFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents,
				_inputReportData.DateFrom.HasValue ? CrossDBSQL.ToDate(_inputReportData.DateFrom.Value) : "null",
				_inputReportData.DateTo.HasValue ? CrossDBSQL.ToDate(_inputReportData.DateTo.Value) : "null",
				RegisterCache.GetAttributeData(_inputReportData.TypeOfUseCodeAttributeId).Id,
				RegisterCache.GetAttributeData(_inputReportData.OksGroupAttributeId).Id,
				RegisterCache.GetAttributeData(_inputReportData.TypeOfUseAttributeId).Id,
				packageSize,
				packageIndex
				);

			return sql;
		}

		private int GetMaxAnalogCount(string baseCondition)
		{
			var columnName = "count";
			var countSql = $@"select count(*) as {columnName} from  MARKET_CORE_OBJECT analog left JOIN GBU_MAIN_OBJECT gbu ON analog.CADASTRAL_NUMBER = gbu.CADASTRAL_NUMBER {baseCondition}";
			var dataSet = QueryManager.ExecuteSqlStringToDataSet(countSql);

			var count = 0;
			var row = dataSet.Tables[0]?.Rows[0];
			if (row != null)
			{
				count = row[columnName].ParseToInt();
			}

			return count;
		}
	}
}