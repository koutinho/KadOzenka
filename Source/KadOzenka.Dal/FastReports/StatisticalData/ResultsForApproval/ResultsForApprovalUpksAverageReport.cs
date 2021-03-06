using System;
using System.Collections.Generic;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsForApproval
{
	public abstract class ResultsForApprovalUpksAverageReport : StatisticalDataReport
	{
		protected readonly ResultsForApprovalService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		protected bool IsOks(NameValueCollection query)
		{
			var zuOksObjectType = GetQueryParam<string>("ZuOksObjectType", query);
			return zuOksObjectType == "ОКС";
		}

		protected ResultsForApprovalUpksAverageReport()
		{
			_service = new ResultsForApprovalService(StatisticalDataService, new GbuCodRegisterService());
			_logger = Log.ForContext<ResultsForApprovalUpksAverageReport>();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(ResultsForApprovalUpksAverageReport);
		}

		protected abstract string GetReportTitle(bool isOks);
		protected abstract string GetDataNameColumnText();
		protected abstract StatisticDataAreaDivisionType GetStatisticDataAreaDivisionTypeReport();

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			_service.QueryManager.SetBaseToken(CancellationToken);
			var taskIdList = GetTaskIdList(query);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Columns.Add("DataNameColumnText");
			dataTitleTable.Rows.Add(GetReportTitle(IsOks(query)),
				GetDataNameColumnText());

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("GroupName");
			dataTable.Columns.Add("UpksAverageWeight");

			var divisionType = GetStatisticDataAreaDivisionTypeReport();
			Logger.Debug("Тип разделения {DivisionType}", divisionType);

			var data = _service.GetResultsForApprovalUpksAverageData(taskIdList, divisionType, IsOks(query));
			Logger.Debug("Найдено {Count} объектов", data?.Count);

			Logger.Debug("Начато формирование таблиц");
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(
					unitDto.Name,
					unitDto.GroupName,
					(unitDto.UpksAverageWeight.HasValue ? Math.Round(unitDto.UpksAverageWeight.Value, PrecisionForDecimalValues) : (decimal?)null)
				);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);
			Logger.Debug("Закончено формирование таблиц");

			return dataSet;
		}
	}
}
