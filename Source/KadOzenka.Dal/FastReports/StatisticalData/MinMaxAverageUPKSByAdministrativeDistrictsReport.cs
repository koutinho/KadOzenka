using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public class MinMaxAverageUPKSByAdministrativeDistrictsReport : StatisticalDataReport
	{
		private readonly MinMaxAverageUPKSByAdministrativeDistrictsService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public MinMaxAverageUPKSByAdministrativeDistrictsReport()
		{
			_service = new MinMaxAverageUPKSByAdministrativeDistrictsService(new GbuCodRegisterService());
			_logger = Log.ForContext<MinMaxAverageUPKSByAdministrativeDistrictsReport>();
		}


		protected override string TemplateName(NameValueCollection query)
		{
			var reportType = GetReportType(GetQueryParam<string>("ReportType", query));
			switch (reportType)
			{
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts:
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions: return "MinMaxAverageUPKSByAdministrativeDistrictsReport";
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions: return "MinMaxAverageUPKSByAdministrativeDistrictsByRegionsReport";
				default: throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}
		}

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			_service.QueryManager.SetBaseToken(CancellationToken);
			var taskIdList = GetTaskIdList(query);
			var reportType = GetReportType(GetQueryParam<string>("ReportType", query));
			Logger.Debug("Тип отчета {ReportType}", reportType);

			var dataSet = new DataSet();
			using (DataTable dataTitleTable = new DataTable("Common"))
            {
				dataTitleTable.Columns.Add("Title");
				dataTitleTable.Columns.Add("DataNameColumnText");
				var title = GetCommonTitle(reportType);
				var dataTypeHeader = GetCommonDataNameColumnText(reportType);
				dataTitleTable.Rows.Add(title, dataTypeHeader);

				using (DataTable dataTable = new DataTable("Data"))
                {
					dataTable.Columns.Add("AdditionalName", typeof(string));
					dataTable.Columns.Add("Name", typeof(string));
					dataTable.Columns.Add("ObjectsCount", typeof(long));
					dataTable.Columns.Add("UpksCalcType", typeof(string));
					dataTable.Columns.Add("PropertyType", typeof(string));
					dataTable.Columns.Add("UpksCalcValue", typeof(decimal));

					var data = _service.GetMinMaxAverageUPKSByAdministrativeDistricts(taskIdList, reportType);
					Logger.Debug("Найдено {Count} объектов", data?.Count);

					Logger.Debug("Начато формирование таблиц");
					foreach (var unitDto in data)
					{
						dataTable.Rows.Add(unitDto.AdditionalName, unitDto.Name, unitDto.ObjectsCount, unitDto.UpksCalcType.GetEnumDescription(), unitDto.PropertyType,
							(unitDto.UpksCalcValue.HasValue ? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues) : (decimal?)null)
						);
					}

					dataSet.Tables.Add(dataTable);
					dataSet.Tables.Add(dataTitleTable);
					Logger.Debug("Закончено формирование таблиц");
				}
			}

			GC.Collect();

			return dataSet;
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
				default: throw new InvalidDataException($"Неизвестный тип отчета: {reportType}");
			}
		}

		private string GetCommonDataNameColumnText(MinMaxAverageUPKSByAdministrativeDistrictsType reportType)
		{
			switch (reportType)
			{
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts: return "Наименование административного округа";
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions: return "Номер кадастрового района";
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions: return "Наименование муниципального района / поселения";
				default: throw new InvalidDataException($"Неизвестный тип отчета: {reportType}");
			}
		}

		public MinMaxAverageUPKSByAdministrativeDistrictsType GetReportType(string reportTypeParam)
		{
			switch (reportTypeParam)
			{
				case "По административным округам": return MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts;
				case "По кадастровым районам": return MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions;
				case "По муниципальным районам": return MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions;
				default: throw new InvalidDataException($"Неизвестный тип формирования данных: {reportTypeParam}");
			}
		}
	}
}
