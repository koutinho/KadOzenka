

using GemBox.Spreadsheet;
using System;
using System.Xml;

namespace KadOzenka.Dal.DataExport
{
    public class DataExportCommon
    {
        /// <summary>
        /// Добавление аттрибута к объекту XML
        /// </summary>
        public static void AddAttribute(XmlDocument xmlFile, XmlNode xn, string name, string value)
        {
            XmlAttribute xa = xn.Attributes.Append(xmlFile.CreateAttribute(name));
            xa.Value = value;
        }
        /// <summary>
        /// Добавление в Excel данных в строку с индексом Row
        /// </summary>
        public static void AddRow(ExcelWorksheet sheet, int Row, object[] values)
        {
            int Col = 0;
            foreach (object value in values)
            {
                if (value is Boolean) sheet.Rows[Row].Cells[Col].SetValue(Convert.ToBoolean(value) ? "Да" : "Нет");
                else
                if (value is string) sheet.Rows[Row].Cells[Col].SetValue(value as string);
                else
                if (value is int)
                {
                    sheet.Rows[Row].Cells[Col].SetValue((int)value);
                    sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0";
                }
                else
                if (value is decimal || value is double)
                {
                    sheet.Rows[Row].Cells[Col].SetValue(Convert.ToDouble(value));
                    sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0.00";
                }
                else
                if (value is DateTime)
                {
                    sheet.Rows[Row].Cells[Col].SetValue(Convert.ToDateTime(value));
                    sheet.Rows[Row].Cells[Col].Style.NumberFormat = "mm/dd/yyyy";
                }
                else
                if (value is int?)
                {
                    if (value == null)
                    {
                        sheet.Rows[Row].Cells[Col].SetValue("-");
                    }
                    else
                    {
                        sheet.Rows[Row].Cells[Col].SetValue((int)value);
                        sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0";
                    }
                }
                else
                if (value is decimal?)
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
                else
                if (value is DateTime?)
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

                sheet.Rows[Row].Cells[Col].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                Col++;
            }
        }
        /// <summary>
        /// Добавление в Excel данных в строку с индексом Row и установка ширины столбцов
        /// </summary>
        public static void AddRow(ExcelWorksheet sheet, int Row, object[] values, int[] widths, bool wrap, bool center, bool bold)
        {
            int Col = 0;
            foreach (object value in values)
            {
                if (value is Boolean) sheet.Rows[Row].Cells[Col].SetValue(Convert.ToBoolean(value) ? "Да" : "Нет");
                else
                if (value is string) sheet.Rows[Row].Cells[Col].SetValue(value as string);
                else
                if (value is int)
                {
                    sheet.Rows[Row].Cells[Col].SetValue((int)value);
                    sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0";
                }
                else
                if (value is decimal || value is double)
                {
                    sheet.Rows[Row].Cells[Col].SetValue(Convert.ToDouble(value));
                    sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0.00";
                }
                else
                if (value is DateTime)
                {
                    sheet.Rows[Row].Cells[Col].SetValue(Convert.ToDateTime(value));
                    sheet.Rows[Row].Cells[Col].Style.NumberFormat = "mm/dd/yyyy";
                }
                else
                if (value is int?)
                {
                    if (value == null)
                    {
                        sheet.Rows[Row].Cells[Col].SetValue("-");
                    }
                    else
                    {
                        sheet.Rows[Row].Cells[Col].SetValue((int)value);
                        sheet.Rows[Row].Cells[Col].Style.NumberFormat = "#,##0";
                    }
                }
                else
                if (value is decimal?)
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
                else
                if (value is DateTime?)
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

                sheet.Rows[Row].Cells[Col].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                if (wrap)
                {
                    sheet.Rows[Row].Cells[Col].Style.WrapText = wrap;
                }

                if (center)
                {
                    sheet.Rows[Row].Cells[Col].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                    sheet.Rows[Row].Cells[Col].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
                }

                if (bold)
                {
                    sheet.Rows[Row].Cells[Col].Style.Font.Weight = ExcelFont.BoldWeight;
                }

                sheet.Columns[Col].Width = widths[Col];
                Col++;
            }
        }
        /// <summary>
        /// Объединение ячеек в одной строке и установка данных в Excel
        /// </summary>
        public static void MergeCell(ExcelWorksheet sheet, int Row, int ColFirst, int ColLast, string value)
        {
            MergeCell(sheet, Row, Row, ColFirst, ColLast, value);
        }
        /// <summary>
        /// Объединение ячеек и установка данных в Excel
        /// </summary>
        public static void MergeCell(ExcelWorksheet sheet, int FirstRow, int LastRow, int ColFirst, int ColLast, string value)
        {
            sheet.Rows[FirstRow].Cells[ColFirst].SetValue(value);
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Merged = true;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.VerticalAlignment = VerticalAlignmentStyle.Center;
        }
    }
}
