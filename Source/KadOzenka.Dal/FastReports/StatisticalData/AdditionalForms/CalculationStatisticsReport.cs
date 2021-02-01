using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms
{
	public class CalculationStatisticsReport : StatisticalDataReport
	{
		private readonly AdditionalFormsService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public CalculationStatisticsReport()
		{
			_service = new AdditionalFormsService(StatisticalDataService);
			_logger = Log.ForContext<CalculationStatisticsReport>();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return "AdditionalFormsCalculationStatisticsReport";
		}

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			_service.QueryManager.SetBaseToken(CancellationToken);
			var taskIdList = GetTaskIdList(query);

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("SubgroupId", typeof(string));
			dataTable.Columns.Add("SubgroupName", typeof(string));
			dataTable.Columns.Add("CalculationMethod", typeof(string));
			dataTable.Columns.Add("Formula", typeof(string));
			dataTable.Columns.Add("FactorsSubgroups", typeof(string));
			dataTable.Columns.Add("Coef", typeof(string));
			dataTable.Columns.Add("SighMarket", typeof(string));

			var zuOksObjectType = GetQueryParam<string>("ZuOksObjectType", query);
			var data = _service.GetCalculationStatisticsData(taskIdList, zuOksObjectType == "ОКС");
			Logger.Debug("Найдено {Count} объектов", data?.Count);

			Logger.Debug("Начато формирование таблиц");
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.SubgroupId,
					unitDto.SubgroupName,
					unitDto.CalculationMethodName,
					unitDto.Formula,
					unitDto.FactorsSubgroups,
					unitDto.Coef,
					unitDto.SighMarket);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			Logger.Debug("Закончено формирование таблиц");

			return dataSet;
		}
	}
}
