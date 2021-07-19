using System;
using GemBox.Spreadsheet;

namespace CommonSdks
{
	public class DataExportCommon
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
	}
}