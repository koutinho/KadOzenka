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


		public GemBoxExcelFileGenerator()
		{
			_excelFile = new ExcelFile();
			var sheet = _excelFile.Worksheets.Add("Лист 1");
			sheet.Cells.Style.Font.Name = "Times New Roman";
		}



		public void AddHeaders(List<string> values)
		{
			var columnIndex = 0;
			var sheet = _excelFile.Worksheets[0];
			foreach (var value in values)
			{
				sheet.Rows[_currentRowIndex].Cells[columnIndex].SetValue(value);
				sheet.Rows[_currentRowIndex].Cells[columnIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
				sheet.Rows[_currentRowIndex].Cells[columnIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
				sheet.Rows[_currentRowIndex].Cells[columnIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
				sheet.Rows[_currentRowIndex].Cells[columnIndex].Style.WrapText = true;
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

			for (var i = 0; i < countRows; i++)
			{
				for (var j = 0; j < countColumns; j++)
				{
					if (sheet.Rows[i] != null && sheet.Rows[i].Cells[j] != null)
					{
						try
						{
							sheet.Rows[i].Cells[j].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
							sheet.Rows[i].Cells[j].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
							sheet.Rows[i].Cells[j].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
							sheet.Rows[i].Cells[j].Style.WrapText = true;
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
	}
}