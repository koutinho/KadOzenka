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
using ObjectModel.KO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;

namespace KadOzenka.Dal.DataExport
{
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

            List<ObjectModel.KO.OMMarkCatalog> objs = ObjectModel.KO.OMMarkCatalog.Where(x=>x.GroupId==groupId && x.FactorId== factorId).SelectAll().Execute();
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
                foreach(List<object> value in values)
                {
                    DataExportCommon.AddRow(mainWorkSheet, row, value.ToArray());
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
        /// Выгрузка списка значений по фактору и группе в формате Excel
        /// </summary>
        public static Stream ExportMarkerListToExcel(long groupId, long factorId)
        {
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Метки");

            DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "Значение фактора", "Метка" });

            List<ObjectModel.KO.OMUnit> units = ObjectModel.KO.OMUnit.Where(x => x.GroupId == groupId).SelectAll().Execute();
            if (units.Count > 0)
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                ParallelOptions options = new ParallelOptions
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = 20
                };

                object locked = new object();
                List<List<object>> values = new List<List<object>>();
                List<string> fvalues = new List<string>();
                int curIndex = 0;
                Parallel.ForEach(units, options, unit =>
                {
                    curIndex++;
                    if (curIndex % 40 == 0) Console.WriteLine(curIndex);

                    DataTable data = RegisterStorage.GetAttribute((int)unit.Id, 252, (int)factorId);
                    string curValue = string.Empty;
                    if (data != null)
                    {
                        if (data.Rows.Count > 0)
                        {
                            curValue = data.Rows[0].ItemArray[6].ParseToString();
                        }
                    }


                    if (curValue != string.Empty)
                    {
                        lock (locked)
                        {
                            if (!fvalues.Contains(curValue))
                            fvalues.Add(curValue);
                        }
                    }

                });

                int row = 1;
                List<ObjectModel.KO.OMMarkCatalog> markers = ObjectModel.KO.OMMarkCatalog.Where(x => x.GroupId == groupId && x.FactorId == factorId).SelectAll().Execute();

                foreach (string value in fvalues)
                {
                    string mvalue = string.Empty;
                    ObjectModel.KO.OMMarkCatalog marker = markers.Find(x => x.ValueFactor.ToUpper() == value.ToUpper());
                    if (marker!=null)
                    {
                        mvalue = (marker.MetkaFactor == null) ? string.Empty : marker.MetkaFactor.ParseToString();
                    }
                    DataExportCommon.AddRow(mainWorkSheet, row, new object[] { value, mvalue });
                    row++;
                }
                Console.WriteLine(fvalues.Count);
            }

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
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
            AddXmlSender(xmlFile, xneDocument, sender, dupload);
            AddXmlRecipient(xmlFile, xneDocument);
            parent.AppendChild(xneDocument);
        }
        public static void AddXmlSender(XmlDocument xmlFile, XmlNode parent, string sender, DateTime dupload)
        {
            XmlNode xnSender = xmlFile.CreateElement("Sender");
            DataExportCommon.AddAttribute(xmlFile, xnSender, "Name", sender);
            DataExportCommon.AddAttribute(xmlFile, xnSender, "Date_Upload", dupload.ToString("yyyy-MM-dd"));
            parent.AppendChild(xnSender);
        }
        public static void AddXmlRecipient(XmlDocument xmlFile, XmlNode parent)
        {
            XmlNode xnRecipient = xmlFile.CreateElement("Recipient");
            DataExportCommon.AddAttribute(xmlFile, xnRecipient, "Name", "");
            parent.AppendChild(xnRecipient);
        }
        public static void AddXmlPackage(XmlDocument xmlFile, XmlNode parent, List<ObjectModel.KO.OMUnit> units)
        {
            XmlNode xnRecipient = xmlFile.CreateElement("Package");
            AddXmlFederal(xmlFile, xnRecipient, units);
            parent.AppendChild(xnRecipient);
        }
        public static void AddXmlFederal(XmlDocument xmlFile, XmlNode parent, List<ObjectModel.KO.OMUnit> units)
        {
            XmlNode xnRecipient = xmlFile.CreateElement("Federal");
            DataExportCommon.AddAttribute(xmlFile, xnRecipient, "CadastralNumber", "00");
            AddXmlRegions(xmlFile, xnRecipient, units);
            parent.AppendChild(xnRecipient);
        }
        public static void AddXmlRegions(XmlDocument xmlFile, XmlNode parent, List<ObjectModel.KO.OMUnit> units)
        {
            XmlNode xnRecipient = xmlFile.CreateElement("Cadastral_Regions");

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
                        xnRecipient.AppendChild(xn_kn_sub);
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
            parent.AppendChild(xnRecipient);
        }
        /// <summary>
        /// Выгрузка в XML результатов Кадастровой оценки: Кадастровый номер, УПКСЗ, Кадастровая стоимость.
        /// </summary>
        public static Stream ExportCostToXml(List<ObjectModel.KO.OMUnit> units)
        {
            XmlDocument xmlFile = new XmlDocument();
            XmlNode xnLandValuation = xmlFile.CreateElement("LandValuation");

            AddXmlDocument(xmlFile, xnLandValuation, ConfigurationManager.AppSettings["ucUseDoc" ].ParseToBoolean(),
                                                     ConfigurationManager.AppSettings["ucDocName"],
                                                     ConfigurationManager.AppSettings["ucDocNum" ],
                                                     ConfigurationManager.AppSettings["ucDocDate"].ParseToDateTime(),
                                                     ConfigurationManager.AppSettings["ucAppDate"].ParseToDateTime(),
                                                     ConfigurationManager.AppSettings["ucSender" ],
                                                     DateTime.Now);
            AddXmlPackage(xmlFile, xnLandValuation, units);

            xmlFile.AppendChild(xnLandValuation);
            MemoryStream stream = new MemoryStream();
            xmlFile.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
