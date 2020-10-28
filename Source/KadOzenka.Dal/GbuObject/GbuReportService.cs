﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		public string UrlToDownload => $"/DataExport/DownloadExportResult?exportId={_reportId}";
		
		private ExcelFile _excelTemplate;
		private ExcelWorksheet currentSheet;
		private List<ExcelWorksheet> workSheets;
		private List<string> _headers;
		private readonly Serilog.ILogger _log = Serilog.Log.ForContext<GbuReportService>();
		private int _currentRow { get; set; }
		private Row _currentRow2 { get; set; }
		private long _reportId { get; set; }
		private int _listCounter;
		private const int MaxRowsCount = 1;
		private List<Column> _columnsWidth;


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
		public int GetCurrentRow()
		{
			int tmp = _currentRow;
			_currentRow++;
			return tmp;
		}

		public Row GetCurrentRowNew()
		{
			ValidateRowsCountInSheet();

			var tmpRow = _currentRow2.Copy();

			_currentRow2.Index++;

			return tmpRow;
		}

		/// <summary>
		/// Отдает запрошенное количество строк для заполнения и увеличивает значения для след запроса
		/// </summary>
		/// <param name="rangeRows"></param>
		/// <returns></returns>
		public List<int> GetRangeRows(int rangeRows)
		{
			List<int> res = new List<int>();
			int tmpRangeRows = rangeRows;
			int counter = 0;
			while (_currentRow + tmpRangeRows >= _currentRow)
			{
				res.Add(_currentRow + counter);
				counter++;
				tmpRangeRows--;
			}

			_currentRow += rangeRows +1;// перводим указатель на следующую пустую строку
			return res;
		}

		public List<Row> GetRangeRowsNew(int rangeRows)
		{
			var res = new List<Row>();
			var tmpRangeRows = rangeRows;
			while (_currentRow2.Index + tmpRangeRows >= _currentRow2.Index)
			{
				res.Add(_currentRow2.Copy());
				_currentRow2.Index++;
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
				currentSheet.Rows[rowIndex].Cells[columnIndex].SetValue(value);
				currentSheet.Rows[rowIndex].Cells[columnIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
				currentSheet.Rows[rowIndex].Cells[columnIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
				currentSheet.Rows[rowIndex].Cells[columnIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
				currentSheet.Rows[rowIndex].Cells[columnIndex].Style.WrapText = true;
				columnIndex++;
			}

			_headers = values;
			_currentRow++;
		}

		public void AddHeadersNew(List<string> values)
		{
			var rowIndex = 0;
			int columnIndex = 0;
			foreach (string value in values)
			{
				_currentRow2.Sheet.Rows[rowIndex].Cells[columnIndex].SetValue(value);
				_currentRow2.Sheet.Rows[rowIndex].Cells[columnIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
				_currentRow2.Sheet.Rows[rowIndex].Cells[columnIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
				_currentRow2.Sheet.Rows[rowIndex].Cells[columnIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
				_currentRow2.Sheet.Rows[rowIndex].Cells[columnIndex].Style.WrapText = true;
				columnIndex++;
			}

			_headers = values;
			_currentRow2.Index++;
		}

		public void AddValue(string value, int column, int row, CellStyle cellStyle = null)
		{
			try
			{
				var cell = currentSheet.Rows[row].Cells[column];

				cell.SetValue(value);

				if (cellStyle != null)
					cell.Style = cellStyle;

				if (new Random().Next(0, 10000) > 9950)
					Serilog.Log.ForContext<ExcelFile>().Verbose("Запись значения в Excel. Строка {Row}, столбец {Column}, значение {Value}", row, column, value);
			}
			catch (Exception ex) {
				if (new Random().Next(0, 100) > 80)
					Serilog.Log.ForContext<ExcelFile>().Warning(ex, "Ошибка записи значения в Excel. Строка {Row}, столбец {Column}, значение {Value}", row, column, value);
            }
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

		public void AddRow(List<string> values)
        {
            for (var i = 0; i < values.Count; i++)
            {
	            currentSheet.Rows[_currentRow].Cells[i].SetValue(values[i]);
            }

            _currentRow++;
        }

		public void AddRowNew(Row row, List<string> values)
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
				_log.Debug("Установка стилей в Excel таблице {countRows} x {countColumns}", countRows, countColumns);
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

				_reportId = export.Id;
				return _reportId;
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
			currentSheet = _excelTemplate.Worksheets.Add($"Лист {_listCounter}");
			currentSheet.Cells.Style.Font.Name = "Times New Roman";
			workSheets.Add(currentSheet);

			_listCounter++;

			_currentRow2 = new Row
			{
				Sheet = currentSheet
			};
		}

		private void ValidateRowsCountInSheet()
		{
			if (_currentRow2.Index > MaxRowsCount)
			{
				CreateWorkSheet();

				AddHeadersNew(_headers);

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