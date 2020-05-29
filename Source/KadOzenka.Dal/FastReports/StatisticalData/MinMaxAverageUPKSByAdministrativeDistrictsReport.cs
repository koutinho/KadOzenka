using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public class MinMaxAverageUPKSByAdministrativeDistrictsReport : StatisticalDataReport
	{
		private readonly StatisticalDataService _statisticalDataService;

		public MinMaxAverageUPKSByAdministrativeDistrictsReport()
		{
			_statisticalDataService = new StatisticalDataService();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			var reportType = GetReportType(GetQueryParam<string>("ReportType", query));
			switch (reportType)
			{
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts:
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions:
					return "MinMaxAverageUPKSByAdministrativeDistrictsReport";
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions:
					return "MinMaxAverageUPKSByAdministrativeDistrictsByRegionsReport";
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);
			var reportType = GetReportType(GetQueryParam<string>("ReportType", query));

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Columns.Add("DataNameColumnText");
			var title = GetCommonTitle(reportType);
			var dataTypeHeader = GetCommonDataNameColumnText(reportType);
			dataTitleTable.Rows.Add(title, dataTypeHeader);

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("AdditionalName", typeof(string));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));
			dataTable.Columns.Add("UpksCalcType", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("UpksCalcValue", typeof(decimal));

			var data = _statisticalDataService.GetMinMaxAverageUPKSByAdministrativeDistricts(taskIdList, reportType);
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.AdditionalName, unitDto.Name, unitDto.ObjectsCount, unitDto.UpksCalcType.GetEnumDescription(), unitDto.PropertyType, unitDto.UpksCalcValue);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return HadleData(dataSet);
		}

		private string GetCommonTitle(MinMaxAverageUPKSByAdministrativeDistrictsType reportType)
		{
			switch (reportType)
			{
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts:
					return "Минимальные, максимальные, средние удельные показатели кадастровой стоимости объектов недвижимости по административным округам города Москвы";
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions:
					return "Минимальные, максимальные, средние удельные показатели кадастровой стоимости объектов недвижимости по кадастровым районам города Москвы";
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions:
					return "Минимальные, максимальные, средние удельные показатели кадастровой стоимости объектов недвижимости по муниципальным районам города Москвы";
				default:
					throw new InvalidDataException($"Неизвестный тип отчета: {reportType}");
			}
		}

		private string GetCommonDataNameColumnText(MinMaxAverageUPKSByAdministrativeDistrictsType reportType)
		{
			switch (reportType)
			{
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts:
					return "Наименование административного округа";
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions:
					return "Номер кадастрового района";
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions:
					return "Наименование муниципального района / поселения";
				default:
					throw new InvalidDataException($"Неизвестный тип отчета: {reportType}");
			}
		}

		public MinMaxAverageUPKSByAdministrativeDistrictsType GetReportType(string reportTypeParam)
		{
			switch (reportTypeParam)
			{
				case "По административным округам":
					return MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts;
				case "По кадастровым районам":
					return MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions;
				case "По муниципальным районам":
					return MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions;
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportTypeParam}");
			}
		}
	}
}
