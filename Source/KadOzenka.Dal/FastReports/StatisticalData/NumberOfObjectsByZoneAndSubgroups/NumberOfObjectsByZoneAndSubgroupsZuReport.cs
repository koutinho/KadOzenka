using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.NumberOfObjectsByZoneAndSubgroups
{
	public class NumberOfObjectsByZoneAndSubgroupsZuReport : StatisticalDataReport
	{
		private readonly NumberOfObjectsByZoneAndSubgroupsService _service;

		public NumberOfObjectsByZoneAndSubgroupsZuReport()
		{
			_service = new NumberOfObjectsByZoneAndSubgroupsService(StatisticalDataService, GbuObjectService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return "NumberOfObjectsByZoneAndSubgroupsZuOksReport";
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var firstTourId = GetQueryParam<long?>("TourId", query);
			if (!firstTourId.HasValue)
			{
				throw new Exception("Истекло время ожидания сессии. Обновите страницу.");
			}
			var secondTourId = GetQueryParam<long?>("SecondTourId", query);
			if (!secondTourId.HasValue)
			{
				throw new Exception("Истекло время ожидания сессии. Обновите страницу.");
			}

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Статистика по зонам ЗУ");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("Zone");
			dataTable.Columns.Add("ZoneNameByCircles");
			dataTable.Columns.Add("DistrictName");
			dataTable.Columns.Add("RegionName");
			dataTable.Columns.Add("ZoneDistrict");
			dataTable.Columns.Add("ZoneDistrictRegion");
			dataTable.Columns.Add("PropertyType");
			dataTable.Columns.Add("FirstTourObjectCount");
			dataTable.Columns.Add("SecondTourObjectCount");
			dataTable.Columns.Add("FirstTourObjectCountWithoutGroupChanging");
			dataTable.Columns.Add("SecondTourObjectCountWithoutGroupChanging");
			dataTable.Columns.Add("FirstTourObjectCountWithGroupChanging");
			dataTable.Columns.Add("SecondTourObjectCountWithGroupChanging");
			dataTable.Columns.Add("FirstTourMinUpks");
			dataTable.Columns.Add("FirstTourMaxUpks");
			dataTable.Columns.Add("FirstTourAverageUpks");
			dataTable.Columns.Add("SecondTourMinUpks");
			dataTable.Columns.Add("SecondTourMaxUpks");
			dataTable.Columns.Add("SecondTourAverageUpks");
			dataTable.Columns.Add("MinUpksVarianceBetweenTours");
			dataTable.Columns.Add("MaxUpksVarianceBetweenTours");
			dataTable.Columns.Add("AverageUpksVarianceBetweenTours");
			dataTable.Columns.Add("MinUpksVarianceBetweenToursWithoutGroupChanging");
			dataTable.Columns.Add("MaxUpksVarianceBetweenToursWithoutGroupChanging");
			dataTable.Columns.Add("AverageUpksVarianceBetweenToursWithoutGroupChanging");


			var data = _service.GetNumberOfObjectsByZoneAndSubgroupsData(firstTourId.Value, 
				secondTourId.Value, GetReportDataType(query), false);


			foreach (var dto in data)
			{
				dataTable.Rows.Add(dto.Zone,
					dto.ZoneNameByCircles,
					dto.DistrictName,
					dto.RegionName,
					dto.ZoneDistrict,
					dto.ZoneDistrictRegion,
					dto.PropertyType,
					dto.FirstTourObjectCount,
					dto.SecondTourObjectCount,
					dto.FirstTourObjectCountWithoutGroupChanging,
					dto.SecondTourObjectCountWithoutGroupChanging,
					dto.FirstTourObjectCountWithGroupChanging,
					dto.SecondTourObjectCountWithGroupChanging,
					dto.FirstTourMinUpks.HasValue
						? Math.Round(dto.FirstTourMinUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.FirstTourMaxUpks.HasValue
						? Math.Round(dto.FirstTourMaxUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.FirstTourAverageUpks.HasValue
						? Math.Round(dto.FirstTourAverageUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.SecondTourMinUpks.HasValue
						? Math.Round(dto.SecondTourMinUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.SecondTourMaxUpks.HasValue
						? Math.Round(dto.SecondTourMaxUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.SecondTourAverageUpks.HasValue
						? Math.Round(dto.SecondTourAverageUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.MinUpksVarianceBetweenTours.HasValue
						? Math.Round(dto.MinUpksVarianceBetweenTours.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.MaxUpksVarianceBetweenTours.HasValue
						? Math.Round(dto.MaxUpksVarianceBetweenTours.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.AverageUpksVarianceBetweenTours.HasValue
						? Math.Round(dto.AverageUpksVarianceBetweenTours.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.MinUpksVarianceBetweenToursWithoutGroupChanging.HasValue
						? Math.Round(dto.MinUpksVarianceBetweenToursWithoutGroupChanging.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.MaxUpksVarianceBetweenToursWithoutGroupChanging.HasValue
						? Math.Round(dto.MaxUpksVarianceBetweenToursWithoutGroupChanging.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.AverageUpksVarianceBetweenToursWithoutGroupChanging.HasValue
						? Math.Round(dto.AverageUpksVarianceBetweenToursWithoutGroupChanging.Value, PrecisionForDecimalValues)
						: (decimal?)null
					);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}

		private NumberOfObjectsByZoneAndSubgroupsReportDataType GetReportDataType(NameValueCollection query)
		{
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "По итогам сравнения туров оценки":
					return NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial;
				case "По итогам сравнения с учетом данных по ВУОНам":
					return NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon;
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}
		}
	}

}
