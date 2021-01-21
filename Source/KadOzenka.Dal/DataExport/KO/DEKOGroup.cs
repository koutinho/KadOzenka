using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.DataExport
{
    /// <summary>
    /// Класс выгрузки в XML результатов Кадастровой оценки по группам.
    /// </summary>
    public class DEKOGroup : IKoUnloadResult
    {
        private static readonly ILogger _log = Log.ForContext<DEKOGroup>();

        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMGroup
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadXML2)]
        public static List<ResultKoUnloadSettings> ExportToXml(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            _log.ForContext("InputParameters", setting, true).Debug("Начата выгрузка в XML результатов Кадастровой оценки по группам");

            var progressMessage = "Выгрузка в XML результатов Кадастровой оценки по группам";
            var progress = 0;
            if (unloadResultQueue != null)
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);

            List<ResultKoUnloadSettings> res = new List<ResultKoUnloadSettings>();
            // Выбираем все подгруппы
            List<OMGroup> koGroups = OMGroup.Where(x => x.ParentId != -1).SelectAll().Execute();
            int countCurr = 0;
            int countAll = koGroups.Count();
            _log.Debug($"Найдено {countAll} подгрупп");

            foreach (OMGroup subgroup in koGroups)
            {
                _log.ForContext("SubGroupId", subgroup.Id).Debug($"Начата работа с подгруппой '{subgroup.GroupName}', №{countCurr} из {countAll}");

                countCurr++;
                string str_message = "Выгружается группа " + countCurr.ToString() + " (Id=" + subgroup.Id.ToString() + ") из " + countAll.ToString();
                Console.WriteLine(str_message);
                var taskCounter = 0;
                foreach (long taskId in setting.TaskFilter)
                {
                    _log.Debug("Начата работа с ЗнО {TaskId}", taskId);

                    subgroup.Unit = OMUnit.Where(x => x.GroupId == subgroup.Id && x.TaskId == taskId && x.CadastralCost > 0).SelectAll().Execute();
                    _log.ForContext("SubGroupId", subgroup.Id).ForContext("TaskId", taskId)
                        .Debug($"Найдено {subgroup.Unit.Count} ЕО с группой {subgroup.Id} и ЗнО {taskId}, у которых положительная Кадастровая стоимость");

                    if (subgroup.Unit.Count > 0)
                    {
                        Stream resultFile;
                        using (_log.TimeOperation($"Формирование файла для подгруппы '{subgroup.GroupName}'"))
                        {
                            resultFile = SaveXmlDocument(subgroup, str_message);
                        }

                        OMGroup parent_group = OMGroup.Where(x => x.Id == subgroup.ParentId).SelectAll().ExecuteFirstOrDefault();
                        string full_group_num = ((parent_group.Number == null ? parent_group.Id.ToString() : parent_group.Number)) + "." +
                                                ((subgroup.Number == null ? subgroup.Id.ToString() : subgroup.Number));
                        full_group_num = full_group_num.Replace("\n", "");

                        string file_name = "Task_" + taskId  +"_" + full_group_num
                                           + "_FD_State_Cadastral_Valuation_"
                                           + subgroup.Id.ToString().PadLeft(5, '0')
                                           + countCurr.ToString().PadLeft(5, '0');

                        if (setting.IsDataComparingUnload)
                        {
                            using (_log.TimeOperation($"Копирование файла для сравнения '{file_name}'"))
                            {
                                var fullFileName = Path.Combine(setting.DirectoryName, $"{file_name}.xml");
                                using var fs = File.Create(fullFileName);
                                fs.Seek(0, SeekOrigin.Begin);
                                resultFile.CopyTo(fs);
                            }
                        }
                        else
                        {
                            long id;
                            using (_log.TimeOperation($"Сохранение файла '{file_name}'"))
                            {
                                id = SaveReportDownload.SaveReport(file_name, resultFile, OMGroup.GetRegisterId());
                            }

                            var fileResult = new ResultKoUnloadSettings
                            {
                                FileName = file_name,
                                FileId = id,
                                TaskId = taskId
                            };
                            res.Add(fileResult);
                        }
                    }
                    taskCounter++;
                    progress = (taskCounter * 100 / setting.TaskFilter.Count + (countCurr - 1) * 100) / koGroups.Count;
                    setProgress?.Invoke(progress, progressMessage: progressMessage);
                    if(unloadResultQueue != null)
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }
            }

            setProgress?.Invoke(100, true, progressMessage);
            if (unloadResultQueue != null)
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);

            _log.Debug("Закончена выгрузка в XML результатов Кадастровой оценки по группам");

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

            using (_log.TimeOperation("Добавление общей информации в файл"))
            {
                AddXmlGeneralInfo(xmlFile, xnLandValuation);
            }

            Dictionary<Int64, XmlNode> dictNodes = new Dictionary<long, XmlNode>();
            using (_log.TimeOperation("Добавление основной информации в файл"))
            {
                AddXmlPackage(xmlFile, xnLandValuation, _subgroup, dictNodes, _message);
            }
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
                _log.ForContext("ModelId", model?.Id).ForContext("SubgroupId", _subgroup.Id)
                    .Debug($"Поиск активной модели для погруппы {_subgroup.GroupName}. Модель найдена - {model != null}");
                if (model != null)
                {
                    if (model.ModelFactor.Count == 0)
                    {
                        model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id && x.AlgorithmType_Code == model.AlgoritmType_Code).SelectAll().Execute();
                        _log.Debug($"Найдено {model.ModelFactor.Count} факторов типа {model.AlgoritmType_Code.GetEnumDescription()} для модели");
                    }

                    int countCurr = 0;
                    int countAll = model.ModelFactor.Count();
                    var marks = OMMarkCatalog.Where(x => x.GroupId == model.GroupId).SelectAll().Execute();
                    foreach (OMModelFactor factor in model.ModelFactor)
                    {
                        RegisterAttribute attribute_factor = RegisterCache.GetAttributeData((int)(factor.FactorId));
                        factor.FillMarkCatalogsFromList(marks, model.GroupId ?? 0);

                        XmlNode xnEvaluative_Factor = _xmlFile.CreateElement("Evaluative_Factor");
                        DataExportCommon.AddAttribute(_xmlFile, xnEvaluative_Factor, "Id_Factor", factor.FactorId.ToString() + "_" + _subgroup.Id.ToString());
                        DataExportCommon.AddAttribute(_xmlFile, xnEvaluative_Factor, "Type", factor.SignMarket ? "1" : "2");
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

            _log.Debug($"Механизм группировки подгруппы - '{_subgroup.GroupAlgoritm_Code}'");

            var objectIds = _subgroup.Unit.Select(x => x.ObjectId.GetValueOrDefault()).ToList();
            var allAttributes = new GbuObjectService().GetAllAttributes(objectIds, null, new List<long> {3,4,5,8,600});

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

                var currentUnitsCount = 0;
                _log.Debug("Начата обработка ЕО подгруппы");

                List<OMMarkCatalog> marks = new List<OMMarkCatalog>();
                if (model!=null)
                    marks = OMMarkCatalog.Where(x => x.GroupId == model.GroupId ).SelectAll().Execute();
                int? factorReestrId = OMGroup.GetFactorReestrId(_subgroup);


                foreach (OMUnit unit in _subgroup.Unit)
                {
                    if (++currentUnitsCount % 1000 == 0)
                        _log.Debug($"Начата обработка ЕО №{currentUnitsCount} из {_subgroup.Unit.Count} (Заполнение информации по ГБУ-атрибутам)");

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
                        if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 3, out value_attr_3_4)) //Код категории земель из ГКН
                        {
                            XmlNode xnAssignation = _xmlFile.CreateElement("Category");
                            DataExportCommon.AddAttribute(_xmlFile, xnAssignation, "Name", value_attr_3_4);
                            xnReal_Estate.AppendChild(xnAssignation);
                        }

                        value_attr_3_4 = "";
                        if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 4, out value_attr_3_4)) //Код вида использования по документам
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
                        if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 5, out value_attr_5)) //Код Вид использования по классификатору
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
                    if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 600, out value_attr)) //Код 600 - Адрес
                    {
                        XmlNode xnCadastralNote = _xmlFile.CreateElement("Note");
                        xnCadastralNote.InnerText = value_attr;
                        xnCadastralLocation.AppendChild(xnCadastralNote);
                    }
                    value_attr = "";
                    if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 8, out value_attr)) //Код 8 - Местоположение
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

                    //Получаем список факторов группы
                    List<CalcItem> FactorValuesGroup = new List<CalcItem>();
                    DataTable data;
                    using (_log.TimeOperation("Получение атрибутов")){
                        data = RegisterStorage.GetAttributes((int) unit.Id, factorReestrId.Value);
                    }
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
                                factor_model.FillMarkCatalogsFromList(marks, model.GroupId ?? 0);

                                bool addf = false;
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
                               
                                if (addf)
                                    xnCEvaluative_Factors.AppendChild(xnCEvaluative_Factor);
                            }
                        }
                    }

                    xnReal_Estate.AppendChild(xnCEvaluative_Factors);
                    xnReal_Estates.AppendChild(xnReal_Estate);
                }
                _log.Debug("Закончена обработка ЕО подгруппы");
                xnGroup_Real_Estate_Modelling.AppendChild(xnReal_Estates);
                #endregion

                #region Evaluative_Factors_Modelling
                XmlNode xnEvaluative_Factors_Modelling = _xmlFile.CreateElement("Evaluative_Factors_Modelling");

                if (model != null)
                {
                    foreach (OMModelFactor factor_model in model.ModelFactor)
                    {
                        RegisterAttribute attribute_factor = RegisterCache.GetAttributeData((int)(factor_model.FactorId));
                        factor_model.FillMarkCatalogsFromList(marks, model.GroupId);

                        XmlNode xnEvaluative_Factor_Modelling = _xmlFile.CreateElement("Evaluative_Factor_Modelling");
                        DataExportCommon.AddAttribute(_xmlFile, xnEvaluative_Factor_Modelling, "Id_factor", factor_model.Id.ToString() + "_" + _subgroup.Id.ToString());
                        if (factor_model.SignMarket)
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

                var currentUnitsCount = 0;

                foreach (OMUnit unit in _subgroup.Unit)
                {
                    if (++currentUnitsCount % 1000 == 0 || currentUnitsCount == 1)
                        _log.Debug(
                            $"Начата обработка ЕО №{currentUnitsCount} из {_subgroup.Unit.Count} (Заполнение информации по ГБУ-атрибутам)");

                    XmlNode xnReal_Estate = _xmlFile.CreateElement("Real_Estate");
                    DataExportCommon.AddAttribute(_xmlFile, xnReal_Estate, "ID_Group", _subgroup.Id.ToString());

                    XmlNode xnCadastralNumber = _xmlFile.CreateElement("CadastralNumber");
                    xnCadastralNumber.InnerText = unit.CadastralNumber;
                    xnReal_Estate.AppendChild(xnCadastralNumber);

                    XmlNode xnCadastralType = _xmlFile.CreateElement("Type");
                    xnCadastralType.InnerText = unit.PropertyType_Code.ToString();
                    xnReal_Estate.AppendChild(xnCadastralType);

                    XmlNode xnSpecific_CadastralCost = _xmlFile.CreateElement("Specific_CadastralCost");
                    DataExportCommon.AddAttribute(_xmlFile, xnSpecific_CadastralCost, "Value",
                        unit.Upks.ToString());
                    DataExportCommon.AddAttribute(_xmlFile, xnSpecific_CadastralCost, "Unit", "1002");
                    xnReal_Estate.AppendChild(xnSpecific_CadastralCost);

                    XmlNode xnCadastralArea = _xmlFile.CreateElement("Area");
                    xnCadastralArea.InnerText = unit.Square.ToString();
                    xnReal_Estate.AppendChild(xnCadastralArea);

                    if (unit.PropertyType_Code == PropertyTypes.Stead)
                    {
                        string value_attr_3 = "";
                        if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 3, out value_attr_3)
                        ) //Код категории земель из ГКН
                        {
                            XmlNode xnAssignation = _xmlFile.CreateElement("Category");
                            DataExportCommon.AddAttribute(_xmlFile, xnAssignation, "Name", value_attr_3);
                            xnReal_Estate.AppendChild(xnAssignation);
                        }

                        value_attr_3 = "";
                        if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 4, out value_attr_3)
                        ) //Код Вид использования по документам
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
                        if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 5, out value_attr_5)
                        ) //Код Вид использования по классификатору
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
                                XmlNode xnFormalized_Constr_Uncompleted =
                                    _xmlFile.CreateElement("Formalized_Constr_Uncompleted");
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
                    if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 600, out value_attr)) //Код Адрес
                    {
                        XmlNode xnCadastralNote = _xmlFile.CreateElement("Note");
                        xnCadastralNote.InnerText = value_attr;
                        xnCadastralLocation.AppendChild(xnCadastralNote);
                    }

                    value_attr = "";
                    if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 8, out value_attr)
                    ) //Код Местоположение
                    {
                        XmlNode xnCadastralOther = _xmlFile.CreateElement("Other");
                        xnCadastralOther.InnerText = value_attr;
                        xnCadastralLocation.AppendChild(xnCadastralOther);
                    }

                    xnReal_Estate.AppendChild(xnCadastralLocation);
                    XmlNode xnDate_valuation = _xmlFile.CreateElement("Date_valuation");
                    xnDate_valuation.InnerText = ConfigurationManager.AppSettings["ucAppDate"].ParseToDateTime()
                        .ToString("yyyy-MM-dd");
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
}