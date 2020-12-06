using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.Register.QuerySubsystem;
using GemBox.Spreadsheet;
using GemBox.Document;
using GemBox.Document.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.Reflection;
using System.Text;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.SRD;
using Ionic.Zip;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.Directory.Common;
using SaveOptions = GemBox.Document.SaveOptions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Modeling;
using Serilog;

namespace KadOzenka.Dal.DataExport
{
    public struct ActOpredel
    {
        public string kn;
        public string osnovanie;
        public string code;
        public string subgroup;
        public string act_model;
        public decimal? kc;
        public string act_dop;
        public string act_other;
    }

    public class GeneralizedValuesUPKSZ
    {
        public int       NumberGroup   = 0;  //Количество групп
        public int       CountObj      = 0;  //Количество объектов в районе
        public string    CadastralArea = ""; //Номер кадастрового района
        public string    CadastralBlok = ""; //Номер кадастрового квартала
        public double[,] MinAvgMax;          //Массив УПКСЗ по группам

        public GeneralizedValuesUPKSZ(int _num)
        {
            NumberGroup = _num;
            MinAvgMax = new double[4, NumberGroup];
            for (int i = 0; i < NumberGroup; i++)
            {
                MinAvgMax[0, i] = -1;  //Минимальное УПКСЗ
                MinAvgMax[1, i] = 0;   //Среднее УПКСЗ. В начале записывается сумма УПКСЗ. Потом пересчитывается.
                MinAvgMax[2, i] = 0;   //Максимальное УПКСЗ
                MinAvgMax[3, i] = 0;   //Количество объектов данной группы.  
            }
        }
    }

    public class DataExporterKO
    {
        /// <summary>
        /// Выгрузка значений меток по фактору и группе в формате Excel
        /// </summary>
        public static Stream ExportMarkerToExcel(long groupId, long factorId)
        {
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Метки");

            DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "Значение фактора", "Метка" });

            List<ObjectModel.KO.OMMarkCatalog> objs = ObjectModel.KO.OMMarkCatalog.Where(x => x.GroupId == groupId && x.FactorId == factorId).SelectAll().Execute();
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

                    #region Заголовок объекта
                    List<object> value = new List<object>();
                    value.Add(obj.ValueFactor);
                    value.Add(obj.MetkaFactor);
                    #endregion

