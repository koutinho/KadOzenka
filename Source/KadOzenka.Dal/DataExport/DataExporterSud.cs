using Core.Main.FileStorages;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace KadOzenka.Dal.DataExport
{
    public class DataExporterSud
    {
        /// <summary>
        /// Добавление аттрибута к объекту XML
        /// </summary>
        private static void AddAttribute(XmlDocument xmlFile, XmlNode xn, string name, string value)
        {
            XmlAttribute xa = xn.Attributes.Append(xmlFile.CreateAttribute(name));
            xa.Value = value;
        }
        /// <summary>
        /// Добавление в Excel данных в строку с индексом Row
        /// </summary>
        private static void AddRow(ExcelWorksheet sheet, int Row, object[] values)
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
        private static void AddRow(ExcelWorksheet sheet, int Row, object[] values, int[] widths, bool wrap, bool center, bool bold)
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
        private static void MergeCell(ExcelWorksheet sheet, int Row, int ColFirst, int ColLast, string value)
        {
            MergeCell(sheet, Row, Row, ColFirst, ColLast, value);
        }
        /// <summary>
        /// Объединение ячеек и установка данных в Excel
        /// </summary>
        private static void MergeCell(ExcelWorksheet sheet, int FirstRow, int LastRow, int ColFirst, int ColLast, string value)
        {
            sheet.Rows[FirstRow].Cells[ColFirst].SetValue(value);
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Merged = true;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            sheet.Cells.GetSubrangeAbsolute(FirstRow, ColFirst, LastRow, ColLast).Style.VerticalAlignment = VerticalAlignmentStyle.Center;
        }


        /// <summary>
        /// Выгрузка судебных решений для ГБУ в формате Excel
        /// </summary>
        public static Stream ExportDataToExcelGbu()
        {
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Экспорт данных");

            AddRow(mainWorkSheet, 0, new object[] { "Кадастровый номер объекта", "Дата определения КС", "Кадастровая стоимость", "№ дела", "Дата судебного акта", "Установленная судом РС", "Административный истец" });

            List<ObjectModel.Sud.OMObject> objs = ObjectModel.Sud.OMObject.Where().SelectAll().Execute();
            int curIndex = 0;
            if (objs.Count > 0)
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                ParallelOptions options = new ParallelOptions
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = 20
                };

                object locked = new object();
                List<List<object>> values = new List<List<object>>();

                Parallel.ForEach(objs, options, obj =>
                {
                    curIndex++;
                    if (curIndex % 40 == 0) Console.WriteLine(curIndex);


                    string Kn = obj.Kn.Replace("\n", "").Replace("\r", "").Replace(" ", "");



                    obj.SudLink = ObjectModel.Sud.OMSudLink.Where(x => x.IdObject == obj.Id).SelectAll().Execute();

                    ObjectModel.Sud.OMSudLink last = null;
                    ObjectModel.Sud.OMSud lastsud = null;
                    DateTime lastSudDate = DateTime.MinValue;
                    foreach (ObjectModel.Sud.OMSudLink link in obj.SudLink)
                    {
                        ObjectModel.Sud.OMSud tmpSud = ObjectModel.Sud.OMSud.Where(x => x.Id == link.IdSud).SelectAll().ExecuteFirstOrDefault();
                        if (tmpSud.SudDate > lastSudDate)
                        {
                            lastSudDate = tmpSud.SudDate.ParseToDateTime();
                            last = link;
                            lastsud = tmpSud;
                        }
                    }
                    if (last != null && lastsud != null)
                    {
                        if (lastsud.Status == 1 || lastsud.Status == 4)
                        {
                            #region Заголовок объекта
                            List<object> value = new List<object>();
                            value.Add(Kn);
                            value.Add(obj.Date);
                            value.Add(obj.Kc);
                            value.Add(lastsud.Number);
                            value.Add(lastsud.Date);
                            value.Add(last.Rs);
                            value.Add(obj.Owner);
                            #endregion


                            lock (locked)
                            {
                                values.Add(value);
                            }
                        }
                    }
                });

                int row = 1;
                foreach(List<object> value in values)
                {
                    AddRow(mainWorkSheet, row, value.ToArray());
                    row++;
                }
                Console.WriteLine(values.Count);
            }

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        /// <summary>
        /// Выгрузка судебных решений на сайт в формате XML
        /// </summary>
        public static Stream ExportDataToXml()
        {
            XmlDocument xmlFile = new XmlDocument();
            XmlNode xnLandValuation = xmlFile.CreateElement("SudExport");
            AddAttribute(xmlFile, xnLandValuation, "Version", "01");
            XmlNode xnobjects = xmlFile.CreateElement("Objects");

            List<ObjectModel.Sud.OMObject> objs = ObjectModel.Sud.OMObject.Where().SelectAll().Execute();
            int curIndex = 0;
            if (objs.Count > 0)
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                ParallelOptions options = new ParallelOptions
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = 20
                };

                object locked = new object();
                Parallel.ForEach(objs, options, obj =>
                {
                    curIndex++;
                    if (curIndex % 40 == 0) Console.WriteLine(curIndex);
                    string Kn = obj.Kn.Replace("\n", "").Replace("\r", "").Replace(" ", "");
                    string Type = (obj.Typeobj_Code == ObjectModel.Directory.Sud.SudObjectType.Site) ? "0" : "1";
                    int yearapp = obj.Date.ParseToDateTime().Year;
                    if (yearapp >= 2019) yearapp = 2019;
                    else
                    if (yearapp >= 2018) yearapp = 2018;
                    else
                    if (yearapp >= 2016) yearapp = 2016;
                    else
                    if (yearapp >= 2014) yearapp = 2014;
                    string Year = yearapp.ToString();

                    if (obj.SudLink.Count == 0)
                        obj.SudLink = ObjectModel.Sud.OMSudLink.Where(x => x.IdObject == obj.Id).SelectAll().Execute();

                    ObjectModel.Sud.OMSudLink last = null;
                    ObjectModel.Sud.OMSud lastsud = null;
                    DateTime lastSudDate = DateTime.MinValue;
                    foreach (ObjectModel.Sud.OMSudLink link in obj.SudLink)
                    {
                        ObjectModel.Sud.OMSud tmpSud = ObjectModel.Sud.OMSud.Where(x => x.Id == link.IdSud).SelectAll().ExecuteFirstOrDefault();
                        if (tmpSud.SudDate > lastSudDate)
                        {
                            lastSudDate = tmpSud.SudDate.ParseToDateTime();
                            last = link;
                            lastsud = tmpSud;
                        }
                    }
                    if (last != null && lastsud != null)
                    {
                        if (lastsud.Status == 1 || lastsud.Status == 4)
                        {
                            lock (locked)
                            {
                                XmlNode xnobject = xmlFile.CreateElement("object");
                                AddAttribute(xmlFile, xnobject, "kn", Kn);
                                AddAttribute(xmlFile, xnobject, "type", Type);
                                AddAttribute(xmlFile, xnobject, "year", Year);
                                //Установленная судом рыночная стоимость, руб.
                                XmlNode xnvalue_25 = xmlFile.CreateElement("value_25"); xnvalue_25.InnerText = last.Rs.ParseToString(); xnobject.AppendChild(xnvalue_25);
                                //Номер дела
                                XmlNode xnvalue_26 = xmlFile.CreateElement("value_26"); xnvalue_26.InnerText = lastsud.Number; xnobject.AppendChild(xnvalue_26);
                                //Дата вынесения решения судом
                                XmlNode xnvalue_27 = xmlFile.CreateElement("value_27"); xnvalue_27.InnerText = lastsud.SudDate.ParseToDateTime().ToString("dd.MM.yyyy"); xnobject.AppendChild(xnvalue_27);
                                xnobjects.AppendChild(xnobject);
                            }
                        }
                    }
                });


            }

            Console.WriteLine(xnobjects.ChildNodes.Count);


            xnLandValuation.AppendChild(xnobjects);
            xmlFile.AppendChild(xnLandValuation);


            MemoryStream stream = new MemoryStream();
            xmlFile.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
        /// <summary>
        /// Выгрузка полная в Excel
        /// </summary>
        public static Stream ExportAllDataToExcel()
        {
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Экспорт данных");

            List<ObjectModel.Sud.OMObject> objs = ObjectModel.Sud.OMObject.Where(x => x.Adres == "ул Шереметьевская, д.6, корп.1").SelectAll().Execute();
            int curIndex = 0;
            if (objs.Count > 0)
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                ParallelOptions optionsAnaliz = new ParallelOptions
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = 20,
                };
                ParallelOptions optionsExport = new ParallelOptions
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = 1,
                };


                int maxsud = 1;
                int maxzak = 1;
                int maxotchet = 1;
                object locked = new object();
                List<List<object>> values = new List<List<object>>();

                Parallel.ForEach(objs, optionsAnaliz, obj =>
                {
                    curIndex++;
                    if (curIndex % 40 == 0) Console.WriteLine(curIndex);

                    obj.SudLink = ObjectModel.Sud.OMSudLink.Where(x => x.IdObject == obj.Id).SelectAll().Execute();
                    obj.ZakLink = ObjectModel.Sud.OMZakLink.Where(x => x.IdObject == obj.Id).SelectAll().Execute();
                    obj.OtchetLink = ObjectModel.Sud.OMOtchetLink.Where(x => x.IdObject == obj.Id).SelectAll().Execute();
                    lock (locked)
                    {
                        maxsud = Math.Max(maxsud, obj.SudLink.Count);
                        maxzak = Math.Max(maxzak, obj.ZakLink.Count);
                        maxotchet = Math.Max(maxotchet, obj.OtchetLink.Count);
                    }
                });

                List<object> caption = new List<object>();

                #region Заголовок объекта
                caption.Add("Кадастровый номер объекта");
                caption.Add("Тип объекта");
                caption.Add("Адрес");
                caption.Add("Площадь, кв.");
                caption.Add("Дата определения стоимости");
                caption.Add("Оспариваемая КС");
                caption.Add("Наименование(ТЦ, БЦ)");
                caption.Add("Внесено в статистику ДГИ");
                caption.Add("Заказчик / Административный истец");
                #endregion

                for (int i = 0; i < maxotchet; i++)
                {
                    #region Заголовок отчета
                    caption.Add("Номер отчета");
                    caption.Add("Дата составления");
                    caption.Add("Текущее использование");
                    caption.Add("Организация");
                    caption.Add("Оценщик");
                    caption.Add("СРО");
                    caption.Add("Рыночная стоимость");
                    caption.Add("Удельная стоимость");
                    caption.Add("Дата получения");
                    caption.Add("Жалоба в СРО");
                    #endregion
                }

                for (int i = 0; i < maxsud; i++)
                {
                    #region Заголовок суда
                    caption.Add("Наименование суда");
                    caption.Add("Статус");
                    caption.Add("Номер дела");
                    caption.Add("Дата заседания");
                    caption.Add("Дата судебного акта");
                    caption.Add("Рыночная стоимость");
                    caption.Add("Источник информации");
                    caption.Add("Примечание");
                    #endregion
                }

                for (int i = 0; i < maxzak; i++)
                {
                    #region Заголовок заключения
                    caption.Add("Номер заключения");
                    caption.Add("Дата составления");
                    caption.Add("Текущее использование");
                    caption.Add("Организация");
                    caption.Add("Эксперт");
                    caption.Add("СРО");
                    caption.Add("Рыночная стоимость");
                    caption.Add("Удельная стоимость");
                    caption.Add("Предварительная рецензия");
                    caption.Add("Рецензия после анализа");
                    caption.Add("Дата сдачи");
                    caption.Add("Исполнитель");
                    caption.Add("Номер письма");
                    caption.Add("Примечание");
                    caption.Add("Рассмотрено с Ковалевым Д.В.");
                    #endregion
                }

                MergeCell(mainWorkSheet, 0, 1, 0, 8, "Объект");
                MergeCell(mainWorkSheet, 0, 9, 9 + (10 * maxotchet) - 1, "Отчет");
                MergeCell(mainWorkSheet, 0, 9 + (10 * maxotchet), 9 + (10 * maxotchet) + (8 * maxsud) - 1, "Судебное решение");
                MergeCell(mainWorkSheet, 0, 9 + (10 * maxotchet) + (8 * maxsud), 9 + (10 * maxotchet) + (8 * maxsud) + (15 * maxzak) - 1, "Экспертное заключение");

                for (int i = 0; i < maxotchet; i++)
                {
                    MergeCell(mainWorkSheet, 1, 9 + (10 * (i)), 9 + (10 * (i + 1)) - 1, "Отчет " + "№" + (i + 1).ToString());
                }

                for (int i = 0; i < maxsud; i++)
                {
                    MergeCell(mainWorkSheet, 1, 9 + (10 * maxotchet) + (8 * (i)), 9 + (10 * maxotchet) + (8 * (i + 1)) - 1, "Судебное решение " + "№" + (i + 1).ToString());
                }

                for (int i = 0; i < maxzak; i++)
                {
                    MergeCell(mainWorkSheet, 1, 9 + (10 * maxotchet) + (8 * maxsud) + (15 * (i)), 9 + (10 * maxotchet) + (8 * maxsud) + (15 * (i + 1)) - 1, "Экспертное заключение " + "№" + (i + 1).ToString());
                }

                AddRow(mainWorkSheet, 2, caption.ToArray());

                curIndex = 0;
                int dataRow = 2;
                Parallel.ForEach(objs, optionsExport, obj =>
                {
                    curIndex++;
                    if (curIndex % 40 == 0) Console.WriteLine(curIndex);

                    List<object> datas = FillExportData(obj, maxotchet, maxsud, maxzak);
                    lock (locked)
                    {
                        dataRow++;
                        AddRow(mainWorkSheet, dataRow, datas.ToArray());
                    }
                });

                Console.WriteLine(values.Count);
            }

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        /// <summary>
        /// Получение полных данных по объекту
        /// </summary>
        private static List<object> FillExportData(ObjectModel.Sud.OMObject obj, int maxotchet, int maxsud, int maxzak)
        {
            List<object> value = new List<object>();

            #region Заголовок объекта
            value.Add(obj.Kn.Replace("\n", "").Replace("\r", "").Replace(" ", ""));
            value.Add(obj.Typeobj);
            value.Add(obj.Adres);
            value.Add(obj.Square);
            value.Add(obj.Date);
            value.Add(obj.Kc);
            value.Add(obj.NameCenter);
            value.Add(obj.StatDgi);
            value.Add(obj.Owner);
            #endregion

            for (int i = 0; i < maxotchet; i++)
            {
                #region Заголовок отчета
                if (obj.OtchetLink.Count >= i + 1)
                {

                    ObjectModel.Sud.OMOtchet otchet = ObjectModel.Sud.OMOtchet.Where(x=>x.Id==obj.OtchetLink[i].IdOtchet).SelectAll().ExecuteFirstOrDefault();
                    if (otchet != null)
                    {
                        value.Add(otchet.Number);
                        value.Add(otchet.Date);
                        value.Add(obj.OtchetLink[i].Use);
                        value.Add(otchet.Org);
                        value.Add(otchet.Fio);
                        value.Add(otchet.Sro);
                        value.Add(obj.OtchetLink[i].Rs);
                        value.Add(obj.OtchetLink[i].Uprs);
                        value.Add(otchet.DateIn);
                        value.Add(otchet.Jalob==1);
                    }
                    else
                    {
                        value.Add("-");
                        value.Add("-");
                        value.Add(obj.OtchetLink[i].Use);
                        value.Add("-");
                        value.Add("-");
                        value.Add("-");
                        value.Add(obj.OtchetLink[i].Rs);
                        value.Add(obj.OtchetLink[i].Uprs);
                        value.Add("-");
                        value.Add("-");
                    }
                }
                else
                {
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                }
                #endregion
            }

            for (int i = 0; i < maxsud; i++)
            {
                #region Заголовок суда
                if (obj.SudLink.Count >= i + 1)
                {
                    ObjectModel.Sud.OMSud sud = ObjectModel.Sud.OMSud.Where(x => x.Id == obj.SudLink[i].IdSud).SelectAll().ExecuteFirstOrDefault();
                    if (sud != null)
                    {
                        value.Add(sud.Name);
                        string Status = "Без статуса";
                        switch (sud.Status)
                        {
                            case 1: Status = "Отказано";
                                break;
                            case 2: Status = "Удовлетворено";
                                break;
                            case 3: Status = "Приостановлено";
                                break;
                            case 4: Status = "Частично удовлетворено";
                                break;
                            default:
                                break;
                        }
                        value.Add(Status);
                        value.Add(sud.Number);
                        value.Add(sud.Date);
                        value.Add(sud.SudDate);
                        value.Add(obj.SudLink[i].Rs);
                        value.Add(obj.SudLink[i].Use);
                        value.Add(obj.SudLink[i].Descr);
                    }
                    else
                    {
                        value.Add("-");
                        value.Add("-");
                        value.Add("-");
                        value.Add("-");
                        value.Add("-");
                        value.Add(obj.SudLink[i].Rs);
                        value.Add(obj.SudLink[i].Use);
                        value.Add(obj.SudLink[i].Descr);
                    }
                }
                else
                {
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                }
                #endregion
            }

            for (int i = 0; i < maxzak; i++)
            {
                #region Заголовок заключения
                if (obj.ZakLink.Count >= i + 1)
                {
                    ObjectModel.Sud.OMZak zak = ObjectModel.Sud.OMZak.Where(x => x.Id == obj.ZakLink[i].IdZak).SelectAll().ExecuteFirstOrDefault();
                    if (zak != null)
                    {
                        value.Add(zak.Number);
                        value.Add(zak.Date);
                        value.Add(obj.ZakLink[i].Use);
                        value.Add(zak.Org);
                        value.Add(zak.Fio);
                        value.Add(zak.Sro);
                        value.Add(obj.ZakLink[i].Rs);
                        value.Add(obj.ZakLink[i].Uprs);
                        value.Add(zak.RecBefore == 1);
                        value.Add(zak.RecAfter == 1);
                        value.Add(zak.RecDate);
                        value.Add(zak.RecUser);
                        value.Add(zak.RecLetter);
                        value.Add(obj.ZakLink[i].Descr);
                        value.Add(zak.RecSoglas == 1);
                    }
                    else
                    {
                        value.Add("-");
                        value.Add("-");
                        value.Add(obj.ZakLink[i].Use);
                        value.Add("-");
                        value.Add("-");
                        value.Add("-");
                        value.Add(obj.ZakLink[i].Rs);
                        value.Add(obj.ZakLink[i].Uprs);
                        value.Add("-");
                        value.Add("-");
                        value.Add("-");
                        value.Add("-");
                        value.Add("-");
                        value.Add(obj.ZakLink[i].Descr);
                        value.Add("-");
                    }
                }
                else
                {
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                    value.Add("-");
                }
                #endregion
            }

            return value;
        }

        /// <summary>
        /// Статистика сводная
        /// </summary>
        public static Stream ExportStatistic()
        {
            ExcelFile excelTemplate = new ExcelFile();
            var mainWorkSheet = excelTemplate.Worksheets.Add("Статистика сводная");
            List<ObjectModel.Sud.OMObject> objs = ObjectModel.Sud.OMObject.Where().SelectAll().Execute();

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };
            int curIndex = 0;
            Parallel.ForEach(objs, options, obj =>
            {
                curIndex++;
                if (curIndex % 40 == 0) Console.WriteLine(curIndex);
                obj.ZakLink = ObjectModel.Sud.OMZakLink.Where(x => x.IdObject == obj.Id).SelectAll().Execute();
                foreach (ObjectModel.Sud.OMZakLink zak in obj.ZakLink)
                {
                    zak.Zak= ObjectModel.Sud.OMZak.Where(x => x.Id == zak.IdZak).SelectAll().ExecuteFirstOrDefault();
                }
                obj.OtchetLink = ObjectModel.Sud.OMOtchetLink.Where(x => x.IdObject == obj.Id).SelectAll().Execute();
            });




            int count1_old = 0;
            int count1_new = 0;
            int count2_old = 0;
            int count2_new = 0;
            int count3_old = 0;
            int count3_new = 0;
            int count4_old = 0;
            int count4_new = 0;
            int count5_old = 0;
            int count5_new = 0;
            int count6_old = 0;
            int count6_new = 0;
            int count7_old = 0;
            int count7_new = 0;
            int count8_old = 0;
            int count8_new = 0;
            int count9_old = 0;
            int count9_new = 0;
            List<long> id_otchets = new List<long>();
            List<long> id_zaks = new List<long>();

            int cc = 0;
            foreach (ObjectModel.Sud.OMObject obj in objs)
            {
                cc++;
                if (cc % 40 == 0) Console.WriteLine(cc);
                if (obj.Date.ParseToDateTime().Date == new DateTime(2018, 1, 1).Date || obj.Date.ParseToDateTime().Date >= new DateTime(2019, 1, 1).Date)
                {
                    count1_new++;


                    foreach (ObjectModel.Sud.OMOtchetLink otch in obj.OtchetLink)
                    {
                        if (otch.IdOtchet != null)
                            if (id_otchets.IndexOf(otch.IdOtchet.ParseToLong()) < 0)
                            {
                                id_otchets.Add(otch.IdOtchet.ParseToLong());
                                count2_new++;
                            }
                    }


                    foreach (ObjectModel.Sud.OMZakLink zak in obj.ZakLink)
                    {
                        if (zak.IdZak != null)
                            if (id_zaks.IndexOf(zak.IdZak.ParseToLong()) < 0)
                            {
                                id_zaks.Add(zak.IdZak.ParseToLong());
                                count3_new++;

                                if (zak.Zak != null)
                                {
                                    if (zak.Zak.RecSoglas==1) count4_new++; else count5_new++;
                                    if (zak.Zak.RecBefore==1) count6_new++; else count7_new++;
                                    if (zak.Zak.RecAfter==1) count8_new++; else count9_new++;
                                }

                            }
                    }
                }
                else
                {
                    count1_old++;

                    foreach (ObjectModel.Sud.OMOtchetLink otch in obj.OtchetLink)
                    {
                        if (otch.IdOtchet != null)
                            if (id_otchets.IndexOf(otch.IdOtchet.ParseToLong()) < 0)
                            {
                                id_otchets.Add(otch.IdOtchet.ParseToLong());
                                count2_old++;
                            }
                    }


                    foreach (ObjectModel.Sud.OMZakLink zak in obj.ZakLink)
                    {
                        if (zak.IdZak != null)
                            if (id_zaks.IndexOf(zak.IdZak.ParseToLong()) < 0)
                            {
                                id_zaks.Add(zak.IdZak.ParseToLong());
                                count3_old++;

                                if (zak.Zak != null)
                                {
                                    if (zak.Zak.RecSoglas == 1) count4_old++; else count5_old++;
                                    if (zak.Zak.RecBefore == 1) count6_old++; else count7_old++;
                                    if (zak.Zak.RecAfter == 1) count8_old++; else count9_old++;
                                }

                            }
                    }


                }
            }

            object[] captions = new string[] { "Статистика СПО СУДЫ", "Всего", "Дата определения стоимости: все, кроме 01.01.2018, 01.01.2019 и позднее", "Дата определения стоимости: 01.01.2018, 01.01.2019 и позднее" };
            AddRow(mainWorkSheet, 0, captions, new int[] { 5600, 5600, 5600, 5600}, true, true, true);


            object[] row1 = new object[] { "Количество объектов недвижимости", count1_new + count1_old, count1_old, count1_new };
            object[] row2 = new object[] { "Количество отчетов об оценке", count2_new + count2_old, count2_old, count2_new };
            object[] row3 = new object[] { "Количество заключений", count3_new + count3_old, count3_old, count3_new };
            object[] row4 = new object[] { "Рассмотренно с Ковалевым Д.В. (ДА)", count4_new + count4_old, count4_old, count4_new };
            object[] row5 = new object[] { "Рассмотренно с Ковалевым Д.В. (НЕТ)", count5_new + count5_old, count5_old, count5_new };
            object[] row6 = new object[] { "Предварительная рецензия (ДА)", count6_new + count6_old, count6_old, count6_new };
            object[] row7 = new object[] { "Предварительная рецензия (НЕТ)", count7_new + count7_old, count7_old, count7_new };
            object[] row8 = new object[] { "Рецензия после анализа (ДА)", count8_new + count8_old, count8_old, count8_new };
            object[] row9 = new object[] { "Рецензия после анализа (НЕТ)", count9_new + count9_old, count9_old, count9_new };

            AddRow(mainWorkSheet, 1, row1);
            AddRow(mainWorkSheet, 2, row2);
            AddRow(mainWorkSheet, 3, row3);
            AddRow(mainWorkSheet, 4, row4);
            AddRow(mainWorkSheet, 5, row5);
            AddRow(mainWorkSheet, 6, row6);
            AddRow(mainWorkSheet, 7, row7);
            AddRow(mainWorkSheet, 8, row8);
            AddRow(mainWorkSheet, 9, row9);

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        /// <summary>
        /// Статискика по объектам недвидимости
        /// </summary>
        public static Stream ExportStatisticObject()
        {
            ExcelFile excelTemplate = new ExcelFile();
            var mainWorkSheet = excelTemplate.Worksheets.Add("Статискика по объектам");
            List<ObjectModel.Sud.OMObject> objs = ObjectModel.Sud.OMObject.Where().SelectAll().Execute();

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };
            int curIndex = 0;
            Parallel.ForEach(objs, options, obj =>
            {
                curIndex++;
                if (curIndex % 40 == 0) Console.WriteLine(curIndex);
                obj.SudLink = ObjectModel.Sud.OMSudLink.Where(x => x.IdObject == obj.Id).SelectAll().Execute();
                foreach (ObjectModel.Sud.OMSudLink link in obj.SudLink)
                {
                   link.Sud = ObjectModel.Sud.OMSud.Where(x => x.Id == link.IdSud).SelectAll().ExecuteFirstOrDefault();
                }

            });




            int zu_stop = 0;
            int zu_no = 0;
            int zu_yes = 0;
            int zu_none = 0;
            int oks_stop = 0;
            int oks_no = 0;
            int oks_yes = 0;
            int oks_none = 0;
            int bld_stop = 0;
            int bld_no = 0;
            int bld_yes = 0;
            int bld_none = 0;
            int flt_stop = 0;
            int flt_no = 0;
            int flt_yes = 0;
            int flt_none = 0;



            int cc = 0;
            foreach (ObjectModel.Sud.OMObject obj in objs)
            {
                cc++;
                if (cc % 40 == 0) Console.WriteLine(cc);

                switch (obj.Typeobj_Code)
                {
                    case ObjectModel.Directory.Sud.SudObjectType.None:
                        break;
                    case ObjectModel.Directory.Sud.SudObjectType.Site:
                        foreach (ObjectModel.Sud.OMSudLink link in obj.SudLink)
                        {
                            if (link.Sud != null)
                            {
                                if (link.Sud.Status == 0) zu_none++;
                                if (link.Sud.Status == 1) zu_yes++;
                                if (link.Sud.Status == 2) zu_no++;
                                if (link.Sud.Status == 3) zu_stop++;
                            }
                        }
                        break;
                    case ObjectModel.Directory.Sud.SudObjectType.Building:
                        foreach (ObjectModel.Sud.OMSudLink link in obj.SudLink)
                        {
                            if (link.Sud != null)
                            {
                                if (link.Sud.Status == 0) bld_none++;
                                if (link.Sud.Status == 1) bld_yes++;
                                if (link.Sud.Status == 2) bld_no++;
                                if (link.Sud.Status == 3) bld_stop++;
                            }
                        }
                        break;
                    case ObjectModel.Directory.Sud.SudObjectType.Room:
                        foreach (ObjectModel.Sud.OMSudLink link in obj.SudLink)
                        {
                            if (link.Sud != null)
                            {
                                if (link.Sud.Status == 0) flt_none++;
                                if (link.Sud.Status == 1) flt_yes++;
                                if (link.Sud.Status == 2) flt_no++;
                                if (link.Sud.Status == 3) flt_stop++;
                            }
                        }
                        break;
                    case ObjectModel.Directory.Sud.SudObjectType.Construction:
                    case ObjectModel.Directory.Sud.SudObjectType.Ons:
                    case ObjectModel.Directory.Sud.SudObjectType.ParkingPlace:
                        foreach (ObjectModel.Sud.OMSudLink link in obj.SudLink)
                        {
                            if (link.Sud != null)
                            {
                                if (link.Sud.Status == 0) oks_none++;
                                if (link.Sud.Status == 1) oks_yes++;
                                if (link.Sud.Status == 2) oks_no++;
                                if (link.Sud.Status == 3) oks_stop++;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            oks_none += (flt_none + bld_none);
            oks_yes += (flt_yes + bld_yes);
            oks_no += (flt_no + bld_no);
            oks_stop += (flt_stop + bld_stop);


            object[] captions = new string[] { "Тип Объекта недвижимости", "Приостановлено", "Отказано", "Удовлетворено", "Без статуса", "ИТОГО рассмотренных" };
            object[] row1 = new object[] { "Земельный участок", zu_stop, zu_no, zu_yes, zu_none, zu_no + zu_yes };
            object[] row2 = new object[] { "ОКС", oks_stop, oks_no, oks_yes, oks_none, oks_no + oks_yes };
            object[] row3 = new object[] { "в т.ч. Здание", bld_stop, bld_no, bld_yes, bld_none, bld_no + bld_yes };
            object[] row4 = new object[] { "в т.ч. Помещение", flt_stop, flt_no, flt_yes, flt_none, flt_no + flt_yes };
            object[] row5 = new object[] { "ВСЕГО", zu_stop + oks_stop, zu_no + oks_no, zu_yes + oks_yes, zu_none + oks_none, zu_no + zu_yes + oks_no + oks_yes };

            AddRow(mainWorkSheet, 0, captions);
            AddRow(mainWorkSheet, 1, row1);
            AddRow(mainWorkSheet, 2, row2);
            AddRow(mainWorkSheet, 3, row3);
            AddRow(mainWorkSheet, 4, row4);
            AddRow(mainWorkSheet, 5, row5);

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }


    }
}
