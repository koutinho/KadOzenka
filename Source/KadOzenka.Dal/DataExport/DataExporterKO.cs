using Core.Register;
using Core.Register.RegisterEntities;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
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
using ObjectModel.KO;
using ObjectModel.Core.TD;
using ObjectModel.Directory;

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

                    DataTable data = RegisterStorage.GetAttribute((int)unit.Id, OMGroup.GetFactorReestrId(unit), (int)factorId);
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
                    if (marker != null)
                    {
                        mvalue = (marker.MetkaFactor == null) ? string.Empty : marker.MetkaFactor.ParseToString();
                    }
                    DataExportCommon.AddRow(mainWorkSheet, row, new object[] { value, mvalue });
                    row++;
                }
                Console.WriteLine(fvalues.Count);
            }

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }

    /// <summary>
    /// Класс выгрузки в XML результатов Кадастровой оценки по объектам.
    /// </summary>
    public class DEKOUnit
    {
        public static Stream ExportToXml(List<ObjectModel.KO.OMUnit> units)
        {
            XmlDocument xmlFile = new XmlDocument();
            XmlNode xnLandValuation = xmlFile.CreateElement("LandValuation");

            AddXmlDocument(xmlFile, xnLandValuation, ConfigurationManager.AppSettings["ucUseDoc"].ParseToBoolean(),
                                                     ConfigurationManager.AppSettings["ucDocName"],
                                                     ConfigurationManager.AppSettings["ucDocNum"],
                                                     ConfigurationManager.AppSettings["ucDocDate"].ParseToDateTime(),
                                                     ConfigurationManager.AppSettings["ucAppDate"].ParseToDateTime(),
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
    public class DEKOGroup
    {
        public static Stream ExportToXml(OMGroup _subgroup, string _message)
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
                OMModel model = OMModel.Where(x => x.GroupId == _subgroup.Id).SelectAll().ExecuteFirstOrDefault();
                if (model != null)
                {
                    if (model.ModelFactor.Count == 0)
                        model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id).SelectAll().Execute();

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
                        string str_message = _message + "    -- " + "Evaluative_Factors " + countCurr.ToString() + " из " + countAll.ToString();
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

            if (_subgroup.GroupAlgoritm_Code == ObjectModel.Directory.KoGroupAlgoritm.Model)
            {
                #region Statistical_Modelling
                ObjectModel.KO.OMModel model = OMModel.Where(x => x.GroupId == _subgroup.Id).SelectAll().ExecuteFirstOrDefault();

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

                int countCurr = 0;
                int countAll = _subgroup.Unit.Count();
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
                            model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id).SelectAll().Execute();
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

                    countCurr++;
                    string str_message = _message + " -- " + "Real_Estates " + countCurr.ToString() + " units  из " + countAll.ToString();
                    Console.WriteLine(str_message);
                    if (countCurr == 10) break;  // TEMP
                }

                xnGroup_Real_Estate_Modelling.AppendChild(xnReal_Estates);
                #endregion

                #region Evaluative_Factors_Modelling
                XmlNode xnEvaluative_Factors_Modelling = _xmlFile.CreateElement("Evaluative_Factors_Modelling");

                if (model != null)
                {
                    int fmCurr = 0;
                    int fmAll = model.ModelFactor.Count();
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

                        fmCurr++;
                        string str_message = _message + " -- " + "Evaluative_Factors_Modelling " + fmCurr.ToString() + " из " + fmAll.ToString();
                        Console.WriteLine(str_message);
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

                int otherCurr = 0;
                int otherAll = _subgroup.Unit.Count();
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

                    otherCurr++;
                    string str_message = _message + "    -- " + "Appraise - Other " + otherCurr.ToString() + " из " + otherAll.ToString();
                    Console.WriteLine(str_message);
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
    public class DEKOResponseDoc
    {
        public static /*Stream*/ void ExportToXml(OMInstance _doc, string _dir_name)
        {
            List<OMUnit> units = OMUnit.Where(x => x.ResponseDocId == _doc.Id).SelectAll().Execute();
            if (units.Count == 0) return;

            List<ActOpredel> list_act = new List<ActOpredel>();
            List<OMInstance> list_doc_in = new List<OMInstance>();
            List<string> list_bads = new List<string>();

            int num_pp = 0;
            list_bads.AddRange(CalcXMLResponseDoc(ref num_pp, units, _doc, out List<ActOpredel> list_act_out, out List<OMInstance> list_doc_out, _dir_name));
            list_doc_out.ForEach(x => { if (list_doc_in.Find(y => y.Id == x.Id) == null) list_doc_in.Add(x); });
            list_act.AddRange(list_act_out);
            ObjectNotChange(list_bads, list_act, list_doc_in, _dir_name);

            return /*stream*/;
        }

        private static string[] CalcXMLResponseDoc(ref int _num_pp, List<OMUnit> _units, OMInstance _doc_out, out List<ActOpredel> _list_act, out List<OMInstance> _list_doc_in, string _file_path)
        {
            Console.WriteLine("CalcXMLResponseDoc - start");
            List<string> bads = new List<string>();
            _list_act = new List<ActOpredel>();
            _list_doc_in = new List<OMInstance>();
            Dictionary<Int64, XmlNode> dictNodes = new Dictionary<long, XmlNode>();

            int count_curr = 0;
            int count_all = _units.Count();
            foreach (OMUnit unit in _units)
            {
                count_curr++;
                string str_message = "Doc - " + _doc_out.Id.ToString() +  " - (" + count_curr.ToString() + " из " + count_all.ToString() + " unit)";
                Console.WriteLine(str_message);

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
                    //act_opredel.act_dop   = group_unit.act_dop;    //aSubGroup.act_dop;  //TODO
                    act_opredel.kc = unit.CadastralCost;
                    //act_opredel.act_model = group_unit.act_model;// aSubGroup.act_model;  //TODO
                    act_opredel.osnovanie = unit.Status;
                    //act_opredel.act_other = group_unit.act_other;  //TODO
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

                        if (!Directory.Exists(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_")))
                            Directory.CreateDirectory(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_"));

                        xmlFile.Save(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_") +
                                                  "\\COST_" + ConfigurationManager.AppSettings["ucSender"] +
                                                  "_" + doc_in.CreateDate.ToString("ddMMyyyy") +
                                                  "_" + DateTime.Now.ToString("ddMMyyyy") +
                                                  "_" + ((int)unit.PropertyType_Code).ToString() +
                                                  "_" + unit.Id.ToString() + ".xml");

                        XmlVuonExport(group_unit, unit, _file_path + "\\" + unit.CadastralNumber.Replace(":", "_"), doc_in, _doc_out, dictNodes);
                        XmlWebExport(group_unit, unit, _file_path, doc_in, _doc_out);
                        GetOtvetDocX(unit, _file_path, _doc_out, _num_pp);
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

                        if (!Directory.Exists(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_")))
                            Directory.CreateDirectory(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_"));

                        xmlFile.Save(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_") +
                                                  "\\COST_" + ConfigurationManager.AppSettings["ucSender"] +
                                                  "_" + doc_in.CreateDate.ToString("ddMMyyyy") +
                                                  "_" + DateTime.Now.ToString("ddMMyyyy") +
                                                  "_" + ((int)unit.PropertyType_Code).ToString() +
                                                  "_" + unit.Id.ToString() + ".xml");

                        XmlVuonExport(group_unit, unit, _file_path + "\\" + unit.CadastralNumber.Replace(":", "_"), doc_in, _doc_out, dictNodes);
                        GetOtvetDocX(unit, _file_path, _doc_out, _num_pp);
                        bads.Add("g|" + unit.CadastralNumber + "|" + ((unit.CadastralCost == null) ? 0 : (decimal)unit.CadastralCost).ToString("0.00"));

                        string dateapp = "01.01.2018";
                        OMTask task_old = OMTask.Where(x => x.Id == unit.TaskId).SelectAll().ExecuteFirstOrDefault();
                        if (task != null)
                        {
                            OMInstance doc_old = OMInstance.Where(x => x.Id == task.DocumentId).SelectAll().ExecuteFirstOrDefault();
                            if (doc_old != null) dateapp = doc_old.CreateDate.ToString("dd.MM.yyyy");
                        }
                        bads.Add("b|" + unit.CadastralNumber + "|" + dateapp);
                    }
                }
            }

            Console.WriteLine("CalcXMLResponseDoc - end");
            return bads.ToArray();
        }

        public static void ObjectNotChange(List<string> list_bads, List<ActOpredel> _list_act, List<OMInstance> _list_doc, string _dir_name)
        {
            Console.WriteLine("ObjectNotChange - start");
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

                if (!Directory.Exists(_dir_name + "\\" + "DOC")) Directory.CreateDirectory(_dir_name + "\\" + "DOC");
                string filenn_temp = _dir_name + "\\" + "DOC" + "\\Акт_об_определении_КС" + DateTime.Now.ToString("ddMMyyyyhhmmss") + "_без_изменений.xlsx";
                excelTemplate.Save(filenn_temp); 
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

                if (!Directory.Exists(_dir_name + "\\" + "DOC")) Directory.CreateDirectory(_dir_name + "\\" + "DOC");
                string filenn = _dir_name + "\\" + "DOC" + "\\Акт_об_определении_КС" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                excel_edit.Save(filenn);

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

                if (!Directory.Exists(_dir_name + "\\" + "DOC")) Directory.CreateDirectory(_dir_name + "\\" + "DOC");
                string afilenn = _dir_name + "\\" + "DOC" + "\\Акт_определения_КС_по_МУ_пункт_12_2" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                aexcell.Save(afilenn);
            }
            #endregion

            Console.WriteLine("ObjectNotChange - end");
        }

        public static bool XmlVuonExport(OMGroup _group_unit, OMUnit _unit, string _dir_name, OMInstance _doc_in, OMInstance _doc_out, Dictionary<Int64, XmlNode> dictNodes)
        {
            Console.WriteLine("XmlVuonExport - start");
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

                if (!Directory.Exists(_dir_name))
                    Directory.CreateDirectory(_dir_name);

                xmlFile.Save(_dir_name + "\\FD_State_Cadastral_Valuation_" + _group_unit.Id.ToString().PadLeft(5, '0') +
                    "_" + _doc_in.CreateDate.ToString("ddMMyyyy") +
                    "_" + ((int)_unit.PropertyType_Code).ToString() +
                    "_" + _unit.Id.ToString() + ".xml");
            }

            Console.WriteLine("XmlVuonExport - end");
            return true;
        }

        public static bool XmlWebExport(OMGroup _group_unit, OMUnit _unit, string _dir_name, OMInstance _doc_in, OMInstance _doc_out)
        {
            Console.WriteLine("XmlWebExport - start");
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
                        DataExportCommon.GetObjectAttribute(_unit, number_attr, out value_attr); //Код  - Назначение здания
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

            if (!Directory.Exists(_dir_name))
                Directory.CreateDirectory(_dir_name);
            if (!Directory.Exists(_dir_name + "\\" + "_web_obmen_"))
                Directory.CreateDirectory(_dir_name + "\\" + "_web_obmen_");
            xmlFile.Save(_dir_name + "\\" + "_web_obmen_" + "\\" + _unit.CadastralNumber.Replace(":", "_") + ".xml");

            Console.WriteLine("XmlWebExport - end");
            return true;
        }

        public static void GetOtvetDocX(OMUnit _unit, string _dir_name, OMInstance _doc_out, int numpp)
        {
            Console.WriteLine("GetOtvetDocX - start");

            string file_name = _dir_name + "\\Акты по объектам";
            if (!Directory.Exists(file_name)) Directory.CreateDirectory(file_name);
            string num = _doc_out.RegNumber;
            num = num.Replace("/", "");
            string kn = _unit.CadastralNumber;
            kn = kn.Replace(":", "_");
            file_name = file_name + "\\" + num + "_" + kn + ".docx";

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
            DataExportCommon.SetTextToCell(document, table.Rows[curCount++ - 1].Cells[0], _doc_out.Description.Replace("Акт определения", "Выписка из акта об определении").Replace("Акт об определении", "Выписка из акта об определении"), 12, HorizontalAlignment.Center, false, false);
            DataExportCommon.SetTextToCell(document, table.Rows[curCount++ - 1].Cells[0], "№" + _doc_out.RegNumber + " от " + NullConvertorMS.DTtoSTR(_doc_out.CreateDate) + " г.", 12, HorizontalAlignment.Center, false, false);
            DataExportCommon.SetTextToCell(document, table.Rows[curCount++ - 1].Cells[0], " ", 6, HorizontalAlignment.Center, false, false);
            DataExportCommon.SetText3(document, table.Rows[curCount++ - 1], "№ п/п", "Кадастровый номер", "Кадастровая стоимость, руб.", 12, HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center, true, false);
            DataExportCommon.SetText3(document, table.Rows[curCount++ - 1], numpp.ToString(), _unit.CadastralNumber, _unit.CadastralCost.ToString(), 12, HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Center, true, false);

            // Парметры страницы
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

            document.Save(file_name);
            Console.WriteLine("GetOtvetDocX - end");
        }
    }

    /// <summary>
    /// Класс выгрузки в XML результатов Кадастровой оценки для ВУОН.
    /// </summary>
    public class DEKOVuon
    {
        public static /*Stream*/ void ExportToXml(OMInstance _doc, string _dir_name)
        {
            List<OMUnit> units = OMUnit.Where(x => x.ResponseDocId == _doc.Id).SelectAll().Execute();
            if (units.Count == 0) return;

            List<ActOpredel> list_act = new List<ActOpredel>();
            List<OMInstance> list_doc_in = new List<OMInstance>();
            List<string> list_bads = new List<string>();

            int num_pp = 0;
            list_bads.AddRange(CalcXMLFromVuon(num_pp, units, _doc, out List<ActOpredel> list_act_out, out List<OMInstance> list_doc_out, _dir_name));
            list_doc_out.ForEach(x => { if (list_doc_in.Find(y => y.Id == x.Id) == null) list_doc_in.Add(x); });
            list_act.AddRange(list_act_out);

            DEKOResponseDoc.ObjectNotChange(list_bads, list_act, list_doc_in, _dir_name);

            return /*stream*/;
        }

        private static string[] CalcXMLFromVuon(int _num_pp, List<OMUnit> _units, OMInstance _doc_out, out List<ActOpredel> _list_act, out List<OMInstance> _list_doc_in, string _file_path)
        {
            List<string> bads = new List<string>();
            _list_act = new List<ActOpredel>();
            _list_doc_in = new List<OMInstance>();
            Dictionary<Int64, XmlNode> dictNodes = new Dictionary<long, XmlNode>();

            List<OMUnit> units = OMUnit.Where(x => x.ResponseDocId == _doc_out.Id).SelectAll().Execute();
            int count_curr = 0;
            int count_all = _units.Count();
            foreach (OMUnit unit in _units)
            {
                count_curr++;
                string str_message = "Doc - " + _doc_out.Id.ToString() + " - (" + count_curr.ToString() + " из " + count_all.ToString() + " unit)";
                Console.WriteLine(str_message);

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
                //act_opredel.act_dop   = group_unit.act_dop;    //aSubGroup.act_dop;  //TODO
                act_opredel.kc = unit.CadastralCost;
                //act_opredel.act_model = group_unit.act_model;// aSubGroup.act_model;  //TODO
                act_opredel.osnovanie = unit.Status;
                //act_opredel.act_other = group_unit.act_other;  //TODO
                act_opredel.subgroup = group_unit.GroupName;
                _list_act.Add(act_opredel);

                if (unit.GroupId <= 0)                         return bads.ToArray();
                if (_doc_out.Status != doc_in.Status)          return bads.ToArray();
                if (unit.Upks <= 0 && unit.CadastralCost <= 0) return bads.ToArray();

                if (DataExportCommon.GetObjectLastByKN(unit))
                {
                    _num_pp++;
                    XmlDocument xmlFile = new XmlDocument();
                    XmlNode xnLandValuation = xmlFile.CreateElement("LandValuation");
                    xmlFile.AppendChild(xnLandValuation);
                    DEKOUnit.AddXmlDocument(xmlFile, xnLandValuation, true, _doc_out.Description, _doc_out.RegNumber, _doc_out.CreateDate, doc_in.CreateDate,
                                   ConfigurationManager.AppSettings["ucSender"], DateTime.Now);
                    DEKOUnit.AddXmlPackage(xmlFile, xnLandValuation, new List<OMUnit> { unit });

                    if (!Directory.Exists(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_")))
                        Directory.CreateDirectory(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_"));

                    xmlFile.Save(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_") +
                                              "\\COST_" + ConfigurationManager.AppSettings["ucSender"] +
                                              "_" + doc_in.CreateDate.ToString("ddMMyyyy") +
                                              "_" + DateTime.Now.ToString("ddMMyyyy") +
                                              "_" + ((int)unit.PropertyType_Code).ToString() +
                                              "_" + unit.Id.ToString() + ".xml");

                    DEKOResponseDoc.XmlVuonExport(group_unit, unit, _file_path + "\\" + unit.CadastralNumber.Replace(":", "_"), doc_in, _doc_out, dictNodes);
                    DEKOResponseDoc.XmlWebExport(group_unit, unit, _file_path, doc_in, _doc_out);
                    DEKOResponseDoc.GetOtvetDocX(unit, _file_path, _doc_out, _num_pp);
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

                    if (!Directory.Exists(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_")))
                        Directory.CreateDirectory(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_"));

                    xmlFile.Save(_file_path + "\\" + unit.CadastralNumber.Replace(":", "_") +
                                              "\\COST_" + ConfigurationManager.AppSettings["ucSender"] +
                                              "_" + doc_in.CreateDate.ToString("ddMMyyyy") +
                                              "_" + DateTime.Now.ToString("ddMMyyyy") +
                                              "_" + ((int)unit.PropertyType_Code).ToString() +
                                              "_" + unit.Id.ToString() + ".xml");

                    DEKOResponseDoc.XmlVuonExport(group_unit, unit, _file_path + "\\" + unit.CadastralNumber.Replace(":", "_"), doc_in, _doc_out, dictNodes);
                    DEKOResponseDoc.GetOtvetDocX(unit, _file_path, _doc_out, _num_pp);
                    bads.Add("g|" + unit.CadastralNumber + "|" + ((unit.CadastralCost == null) ? 0 : (decimal)unit.CadastralCost).ToString("0.00"));

                    string dateapp = "01.01.2018";
                    OMTask task_old = OMTask.Where(x => x.Id == unit.TaskId).SelectAll().ExecuteFirstOrDefault();
                    if (task != null)
                    {
                        OMInstance doc_old = OMInstance.Where(x => x.Id == task.DocumentId).SelectAll().ExecuteFirstOrDefault();
                        if (doc_old != null) dateapp = doc_old.CreateDate.ToString("dd.MM.yyyy");
                    }
                    bads.Add("b|" + unit.CadastralNumber + "|" + dateapp);
                }
            }

            return bads.ToArray();
        }
    }
}
