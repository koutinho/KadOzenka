using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.MinMaxAverageUpksAndUprsByGroups.Dto;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.MinMaxAverageByGroups
{
	public abstract class BaseMinMaxAverageByGroupsReport : StatisticalDataReport
	{
        protected readonly UpksService UpksService;
		protected readonly UprsService UprsService;
		protected readonly UpksAndUprsService UpksAndUprsService;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public BaseMinMaxAverageByGroupsReport()
		{
            UpksService = new UpksService(StatisticalDataService);
            UprsService = new UprsService(StatisticalDataService);
            UpksAndUprsService = new UpksAndUprsService(UpksService, UprsService);
            _logger = Log.ForContext<BaseMinMaxAverageByGroupsReport>();
        }


        protected abstract string GetReportTitle();
        protected abstract string GetDataNameColumnText();
        protected abstract DataTable GetDataByGroups(long[] taskIdList, bool isOks);
        protected abstract DataTable GetDataByGroupsAndSubgroups(long[] taskIdList, bool isOks);


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


        protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

            var dataSet = new DataSet();

            using (var dataTitleTable = new DataTable("Common"))
            {
                dataTitleTable.Columns.Add("Title");
                dataTitleTable.Columns.Add("DataNameColumnText");
                dataTitleTable.Rows.Add(GetReportTitle(), GetDataNameColumnText());

                DataTable dataTable;
                var reportType = GetQueryParam<string>("ReportType", query);
                var zuOksObjectType = GetQueryParam<string>("ZuOksObjectType", query);
                switch (reportType)
                {
                    case "По группам":
	                    Logger.Debug("По группам");
                        dataTable = GetDataByGroups(taskIdList, zuOksObjectType == "ОКС");
                        break;
                    case "По группам и подгруппам":
	                    Logger.Debug("По группам и подгруппам");
                        dataTable = GetDataByGroupsAndSubgroups(taskIdList, zuOksObjectType == "ОКС");
                        break;
                    default:
                        throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
                }

                dataSet.Tables.Add(dataTable);
                dataSet.Tables.Add(dataTitleTable);
            }

            return dataSet;
		}

        protected string PreprocessGroupName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? "Без группы" : name;
        }

        protected decimal? GetCalcValue(UpksCalcType upksCalcType, CalculationInfoDto unitDto)
        {
            decimal? value = null;
            switch (upksCalcType)
            {
                case UpksCalcType.Min:
                    value = unitDto.Min;
                    break;
                case UpksCalcType.Average:
                    value = unitDto.Avg;
                    break;
                case UpksCalcType.AverageWeight:
                    value = unitDto.AvgWeight;
                    break;
                case UpksCalcType.Max:
                    value = unitDto.Max;
                    break;
            }

            return value.HasValue
                ? Math.Round(value.Value, PrecisionForDecimalValues)
                : (decimal?)null;
        }
    }
}
