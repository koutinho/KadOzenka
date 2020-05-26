using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using KadOzenka.Dal.ManagementDecisionSupport;
using Newtonsoft.Json;
using Platform.Reports;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
    public class NumberOfObjectsByAdministrativeDistrictsReport : FastReportBase
    {
	    private readonly StatisticalDataService _statisticalDataService;

		public NumberOfObjectsByAdministrativeDistrictsReport()
		{
			_statisticalDataService = new StatisticalDataService();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "Количество объектов недвижимости (по административным округам), в разрезе групп и видов объектов недвижимости":
					return "NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesReport";
				case "Количество объектов по субъекту в разрезе групп и видов объектов недвижимости":
					return "NumberOfObjectsByAdministrativeDistrictsBySubjectReport";
				case "Количество объектов по группам":
					return "NumberOfObjectsByAdministrativeDistrictsByGroupsReport";
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}

		}

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
			var taskIdList = GetQueryParam<string>("TaskIdList", query);
			var taskIdListValue = JsonConvert.DeserializeObject<long[]>(taskIdList);

			DataSet dataSet;
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "В разрезе видов объекта недвижимости":
					dataSet = GetDataByGroupsAndTypesReport(taskIdListValue);
					break;
				case "В разрезе видов использования":
					dataSet = GetDataBySubjectReport(taskIdListValue);
					break;
				default:
					dataSet = GetDataByGroupsReport(taskIdListValue);
					break;
			}

			return dataSet;
        }

        private DataSet GetDataByGroupsAndTypesReport(long[] taskList)
        {
	        var data = _statisticalDataService.GetNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes(taskList);

			throw new NotImplementedException();
        }

        private DataSet GetDataBySubjectReport(long[] taskList)
        {
	        var data = _statisticalDataService.GetNumberOfObjectsByAdministrativeDistrictsBySubject(taskList);

			throw new NotImplementedException();
        }

        private DataSet GetDataByGroupsReport(long[] taskList)
        {
	        var data = _statisticalDataService.GetNumberOfObjectsByAdministrativeDistrictsByGroups(taskList);

			throw new NotImplementedException();
        }
    }
}
