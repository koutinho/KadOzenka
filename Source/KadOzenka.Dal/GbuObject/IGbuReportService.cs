using System.Collections.Generic;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Common;

namespace KadOzenka.Dal.GbuObject
{
	public interface IGbuReportService
	{
		string DefaultExtension { get; }
		CellStyle WarningCellStyle { get; }
		CellStyle ErrorCellStyle { get; }
		string FileStorageKey { get; }

		/// <summary>
		/// Отдает текущий номер строки и инкрементирует значение для следующего обращения 
		/// </summary>
		/// <returns></returns>
		GbuReportService.Row GetCurrentRow();

		/// <summary>
		/// Отдает запрошенное количество строк для заполнения и увеличивает значения для след запроса
		/// </summary>
		/// <param name="rangeRows"></param>
		/// <returns></returns>
		List<GbuReportService.Row> GetRangeRows(int rangeRows);

		void AddHeaders(List<string> values);
		void SetIndividualWidth(int column, int width, bool saveWidth = true);
		void SetIndividualWidth(List<GbuReportService.Column> columns);
		void AddHeaders(List<GbuReportService.Column> columns);
		void AddValue(string value, int column, GbuReportService.Row row, CellStyle cellStyle = null);
		void AddRow(GbuReportService.Row row, List<string> values);
		void AddRow(GbuReportService.Row row, List<object> values);
		long SaveReport();
		GbuReportService.ReportFile GetReportFile();
		void Dispose();

		string GetUrlToDownloadFile(long reportId);
		ReportInfo GetFile(long reportId);
		OMGbuOperationsReports GetFileInfo(long reportId);
	}
}
