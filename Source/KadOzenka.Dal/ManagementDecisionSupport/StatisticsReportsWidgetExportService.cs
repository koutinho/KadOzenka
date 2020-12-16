using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalReportsExport;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class StatisticsReportsWidgetExportService
	{
		private readonly StatisticsReportsWidgetService _statisticsReportsWidgetService;

		private List<string> _columnNameBaseList = new List<string>
		{
			"№", "Кадастровый номер", "Вид объекта недвижимости", "Площадь", "Дата создания задания на оценку"
		};

		public StatisticsReportsWidgetExportService(StatisticsReportsWidgetService statisticsReportsWidgetService)
		{
			_statisticsReportsWidgetService = statisticsReportsWidgetService;
		}


		public StatisticsReportsExportResult ExportImportedObjects(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd, bool useExportSavingToStorage = false)
		{
			var exportRequest = new ExportObjectsRequest<UnitObjectDto>
			{
				ExportName = StatisticsReportExportType.ImportedObjects.GetEnumDescription(),
				ColumnNameList = _columnNameBaseList,
				Request = request,
				DateStart = dateStart,
				DateEnd = dateEnd,
				UseExportSavingToStorage = useExportSavingToStorage,
				DataFunc = _statisticsReportsWidgetService.GetImportedObjectsData,
				DataCountFunc = _statisticsReportsWidgetService.GetImportedObjectsDataCount
			};

			return ExportObjects(exportRequest);
		}

		public StatisticsReportsExportResult ExportExportedObjects(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd, bool useExportSavingToStorage = false)
		{
			var columnNameList = new List<string>();
			columnNameList.AddRange(_columnNameBaseList);
			columnNameList.Add("Статус");
			var exportRequest = new ExportObjectsRequest<ExportedObjectDto>
			{
				ExportName = StatisticsReportExportType.ExportedObjects.GetEnumDescription(),
				ColumnNameList = columnNameList,
				Request = request,
				DateStart = dateStart,
				DateEnd = dateEnd,
				UseExportSavingToStorage = useExportSavingToStorage,
				DataFunc = _statisticsReportsWidgetService.GetExportedObjectsData,
				DataCountFunc = _statisticsReportsWidgetService.GetExportedObjectsDataCount
			};

			return ExportObjects(exportRequest);
		}

		public StatisticsReportsExportResult ExportZoneStatistics(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd, bool useExportSavingToStorage = false)
		{
			var columnNameList = new List<string>();
			columnNameList.AddRange(_columnNameBaseList);
			columnNameList.Add("Зона");
			var exportRequest = new ExportObjectsRequest<ZoneStatisticDto>
			{
				ExportName = StatisticsReportExportType.ZoneStatistics.GetEnumDescription(),
				ColumnNameList = columnNameList,
				Request = request,
				DateStart = dateStart,
				DateEnd = dateEnd,
				UseExportSavingToStorage = useExportSavingToStorage,
				DataFunc = _statisticsReportsWidgetService.GetZoneStatisticsData,
				DataCountFunc = _statisticsReportsWidgetService.GetZoneStatisticsDataCount
			};

			return ExportObjects(exportRequest);
		}

		public StatisticsReportsExportResult ExportFactorStatistics(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd, bool useExportSavingToStorage = false)
		{
			var columnNameList = new List<string>();
			columnNameList.AddRange(_columnNameBaseList);
			columnNameList.Add("Измененные факторы");
			var exportRequest = new ExportObjectsRequest<FactorStatisticDto>
			{
				ExportName = StatisticsReportExportType.FactorStatistics.GetEnumDescription(),
				ColumnNameList = columnNameList,
				Request = request,
				DateStart = dateStart,
				DateEnd = dateEnd,
				UseExportSavingToStorage = useExportSavingToStorage,
				DataFunc = _statisticsReportsWidgetService.GetFactorStatisticsData,
				DataCountFunc = _statisticsReportsWidgetService.GetFactorStatisticsDataCount
			};

			return ExportObjects(exportRequest);
		}

		public StatisticsReportsExportResult ExportGroupStatistics(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd, bool useExportSavingToStorage = false)
		{
			var columnNameList = new List<string>();
			columnNameList.AddRange(_columnNameBaseList);
			columnNameList.Add("Группа");
			columnNameList.Add("Подгруппа");
			var exportRequest = new ExportObjectsRequest<GroupStatisticDto>
			{
				ExportName = StatisticsReportExportType.GroupStatistics.GetEnumDescription(),
				ColumnNameList = columnNameList,
				Request = request,
				DateStart = dateStart,
				DateEnd = dateEnd,
				UseExportSavingToStorage = useExportSavingToStorage,
				DataFunc = _statisticsReportsWidgetService.GetGroupStatisticsData,
				DataCountFunc = _statisticsReportsWidgetService.GetGroupStatisticsDataCount
			};

			return ExportObjects(exportRequest);
		}

		private StatisticsReportsExportResult ExportObjects<T>(ExportObjectsRequest<T> exportRequest) where T : UnitObjectDto
		{
			var result = new StatisticsReportsExportResult { UseExportSavingToStorage = true };

			using var gbuReportService = new GbuReportService($"{exportRequest.ExportName}(Период с {exportRequest.DateStart?.ToString("dd.MM.yyyy")} по {exportRequest.DateEnd?.ToString("dd.MM.yyyy")})");
			gbuReportService.AddHeaders(exportRequest.ColumnNameList.ToList());

			var number = 1;
			var totalDataCount = exportRequest.DataCountFunc(exportRequest.Request, exportRequest.DateStart, exportRequest.DateEnd);
			var pageCount = totalDataCount / GbuReportService.MaxRowsCountInSheet;
			for (var i = 1; i <= pageCount + 1; i++)
			{
				exportRequest.Request.Page = i;
				exportRequest.Request.PageSize = GbuReportService.MaxRowsCountInSheet;
				var data = exportRequest.DataFunc(exportRequest.Request, exportRequest.DateStart, exportRequest.DateEnd, false).Data;
				foreach (var dataRecord in data)
				{
					var values = dataRecord.ToRowExportObjects();
					values.Insert(0, number);
					gbuReportService.AddRow(gbuReportService.GetCurrentRow(), values);
					number++;
				}
			}

			if (exportRequest.UseExportSavingToStorage)
			{
				result.ReportId = gbuReportService.SaveReport();
			}
			else
			{
				result.ReportFile = gbuReportService.GetReportFile();
			}

			return result;
		}

		private class ExportObjectsRequest<T> where T : UnitObjectDto
		{
			public string ExportName { get; set; }
			public List<string> ColumnNameList { get; set; }
			public DataSourceRequestDto Request { get; set; }
			public DateTime? DateStart { get; set; }
			public DateTime? DateEnd { get; set; }
			public bool UseExportSavingToStorage { get; set; }
			public Func<DataSourceRequestDto, DateTime?, DateTime?, bool, GridDataDto<T>> DataFunc { get; set; }
			public Func<DataSourceRequestDto, DateTime?, DateTime?, long> DataCountFunc { get; set; }
		}
	}
}