                    lock (locked)
                    {
                        values.Add(value);
                    }
                });

                int row = 1;
                foreach (List<object> value in values)
                {
                    DataExportCommon.AddRow(mainWorkSheet, row, value.ToArray());
                    row++;
                }
                Console.WriteLine(values.Count);
            }

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        /// <summary>
        /// Выгрузка списка значений по фактору и группе в формате Excel
        /// </summary>
        public static Stream ExportMarkerListToExcel(long groupId, long factorId)
        {
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Метки");

            DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "Значение фактора", "Метка" });

            //List<ObjectModel.KO.OMUnit> units = ObjectModel.KO.OMUnit.Where(x => x.GroupId == groupId).SelectAll().Execute();
            //if (units.Count > 0)
            //{
            //CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            //ParallelOptions options = new ParallelOptions
            //{
            //    CancellationToken = cancelTokenSource.Token,
            //    MaxDegreeOfParallelism = 20
            //};

            //object locked = new object();
            //List<List<object>> values = new List<List<object>>();
            //List<string> fvalues = new List<string>();
            //int curIndex = 0;
            //Parallel.ForEach(units, options, unit =>
            //{
            //    curIndex++;
            //    if (curIndex % 40 == 0) Console.WriteLine(curIndex);

            //    DataTable data = RegisterStorage.GetAttribute((int)unit.Id, OMGroup.GetFactorReestrId(unit), (int)factorId);
            //    string curValue = string.Empty;
            //    if (data != null)
            //    {
            //        if (data.Rows.Count > 0)
            //        {
            //            curValue = data.Rows[0].ItemArray[6].ParseToString();
            //        }
            //    }


            //    if (curValue != string.Empty)
            //    {
            //        lock (locked)
            //        {
            //            if (!fvalues.Contains(curValue))
            //                fvalues.Add(curValue);
            //        }
            //    }

            //});

            //int row = 1;
            //List<ObjectModel.KO.OMMarkCatalog> markers = ObjectModel.KO.OMMarkCatalog.Where(x => x.GroupId == groupId && x.FactorId == factorId).SelectAll().Execute();

            //foreach (string value in fvalues)
            //{
            //    string mvalue = string.Empty;
            //    ObjectModel.KO.OMMarkCatalog marker = markers.Find(x => x.ValueFactor.ToUpper() == value.ToUpper());
            //    if (marker != null)
            //    {
            //        mvalue = (marker.MetkaFactor == null) ? string.Empty : marker.MetkaFactor.ParseToString();
            //    }
            //    DataExportCommon.AddRow(mainWorkSheet, row, new object[] { value, mvalue });
            //    row++;
            //}
            //Console.WriteLine(fvalues.Count);
            //}

            var row = 1;
            var markers = new ModelFactorsService().GetMarks(groupId, factorId);

            foreach (var marker in markers)
            {
	            var factor = marker.ValueFactor == null ? string.Empty : marker.ValueFactor;
	            var metka = marker.MetkaFactor == null ? string.Empty : marker.MetkaFactor.ParseToString();

                DataExportCommon.AddRow(mainWorkSheet, row, new object[] { factor, metka });
	            row++;
            }

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }

    /// <summary>
    /// Класс выгрузки изменений по объектам.
    /// </summary>
    public class DEKOChange : IKoUnloadResult
    {
        /// <summary>
        /// Выгрузка изменений
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadChange)]
        public static List<ResultKoUnloadSettings> ExportUnitChangeToExcel(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings settings, SetProgress setProgress)
        {
	        KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var result = new List<ResultKoUnloadSettings>();
            var progressMessage = "Выгрузка изменений";
            string filename = "Список_изменений_"+DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Изменения");

            DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "КН", "Дата изменения", "Тип", "Статус", "Старое значение", "Новое значение", "Изменение" });
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };

            var taskCounter = 0;
            var progress = 0;
            foreach (long taskId in settings.TaskFilter)
            {
	            List<ObjectModel.KO.OMUnit> units = null;
                if (settings.UnloadParcel)
                    units = ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == PropertyTypes.Stead).SelectAll().Execute();
                else
                    units = ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code != PropertyTypes.Stead).SelectAll().Execute();

                var unitsCounter = 0;
                if (units.Count > 0)
                {
                    int row = 1;

                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        List<ObjectModel.KO.OMUnitChange> changes = ObjectModel.KO.OMUnitChange.Where(x => x.UnitId == unit.Id).SelectAll().Execute();

                        foreach (ObjectModel.KO.OMUnitChange change in changes)
                        {
                            DataExportCommon.AddRow(mainWorkSheet, row, new object[] { unit.CadastralNumber, unit.CreationDate.Value, unit.PropertyType, unit.StatusRepeatCalc, change.OldValue, change.NewValue, change.ChangeStatus });
                            row++;
                        }

                        unitsCounter++;
                        progress = (unitsCounter * 100 / units.Count + taskCounter * 100) / settings.TaskFilter.Count;
                        setProgress(progress, progressMessage:progressMessage);
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                }

                taskCounter++;
            }

            long id = SaveReportDownload.SaveReportExcel(filename, excelTemplate, OMUnit.GetRegisterId());
            //excelTemplate.Save(filename, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
            var resultFile = new ResultKoUnloadSettings
            {
	            FileId = id,
	            FileName = filename,
	            TaskId = settings.TaskFilter.FirstOrDefault()
            };
            result.Add(resultFile);
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return result;
        }
    }

    /// <summary>
    /// Класс выгрузки в XML результатов Кадастровой оценки по объектам.
    /// </summary>
    public class DEKOUnit : IKoUnloadResult
    {
        /// <summary>
        /// Экспорт в Xml - КНомер, УПКСЗ, КСтоимость. По 5000 записей. 
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadXML1)]
        public static List<ResultKoUnloadSettings> ExportToXml(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
	        var progressMessage = "Выгрузка в XML результатов Кадастровой оценки по объектам";
	        var taskCounter = 0;
	        var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            List<ResultKoUnloadSettings> res = new List<ResultKoUnloadSettings>();

            string file_name = "";
            foreach (long taskId in setting.TaskFilter)
            {
                OMTask currentTask = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
                List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == taskId && x.CadastralCost > 0).SelectAll().Execute();
                int count_curr = 0;
                int count_all = units_all.Count();

                List<OMUnit> units_curr = new List<OMUnit>();
                int count_write = 5000;
                int count_file = 1;
                var unitCounter = 0;
                foreach (OMUnit unit in units_all)
                {
                    units_curr.Add(unit);
                    count_curr++;
                    if (count_curr == count_write)
                    {
                        file_name = "Task_" + taskId + "COST_" + ConfigurationManager.AppSettings["ucSender"] + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + count_file.ToString().PadLeft(4, '0');
                        Stream resultFile = SaveXmlDocument(units_curr, currentTask.EstimationDate);
                        long id = SaveReportDownload.SaveReport(file_name, resultFile, OMUnit.GetRegisterId());
                        var resFile = new ResultKoUnloadSettings
                        {
                            FileName = file_name,
                            FileId = id,
                            TaskId = taskId
                        };
                        res.Add(resFile);

                        units_curr.Clear();
                        count_curr = 0;
                        count_file++;
                    }

                    unitCounter++;
                    progress = (unitCounter * 100 / count_all + taskCounter * 100) / setting.TaskFilter.Count;
                    setProgress(progress, progressMessage: progressMessage);
                    KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }

                file_name = "Task_" + taskId + "COST_" + ConfigurationManager.AppSettings["ucSender"] + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + count_file.ToString().PadLeft(4, '0');
                Stream resultFile1 = SaveXmlDocument(units_curr, currentTask.EstimationDate);

				long id1 = SaveReportDownload.SaveReport(file_name, resultFile1, OMUnit.GetRegisterId());
				var koResultFile = new ResultKoUnloadSettings
				{
					FileName = file_name,
					FileId = id1,
					TaskId = taskId
				};
				res.Add(koResultFile);

				taskCounter++;
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return res;
        }

        public static Stream SaveXmlDocument(List<OMUnit> units, DateTime? estimationDate)
        {
            XmlDocument xmlFile = new XmlDocument();
            XmlNode xnLandValuation = xmlFile.CreateElement("LandValuation");

            AddXmlDocument(xmlFile, xnLandValuation, ConfigurationManager.AppSettings["ucUseDoc"].ParseToBoolean(),
                                                     ConfigurationManager.AppSettings["ucDocName"],
                                                     ConfigurationManager.AppSettings["ucDocNum"],
                                                     ConfigurationManager.AppSettings["ucDocDate"].ParseToDateTime(),
                                                     (estimationDate != null)? estimationDate.Value:DateTime.Now,
                                                     ConfigurationManager.AppSettings["ucSender"],
                                                     DateTime.Now);
            AddXmlPackage(xmlFile, xnLandValuation, units);

            xmlFile.AppendChild(xnLandValuation);
            MemoryStream stream = new MemoryStream();
            xmlFile.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public static void AddXmlDocument(XmlDocument xmlFile, XmlNode parent, bool usedoc, string docname, string docn, DateTime docdate, DateTime ddate, string sender, DateTime dupload)
        {
            XmlNode xneDocument = xmlFile.CreateElement("eDocument");
            DataExportCommon.AddAttribute(xmlFile, xneDocument, "Date", ddate.ToString("yyyy-MM-dd"));
            DataExportCommon.AddAttribute(xmlFile, xneDocument, "CodeType", "004");
            DataExportCommon.AddAttribute(xmlFile, xneDocument, "Version", "02");
            if (usedoc)
            {
                DataExportCommon.AddAttribute(xmlFile, xneDocument, "Name_Doc", docname);
                DataExportCommon.AddAttribute(xmlFile, xneDocument, "N_doc", docn);
                DataExportCommon.AddAttribute(xmlFile, xneDocument, "Date_Doc", docdate.ToString("yyyy-MM-dd"));
            }

            #region Sender 
            XmlNode xnSender = xmlFile.CreateElement("Sender");
            DataExportCommon.AddAttribute(xmlFile, xnSender, "Name", sender);
            DataExportCommon.AddAttribute(xmlFile, xnSender, "Date_Upload", dupload.ToString("yyyy-MM-dd"));
            parent.AppendChild(xnSender);
            #endregion

            #region Recipient 
            XmlNode xnRecipient = xmlFile.CreateElement("Recipient");
            DataExportCommon.AddAttribute(xmlFile, xnRecipient, "Name", "");
            parent.AppendChild(xnRecipient);
            #endregion

            parent.AppendChild(xneDocument);
        }

        public static void AddXmlPackage(XmlDocument xmlFile, XmlNode parent, List<OMUnit> units)
        {
            XmlNode xnPackage = xmlFile.CreateElement("Package");

            XmlNode xnFederal = xmlFile.CreateElement("Federal");
            DataExportCommon.AddAttribute(xmlFile, xnFederal, "CadastralNumber", "00");
            AddXmlRegions(xmlFile, xnFederal, units);
            parent.AppendChild(xnFederal);

            parent.AppendChild(xnPackage);
        }

        public static void AddXmlRegions(XmlDocument xmlFile, XmlNode parent, List<OMUnit> units)
        {
            XmlNode xnRegions = xmlFile.CreateElement("Cadastral_Regions");

            string kn_sub = "";
            string kn_raion = "";
            string kn_kvartal = "";

            XmlNode xn_kn_sub = null;
            XmlNode xn_kn_raion = null;
            XmlNode xn_kn_kvartal = null;

            XmlNode xn_Cadastral_Districts = null;
            XmlNode xn_Cadastral_Blocks = null;
            XmlNode xn_Parcels = null;

            foreach (ObjectModel.KO.OMUnit unit in units)
            {
                string[] arrtmp = unit.CadastralNumber.Split(':');
                if (arrtmp.Length == 4)
                {
                    string kn = unit.CadastralNumber;
                    string tupksz = unit.Upks.ToString();
                    string tks = unit.CadastralCost.ToString();
                    string upksz = "";
                    string ks = "";
                    double dUPKSZ = 0;
                    double dKS = 0;

                    if (double.TryParse(tupksz.Replace(',', '.'), out dUPKSZ))
                        upksz = dUPKSZ.ToString("0.00").Replace(',', '.');
                    else
                        if (double.TryParse(tupksz.Replace('.', ','), out dUPKSZ))
                        upksz = dUPKSZ.ToString("0.00").Replace(',', '.');
                    else
                        upksz = "";

                    if (double.TryParse(tks.Replace(',', '.'), out dKS))
                        ks = dKS.ToString("0.00").Replace(',', '.');
                    else
                        if (double.TryParse(tks.Replace('.', ','), out dKS))
                        ks = dKS.ToString("0.00").Replace(',', '.');
                    else
                        ks = "";

                    string[] arr = kn.Split(':');
                    string new_kn_sub = arr[0];
                    string new_kn_raion = arr[0] + ":" + arr[1];
                    string new_kn_kvartal = arr[0] + ":" + arr[1] + ":" + arr[2];

                    if (kn_sub != new_kn_sub)
                    {
                        xn_kn_sub = xmlFile.CreateElement("Cadastral_Region");
                        DataExportCommon.AddAttribute(xmlFile, xn_kn_sub, "CadastralNumber", new_kn_sub);
                        xn_Cadastral_Districts = xmlFile.CreateElement("Cadastral_Districts");
                        xn_kn_sub.AppendChild(xn_Cadastral_Districts);
                        xnRegions.AppendChild(xn_kn_sub);
                        kn_sub = new_kn_sub;
                    }

                    if (kn_raion != new_kn_raion)
                    {
                        xn_kn_raion = xmlFile.CreateElement("Cadastral_District");
                        DataExportCommon.AddAttribute(xmlFile, xn_kn_raion, "CadastralNumber", new_kn_raion);
                        xn_Cadastral_Blocks = xmlFile.CreateElement("Cadastral_Blocks");
                        xn_Cadastral_Districts.AppendChild(xn_kn_raion);
                        xn_kn_raion.AppendChild(xn_Cadastral_Blocks);
                        kn_raion = new_kn_raion;
                    }

                    if (kn_kvartal != new_kn_kvartal)
                    {
                        xn_kn_kvartal = xmlFile.CreateElement("Cadastral_Block");
                        DataExportCommon.AddAttribute(xmlFile, xn_kn_kvartal, "CadastralNumber", new_kn_kvartal);
                        xn_Parcels = xmlFile.CreateElement("Parcels");
                        xn_Cadastral_Blocks.AppendChild(xn_kn_kvartal);
                        xn_kn_kvartal.AppendChild(xn_Parcels);
                        kn_kvartal = new_kn_kvartal;
                    }

                    XmlNode xn_parcel = xmlFile.CreateElement("Parcel");
                    DataExportCommon.AddAttribute(xmlFile, xn_parcel, "CadastralNumber", kn);
                    XmlNode xn_Ground_Payments = xmlFile.CreateElement("Ground_Payments");
                    XmlNode xn_Specific_CadastralCost = xmlFile.CreateElement("Specific_CadastralCost");
                    DataExportCommon.AddAttribute(xmlFile, xn_Specific_CadastralCost, "Value", upksz);
                    DataExportCommon.AddAttribute(xmlFile, xn_Specific_CadastralCost, "Unit", "1002");
                    XmlNode xn_CadastralCost = xmlFile.CreateElement("CadastralCost");
                    DataExportCommon.AddAttribute(xmlFile, xn_CadastralCost, "Value", ks);
                    DataExportCommon.AddAttribute(xmlFile, xn_CadastralCost, "Unit", "383");
                    xn_Ground_Payments.AppendChild(xn_Specific_CadastralCost);
                    xn_Ground_Payments.AppendChild(xn_CadastralCost);
                    xn_parcel.AppendChild(xn_Ground_Payments);

                    if ((ks != String.Empty) & (upksz != string.Empty))
                        xn_Parcels.AppendChild(xn_parcel);
                }
            }
            parent.AppendChild(xnRegions);
        }
    }

    /// <summary>
    /// Класс выгрузки в XML результатов Кадастровой оценки по группам.
    /// </summary>
    public class DEKOGroup : IKoUnloadResult
    {
        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMGroup
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadXML2)]
        public static List<ResultKoUnloadSettings> ExportToXml(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
	        var progressMessage = "Выгрузка в XML результатов Кадастровой оценки по группам";
	        var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            List<ResultKoUnloadSettings> res = new List<ResultKoUnloadSettings>();
            // Выбираем все подгруппы
            List<OMGroup> koGroups = OMGroup.Where(x => x.ParentId != -1).SelectAll().Execute();
            int countCurr = 0;
            int countAll = koGroups.Count();
            foreach (OMGroup subgroup in koGroups)
            {
                countCurr++;
                string str_message = "Выгружается группа " + countCurr.ToString() + " (Id=" + subgroup.Id.ToString() + ") из " + countAll.ToString();
                Console.WriteLine(str_message);
                var taskCounter = 0;
                foreach (long taskId in setting.TaskFilter)
                {
                    subgroup.Unit = OMUnit.Where(x => x.GroupId == subgroup.Id && x.TaskId == taskId && x.CadastralCost > 0).SelectAll().Execute();

                    if (subgroup.Unit.Count > 0)
                    {
                        Stream resultFile = SaveXmlDocument(subgroup, str_message);

                        OMGroup parent_group = OMGroup.Where(x => x.Id == subgroup.ParentId).SelectAll().ExecuteFirstOrDefault();
                        string full_group_num = ((parent_group.Number == null ? parent_group.Id.ToString() : parent_group.Number)) + "." +
                                                ((subgroup.Number == null ? subgroup.Id.ToString() : subgroup.Number));
                        full_group_num = full_group_num.Replace("\n", "");

                        string file_name = "Task_" + taskId  +"_" + full_group_num
										   + "_FD_State_Cadastral_Valuation_"
                                           + subgroup.Id.ToString().PadLeft(5, '0')
                                           + countCurr.ToString().PadLeft(5, '0');

                        long id = SaveReportDownload.SaveReport(file_name, resultFile, OMGroup.GetRegisterId());
                        var fileResult = new ResultKoUnloadSettings
                        {
	                        FileName = file_name,
	                        FileId = id,
	                        TaskId = taskId
                        };
                        res.Add(fileResult);
                    }
                    taskCounter++;
                    progress = (taskCounter * 100 / setting.TaskFilter.Count + (countCurr-1) * 100) / countCurr;
                    setProgress(progress, progressMessage: progressMessage);
                    KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return res;
        }

        /// <summary>
        /// Экспорт в Xml - КОценка по группам.
        /// </summary>
        public static Stream SaveXmlDocument(OMGroup _subgroup, string _message)
        {

            XmlDocument xmlFile = new XmlDocument();
            XmlNode xnLandValuation = xmlFile.CreateElement("FD_State_Cadastral_Valuation");
            DataExportCommon.AddAttribute(xmlFile, xnLandValuation, "Version", "02");
            xmlFile.AppendChild(xnLandValuation);

            AddXmlGeneralInfo(xmlFile, xnLandValuation);
            Dictionary<Int64, XmlNode> dictNodes = new Dictionary<long, XmlNode>();
            AddXmlPackage(xmlFile, xnLandValuation, _subgroup, dictNodes, _message);
            xmlFile.AppendChild(xnLandValuation);

            MemoryStream stream = new MemoryStream();
            xmlFile.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public static void AddXmlGeneralInfo(XmlDocument xmlFile, XmlNode parent)
        {
            XmlNode xnGeneral_Information = xmlFile.CreateElement("General_Information");

            #region RegionsRF
            XmlNode xnRegionsRF = xmlFile.CreateElement("RegionsRF");
            XmlNode xnRegionRF = xmlFile.CreateElement("RegionRF");
            xnRegionRF.InnerText = ConfigurationManager.AppSettings["3XML_RegionRF"];
            xnRegionsRF.AppendChild(xnRegionRF);
            xnGeneral_Information.AppendChild(xnRegionsRF);
            #endregion

            #region Contract_Evaluation
            XmlNode xnContract_Evaluation = xmlFile.CreateElement("Contract_Evaluation");

            #region Details
            XmlNode xnDetails = xmlFile.CreateElement("Details");
            XmlNode xnDate_Doc = xmlFile.CreateElement("Date_Doc");
            xnDate_Doc.InnerText = ConfigurationManager.AppSettings["3XML_Detail_Date_Doc"].ParseToDateTime().ToString();
            xnDetails.AppendChild(xnDate_Doc);

            XmlNode xnN_Doc = xmlFile.CreateElement("N_Doc");
            xnN_Doc.InnerText = ConfigurationManager.AppSettings["3XML_Detail_N_Doc"];
            xnDetails.AppendChild(xnN_Doc);

            XmlNode xnDocName = xmlFile.CreateElement("Name");
            xnDocName.InnerText = ConfigurationManager.AppSettings["3XML_Detail_DocName"];
            xnDetails.AppendChild(xnDocName);

            xnContract_Evaluation.AppendChild(xnDetails);
            #endregion

            #region Customer
            XmlNode xnCustomer = xmlFile.CreateElement("Customer");
            XmlNode xnCustomerName = xmlFile.CreateElement("Name");
            xnCustomerName.InnerText = ConfigurationManager.AppSettings["3XML_Customer_Name"];
            xnCustomer.AppendChild(xnCustomerName);

            XmlNode xnCustomerCode_OGRN = xmlFile.CreateElement("Code_OGRN");
            xnCustomerCode_OGRN.InnerText = ConfigurationManager.AppSettings["3XML_Customer_Code_OGRN"];
            xnCustomer.AppendChild(xnCustomerCode_OGRN);

            XmlNode xnCustomerAddress = xmlFile.CreateElement("Address");
            xnCustomerAddress.InnerText = ConfigurationManager.AppSettings["3XML_Customer_Address"];
            xnCustomer.AppendChild(xnCustomerAddress);

            xnContract_Evaluation.AppendChild(xnCustomer);
            #endregion

            #region Administrant
            XmlNode xnAdministrant = xmlFile.CreateElement("Administrant");
            XmlNode xnJuridic = xmlFile.CreateElement("Juridic");
            XmlNode xnAdministrantName = xmlFile.CreateElement("Name");
            xnAdministrantName.InnerText = ConfigurationManager.AppSettings["3XML_Administrant_Name"];
            xnJuridic.AppendChild(xnAdministrantName);

            XmlNode xnAdministrantCode_OGRN = xmlFile.CreateElement("Code_OGRN");
            xnAdministrantCode_OGRN.InnerText = ConfigurationManager.AppSettings["3XML_Administrant_Code_OGRN"];
            xnJuridic.AppendChild(xnAdministrantCode_OGRN);

            XmlNode xnAdministrantAddress = xmlFile.CreateElement("Address");
            xnAdministrantAddress.InnerText = ConfigurationManager.AppSettings["3XML_Administrant_Address"];
            xnJuridic.AppendChild(xnAdministrantAddress);

            xnAdministrant.AppendChild(xnJuridic);
            xnContract_Evaluation.AppendChild(xnAdministrant);
            #endregion

            xnGeneral_Information.AppendChild(xnContract_Evaluation);
            #endregion

            #region Report_Details
            XmlNode xnReport_Details = xmlFile.CreateElement("Report_Details");
            DataExportCommon.AddAttribute(xmlFile, xnReport_Details, "Date", ConfigurationManager.AppSettings["3XML_Report_Details_Date"].ParseToDateTime().ToString());
            DataExportCommon.AddAttribute(xmlFile, xnReport_Details, "Number", ConfigurationManager.AppSettings["3XML_Report_Details_Number"]);

            #region Appraisers
            XmlNode xnAppraisers = xmlFile.CreateElement("Appraisers");

            #region App_1
            XmlNode xnAppraiser = xmlFile.CreateElement("Appraiser");
            XmlNode xnAppraiserFIO = xmlFile.CreateElement("FIO");
            XmlNode xnAppraiserF = xmlFile.CreateElement("Surname");
            xnAppraiserF.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserF"];
            xnAppraiserFIO.AppendChild(xnAppraiserF);

            XmlNode xnAppraiserI = xmlFile.CreateElement("First");
            xnAppraiserI.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserI"];
            xnAppraiserFIO.AppendChild(xnAppraiserI);

            XmlNode xnAppraiserO = xmlFile.CreateElement("Patronymic");
            xnAppraiserO.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserO"];
            xnAppraiserFIO.AppendChild(xnAppraiserO);

            xnAppraiser.AppendChild(xnAppraiserFIO);

            XmlNode xnAppraiserName_Org = xmlFile.CreateElement("Name_Org");
            xnAppraiserName_Org.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserName_Org"];
            xnAppraiser.AppendChild(xnAppraiserName_Org);

            xnAppraisers.AppendChild(xnAppraiser);
            #endregion

            #region App_2
            XmlNode xnAppraiser1 = xmlFile.CreateElement("Appraiser");
            XmlNode xnAppraiserFIO1 = xmlFile.CreateElement("FIO");
            XmlNode xnAppraiserF1 = xmlFile.CreateElement("Surname");
            xnAppraiserF1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserF1"];
            xnAppraiserFIO1.AppendChild(xnAppraiserF1);

            XmlNode xnAppraiserI1 = xmlFile.CreateElement("First");
            xnAppraiserI1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserI1"];
            xnAppraiserFIO1.AppendChild(xnAppraiserI1);

            XmlNode xnAppraiserO1 = xmlFile.CreateElement("Patronymic");
            xnAppraiserO1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserO1"];
            xnAppraiserFIO1.AppendChild(xnAppraiserO1);

            xnAppraiser1.AppendChild(xnAppraiserFIO1);

            XmlNode xnAppraiserName_Org1 = xmlFile.CreateElement("Name_Org");
            xnAppraiserName_Org1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserName_Org1"];
            xnAppraiser1.AppendChild(xnAppraiserName_Org1);

            xnAppraisers.AppendChild(xnAppraiser1);
            #endregion

            xnReport_Details.AppendChild(xnAppraisers);
            #endregion

            xnGeneral_Information.AppendChild(xnReport_Details);
            #endregion

            parent.AppendChild(xnGeneral_Information);
        }

        public static void AddXmlPackage(XmlDocument _xmlFile, XmlNode _parent_node, OMGroup _subgroup, Dictionary<Int64, XmlNode> _dictNodes, string _message)
        {
            XmlNode xnPackage = _xmlFile.CreateElement("Package");

            #region Groups_Real_Estates
            XmlNode xnGroups_Real_Estates = _xmlFile.CreateElement("Groups_Real_Estates");

            XmlNode xnGroup_Real_Estate = _xmlFile.CreateElement("Group_Real_Estate");
            XmlNode xnID_Group = _xmlFile.CreateElement("ID_Group");
            xnID_Group.InnerText = _subgroup.Id.ToString();
            xnGroup_Real_Estate.AppendChild(xnID_Group);

            XmlNode xnName_Group = _xmlFile.CreateElement("Name_Group");
            xnName_Group.InnerText = _subgroup.GroupName;
            xnGroup_Real_Estate.AppendChild(xnName_Group);

            xnGroups_Real_Estates.AppendChild(xnGroup_Real_Estate);
            xnPackage.AppendChild(xnGroups_Real_Estates);
            #endregion

            #region Evaluative_Factors
            if (_dictNodes.ContainsKey(_subgroup.Id))
            {
                XmlNode temp = _xmlFile.ImportNode(_dictNodes[_subgroup.Id], true);
                xnPackage.AppendChild(temp);
            }
            else
            {
                XmlNode xnEvaluative_Factors = _xmlFile.CreateElement("Evaluative_Factors");
                OMModel model = OMModel.Where(x => x.GroupId == _subgroup.Id && x.IsActive.Coalesce(false) == true).SelectAll().ExecuteFirstOrDefault();
                if (model != null)
                {
                    if (model.ModelFactor.Count == 0)
                        model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id && x.AlgorithmType_Code==model.AlgoritmType_Code).SelectAll().Execute();

                    int countCurr = 0;
                    int countAll = model.ModelFactor.Count();
                    foreach (OMModelFactor factor in model.ModelFactor)
                    {
                        RegisterAttribute attribute_factor = RegisterCache.GetAttributeData((int)(factor.FactorId));
                        factor.FillMarkCatalogs(model);

                        XmlNode xnEvaluative_Factor = _xmlFile.CreateElement("Evaluative_Factor");
                        DataExportCommon.AddAttribute(_xmlFile, xnEvaluative_Factor, "Id_Factor", factor.FactorId.ToString() + "_" + _subgroup.Id.ToString());
                        DataExportCommon.AddAttribute(_xmlFile, xnEvaluative_Factor, "Type", ((attribute_factor.Type == RegisterAttributeType.STRING) & (factor.MarkCatalogs.Count > 0)) ? "1" : "2");
                        XmlNode xnName_Factor = _xmlFile.CreateElement("Name_Factor");
                        xnName_Factor.InnerText = attribute_factor.Name;
                        xnEvaluative_Factor.AppendChild(xnName_Factor);

                        XmlNode xnName_Factor_Desc = _xmlFile.CreateElement("Name_Factor_Desc");
                        xnName_Factor_Desc.InnerText = attribute_factor.Description;
                        xnEvaluative_Factor.AppendChild(xnName_Factor_Desc);
                        bool addfactor = false;
                        if (attribute_factor.Type == RegisterAttributeType.STRING)
                        {
                            if (factor.MarkCatalogs.Count > 0)
                            {
                                XmlNode xnQualitativeValues = _xmlFile.CreateElement("QualitativeValues");
                                foreach (ObjectModel.KO.OMMarkCatalog mark in factor.MarkCatalogs)
                                {
                                    XmlNode xnQualitativeValue = _xmlFile.CreateElement("QualitativeValue");

                                    XmlNode xnQualitative_Id = _xmlFile.CreateElement("Qualitative_Id");
                                    xnQualitative_Id.InnerText = mark.Id.ToString();
                                    xnQualitativeValue.AppendChild(xnQualitative_Id);

                                    XmlNode xnQualitative_Value = _xmlFile.CreateElement("Qualitative_Value");
                                    xnQualitative_Value.InnerText = mark.ValueFactor.ToUpper();
                                    xnQualitativeValue.AppendChild(xnQualitative_Value);

                                    xnQualitativeValues.AppendChild(xnQualitativeValue);
                                    addfactor = true;
                                }
                                xnEvaluative_Factor.AppendChild(xnQualitativeValues);
                            }
                            else
                            {
                                XmlNode xnQuantitative_Dimension = _xmlFile.CreateElement("Quantitative_Dimension");
                                xnQuantitative_Dimension.InnerText = "Метр";
                                xnEvaluative_Factor.AppendChild(xnQuantitative_Dimension);
                                addfactor = true;
                            }
                        }
                        else
                        {
                            XmlNode xnQuantitative_Dimension = _xmlFile.CreateElement("Quantitative_Dimension");
                            xnQuantitative_Dimension.InnerText = "Метр";
                            xnEvaluative_Factor.AppendChild(xnQuantitative_Dimension);
                            addfactor = true;
                        }

                        if (addfactor)
                            xnEvaluative_Factors.AppendChild(xnEvaluative_Factor);

                        countCurr++;
                        string str_message = _message + " -- " + "Evaluative_Factors " + countCurr.ToString() + " из " + countAll.ToString();
                        Console.WriteLine(str_message);
                    }
                }
                xnPackage.AppendChild(xnEvaluative_Factors);
                _dictNodes.Add(_subgroup.Id, xnEvaluative_Factors);
            }
            #endregion

            #region Appraise
            XmlNode xnAppraise = _xmlFile.CreateElement("Appraise");
            string region_rf = ConfigurationManager.AppSettings["3XML_RegionRF"];

            if (_subgroup.GroupAlgoritm_Code == KoGroupAlgoritm.Model)
            {
                #region Statistical_Modelling
                OMModel model = OMModel.Where(x => x.GroupId == _subgroup.Id && x.IsActive.Coalesce(false) == true).SelectAll().ExecuteFirstOrDefault();

                XmlNode xnStatistical_Modelling = _xmlFile.CreateElement("Statistical_Modelling");
                XmlNode xnGroup_Real_Estate_Modelling = _xmlFile.CreateElement("Group_Real_Estate_Modelling");
                DataExportCommon.AddAttribute(_xmlFile, xnGroup_Real_Estate_Modelling, "ID_Group", _subgroup.Id.ToString());

                #region Rating_Model
                XmlNode xnRating_Model = _xmlFile.CreateElement("Rating_Model");
                if (model != null) xnRating_Model.InnerText = model.AlgoritmType;
                xnGroup_Real_Estate_Modelling.AppendChild(xnRating_Model);
                #endregion

                #region Real_Estates
                XmlNode xnReal_Estates = _xmlFile.CreateElement("Real_Estates");

                foreach (OMUnit unit in _subgroup.Unit)
                {
                    XmlNode xnReal_Estate = _xmlFile.CreateElement("Real_Estate");
                    DataExportCommon.AddAttribute(_xmlFile, xnReal_Estate, "ID_Group", _subgroup.Id.ToString());

                    XmlNode xnCadastralNumber = _xmlFile.CreateElement("CadastralNumber");
                    xnCadastralNumber.InnerText = unit.CadastralNumber;
                    xnReal_Estate.AppendChild(xnCadastralNumber);

                    XmlNode xnCadastralType = _xmlFile.CreateElement("Type");
                    xnCadastralType.InnerText = unit.PropertyType_Code.ToString();
                    xnReal_Estate.AppendChild(xnCadastralType);

                    XmlNode xnCadastralArea = _xmlFile.CreateElement("Area");
                    xnCadastralArea.InnerText = unit.Square.ToString();
                    xnReal_Estate.AppendChild(xnCadastralArea);

                    if (unit.PropertyType_Code == PropertyTypes.Stead)
                    {
                        string value_attr_3_4 = "";
                        if (DataExportCommon.GetObjectAttribute(unit, 3, out value_attr_3_4)) //Код категории земель из ГКН
                        {
                            XmlNode xnAssignation = _xmlFile.CreateElement("Category");
                            DataExportCommon.AddAttribute(_xmlFile, xnAssignation, "Name", value_attr_3_4);
                            xnReal_Estate.AppendChild(xnAssignation);
                        }

                        value_attr_3_4 = "";
                        if (DataExportCommon.GetObjectAttribute(unit, 4, out value_attr_3_4)) //Код вида использования по документам
                        {
                            XmlNode xnUtilization = _xmlFile.CreateElement("Utilization");
                            DataExportCommon.AddAttribute(_xmlFile, xnUtilization, "Name_doc", value_attr_3_4);
                            xnReal_Estate.AppendChild(xnUtilization);
                        }
                    }
                    else
                    {
                        #region Assignation
                        XmlNode xnAssignation = _xmlFile.CreateElement("Assignation");
                        string value_attr_5 = "";
                        if (DataExportCommon.GetObjectAttribute(unit, 5, out value_attr_5)) //Код Вид использования по классификатору
                        {
                            if (unit.PropertyType_Code == PropertyTypes.Building)
                            {
                                XmlNode xnAssignation_Building = _xmlFile.CreateElement("Assignation_Building");
                                XmlNode xnAss_Building = _xmlFile.CreateElement("Ass_Building");
                                xnAss_Building.InnerText = value_attr_5;
                                xnAssignation_Building.AppendChild(xnAss_Building);
                                xnAssignation.AppendChild(xnAssignation_Building);
                            }
                            if (unit.PropertyType_Code == PropertyTypes.Pllacement)
                            {
                                XmlNode xnAssignation_Flat = _xmlFile.CreateElement("Assignation_Flat");
                                XmlNode xnAss_Flat = _xmlFile.CreateElement("Ass_Flat");
                                xnAss_Flat.InnerText = value_attr_5;
                                xnAssignation_Flat.AppendChild(xnAss_Flat);
                                xnAssignation.AppendChild(xnAssignation_Flat);
                            }
                            if ((unit.PropertyType_Code == PropertyTypes.Construction) ||
                                (unit.PropertyType_Code == PropertyTypes.UncompletedBuilding))
                            {
                                XmlNode xnFormalized_Constr_Uncompleted = _xmlFile.CreateElement("Formalized_Constr_Uncompleted");
                                xnFormalized_Constr_Uncompleted.InnerText = value_attr_5;
                                xnAssignation.AppendChild(xnFormalized_Constr_Uncompleted);
                            }
                            if (value_attr_5 != "005002002999")
                                xnReal_Estate.AppendChild(xnAssignation);
                        }
                        #endregion
                    }

                    XmlNode xnCadastralLocation = _xmlFile.CreateElement("Location");
                    XmlNode xnCadastralRegion = _xmlFile.CreateElement("Region");
                    xnCadastralRegion.InnerText = region_rf;
                    xnCadastralLocation.AppendChild(xnCadastralRegion);

                    string value_attr = "";
                    if (DataExportCommon.GetObjectAttribute(unit, 600, out value_attr)) //Код 600 - Адрес 
                    {
                        XmlNode xnCadastralNote = _xmlFile.CreateElement("Note");
                        xnCadastralNote.InnerText = value_attr;
                        xnCadastralLocation.AppendChild(xnCadastralNote);
                    }
                    value_attr = "";
                    if (DataExportCommon.GetObjectAttribute(unit, 8, out value_attr)) //Код 8 - Местоположение
                    {
                        XmlNode xnCadastralOther = _xmlFile.CreateElement("Other");
                        xnCadastralOther.InnerText = value_attr;
                        xnCadastralLocation.AppendChild(xnCadastralOther);
                    }
                    xnReal_Estate.AppendChild(xnCadastralLocation);

                    XmlNode xnDate_valuation = _xmlFile.CreateElement("Date_valuation");
                    xnDate_valuation.InnerText = ConfigurationManager.AppSettings["ucAppDate"].ParseToDateTime().ToString("yyyy-MM-dd");
                    xnReal_Estate.AppendChild(xnDate_valuation);

                    XmlNode xnCEvaluative_Factors = _xmlFile.CreateElement("Evaluative_Factors");

                    #region Получили реестр Id группы, реестр, где лежат ее факторы
                    int? factorReestrId = OMGroup.GetFactorReestrId(_subgroup);
                    //Получаем список факторов группы
                    List<CalcItem> FactorValuesGroup = new List<CalcItem>();
                    DataTable data = RegisterStorage.GetAttributes((int)unit.Id, factorReestrId.Value);
                    if (data != null)
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            FactorValuesGroup.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString(), row.ItemArray[7].ParseToString()));
                        }
                    }
                    #endregion

                    if (model != null)
                    {
                        if (model.ModelFactor.Count == 0)
                            model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id && x.AlgorithmType_Code == model.AlgoritmType_Code).SelectAll().Execute();
                        foreach (OMModelFactor factor_model in model.ModelFactor)
                        {
                            bool findf = false;
                            string value_item = string.Empty;//TODO: значение фактора для данного объекта
                            CalcItem factor_item = FactorValuesGroup.Find(x => x.FactorId == factor_model.FactorId);
                            if (factor_item != null)
                            {
                                findf = true;
                                value_item = factor_item.Value;
                            }

                            if (findf) // если фактор найден
                            {
                                XmlNode xnCEvaluative_Factor = _xmlFile.CreateElement("Evaluative_Factor");
                                DataExportCommon.AddAttribute(_xmlFile, xnCEvaluative_Factor, "ID_Factor", factor_model.FactorId.ToString() + "_" + _subgroup.Id.ToString());

                                RegisterAttribute attribute_factor = RegisterCache.GetAttributeData((int)(factor_model.FactorId));
                                factor_model.FillMarkCatalogs(model);

                                bool addf = false;
                                if (attribute_factor.Type == RegisterAttributeType.STRING)
                                {
                                    if (factor_model.SignMarket)
                                    {
                                        OMMarkCatalog mark = factor_model.MarkCatalogs.Find(x => x.ValueFactor == value_item);
                                        if (mark != null)
                                        {
                                            XmlNode xnQualitative_Id = _xmlFile.CreateElement("Qualitative_Id");
                                            xnQualitative_Id.InnerText = mark.Id.ToString();
                                            xnCEvaluative_Factor.AppendChild(xnQualitative_Id);
                                            addf = true;
                                        }
                                    }
                                    else
                                    {
                                        XmlNode xnQuantitative_Value = _xmlFile.CreateElement("Quantitative_Value");
                                        xnQuantitative_Value.InnerText = value_item.ToUpper();
                                        xnCEvaluative_Factor.AppendChild(xnQuantitative_Value);
                                        addf = true;
                                    }
                                }
                                else
                                {
                                    if (factor_model.SignMarket)
                                    {
                                        OMMarkCatalog mark = factor_model.MarkCatalogs.Find(x => x.ValueFactor == value_item);
                                        if (mark != null)
                                        {
                                            XmlNode xnQuantitative_Formula_Value = _xmlFile.CreateElement("Quantitative_Value");
                                            xnQuantitative_Formula_Value.InnerText = mark.Id.ToString();
                                            xnCEvaluative_Factor.AppendChild(xnQuantitative_Formula_Value);
                                            addf = true;
                                        }
                                        else
                                        {
                                            XmlNode xnQuantitative_Formula_Value = _xmlFile.CreateElement("Quantitative_Value");
                                            xnQuantitative_Formula_Value.InnerText = value_item.ToUpper();
                                            xnCEvaluative_Factor.AppendChild(xnQuantitative_Formula_Value);
                                            addf = true;
                                        }
                                    }
                                }
                                if (addf)
                                    xnCEvaluative_Factors.AppendChild(xnCEvaluative_Factor);
                            }
                        }
                    }

                    xnReal_Estate.AppendChild(xnCEvaluative_Factors);
                    xnReal_Estates.AppendChild(xnReal_Estate);
                }
                xnGroup_Real_Estate_Modelling.AppendChild(xnReal_Estates);
                #endregion

                #region Evaluative_Factors_Modelling
                XmlNode xnEvaluative_Factors_Modelling = _xmlFile.CreateElement("Evaluative_Factors_Modelling");

                if (model != null)
                {
                    foreach (OMModelFactor factor_model in model.ModelFactor)
                    {
                        RegisterAttribute attribute_factor = RegisterCache.GetAttributeData((int)(factor_model.FactorId));
                        factor_model.FillMarkCatalogs(model);

                        XmlNode xnEvaluative_Factor_Modelling = _xmlFile.CreateElement("Evaluative_Factor_Modelling");
                        DataExportCommon.AddAttribute(_xmlFile, xnEvaluative_Factor_Modelling, "Id_factor", factor_model.Id.ToString() + "_" + _subgroup.Id.ToString());
                        if (attribute_factor.Type == RegisterAttributeType.STRING)
                        {
                            if (factor_model.MarkCatalogs.Count > 0)
                            {
                                XmlNode xnCast_Accounts = _xmlFile.CreateElement("Cast_Accounts");
                                foreach (ObjectModel.KO.OMMarkCatalog mark in factor_model.MarkCatalogs)
                                {
                                    XmlNode xnCast_Account = _xmlFile.CreateElement("Cast_Account");

                                    XmlNode xnQualitative_Id = _xmlFile.CreateElement("Qualitative_Id");
                                    xnQualitative_Id.InnerText = mark.Id.ToString();
                                    xnCast_Account.AppendChild(xnQualitative_Id);

                                    XmlNode xnDimension = _xmlFile.CreateElement("Dimension");
                                    xnDimension.InnerText = mark.MetkaFactor.ToString();
                                    xnCast_Account.AppendChild(xnDimension);

                                    xnCast_Accounts.AppendChild(xnCast_Account);
                                }
                                xnEvaluative_Factor_Modelling.AppendChild(xnCast_Accounts);
                            }
                        }
                        xnEvaluative_Factors_Modelling.AppendChild(xnEvaluative_Factor_Modelling);
                    }
                }
                xnGroup_Real_Estate_Modelling.AppendChild(xnEvaluative_Factors_Modelling);
                #endregion

                xnStatistical_Modelling.AppendChild(xnGroup_Real_Estate_Modelling);
                xnAppraise.AppendChild(xnStatistical_Modelling);
                #endregion
            }
            else
            {
                #region Other
                XmlNode xnOther = _xmlFile.CreateElement("Other");
                XmlNode xnEvaluation_Group = _xmlFile.CreateElement("Evaluation_Group");

                XmlNode xnDescription = _xmlFile.CreateElement("Description");
                xnDescription.InnerText = _subgroup.GroupName;  //TODO???    // Desc_SubGroup;
                xnEvaluation_Group.AppendChild(xnDescription);

                XmlNode xnReal_Estates = _xmlFile.CreateElement("Real_Estates");

                foreach (OMUnit unit in _subgroup.Unit)
                {
                    XmlNode xnReal_Estate = _xmlFile.CreateElement("Real_Estate");
                    DataExportCommon.AddAttribute(_xmlFile, xnReal_Estate, "ID_Group", _subgroup.Id.ToString());

                    XmlNode xnCadastralNumber = _xmlFile.CreateElement("CadastralNumber");
                    xnCadastralNumber.InnerText = unit.CadastralNumber;
                    xnReal_Estate.AppendChild(xnCadastralNumber);

                    XmlNode xnCadastralType = _xmlFile.CreateElement("Type");
                    xnCadastralType.InnerText = unit.PropertyType_Code.ToString();
                    xnReal_Estate.AppendChild(xnCadastralType);

                    XmlNode xnSpecific_CadastralCost = _xmlFile.CreateElement("Specific_CadastralCost");
                    DataExportCommon.AddAttribute(_xmlFile, xnSpecific_CadastralCost, "Value", unit.Upks.ToString());
                    DataExportCommon.AddAttribute(_xmlFile, xnSpecific_CadastralCost, "Unit", "1002");
                    xnReal_Estate.AppendChild(xnSpecific_CadastralCost);

                    XmlNode xnCadastralArea = _xmlFile.CreateElement("Area");
                    xnCadastralArea.InnerText = unit.Square.ToString();
                    xnReal_Estate.AppendChild(xnCadastralArea);

                    if (unit.PropertyType_Code == PropertyTypes.Stead)
                    {
                        string value_attr_3 = "";
                        if (DataExportCommon.GetObjectAttribute(unit, 3, out value_attr_3)) //Код категории земель из ГКН
                        {
                            XmlNode xnAssignation = _xmlFile.CreateElement("Category");
                            DataExportCommon.AddAttribute(_xmlFile, xnAssignation, "Name", value_attr_3);
                            xnReal_Estate.AppendChild(xnAssignation);
                        }

                        value_attr_3 = "";
                        if (DataExportCommon.GetObjectAttribute(unit, 4, out value_attr_3)) //Код Вид использования по документам
                        {
                            XmlNode xnUtilization = _xmlFile.CreateElement("Utilization");
                            DataExportCommon.AddAttribute(_xmlFile, xnUtilization, "Name_doc", value_attr_3);
                            xnReal_Estate.AppendChild(xnUtilization);
                        }
                    }
                    else
                    {
                        #region Assignation
                        XmlNode xnAssignation = _xmlFile.CreateElement("Assignation");
                        string value_attr_5 = "";
                        if (DataExportCommon.GetObjectAttribute(unit, 5, out value_attr_5)) //Код Вид использования по классификатору
                        {
                            if (unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Building)
                            {
                                XmlNode xnAssignation_Building = _xmlFile.CreateElement("Assignation_Building");
                                XmlNode xnAss_Building = _xmlFile.CreateElement("Ass_Building");
                                xnAss_Building.InnerText = value_attr_5;
                                xnAssignation_Building.AppendChild(xnAss_Building);
                                xnAssignation.AppendChild(xnAssignation_Building);
                            }
                            if (unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Pllacement)
                            {
                                XmlNode xnAssignation_Flat = _xmlFile.CreateElement("Assignation_Flat");
                                XmlNode xnAss_Flat = _xmlFile.CreateElement("Ass_Flat");
                                xnAss_Flat.InnerText = value_attr_5;
                                xnAssignation_Flat.AppendChild(xnAss_Flat);
                                xnAssignation.AppendChild(xnAssignation_Flat);
                            }
                            if ((unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Construction) ||
                                (unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.UncompletedBuilding))
                            {
                                XmlNode xnFormalized_Constr_Uncompleted = _xmlFile.CreateElement("Formalized_Constr_Uncompleted");
                                xnFormalized_Constr_Uncompleted.InnerText = value_attr_5;
                                xnAssignation.AppendChild(xnFormalized_Constr_Uncompleted);
                            }
                            if (value_attr_5 != "005002002999")

                                xnReal_Estate.AppendChild(xnAssignation);
                        }
                        #endregion
                    }

                    XmlNode xnCadastralLocation = _xmlFile.CreateElement("Location");

                    XmlNode xnCadastralRegion = _xmlFile.CreateElement("Region");
                    xnCadastralRegion.InnerText = region_rf;
                    xnCadastralLocation.AppendChild(xnCadastralRegion);

                    string value_attr = "";
                    if (DataExportCommon.GetObjectAttribute(unit, 600, out value_attr))  //Код Адрес
                    {
                        XmlNode xnCadastralNote = _xmlFile.CreateElement("Note");
                        xnCadastralNote.InnerText = value_attr;
                        xnCadastralLocation.AppendChild(xnCadastralNote);
                    }
                    value_attr = "";
                    if (DataExportCommon.GetObjectAttribute(unit, 8, out value_attr))  //Код Местоположение
                    {
                        XmlNode xnCadastralOther = _xmlFile.CreateElement("Other");
                        xnCadastralOther.InnerText = value_attr;
                        xnCadastralLocation.AppendChild(xnCadastralOther);
                    }

                    xnReal_Estate.AppendChild(xnCadastralLocation);
                    XmlNode xnDate_valuation = _xmlFile.CreateElement("Date_valuation");
                    xnDate_valuation.InnerText = ConfigurationManager.AppSettings["ucAppDate"].ParseToDateTime().ToString("yyyy-MM-dd");
                    xnReal_Estate.AppendChild(xnDate_valuation);

                    xnReal_Estates.AppendChild(xnReal_Estate);
                }

                xnEvaluation_Group.AppendChild(xnReal_Estates);
                xnOther.AppendChild(xnEvaluation_Group);
                xnAppraise.AppendChild(xnOther);
                #endregion
            }

            xnPackage.AppendChild(xnAppraise);
            #endregion

            _parent_node.AppendChild(xnPackage);
        }
    }

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
                DEKOGroup.AddXmlPackage(xmlFile, xnLandValuation, _group_unit, dictNodes, ""); //"Выгрузка XML для ФД"

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
	            list_bads.AddRange(CalcXMLFromVuon(num_pp, units, _doc, out List<ActOpredel> list_act_out, out List<OMInstance> list_doc_out, setting.DirectoryName, zipFile));
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
	            long id = SaveReportDownload.SaveReport(fileName, stream, OMUnit.GetRegisterId(), reportExtension: "zip");
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

        private static string[] CalcXMLFromVuon(int _num_pp, List<OMUnit> _units, OMInstance _doc_out,
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

                    DEKOResponseDoc.XmlVuonExport(group_unit, unit, file_name_common, doc_in, _doc_out, dictNodes, zipFile);
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

    /// <summary>
    /// Класс разнообразной выгрузки в Excel результатов Кадастровой оценки.
    /// </summary>
    public class DEKODifferent : IKoUnloadResult
    {
        /// <summary>
        /// Выгрузка Таблица 4. Группировка объектов недвижимости
        /// </summary>
        /// <param name="setting"></param>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable04)]
        public static List<ResultKoUnloadSettings> ExportToXls4(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
	        var progressMessage = "Группировка объектов недвижимости";
	        var taskCounter = 0;
	        var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var res = new List<ResultKoUnloadSettings>();
            foreach (long taskId in setting.TaskFilter)
            {
                List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == taskId && x.CadastralCost > 0).OrderBy(x => x.CadastralNumber).SelectAll().Execute();
                if (units_all.Count == 0) return res;

                List<OMUnit> units_curr = new List<OMUnit>();
                int num_pp = 0;
                int count_curr = 0;
                int count_file = 0;
                string message = "";
                int count_all = units_all.Count();
                string cad_num_curr = "";
                string cad_num = units_all[0].CadastralNumber.Substring(0, 5); // Номер кадастрового района первого объекта

                foreach (OMUnit unit in units_all)
                {
                    cad_num_curr = unit.CadastralNumber.Substring(0, 5);
                    if (cad_num_curr != cad_num)
                    {  // Если начались объекты из другого кадастрового района, то предыдущие сохраняем в файл 

                        count_file++;
                        var fileResult = SaveExcel4(units_curr, ref num_pp, count_file, cad_num, setting.DirectoryName,
                            taskId, message);
                        res.Add(fileResult);
                        units_curr.Clear();
                        cad_num = cad_num_curr;
                    }
                    units_curr.Add(unit);

                    progress = (count_curr * 100 / count_all + taskCounter * 100) / setting.TaskFilter.Count;
                    setProgress(progress, progressMessage: progressMessage);
                    KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }

                if (units_curr.Count > 0)
                {
                    count_file++;
                    var fileResult = SaveExcel4(units_curr, ref num_pp, count_file, cad_num_curr, setting.DirectoryName, taskId, message);
                    res.Add(fileResult);
                }

                taskCounter++;
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return res;
        }

        private static ResultKoUnloadSettings SaveExcel4(List<OMUnit> _units_curr, ref int _num_pp, int _count_file,
                                       string _cad_num, string _dir_name, long _taskid, string _mess)
        {
            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 3;
            int count_cells = 0;
            HeaderExcel4(sheet_edit, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            for (int i = 0; i < _units_curr.Count; i++)
            {
                if (_units_curr[i].GroupId != -1)
                {
                    OMGroup group = OMGroup.Where(x => x.Id == _units_curr[i].GroupId).SelectAll().ExecuteFirstOrDefault();

                    _num_pp++;
                    List<object> objarrs = new List<object>();
                    objarrs.Add(_num_pp);
                    objarrs.Add(_units_curr[i].PropertyType);
                    objarrs.Add(_units_curr[i].CadastralNumber);
                    objarrs.Add(group.Number);
                    objarrs.Add(group.GroupAlgoritm);

                    for (int f = 0; f < count_cells; f++)
                    {
                        objvals[curindval, f] = objarrs[f];
                    }

                    if (curindval >= 99)
                    {
                        DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                        curindval = -1;
                        objvals = new object[100, count_cells];
                    }
                    curindval++;
                    start_rows++;
                }
            }
            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            string file_name = "Task_" + _taskid + "_Таблица 4.Группировка объектов недвижимости"
                                         + " " + _cad_num.Replace(":", "_")
                                         + "." + _count_file.ToString().PadLeft(5, '0');

            long id =  SaveReportDownload.SaveReportExcel(file_name, excel_edit, OMUnit.GetRegisterId());
            
            //excel_edit.Save(_dir_name + "\\" + file_name + ".xlsx"); //temp
            
            return new ResultKoUnloadSettings
		    {
			    FileId = id,
			    FileName = file_name,
			    TaskId = _taskid
		    };
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 4
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel4(ExcelWorksheet _sheet, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№ п/п");
            objcaps1.Add("Тип");
            objcaps1.Add("Кадастровый номер");
            objcaps1.Add("Номер подгруппы");
            objcaps1.Add("Метод расчета");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");
            objcaps2.Add("4");
            objcaps2.Add("5");

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(50 * 256);
            widths_cells.Add(50 * 256);

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Таблица 4. Группировка объектов недвижимости");
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[1].Height = 4 * 256;
        }

        /// <summary>
        /// Выгрузка  Таблица 5. Модельная стоимость и Таблица 5. Метод УПКС
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable05)]
        public static List<ResultKoUnloadSettings> ExportToXls5(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
	        var progressMessage = "Модельная стоимость и Метод УПКС";
	        var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var res = new List<ResultKoUnloadSettings>();
			// Выбираем все группы
			List<OMGroup> groups = OMGroup.Where(x => x.ParentId == -1).SelectAll().Execute();
            int    num_group    = 0;
            int    num_subgroup = 0;
            int countAll = groups.Count;
            string message      = "";
            foreach (OMGroup group in groups)
            {
                num_group++;
                num_subgroup = 0;
                // Выбираем все подгруппы группы
                List<OMGroup> subgroups = OMGroup.Where(x => x.ParentId == group.Id).SelectAll().Execute();
                foreach (OMGroup subgroup in subgroups)
                {
                    num_subgroup++;
                    message = "Группа (" + num_group.ToString() + "-" + groups.Count().ToString() + ")"
                              + " - подгруппа (" + num_subgroup.ToString() + "-" + subgroups.Count().ToString() + ")";

                    var taskCounter = 0;
                    foreach (long taskId in setting.TaskFilter)
                    {
                        //Выбираем объекты данной подгруппы и ID задачи на оценку
                        List<OMUnit> units = OMUnit.Where(x => x.GroupId == subgroup.Id && x.TaskId == taskId && x.CadastralCost > 0).SelectAll().Execute();
                        if (units.Count == 0)
                        {
                            setProgress(100, true, progressMessage);
                            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
                            return res;
                        }

                        if ((subgroup.GroupAlgoritm_Code == KoGroupAlgoritm.Model) || (subgroup.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon))
                        {
	                        var fileResult = SaveExcel5Model(units, subgroup, setting.DirectoryName, taskId, message);
                            res.Add(fileResult);
                        }
                        else
                        {
	                        var fileResult = SaveExcel5Upksz(units, subgroup, setting.DirectoryName, taskId, message);
                            res.Add(fileResult);
                        }
                        taskCounter++;
                        progress = ((taskCounter*100/setting.TaskFilter.Count + num_subgroup * 100 )
                            / subgroups.Count + num_group * 100) / groups.Count;
                        setProgress(progress, progressMessage: progressMessage);
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                }
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return res;
        }

        private static ResultKoUnloadSettings SaveExcel5Model(List<OMUnit> _units, OMGroup _subgroup, string _dir_name, long _taskid, string _message)
        {
            OMModel model = OMModel.Where(x => x.GroupId == _subgroup.Id && x.IsActive.Coalesce(false) == true).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
            {
	            return new ResultKoUnloadSettings(true);
            }

            if (model.ModelFactor.Count == 0)
                model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id && x.AlgorithmType_Code == model.AlgoritmType_Code).SelectAll().Execute();

            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 4;
            int count_cells = 0;
            int num_pp = 0;
            HeaderExcel5Model(sheet_edit, _subgroup, model, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            foreach (OMUnit unit in _units)
            {
                List<object> objarrs = new List<object>();
                objarrs.Add(++num_pp);
                objarrs.Add(unit.CadastralBlock.Substring(0, 5));
                objarrs.Add(unit.PropertyType);
                objarrs.Add(unit.CadastralNumber);
                string value_attr = "";
                DataExportCommon.GetObjectAttribute(unit, 600, out value_attr);
                objarrs.Add(value_attr);

                #region Получили реестр Id группы, реестр, где лежат ее факторы
                int? factorReestrId = OMGroup.GetFactorReestrId(_subgroup);
                //Получаем список факторов группы и их значения
                List<CalcItem> FactorValuesGroup = new List<CalcItem>();
                DataTable data = RegisterStorage.GetAttributes((int)unit.Id, factorReestrId.Value);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        FactorValuesGroup.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString(), row.ItemArray[7].ParseToString()));
                    }
                }
                #endregion

                foreach (OMModelFactor factor in model.ModelFactor)
                {
                    bool findf = false;
                    string value_item = string.Empty;//TODO: значение фактора для данного объекта
                    CalcItem factor_item = FactorValuesGroup.Find(x => x.FactorId == factor.FactorId);
                    if (factor_item != null)
                    {
                        findf = true;
                        value_item = factor_item.Value;
                    }
                    objarrs.Add((findf) ? value_item : "");
                }

                objarrs.Add(unit.Square.ToString());
                objarrs.Add(unit.Upks.ToString());
                objarrs.Add(unit.CadastralCost.ToString());

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }

                curindval++;
                start_rows++;
            }

            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            string file_name = "Task_" + _taskid + "_Таблица 5. Модельная стоимость"
                                         + " " + DataExportCommon.GetFullNumberGroup(_subgroup);
            long id = SaveReportDownload.SaveReportExcel(file_name, excel_edit, OMUnit.GetRegisterId());

            return new ResultKoUnloadSettings
            {
	            FileId = id,
	            FileName = file_name,
	            TaskId = _taskid
            };

        }

        /// <summary>
        /// Сохранение в Excel "Таблица 5. Метод УПКС".
        /// </summary>
        /// <param name="_subgroup"></param>
        private static ResultKoUnloadSettings SaveExcel5Upksz(List<OMUnit> _units, OMGroup _subgroup, string _dir_name, long _taskid, string _message)
        {
            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 3;
            int count_cells = 0;
            int num_pp = 0;
            HeaderExcel5Upksz(sheet_edit, _subgroup, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            foreach (OMUnit unit in _units)
            {
                List<object> objarrs = new List<object>();
                objarrs.Add(++num_pp);
                objarrs.Add(unit.PropertyType);
                objarrs.Add(unit.CadastralNumber);
                objarrs.Add(unit.CadastralBlock);
                objarrs.Add(unit.CadastralBlock.Substring(0, 5));
                objarrs.Add(unit.Square.ToString());
                objarrs.Add(unit.Upks.ToString());
                objarrs.Add(unit.CadastralCost.ToString());

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }

                curindval++;
                start_rows++;
            }

            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            string file_name =  "Task_" + _taskid + "\\Таблица 5. Метод УПКС"
                                         + " " + DataExportCommon.GetFullNumberGroup(_subgroup);
            long id = SaveReportDownload.SaveReportExcel(file_name, excel_edit, OMUnit.GetRegisterId());

			return new ResultKoUnloadSettings
			{
				FileName = file_name,
				FileId = id,
				TaskId = _taskid
			};
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 5. Модельная стоимость
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel5Model(ExcelWorksheet _sheet, OMGroup _sub_group, OMModel _model, ref int _count_cells)
        {
            List<object> objcaps2 = new List<object>();
            objcaps2.Add("№п/п");
            objcaps2.Add("Номер кадастрового района");
            objcaps2.Add("Вид объекта недвижимости");
            objcaps2.Add("Кадастровый номер объекта недвижимости");
            objcaps2.Add("Адрес (местоположение) объекта недвижимости");
            foreach (OMModelFactor factor in _model.ModelFactor)
            {
                RegisterAttribute attribute_factor = RegisterCache.GetAttributeData((int)(factor.FactorId));
                objcaps2.Add(attribute_factor.Name);
            }
            objcaps2.Add("Площадь, кв.м");
            objcaps2.Add("УПКС, руб./кв.м");
            objcaps2.Add("Кадастровая стоимость, руб.");

            List<object> objcaps3 = new List<object>();
            for (int i = 0; i < objcaps2.Count; i++)
                objcaps3.Add(i + 1);

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(40 * 256);
            for (int i = 0; i < _model.ModelFactor.Count; i++)
                widths_cells.Add(30 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);

            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[2].Height = 5 * 256;

            _count_cells = objcaps2.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Таблица 5. Результаты моделирования " + DataExportCommon.GetFullNameGroup(_sub_group), true);
            DataExportCommon.MergeCell(_sheet, 1, 1, 5, 4+_model.ModelFactor.Count(), "Значения ценообразующих факторов");
            _sheet.Cells.GetSubrangeAbsolute(1, 0, 2, 0).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 1, 2, 1).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 2, 2, 2).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 3, 2, 3).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 4, 2, 4).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 5 + _model.ModelFactor.Count(), 2, 5 + _model.ModelFactor.Count()).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 6 + _model.ModelFactor.Count(), 2, 6 + _model.ModelFactor.Count()).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 7 + _model.ModelFactor.Count(), 2, 7 + _model.ModelFactor.Count()).Merged = true;
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 3, objcaps3.ToArray(), widths_cells.ToArray(), true, true, false);
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 5. Метод УПКС
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel5Upksz(ExcelWorksheet _sheet, OMGroup _sub_group, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№п/п");
            objcaps1.Add("Вид объекта недвижимости");
            objcaps1.Add("Кадастровый номер объекта недвижимости");
            objcaps1.Add("Кадастровый квартал");
            objcaps1.Add("Кадастровый район");
            objcaps1.Add("Площадь, кв.м");
            objcaps1.Add("УПКС, руб./кв.м");
            objcaps1.Add("Кадастровая стоимость, руб.");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");
            objcaps2.Add("4");
            objcaps2.Add("5");
            objcaps2.Add("6");
            objcaps2.Add("7");
            objcaps2.Add("8");

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Результаты расчета кадастровой стоимости объектов недвижимости (метод УПКС) подгруппы " + DataExportCommon.GetFullNameGroup(_sub_group), true);
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 5 * 256;
            _sheet.Rows[1].Height = 5 * 256;
        }

        /// <summary>
        /// Выгрузка файлов Excel "Таблица 7. Обобщенные показатели по кадастровым районам"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable07)]
        public static List<ResultKoUnloadSettings> ExportToXls7(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
	        var progressMessage = "Обобщенные показатели по кадастровым районам";
	        var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var result = new List<ResultKoUnloadSettings>();
            int num_save = 0;
            int count_group = (setting.UnloadParcel) ?13:16;
            List<GeneralizedValuesUPKSZ> list_statistics = new List<GeneralizedValuesUPKSZ>(count_group);

            #region Собираем статистику
            if (setting.UnloadParcel)
            {   //Выгружаем Земельные участки 
                var taskCounter = 0;
                foreach (long taskId in setting.TaskFilter)
                {
                    var unitCounter = 0;
                    List<OMUnit> units_zu = OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == PropertyTypes.Stead && x.CadastralCost > 0).OrderBy(x => x.CadastralBlock).SelectAll().Execute();
                    foreach (OMUnit unit in units_zu)
                    {
                        OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                        if (!group.Number.IsNullOrEmpty())
                        {
                            int num_group = Convert.ToInt32(group.Number.Substring(0, 1));
                            CalculationStat7(ref list_statistics, unit, num_group, count_group);
                            num_save++;
                        }

                        unitCounter++;
                        progress = (unitCounter * 70 / units_zu.Count + taskCounter * 70) / setting.TaskFilter.Count;
                        setProgress(progress, progressMessage: progressMessage);
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                    taskCounter++;
                }
            }
            else
            {   //Выгружаем объекты ОКС
                List<PropertyTypes> prop_types = new List<PropertyTypes>()
                {
                    PropertyTypes.Building,
                    PropertyTypes.Pllacement,
                    PropertyTypes.Construction,
                    PropertyTypes.OtherMore,
                    PropertyTypes.Parking,
                };

                int num_prop = 0;
                foreach (PropertyTypes prop_type in prop_types)
                {
                    num_prop++;
                    var taskCounter = 0;
                    foreach (long taskId in setting.TaskFilter)
                    {
                        var unitCounter = 0;
                        List<OMUnit> units_oks = OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == prop_type && x.CadastralCost > 0).OrderBy(x => x.CadastralBlock).SelectAll().Execute();
                        foreach (OMUnit unit in units_oks)
                        {
                            OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                            if (group != null)
                            {
                                if (!group.Number.IsNullOrEmpty())
                                {
                                    int num_group = Convert.ToInt32(group.Number.Substring(0, 1));
                                    CalculationStat7(ref list_statistics, unit, num_group, count_group);
                                    num_save++;
                                }
                            }

                            unitCounter++;
                            progress = (unitCounter * 70 / units_oks.Count + taskCounter * 70 / setting.TaskFilter.Count + (num_prop - 1) * 70) / prop_types.Count;
                            setProgress(progress, progressMessage: progressMessage);
                            KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                        }
                        taskCounter++;
                    }
                }
            }
            #endregion

            setProgress(70, progressMessage: "Обобщенные показатели по кадастровым районам - средние и итого");
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 70);
            #region Пересчитываем средние и всего
            Console.WriteLine("Пересчитываем средние и итого ...");
            GeneralizedValuesUPKSZ stat_total = new GeneralizedValuesUPKSZ(count_group);
            stat_total.CadastralArea = "Итого по Москве";
            var statsCount = 0;
            foreach (GeneralizedValuesUPKSZ stat in list_statistics)
            {
                stat_total.CountObj += stat.CountObj;
                for(int i = 0; i < count_group; i++)
                {
                    if (stat.MinAvgMax[3, i] != 0)
                    {
                        stat_total.MinAvgMax[0, i]  = (stat_total.MinAvgMax[0, i] == -1)
                                                      ? stat.MinAvgMax[0, i]
                                                      : Math.Min(stat_total.MinAvgMax[0, i], stat.MinAvgMax[0, i]);
                              stat.MinAvgMax[1, i]  = stat.MinAvgMax[1, i] / stat.MinAvgMax[3, i];
                        stat_total.MinAvgMax[1, i] += stat.MinAvgMax[1, i];
                        stat_total.MinAvgMax[2, i]  = Math.Max(stat_total.MinAvgMax[2, i], stat.MinAvgMax[2, i]);
                    }
                }

                statsCount++;
                progress = 70+(statsCount*20/list_statistics.Count);
                setProgress(progress, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
            }
            // Расчет Средние ИТОГО
            for (int i = 0; i < count_group; i++)
            {
                stat_total.MinAvgMax[1, i] = stat_total.MinAvgMax[1, i] / list_statistics.Count;
                progress = 90 + (i * 10 / count_group);
                setProgress(progress, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
            }

            list_statistics.Add(stat_total);
            #endregion

            #region Формирование отчета
            Console.WriteLine("Формирование отчета ...");
            var fileResult = SaveExcel7(list_statistics, setting.UnloadParcel, count_group, setting.DirectoryName,
	            setting.TaskFilter.FirstOrDefault());
            result.Add(fileResult);
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
			setProgress(100, true, "Обобщенные показатели по кадастровым районам - Формирование отчета");

            return result;

			#endregion
        }

        private static void CalculationStat7(ref List<GeneralizedValuesUPKSZ> _list_statistics, OMUnit _unit, int _num, int _count_group)
        {
            bool is_find = false;
            string cad_area = _unit.CadastralBlock.Substring(0, 5);

            foreach (var statistic in _list_statistics)
            {
                if (statistic.CadastralArea == cad_area)
                {
                    statistic.CountObj++;
                    statistic.MinAvgMax[0, _num - 1] = (statistic.MinAvgMax[0, _num - 1] == -1)
                                                       ? (double)_unit.Upks
                                                       : Math.Min(statistic.MinAvgMax[0, _num - 1], (double)_unit.Upks);
                    statistic.MinAvgMax[1, _num - 1] += (double)_unit.Upks;
                    statistic.MinAvgMax[2, _num - 1] = Math.Max(statistic.MinAvgMax[2, _num - 1], (double)_unit.Upks);
                    statistic.MinAvgMax[3, _num - 1]++;
                    is_find = true;
                    break;
                }
            }
            if (!is_find)
            {   //Добавить новый кадастровый район
                GeneralizedValuesUPKSZ statistic_new = new GeneralizedValuesUPKSZ(_count_group);
                statistic_new.CadastralArea = cad_area;
                statistic_new.CountObj = 1;
                statistic_new.MinAvgMax[0, _num - 1] = (double)_unit.Upks; //Минимальное
                statistic_new.MinAvgMax[1, _num - 1] = (double)_unit.Upks; //Сумма УПКСЗ, потом запишется среднее
                statistic_new.MinAvgMax[2, _num - 1] = (double)_unit.Upks; //Максимальное
                statistic_new.MinAvgMax[3, _num - 1] = 1;                  //Количество объектов этой группы. Для расчета среднего УПКСЗ
                _list_statistics.Add(statistic_new);
            }
        }

        private static ResultKoUnloadSettings SaveExcel7(List<GeneralizedValuesUPKSZ> _statistics, bool _is_parsel, int _count_group, string _dir_name, long firrsTaskId)
        {
            FileStream fileStream = Core.ConfigParam.Configuration.GetFileStream((_is_parsel) ?"Table7_zu": "Table7_oks", ".xlsx", "ExcelTemplates");
            ExcelFile excel_edit = ExcelFile.Load(fileStream, GemBox.Spreadsheet.LoadOptions.XlsxDefault);
            var sheet_edit = excel_edit.Worksheets[0];

            int num_pp = 0;
            int start_rows = 7;
            foreach (GeneralizedValuesUPKSZ stat in _statistics)
            {
                object[,] objvals = new object[3, _count_group+1];
                objvals[0, 0] = "Минимальное";
                objvals[1, 0] = "Среднее";
                objvals[2, 0] = "Максимальное";
                for (int i = 1; i <= _count_group; i++)
                {
                    objvals[0, i] = (stat.MinAvgMax[0, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[0, i - 1].ToString();
                    objvals[1, i] = (stat.MinAvgMax[1, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[1, i - 1].ToString();
                    objvals[2, i] = (stat.MinAvgMax[2, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[2, i - 1].ToString();
                }
                num_pp++;
                DataExportCommon.AddRow(sheet_edit, start_rows, 3, objvals);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows+2, 0, 0, num_pp.ToString()       , false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows+2, 1, 1, stat.CadastralArea      , false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows+2, 2, 2, stat.CountObj.ToString(), false);
                start_rows += 3;
            }

            //string path_name = _dir_name + "\\Table7";
            //if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name =  "Таблица 7. Обобщенные показатели результатов расчета кадастровой стоимости по кадастровым районам города Москвы"
                                         + "." + ((_is_parsel) ? "ЗУ" : "ОКС");

            long id = SaveReportDownload.SaveReportExcel(file_name, excel_edit, OMUnit.GetRegisterId());

			return new ResultKoUnloadSettings
			{
				FileId = id,
				FileName = file_name,
				TaskId = firrsTaskId
			};
            //excel_edit.Save(file_name);
        }

        /// <summary>
        /// Выгрузка файлов Excel "Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable08)]
        public static List<ResultKoUnloadSettings> ExportToXls8(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
	        var progressMessage = "Минимальные, максимальные, средние УПКС по кадастровым кварталам";
	        var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var result = new List<ResultKoUnloadSettings>();
            int num_save = 0;
            int count_group = (setting.UnloadParcel) ? 13 : 16;
            List<GeneralizedValuesUPKSZ> list_statistics = new List<GeneralizedValuesUPKSZ>(count_group);

            #region Собираем статистику
            if (setting.UnloadParcel)
            {  
                //Выгружаем Земельные участки 
                var taskCounter = 0;
                foreach (long taskId in setting.TaskFilter)
                {
                    var unitCounter = 0;
                    List<OMUnit> units_zu = OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == PropertyTypes.Stead && x.CadastralCost > 0).OrderBy(x => x.CadastralBlock).SelectAll().Execute();
                    foreach (OMUnit unit in units_zu)
                    {
                        OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                        if (!group.Number.IsNullOrEmpty())
                        {
                            int num_group = Convert.ToInt32(group.Number.Substring(0, 1));
                            CalculationStat8(ref list_statistics, unit, num_group, count_group);
                            num_save++;
                        }

                        unitCounter++;
                        progress = (unitCounter * 70 / units_zu.Count + taskCounter * 70) / setting.TaskFilter.Count;
                        setProgress(progress, progressMessage: progressMessage);
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                    taskCounter++;
                }
            }
            else
            {   //Выгружаем объекты ОКС
                List<PropertyTypes> prop_types = new List<PropertyTypes>()
                {
                    PropertyTypes.Building,
                    PropertyTypes.Pllacement,
                    PropertyTypes.Construction,
                    PropertyTypes.OtherMore,
                    PropertyTypes.Parking,
                };

                int num_prop = 0;
                foreach (PropertyTypes prop_type in prop_types)
                {
                    num_prop++;
                    var taskCounter = 0;
                    foreach (long taskId in setting.TaskFilter)
                    {
                        var unitCounter = 0;
                        List<OMUnit> units_oks = OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == prop_type && x.CadastralCost > 0).OrderBy(x => x.CadastralBlock).SelectAll().Execute();
                        foreach (OMUnit unit in units_oks)
                        {
                            OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                            if (group != null)
                            {
                                if (!group.Number.IsNullOrEmpty())
                                {
                                    int num_group = Convert.ToInt32(group.Number.Substring(0, 1));
                                    CalculationStat8(ref list_statistics, unit, num_group, count_group);
                                    num_save++;
                                }
                            }

                            unitCounter++;
                            progress = (unitCounter * 70 / units_oks.Count + taskCounter * 70 / setting.TaskFilter.Count + (num_prop - 1) * 70) / prop_types.Count;
                            setProgress(progress, progressMessage: progressMessage);
                            KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                        }
                        taskCounter++;
                        
                    }
                }
            }
            #endregion

            #region Пересчитываем средние
            Console.WriteLine("Пересчитываем средние ...");
            setProgress(70, progressMessage: "Минимальные, максимальные, средние УПКС по кадастровым кварталам - средние");
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
            var statsCount = 0;
            foreach (GeneralizedValuesUPKSZ stat in list_statistics)
            {
                for (int i = 0; i < count_group; i++)
                {
                    if (stat.MinAvgMax[3, i] != 0)
                    {
                        stat.MinAvgMax[1, i] = stat.MinAvgMax[1, i] / stat.MinAvgMax[3, i];
                    }
                }
                statsCount++;
                progress = 70 + (statsCount * 30 / list_statistics.Count);
                setProgress(progress, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
            }
            #endregion

            #region Формирование отчета
            Console.WriteLine("Формирование отчета ...");
            var fileResult = SaveExcel8(list_statistics, setting.UnloadParcel, count_group, setting.DirectoryName,
	            setting.TaskFilter.FirstOrDefault());
            result.Add(fileResult);
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, "Минимальные, максимальные, средние УПКС по кадастровым кварталам - Формирование отчета");


            return result;

            #endregion
        }

        private static void CalculationStat8(ref List<GeneralizedValuesUPKSZ> _list_statistics, OMUnit _unit, int _num, int _count_group)
        {
            bool is_find = false;
            string cad_area = _unit.CadastralBlock.Substring(0, 5);

            foreach (var statistic in _list_statistics)
            {
                if (statistic.CadastralBlok == _unit.CadastralBlock)
                {
                    statistic.CountObj++;
                    statistic.MinAvgMax[0, _num - 1] = (statistic.MinAvgMax[0, _num - 1] == -1)
                                                       ? (double)_unit.Upks
                                                       : Math.Min(statistic.MinAvgMax[0, _num - 1], (double)_unit.Upks);
                    statistic.MinAvgMax[1, _num - 1] += (double)_unit.Upks;
                    statistic.MinAvgMax[2, _num - 1] = Math.Max(statistic.MinAvgMax[2, _num - 1], (double)_unit.Upks);
                    statistic.MinAvgMax[3, _num - 1]++;
                    is_find = true;
                    break;
                }
            }
            if (!is_find)
            {   //Добавить новый кадастровый квартал
                GeneralizedValuesUPKSZ statistic_new = new GeneralizedValuesUPKSZ(_count_group);
                statistic_new.CadastralArea = cad_area;
                statistic_new.CadastralBlok = _unit.CadastralBlock;
                statistic_new.CountObj = 1;
                statistic_new.MinAvgMax[0, _num - 1] = (double)_unit.Upks; //Минимальное
                statistic_new.MinAvgMax[1, _num - 1] = (double)_unit.Upks; //Сумма УПКСЗ, потом запишется среднее
                statistic_new.MinAvgMax[2, _num - 1] = (double)_unit.Upks; //Максимальное
                statistic_new.MinAvgMax[3, _num - 1] = 1;                  //Количество объектов этой группы. Для расчета среднего УПКСЗ
                _list_statistics.Add(statistic_new);
            }
        }

        private static ResultKoUnloadSettings SaveExcel8(List<GeneralizedValuesUPKSZ> _statistics, bool _is_parsel, int _count_group, string _dir_name, long firstTaskId)
        {
            FileStream fileStream = Core.ConfigParam.Configuration.GetFileStream((_is_parsel) ? "Table8_zu" : "Table8_oks", ".xlsx", "ExcelTemplates");
            ExcelFile excel_edit = ExcelFile.Load(fileStream, GemBox.Spreadsheet.LoadOptions.XlsxDefault);
            var sheet_edit = excel_edit.Worksheets[0];

            int num_pp = 0;
            int start_rows = 7;
            foreach (GeneralizedValuesUPKSZ stat in _statistics)
            {
                object[,] objvals = new object[3, _count_group + 1];
                objvals[0, 0] = "Минимальное";
                objvals[1, 0] = "Среднее";
                objvals[2, 0] = "Максимальное";
                for (int i = 1; i <= _count_group; i++)
                {
                    objvals[0, i] = (stat.MinAvgMax[0, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[0, i - 1].ToString();
                    objvals[1, i] = (stat.MinAvgMax[1, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[1, i - 1].ToString();
                    objvals[2, i] = (stat.MinAvgMax[2, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[2, i - 1].ToString();
                }
                num_pp++;
                DataExportCommon.AddRow(sheet_edit, start_rows, 4, objvals);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows + 2, 0, 0, num_pp.ToString(), false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows + 2, 1, 1, stat.CadastralArea, false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows + 2, 2, 2, stat.CadastralBlok, false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows + 2, 3, 3, stat.CountObj.ToString(), false);
                start_rows += 3;
            }

			// string path_name = _dir_name + "\\Table8";
			// if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name ="Таблица 8. Обобщенные показатели результатов расчета кадастровой стоимости по кадастровым кварталам города Москвы"
                                         + "." + ((_is_parsel) ? "ЗУ" : "ОКС");

            long id = SaveReportDownload.SaveReportExcel(file_name, excel_edit, OMUnit.GetRegisterId());
            // excel_edit.Save(file_name);

			return new ResultKoUnloadSettings
			{
				FileName = file_name,
				FileId = id,
				TaskId = firstTaskId
			};
        }

        /// <summary>
        /// Выгрузка файлов Excel "Таблица 9. Результаты определения КС"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable09)]
        public static List<ResultKoUnloadSettings> ExportToXls9(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
	        var progressMessage = "Результаты определения КС";
	        var taskCounter = 0;
	        var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            List < ResultKoUnloadSettings > res = new List<ResultKoUnloadSettings>();

            foreach (long taskId in setting.TaskFilter)
            {
                List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == taskId && x.CadastralCost > 0).OrderBy(x => x.CadastralNumber).SelectAll().Execute();
                if (units_all.Count == 0) return new List<ResultKoUnloadSettings>();

                List<OMUnit> units_curr = new List<OMUnit>();
                int num_pp = 0;
                int count_curr = 0;
                int count_file = 0;
                string message = "";
                int count_all = units_all.Count();
                string cad_num_curr = "";
                string cad_num = units_all[0].CadastralNumber.Substring(0, 5); // Номер кадастрового района первого объекта

                foreach (OMUnit unit in units_all)
                {
                    cad_num_curr = unit.CadastralNumber.Substring(0, 5);
                    if (cad_num_curr != cad_num)
                    {  // Если начались объекты из другого кадастрового района, то предыдущие сохраняем в файл 
                        count_file++;
                        var fileResult = SaveExcel9(units_curr, ref num_pp, count_file, cad_num, setting.DirectoryName,
                            taskId, message);
                        res.Add(fileResult);
                        units_curr.Clear();
                        cad_num = cad_num_curr;
                    }
                    units_curr.Add(unit);

                    count_curr++;
                    progress = (count_curr * 100 / count_all + taskCounter * 100) / setting.TaskFilter.Count;
                    setProgress(progress, progressMessage: progressMessage);
                    KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }
                if (units_curr.Count > 0)
                {
                    count_file++;
                    var fileResult = SaveExcel9(units_curr, ref num_pp, count_file, cad_num_curr, setting.DirectoryName,
	                    taskId, message);
                    res.Add(fileResult);
                }

                taskCounter++;
            }
			KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
			setProgress(100, true,progressMessage);

			return res;
        }

        private static ResultKoUnloadSettings SaveExcel9(List<OMUnit> _units_curr, ref int _num_pp, int _count_file, string _cad_num, string _dir_name, long _taskid, string _mess)
        {
            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 3;
            int count_cells = 0;
            HeaderExcel9(sheet_edit, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            for (int i = 0; i < _units_curr.Count; i++)
            {
                string message = _mess + " -- сохраняется " + (i + 1).ToString() + " из " + _units_curr.Count.ToString();
                Console.WriteLine(message);

                _num_pp++;
                List<object> objarrs = new List<object>();
                objarrs.Add(_num_pp);
                objarrs.Add(_units_curr[i].CadastralNumber);
                objarrs.Add(_units_curr[i].CadastralCost);

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }
                curindval++;
                start_rows++;
            }
            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            //string path_name = _dir_name + "\\Table9\\Task_" + _taskid.ToString();
            //if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name = "Task_" + _taskid + "Таблица 9. Результаты определения КС"
                                         + " " + _cad_num.Replace(":", "_") + "." + _count_file.ToString().PadLeft(5, '0');
            //excel_edit.Save(file_name);

            long id = SaveReportDownload.SaveReportExcel(file_name, excel_edit, OMUnit.GetRegisterId());

			return new ResultKoUnloadSettings
			{
				FileName = file_name,
				FileId = id,
				TaskId = _taskid
			};
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 9
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel9(ExcelWorksheet _sheet, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№ п/п");
            objcaps1.Add("Кадастровый номер объекта недвижимости");
            objcaps1.Add("Кадастровая стоимость объекта недвижимости, руб.");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(40 * 256);
            widths_cells.Add(40 * 256);

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Результаты определения кадастровой стоимости объектов недвижимости на территории субъекта Российской Федерации город Москва", true);
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[1].Height = 4 * 256;
        }

        /// <summary>
        /// Выгрузка файлов Excel "Таблица 10. Результаты ГКО"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable10)]
        public static List<ResultKoUnloadSettings> ExportToXls10(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
	        var progressMessage = "Результаты ГКО";
	        var taskCounter = 0;
	        var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            List<ResultKoUnloadSettings> res = new List<ResultKoUnloadSettings>();

            foreach (long taskId in setting.TaskFilter)
            {
                List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == taskId && x.CadastralCost > 0).OrderBy(x => x.CadastralNumber).SelectAll().Execute();
                if (units_all.Count == 0) return new List<ResultKoUnloadSettings>();

                List<OMUnit> units_curr = new List<OMUnit>();
                int num_pp = 0;
                int count_curr = 0;
                int count_file = 0;
                string message = "";
                int count_all = units_all.Count();
                string cad_num_curr = "";
                string cad_num = units_all[0].CadastralNumber.Substring(0, 5); // Номер кадастрового района первого объекта

                foreach (OMUnit unit in units_all)
                {
                    cad_num_curr = unit.CadastralNumber.Substring(0, 5);
                    if (cad_num_curr != cad_num)
                    {  // Если начались объекты из другого кадастрового района, то предыдущие сохраняем в файл 

                        count_file++;
                        var fileResult = SaveExcel10(units_curr, ref num_pp, count_file, cad_num, setting.DirectoryName,
                            taskId, message);
                        res.Add(fileResult);
                        units_curr.Clear();
                        cad_num = cad_num_curr;
                    }
                    units_curr.Add(unit);

                    count_curr++;
                    progress = (count_curr * 100 / count_all + taskCounter * 100) / setting.TaskFilter.Count;
                    setProgress(progress, progressMessage: progressMessage);
                    KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }
                if (units_curr.Count > 0)
                {
                    count_file++;
                    var fileResult = SaveExcel10(units_curr, ref num_pp, count_file, cad_num_curr,
	                    setting.DirectoryName, taskId, message);
                    res.Add(fileResult);
                }
                taskCounter++;
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return res;
        }

        private static ResultKoUnloadSettings SaveExcel10(List<OMUnit> _units_curr, ref int _num_pp, int _count_file, string _cad_num,
                                        string _dir_name, long _taskid, string _mess)
        {
            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 3;
            int count_cells = 0;
            HeaderExcel10(sheet_edit, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            for (int i = 0; i < _units_curr.Count; i++)
            {
                string message = _mess + " -- сохраняется " + (i + 1).ToString() + " из " + _units_curr.Count.ToString();
                Console.WriteLine(message);

                _num_pp++;
                List<object> objarrs = new List<object>();
                objarrs.Add(_num_pp);
                objarrs.Add(_cad_num);
                objarrs.Add(_units_curr[i].CadastralNumber);
                objarrs.Add(_units_curr[i].PropertyType);
                objarrs.Add(_units_curr[i].Square);
                objarrs.Add(_units_curr[i].Upks);
                objarrs.Add(_units_curr[i].CadastralCost);

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }

                curindval++;
                start_rows++;
            }
            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            //string path_name = _dir_name + "\\Table10\\Task_" + _taskid.ToString();
            //if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name = "Task_" + _taskid + "Таблица 10. Результаты ГКО"
                                         + " " + _cad_num.Replace(":", "_") + "." + _count_file.ToString().PadLeft(5, '0');
            //excel_edit.Save(file_name);

            long id = SaveReportDownload.SaveReportExcel(file_name, excel_edit, OMUnit.GetRegisterId());

			return new ResultKoUnloadSettings
			{
				FileName = file_name,
				FileId = id,
				TaskId = _taskid
			};
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 10
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel10(ExcelWorksheet _sheet, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№ п/п");
            objcaps1.Add("Кадастровый район");
            objcaps1.Add("Кадастровый номер объекта недвижимости");
            objcaps1.Add("Вид объекта недвижимости");
            objcaps1.Add("Общая площадь объекта недвижимости, кв.м.");
            objcaps1.Add("УПКС объекта недвижимости, руб./кв.м.");
            objcaps1.Add("Кадастровая стоимость объекта недвижимости, руб.");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");
            objcaps2.Add("4");
            objcaps2.Add("5");
            objcaps2.Add("6");
            objcaps2.Add("7");

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(17 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(50 * 256);
            widths_cells.Add(50 * 256);

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Результаты государственной кадастровой оценки объектов недвижимости");
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[1].Height = 4 * 256;
        }

        /// <summary>
        /// Выгрузка в Excel "Таблица 11. Сводные результаты по КР"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable11)]
        public static List<ResultKoUnloadSettings> ExportToXls11(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
	        var progressMessage = "Сводные результаты по КР";
	        var taskCounter = 0;
	        var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            List <ResultKoUnloadSettings> res = new List<ResultKoUnloadSettings>();

            foreach (long taskId in setting.TaskFilter)
            {
                List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == taskId).OrderBy(x => x.CadastralNumber).SelectAll().Execute();
                if (units_all.Count == 0) return new List<ResultKoUnloadSettings>();

				List<PropertyTypes> prop_types = new List<PropertyTypes>()
                {
                    PropertyTypes.Building,
                    PropertyTypes.Company,
                    PropertyTypes.Construction,
                    PropertyTypes.Other,
                    PropertyTypes.OtherMore,
                    PropertyTypes.Parking,
                    PropertyTypes.Pllacement,
                    PropertyTypes.Stead,
                    PropertyTypes.UncompletedBuilding,
                    PropertyTypes.UnitedPropertyComplex
                };

                string message = "";
                int ind_type = 0;
                foreach (PropertyTypes prop_type in prop_types)
                {
                    ind_type++;
                    message = prop_type.GetEnumDescription() + " (" + ind_type.ToString() + "-" + prop_types.Count().ToString() + ")";
                    List<OMUnit> units_types = units_all.Where(x => x.PropertyType_Code == prop_type && x.CadastralCost > 0).OrderBy(x => x.CadastralNumber).ToList();
                    if (units_types.Count == 0) continue;

                    List<OMUnit> units_curr = new List<OMUnit>();
                    int num_pp = 0;
                    int count_curr = 0;
                    int count_file = 0;
                    int count_all = units_types.Count();
                    string message1 = "";
                    string cad_num_curr = "";
                    string cad_num = units_types[0].CadastralNumber.Substring(0, 5); // Номер кадастрового района первого объекта

                    foreach (OMUnit unit in units_types)
                    {
                        cad_num_curr = unit.CadastralNumber.Substring(0, 5);
                        if (cad_num_curr != cad_num)
                        {  // Если начались объекты из другого кадастрового района, то предыдущие сохраняем в файл 

                            count_file++;
                            var fileResult = SaveExcel11(prop_type, units_curr, ref num_pp, count_file, cad_num,
                                setting.DirectoryName, taskId, message1);
                            res.Add(fileResult);
                            units_curr.Clear();
                            cad_num = cad_num_curr;
                        }
                        units_curr.Add(unit);

                        count_curr++;
                        progress = ((count_curr * 100 / count_all + (ind_type - 1) * 100) / prop_types.Count + taskCounter * 100) / setting.TaskFilter.Count;
                        setProgress(progress, progressMessage: progressMessage);
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                    if (units_curr.Count > 0)
                    {
                        count_file++;
                        var fileResult = SaveExcel11(prop_type, units_curr, ref num_pp, count_file, cad_num_curr,
	                        setting.DirectoryName, taskId, message1);
                        res.Add(fileResult);
                    }
                }
                taskCounter++;
            }
			KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
			setProgress(100, true, progressMessage);

            return res;
        }

        private static ResultKoUnloadSettings SaveExcel11(PropertyTypes _prop_type, List<OMUnit> _units_curr, ref int _num_pp,
                                        int _count_file, string _cad_num, string _dir_name, long _taskid, string _mess)
        {
            int start_rows = 3;
            int count_cells = 0;

            ExcelFile excelTemplate = new ExcelFile();
            ExcelWorksheet mainWorkSheet = excelTemplate.Worksheets.Add("КО");
            HeaderExcel11(mainWorkSheet, _cad_num, _prop_type, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            for (int i = 0; i < _units_curr.Count; i++)
            {
                string message = _mess + " -- сохраняется " + (i + 1).ToString() + " из " + _units_curr.Count.ToString();
                Console.WriteLine(message);

                _num_pp++;
                List<object> objarrs = new List<object>();
                objarrs.Add(_num_pp);
                objarrs.Add(_units_curr[i].CadastralNumber);
                objarrs.Add(_units_curr[i].PropertyType );      //TYPE_OBJECT_STR
                objarrs.Add(_units_curr[i].Square );            //SQUARE_OBJECT

                string value_attr = "";
                long number_attr = -1;

                if (_units_curr[i].PropertyType_Code == PropertyTypes.Stead) number_attr = 1;
                else number_attr = 19;
                if (number_attr > 0)
                    DataExportCommon.GetObjectAttribute(_units_curr[i], number_attr, out value_attr);   //Код - Наименование объекта
                objarrs.Add(value_attr);                        //NAME_OBJECT
                value_attr = "";
                number_attr = -1;
                if (_units_curr[i].PropertyType_Code == PropertyTypes.Building) number_attr = 14;
                else if (_units_curr[i].PropertyType_Code == PropertyTypes.Construction) number_attr = 22;
                else if (_units_curr[i].PropertyType_Code == PropertyTypes.Pllacement) number_attr = 23;
                if (number_attr > 0)
                    DataExportCommon.GetObjectAttribute(_units_curr[i], number_attr, out value_attr);   //Код  - Назначение
                objarrs.Add(value_attr);                        //USE_OBJECT);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 600, out value_attr);               //Код 600 - Адрес 
                objarrs.Add(value_attr);                        //ADRESS_OBJECT
                objarrs.Add("770000000000");                    //KLADR_OBJECT                          //TODO надо определять КЛАДР
                value_attr = "";
                if (_units_curr[i].PropertyType_Code == PropertyTypes.Pllacement)
                    DataExportCommon.GetObjectAttribute(_units_curr[i], 604, out value_attr);           //Код 604 - Кадастровый номер здания или сооружения, в котором расположено помещение
                objarrs.Add(value_attr);                        //KN_PARENT
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 8, out value_attr);                 //Код 8 - Местоположение
                objarrs.Add(value_attr);                        //PLACE_OBJECT
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 601, out value_attr);               //Код 601 - Кадастровый квартал 
                objarrs.Add(value_attr);                        //KN_KK
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 602, out value_attr);               //Код 602 - Земельный участок 
                objarrs.Add(value_attr);                        //KN_ZU
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 15, out value_attr);                //Код 15 - Год постройки
                objarrs.Add(value_attr);                        //YEAR_BUILT
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 16, out value_attr);                //Код 16 - Год ввода в эксплуатацию
                objarrs.Add(value_attr);                        //YEAR_USED
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 17, out value_attr);                //Код 17 - Количество этажей
                objarrs.Add(value_attr);                        //FLOORS
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 18, out value_attr);                //Код 18 - Количество подземных этажей
                objarrs.Add(value_attr);                        //UNDER_FLOORS
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 24, out value_attr);                //Код 24 - Этаж (для помещения)
                objarrs.Add(value_attr);                        //LEVEL_FLAT
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 21, out value_attr);                //Код 21 - Материал стен
                objarrs.Add(value_attr);                        //WALL
                if (_prop_type == PropertyTypes.Pllacement)
                {
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_units_curr[i], 14, out value_attr);            //Код 14 - Назначение здания
                    objarrs.Add(value_attr);
                    objarrs.Add("");   //TODO разобраться что сюда записывать, должно "Наименование здания"
                }
                if (_prop_type == PropertyTypes.Construction)
                {
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_units_curr[i], 44, out value_attr);            //Код 44 - Характеристика (протяженность)
                    objarrs.Add(value_attr);                    //KEY_PARAMETR
                }
                if (_prop_type == PropertyTypes.UncompletedBuilding)
                {
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_units_curr[i], 46, out value_attr);            //Код 46 - Процент готовности
                    objarrs.Add(value_attr);                    //ProcentOrName
                }
                objarrs.Add(_units_curr[i].Upks);
                objarrs.Add(_units_curr[i].CadastralCost);

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(mainWorkSheet, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }

                curindval++;
                start_rows++;
            }
            if (curindval != 0)
            {
                DataExportCommon.AddRow(mainWorkSheet, start_rows - curindval, objvals, curindval);
            }

            string path_name = _dir_name + "\\Table11\\Task_" + _taskid.ToString();
            if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name = "Task_" + _taskid + "Таблица 11. Сводные результаты по КР"
                                         + " " + _cad_num.Replace(":", "_")
                                         + "." + _prop_type.GetEnumDescription()
                                         + "." + _count_file.ToString().PadLeft(5, '0');

            long id = SaveReportDownload.SaveReportExcel(file_name, excelTemplate, OMUnit.GetRegisterId());

            //excelTemplate.Save(file_name);
            return new ResultKoUnloadSettings
            {
	            FileName = file_name,
	            FileId = id,
	            TaskId = _taskid
            };
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 11
        /// </summary>
        /// <param name="_sheet"></param>
        private static void HeaderExcel11(ExcelWorksheet _sheet, string _cad_num, PropertyTypes _prop_type, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№ п/п");
            objcaps1.Add("КН");
            objcaps1.Add("Тип");
            objcaps1.Add("Площадь");
            objcaps1.Add("Наименование");
            objcaps1.Add("Назначение");
            objcaps1.Add("Адрес");
            objcaps1.Add("КЛАДР");
            objcaps1.Add("КН родителя");
            objcaps1.Add("Местоположение");
            objcaps1.Add("Кадастровый квартал");
            objcaps1.Add("Кадастровый номер земельного участка");
            objcaps1.Add("Год постройки");
            objcaps1.Add("Год ввода в эксплуатацию");
            objcaps1.Add("Кол-во этажей");
            objcaps1.Add("Подземных этажей");
            objcaps1.Add("Этаж (для помещения)");
            objcaps1.Add("Материал стен");
            if (_prop_type == PropertyTypes.Pllacement)
            {
                objcaps1.Add("Назначение здания");
                objcaps1.Add("Наименование здания");
            }
            if (_prop_type == PropertyTypes.Construction)
            {
                objcaps1.Add("Характеристика");
            }
            if (_prop_type == PropertyTypes.UncompletedBuilding)
            {
                objcaps1.Add("Процент готовности");
            }
            objcaps1.Add("УПКС объекта недвижимости, руб./кв.м.");
            objcaps1.Add("Кадастровая стоимость объекта недвижимости, руб.");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");
            objcaps2.Add("4");
            objcaps2.Add("5");
            objcaps2.Add("6");
            objcaps2.Add("7");
            objcaps2.Add("8");
            objcaps2.Add("9");
            objcaps2.Add("10");
            objcaps2.Add("11");
            objcaps2.Add("12");
            objcaps2.Add("13");
            objcaps2.Add("14");
            objcaps2.Add("15");
            objcaps2.Add("16");
            objcaps2.Add("17");
            objcaps2.Add("18");
            if (_prop_type == PropertyTypes.Pllacement) {
                objcaps2.Add("19");
                objcaps2.Add("20");
                objcaps2.Add("21");
                objcaps2.Add("22");
            } else if (_prop_type == PropertyTypes.Construction) {
                objcaps2.Add("19");
                objcaps2.Add("20");
                objcaps2.Add("21");
            } else if (_prop_type == PropertyTypes.UncompletedBuilding) {
                objcaps2.Add("19");
                objcaps2.Add("20");
                objcaps2.Add("21");
            } else {
                objcaps2.Add("19");
                objcaps2.Add("20");
            }

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(17 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(30 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);

            if (_prop_type == PropertyTypes.Pllacement) {
                widths_cells.Add(15 * 256);
                widths_cells.Add(15 * 256);
                widths_cells.Add(15 * 256);
                widths_cells.Add(20 * 256);
            } else if (_prop_type == PropertyTypes.Construction) {
                widths_cells.Add(15 * 256);
                widths_cells.Add(15 * 256);
                widths_cells.Add(20 * 256);
            } else if (_prop_type == PropertyTypes.UncompletedBuilding) {
                widths_cells.Add(15 * 256);
                widths_cells.Add(15 * 256);
                widths_cells.Add(20 * 256);
            } else {
                widths_cells.Add(15 * 256);
                widths_cells.Add(20 * 256);
            }

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (" + _cad_num + ")");
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[1].Height = 4 * 256;
        }
    }

    /// <summary>
    /// Класс выгрузки в Word ответных документов по отдельным объектам.
    /// </summary>
    public class DEKODocOtvet
    {
        /// <summary>
        /// Экспорт в Word - ответные документы по объектам.
        /// </summary>
        public static Stream ExportToDoc(OMUnit _unit)
        {
            if (_unit == null)
            {
                throw new Exception($"Не найдена единица оценки.");
            }

            // Проверка КС, игнорируем = 0
            if (CheckNullEmpty.CheckDecimal(_unit.CadastralCost.Value) == 0)
            {
                throw new Exception($"У единицы оценки не определена кадастровая стоимость.");
            }

            OMTask task = OMTask.Where(x => x.Id == _unit.TaskId).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                throw new Exception($"Не найдено задание на оценку для Единицы оценки с Id = '{_unit.Id}'");
            }
            OMGroup group_unit = OMGroup.Where(x => x.Id == _unit.GroupId).SelectAll().ExecuteFirstOrDefault();
            if (group_unit == null)
            {
                throw new Exception($"Не найдена группа для Единицы оценки с Id = '{_unit.Id}'");
            }
            OMGroup calc_group = null;
            OMUnit calc_unit = null;
            if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.FlatOnBuilding)//  subgroup.Type_SubGroup == 9)
            {
                // Определяем объект, по которому был расcчитан _unit, и у этого объекта берем группу
                GetCalcGroupFromUnit(_unit, task.EstimationDate, out calc_unit, out calc_group);
            }

            try
            {
                Console.WriteLine("Выгрузка Otvet ...");
                ComponentInfo.SetLicense("DN-2020Feb27-7KwYZ43Y+lJR5YBeTLWW8F+pXE9Aj3uU2ru+Jk1lHxILYWKJhT8TZQLCztE1qx6MQx/MnAR8BGGPC6QpAmIgm2EZh0w==A");
                var document = new DocumentModel();
                document.DefaultParagraphFormat.SpaceAfter = 0;

                Console.WriteLine(" - создание таблицы ...");
                #region Создание таблицы      
                int idx_row = -1;
                int count_cells = 4;
                var table = new Table(document);
                table.Columns.Add(new TableColumn(96f / 2.54f * 1.50f));
                table.Columns.Add(new TableColumn(96f / 2.54f * 7.25f));
                table.Columns.Add(new TableColumn(96f / 2.54f * 2.95f));
                table.Columns.Add(new TableColumn(96f / 2.54f * 5.80f));
                table.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
                table.TableFormat.Alignment = HorizontalAlignment.Center;
                table.TableFormat.Borders.ClearBorders();

                #endregion
                Console.WriteLine(" - создание таблицы выполнено");

                #region Сбор и формирование данных по объекту
                Console.WriteLine(" - заголовок ...");
                string strDateApp = (task.EstimationDate != null) ? task.EstimationDate.Value.ToString("dd.MM.yyyy") : "-";
                string strActReq_01_06 = "-";
                string strActReq_01_07 = "-";
                string strActReq_01_10 = "01.01.2019";
                string str_3_0 = "-";
                string value_attr = "";

                #region Нашли и записали в список входящий документ
                KoNoteType doc_status = task.NoteType_Code;
                #endregion
                OMInstance doc_out = OMInstance.Where(x => x.Id == _unit.ResponseDocId).SelectAll().ExecuteFirstOrDefault();
                if (doc_out != null)
                {
                    switch (doc_status)
                    {
                        case KoNoteType.Day:      // STATUS_DOC == СтатусДокумента.Ежедневка)
                            strActReq_01_10 = "-";
                            strActReq_01_06 = DataExportCommon.GetFullNameDoc(doc_out);
                            strActReq_01_07 = "Ковалев Д.В." + "$ $" + "Капитонов К.С.";
                            str_3_0 = "Кадастровая стоимость объекта недвижимости определена в соответствии с положениями статьи 16 Федерального закона от 03 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке».";

                            break;
                        case KoNoteType.Petition:      //СтатусДокумента.Обращение
                            strActReq_01_10 = "-";
                            str_3_0 = "Кадастровая стоимость объекта недвижимости определена в соответствии с положениями статьи 21 Федерального закона от 03 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке».";

                            break;
                        default:     //СтатусДокумента.Иное
                            str_3_0 = "Кадастровая стоимость объекта недвижимости определена в соответствии с положениями части 9 статьи 24 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке».";
                            break;
                    }

                }

                #region 0. Заголовок:
                // Добавляем символ разделитель строки "$". Потом он обрабатывается в методе DataExportCommon.SetTextToCellDoc
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "Приложение", 12, HorizontalAlignment.Right, false, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "Разъяснения, связанные с определением кадастровой стоимости", 14, HorizontalAlignment.Center, false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "Государственное бюджетное учреждение города Москвы" + "$" + "«Городской центр имущественных платежей и жилищного страхования»", 12, HorizontalAlignment.Center, false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "«__» _______ " + DateTime.Now.Year.ToString() + " г.",
                    "№______________", 12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "На основании обращения от _____________, поступившего" + /*Environment.NewLine +*/ "  " +
                    "_________________ г., сообщаем относительно определения кадастровой" + "$" +
                    "стоимости объекта недвижимости с кадастровым номером " + _unit.CadastralNumber, 12, HorizontalAlignment.Center, false, true);
                #endregion
                Console.WriteLine(" - заголовок выполнено");

                #region 1. Общие сведения:
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0], "1. Общие сведения:", 12, HorizontalAlignment.Left, false, true);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "№ п/п",
                     "Наименование показателя",
                     "Значение, описание",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center,
                     true, true);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.1",
                     "Кадастровая стоимость",
                     _unit.CadastralCost.ToString()?.Replace(",", "."),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.2",
                     "Дата, по состоянию на которую определена кадастровая стоимость (дата определения кадастровой стоимости)",
                     strDateApp,
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);

                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                if (_unit.PropertyType_Code == PropertyTypes.Stead)
                    DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                         "1.3",
                         "Реквизиты отчета об итогах государственной кадастровой оценки, составленного в соответствии со статьей 14 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке»",
                         "Отчет от 19.11.2018 № 2/2018 «Об итогах государственной кадастровой оценки земельных участков(категория земель «земли населенных пунктов»), расположенных на территории города Москвы по состоянию на 01.01.2018»",
                         12,
                         HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                         true, false);
                else
                    DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                         "1.3",
                         "Реквизиты отчета об итогах государственной кадастровой оценки, составленного в соответствии со статьей 14 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке»",
                         "Отчет от 19.11.2018 № 1/2018 «Об итогах государственной кадастровой оценки зданий, помещений, объектов незавершенного строительства, машино-мест и сооружений, расположенных на территории города Москвы по состоянию на 01.01.2018»",
                         12,
                         HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                         true, false);

                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                string sss = "http://cadastre.gcgs.ru/state/" + "$" + "https://rosreestr.ru/wps/portal/p/cc_ib_portal_services/cc_ib_ais_fdgko";
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.4",
                     "Полный электронный адрес размещения отчета об итогах государственной кадастровой оценки в информационно - телекоммуникационной сети «Интернет»",
                     "http://cadastre.gcgs.ru/state/" + "$" + "https://rosreestr.ru/wps/portal/p/cc_ib_portal_services/cc_ib_ais_fdgko",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.5",
                     "Сведения о работнике бюджетного учреждения, созданного субъектом Российской Федерации и наделенного полномочиями, связанными с определением кадастровой стоимости, подготовившем отчет об итогах государственной кадастровой оценки",
                     "Ковалев Д.В." + "$" + "Капитонов К.С.",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.6",
                     "Реквизиты акта определения кадастровой стоимости, составленного в соответствии со статьей 16 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке»",
                     strActReq_01_06,
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.7",
                     "Сведения о работнике бюджетного учреждения, созданного субъектом Российской Федерации и наделенного полномочиями, связанными с определением кадастровой стоимости, определившем кадастровую стоимость в соответствии со статьей 16 Федерального закона от 3 июля 2016 г. № 237-ФЗ «О государственной кадастровой оценке»",
                     strActReq_01_07,
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.8",
                     "Дата внесения сведений о кадастровой стоимости в Единый государственный реестр недвижимости",
                     "-",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.9",
                     "Дата подачи заявления об оспаривании кадастровой стоимости, по результатам рассмотрения которого определена кадастровая стоимость по решению комиссии по рассмотрению споров о результатах определения кадастровой стоимости или по решению суда",
                     "-",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.10",
                     "Дата начала применения кадастровой стоимости, в том числе в случае изменения кадастровой стоимости по решению комиссии по рассмотрению споров о результатах определения кадастровой стоимости или по решению суда",
                     strActReq_01_10,
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "1.11",
                     "Сведения об органе, его местонахождении, официальном сайте в информационно-телекоммуникационной сети «Интернет», адресе электронной почты, контактных телефонах, в который следует обращаться в отношении исчисления налогов, исчисляемых от кадастровой стоимости объекта недвижимости",
                     "Управление Федеральной налоговой службы по г. Москве" + "$" +
                     "Адрес: 125284, г. Москва, Хорошевское шоссе, д. 12А" + "$" +
                     "https://www.nalog.ru" + "$" +
                     "Телефоны:" + "$" +
                     "Для справок: +7 (495) 400-67-90, +7(495) 400-67- 68" + "$" +
                     "Единый контакт-центр: 8-800-222-2222",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                #endregion

                #region  2. 
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                    "2.Кадастровая стоимость объекта недвижимости определена на основании следующей информации:",
                    12,
                    HorizontalAlignment.Left,
                    false, true);
                #endregion

                Console.WriteLine(" - характеристики ...");
                #region  2.1.  О  характеристиках объекта недвижимости, с использованием которых была определена его кадастровая стоимость:
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                     "2.1. О характеристиках объекта недвижимости, с использованием которых была определена его кадастровая стоимость:",
                     12,
                     HorizontalAlignment.Left,
                     false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);

                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "№ п/п",
                     "Наименование показателя",
                     "Значение, описание",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, true);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.1",
                     "Кадастровый номер объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(_unit.CadastralNumber),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.2",
                     "Вид объекта недвижимости (земельный участок, здание, сооружение, помещение, машино-место, объект незавершенного строительства, единый недвижимый комплекс, предприятие как имущественный комплекс или иной вид)",
                     CheckNullEmpty.CheckStringOut(_unit.PropertyType),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 600, out value_attr);               //Код 600 - Адрес 
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.3",
                     "Адрес объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 8, out value_attr);                 //Код 8 - Местоположение PLACE_OBJECT
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.4",
                     "Описание местоположения объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.5",
                     "Площадь (для земельного участка, здания, помещения или машино-места) или иная основная характеристика (протяженность, глубина, глубина залегания, площадь, объем, высота, площадь застройки - для сооружения, объекта незавершенного строительства) объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(_unit.Square.ToString()?.Replace(",", ".")),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 3, out value_attr); //Код 3 - Категория земель
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.6",
                     "Категория земель, к которой относится земельный участок, если объектом недвижимости является земельный участок",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 4, out value_attr); //Код 4 - Вид использования по документам
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.7",
                     "Вид разрешенного использования объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                long number_attr = -1;
                if (_unit.PropertyType_Code == PropertyTypes.Building) number_attr = 14;
                else if (_unit.PropertyType_Code == PropertyTypes.Construction) number_attr = 22;
                else if (_unit.PropertyType_Code == PropertyTypes.Pllacement) number_attr = 23;
                if (number_attr > 0)
                    DataExportCommon.GetObjectAttribute(_unit, number_attr, out value_attr); //Код  - Назначение
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.8",
                     "Назначение (для зданий, сооружений, помещения, единого недвижимого комплекса, предприятия как имущественного комплекса), проектируемое назначение (для объектов незавершенного строительства) объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 17, out value_attr); //Код 17 - Количество этажей
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.9",
                     "Этажность объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 21, out value_attr); //Код 21 - Материал стен
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.10",
                     "Материал наружных стен объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                //DataExportCommon.GetObjectAttribute(_unit, 21, out value_attr); //Код 21 - Материал стен
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.11",
                     "Обременения (ограничения) объекта недвижимости, использованные при определении кадастровой стоимости",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_unit, 46, out value_attr);            //Код 46 - Процент готовности
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.12",
                     "Степень готовности объекта незавершенного строительства в процентах",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                value_attr = "";
                //DataExportCommon.GetObjectAttribute(_unit, 21, out value_attr); //Код 21 - Материал стен
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.1.13",
                     "Иные сведения об объекте недвижимости, использованные при определении кадастровой стоимости",
                     CheckNullEmpty.CheckStringOut(value_attr),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                #endregion
                Console.WriteLine(" - характеристики выполнено");

                #region 2.2. О рынке недвижимости:
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                    "2.2. О рынке недвижимости:",
                    12,
                    HorizontalAlignment.Left,
                    false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "№ п/п",
                     "Наименование показателя",
                     "Значение, описание",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center,
                     true, true);

                //OMGroupToMarketSegmentRelation segment = OMGroupToMarketSegmentRelation.Where(x => x.GroupId == group_unit.Id).SelectAll().ExecuteFirstOrDefault();
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.2.1",
                     "Сегмент рынка объектов недвижимости, к которому отнесен объект недвижимости",
                     CheckNullEmpty.CheckStringOut(group_unit.MarketSegment),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.2.2",
                     "Краткая характеристика особенностей функционирования сегмента рынка объектов недвижимости, к которому отнесен объект недвижимости (с указанием на страницы отчета об итогах государственной кадастровой оценки, где содержится полная характеристика сегмента рынка объектов недвижимости, в том числе анализ рыночной информации о ценах сделок (предложений) в таком сегменте, затрат на строительство объектов недвижимости)",
                     CheckNullEmpty.CheckStringOut(group_unit.MarketSegmentFunctioningFeatures),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.2.3",
                     "Характеристика ценовой зоны, в которой находится объект недвижимости, в том числе характеристика типового объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(group_unit.PriceZoneCharacteristic),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);

                #endregion

                Console.WriteLine(" - факторы ...");
                #region 2.3. Факторы:
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                     "2.3. Перечень ценообразующих факторов, использованных для определения кадастровой стоимости объекта недвижимости, их значения и источники сведений о них:",
                     12,
                     HorizontalAlignment.Left,
                     false, true);

                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells);
                DataExportCommon.SetText4Doc(document, table.Rows[idx_row],
                     "№ п/п",
                     "Наименование",
                     "Значение",
                     "Источник",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center,
                     true, true);

                int pp = 0;

                #region Calc Group
                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.FlatOnBuilding)//  subgroup.Type_SubGroup == 9)
                {
                    if (calc_group != null)
                    {
                        OMModel model_calc = OMModel.Where(x => x.GroupId == calc_group.Id && x.IsActive.Coalesce(false) == true).SelectAll().ExecuteFirstOrDefault();
                        if (model_calc != null)
                        {
                            if (model_calc.ModelFactor.Count == 0)
                                model_calc.ModelFactor = OMModelFactor.Where(x => x.ModelId == model_calc.Id && x.AlgorithmType_Code == model_calc.AlgoritmType_Code).SelectAll().Execute();

                            foreach (OMModelFactor factor in model_calc.ModelFactor)
                            {
                                string attribute_name = "-";
                                long attribute_id = -1;
                                string attribute_value = "-";
                                string attribute_source = "-";

                                GetAttributeValue(_unit, calc_group, task, factor.FactorId, factor.SignMarket,
                                                   out attribute_id,
                                                   out attribute_name,
                                                   out attribute_value,
                                                   out attribute_source);

                                pp++;
                                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells);
                                DataExportCommon.SetText4Doc(document, table.Rows[idx_row],
                                     "2.3." + pp.ToString(),
                                     attribute_name,
                                     attribute_value,
                                     attribute_source,
                                     12,
                                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center, HorizontalAlignment.Center,
                                     true, false);
                            }
                        }
                        List<OMGroupFactor> group_factors = OMGroupFactor.Where(x => x.GroupId == calc_group.Id).SelectAll().Execute();
                        foreach (OMGroupFactor factor in group_factors)
                        {
                            string attribute_name = "-";
                            long attribute_id = -1;
                            string attribute_value = "-";
                            string attribute_source = "-";

                            GetAttributeValue(_unit, group_unit, task, factor.FactorId, (bool)factor.SignMarket,
                                               out attribute_id,
                                               out attribute_name,
                                               out attribute_value,
                                               out attribute_source);

                            pp++;
                            idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells);
                            DataExportCommon.SetText4Doc(document, table.Rows[idx_row],
                                 "2.3." + pp.ToString(),
                                 attribute_name,
                                 attribute_value,
                                 attribute_source,
                                 12,
                                 HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center, HorizontalAlignment.Center,
                                 true, false);
                        }

                    }
                }
                #endregion

                #region Group
                OMModel model = OMModel.Where(x => x.GroupId == group_unit.Id && x.IsActive.Coalesce(false) == true).SelectAll().ExecuteFirstOrDefault();
                if (model != null)
                {
                    if (model.ModelFactor.Count == 0)
                        model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id && x.AlgorithmType_Code == model.AlgoritmType_Code).SelectAll().Execute();

                    foreach (OMModelFactor factor in model.ModelFactor)
                    {
                        string attribute_name = "-";
                        long attribute_id = -1;
                        string attribute_value = "-";
                        string attribute_source = "-";

                        GetAttributeValue(_unit, group_unit, task, factor.FactorId, factor.SignMarket,
                                           out attribute_id,
                                           out attribute_name,
                                           out attribute_value,
                                           out attribute_source);

                        pp++;
                        idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells);
                        DataExportCommon.SetText4Doc(document, table.Rows[idx_row],
                             "2.3." + pp.ToString(),
                             attribute_name,
                             attribute_value,
                             attribute_source,
                             12,
                             HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center, HorizontalAlignment.Center,
                             true, false);
                    }
                }

                List<OMGroupFactor> gr_factors = OMGroupFactor.Where(x => x.GroupId == group_unit.Id).SelectAll().Execute();
                foreach (OMGroupFactor factor in gr_factors)
                {
                    string attribute_name = "-";
                    long attribute_id = -1;
                    string attribute_value = "-";
                    string attribute_source = "-";

                    GetAttributeValue(_unit, group_unit, task, factor.FactorId, (bool)factor.SignMarket,
                                       out attribute_id,
                                       out attribute_name,
                                       out attribute_value,
                                       out attribute_source);

                    pp++;
                    idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells);
                    DataExportCommon.SetText4Doc(document, table.Rows[idx_row],
                         "2.3." + pp.ToString(),
                         attribute_name,
                         attribute_value,
                         attribute_source,
                         12,
                         HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center, HorizontalAlignment.Center,
                         true, false);
                }
                #endregion

                #endregion
                Console.WriteLine(" - факторы выполнено");

                Console.WriteLine(" - методология расчета ...");
                #region 2.4.   Кадастровая   стоимость   объекта   недвижимости   определена  в соответствии со следующей методологией:
                string formula = string.Empty;
                string pr_kn = string.Empty;
                string parent_calc_number = (_unit.ParentCalcNumber == null) ? string.Empty : _unit.ParentCalcNumber;
                bool dd = false;
                bool jj = true;

                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.FlatOnBuilding)//  subgroup.Type_SubGroup == 9)
                {
                    if ((calc_unit != null) && ((parent_calc_number == calc_unit.CadastralNumber) || parent_calc_number == string.Empty))
                    {
                        formula = OMGroup.GetFormulaKoeff(calc_group, true, "УПКС здания, в котором расположено помещение(" + calc_unit.CadastralNumber + ")");
                        dd = true;
                    }
                    else
                    {
                        string[] kk = parent_calc_number.Split(':');
                        string calc_group_num = group_unit.Number;
                        if (kk.Length == 1)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", по субъекту)") +
                                "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 2)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом районе " + _unit.ParentCalcNumber + ")") +
                                "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 3)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом квартале " + _unit.ParentCalcNumber + ")") +
                                "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        jj = false;
                    }
                }
                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.AVG) //subgroup.Type_SubGroup == 10)
                {
                    string[] kk = parent_calc_number.Split(':');
                    string calc_group_num = group_unit.Number;
                    if (kk.Length == 1)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + _unit.ParentCalcNumber + ", по субъекту)") +
                            "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    if (kk.Length == 2)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + _unit.ParentCalcNumber + ", в кадастровом районе " + _unit.ParentCalcNumber + ")") +
                            "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    if (kk.Length == 3)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + _unit.ParentCalcNumber + ", в кадастровом квартале " + _unit.ParentCalcNumber + ")") +
                            "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                }
                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.UnComplited) //subgroup.Type_SubGroup == 11)
                {
                    string[] kk = parent_calc_number.Split(':');
                    string calc_group_num = group_unit.Number;
                    if (kk.Length == 1)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", по субъекту)") +
                            "*Степень готовности объекта незавершенного строительства=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    if (kk.Length == 2)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом районе " + _unit.ParentCalcNumber + ")") +
                            "*Степень готовности объекта незавершенного строительства=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    if (kk.Length == 3)
                    {
                        formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом квартале " + _unit.ParentCalcNumber + ")") +
                            "*Степень готовности объекта незавершенного строительства=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                }
                if (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.Min) //subgroup.Type_SubGroup == 12)
                {
                    string[] kk1 = parent_calc_number.Split('(');
                    if (kk1.Length > 1)
                    {
                        string ppkk = kk1[1].Replace(")", "").Replace(" ", "");
                        string[] kk = ppkk.Split(':');

                        string calc_group_num = group_unit.Number;
                        if (kk.Length == 1)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Минимальное значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", по субъекту)") +
                                "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 2)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Минимальное значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом районе " + ppkk + ")") +
                                "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 3)
                        {
                            formula = OMGroup.GetFormulaKoeff(group_unit, true, "(Минимальное значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом квартале " + ppkk + ")") +
                                "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                    }
                }
                if ((group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.Model) && jj)
                {
                    if (!dd)
                    {
                        formula = OMGroup.GetFormulaFull(group_unit, true) + "=" + _unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                    else
                    {
                        formula = formula + "$$" + "УПКС здания, в котором расположено помещение (" + pr_kn + ")=" +
                            OMGroup.GetFormulaFull(calc_group, false) + "=" + calc_unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                    }
                }
                if ((group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.AVG) && (group_unit.GroupAlgoritm_Code == KoGroupAlgoritm.FlatOnBuilding))
                {
                    if ((calc_unit != null) && ((parent_calc_number == calc_unit.CadastralNumber) || parent_calc_number == string.Empty))
                    {
                        string[] kk = parent_calc_number.Split(':');
                        string calc_group_num = calc_group.Number;
                        if (kk.Length == 1)
                        {
                            formula = formula + "$$" +
                                "УПКС здания, в котором расположено помещение (" + pr_kn + ")=" +
                                OMGroup.GetFormulaKoeff(calc_group, false, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", по субъекту)") +
                                "=" + calc_unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 2)
                        {
                            formula = formula + "$$" + "УПКС здания, в котором расположено помещение (" + pr_kn + ")=" +
                                OMGroup.GetFormulaKoeff(calc_group, false, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом районе " + calc_unit.CadastralNumber + ")") +
                                "=" + calc_unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                        if (kk.Length == 3)
                        {
                            formula = formula + "$$" + "УПКС здания, в котором расположено помещение (" + pr_kn + ")=" +
                                OMGroup.GetFormulaKoeff(calc_group, false, "(Среднее взвешенное по площади значение УПКС объектов, отнесенных к оценочным подгруппам: " + calc_group_num + ", в кадастровом квартале " + calc_unit.CadastralNumber + ")") +
                                "=" + calc_unit.Upks.ToString()?.Replace(',', '.') + " руб./кв.м" + "$$" + "Кадастровая стоимость = УПКС * Площадь";
                        }
                    }
                }

                string dopmodel = group_unit.ModelJustification;
                if (dopmodel != string.Empty)
                    formula = formula + " " + dopmodel;

                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, count_cells - 1);
                DataExportCommon.SetTextToCellDoc(document, table.Rows[idx_row].Cells[0],
                     "2.4. Кадастровая стоимость объекта недвижимости определена в соответствии со следующей методологией:",
                     12,
                     HorizontalAlignment.Left,
                     false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "№ п/п",
                     "Наименование показателя",
                     "Значение, описание",
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center,
                     true, true);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.4.1",
                     "Примененные подходы при определении кадастровой стоимости объекта недвижимости с обоснованием их выбора",
                     CheckNullEmpty.CheckStringOut(group_unit.AppliedApproachesInCadastralCost),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.4.2",
                     "Примененные методы оценки при определении кадастровой стоимости объекта недвижимости с обоснованием их выбора",
                     CheckNullEmpty.CheckStringOut(group_unit.AppliedEvaluationMethodsInCadastralCost),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.4.3",
                     "Способ определения кадастровой стоимости объекта недвижимости (массовая или индивидуальная оценка в отношении объектов недвижимости) с обоснованием его выбора",
                     CheckNullEmpty.CheckStringOut(group_unit.CadastralCostDetermingMethod),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.4.4",
                     "Модель определения кадастровой стоимости объекта недвижимости с обоснованием ее выбора",
                     CheckNullEmpty.CheckStringOut(formula),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.4.5",
                     "Сегмент объектов недвижимости, к которому относится объект недвижимости, с обоснованием его выбора",
                     CheckNullEmpty.CheckStringOut(group_unit.ObjectsSegment),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.4.6",
                     "Группа (подгруппа) объектов недвижимости, к которой относится объект недвижимости, с обоснованием ее выбора",
                     CheckNullEmpty.CheckStringOut(group_unit.ObjectsSubgroup),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 2, 3);
                DataExportCommon.SetText3Doc(document, table.Rows[idx_row],
                     "2.4.7",
                     "Краткое описание последовательности определения кадастровой стоимости объекта недвижимости",
                     CheckNullEmpty.CheckStringOut(group_unit.CadastralCostCalculationOrderDescription),
                     12,
                     HorizontalAlignment.Center, HorizontalAlignment.Left, HorizontalAlignment.Center,
                     true, false);

                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                #endregion
                Console.WriteLine(" - методология расчета выполнено");

                #region  3. 
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row], "3. Иная информация по запросу заявителя:", str_3_0, 12, HorizontalAlignment.Left, HorizontalAlignment.Center, false, true);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                #endregion

                #region  Подпись 
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "Начальник отдела по работе с разъяснениями",
                    " ",
                    12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "ГБУ «Центр имущественных платежей и жилищного страхования»",
                    "Н.А. Завьялова",
                    12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddEmptyRowToTableDoc(document, table, count_cells);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "Исполнитель:",
                    " ",
                    12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                idx_row = DataExportCommon.AddRowToTableDoc(document, table, count_cells, 0, 2);
                DataExportCommon.SetText2Doc(document, table.Rows[idx_row],
                    "Асоян А.С./Андреева И.В./Прусенкова О.В./Илюхин Б.В.",
                    " ",
                    12,
                    HorizontalAlignment.Left, HorizontalAlignment.Right,
                    false, false);
                #endregion

                #endregion

                Console.WriteLine(" - сохранение ...");
                #region Задаем параметры страницы и сохраняем таблицу и документ
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
                document.Save(stream, GemBox.Document.SaveOptions.DocxDefault);
                stream.Seek(0, SeekOrigin.Begin);
                #endregion
                Console.WriteLine(" - сохранение выполнено");
                Console.WriteLine("Выгрузка Otvet выполнено");

                return stream;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ErrorManager.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// Получить значения аттрибутов фактора
        /// </summary>
        public static void GetAttributeValue(OMUnit _unit, OMGroup _group, OMTask _task, long? _factor_id, bool _sign_market,
                                             out long attr_id, out string attr_name, out string attr_value, out string attr_source)
        {
            RegisterAttribute attribute_factor = RegisterCache.GetAttributeData((int)(_factor_id));

            attr_id = (attribute_factor != null) ? attribute_factor.Id : -1;
            attr_name = (attribute_factor != null) ? attribute_factor.Name : "-";
            attr_value = "-";
            attr_source = "-";

            int? factorReestrId = OMGroup.GetFactorReestrId(_group);
            List<CalcItem> FactorValuesGroup = new List<CalcItem>();
            DataTable data = (factorReestrId != null) ?
                              RegisterStorage.GetAttributes((int)_unit.Id, factorReestrId.Value) :
                              null;
            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
	                var numberValue = row.ItemArray[7].ParseToDecimalNullable();
                    if (numberValue.HasValue)
	                    FactorValuesGroup.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString(), numberValue.ToString().Replace(",", ".")));
                    else
	                    FactorValuesGroup.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString(), row.ItemArray[7].ParseToString()));
                }
                CalcItem factor_item = (FactorValuesGroup.Count > 0) ?
                                        FactorValuesGroup.Find(x => x.FactorId == _factor_id) :
                                        null;
                if (factor_item != null)
                {
                    attr_value = factor_item.Value;

                    //Ищем источник в OMFactorSetting в KO, если нет, то в ГБУ
                    OMFactorSettings factor_sett = OMFactorSettings.Where(x => x.FactorId == attribute_factor.Id).SelectAll().ExecuteFirstOrDefault();
                    attr_source = (factor_sett != null) ? factor_sett.Source : GetSourceAttributeFromGbu(_unit, attr_id, factor_item.Value, _task.EstimationDate);

                    #region Если есть метка, получаем результирущий коэффициент в подставляемое значение
                    if (_sign_market.ParseToBoolean())
                    {
                        List<OMMarkCatalog> MarkCatalogs = new List<OMMarkCatalog>();
                        MarkCatalogs.AddRange(OMMarkCatalog.Where(x => x.GroupId == _group.Id && x.FactorId == factor_item.FactorId).SelectAll().Execute());

                        OMMarkCatalog mc = null;
                        string temp_val = attr_value;
                        mc = MarkCatalogs.Find(x => x.ValueFactor.ToUpper() == temp_val.ToUpper().Replace('.', ','));
                        if (mc == null)
                            mc = MarkCatalogs.Find(x => x.ValueFactor.ToUpper() == temp_val.ToUpper().Replace(',', '.'));

                        if (mc != null)
                        {
                            attr_value = attr_value + " (подставляемое значение: " + mc.MetkaFactor.ToString().Replace(',', '.').Replace(".00000000000000000000", ".00") + ")";
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// Получить расчетную группц объекта оценки
        /// </summary>
        public static void GetCalcGroupFromUnit(OMUnit _unit, DateTime? _estimatedate, out OMUnit unit_out, out OMGroup group_out)
        {
            unit_out = null;
            group_out = null;

            string value_attr = "";
            DataExportCommon.GetObjectAttribute(_unit, 604, out value_attr);  //Код 604 - Кадастровый номер здания или сооружения, в котором расположено помещение

            List<OMUnit> units_parent = OMUnit.Where(x => x.CadastralNumber == value_attr).SelectAll().Execute();
            if (units_parent.Count == 0) return;

            DateTime? date_temp = new DateTime(2010, 1, 1);
            TimeSpan? date_near = _estimatedate - date_temp;
            foreach (OMUnit unit_par in units_parent)
            {
                OMTask task = OMTask.Where(x => x.Id == unit_par.TaskId).SelectAll().ExecuteFirstOrDefault();
                if (task != null)
                {
                    if (_estimatedate - task.EstimationDate < date_near)
                    {
                        date_near = _estimatedate - task.EstimationDate;
                        unit_out = unit_par;
                        group_out = OMGroup.Where(x => x.Id == unit_par.GroupId).SelectAll().ExecuteFirstOrDefault();
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Получить источник аттрибута объекта КО из ГБУ
        /// </summary>
        /// <param name="_unit">Объект КО</param>
        /// <param name="_id">Идентификатор аттрибута объекта КО</param>
        /// <param name="_value">Значение аттрибута объекта КО</param>
        /// <param name="_date">Дата задания на оценку</param>
        /// <returns></returns>
        public static string GetSourceAttributeFromGbu(OMUnit _unit, long _id, string _value, DateTime? _date)
        {
            string source_out = "-";

            OMTransferAttributes transferAttribute = OMTransferAttributes
                .Where(x => x.KoId == _id)
                .SelectAll()
                .ExecuteFirstOrDefault();

            if (transferAttribute != null)
            {
                List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(_unit.ObjectId.Value,
                    null,
                    new List<long> { transferAttribute.GbuId },
                    _date, isLight:true);

                if (attribs.Count > 0)
                {
	                var attribute_source = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == attribs.First().RegisterData.Id);
	                if (attribute_source != null)
	                {
		                source_out = attribute_source.Description;
	                }
                }
            }

            return source_out;
        }
    }


    /// <summary>
    /// Класс итоговой выгрузки результатов
    /// </summary>
    public class KOUnloadResult
    {
        private static readonly ILogger _log = Log.ForContext<KOUnloadResult>();
        public static List<KoUnloadResultType> GetKoUnloadResultTypes(KOUnloadSettings setting)
	    {
		    List<KoUnloadResultType> koUnloadResults = new List<KoUnloadResultType>();

		    var usedUnloadTypes = typeof(KOUnloadSettings).GetFields()
			    .Where(fieldInfo => fieldInfo.GetCustomAttributes(typeof(KoUnloadResultTypeAttribute), false).Length > 0
			                && (bool) fieldInfo.GetValue(setting))
			    .Select(fieldInfo =>
				    ((KoUnloadResultTypeAttribute) fieldInfo.GetCustomAttributes(typeof(KoUnloadResultTypeAttribute), false).First())
				    .UnloadType);
		    koUnloadResults.AddRange(usedUnloadTypes);

		    return koUnloadResults;
	    }

	    /// <summary>
        /// Выгрузка результатов
        /// </summary>
        public static List<ResultKoUnloadSettings> Unload(OMQueue queue, OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting)
        {
	        List<ResultKoUnloadSettings> result = new List<ResultKoUnloadSettings>();
            setting.DirectoryName = "";
            var counter = new UnloadCounter(setting, queue, 90);
            SetProgress setProgress = counter.ReportProgress;

            var koUnloadResultTypes = JsonConvert.DeserializeObject<List<KoUnloadResultType>>(unloadResultQueue.UnloadTypesMapping);
            var unloadResultMethodInfoDictionary = GetUnloadResultMethodInfoDictionary();

            _log.ForContext("UnloadTypesMapping", unloadResultQueue.UnloadTypesMapping)
                .Information("Текущее количество выгрузок {UnloadCurrentCount} из {UnloadTotalCount} ", unloadResultQueue.UnloadCurrentCount, unloadResultQueue.UnloadTotalCount);

            var unloadCurrentCount = 1;
            foreach (var koUnloadResultType in koUnloadResultTypes)
            {
              
                unloadResultQueue.UnloadCurrentCount = unloadCurrentCount;
	            unloadResultQueue.CurrentUnloadType_Code = koUnloadResultType;
	            unloadResultQueue.Save();
                try
                {
                    //TODO: сейчас не реализована Выгрузка истории по объектам
                    if (koUnloadResultType != KoUnloadResultType.UnloadHistory)
                    {
                        var unloadResult = (List<ResultKoUnloadSettings>)unloadResultMethodInfoDictionary[koUnloadResultType]
                            .Invoke(null, new object[] { unloadResultQueue, setting, setProgress });

                        result.AddRange(unloadResult);
                    }
                }
                catch (Exception ex) {
                    _log.ForContext("UnloadCurrentCount", unloadCurrentCount)
                        .Warning(ex, "Ошибка в процессе выгрузки {CurrentUnloadTypeCode}", unloadResultQueue.CurrentUnloadType_Code);
                }
                
                unloadCurrentCount++;
            }
            _log.Debug("Выгрузка результатов {ResultCount} из {UnloadCurrentCount}", result.Count, unloadCurrentCount);
            return result;
        }

	    private static Dictionary<KoUnloadResultType, MethodInfo> GetUnloadResultMethodInfoDictionary()
	    {
		    var koUnloadResultTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
			    .Where(x => typeof(IKoUnloadResult).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();

		    var methods = koUnloadResultTypes
			    .SelectMany(t => t.GetMethods())
			    .Where(m => m.GetCustomAttributes(typeof(KoUnloadResultActionAttribute), false).Length > 0)
			    .ToDictionary(
				    x => ((KoUnloadResultActionAttribute) x
					    .GetCustomAttributes(typeof(KoUnloadResultActionAttribute), false)
					    .First()).UnloadType, x => x);

		    return methods;
	    }

	    public static void SetCurrentProgress(OMUnloadResultQueue unloadResultQueue, long progress)
	    {
		    unloadResultQueue.CurrentUnloadProgress = progress;
		    unloadResultQueue.Save();
	    }
    }

    public interface IKoUnloadResult { }

    public class KoUnloadResultActionAttribute : Attribute
    {
	    public KoUnloadResultType UnloadType { get; }

	    public KoUnloadResultActionAttribute(KoUnloadResultType unloadType)
	    {
		    UnloadType = unloadType;
	    }
    }

    public delegate void SetProgress(int taskProgress, bool reportFinish = false, string progressMessage = "");

    class UnloadCounter
    {
        private int _currentTaskProgress;
        private int _completedTasks;
        private int _totalTasks;
        private int _maxValue;
        private int _totalProgress;
        private OMQueue _queue;
        public UnloadCounter(KOUnloadSettings settings, OMQueue queue, int maxValue)
        {
            _totalProgress = 0;
            _maxValue = maxValue;
            _queue = queue;
            var counter = 0;
            if (settings.UnloadChange) counter++;
            if (settings.UnloadTable04) counter++;
            if (settings.UnloadTable05) counter++;
            if (settings.UnloadTable07) counter++;
            if (settings.UnloadTable08) counter++;
            if (settings.UnloadTable09) counter++;
            if (settings.UnloadTable10) counter++;
            if (settings.UnloadTable11) counter++;
            if (settings.UnloadXML1) counter++;
            if (settings.UnloadXML2) counter++;
            if (settings.UnloadDEKOResponseDocExportToXml) counter++;
            if (settings.UnloadDEKOVuonExportToXml) counter++;
            _totalTasks = counter;
        }

        public void ReportProgress(int prog, bool endOfProcess, string progressMessage)
        {
            if (endOfProcess)
            {
                var logMessage = $"[{DateTime.Now.ToLongTimeString()}] Завершено - {progressMessage}";
                _currentTaskProgress = 0;
                _completedTasks++;
                WorkerCommon.LogState(_queue, logMessage);
            }
            else
            {
                if (_currentTaskProgress < prog)
                {
                    var logMessage = $"[{DateTime.Now.ToLongTimeString()}] {progressMessage} - {_currentTaskProgress}%";
                    _currentTaskProgress = prog;
                    WorkerCommon.LogState(_queue, logMessage);
                }
            }
            var progress = (_completedTasks * 100 + _currentTaskProgress) * _maxValue / (_totalTasks * 100);
            if (progress > _totalProgress)
            {
                var logMessage = $"[{DateTime.Now.ToLongTimeString()}] Общий прогресс выгрузки: {_totalProgress}%";
                _totalProgress = progress;
                WorkerCommon.SetProgress(_queue, progress);
                WorkerCommon.LogState(_queue, logMessage);
            }
        }
    }

    public class SaveReportDownload
    {
	    public static long SaveReportExcel(string nameReport, ExcelFile excel, long registerId, string registerViewId = "KoTasks")
	    {
		    var currentDate = DateTime.Now;
		    long reportId = 0;
		    try
		    {
			    var export = new OMExportByTemplates
			    {
				    UserId = SRDSession.GetCurrentUserId().Value,
				    DateCreated = currentDate,
				    Status = (long)ImportStatus.Added,
				    FileResultTitle = nameReport,
                    FileExtension = "xlsx",
                    MainRegisterId = registerId,
				    RegisterViewId = registerViewId
				};

			    reportId = export.Save();

			    export.Status = (long)ImportStatus.Running;
			    export.DateStarted = DateTime.Now;
			    export.Save();


				MemoryStream stream = new MemoryStream();
			    excel.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
			    stream.Seek(0, SeekOrigin.Begin);

                export.ResultFileName = DataExporterCommon.GetStorageResultFileName(export.Id);
                export.DateFinished = DateTime.Now;
                export.Status = (long)ImportStatus.Completed;

                FileStorageManager.Save(stream, DataExporterCommon.FileStorageName, export.DateFinished.Value, export.ResultFileName);
                export.Save();

			    return reportId;
		    }
		    catch (Exception e)
		    {
			    var export = OMExportByTemplates.Where(x => x.Id == reportId).SelectAll().ExecuteFirstOrDefault();
			    if (export != null)
			    {
				    export.DateFinished = DateTime.Now;
				    export.Status = (long)ImportStatus.Faulted;
				    export.Save();
			    }
				
			    Console.WriteLine(e);
			    ErrorManager.LogError(e);
			    return 0;
		    }
	    }

	    public static long SaveReport(string nameReport, Stream stream, long registerId, string registerViewId = "KoTasks", string reportExtension = "xml")
	    {
		    var currentDate = DateTime.Now;
		    long reportId = 0;
		    try
		    {
			    var export = new OMExportByTemplates
			    {
				    UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				    DateCreated = currentDate,
				    Status = (long)ImportStatus.Added,
				    FileResultTitle = nameReport,
				    FileExtension = reportExtension,
                    MainRegisterId = registerId,
				    RegisterViewId = registerViewId
			    };

			    reportId = export.Save();

			    export.Status = (long)ImportStatus.Running;
			    export.DateStarted = DateTime.Now;
			    export.Save();

			    export.ResultFileName = DataExporterCommon.GetStorageResultFileName(export.Id);
			    export.DateFinished = DateTime.Now;
			    export.Status = (long)ImportStatus.Completed;
                FileStorageManager.Save(stream, DataExporterCommon.FileStorageName, export.DateFinished.Value, export.ResultFileName);
                export.Save();

			    return reportId;
		    }
		    catch (Exception e)
		    {
			    var export = OMExportByTemplates.Where(x => x.Id == reportId).SelectAll().ExecuteFirstOrDefault();
			    export.DateFinished = DateTime.Now;
			    export.Status = (long)ImportStatus.Faulted;
			    Console.WriteLine(e);
			    ErrorManager.LogError(e);
			    return 0;
		    }

		}

	    public static void SetCurrentProgress(OMUnloadResultQueue unloadResultQueue, long progress)
	    {
		    unloadResultQueue.CurrentUnloadProgress = progress;
		    unloadResultQueue.Save();
	    }
    }
}
