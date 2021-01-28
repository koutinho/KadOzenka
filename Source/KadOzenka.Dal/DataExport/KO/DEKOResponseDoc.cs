using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using GemBox.Document;
using GemBox.Document.Tables;
using GemBox.Spreadsheet;
using Ionic.Zip;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;
using SaveOptions = GemBox.Document.SaveOptions;

namespace KadOzenka.Dal.DataExport
{
    /// <summary>
    /// Класс выгрузки в XML результатов Кадастровой оценки по исходящим документам.
    /// </summary>
    public class DEKOResponseDoc : IKoUnloadResult
    {
        /// <summary>
        /// Экспорт в Xml - КОценка по исходящим документам.
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadDEKOResponseDocExportToXml)]
        public static List<ResultKoUnloadSettings> ExportToXml(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            var progressMessage = "Выгрузка в XML результатов Кадастровой оценки по исходящим документам";
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var _doc = OMInstance.Where(x => x.Id == setting.IdResponseDocument).SelectAll().ExecuteFirstOrDefault();
            var result = new List<ResultKoUnloadSettings>();
            List<OMUnit> units = OMUnit.Where(x => x.ResponseDocId == _doc.Id && x.CadastralCost > 0).SelectAll().Execute();
            if (units.Count == 0)
            {
                setProgress(100, true, progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
                return result;
            }

            List<ActOpredel> list_act = new List<ActOpredel>();
            List<OMInstance> list_doc_in = new List<OMInstance>();
            List<string> list_bads = new List<string>();
            using (ZipFile zipFile = new ZipFile())
            {
                zipFile.AlternateEncoding = Encoding.UTF8;
                zipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

                int num_pp = 0;
                list_bads.AddRange(CalcXMLResponseDoc(ref num_pp, units, _doc, out List<ActOpredel> list_act_out, out List<OMInstance> list_doc_out, setting.DirectoryName, zipFile));
                setProgress(20, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 20);
                list_doc_out.ForEach(x => { if (list_doc_in.Find(y => y.Id == x.Id) == null) list_doc_in.Add(x); });
                setProgress(40, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 40);
                list_act.AddRange(list_act_out);
                setProgress(80, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 80);
                ObjectNotChange(list_bads, list_act, list_doc_in, setting.DirectoryName, zipFile);
                setProgress(90, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 90);

                MemoryStream stream = new MemoryStream();
                zipFile.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var fileName = $"Выгрузка XML результатов Кадастровой оценки по исходящим документам ({ _doc.RegNumber} { _doc.Description})";
                long id = SaveReportDownload.SaveReport(fileName, stream, OMUnit.GetRegisterId(), reportExtension: "zip");
                var fileResult = new ResultKoUnloadSettings
                {
                    FileId = id,
                    FileName = fileName,
                    TaskId = units.FirstOrDefault().TaskId.GetValueOrDefault()
                };
                result.Add(fileResult);
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return result;
        }

        private static string[] CalcXMLResponseDoc(ref int _num_pp, List<OMUnit> _units, OMInstance _doc_out,
            out List<ActOpredel> _list_act,
            out List<OMInstance> _list_doc_in,
            string _dir_name, ZipFile zipFile)
        {
            List<string> bads = new List<string>();
            _list_act = new List<ActOpredel>();
            _list_doc_in = new List<OMInstance>();
            Dictionary<Int64, XmlNode> dictNodes = new Dictionary<long, XmlNode>();

            foreach (OMUnit unit in _units)
            {
                #region Нашли и записали в список входящий документ
                OMInstance doc_in = new OMInstance();
                OMTask task = OMTask.Where(x => x.Id == unit.TaskId).SelectAll().ExecuteFirstOrDefault();
                if (task != null)
                {
                    doc_in = OMInstance.Where(x => x.Id == task.DocumentId).SelectAll().ExecuteFirstOrDefault();
                    if (_list_doc_in.Find(x => x.Id == doc_in.Id) == null)
                        _list_doc_in.Add(doc_in);
                }
                #endregion

                ActOpredel act_opredel = new ActOpredel();
                act_opredel.kn = unit.CadastralNumber;
                act_opredel.code = "-";
                string str_attr = "";
                if (DataExportCommon.GetObjectAttribute(unit, 15, out str_attr)) //TODO уточнить номер атрибута
                    act_opredel.code = str_attr;

                if (unit.GroupId > 0)
                {
                    OMGroup group_unit = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                    act_opredel.act_dop = group_unit.AssumptionsReference;
                    act_opredel.kc = unit.CadastralCost;
                    act_opredel.act_model = group_unit.CadastralCostEstimationModelsReferences;
                    act_opredel.osnovanie = unit.Status;
                    act_opredel.act_other = group_unit.OtherCostRelatedInfo;
                    act_opredel.subgroup = group_unit.GroupName;
                    _list_act.Add(act_opredel);

                    if (DataExportCommon.GetObjectLastByKN(unit))
                    {
                        _num_pp++;
                        XmlDocument xmlFile = new XmlDocument();
                        XmlNode xnLandValuation = xmlFile.CreateElement("LandValuation");
                        xmlFile.AppendChild(xnLandValuation);
                        DEKOUnit.AddXmlDocument(xmlFile, xnLandValuation, true, _doc_out.Description, _doc_out.RegNumber, _doc_out.CreateDate, doc_in.CreateDate,
                            ConfigurationManager.AppSettings["ucSender"], DateTime.Now);
                        DEKOUnit.AddXmlPackage(xmlFile, xnLandValuation, new List<OMUnit> { unit });

                        string fileName = unit.CadastralNumber.Replace(":", "_") +
                                          "\\COST_" + ConfigurationManager.AppSettings["ucSender"] +
                                          "_" + doc_in.CreateDate.ToString("ddMMyyyy") +
                                          "_" + DateTime.Now.ToString("ddMMyyyy") +
                                          "_" + ((int)unit.PropertyType_Code).ToString() +
                                          "_" + unit.Id.ToString() + ".xml";
                        MemoryStream stream = new MemoryStream();
                        xmlFile.Save(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        zipFile.AddEntry(fileName, stream);

                        XmlVuonExport(group_unit, unit, "", doc_in, _doc_out, dictNodes, zipFile);
                        XmlWebExport(group_unit, unit, "", doc_in, _doc_out, zipFile);
                        GetOtvetDocX(unit, "", _doc_out, _num_pp, zipFile);
                        bads.Add("g|" + unit.CadastralNumber + "|" + ((unit.CadastralCost == null) ? 0 : (decimal)unit.CadastralCost).ToString("0.00"));
                    }
                    else
                    {
                        _num_pp++;
                        XmlDocument xmlFile = new XmlDocument();
                        XmlNode xnLandValuation = xmlFile.CreateElement("LandValuation");
                        xmlFile.AppendChild(xnLandValuation);
                        DEKOUnit.AddXmlDocument(xmlFile, xnLandValuation, true, _doc_out.Description, _doc_out.RegNumber, _doc_out.CreateDate, doc_in.CreateDate,
                            ConfigurationManager.AppSettings["ucSender"], DateTime.Now);
                        DEKOUnit.AddXmlPackage(xmlFile, xnLandValuation, new List<OMUnit> { unit });

                        string fileName = unit.CadastralNumber.Replace(":", "_") +
                                          "\\COST_" + ConfigurationManager.AppSettings["ucSender"] +
                                          "_" + doc_in.CreateDate.ToString("ddMMyyyy") +
                                          "_" + DateTime.Now.ToString("ddMMyyyy") +
                                          "_" + ((int)unit.PropertyType_Code).ToString() +
                                          "_" + unit.Id.ToString() + ".xml";
                        MemoryStream stream = new MemoryStream();
                        xmlFile.Save(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        zipFile.AddEntry(fileName, stream);

                        XmlVuonExport(group_unit, unit, "", doc_in, _doc_out, dictNodes, zipFile);
                        GetOtvetDocX(unit, "", _doc_out, _num_pp, zipFile);
                        bads.Add("g|" + unit.CadastralNumber + "|" + ((unit.CadastralCost == null) ? 0 : (decimal)unit.CadastralCost).ToString("0.00"));

                        string dateapp = "01.01.2018";
                        OMTask task_old = OMTask.Where(x => x.Id == unit.TaskId).SelectAll().ExecuteFirstOrDefault();
                        if (task_old != null)
                        {
                            OMInstance doc_old = OMInstance.Where(x => x.Id == task_old.DocumentId).SelectAll().ExecuteFirstOrDefault();
                            if (doc_old != null) dateapp = doc_old.CreateDate.ToString("dd.MM.yyyy");
                        }
                        bads.Add("b|" + unit.CadastralNumber + "|" + dateapp);
                    }
                }
            }

            return bads.ToArray();
        }

        public static void ObjectNotChange(List<string> list_bads, List<ActOpredel> _list_act, List<OMInstance> _list_doc, string _dir_name, ZipFile zipFile)
        {
            List<string> baditems = new List<string>();
            List<string> gooditems = new List<string>();
            foreach (string item in list_bads)
            {
                if (item.IndexOf("b|") == 0) baditems.Add(item);
                if (item.IndexOf("g|") == 0) gooditems.Add(item);
            }
             
            #region Акт_об_определении_КС - без изменений
            {
                ExcelFile excelTemplate = new ExcelFile();
                var mainWorkSheet = excelTemplate.Worksheets.Add("КС");
                int curcount = 2;
                int curcounti = 2;

                List<object> objcaps = new List<object>();
                objcaps.Add("КН");
                objcaps.Add("Дата определения КС");
                int fieldcount = objcaps.Count;
                DataExportCommon.AddRow(mainWorkSheet, 1, objcaps.ToArray());


                int lenobjs = baditems.Count;

                object[,] objvals = new object[100, fieldcount];
                int curindval = 0;
                for (int i = 0; i < lenobjs; i++)
                {
                    List<object> objarrs = new List<object>();
                    string[] arrrec = baditems[i].Split('|');

                    objarrs.Add(arrrec[1]);
                    objarrs.Add(arrrec[2]);

                    #region array
                    for (int f = 0; f < fieldcount; f++)
                    {
                        objvals[curindval, f] = objarrs[f];
                    }

                    if (curindval >= 99)
                    {
                        DataExportCommon.AddRow(mainWorkSheet, curcount - 99, objvals);
                        curindval = -1;
                        objvals = new object[100, fieldcount];
                    }
                    #endregion

                    curindval++;
                    curcount++;
                    curcounti++;

                }
                if (curindval != 0)
                {
                    DataExportCommon.AddRow(mainWorkSheet, curcount - curindval, objvals, curindval); 
                }

                //if (!Directory.Exists(_dir_name + "\\" + "DOC")) Directory.CreateDirectory(_dir_name + "\\" + "DOC");
                //string filenn_temp = _dir_name + "\\" + "DOC" + "\\Акт_об_определении_КС" + DateTime.Now.ToString("ddMMyyyyhhmmss") + "_без_изменений.xlsx";
                //excelTemplate.Save(filenn_temp);

                string fileName = "DOC" + "\\Акт_об_определении_КС" + DateTime.Now.ToString("ddMMyyyyhhmmss") + "_без_изменений.xlsx";
                MemoryStream stream = new MemoryStream();
                excelTemplate.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
                stream.Seek(0, SeekOrigin.Begin);
                zipFile.AddEntry(fileName, stream);
            }
            #endregion

            #region Акт_об_определении_КС - с изменениями
            {
                FileStream fileStream = Core.ConfigParam.Configuration.GetFileStream("ActDeterminingCadastralCost", ".xlsx", "ExcelTemplates");
                ExcelFile excel_edit = ExcelFile.Load(fileStream, GemBox.Spreadsheet.LoadOptions.XlsxDefault);
                var sheet_edit = excel_edit.Worksheets[0];

                int curcount = 3;
                int curcounti = 3;
                int fieldcount = 3;
                int lenobjs = gooditems.Count;

                object[,] objvals = new object[100, fieldcount];
                int curindval = 0;
                for (int i = 0; i < lenobjs; i++)
                {
                    List<object> objarrs = new List<object>();
                    string[] arrrec = gooditems[i].Split('|');
                    objarrs.Add(i + 1);
                    objarrs.Add(arrrec[1]);
                    objarrs.Add(Convert.ToDouble(arrrec[2]));

                    #region array
                    for (int f = 0; f < fieldcount; f++)
                    {
                        objvals[curindval, f] = objarrs[f];
                    }

                    if (curindval >= 99)
                    {
                        DataExportCommon.AddRow(sheet_edit, curcount - 99, objvals);
                        curindval = -1;
                        objvals = new object[100, fieldcount];
                    }
                    #endregion

                    curindval++;
                    curcount++;
                    curcounti++;
                }
                if (curindval != 0)
                {
                    DataExportCommon.AddRow(sheet_edit, curcount - curindval, objvals, curindval);
                }

                //if (!Directory.Exists(_dir_name + "\\" + "DOC")) Directory.CreateDirectory(_dir_name + "\\" + "DOC");
                //string filenn = _dir_name + "\\" + "DOC" + "\\Акт_об_определении_КС" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                //excel_edit.Save(filenn);

                string fileName = "DOC" + "\\Акт_об_определении_КС" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                MemoryStream stream = new MemoryStream();
                excel_edit.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
                stream.Seek(0, SeekOrigin.Begin);
                zipFile.AddEntry(fileName, stream);
            }
            #endregion

            #region Акт_определения_КС_по_МУ_пункт_12_2
            {
                FileStream fileStream = Core.ConfigParam.Configuration.GetFileStream("ActDeterminingCadastralCostMUitem122", ".xlsx", "ExcelTemplates");
                ExcelFile aexcell = ExcelFile.Load(fileStream, GemBox.Spreadsheet.LoadOptions.XlsxDefault);
                var asheet = aexcell.Worksheets[0];

                int acurcount = 10;
                int acurcounti = 10;
                int afieldcount = 9;
                int alenobjs = _list_act.Count;

                string docin = string.Empty;
                _list_doc.ForEach(x => docin += (DataExportCommon.GetFullNameDoc(x) + "; "));
                DataExportCommon.SetCellValueNoBorder(asheet, 1, 7, "Документ-основание для пересчета кадастровой стоимости: " + docin);

                object[,] aobjvals = new object[100, afieldcount];
                int acurindval = 0;
                for (int i = 0; i < alenobjs; i++)
                {
                    List<object> aobjarrs = new List<object>();
                    aobjarrs.Add(i + 1);
                    aobjarrs.Add(_list_act[i].kn);
                    aobjarrs.Add(_list_act[i].osnovanie);
                    aobjarrs.Add(_list_act[i].code);
                    aobjarrs.Add(_list_act[i].subgroup);
                    aobjarrs.Add(_list_act[i].act_model);
                    aobjarrs.Add(_list_act[i].kc);
                    aobjarrs.Add(_list_act[i].act_dop);
                    aobjarrs.Add(_list_act[i].act_other);

                    #region array
                    for (int f = 0; f < afieldcount; f++)
                    {
                        aobjvals[acurindval, f] = aobjarrs[f];
                    }

                    if (acurindval >= 99)
                    {
                        DataExportCommon.AddRow(asheet, acurcount - 99, aobjvals);
                        acurindval = -1;
                        aobjvals = new object[100, afieldcount];
                    }

                    #endregion

                    acurindval++;
                    acurcount++;
                    acurcounti++;
                }
                if (acurindval != 0)
                {
                    DataExportCommon.AddRow(asheet, acurcount - acurindval, aobjvals, acurindval); 
                }

                //if (!Directory.Exists(_dir_name + "\\" + "DOC")) Directory.CreateDirectory(_dir_name + "\\" + "DOC");
                //string afilenn = _dir_name + "\\" + "DOC" + "\\Акт_определения_КС_по_МУ_пункт_12_2" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                //aexcell.Save(afilenn);

                string fileName = "DOC" + "\\Акт_определения_КС_по_МУ_пункт_12_2" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                MemoryStream stream = new MemoryStream();
                aexcell.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
                stream.Seek(0, SeekOrigin.Begin);
                zipFile.AddEntry(fileName, stream);
            }
            #endregion

        }

        public static bool XmlVuonExport(OMGroup _group_unit, OMUnit _unit, string _dir_name,
            OMInstance _doc_in, OMInstance _doc_out,
            Dictionary<Int64, XmlNode> dictNodes, ZipFile zipFile)
        {
            List<OMUnit> units = new List<OMUnit> { _unit };
            _group_unit.Unit = units;
            if (units.Count > 0)
            {
                //int lenobjs = units.Length;
                XmlDocument xmlFile = new XmlDocument();
                XmlNode xnLandValuation = xmlFile.CreateElement("FD_State_Cadastral_Valuation");
                DataExportCommon.AddAttribute(xmlFile, xnLandValuation, "Version", "02");
                xmlFile.AppendChild(xnLandValuation);

                DEKOGroup.AddXmlGeneralInfo(xmlFile, xnLandValuation);
                //"Выгрузка XML для ФД"
                DEKOGroup.AddXmlPackage(xmlFile, xnLandValuation, _group_unit, dictNodes, "",null); //TODO: calcItemDict

                //if (!Directory.Exists(_dir_name))
                //    Directory.CreateDirectory(_dir_name);

                //xmlFile.Save(_dir_name + "\\FD_State_Cadastral_Valuation_" + _group_unit.Id.ToString().PadLeft(5, '0') +
                //    "_" + _doc_in.CreateDate.ToString("ddMMyyyy") +
                //    "_" + ((int)_unit.PropertyType_Code).ToString() +
                //    "_" + _unit.Id.ToString() + ".xml");

                string file_name_prev = (CheckNullEmpty.CheckString(_dir_name) == "") ? "" : _dir_name + "\\";
                string fileName = file_name_prev + _unit.CadastralNumber.Replace(":", "_") +
                                  "\\FD_State_Cadastral_Valuation_" +
                                  _group_unit.Id.ToString().PadLeft(5, '0') +
                                  "_" + _doc_in.CreateDate.ToString("ddMMyyyy") +
                                  "_" + ((int)_unit.PropertyType_Code).ToString() +
                                  "_" + _unit.Id.ToString() + ".xml";
                MemoryStream stream = new MemoryStream();
                xmlFile.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                zipFile.AddEntry(fileName, stream);
            }

            return true;
        }

        public static bool XmlWebExport(OMGroup _group_unit, OMUnit _unit, string _dir_name, OMInstance _doc_in, OMInstance _doc_out, ZipFile zipFile)
        {
            if (_unit == null) return false;
            XmlDocument xmlFile = new XmlDocument();
            XmlNode xnLandValuation = xmlFile.CreateElement("WebExport");
            DataExportCommon.AddAttribute(xmlFile, xnLandValuation, "Version", "01");
            XmlNode xnobject = xmlFile.CreateElement("object");
            {
                XmlNode xnkn = xmlFile.CreateElement("kn"); xnkn.InnerText = _unit.CadastralNumber; xnobject.AppendChild(xnkn);
                XmlNode xntype = xmlFile.CreateElement("type"); xntype.InnerText = (_unit.PropertyType_Code == PropertyTypes.Stead) ? "0" : "1"; xnobject.AppendChild(xntype);
                XmlNode xnyear = xmlFile.CreateElement("year"); xnyear.InnerText = "2019"; xnobject.AppendChild(xnyear);
                XmlNode xnactual = xmlFile.CreateElement("actual"); xnactual.InnerText = "1"; xnobject.AppendChild(xnactual);

                XmlNode xnvalue_01 = xmlFile.CreateElement("value_01"); xnvalue_01.InnerText = _unit.CadastralNumber    ; xnobject.AppendChild(xnvalue_01);
                XmlNode xnvalue_02 = xmlFile.CreateElement("value_02"); xnvalue_02.InnerText = _unit.CadastralBlock        ; xnobject.AppendChild(xnvalue_02);
                XmlNode xnvalue_03 = xmlFile.CreateElement("value_03"); xnvalue_03.InnerText = _unit.PropertyType; xnobject.AppendChild(xnvalue_03);
                string value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 600, out value_attr); //Код 600 - Адрес 
                XmlNode xnvalue_04 = xmlFile.CreateElement("value_04"); xnvalue_04.InnerText = value_attr; xnobject.AppendChild(xnvalue_04);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 8, out value_attr); //Код 8 - Местоположение
                XmlNode xnvalue_05 = xmlFile.CreateElement("value_05"); xnvalue_05.InnerText = value_attr; xnobject.AppendChild(xnvalue_05);
                XmlNode xnvalue_06 = xmlFile.CreateElement("value_06"); xnvalue_06.InnerText = NullConvertorMS.DecimaltoSTR(_unit.Square); xnobject.AppendChild(xnvalue_06);
                if (_unit.PropertyType_Code != PropertyTypes.Stead)
                {   // Все объекты оценки, кроме земельных участков
                    XmlNode xnvalue_07 = xmlFile.CreateElement("value_07"); xnvalue_07.InnerText = string.Empty; xnobject.AppendChild(xnvalue_07);
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_unit, 19, out value_attr); //Код 19 - Наименование объекта
                    XmlNode xnvalue_08 = xmlFile.CreateElement("value_08"); xnvalue_08.InnerText = value_attr; xnobject.AppendChild(xnvalue_08);
                    XmlNode xnvalue_09 = xmlFile.CreateElement("value_09"); xnvalue_09.InnerText = string.Empty; xnobject.AppendChild(xnvalue_09);
                    XmlNode xnvalue_10 = xmlFile.CreateElement("value_10"); xnvalue_10.InnerText = string.Empty; xnobject.AppendChild(xnvalue_10);
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_unit, 16, out value_attr); //Код 16 - Год ввода в эксплуатацию
                    if (value_attr == "")
                        DataExportCommon.GetObjectAttribute(_unit, 15, out value_attr); //Код 15 - Год постройки
                    XmlNode xnvalue_11 = xmlFile.CreateElement("value_11"); xnvalue_11.InnerText = value_attr; xnobject.AppendChild(xnvalue_11);
                    value_attr = "";
                    // Определяем значения кода аттрибута по типу объекта
                    long number_attr = -1;
                    if (_unit.PropertyType_Code == PropertyTypes.Building) number_attr = 14;
                    else if (_unit.PropertyType_Code == PropertyTypes.Construction) number_attr = 22;
                    else if (_unit.PropertyType_Code == PropertyTypes.Pllacement) number_attr = 23;
                    if (number_attr > 0)
                        DataExportCommon.GetObjectAttribute(_unit, number_attr, out value_attr); //Код  - Назначение
                    XmlNode xnvalue_12 = xmlFile.CreateElement("value_12"); xnvalue_12.InnerText = value_attr; xnobject.AppendChild(xnvalue_12);
                    XmlNode xnvalue_13 = xmlFile.CreateElement("value_13"); xnvalue_13.InnerText = string.Empty; xnobject.AppendChild(xnvalue_13);
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_unit, 21, out value_attr); //Код 21 - Материал стен
                    XmlNode xnvalue_14 = xmlFile.CreateElement("value_14"); xnvalue_14.InnerText = value_attr; xnobject.AppendChild(xnvalue_14);
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_unit, 17, out value_attr); //Код 17 - Количество этажей
                    XmlNode xnvalue_15 = xmlFile.CreateElement("value_15"); xnvalue_15.InnerText = value_attr; xnobject.AppendChild(xnvalue_15);

                    if (_unit.PropertyType_Code == PropertyTypes.Pllacement)
                    {
                        value_attr = "";
                        DataExportCommon.GetObjectAttribute(_unit, 604, out value_attr); //Код 604 - Кадастровый номер здания или сооружения, в котором расположено помещение
                        if (value_attr == "")
                            DataExportCommon.GetObjectAttribute(_unit, 605, out value_attr); //Код 605 - Кадастровый номер квартиры, в которой расположена комната
                        XmlNode xnvalue_16 = xmlFile.CreateElement("value_16"); xnvalue_16.InnerText = value_attr; xnobject.AppendChild(xnvalue_16);
                        XmlNode xnvalue_17 = xmlFile.CreateElement("value_17"); xnvalue_17.InnerText = string.Empty; xnobject.AppendChild(xnvalue_17);
                    }
                    else
                    {
                        XmlNode xnvalue_16 = xmlFile.CreateElement("value_16"); xnvalue_16.InnerText = string.Empty; xnobject.AppendChild(xnvalue_16);
                        value_attr = "";
                        DataExportCommon.GetObjectAttribute(_unit, 602, out value_attr); //Код 602 - Земельный участок
                        XmlNode xnvalue_17 = xmlFile.CreateElement("value_17"); xnvalue_17.InnerText = value_attr; xnobject.AppendChild(xnvalue_17);
                    }
                }
                else
                {   // Земельные участки
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_unit, 3, out value_attr); //Код 3 - Категория земель
                    XmlNode xnvalue_07 = xmlFile.CreateElement("value_07"); xnvalue_07.InnerText = value_attr; xnobject.AppendChild(xnvalue_07);
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_unit, 1, out value_attr); //Код 1 - Наименование участка
                    XmlNode xnvalue_08 = xmlFile.CreateElement("value_08"); xnvalue_08.InnerText = value_attr; xnobject.AppendChild(xnvalue_08);
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_unit, 4, out value_attr); //Код 4 - Вид использования по документам
                    XmlNode xnvalue_09 = xmlFile.CreateElement("value_09"); xnvalue_09.InnerText = value_attr; xnobject.AppendChild(xnvalue_09);
                    XmlNode xnvalue_10 = xmlFile.CreateElement("value_10"); xnvalue_10.InnerText = string.Empty; xnobject.AppendChild(xnvalue_10);

                    XmlNode xnvalue_11 = xmlFile.CreateElement("value_11"); xnvalue_11.InnerText = string.Empty; xnobject.AppendChild(xnvalue_11);
                    XmlNode xnvalue_12 = xmlFile.CreateElement("value_12"); xnvalue_12.InnerText = string.Empty; xnobject.AppendChild(xnvalue_12);
                    XmlNode xnvalue_13 = xmlFile.CreateElement("value_13"); xnvalue_13.InnerText = string.Empty; xnobject.AppendChild(xnvalue_13);
                    XmlNode xnvalue_14 = xmlFile.CreateElement("value_14"); xnvalue_14.InnerText = string.Empty; xnobject.AppendChild(xnvalue_14);
                    XmlNode xnvalue_15 = xmlFile.CreateElement("value_15"); xnvalue_15.InnerText = string.Empty; xnobject.AppendChild(xnvalue_15);
                    XmlNode xnvalue_16 = xmlFile.CreateElement("value_16"); xnvalue_16.InnerText = string.Empty; xnobject.AppendChild(xnvalue_16);
                    XmlNode xnvalue_17 = xmlFile.CreateElement("value_17"); xnvalue_17.InnerText = string.Empty; xnobject.AppendChild(xnvalue_17);
                }

                XmlNode xnvalue_18 = xmlFile.CreateElement("value_18"); xnvalue_18.InnerText = NullConvertorMS.DecimaltoSTR(_unit.Upks); xnobject.AppendChild(xnvalue_18);
                XmlNode xnvalue_19 = xmlFile.CreateElement("value_19"); xnvalue_19.InnerText = NullConvertorMS.DecimaltoSTR(_unit.CadastralCost); xnobject.AppendChild(xnvalue_19);
                XmlNode xnvalue_20 = xmlFile.CreateElement("value_20"); xnvalue_20.InnerText = DataExportCommon.GetFullNameDoc(_doc_out); xnobject.AppendChild(xnvalue_20);
                XmlNode xnvalue_21 = xmlFile.CreateElement("value_21"); xnvalue_21.InnerText = DataExportCommon.GetFullNameGroup(_group_unit); xnobject.AppendChild(xnvalue_21);
                XmlNode xnvalue_22 = xmlFile.CreateElement("value_22"); xnvalue_22.InnerText = DataExportCommon.GetShortNameDoc(_doc_in); xnobject.AppendChild(xnvalue_22);
                XmlNode xnvalue_23 = xmlFile.CreateElement("value_23"); xnvalue_23.InnerText = NullConvertorMS.DTtoSTR(_doc_in.CreateDate); xnobject.AppendChild(xnvalue_23);
                XmlNode xnvalue_24 = xmlFile.CreateElement("value_24"); xnvalue_24.InnerText = DataExportCommon.GetShortNameDoc(_doc_in); xnobject.AppendChild(xnvalue_24);

                XmlNode xnvalue_25 = xmlFile.CreateElement("value_25"); xnvalue_25.InnerText = String.Empty; xnobject.AppendChild(xnvalue_25);
                XmlNode xnvalue_26 = xmlFile.CreateElement("value_26"); xnvalue_26.InnerText = String.Empty; xnobject.AppendChild(xnvalue_26);
                XmlNode xnvalue_27 = xmlFile.CreateElement("value_27"); xnvalue_27.InnerText = String.Empty; xnobject.AppendChild(xnvalue_27);
                XmlNode xnvalue_28 = xmlFile.CreateElement("value_28"); xnvalue_28.InnerText = String.Empty; xnobject.AppendChild(xnvalue_28);
                XmlNode xnvalue_29 = xmlFile.CreateElement("value_29"); xnvalue_29.InnerText = String.Empty; xnobject.AppendChild(xnvalue_29);
                XmlNode xnvalue_30 = xmlFile.CreateElement("value_30"); xnvalue_30.InnerText = String.Empty; xnobject.AppendChild(xnvalue_30);
                XmlNode xnvalue_31 = xmlFile.CreateElement("value_31"); xnvalue_31.InnerText = String.Empty; xnobject.AppendChild(xnvalue_31);
                XmlNode xnvalue_32 = xmlFile.CreateElement("value_32"); xnvalue_32.InnerText = String.Empty; xnobject.AppendChild(xnvalue_32);
                XmlNode xnvalue_33 = xmlFile.CreateElement("value_33"); xnvalue_33.InnerText = String.Empty; xnobject.AppendChild(xnvalue_33);
                XmlNode xnvalue_34 = xmlFile.CreateElement("value_34"); xnvalue_34.InnerText = String.Empty; xnobject.AppendChild(xnvalue_34);
                XmlNode xnvalue_35 = xmlFile.CreateElement("value_35"); xnvalue_35.InnerText = String.Empty; xnobject.AppendChild(xnvalue_35);
                XmlNode xnvalue_36 = xmlFile.CreateElement("value_36"); xnvalue_36.InnerText = String.Empty; xnobject.AppendChild(xnvalue_36);
                XmlNode xnvalue_37 = xmlFile.CreateElement("value_37"); xnvalue_37.InnerText = String.Empty; xnobject.AppendChild(xnvalue_37);
                XmlNode xnvalue_38 = xmlFile.CreateElement("value_38"); xnvalue_38.InnerText = String.Empty; xnobject.AppendChild(xnvalue_38);
                XmlNode xnvalue_39 = xmlFile.CreateElement("value_39"); xnvalue_39.InnerText = String.Empty; xnobject.AppendChild(xnvalue_39);
                XmlNode xnvalue_40 = xmlFile.CreateElement("value_40"); xnvalue_40.InnerText = String.Empty; xnobject.AppendChild(xnvalue_40);
                XmlNode xnvalue_41 = xmlFile.CreateElement("value_41"); xnvalue_41.InnerText = String.Empty; xnobject.AppendChild(xnvalue_41);
            }
            xnLandValuation.AppendChild(xnobject);
            xmlFile.AppendChild(xnLandValuation);

            //if (!Directory.Exists(_dir_name))
            //    Directory.CreateDirectory(_dir_name);
            //if (!Directory.Exists(_dir_name + "\\" + "_web_obmen_"))
            //    Directory.CreateDirectory(_dir_name + "\\" + "_web_obmen_");
            //xmlFile.Save(_dir_name + "\\" + "_web_obmen_" + "\\" + _unit.CadastralNumber.Replace(":", "_") + ".xml");

            string file_name_prev = (CheckNullEmpty.CheckString(_dir_name) == "") ? "" : _dir_name + "\\";
            string fileName = file_name_prev + "_web_obmen_" + "\\" + _unit.CadastralNumber.Replace(":", "_") + ".xml";
            MemoryStream stream = new MemoryStream();
            xmlFile.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            zipFile.AddEntry(fileName, stream);

            return true;
        }

        public static void GetOtvetDocX(OMUnit _unit, string _dir_name, OMInstance _doc_out, int numpp, ZipFile zipFile)
        {
            string num = _doc_out.RegNumber;
            num = num.Replace("/", "");
            string kn = _unit.CadastralNumber;
            kn = kn.Replace(":", "_");
            string file_name_prev = (CheckNullEmpty.CheckString(_dir_name) == "") ? "" : _dir_name + "\\";
            string fileName = file_name_prev + "Акты по объектам" + "\\" + num + "_" + kn + ".docx";


            ComponentInfo.SetLicense("DN-2020Feb27-7KwYZ43Y+lJR5YBeTLWW8F+pXE9Aj3uU2ru+Jk1lHxILYWKJhT8TZQLCztE1qx6MQx/MnAR8BGGPC6QpAmIgm2EZh0w==A");
            var document = new DocumentModel();
            document.DefaultParagraphFormat.SpaceAfter = 0;

            int count_rows = 5;
            int count_cells = 3;

            // Создание колонок (ячеек) в строке (Rows) таблицы
            var table = new Table(document);
            table.Columns.Add(new TableColumn(96f / 2.54f * 2.0f));
            table.Columns.Add(new TableColumn(96f / 2.54f * 5.0f));
            table.Columns.Add(new TableColumn(96f / 2.54f * 8.0f));
            table.Rows.Add(new TableRow(document, new TableCell(document) { ColumnSpan = 3 }));
            table.Rows.Add(new TableRow(document, new TableCell(document) { ColumnSpan = 3 }));
            table.Rows.Add(new TableRow(document, new TableCell(document) { ColumnSpan = 3 }));
            table.TableFormat.Alignment = HorizontalAlignment.Center;

            for (int i = 3; i < count_rows; i++)
            {
                table.Rows.Add(new TableRow(document));
                for (int j = 0; j < count_cells; j++)
                {
                    var cell = new TableCell(document);
                    table.Rows[i].Cells.Add(cell);
                }
            }

            int curCount = 1;
            DataExportCommon.SetTextToCellDoc(document, table.Rows[curCount++ - 1].Cells[0], _doc_out.Description.Replace("Акт определения", "Выписка из акта об определении").Replace("Акт об определении", "Выписка из акта об определении"), 12, HorizontalAlignment.Center, false, false);
            DataExportCommon.SetTextToCellDoc(document, table.Rows[curCount++ - 1].Cells[0], "№" + _doc_out.RegNumber + " от " + NullConvertorMS.DTtoSTR(_doc_out.CreateDate) + " г.", 12, HorizontalAlignment.Center, false, false);
            DataExportCommon.SetTextToCellDoc(document, table.Rows[curCount++ - 1].Cells[0], " ", 6, HorizontalAlignment.Center, false, false);
            DataExportCommon.SetText3Doc(document, table.Rows[curCount++ - 1], "№ п/п", "Кадастровый номер", "Кадастровая стоимость, руб.", 12, HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center, true, false);
            DataExportCommon.SetText3Doc(document, table.Rows[curCount++ - 1], numpp.ToString(), _unit.CadastralNumber, _unit.CadastralCost.ToString(), 12, HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center, true, false);

            // Параметры страницы
            var section = new Section(document, table);
            PageSetup ps = new PageSetup();
            ps.PageHeight = 16838f / 15f;
            ps.PageWidth = 11906f / 15f;
            PageMargins pm = new PageMargins();
            pm.Left = 96f / 2.54f * 2.0f;
            pm.Right = 96f / 2.54f * 1.5f;
            pm.Top = 96f / 2.54f * 2.0f;
            pm.Bottom = 96f / 2.54f * 2.0f;
            ps.PageMargins = pm;
            section.PageSetup = ps;
            document.Sections.Add(section);

            MemoryStream stream = new MemoryStream();
            document.Save(stream, SaveOptions.DocxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            zipFile.AddEntry(fileName, stream);
        }
    }
}