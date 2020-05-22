using System;
using System.Collections.Generic;
using System.IO;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports;
using Kendo.Mvc.UI;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class StatisticsReportsExportService
	{
		private readonly StatisticsReportsService _statisticsReportsService;

		private List<object> _columnNameBaseList = new List<object>
		{
			"№", "Кадастровый номер", "Вид объекта недвижимости", "Площадь", "Дата создания задания на оценку"
		};

		public StatisticsReportsExportService(StatisticsReportsService statisticsReportsService)
		{
			_statisticsReportsService = statisticsReportsService;
		}

		public Stream ExportImportedObjects(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = _statisticsReportsService.GetImportedObjectsData(request, dateStart, dateEnd);
			return ExportImportedObjects(data.Data, _columnNameBaseList.ToArray());
		}

		public Stream ExportExportedObjects(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = _statisticsReportsService.GetExportedObjectsData(request, dateStart, dateEnd);
			var columnNameList = new List<object>();
			columnNameList.AddRange(_columnNameBaseList);
			columnNameList.Add("Статус");

			return ExportImportedObjects(data.Data, columnNameList.ToArray());
		}

		public Stream ExportZoneStatistics(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = _statisticsReportsService.GetZoneStatisticsData(request, dateStart, dateEnd);
			var columnNameList = new List<object>();
			columnNameList.AddRange(_columnNameBaseList);
			columnNameList.Add("Зона");

			return ExportImportedObjects(data.Data, columnNameList.ToArray());
		}

		

		public Stream ExportFactorStatistics(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = _statisticsReportsService.GetFactorStatisticsData(request, dateStart, dateEnd);
			var columnNameList = new List<object>();
			columnNameList.AddRange(_columnNameBaseList);
			columnNameList.Add("Измененные факторы");

			return ExportImportedObjects(data.Data, columnNameList.ToArray());
		}

		public Stream ExportGroupStatistics(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = _statisticsReportsService.GetGroupStatisticsData(request, dateStart, dateEnd);
			var columnNameList = new List<object>();
			columnNameList.AddRange(_columnNameBaseList);
			columnNameList.Add("Группа");
			columnNameList.Add("Подгруппа");

			return ExportImportedObjects(data.Data, columnNameList.ToArray());
		}

		private Stream ExportImportedObjects<T>(List<T> data, object[] columnNameList) where T : UnitObjectDto
		{
			var maxExcelWorksheetRowCount = 1000001;
			ExcelFile excelTemplate = new ExcelFile();
			var workSheet = excelTemplate.Worksheets.Add(data.Count > (maxExcelWorksheetRowCount - 1) ? "Экспорт данных (стр. 1)" : "Экспорт данных");
			DataExportCommon.AddRow(workSheet, 0, columnNameList);

			var excelRow = 1;
			var page = 1;
			for(var i = 0; i < data.Count; i++)
			{
				var rowValue = new List<object> {i + 1};
				rowValue.AddRange(data[i].ToRowExportObjects());
				if (excelRow == maxExcelWorksheetRowCount)
				{
					page++;
					workSheet = excelTemplate.Worksheets.Add($"Экспорт данных (стр. {page})");
					DataExportCommon.AddRow(workSheet, 0, columnNameList);
					excelRow = 1;
					DataExportCommon.AddRow(workSheet, excelRow, rowValue.ToArray());
				}
				else
				{
					DataExportCommon.AddRow(workSheet, excelRow, rowValue.ToArray());
				}
				
				excelRow++;
			}

			MemoryStream stream = new MemoryStream();
			excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
		}
	}
}
