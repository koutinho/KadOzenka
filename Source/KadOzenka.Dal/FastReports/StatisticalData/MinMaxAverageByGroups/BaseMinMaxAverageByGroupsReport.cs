using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.MinMaxAverageByGroups
{
	public abstract class BaseMinMaxAverageByGroupsReport : StatisticalDataReport
	{
		protected readonly MinMaxAverageByGroupsService _service;

		public BaseMinMaxAverageByGroupsReport()
		{
			_service = new MinMaxAverageByGroupsService(StatisticalDataService, GbuObjectService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			var reportType = GetQueryParam<string>("ReportType", query);
			var zuOksObjectType = GetQueryParam<string>("ZuOksObjectType", query);

			switch (reportType)
			{
				case "По группам":
					return zuOksObjectType == "ОКС"
						? "MinMaxAverageByGroupsOksReport"
						: "MinMaxAverageByGroupsZuReport";
				case "По группам и подгруппам":
					return zuOksObjectType == "ОКС"
						? "MinMaxAverageByGroupsAndSubgroupsOksReport"
						: "MinMaxAverageByGroupsAndSubgroupsZuReport";
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}
		}

		protected abstract string GetReportTitle();
		protected abstract string GetDataNameColumnText();

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Columns.Add("DataNameColumnText");
			dataTitleTable.Rows.Add(GetReportTitle(), GetDataNameColumnText());

			DataTable dataTable;
			var reportType = GetQueryParam<string>("ReportType", query);
			var zuOksObjectType = GetQueryParam<string>("ZuOksObjectType", query);
			switch (reportType)
			{
				case "По группам":
					dataTable = GetDataByGroups(taskIdList, zuOksObjectType == "ОКС");
					break;
				case "По группам и подгруппам":
					dataTable = GetDataByGroupsAndSubgroups(taskIdList, zuOksObjectType == "ОКС");
					break;
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return HadleData(dataSet);
		}

		protected abstract DataTable GetDataByGroups(long[] taskIdList, bool isOks);

		protected abstract DataTable GetDataByGroupsAndSubgroups(long[] taskIdList, bool isOks);

	}
}
