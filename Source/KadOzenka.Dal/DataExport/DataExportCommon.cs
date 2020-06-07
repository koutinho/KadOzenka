using GemBox.Spreadsheet;
using GemBox.Document;
using GemBox.Document.Tables;
using System;
using System.Xml;
using ObjectModel.KO;
using System.Collections.Generic;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.TD;
using System.Globalization;
using System.IO;

namespace KadOzenka.Dal.DataExport
{
    public class DataExportCommon
    {
        public static string specifierInteger = "D";
        public static string specifierDouble = "N";
        public static CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");

        /// <summary>
        /// Добавление аттрибута к объекту XML
        /// </summary>
        public static void AddAttribute(XmlDocument xmlFile, XmlNode xn, string name, string value)
        {
            XmlAttribute xa = xn.Attributes.Append(xmlFile.CreateAttribute(name));
            xa.Value = value;
        }
        
        /// <summary>
        /// Добавление в Excel строки данных в строку с индексом Row
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

                sheet.Rows[Row].Cells[Col].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                Col++;
            }
        }
        
        /// <summary>
        /// Добавление в Excel таблицы данных, начиная со строки с индексом Row
        /// </summary>
        public static void AddRow(ExcelWorksheet sheet, int Row, object[,] _values, int _y = -1)
        {
            int count_rows    = (_y == -1) ? _values.GetUpperBound(0) + 1 : _y;
            int count_columns = _values.GetUpperBound(1)+1;

            for (int inx_col=0; inx_col < count_columns; inx_col++)
            {
                // Считываем r-ую строку
                object[] values = new object[count_rows];
                for (int r = 0; r < count_rows; r++)
                {
                    values[r] = _values[r, inx_col];
                }

                int inx_row = 0;
                foreach (object value in values)
                {
                    if (value is Boolean) sheet.Rows[Row + inx_row].Cells[inx_col].SetValue(Convert.ToBoolean(value) ? "Да" : "Нет");
                    else
                    if (value is string) sheet.Rows[Row + inx_row].Cells[inx_col].SetValue(value as string);
                    else
                    if (value is int)
                    {
                        sheet.Rows[Row + inx_row].Cells[inx_col].SetValue((int)value);
                        sheet.Rows[Row + inx_row].Cells[inx_col].Style.NumberFormat = "#,##0";
                    }
                    else
                    if (value is decimal || value is double)
                    {
                        sheet.Rows[Row + inx_row].Cells[inx_col].SetValue(Convert.ToDouble(value));
                        sheet.Rows[Row + inx_row].Cells[inx_col].Style.NumberFormat = "#,##0.00";
                    }
                    else
                    if (value is DateTime)
                    {
                        sheet.Rows[Row + inx_row].Cells[inx_col].SetValue(Convert.ToDateTime(value));
                        sheet.Rows[Row + inx_row].Cells[inx_col].Style.NumberFormat = "mm/dd/yyyy";
                    }
                    else
                    if (value is int?)
                    {
                        if (value == null)
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col].SetValue("-");
                        }
                        else
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col].SetValue((int)value);
                            sheet.Rows[Row + inx_row].Cells[inx_col].Style.NumberFormat = "#,##0";
                        }
                    }
                    else
                    if (value is decimal?)
                    {
                        if (value == null)
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col].SetValue("-");
                        }
                        else
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col].SetValue(Convert.ToDouble(value));
                            sheet.Rows[Row + inx_row].Cells[inx_col].Style.NumberFormat = "#,##0.00";
                        }
                    }
                    else
                    if (value is DateTime?)
                    {
                        if (value == null)
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col].SetValue("-");
                        }
                        else
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col].SetValue(Convert.ToDateTime(value));
                            sheet.Rows[Row + inx_row].Cells[inx_col].Style.NumberFormat = "mm/dd/yyyy";
                        }
                    }

                    sheet.Rows[Row + inx_row].Cells[inx_col].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                    inx_row++;
                }
            }
        }

        /// <summary>
        /// Добавление в Excel таблицы данных, начиная со строки с индексом Row и столбца Col
        /// </summary>
        public static void AddRow(ExcelWorksheet sheet, int Row, int Col, object[,] _values, int _y = -1)
        {
            int count_rows = (_y == -1) ? _values.GetUpperBound(0) + 1 : _y;
            int count_columns = _values.GetUpperBound(1) + 1;

            for (int inx_col = 0; inx_col < count_columns; inx_col++)
            {
                // Считываем r-ую строку
                object[] values = new object[count_rows];
                for (int r = 0; r < count_rows; r++)
                {
                    values[r] = _values[r, inx_col];
                }

                int inx_row = 0;
                foreach (object value in values)
                {
                    if (value is Boolean) sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue(Convert.ToBoolean(value) ? "Да" : "Нет");
                    else
                    if (value is string) sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue(value as string);
                    else
                    if (value is int)
                    {
                        sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue((int)value);
                        sheet.Rows[Row + inx_row].Cells[inx_col + Col].Style.NumberFormat = "#,##0";
                    }
                    else
                    if (value is decimal || value is double)
                    {
                        sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue(Convert.ToDouble(value));
                        sheet.Rows[Row + inx_row].Cells[inx_col + Col].Style.NumberFormat = "#,##0.00";
                    }
                    else
                    if (value is DateTime)
                    {
                        sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue(Convert.ToDateTime(value));
                        sheet.Rows[Row + inx_row].Cells[inx_col + Col].Style.NumberFormat = "mm/dd/yyyy";
                    }
                    else
                    if (value is int?)
                    {
                        if (value == null)
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue("-");
                        }
                        else
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue((int)value);
                            sheet.Rows[Row + inx_row].Cells[inx_col + Col].Style.NumberFormat = "#,##0";
                        }
                    }
                    else
                    if (value is decimal?)
                    {
                        if (value == null)
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue("-");
                        }
                        else
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue(Convert.ToDouble(value));
                            sheet.Rows[Row + inx_row].Cells[inx_col + Col].Style.NumberFormat = "#,##0.00";
                        }
                    }
                    else
                    if (value is DateTime?)
                    {
                        if (value == null)
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue("-");
                        }
                        else
                        {
                            sheet.Rows[Row + inx_row].Cells[inx_col + Col].SetValue(Convert.ToDateTime(value));
                            sheet.Rows[Row + inx_row].Cells[inx_col + Col].Style.NumberFormat = "mm/dd/yyyy";
                        }
                    }

                    sheet.Rows[Row + inx_row].Cells[inx_col + Col].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                    inx_row++;
                }
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

                sheet.Rows[Row].Cells[Col].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
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
        /// Записать значение в ячейку без границ
        /// </summary>
        public static void SetCellValueNoBorder(ExcelWorksheet _sheet, int _x, int _y, string _val)
        {
            _sheet.Rows[_y].Cells[_x].SetValue(_val);
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
        public static void MergeCell(ExcelWorksheet sheet, int FirstRow, int LastRow, int ColFirst, int ColLast, string value, bool wrap = false)
        {
            sheet.Rows[FirstRow].Cells[ColFirst].SetValue(value);
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Merged = true;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.WrapText = wrap;
        }
        
        /// <summary>
        /// Получить значение атрибута по его номеру
        /// </summary>
        public static bool GetObjectAttribute(OMUnit _unit, long _number, out string _value)
        {
            bool result = false;
            _value = "";
            List<long> lstIds = new List<long>();
            lstIds.Add(_number);
            List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(_unit.ObjectId.Value, null, lstIds, _unit.CreationDate.Value);
            if (attribs.Count > 0)
            {
                _value = attribs[0].StringValue;
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Проверка значений УПКСЗ и КС с предыдущими. Были ли изменения. Поиск по Кадастровому номеру
        /// </summary>
        public static bool GetObjectLastByKN(OMUnit _unit)
        {
            bool result = true;
            OMUnit units_out = new OMUnit();
            List<OMUnit> units = OMUnit.Where(x => x.CadastralNumber == _unit.CadastralNumber).SelectAll().Execute();
            TimeSpan ts = TimeSpan.MaxValue;
            foreach (OMUnit unit_curr in units)
            {
                DateTime dt_curr = unit_curr.CreationDate.Value;
                DateTime dt_in = _unit.CreationDate.Value;
                if (dt_in.Date >= dt_curr.Date)
                {
                    if (dt_in - dt_curr <= ts)
                    {
                        ts = dt_in - dt_curr;
                        units_out = unit_curr;
                    }
                }
            }
            if (units_out != null)
            {
                if (_unit.Upks == units_out.Upks && _unit.CadastralCost == units_out.CadastralCost)
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Получить полное название документа: наименование, номер
        /// </summary>
        public static string GetFullNameDoc(OMInstance _doc)
        {
            return _doc.Description +
                   ((_doc.RegNumber != string.Empty) ? " №" : string.Empty) + _doc.RegNumber +
                   ((NullConvertorMS.DTtoSTR(_doc.CreateDate) != string.Empty) ? " от " : string.Empty) + NullConvertorMS.DTtoSTR(_doc.CreateDate);
        }

        public static string GetShortNameDoc(OMInstance _doc)
        {
            string name_doc = _doc.Description.Substring(0, _doc.Description.IndexOf('(') > 0 ? _doc.Description.IndexOf('(') : _doc.Description.Length).Trim();
            return name_doc + ", №" + _doc.RegNumber + " от " + NullConvertorMS.DTtoSTR(_doc.CreateDate);
        }

        /// <summary>
        /// Получить полный номер группы, с учетом родительской группы если есть
        /// </summary>
        public static string GetFullNumberGroup(OMGroup _group)
        {
            string full_group_num = "";
            if (_group.ParentId > 0)
            {
                OMGroup parent_group = OMGroup.Where(x => x.Id == _group.ParentId).SelectAll().ExecuteFirstOrDefault();

                full_group_num = ((parent_group.Number == null ? parent_group.Id.ToString() : parent_group.Number)) + "." +
                                                ((_group.Number == null ? _group.Id.ToString() : _group.Number));
            }
            else
            {
                full_group_num = _group.Number == null ? _group.Id.ToString() : _group.Number;
            }

            return full_group_num;
        }

        /// <summary>
        /// Получить полноре имя группы с учетом ее номера
        /// </summary>
        public static string GetFullNameGroup(OMGroup _group)
        {
            return GetFullNumberGroup(_group) + " " + _group.GroupName;
        }

        /// <summary>
        /// Создает строку в таблице документа doc
        /// Возарвщает индекс строки
        /// </summary>
        public static int AddRowToTableDoc(DocumentModel _document, Table _table, int _count_cells, int _begin = -1, int _end = -1)
        {
            _table.Rows.Add(new TableRow(_document));
            int idx_row = _table.Rows.Count - 1;

            for (int i = 0; i < _count_cells; i++)
            {
                if (_begin != -1 && _end != -1)
                {
                    if (_begin == i)
                    {
                        string str_merge = "Cell (" + _begin.ToString() +
                                   "," + idx_row.ToString() + ") -> (" +
                                   _end.ToString() + "," + idx_row.ToString() + ")";
                        var cell_merge = new TableCell(_document, new Paragraph(_document));
                        cell_merge.ColumnSpan = _end - _begin + 1;
                        _table.Rows[idx_row].Cells.Add(cell_merge);
                        i = _end;
                    }
                    else
                    {
                        var cell = new TableCell(_document);
                        _table.Rows[idx_row].Cells.Add(cell);
                    }
                }
                else
                {
                    var cell = new TableCell(_document);
                    _table.Rows[idx_row].Cells.Add(cell);
                }
            }

            return idx_row;
        }

        public static int AddEmptyRowToTableDoc(DocumentModel _document, Table _table, int _count_cells)
        {
            int idx = AddRowToTableDoc(_document, _table, _count_cells, 0, _count_cells - 1);
            SetTextToCellDoc(_document, _table.Rows[idx].Cells[0], "", 8, HorizontalAlignment.Center, false, false);

            return idx;
        }

        public static string SStr(string value)
        {
            if (value == string.Empty) return "-"; else return value;
        }

        /// <summary>
        /// Вставить текст в ячейку таблицы документа doc
        /// </summary>
        public static void SetTextToCellDoc(DocumentModel _document, TableCell _cell, 
                                    string _col1, double _size,
                                    HorizontalAlignment _align1, bool _border, bool _bold)
        {
            Paragraph paragraph = new Paragraph(_document);
            paragraph.ParagraphFormat.Alignment = _align1;
            Run run = new Run(_document, _col1);
            run.CharacterFormat.FontName  = "Times New Roman";
            run.CharacterFormat.Bold      = _bold;
            run.CharacterFormat.FontColor = Color.Black;
            run.CharacterFormat.Size      = _size;
            paragraph.Inlines.Add(run);
            _cell.Blocks.Add(paragraph);
            _cell.CellFormat.VerticalAlignment = VerticalAlignment.Center;

            if (_border)
            {
                _cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1);
            }
            else
                _cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.None, Color.Black, 1);
        }

        public static void SetText2Doc(DocumentModel document, TableRow row,
                            string col1, string col2, int size,
                            HorizontalAlignment align1, HorizontalAlignment align2,
                            bool border, bool bold)
        {
            SetTextToCellDoc(document, row.Cells[0], col1, size, align1, border, bold);
            SetTextToCellDoc(document, row.Cells[1], col2, size, align2, border, bold);
        }

        public static void SetText3Doc(DocumentModel document, TableRow row,
                                    string col1, string col2, string col3, int size,
                                    HorizontalAlignment align1, HorizontalAlignment align2, HorizontalAlignment align3, 
                                    bool border, bool bold)
        {
            SetTextToCellDoc(document, row.Cells[0], col1, size, align1, border, bold);
            SetTextToCellDoc(document, row.Cells[1], col2, size, align2, border, bold);
            SetTextToCellDoc(document, row.Cells[2], col3, size, align3, border, bold);
        }

        public static void SetText4Doc(DocumentModel document, TableRow row,
                            string col1, string col2, string col3, string col4, int size,
                            HorizontalAlignment align1, HorizontalAlignment align2, HorizontalAlignment align3, HorizontalAlignment align4,
                            bool border, bool bold)
        {
            SetTextToCellDoc(document, row.Cells[0], col1, size, align1, border, bold);
            SetTextToCellDoc(document, row.Cells[1], col2, size, align2, border, bold);
            SetTextToCellDoc(document, row.Cells[2], col3, size, align3, border, bold);
            SetTextToCellDoc(document, row.Cells[3], col4, size, align4, border, bold);
        }
    }

    public class NullConvertorMS
    {
        public static string DTtoSTRDOC(DateTime value)
        {
            if (value <= Convert.ToDateTime("01.01.1901")) return String.Empty; else return " от " + value.ToString("dd.MM.yyyy");
        }

        public static object StringToDB(string value)
        {
            if (value == null)
                return DBNull.Value;
            if (value.Equals(String.Empty))
                return DBNull.Value;
            return value;
        }

        public static string ToString(object value)
        {
            return (value == null) ? String.Empty : value.ToString();
        }

        public static string ToStringOrNull(object value)
        {
            return ((value == null) || (value == DBNull.Value)) ? null : value.ToString();
        }

        public static string ToString(string value)
        {
            return (value == null) ? String.Empty : value;
        }

        public static void SwapString(ref string sold, ref string snew)
        {
            if (snew == null)
                snew = sold;
        }

        public static void SwapDouble(ref double dold, ref double dnew)
        {
            if (dnew == double.MinValue)
                dnew = dold;
        }

        public static void SwapInt(ref Int32 dold, ref Int32 dnew)
        {
            if (dnew == Int32.MinValue)
                dnew = dold;
        }

        public static object IntIdToDB(int value)
        {
            if (value < 0)
                return DBNull.Value;
            else
                return value;
        }

        public static object IntIdToDB(Int64 value)
        {
            if (value < 0)
                return DBNull.Value;
            else
                return value;
        }

        public static object Int64ToDB(Int64 value)
        {
            if (value < 0)
                return DBNull.Value;
            else
                return value;
        }

        public static object IntToSTR(int value)
        {
            if (value == int.MinValue)
                return String.Empty;
            else
                return value.ToString();
        }

        public static int DBToInt(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return int.MinValue;
            else
                return Convert.ToInt32(value);
        }

        public static bool DBToBoolean(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return false;
            else
                if (Convert.ToInt32(value) > 0) return true;
            else
                return false;
        }

        public static byte[] DBToByteArray(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return null;
            else
                return (byte[])(value);
        }

        public static int DBToInt(object value, int _default)
        {
            if ((value == null) || (value == DBNull.Value))
                return _default;
            else
                return Convert.ToInt32(value);
        }

        public static Int64 DBToInt64(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return -1;
            else
                return Convert.ToInt64(value);
        }

        public static object DoubleToDB(double value)
        {
            if (value == double.MinValue)
                return DBNull.Value;
            else
                return value;
        }

        public static object DoubleToObject(double value)
        {
            if (value == double.MinValue)
                return "-";
            else
                return value;
        }

        public static double DBToDouble(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return double.MinValue;
            else
                return Convert.ToDouble(value);
        }

        public static decimal DBToDecimal(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return decimal.MinValue;
            else
                return Convert.ToDecimal(value);
        }

        public static string DBDoubleNullToString(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return String.Empty;
            else
                return Convert.ToDouble(value).ToString("0.##");
        }

        public static object DateTimeToDB(DateTime value)
        {
            if (value <= Convert.ToDateTime("01.01.1901"))
                return DBNull.Value;
            else
                return value;
        }

        public static DateTime DBToDateTime(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(value);
        }

        public static object IdNameToDB(int id, string name)
        {
            if (id != int.MinValue)
                return DBNull.Value;
            else
            {
                return name;
            }
        }

        public static string DTtoSTR(DateTime value)
        {
            if (value <= Convert.ToDateTime("01.01.1901")) return String.Empty; else return value.ToString("dd.MM.yyyy");
        }

        public static string DTtoSTRTime(DateTime value)
        {
            if (value <= Convert.ToDateTime("01.01.1901")) return String.Empty; else return value.ToString("dd.MM.yyyy") + " " + value.ToLongTimeString();
        }

        public static string DoubletoSTR(double value)
        {
            if (value == double.MinValue) return String.Empty; else return value.ToString();
        }

        public static string PriceToSTR(double value)
        {
            if (value == double.MinValue) return String.Empty; else return value.ToString("0.00");
        }

        public static double PriceToDouble(double value)
        {
            if (value == double.MinValue) return 0; else return Convert.ToDouble(value.ToString("0.00"));
        }

        public static string DecimaltoSTR(decimal? value)
        {
            if (value == decimal.MinValue) return String.Empty; else return value.ToString();
        }

        public static string BooltoSTR(bool value)
        {
            if (!value) return String.Empty; else return "Да";
        }
    }
}
