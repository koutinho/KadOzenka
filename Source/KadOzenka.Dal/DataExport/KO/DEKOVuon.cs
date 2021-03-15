using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using GemBox.Spreadsheet;
using Ionic.Zip;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataExport
{
    /// <summary>
    /// Класс выгрузки в XML результатов Кадастровой оценки для ВУОН.
    /// </summary>
    public class DEKOVuon : IKoUnloadResult
    {
        /// <summary>
        /// Экспорт в Xml - КОценка для ВУОН.
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadDEKOVuonExportToXml)]
        public static List<ResultKoUnloadSettings> ExportToXml(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            var progressMessage = "Выгрузки в XML результатов Кадастровой оценки для ВУОН";
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var _doc = OMInstance.Where(x => x.Id == setting.IdResponseDocument).SelectAll().ExecuteFirstOrDefault();
            var result = new List<ResultKoUnloadSettings>();
            List <OMUnit> units = OMUnit.Where(x => x.ResponseDocId == _doc.Id && x.CadastralCost > 0).SelectAll().Execute();
            if (units.Count == 0)
            {
                setProgress(100, true, progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
                return result;
            }
            using (ZipFile zipFile = new ZipFile())
            {
                zipFile.AlternateEncoding = Encoding.UTF8;
                zipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

                List<ActOpredel> list_act = new List<ActOpredel>();
                List<OMInstance> list_doc_in = new List<OMInstance>();
                List<string> list_bads = new List<string>();

                int num_pp = 0;
                list_bads.AddRange(CalcXMLFromVuon(unloadResultQueue.Id, num_pp, units, _doc, out List<ActOpredel> list_act_out, out List<OMInstance> list_doc_out, setting.DirectoryName, zipFile));
                setProgress(20, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 20);
                list_doc_out.ForEach(x => { if (list_doc_in.Find(y => y.Id == x.Id) == null) list_doc_in.Add(x); });
                setProgress(40, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 40);
                list_act.AddRange(list_act_out);
                setProgress(60, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 60);
                ObjectNotChange(list_bads, list_act, list_doc_in, setting.DirectoryName, zipFile);
                setProgress(80, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 80);
                MemoryStream stream = new MemoryStream();
                zipFile.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var fileName = $"Выгрузка XML результатов Кадастровой оценки для ВУОН ({ _doc.RegNumber} { _doc.Description})";
                long id = SaveUnloadResult.SaveResult(fileName, stream, unloadResultQueue.Id, "zip", KoUnloadResultType.UnloadDEKOVuonExportToXml);
                setProgress(90, progressMessage: progressMessage);
                var fileResult = new ResultKoUnloadSettings
                {
                    FileId = id,
                    FileName = fileName,
                    TaskId = units.FirstOrDefault().TaskId.GetValueOrDefault()
                };
                result.Add(fileResult);
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100,true, progressMessage);

            return result;
        }

        private static string[] CalcXMLFromVuon(long unloadId, int _num_pp, List<OMUnit> _units, OMInstance _doc_out,
            out List<ActOpredel> _list_act,
            out List<OMInstance> _list_doc_in,
            string _dir_name, ZipFile zipFile)
        {
            List<string> bads = new List<string>();
            _list_act = new List<ActOpredel>();
            _list_doc_in = new List<OMInstance>();
            Dictionary<Int64, XmlNode> dictNodes = new Dictionary<long, XmlNode>();

            List<OMUnit> units = OMUnit.Where(x => x.ResponseDocId == _doc_out.Id).SelectAll().Execute();
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

                OMGroup group_unit = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                act_opredel.act_dop = group_unit.AssumptionsReference;
                act_opredel.kc = unit.CadastralCost;
                act_opredel.act_model = group_unit.CadastralCostEstimationModelsReferences;
                act_opredel.osnovanie = unit.Status;
                act_opredel.act_other = group_unit.OtherCostRelatedInfo;
                act_opredel.subgroup = group_unit.GroupName;
                _list_act.Add(act_opredel);

                if (unit.GroupId <= 0) return bads.ToArray();
                if (_doc_out.Status != doc_in.Status) return bads.ToArray();
                if (unit.Upks <= 0 && unit.CadastralCost <= 0) return bads.ToArray();

                DateTime estimation_date = (task != null) ? ((task.EstimationDate != null) ? task.EstimationDate.Value : DateTime.Now) : DateTime.Now;
                if (DataExportCommon.GetObjectLastByKN(unit))
                {
                    _num_pp++;
                    XmlDocument xmlFile = new XmlDocument();
                    XmlNode xnLandValuation = xmlFile.CreateElement("LandValuation");
                    xmlFile.AppendChild(xnLandValuation);

                    DEKOUnit.AddXmlDocument(xmlFile, xnLandValuation, true, _doc_out.Description, _doc_out.RegNumber, _doc_out.CreateDate, estimation_date,
                        ConfigurationManager.AppSettings["ucSender"], DateTime.Now);
                    DEKOUnit.AddXmlPackage(xmlFile, xnLandValuation, new List<OMUnit> { unit });

                    string file_name_common = estimation_date.ToString("dd_MM_yyyy");
                    string fileName = file_name_common + "\\" + unit.CadastralNumber.Replace(":", "_") +
                                      "\\COST_" + ConfigurationManager.AppSettings["ucSender"] +
                                      "_" + estimation_date.ToString("ddMMyyyy") +
                                      "_" + DateTime.Now.ToString("ddMMyyyy") +
                                      "_" + ((int)unit.PropertyType_Code).ToString() +
                                      "_" + unit.Id.ToString() + ".xml";
                    MemoryStream stream = new MemoryStream();
                    xmlFile.Save(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    zipFile.AddEntry(fileName, stream);

                    DEKOResponseDoc.XmlVuonExport(unloadId, group_unit, unit, file_name_common, doc_in, _doc_out, dictNodes, zipFile);
                    DEKOResponseDoc.XmlWebExport(group_unit, unit, file_name_common, doc_in, _doc_out, zipFile);
                    DEKOResponseDoc.GetOtvetDocX(unit, file_name_common, _doc_out, _num_pp, zipFile);
                    bads.Add("g|" + unit.CadastralNumber +
                             "|" + ((unit.CadastralCost == null) ? 0 : (decimal)unit.CadastralCost).ToString("0.00") +
                             "|" + estimation_date.ToString("dd.MM.yyyy") +
                             "|" + _doc_out.CreateDate.ToString("dd.MM.yyyy"));
                }
                else
                {
                    _num_pp++;

                    bads.Add("b|" + unit.CadastralNumber +
                             "|" + estimation_date.ToString("dd.MM.yyyy") +
                             "|" + _doc_out.CreateDate.ToString("dd.MM.yyyy"));
                }
            }

            return bads.ToArray();
        }

        public static void ObjectNotChange(List<string> list_bads,
            List<ActOpredel> _list_act,
            List<OMInstance> _list_doc,
            string _dir_name, ZipFile zipFile)
        {
            List<string> baditems = new List<string>();
            List<string> gooditems = new List<string>();
            foreach (string item in list_bads)
            {
                if (item.IndexOf("b|") == 0) baditems.Add(item);
                if (item.IndexOf("g|") == 0) gooditems.Add(item);
            }

            #region Акт_об_определении_КС
            {
                FileStream fileStream = Core.ConfigParam.Configuration.GetFileStream("ActDeterminingCadastralCostVUON", ".xlsx", "ExcelTemplates");
                ExcelFile excelAct = ExcelFile.Load(fileStream, GemBox.Spreadsheet.LoadOptions.XlsxDefault);
                var sheetAct = excelAct.Worksheets[0];

                //Записываем количество bad, good
                DataExportCommon.SetCellValueNoBorder(sheetAct, 5, 9 , gooditems.Count.ToString(), HorizontalAlignmentStyle.Center, VerticalAlignmentStyle.Center);
                DataExportCommon.SetCellValueNoBorder(sheetAct, 5, 10, baditems .Count.ToString(), HorizontalAlignmentStyle.Center, VerticalAlignmentStyle.Center);

                #region Хорошие - с изменениями, good
                {
                    int fieldcount = 6;
                    int number_row_curr = 13;

                    int lenobjs = gooditems.Count;
                    sheetAct.Rows.InsertCopy(number_row_curr, lenobjs-1, sheetAct.Rows[number_row_curr]);

                    object[,] objvals = new object[100, fieldcount];
                    int curindval = 0;
                    for (int i = 0; i < lenobjs; i++)
                    {
                        List<object> objarrs = new List<object>();
                        string[] arrrec = gooditems[i].Split('|');
                        objarrs.Add(i + 1);
                        objarrs.Add(arrrec[1]);
                        objarrs.Add(Convert.ToDouble(arrrec[2]));
                        objarrs.Add(null);
                        objarrs.Add(Convert.ToDateTime(arrrec[3]).ToString("dd.MM.yyyy"));
                        objarrs.Add(Convert.ToDateTime(arrrec[4]).ToString("dd.MM.yyyy"));

                        #region array
                        for (int f = 0; f < fieldcount; f++)
                        {
                            objvals[curindval, f] = objarrs[f];
                        }

                        if (curindval >= 99)
                        {
                            DataExportCommon.AddRow(sheetAct, number_row_curr - 99, objvals);
                            curindval = -1;
                            objvals = new object[100, fieldcount];
                        }
                        #endregion

                        curindval++;
                        number_row_curr++;
                    }
                    if (curindval != 0)
                    {
                        DataExportCommon.AddRow(sheetAct, number_row_curr - curindval, objvals, curindval);
                    }
                }
                #endregion

                #region Плохие - без изменений, bad
                {
                    int fieldcount = 6;
                    int number_row_curr = gooditems.Count + 13 + 4 - 1;

                    int lenobjs = baditems.Count;
                    sheetAct.Rows.InsertCopy(number_row_curr, lenobjs - 1, sheetAct.Rows[number_row_curr]);

                    object[,] objvals = new object[100, fieldcount];
                    int curindval = 0;
                    for (int i = 0; i < lenobjs; i++)
                    {
                        List<object> objarrs = new List<object>();
                        string[] arrrec = baditems[i].Split('|');
                        objarrs.Add(i + 1);
                        objarrs.Add(arrrec[1]);
                        objarrs.Add(Convert.ToDateTime(arrrec[2]).ToString("dd.MM.yyyy"));
                        objarrs.Add(null);
                        objarrs.Add(Convert.ToDateTime(arrrec[3]).ToString("dd.MM.yyyy"));
                        objarrs.Add(null);

                        #region array
                        for (int f = 0; f < fieldcount; f++)
                        {
                            objvals[curindval, f] = objarrs[f];
                        }

                        if (curindval >= 99)
                        {
                            DataExportCommon.AddRow(sheetAct, number_row_curr - 99, objvals);
                            curindval = -1;
                            objvals = new object[100, fieldcount];
                        }
                        #endregion

                        curindval++;
                        number_row_curr++;
                    }
                    if (curindval != 0)
                    {
                        DataExportCommon.AddRow(sheetAct, number_row_curr - curindval, objvals, curindval);
                    }
                }
                #endregion

                string fileName = "DOC" + "\\Акт_об_определении_КС" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                MemoryStream stream = new MemoryStream();
                excelAct.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
                stream.Seek(0, SeekOrigin.Begin);
                zipFile.AddEntry(fileName, stream);

                //excelAct.Save("D:\\Temp\\KO_Vuon\\ActDeterminingCadastralCostVUON.xlsx");

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

                string fileName = "DOC" + "\\Акт_определения_КС_по_МУ_пункт_12_2" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                MemoryStream stream = new MemoryStream();
                aexcell.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
                stream.Seek(0, SeekOrigin.Begin);
                zipFile.AddEntry(fileName, stream);
            }
            #endregion

        }
    }
}