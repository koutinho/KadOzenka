using System;
using System.Collections.Generic;
using System.IO;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.GbuObject
{
	public class GbuReportService
	{
		public string UrlToDownload => $"/DataExport/DownloadExportResult?exportId={ReportId}";
		private readonly Serilog.ILogger _log = Serilog.Log.ForContext<GbuReportService>();
		private const int MaxRowsCountInSheet = 1000000;

		private ExcelFile _excelTemplate;
		private List<ExcelWorksheet> workSheets;
		private Row CurrentRow { get; set; }
		private long ReportId { get; set; }
		private int _listCounter;
		private readonly List<Column> _columnsWidth;
		private List<string> _headers;


		public GbuReportService()
		{
			_listCounter = 1;
			_excelTemplate = new ExcelFile();
			_headers = new List<string>();
			workSheets = new List<ExcelWorksheet>();
			_columnsWidth = new List<Column>();

			CreateWorkSheet();
		}


		/// <summary>
		/// Отдает текущий номер строки и инкрементирует значение для следующего обращения 
		/// </summary>
		/// <returns></returns>
		public Row GetCurrentRow()
		{
			ValidateRowsCountInSheet();

			var tmpRow = CurrentRow.Copy();

			CurrentRow.Index++;

			return tmpRow;
		}

		/// <summary>
		/// Отдает запрошенное количество строк для заполнения и увеличивает значения для след запроса
		/// </summary>
		/// <param name="rangeRows"></param>
		/// <returns></returns>
		public List<Row> GetRangeRows(int rangeRows)
		{
			var res = new List<Row>();
			var tmpRangeRows = rangeRows;
			while (CurrentRow.Index + tmpRangeRows >= CurrentRow.Index)
			{
				res.Add(CurrentRow.Copy());
				CurrentRow.Index++;
				tmpRangeRows--;
			}

			return res;
		}

		public void AddHeaders(List<string> values)
		{
			var rowIndex = 0;
			int columnIndex = 0;
			foreach (string value in values)
			{
				CurrentRow.Sheet.Rows[rowIndex].Cells[columnIndex].SetValue(value);
				CurrentRow.Sheet.Rows[rowIndex].Cells[columnIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
				CurrentRow.Sheet.Rows[rowIndex].Cells[columnIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
				CurrentRow.Sheet.Rows[rowIndex].Cells[columnIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
				CurrentRow.Sheet.Rows[rowIndex].Cells[columnIndex].Style.WrapText = true;
				columnIndex++;
			}

			_headers = values;
			CurrentRow.Index++;
		}

		public void AddValue(string value, int column, Row row, CellStyle cellStyle = null)
		{
			try
			{
				var cell = row.Sheet.Rows[row.Index].Cells[column];

				cell.SetValue(value);

				if (cellStyle != null)
					cell.Style = cellStyle;

				if (new Random().Next(0, 10000) > 9950)
					Serilog.Log.ForContext<ExcelFile>().Verbose("Запись значения в Excel. Строка {Row}, столбец {Column}, значение {Value}", row, column, value);
			}
			catch (Exception ex)
			{
				if (new Random().Next(0, 100) > 80)
					Serilog.Log.ForContext<ExcelFile>().Warning(ex, "Ошибка записи значения в Excel. Строка {Row}, столбец {Column}, значение {Value}", row, column, value);
			}
		}

		public void AddRow(Row row, List<string> values)
		{
			for (var i = 0; i < values.Count; i++)
			{
				row.Sheet.Rows[row.Index].Cells[i].SetValue(values[i]);
			}
		}

		public void SetStyle()
		{
			for (var sheetCounter = 0; sheetCounter < workSheets.Count; sheetCounter++)
			{
				var sheet = workSheets[sheetCounter];

				int countRows = sheet.Rows.Count;
				int countColumns = sheet.CalculateMaxUsedColumns();
				int errCount = 0;
				int successCount = 0;
				_log.Debug("Установка стилей в Excel для листа {ListIndex}. В таблице {countRows} x {countColumns}", sheet.Index, countRows, countColumns);
				
				for (int i = 0; i < countRows; i++)
				{
					for (int j = 0; j < countColumns; j++)
					{
						if (sheet.Rows[i] != null && sheet.Rows[i].Cells[j] != null)
						{
							try
							{
								sheet.Rows[i].Cells[j].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
								sheet.Rows[i].Cells[j].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
								sheet.Rows[i].Cells[j].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
								sheet.Rows[i].Cells[j].Style.WrapText = true;

								if (successCount < 5)
									_log.Verbose("Применение стилей в Excel {mainWorkSheetRow} {mainWorkSheetCell}", i, j);
								successCount++;
							}
							catch (Exception ex)
							{
								if (errCount < 5)
									_log.Warning(ex, "Ошибка применения стилей в Excel {mainWorkSheetRow} {mainWorkSheetCell}", i, j);
								errCount++;
							}
						}
					}
				}

				_log.Debug("Применение стилей в Excel для страницы {SheetNumber} завершено. Успешно {successCount}, с ошибкой {errCount}", sheetCounter, successCount, errCount);
			}
		}

		public void SetIndividualWidth(int column, int width, bool saveWidth = true)
		{
			_log.Verbose("Установка ширины {width} для столбца {column}", width, column);

			workSheets.ForEach(x => x.Columns[column].SetWidth(width, LengthUnit.Centimeter));

			if (saveWidth)
			{
				_columnsWidth.Add(new Column { Index = column, Width = width });
			}
		}

		public long SaveReport(string fileName, long? mainRegisterId = null, string registerViewId = null)
		{
			try
			{
				MemoryStream stream = new MemoryStream();
				_excelTemplate.Save(stream, SaveOptions.XlsxDefault);
				stream.Seek(0, SeekOrigin.Begin);

				var currentDate = DateTime.Now;
				var export = new OMExportByTemplates
				{
					UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
					DateCreated = currentDate,
					DateStarted = currentDate,
					Status = (int)ImportStatus.Added,
					FileResultTitle = fileName,
					FileExtension = "xlsx",
					MainRegisterId = mainRegisterId.HasValue ? mainRegisterId.Value : OMMainObject.GetRegisterId(),
					RegisterViewId = !string.IsNullOrEmpty(registerViewId) ? registerViewId : "GbuObjects"
				};
				export.Save();

				export.DateFinished = DateTime.Now;
				export.ResultFileName = DataExporterCommon.GetStorageResultFileName(export.Id);
				export.Status = (long)ImportStatus.Completed;
				FileStorageManager.Save(stream, DataExporterCommon.FileStorageName, export.DateFinished.Value, export.ResultFileName);
				export.Save();

				_log.ForContext("ResultFileName", export.ResultFileName)
					.ForContext("FileId", export.Id)
					.Debug("Сохранение отчета {FileName}", fileName);

				ReportId = export.Id;
				return ReportId;
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Сохранение отчета завершилось исключением");
				ErrorManager.LogError(ex);
				throw;
			}
		}


		#region Support Methods

		private void CreateWorkSheet()
		{
			var newSheet = _excelTemplate.Worksheets.Add($"Лист {_listCounter}");
			newSheet.Cells.Style.Font.Name = "Times New Roman";
			workSheets.Add(newSheet);

			_listCounter++;

			CurrentRow = new Row
			{
				Sheet = newSheet
			};
		}

		private void ValidateRowsCountInSheet()
		{
			if (CurrentRow.Index > MaxRowsCountInSheet)
			{
				CreateWorkSheet();

				AddHeaders(_headers);

				_columnsWidth.ForEach(x => SetIndividualWidth(x.Index, x.Width, false));
			}
		}

		#endregion


		#region Entities

		public class Column
		{
			public int Index { get; set; }
			public string Header { get; set; }
			public int Width { get; set; }
		}

		public class Row
		{
			public int Index;
			public ExcelWorksheet Sheet;

			public Row Copy()
			{
				return new Row
				{
					Index = Index,
					Sheet = Sheet
				};
			}
		}

		#endregion
	}
}