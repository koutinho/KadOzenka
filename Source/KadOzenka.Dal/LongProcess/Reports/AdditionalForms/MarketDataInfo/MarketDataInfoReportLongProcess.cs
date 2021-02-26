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

		protected override int GetMaxItemsCount(ReportInputParams inputParameters, QueryManager queryManager)
		{
			if (inputParameters != null && inputParameters.DateFrom < inputParameters.DateTo)
			{
				return GetMaxAnalogCount(CommonConditionToCount, queryManager);
			}

			return 0;
		}

		protected override Func<ReportItem, string> GetSortingCondition()
		{
			return x => "";
		}

		protected override string GenerateReportTitle()
		{
			return $"Состав данных о рыночной информации (с {_inputReportData.DateFrom?.ToString(DateFormat)} по {_inputReportData.DateTo?.ToString(DateFormat)})";
		}


		protected override List<GbuReportService.Column> GenerateReportHeaders()
		{
			var columns = new List<GbuReportService.Column>
			{
				new GbuReportService.Column
				{
					Header = "№ п/п",
					Width = 3
				}
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
				item.AdText,
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
			CommonConditionToCount = "where PROCESS_TYPE_CODE = 742 " +
			                         $"and (IS_ACTIVE <> 0 or IS_ACTIVE is null) and (case when {inputParameters.DateFrom} is not null" +
			                         $" then((LAST_DATE_UPDATE IS NOT NULL AND L1_R100.LAST_DATE_UPDATE >= {inputParameters.DateFrom})" +
			                         $"or LAST_DATE_UPDATE IS NULL AND L1_R100.PARSER_TIME >= {inputParameters.DateFrom})) else true end)" +
			                         $"and (case when {inputParameters.DateTo} then ((LAST_DATE_UPDATE IS NOT NULL AND L1_R100.LAST_DATE_UPDATE <= {inputParameters.DateTo})" +
			                         $"or (LAST_DATE_UPDATE IS NULL AND PARSER_TIME <= {inputParameters.DateTo}))else true end)";
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
				RegisterCache.GetAttributeData(_inputReportData.TypeOfUseAttributeId).Id);

			return sql;
		}

		private int GetMaxAnalogCount(string baseCondition, QueryManager queryManager)
		{
			var columnName = "count";
			var countSql = $@"select count(*) as {columnName} from  MARKET_CORE_OBJECT analog {baseCondition}";
			var dataSet = queryManager.ExecuteSqlStringToDataSet(countSql);

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