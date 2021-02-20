using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;

namespace KadOzenka.Dal.LongProcess.Reports
{
	public class GemBoxExcelFileGenerator
	{
		private readonly ExcelFile _excelFile;
		private int _currentRowIndex;
		private bool _hasTitle;


		public GemBoxExcelFileGenerator()
		{
			_excelFile = new ExcelFile();
			var sheet = _excelFile.Worksheets.Add("Лист 1");
			sheet.Cells.Style.Font.Name = "Times New Roman";
		}


		public void AddTitle(string title, int maxColumnsCount)
		{
			if (string.IsNullOrWhiteSpace(title))
				return;

			_hasTitle = true;
			var sheet = _excelFile.Worksheets[0];
			sheet.Rows[_currentRowIndex].Cells[0].SetValue(title);

			var maxColumnIndex = maxColumnsCount - 1;
			var cells = sheet.Cells.GetSubrangeAbsolute(_currentRowIndex, 0, _currentRowIndex, maxColumnIndex);
			cells.Merged = true;

			SetGeneralCellStyle(sheet, _currentRowIndex, maxColumnIndex);
			cells.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;

			_currentRowIndex++;
		}

		public void AddHeaders(List<string> values)
		{
			var columnIndex = 0;
			var row = _excelFile.Worksheets[0].Rows[_currentRowIndex];
			foreach (var value in values)
			{
				row.Cells[columnIndex].SetValue(value);
				columnIndex++;
			}

			_currentRowIndex++;
		}

		public void SetIndividualWidth(int column, int width)
		{
			_excelFile.Worksheets[0].Columns[column].SetWidth(width, LengthUnit.Centimeter);
		}

		public void SetIndividualWidth(List<GbuObject.GbuReportService.Column> columns)
		{
			columns.ForEach(x => { SetIndividualWidth(x.Index, x.Width); });
		}

		public void AddHeaders(List<GbuObject.GbuReportService.Column> columns)
		{
			var headers = columns.Select(x => x.Header).ToList();
			AddHeaders(headers);
		}

		public void AddRow(List<object> values)
		{
			DataExportCommon.AddRow(_excelFile.Worksheets[0], _currentRowIndex, values.ToArray());
			_currentRowIndex++;
		}


		public void SetStyle()
		{
			var sheet = _excelFile.Worksheets[0];

			var countRows = sheet.Rows.Count;
			var countColumns = sheet.CalculateMaxUsedColumns();
			var errCount = 0;

			//чтобы не сбросить стиль заголовкка
			var startRowIndex = _hasTitle ? 1 : 0;
			for (var i = startRowIndex; i < countRows; i++)
			{
				for (var j = 0; j < countColumns; j++)
				{
					if (sheet.Rows[i] != null && sheet.Rows[i].Cells[j] != null)
					{
						try
						{
							SetGeneralCellStyle(sheet, i, j);
						}
						catch (Exception ex)
						{
							if (errCount < 5)
								Serilog.Log.ForContext<ExcelFile>().Warning(ex, "Ошибка применения стилей в Excel {mainWorkSheetRow} {mainWorkSheetCell}", i, j);
							errCount++;
						}
					}
				}
			}
		}

		public MemoryStream GetStream()
		{
			var stream = new MemoryStream();
			_excelFile.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);
			
			return stream;
		}


		#region Support Methods

		private void SetGeneralCellStyle(ExcelWorksheet sheet, int rowIndex, int columnIndex)
		{
			var cellStyle = sheet.Rows[rowIndex].Cells[columnIndex].Style;

			cellStyle.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			cellStyle.VerticalAlignment = VerticalAlignmentStyle.Center;
			cellStyle.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			cellStyle.WrapText = true;
		}

		#endregion
	}
}