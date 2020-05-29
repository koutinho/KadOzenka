using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
    public class NumberOfObjectsByAdministrativeDistrictsReport : StatisticalDataReport
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

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
	        var taskIdList = GetTaskIdList(query);

            DataSet dataSet;
            var reportType = GetQueryParam<string>("ReportType", query);
            switch (reportType)
            {
                case "Количество объектов недвижимости (по административным округам), в разрезе групп и видов объектов недвижимости":
                    dataSet = GetDataByGroupsAndTypesReport(taskIdList);
                    break;
                case "Количество объектов по субъекту в разрезе групп и видов объектов недвижимости":
                    dataSet = GetDataBySubjectReport(taskIdList);
                    break;
                case "Количество объектов по группам":
                    dataSet = GetDataByGroupsReport(taskIdList);
                    break;
                default:
                    throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
            }

            return HadleData(dataSet);
        }

        private DataSet GetDataByGroupsAndTypesReport(long[] taskList)
        {
	        var dataTitleTable = new DataTable("Common");
	        dataTitleTable.Columns.Add("Title");
	        dataTitleTable.Rows.Add("Количество объектов недвижимости (по административным округам), в разрезе групп и видов объектов недвижимости на территории города Москвы");

	        var dataTable = new DataTable("Data");
	        dataTable.Columns.Add("Distrinct", typeof(string));
	        dataTable.Columns.Add("RegionNumber", typeof(string));
	        dataTable.Columns.Add("PropertyType", typeof(string));
	        dataTable.Columns.Add("Group", typeof(string));
	        dataTable.Columns.Add("ObjectsCount", typeof(long));

            var data = _statisticalDataService.GetNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes(taskList);

            foreach (var unitDto in data)
            {
	            dataTable.Rows.Add(unitDto.Distrinct, unitDto.RegionNumber,unitDto.PropertyType, unitDto.Group, unitDto.Count);
            }

            var dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            dataSet.Tables.Add(dataTitleTable);

            return dataSet;
        }

        private DataSet GetDataBySubjectReport(long[] taskList)
        {
	        var dataTitleTable = new DataTable("Common");
            dataTitleTable.Columns.Add("Title");
            dataTitleTable.Rows.Add("Количество объектов недвижимости (по субъекту), в разрезе групп и видов объектов недвижимости на территории города Москвы");

            var dataTable = new DataTable("Data");
            dataTable.Columns.Add("PropertyType", typeof(string));
            dataTable.Columns.Add("Group", typeof(string));
            dataTable.Columns.Add("ObjectsCount", typeof(long));

            var data = _statisticalDataService.GetNumberOfObjectsByAdministrativeDistrictsBySubject(taskList);

            foreach (var unitDto in data)
            {
                dataTable.Rows.Add(unitDto.PropertyType, unitDto.Group, unitDto.Count);
            }

            var dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            dataSet.Tables.Add(dataTitleTable);

            return dataSet;
        }



        private DataSet GetDataByGroupsReport(long[] taskList)
        {
            var data = _statisticalDataService.GetNumberOfObjectsByAdministrativeDistrictsByGroups(taskList);

            throw new NotImplementedException();
        }
    }
}
