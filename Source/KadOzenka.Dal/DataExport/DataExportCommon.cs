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
using System.Linq;
using Serilog;
using SerilogTimings.Extensions;
using LengthUnit = GemBox.Spreadsheet.LengthUnit;

namespace KadOzenka.Dal.DataExport
{
    public class DataExportCommon
    {
        public static string specifierInteger = "D";
        public static string specifierDouble = "N";
        public static CultureInfo Culture = CultureInfo.CreateSpecificCulture("ru-RU");

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
        public static void AddRow(ExcelWorksheet sheet, int Row, object[] values, CellStyle cellStyle = null)
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
                else
                {
	                sheet.Rows[Row].Cells[Col].SetValue(value?.ToString());
                }

                sheet.Rows[Row].Cells[Col].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

                if (cellStyle != null)
	                sheet.Rows[Row].Cells[Col].Style = cellStyle;

                Col++;
            }
        }

        public static void SetIndividualWidth(ExcelWorksheet sheet, int column, int width)
        {
	        sheet.Columns[column].SetWidth(width, LengthUnit.Centimeter);
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
                if (_values[0, inx_col] == null) continue;

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
        /// Записать значение в ячейку без границ
        /// </summary>
        public static void SetCellValueNoBorder(ExcelWorksheet _sheet, int _x, int _y, string _val,
            HorizontalAlignmentStyle hor, VerticalAlignmentStyle vert)
        {
            _sheet.Rows[_y].Cells[_x].SetValue(_val);
            _sheet.Cells.GetSubrangeAbsolute(_y, _x, _y, _x).Style.HorizontalAlignment = hor;
            _sheet.Cells.GetSubrangeAbsolute(_y, _x, _y, _x).Style.VerticalAlignment = vert;
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
        public static void MergeCell(ExcelWorksheet sheet, int FirstRow, int LastRow, int FirstCol, int LastCol, string value, bool wrap = false)
        {
            sheet.Rows[FirstRow].Cells[FirstCol].SetValue(value);
            sheet.Cells.GetSubrangeAbsolute(FirstRow, FirstCol, LastRow, LastCol).Merged = true;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, FirstCol, LastRow, LastCol).Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
            sheet.Cells.GetSubrangeAbsolute(FirstRow, FirstCol, LastRow, LastCol).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, FirstCol, LastRow, LastCol).Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, FirstCol, LastRow, LastCol).Style.WrapText = wrap;
        }
        
        /// <summary>
        /// Получить значение атрибута по его номеру
        /// </summary>
        public static bool GetObjectAttribute(OMUnit _unit, long _number, out string _value)
        {
            using (Log.Logger.TimeOperation("Получение атрибутов объекта"))
            {
                bool result = false;
                _value = "";
                List<long> lstIds = new List<long>();
                lstIds.Add(_number);
                List<GbuObjectAttribute> attribs =
                    new GbuObjectService().GetAllAttributes(_unit.ObjectId.Value, null, lstIds,
                        _unit.CreationDate.Value);
                if (attribs.Count > 0)
                {
                    _value = attribs[0].StringValue;
                    result = true;
                }

                return result;
            }
        }

        public static bool GetObjectAttribute(List<GbuObjectAttribute> list, OMUnit unit, long attrNumber, out string value)
        {
            bool result = false;
            var value1 = "";
            var attribs = list.Where(x => x.AttributeId == attrNumber && x.ObjectId == unit.ObjectId && x.S <= unit.CreationDate.GetValueOrDefault()).OrderByDescending(x=>x.S).ToList();
            if (attribs.Count > 0)
            {
                value1 = attribs[0].StringValue;
                result = true;
            }

            value = value1;
            return result;
        }

        public static bool GetObjectAttribute(DEKOGroup.UnitInfo unitInfo, long attrNumber, out string value)
        {
            var list = unitInfo.Attributes;
            var unit = unitInfo.Unit;
            bool result = false;
            var value1 = "";
            var attribs = list.Where(x => x.AttributeId == attrNumber && x.ObjectId == unit.ObjectId && x.S <= unit.CreationDate.GetValueOrDefault()).OrderByDescending(x=>x.S).ToList();
            if (attribs.Count > 0)
            {
                value1 = attribs[0].StringValue;
                result = true;
            }

            value = value1;
            return result;
        }

        public static bool GetObjectAttribute(Dictionary<long,List<GbuObjectAttribute>> dict, OMUnit unit, long attrNumber, out string value)
        {
            value = "";
            var dictLookup = dict.TryGetValue(unit.ObjectId.GetValueOrDefault(), out var list);
            if (!dictLookup) return false;
            var attribs = list.Where(x => x.AttributeId == attrNumber && x.ObjectId == unit.ObjectId && x.S <= unit.CreationDate.GetValueOrDefault()).OrderByDescending(x=>x.S).ToList();
            if (attribs.Count <= 0) return false;
            value = attribs[0].StringValue;
            return true;
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
            OMGroup parent_group = OMGroup.Where(x => x.Id == _group.ParentId).SelectAll().ExecuteFirstOrDefault();
            if (parent_group != null)
            {
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
                        cell_merge.Blocks.Clear();  // Ощищаем после объединения, убираем пустую строку
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

        /// <summary>
        /// Вставить текст в ячейку таблицы документа doc
        /// </summary>
        public static void SetTextToCellDoc(DocumentModel document, TableCell cell, 
                                    string cellText, double size,
                                    HorizontalAlignment alignment, bool border, bool bold)
        {
            // Спецсимволы: '\n', '\r', '\t', '\v' and '\f' не обрабатываются в Run / Run.Text
            var symbolsToSplitOn = new List<char> { '\t', '\v', '\f' };
            
            var paragraphs =
                cellText.Replace("\r\n", "\n")
                    .Split('$', '\r', '\n'); // переносы строк, $ оставлен для обратной совместимости 

            foreach (string partext in paragraphs)
            {
                Paragraph paragraph = new Paragraph(document);
                paragraph.ParagraphFormat.Alignment = alignment;


                var inputStr = partext.Replace("\r\n", "\n"); // CRLF -> LF
                var lastPartStartIndex = 0;
                for (int i = 0; i < inputStr.Length; i++)
                {
                    if (symbolsToSplitOn.Contains(inputStr[i]) || i == inputStr.Length-1)
                    {
                        var strPart = inputStr.Substring(lastPartStartIndex, 
                            (i==inputStr.Length-1)
                                ? i - lastPartStartIndex + 1 
                                : i - lastPartStartIndex);
                        lastPartStartIndex = i + 1;
                        var run = GetStyledRunInstance(document, strPart, size, bold);
                        paragraph.Inlines.Add(run);

                        switch (inputStr[i])
                        {
                            case '\t':
                                paragraph.Inlines.Add(new SpecialCharacter(document,
                                    SpecialCharacterType.Tab));
                                break;
                            case '\f':
                                paragraph.Inlines.Add(new SpecialCharacter(document,
                                    SpecialCharacterType.PageBreak));
                                break;
                            case '\v':
                                paragraph.Inlines.Add(new SpecialCharacter(document,
                                    SpecialCharacterType.ColumnBreak));
                                break;
                        }
                    }
                }

                cell.Blocks.Add(paragraph);
                cell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
            }

            cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside,
                border ? BorderStyle.Single : BorderStyle.None, Color.Black, 1);
        }

        private static Run GetStyledRunInstance(DocumentModel document, string text, double size, bool bold = false)
        {
            Run run = new Run(document, text);
            run.CharacterFormat.FontName = "Times New Roman";
            run.CharacterFormat.Bold = bold;
            run.CharacterFormat.FontColor = Color.Black;
            run.CharacterFormat.Size = size;
            return run;
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
    }
}
