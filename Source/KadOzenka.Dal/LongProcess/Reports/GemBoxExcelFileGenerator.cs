using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.LongProcess.Reports.Entities;

namespace KadOzenka.Dal.LongProcess.Reports
{
	public class GemBoxExcelFileGenerator
	{
		private readonly ExcelFile _excelFile;
		private CellStyle GeneralCellStyle { get; }
		private int _currentRowIndex;
		private bool _hasTitle;


		public GemBoxExcelFileGenerator()
		{
			_excelFile = new ExcelFile();
			var sheet = _excelFile.Worksheets.Add("Лист 1");
			sheet.Cells.Style.Font.Name = "Times New Roman";

			GeneralCellStyle = new CellStyle
			{
				HorizontalAlignment = HorizontalAlignmentStyle.Center,
				VerticalAlignment = VerticalAlignmentStyle.Center,
				WrapText = true
			};
			GeneralCellStyle.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
		}



		public void AddTitle(string title, int maxColumnsCount)
		{
			if (string.IsNullOrWhiteSpace(title))
				return;

			_hasTitle = true;

			var mergedCell = new MergedColumns
			{
				OrderNumber = 0,
				Text = title,
				StartColumnIndex = 0,
				EndColumnIndex = maxColumnsCount - 1
			};
			var titleCellStyle = new CellStyle
			{
				HorizontalAlignment = HorizontalAlignmentStyle.Left
			};

			AddMergedHeaders(new List<MergedColumns> {mergedCell}, titleCellStyle);
		}

		public void AddMergedHeaders(List<MergedColumns> commonHeaders, CellStyle style = null)
		{
			var sheet = _excelFile.Worksheets[0];
			
			commonHeaders.OrderBy(x => x.OrderNumber).GroupBy(x => x.OrderNumber).ToList().ForEach(groupedColumns =>
			{
				groupedColumns.ToList().ForEach(columns =>
				{
					var cells = sheet.Cells.GetSubrangeAbsolute(_currentRowIndex, columns.StartColumnIndex, _currentRowIndex, columns.EndColumnIndex);
					cells.Merged = true;
					cells.Value = columns.Text;

					cells.Style = style ?? GeneralCellStyle;
				});

				_currentRowIndex++;
			});
		}

		public void AddSeparateColumnsHeaders(List<string> values)
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

		public void AddSeparateColumnsHeaders(List<GbuObject.GbuReportService.Column> columns)
		{
			var headers = columns.Select(x => x.Header).ToList();
			AddSeparateColumnsHeaders(headers);
		}

		public void AddRow(List<object> values)
		{
			DataExportCommon.AddRow(_excelFile.Worksheets[0], _currentRowIndex, values.ToArray());
			_currentRowIndex++;
		}

		//todo убрать обратоку из цикла
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
							sheet.Rows[i].Cells[j].Style = GeneralCellStyle;
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