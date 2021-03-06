//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Data;
//using System.Linq;
//using Core.UI.Registers.Reports.Model;
//using KadOzenka.Dal.FastReports.StatisticalData.Common;
//using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
//using Serilog;
//
//namespace KadOzenka.Dal.FastReports.StatisticalData.KRSummaryResults
//{
//	public class KRSummaryResultsZuReport : StatisticalDataReport
//	{
//		private readonly KRSummaryResultsService _summaryResultsService;
//		private readonly ILogger _logger;
//		protected override ILogger Logger => _logger;
//
//		public KRSummaryResultsZuReport()
//		{
//			_summaryResultsService = new KRSummaryResultsService(StatisticalDataService);
//			_logger = Log.ForContext<KRSummaryResultsZuReport>();
//		}
//
//
//		protected override string TemplateName(NameValueCollection query)
//		{
//			return nameof(KRSummaryResultsZuReport);
//		}
//
//		public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
//		{
//			if (initialisation)
//			{
//				var klardFilter = filterValues.FirstOrDefault(f => f.ParamName == "KlardAttribute");
//                InitialiseGbuAttributesFilterValue(klardFilter);
//            }
//		}
//
//		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
//		{
//			_summaryResultsService.QueryManager.SetBaseToken(CancellationToken);
//			var taskIdList = GetTaskIdList(query);
//
//			var klardAttributeId = GetQueryParam<long?>("KlardAttribute", query);
//			if (!klardAttributeId.HasValue)
//			{
//				throw new Exception("Не указан атрибут 'КЛАДР'");
//			}
//
//			var dataTitleTable = new DataTable("Common");
//			dataTitleTable.Columns.Add("Title");
//			dataTitleTable.Rows.Add("Сводные результаты государственной кадастровой оценки земельных участков по кадастровому району");
//
//			var dataTable = new DataTable("Data");
//			dataTable.Columns.Add("DataNum");
//			dataTable.Columns.Add("Kn");
//			dataTable.Columns.Add("PropertyType");
//			dataTable.Columns.Add("Square");
//			dataTable.Columns.Add("PermittedUsing");
//			dataTable.Columns.Add("Address");
//			dataTable.Columns.Add("Kladr");
//			dataTable.Columns.Add("Location");
//			dataTable.Columns.Add("CadastralQuarter");
//			dataTable.Columns.Add("LandCategory");
//			dataTable.Columns.Add("Upks");
//			dataTable.Columns.Add("CadastralCost");
//
//			var data = _summaryResultsService.GetKRSummaryResultsZuData(taskIdList, klardAttributeId.Value);
//			Logger.Debug("Найдено {Count} объектов", data?.Count);
//
//			Logger.Debug("Начато формирование таблиц");
//			var i = 1;
//			foreach (var unitDto in data)
//			{
//				dataTable.Rows.Add(i,
//					unitDto.CadastralNumber,
//					unitDto.PropertyType,
//					unitDto.Square,
//					unitDto.PermittedUsing,
//					unitDto.Address,
//					unitDto.Kladr,
//					unitDto.Location,
//					unitDto.CadastralQuarter,
//					unitDto.LandCategory,
//					unitDto.Upks,
//					unitDto.CadastralCost
//					);
//				i++;
//			}
//
//			var dataSet = new DataSet();
//			dataSet.Tables.Add(dataTable);
//			dataSet.Tables.Add(dataTitleTable);
//			Logger.Debug("Закончено формирование таблиц");
//
//			return dataSet;
//		}
//	}
//}
