using System;
using System.Linq;
using GemBox.Spreadsheet;

namespace CommonSdks
{
	public class ExcelFileHelper
	{
		/// <summary>
		/// Добавление в Excel строки данных в строку с индексом Row
		/// </summary>
		public static void AddRow(ExcelWorksheet sheet, int Row, object[] values, CellStyle cellStyle = null)
		{
			int Col = 0;
			foreach (object value in values)
			{
				if (value is Boolean) sheet.Rows[Row].Cells[Col].SetValue(Convert.ToBoolean(value) ? "Да" : "Нет");
				else if (value is string) sheet.Rows[Row].Cells[Col].SetValue(value as string);
				else if (value is int)
				{
					sheet.Rows[Row].Cells[Col].SetValue((int) value);
					sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0";
				}
				else if (value is decimal || value is double)
				{
					sheet.Rows[Row].Cells[Col].SetValue(Convert.ToDouble(value));
					sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0.00";
				}
				else if (value is DateTime)
				{
					sheet.Rows[Row].Cells[Col].SetValue(Convert.ToDateTime(value));
					sheet.Rows[Row].Cells[Col].Style.NumberFormat = "mm/dd/yyyy";
				}
				else if (value is int?)
				{
					if (value == null)
					{
						sheet.Rows[Row].Cells[Col].SetValue("-");
					}
					else
					{
						sheet.Rows[Row].Cells[Col].SetValue((int) value);
						sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0";
					}
				}
				else if (value is decimal?)
				{
					if (value == null)
					{
						sheet.Rows[Row].Cells[Col].SetValue("-");
					}
					else
					{
						sheet.Rows[Row].Cells[Col].SetValue(Convert.ToDouble(value));
						sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0.00";
					}
				}
				else if (value is DateTime?)
				{
					if (value == null)
					{
						sheet.Rows[Row].Cells[Col].SetValue("-");
					}
					else
					{
						sheet.Rows[Row].Cells[Col].SetValue(Convert.ToDateTime(value));
						sheet.Rows[Row].Cells[Col].Style.NumberFormat = "mm/dd/yyyy";
					}
				}
				else
				{
					sheet.Rows[Row].Cells[Col].SetValue(value?.ToString());
				}

				sheet.Rows[Row].Cells[Col].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All,
					SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

				if (cellStyle != null)
					sheet.Rows[Row].Cells[Col].Style = cellStyle;

				Col++;
			}
		}

		public static void SetIndividualWidth(ExcelWorksheet sheet, int column, int width)
		{
			sheet.Columns[column].SetWidth(width, LengthUnit.Centimeter);
		}

		//Возвращает последнего заполненного столбца
		public static int GetLastUsedColumnIndex(ExcelWorksheet worksheet)
		{
			int lastUsedColumnIndex = worksheet.CalculateMaxUsedColumns() - 1;
			int maxRowIndex = worksheet.Rows.Count - 1;
			for (var i = lastUsedColumnIndex; i >= 0; i--)
			{
				if (worksheet.Columns[i].Cells.Where(x => x.Row.Index <= maxRowIndex)
					.All(x => x.ValueType == CellValueType.Null))
				{
					lastUsedColumnIndex--;
				}
				else
				{
					break;
				}
			}

			return lastUsedColumnIndex;
		}

		//Возвращает индекс последней заполненной строки
		public static int GetLastUsedRowIndex(ExcelWorksheet worksheet)
		{
			var lastUsedRowIndex = worksheet.Rows.Count - 1;
			for (var i = lastUsedRowIndex; i >= 0; i--)
			{
				if (worksheet.Rows[i].AllocatedCells.All(x => x.ValueType == CellValueType.Null))
				{
					lastUsedRowIndex--;
				}
				else
				{
					break;
				}
			}

			return lastUsedRowIndex;
		}

		public static void AddSuccessHeaderColumn(ExcelWorksheet mainWorkSheet, int column)
		{
			mainWorkSheet.Rows[0].Cells[column].SetValue($"Результат сохранения");
			mainWorkSheet.Rows[0].Cells[column].Style.Borders.SetBorders(MultipleBorders.All,
				SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
		}

		public static void AddSuccessCell(ExcelWorksheet mainWorkSheet, int row, int column, string message)
		{
			mainWorkSheet.Rows[row].Cells[column].SetValue(message);
			mainWorkSheet.Rows[row].Cells[column].Style.FillPattern
				.SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
			mainWorkSheet.Rows[row].Cells[column].Style.Borders
				.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
					LineStyle.Thin);
		}

		public static void AddWarningCell(ExcelWorksheet mainWorkSheet, int row, int column, string message)
		{
			mainWorkSheet.Rows[row].Cells[column].SetValue(message);
			mainWorkSheet.Rows[row].Cells[column].Style.FillPattern
				.SetSolid(SpreadsheetColor.FromName(ColorName.Yellow));
			mainWorkSheet.Rows[row].Cells[column].Style.Borders
				.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
					LineStyle.Thin);
		}

		public static void AddErrorCell(ExcelWorksheet mainWorkSheet, int row, int column, string message)
		{
			mainWorkSheet.Rows[row].Cells[column].SetValue(message);
			mainWorkSheet.Rows[row].Cells[column].Style.FillPattern
				.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
			mainWorkSheet.Rows[row].Cells[column].Style.Borders
				.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
					LineStyle.Thin);
		}
	}
}