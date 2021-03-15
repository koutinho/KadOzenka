using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;
using Core.Shared.Extensions;
using KadOzenka.Dal.Extentions;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.DataExport
{
    /// <summary>
    /// Класс выгрузки в XML результатов Кадастровой оценки по объектам.
    /// </summary>
    public class DEKOUnit : IKoUnloadResult
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<DEKOUnit>();

        /// <summary>
        /// Экспорт в Xml - КНомер, УПКСЗ, КСтоимость. По 5000 записей. 
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadXML1)]
        public static List<ResultKoUnloadSettings> ExportToXml(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            Log.ForContext("InputParameters", setting, true).Debug("Начата выгрузка в XML результатов Кадастровой оценки по объектам");
            var progressMessage = "Выгрузка в XML результатов Кадастровой оценки по объектам";
            var taskCounter = 0;
            int progress;
            if (unloadResultQueue != null)
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);

            List<ResultKoUnloadSettings> res = new List<ResultKoUnloadSettings>();

            string fileName = "";
            foreach (long taskId in setting.TaskFilter)
            {
                Log.Debug("Начата работа с ЗнО с ИД {TaskId}", taskId);

                const int chunkSize = 5000;
                var currentTask = OMTask.Where(x => x.Id == taskId).Select(x => x.EstimationDate).ExecuteFirstOrDefault();
                var unitsAll = OMUnit.Where(x => x.TaskId == taskId && x.CadastralCost > 0).Select(x => new
                {
                    x.CadastralNumber,
                    x.Upks,
                    x.CadastralCost
                }).Execute().Split(chunkSize).ToArray();
                //int countCurr = 0;
                int countAll = unitsAll.Count();
                Log.Debug($"Найдено {countAll} наборов ЕО у которых Кадастровая стоимость > 0 ");

                //var unitsCurr = new List<OMUnit>();
                //int countWrite = 5000;
                //int countFile = 1;
                var unitCounter = 0;

                for (var index=0; index<unitsAll.Length;index++)
                {
                    //unitsCurr.AddRange(chunk);
                    //countCurr++;
                    //if (countCurr == countWrite)
                    {
                        fileName = "Task_" + taskId + "COST_" + ConfigurationManager.AppSettings["ucSender"] + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + index.ToString().PadLeft(4, '0');

                        Stream resultFile;
                        using (Log.TimeOperation($"Формирование файла '{fileName}'"))
                        {
                            resultFile = SaveXmlDocument(unitsAll[index], currentTask.EstimationDate);
                        }

                        if (setting.IsDataComparingUnload)
                        {
                            using (Log.TimeOperation($"Копирование файла '{fileName}' для сравнения"))
                            {
                                var fullFileName = Path.Combine(setting.DirectoryName, $"{fileName}.xml");
                                using var fs = File.Create(fullFileName);
                                fs.Seek(0, SeekOrigin.Begin);
                                resultFile.CopyTo(fs);
                            }
                        }
                        else
                        {
                            long id;
                            using (Log.TimeOperation($"Сохранение файла '{fileName}'"))
                            {
                                id = SaveUnloadResult.SaveResult(fileName, resultFile, unloadResultQueue.Id, "xml", KoUnloadResultType.UnloadXML1);
                            }

                            var resFile = new ResultKoUnloadSettings
                            {
                                FileName = fileName,
                                FileId = id,
                                TaskId = taskId
                            };
                            res.Add(resFile);
                        }

                        //unitsCurr.Clear();
                        //countCurr = 0;
                        //countFile++;
                    }

                    unitCounter++;
                    progress = (unitCounter * 100 / countAll + taskCounter * 100) / setting.TaskFilter.Count;
                    setProgress?.Invoke(progress, progressMessage: progressMessage);
                    if (unloadResultQueue != null)
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }

//                file_name = "Task_" + taskId + "COST_" + ConfigurationManager.AppSettings["ucSender"] + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + countFile.ToString().PadLeft(4, '0');

//                Stream resultFile1;
//                using (_log.TimeOperation($"Формирование файла (оставшиеся ЕО) '{file_name}'"))
//                {
//                    resultFile1 = SaveXmlDocument(unitsCurr, currentTask.EstimationDate);
//                }
//
//                if (setting.IsDataComparingUnload)
//                {
//                    using (_log.TimeOperation($"Копирование файла (оставшиеся ЕО) '{file_name}' для сравнения"))
//                    {
//                        var fullFileName = Path.Combine(setting.DirectoryName, $"{file_name}.xml");
//                        using var fs = File.Create(fullFileName);
//                        fs.Seek(0, SeekOrigin.Begin);
//                        resultFile1.CopyTo(fs);
//                    }
//                }
//                else
//                {
//                    long id1;
//                    using (_log.TimeOperation($"Сохранение файла (оставшиеся ЕО) '{file_name}'"))
//                    {
//                        id1 = SaveReportDownload.SaveReport(file_name, resultFile1, OMUnit.GetRegisterId());
//                    }
//
//                    var koResultFile = new ResultKoUnloadSettings
//                    {
//                        FileName = file_name,
//                        FileId = id1,
//                        TaskId = taskId
//                    };
//                    res.Add(koResultFile);
//                }

                taskCounter++;
            }
            if (unloadResultQueue != null)
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress?.Invoke(100, true, progressMessage);

            Log.Debug("Закончена выгрузка в XML результатов Кадастровой оценки по объектам");

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

            string knSub = "";
            string knRaion = "";
            string knKvartal = "";

            XmlNode xnKnSub;
            XmlNode xnKnRaion;
            XmlNode xnKnKvartal;

            XmlNode xnCadastralDistricts = null;
            XmlNode xnCadastralBlocks = null;
            XmlNode xnParcels = null;

            var currentUnitCount = -1;
            foreach (var unit in units)
            {
                if (++currentUnitCount % 1000 == 0)
                    Log.ForContext("UnitId", unit.Id).Debug($"Идет обработка {currentUnitCount} ЕО из {units.Count}");

                string[] arrtmp = unit.CadastralNumber.Split(':');

                if (arrtmp.Length != 4) continue;

                string kn = unit.CadastralNumber;
                string tupksz = unit.Upks.ToString();
                string tks = unit.CadastralCost.ToString();
                string upksz;
                string ks;
                double dUpksz;
                double dKs;

                if (double.TryParse(tupksz.Replace(',', '.'), out dUpksz))
                    upksz = dUpksz.ToString("0.00").Replace(',', '.');
                else if (double.TryParse(tupksz.Replace('.', ','), out dUpksz))
                    upksz = dUpksz.ToString("0.00").Replace(',', '.');
                else
                    upksz = "";

                if (double.TryParse(tks.Replace(',', '.'), out dKs))
                    ks = dKs.ToString("0.00").Replace(',', '.');
                else if (double.TryParse(tks.Replace('.', ','), out dKs))
                    ks = dKs.ToString("0.00").Replace(',', '.');
                else
                    ks = "";

                string[] arr = kn.Split(':');
                string newKnSub = arr[0];
                string newKnRaion = arr[0] + ":" + arr[1];
                string newKnKvartal = arr[0] + ":" + arr[1] + ":" + arr[2];

                if (knSub != newKnSub)
                {
                    xnKnSub = xmlFile.CreateElement("Cadastral_Region");
                    DataExportCommon.AddAttribute(xmlFile, xnKnSub, "CadastralNumber", newKnSub);
                    xnCadastralDistricts = xmlFile.CreateElement("Cadastral_Districts");
                    xnKnSub.AppendChild(xnCadastralDistricts);
                    xnRegions.AppendChild(xnKnSub);
                    knSub = newKnSub;
                }

                if (knRaion != newKnRaion)
                {
                    xnKnRaion = xmlFile.CreateElement("Cadastral_District");
                    DataExportCommon.AddAttribute(xmlFile, xnKnRaion, "CadastralNumber", newKnRaion);
                    xnCadastralBlocks = xmlFile.CreateElement("Cadastral_Blocks");
                    xnCadastralDistricts.AppendChild(xnKnRaion);
                    xnKnRaion.AppendChild(xnCadastralBlocks);
                    knRaion = newKnRaion;
                }

                if (knKvartal != newKnKvartal)
                {
                    xnKnKvartal = xmlFile.CreateElement("Cadastral_Block");
                    DataExportCommon.AddAttribute(xmlFile, xnKnKvartal, "CadastralNumber", newKnKvartal);
                    xnParcels = xmlFile.CreateElement("Parcels");
                    xnCadastralBlocks.AppendChild(xnKnKvartal);
                    xnKnKvartal.AppendChild(xnParcels);
                    knKvartal = newKnKvartal;
                }

                XmlNode xnParcel = xmlFile.CreateElement("Parcel");
                DataExportCommon.AddAttribute(xmlFile, xnParcel, "CadastralNumber", kn);
                XmlNode xnGroundPayments = xmlFile.CreateElement("Ground_Payments");
                XmlNode xnSpecificCadastralCost = xmlFile.CreateElement("Specific_CadastralCost");
                DataExportCommon.AddAttribute(xmlFile, xnSpecificCadastralCost, "Value", upksz);
                DataExportCommon.AddAttribute(xmlFile, xnSpecificCadastralCost, "Unit", "1002");
                XmlNode xnCadastralCost = xmlFile.CreateElement("CadastralCost");
                DataExportCommon.AddAttribute(xmlFile, xnCadastralCost, "Value", ks);
                DataExportCommon.AddAttribute(xmlFile, xnCadastralCost, "Unit", "383");
                xnGroundPayments.AppendChild(xnSpecificCadastralCost);
                xnGroundPayments.AppendChild(xnCadastralCost);
                xnParcel.AppendChild(xnGroundPayments);

                if ((ks != String.Empty) & (upksz != string.Empty))
                    xnParcels.AppendChild(xnParcel);
            }
            parent.AppendChild(xnRegions);
        }
    }
}