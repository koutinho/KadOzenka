using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using Core.Register;
using Core.Register.QuerySubsystem;
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
        private static readonly ILogger Log = Serilog.Log.ForContext<DEKOGroup>();

        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMGroup
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadXML2)]
        public static List<ResultKoUnloadSettings> ExportToXml(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            Log.ForContext("InputParameters", setting, true).Debug("Начата выгрузка в XML результатов Кадастровой оценки по группам");

            const string progressMessage = "Выгрузка в XML результатов Кадастровой оценки по группам";
            var progress = 0;
            if (unloadResultQueue != null)
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);

            var res = new List<ResultKoUnloadSettings>();
            // Выбираем все подгруппы
            var koGroups = OMGroup.Where(x => x.ParentId != -1).SelectAll().Execute();
            var countCurr = 0;
            var countAll = koGroups.Count;
            Log.Debug($"Найдено {countAll} подгрупп");

            foreach (var subgroup in koGroups)
            {
                Log.ForContext("SubGroupId", subgroup.Id).Debug($"Начата работа с подгруппой '{subgroup.GroupName}', №{countCurr} из {countAll}");

                countCurr++;
                var strMessage = "Выгружается группа " + countCurr + " (Id=" + subgroup.Id + ") из " + countAll;
                Console.WriteLine(strMessage);
                var taskCounter = 0;
                foreach (var taskId in setting.TaskFilter)
                {
                    Log.Debug("Начата работа с ЗнО {TaskId}", taskId);

                    subgroup.Unit = OMUnit.Where(x => x.GroupId == subgroup.Id && x.TaskId == taskId && x.CadastralCost > 0).SelectAll().Execute();
                    Log.ForContext("SubGroupId", subgroup.Id).ForContext("TaskId", taskId)
                        .Debug($"Найдено {subgroup.Unit.Count} ЕО с группой {subgroup.Id} и ЗнО {taskId}, у которых положительная Кадастровая стоимость");

                    if (subgroup.Unit.Count > 0)
                    {
                        Stream resultFile;
                        using (Log.TimeOperation($"Формирование файла для подгруппы '{subgroup.GroupName}'"))
                        {
                            resultFile = SaveXmlDocument(subgroup, strMessage);
                        }

                        var parentGroup = OMGroup.Where(x => x.Id == subgroup.ParentId).SelectAll().ExecuteFirstOrDefault();
                        var fullGroupNum = GetFullGroupNum(parentGroup, subgroup);
                        var fileName = GetFdFileName(taskId, fullGroupNum, subgroup, countCurr);

                        if (setting.IsDataComparingUnload)
                        {
                            using (Log.TimeOperation($"Копирование файла для сравнения '{fileName}'"))
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
                                id = SaveReportDownload.SaveReport(fileName, resultFile, OMGroup.GetRegisterId());
                            }

                            var fileResult = new ResultKoUnloadSettings
                            {
                                FileName = fileName,
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

            Log.Debug("Закончена выгрузка в XML результатов Кадастровой оценки по группам");

            return res;
        }

        private static string GetFullGroupNum(OMGroup parentGroup, OMGroup subgroup)
        {
            var fullGroupNum = (parentGroup.Number ?? parentGroup.Id.ToString()) + "." +
                               (subgroup.Number ?? subgroup.Id.ToString());
            fullGroupNum = fullGroupNum.Replace("\n", "");
            return fullGroupNum;
        }

        private static string GetFdFileName(long taskId, string fullGroupNum, OMGroup subgroup, int countCurr)
        {
            return "Task_" + taskId  +"_" + fullGroupNum
                   + "_FD_State_Cadastral_Valuation_"
                   + subgroup.Id.ToString().PadLeft(5, '0')
                   + countCurr.ToString().PadLeft(5, '0');
        }

        /// <summary>
        /// Экспорт в Xml - КОценка по группам.
        /// </summary>
        private static Stream SaveXmlDocument(OMGroup subgroup, string message)
        {

            var xmlFile = new XmlDocument();
            XmlNode xnLandValuation = xmlFile.CreateElement("FD_State_Cadastral_Valuation");
            DataExportCommon.AddAttribute(xmlFile, xnLandValuation, "Version", "02");
            xmlFile.AppendChild(xnLandValuation);

            using (Log.TimeOperation("Добавление общей информации в файл"))
            {
                AddXmlGeneralInfo(xmlFile, xnLandValuation);
            }

            var dictNodes = new Dictionary<long, XmlNode>();
            using (Log.TimeOperation("Добавление основной информации в файл"))
            {
                AddXmlPackage(xmlFile, xnLandValuation, subgroup, dictNodes, message);
            }
            xmlFile.AppendChild(xnLandValuation);

            var stream = new MemoryStream();
            xmlFile.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public static void AddXmlGeneralInfo(XmlDocument xmlFile, XmlNode parent)
        {
            XmlNode xnGeneralInformation = xmlFile.CreateElement("General_Information");

            #region RegionsRF
            XmlNode xnRegionsRf = xmlFile.CreateElement("RegionsRF");
            XmlNode xnRegionRf = xmlFile.CreateElement("RegionRF");
            xnRegionRf.InnerText = ConfigurationManager.AppSettings["3XML_RegionRF"];
            xnRegionsRf.AppendChild(xnRegionRf);
            xnGeneralInformation.AppendChild(xnRegionsRf);
            #endregion

            #region Contract_Evaluation
            XmlNode xnContractEvaluation = xmlFile.CreateElement("Contract_Evaluation");

            #region Details
            XmlNode xnDetails = xmlFile.CreateElement("Details");
            XmlNode xnDateDoc = xmlFile.CreateElement("Date_Doc");
            xnDateDoc.InnerText = ConfigurationManager.AppSettings["3XML_Detail_Date_Doc"].ParseToDateTime().ToString(DataExportCommon.Culture);
            xnDetails.AppendChild(xnDateDoc);

            XmlNode xnNDoc = xmlFile.CreateElement("N_Doc");
            xnNDoc.InnerText = ConfigurationManager.AppSettings["3XML_Detail_N_Doc"];
            xnDetails.AppendChild(xnNDoc);

            XmlNode xnDocName = xmlFile.CreateElement("Name");
            xnDocName.InnerText = ConfigurationManager.AppSettings["3XML_Detail_DocName"];
            xnDetails.AppendChild(xnDocName);

            xnContractEvaluation.AppendChild(xnDetails);
            #endregion

            #region Customer
            XmlNode xnCustomer = xmlFile.CreateElement("Customer");
            XmlNode xnCustomerName = xmlFile.CreateElement("Name");
            xnCustomerName.InnerText = ConfigurationManager.AppSettings["3XML_Customer_Name"];
            xnCustomer.AppendChild(xnCustomerName);

            XmlNode xnCustomerCodeOgrn = xmlFile.CreateElement("Code_OGRN");
            xnCustomerCodeOgrn.InnerText = ConfigurationManager.AppSettings["3XML_Customer_Code_OGRN"];
            xnCustomer.AppendChild(xnCustomerCodeOgrn);

            XmlNode xnCustomerAddress = xmlFile.CreateElement("Address");
            xnCustomerAddress.InnerText = ConfigurationManager.AppSettings["3XML_Customer_Address"];
            xnCustomer.AppendChild(xnCustomerAddress);

            xnContractEvaluation.AppendChild(xnCustomer);
            #endregion

            #region Administrant
            XmlNode xnAdministrant = xmlFile.CreateElement("Administrant");
            XmlNode xnJuridic = xmlFile.CreateElement("Juridic");
            XmlNode xnAdministrantName = xmlFile.CreateElement("Name");
            xnAdministrantName.InnerText = ConfigurationManager.AppSettings["3XML_Administrant_Name"];
            xnJuridic.AppendChild(xnAdministrantName);

            XmlNode xnAdministrantCodeOgrn = xmlFile.CreateElement("Code_OGRN");
            xnAdministrantCodeOgrn.InnerText = ConfigurationManager.AppSettings["3XML_Administrant_Code_OGRN"];
            xnJuridic.AppendChild(xnAdministrantCodeOgrn);

            XmlNode xnAdministrantAddress = xmlFile.CreateElement("Address");
            xnAdministrantAddress.InnerText = ConfigurationManager.AppSettings["3XML_Administrant_Address"];
            xnJuridic.AppendChild(xnAdministrantAddress);

            xnAdministrant.AppendChild(xnJuridic);
            xnContractEvaluation.AppendChild(xnAdministrant);
            #endregion

            xnGeneralInformation.AppendChild(xnContractEvaluation);
            #endregion

            #region Report_Details
            XmlNode xnReportDetails = xmlFile.CreateElement("Report_Details");
            DataExportCommon.AddAttribute(xmlFile, xnReportDetails, "Date", ConfigurationManager.AppSettings["3XML_Report_Details_Date"].ParseToDateTime().ToString(DataExportCommon.Culture));
            DataExportCommon.AddAttribute(xmlFile, xnReportDetails, "Number", ConfigurationManager.AppSettings["3XML_Report_Details_Number"]);

            #region Appraisers
            XmlNode xnAppraisers = xmlFile.CreateElement("Appraisers");

            #region App_1
            XmlNode xnAppraiser = xmlFile.CreateElement("Appraiser");
            XmlNode xnAppraiserFio = xmlFile.CreateElement("FIO");
            XmlNode xnAppraiserF = xmlFile.CreateElement("Surname");
            xnAppraiserF.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserF"];
            xnAppraiserFio.AppendChild(xnAppraiserF);

            XmlNode xnAppraiserI = xmlFile.CreateElement("First");
            xnAppraiserI.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserI"];
            xnAppraiserFio.AppendChild(xnAppraiserI);

            XmlNode xnAppraiserO = xmlFile.CreateElement("Patronymic");
            xnAppraiserO.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserO"];
            xnAppraiserFio.AppendChild(xnAppraiserO);

            xnAppraiser.AppendChild(xnAppraiserFio);

            XmlNode xnAppraiserNameOrg = xmlFile.CreateElement("Name_Org");
            xnAppraiserNameOrg.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserName_Org"];
            xnAppraiser.AppendChild(xnAppraiserNameOrg);

            xnAppraisers.AppendChild(xnAppraiser);
            #endregion

            #region App_2
            XmlNode xnAppraiser1 = xmlFile.CreateElement("Appraiser");
            XmlNode xnAppraiserFio1 = xmlFile.CreateElement("FIO");
            XmlNode xnAppraiserF1 = xmlFile.CreateElement("Surname");
            xnAppraiserF1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserF1"];
            xnAppraiserFio1.AppendChild(xnAppraiserF1);

            XmlNode xnAppraiserI1 = xmlFile.CreateElement("First");
            xnAppraiserI1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserI1"];
            xnAppraiserFio1.AppendChild(xnAppraiserI1);

            XmlNode xnAppraiserO1 = xmlFile.CreateElement("Patronymic");
            xnAppraiserO1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserO1"];
            xnAppraiserFio1.AppendChild(xnAppraiserO1);

            xnAppraiser1.AppendChild(xnAppraiserFio1);

            XmlNode xnAppraiserNameOrg1 = xmlFile.CreateElement("Name_Org");
            xnAppraiserNameOrg1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserName_Org1"];
            xnAppraiser1.AppendChild(xnAppraiserNameOrg1);

            xnAppraisers.AppendChild(xnAppraiser1);
            #endregion

            xnReportDetails.AppendChild(xnAppraisers);
            #endregion

            xnGeneralInformation.AppendChild(xnReportDetails);
            #endregion

            parent.AppendChild(xnGeneralInformation);
        }

        public static void AddXmlPackage(XmlDocument xmlFile, XmlNode parentNode, OMGroup subgroup, Dictionary<long, XmlNode> dictNodes, string message)
        {
            XmlNode xnPackage = xmlFile.CreateElement("Package");

            AddXnGroupsRealEstates(xmlFile, subgroup, xnPackage);

            EvaluativeFactors(xmlFile, subgroup, dictNodes, message, xnPackage);

            AddAppraise(xmlFile, subgroup, xnPackage);

            parentNode.AppendChild(xnPackage);
        }

        private static void EvaluativeFactors(XmlDocument xmlFile, OMGroup subgroup, IDictionary<long, XmlNode> dictNodes, string message,
            XmlNode xnPackage)
        {
            if (dictNodes.ContainsKey(subgroup.Id))
            {
                var temp = xmlFile.ImportNode(dictNodes[subgroup.Id], true);
                xnPackage.AppendChild(temp);
            }
            else
            {
                XmlNode xnEvaluativeFactors = xmlFile.CreateElement("Evaluative_Factors");
                var model = OMModel.Where(x => x.GroupId == subgroup.Id && x.IsActive.Coalesce(false) == true).SelectAll()
                    .ExecuteFirstOrDefault();
                Log.ForContext("ModelId", model?.Id).ForContext("SubgroupId", subgroup.Id)
                    .Debug($"Поиск активной модели для погруппы {subgroup.GroupName}. Модель найдена - {model != null}");
                if (model != null)
                {
                    if (model.ModelFactor.Count == 0)
                    {
                        model.ModelFactor = OMModelFactor
                            .Where(x => x.ModelId == model.Id && x.AlgorithmType_Code == model.AlgoritmType_Code).SelectAll()
                            .Execute();
                        Log.Debug(
                            $"Найдено {model.ModelFactor.Count} факторов типа {model.AlgoritmType_Code.GetEnumDescription()} для модели");
                    }

                    var countCurr = 0;
                    var countAll = model.ModelFactor.Count;
                    var marks = OMMarkCatalog.Where(x => x.GroupId == model.GroupId).SelectAll().Execute();
                    foreach (var factor in model.ModelFactor)
                    {
                        var attributeFactor = RegisterCache.GetAttributeData((int) factor.FactorId);
                        factor.FillMarkCatalogsFromList(marks, model.GroupId ?? 0);

                        XmlNode xnEvaluativeFactor = xmlFile.CreateElement("Evaluative_Factor");
                        DataExportCommon.AddAttribute(xmlFile, xnEvaluativeFactor, "Id_Factor",
                            factor.FactorId + "_" + subgroup.Id);
                        DataExportCommon.AddAttribute(xmlFile, xnEvaluativeFactor, "Type", factor.SignMarket ? "1" : "2");
                        XmlNode xnNameFactor = xmlFile.CreateElement("Name_Factor");
                        xnNameFactor.InnerText = attributeFactor.Name;
                        xnEvaluativeFactor.AppendChild(xnNameFactor);

                        XmlNode xnNameFactorDesc = xmlFile.CreateElement("Name_Factor_Desc");
                        xnNameFactorDesc.InnerText = attributeFactor.Description;
                        xnEvaluativeFactor.AppendChild(xnNameFactorDesc);
                        var addfactor = false;
                        if (attributeFactor.Type == RegisterAttributeType.STRING)
                        {
                            if (factor.MarkCatalogs.Count > 0)
                            {
                                XmlNode xnQualitativeValues = xmlFile.CreateElement("QualitativeValues");
                                foreach (var mark in factor.MarkCatalogs)
                                {
                                    XmlNode xnQualitativeValueRoot = xmlFile.CreateElement("QualitativeValue");

                                    XmlNode xnQualitativeId = xmlFile.CreateElement("Qualitative_Id");
                                    xnQualitativeId.InnerText = mark.Id.ToString();
                                    xnQualitativeValueRoot.AppendChild(xnQualitativeId);

                                    XmlNode xnQualitativeValue = xmlFile.CreateElement("Qualitative_Value");
                                    xnQualitativeValue.InnerText = mark.ValueFactor.ToUpper();
                                    xnQualitativeValueRoot.AppendChild(xnQualitativeValue);

                                    xnQualitativeValues.AppendChild(xnQualitativeValueRoot);
                                    addfactor = true;
                                }

                                xnEvaluativeFactor.AppendChild(xnQualitativeValues);
                            }
                            else
                            {
                                XmlNode xnQuantitativeDimension = xmlFile.CreateElement("Quantitative_Dimension");
                                xnQuantitativeDimension.InnerText = "Метр";
                                xnEvaluativeFactor.AppendChild(xnQuantitativeDimension);
                                addfactor = true;
                            }
                        }
                        else
                        {
                            XmlNode xnQuantitativeDimension = xmlFile.CreateElement("Quantitative_Dimension");
                            xnQuantitativeDimension.InnerText = "Метр";
                            xnEvaluativeFactor.AppendChild(xnQuantitativeDimension);
                            addfactor = true;
                        }

                        if (addfactor)
                            xnEvaluativeFactors.AppendChild(xnEvaluativeFactor);

                        countCurr++;
                        var strMessage = message + " -- " + "Evaluative_Factors " + countCurr + " из " +
                                         countAll;
                        Console.WriteLine(strMessage);
                    }
                }

                xnPackage.AppendChild(xnEvaluativeFactors);
                dictNodes.Add(subgroup.Id, xnEvaluativeFactors);
            }
        }

        private static void AddAppraise(XmlDocument xmlFile, OMGroup subgroup, XmlNode xnPackage)
        {
            XmlNode xnAppraise = xmlFile.CreateElement("Appraise");
            var regionRf = ConfigurationManager.AppSettings["3XML_RegionRF"];

            Log.Debug($"Механизм группировки подгруппы - '{subgroup.GroupAlgoritm_Code}'");

            var objectIds = subgroup.Unit.Select(x => x.ObjectId.GetValueOrDefault()).ToList();
            var allAttributes = new GbuObjectService().GetAllAttributes(objectIds, null, new List<long> {3, 4, 5, 8, 600});

            if (subgroup.GroupAlgoritm_Code == KoGroupAlgoritm.Model)
            {
                AddXnStatisticModelling(xmlFile, subgroup, allAttributes, regionRf, xnAppraise);
            }
            else
            {
                AddXnOther(xmlFile, subgroup, allAttributes, regionRf, xnAppraise);
            }

            xnPackage.AppendChild(xnAppraise);
        }

        private static void AddXnGroupsRealEstates(XmlDocument xmlFile, OMGroup subgroup, XmlNode xnPackage)
        {
            XmlNode xnGroupsRealEstates = xmlFile.CreateElement("Groups_Real_Estates");

            XmlNode xnGroupRealEstate = xmlFile.CreateElement("Group_Real_Estate");
            XmlNode xnIdGroup = xmlFile.CreateElement("ID_Group");
            xnIdGroup.InnerText = subgroup.Id.ToString();
            xnGroupRealEstate.AppendChild(xnIdGroup);

            XmlNode xnNameGroup = xmlFile.CreateElement("Name_Group");
            xnNameGroup.InnerText = subgroup.GroupName;
            xnGroupRealEstate.AppendChild(xnNameGroup);

            xnGroupsRealEstates.AppendChild(xnGroupRealEstate);
            xnPackage.AppendChild(xnGroupsRealEstates);
        }

        private static void AddXnOther(XmlDocument xmlFile, OMGroup subgroup, List<GbuObjectAttribute> allAttributes, string regionRf,
            XmlNode xnAppraise)
        {
            XmlNode xnOther = xmlFile.CreateElement("Other");
            XmlNode xnEvaluationGroup = xmlFile.CreateElement("Evaluation_Group");

            XmlNode xnDescription = xmlFile.CreateElement("Description");
            xnDescription.InnerText = subgroup.GroupName; //TODO???    // Desc_SubGroup;
            xnEvaluationGroup.AppendChild(xnDescription);

            XmlNode xnRealEstates = xmlFile.CreateElement("Real_Estates");

            var currentUnitsCount = 0;

            foreach (var unit in subgroup.Unit)
            {
                var xnRealEstate = GetXnRealEstate(xmlFile, subgroup, allAttributes, regionRf, unit, ref currentUnitsCount);

                xnRealEstates.AppendChild(xnRealEstate);
            }

            xnEvaluationGroup.AppendChild(xnRealEstates);
            xnOther.AppendChild(xnEvaluationGroup);
            xnAppraise.AppendChild(xnOther);
        }

        private static void AddXnStatisticModelling(XmlDocument xmlFile, OMGroup subgroup, List<GbuObjectAttribute> allAttributes, string regionRf,
            XmlNode xnAppraise)
        {
            var model = OMModel.Where(x => x.GroupId == subgroup.Id && x.IsActive.Coalesce(false) == true).SelectAll()
                .ExecuteFirstOrDefault();

            XmlNode xnStatisticalModelling = xmlFile.CreateElement("Statistical_Modelling");
            XmlNode xnGroupRealEstateModelling = xmlFile.CreateElement("Group_Real_Estate_Modelling");
            DataExportCommon.AddAttribute(xmlFile, xnGroupRealEstateModelling, "ID_Group", subgroup.Id.ToString());

            XmlNode xnRatingModel = xmlFile.CreateElement("Rating_Model");
            if (model != null) xnRatingModel.InnerText = model.AlgoritmType;
            xnGroupRealEstateModelling.AppendChild(xnRatingModel);

            XmlNode xnRealEstates = xmlFile.CreateElement("Real_Estates");

            var currentUnitsCount = 0;
            Log.Debug("Начата обработка ЕО подгруппы");

            var marks = new List<OMMarkCatalog>();
            if (model != null)
                marks = OMMarkCatalog.Where(x => x.GroupId == model.GroupId).SelectAll().Execute();
            var factorReestrId = OMGroup.GetFactorReestrId(subgroup);

            foreach (var unit in subgroup.Unit)
            {
                var xnRealEstate = GetXnRealEstate(xmlFile, subgroup, allAttributes, regionRf, unit, ref currentUnitsCount);
                AddXnEvaluativeFactors(xmlFile, subgroup, unit, factorReestrId, model, marks, xnRealEstate);
                xnRealEstates.AppendChild(xnRealEstate);
            }

            Log.Debug("Закончена обработка ЕО подгруппы");
            xnGroupRealEstateModelling.AppendChild(xnRealEstates);

            AddXnEvaluativeFactorsModelling(xmlFile, subgroup, model, marks, xnGroupRealEstateModelling);

            xnStatisticalModelling.AppendChild(xnGroupRealEstateModelling);
            xnAppraise.AppendChild(xnStatisticalModelling);
        }

        private static void AddXnEvaluativeFactorsModelling(XmlDocument xmlFile, OMGroup subgroup, OMModel model, List<OMMarkCatalog> marks,
            XmlNode xnGroupRealEstateModelling)
        {
            XmlNode xnEvaluativeFactorsModelling = xmlFile.CreateElement("Evaluative_Factors_Modelling");

            if (model != null)
            {
                foreach (var factorModel in model.ModelFactor)
                {
                    factorModel.FillMarkCatalogsFromList(marks, model.GroupId);

                    XmlNode xnEvaluativeFactorModelling = xmlFile.CreateElement("Evaluative_Factor_Modelling");
                    DataExportCommon.AddAttribute(xmlFile, xnEvaluativeFactorModelling, "Id_factor",
                        factorModel.Id + "_" + subgroup.Id);
                    if (factorModel.SignMarket)
                    {
                        if (factorModel.MarkCatalogs.Count > 0)
                        {
                            XmlNode xnCastAccounts = xmlFile.CreateElement("Cast_Accounts");
                            foreach (var mark in factorModel.MarkCatalogs)
                            {
                                XmlNode xnCastAccount = xmlFile.CreateElement("Cast_Account");

                                XmlNode xnQualitativeId = xmlFile.CreateElement("Qualitative_Id");
                                xnQualitativeId.InnerText = mark.Id.ToString();
                                xnCastAccount.AppendChild(xnQualitativeId);

                                XmlNode xnDimension = xmlFile.CreateElement("Dimension");
                                xnDimension.InnerText = mark.MetkaFactor.ToString();
                                xnCastAccount.AppendChild(xnDimension);

                                xnCastAccounts.AppendChild(xnCastAccount);
                            }

                            xnEvaluativeFactorModelling.AppendChild(xnCastAccounts);
                        }
                    }

                    xnEvaluativeFactorsModelling.AppendChild(xnEvaluativeFactorModelling);
                }
            }

            xnGroupRealEstateModelling.AppendChild(xnEvaluativeFactorsModelling);
        }

        private static XmlNode GetXnRealEstate(XmlDocument xmlFile, OMGroup subgroup, List<GbuObjectAttribute> allAttributes, string regionRf,
            OMUnit unit, ref int currentUnitsCount)
        {
            if (++currentUnitsCount % 1000 == 0 || currentUnitsCount == 1)
                Log.Debug(
                    $"Начата обработка ЕО №{currentUnitsCount} из {subgroup.Unit.Count} (Заполнение информации по ГБУ-атрибутам)");

            XmlNode xnRealEstate = xmlFile.CreateElement("Real_Estate");
            DataExportCommon.AddAttribute(xmlFile, xnRealEstate, "ID_Group", subgroup.Id.ToString());

            XmlNode xnCadastralNumber = xmlFile.CreateElement("CadastralNumber");
            xnCadastralNumber.InnerText = unit.CadastralNumber;
            xnRealEstate.AppendChild(xnCadastralNumber);

            XmlNode xnCadastralType = xmlFile.CreateElement("Type");
            xnCadastralType.InnerText = unit.PropertyType_Code.ToString();
            xnRealEstate.AppendChild(xnCadastralType);

            if (subgroup.GroupAlgoritm_Code != KoGroupAlgoritm.Model)
            {
                XmlNode xnSpecificCadastralCost = xmlFile.CreateElement("Specific_CadastralCost");
                DataExportCommon.AddAttribute(xmlFile, xnSpecificCadastralCost, "Value",
                    unit.Upks.ToString());
                DataExportCommon.AddAttribute(xmlFile, xnSpecificCadastralCost, "Unit", "1002");
                xnRealEstate.AppendChild(xnSpecificCadastralCost);
            }

            XmlNode xnCadastralArea = xmlFile.CreateElement("Area");
            xnCadastralArea.InnerText = unit.Square.ToString();
            xnRealEstate.AppendChild(xnCadastralArea);

            AddPropertyTypeInfo(xmlFile, allAttributes, unit, xnRealEstate);

            XmlNode xnCadastralLocation = xmlFile.CreateElement("Location");
            XmlNode xnCadastralRegion = xmlFile.CreateElement("Region");
            xnCadastralRegion.InnerText = regionRf;
            xnCadastralLocation.AppendChild(xnCadastralRegion);

            if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 600, out var valueAttr600)) //Код 600 - Адрес
            {
                XmlNode xnCadastralNote = xmlFile.CreateElement("Note");
                xnCadastralNote.InnerText = valueAttr600;
                xnCadastralLocation.AppendChild(xnCadastralNote);
            }

            if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 8, out var valueAttr8)) //Код 8 - Местоположение
            {
                XmlNode xnCadastralOther = xmlFile.CreateElement("Other");
                xnCadastralOther.InnerText = valueAttr8;
                xnCadastralLocation.AppendChild(xnCadastralOther);
            }

            xnRealEstate.AppendChild(xnCadastralLocation);

            XmlNode xnDateValuation = xmlFile.CreateElement("Date_valuation");
            xnDateValuation.InnerText =
                ConfigurationManager.AppSettings["ucAppDate"].ParseToDateTime().ToString("yyyy-MM-dd");
            xnRealEstate.AppendChild(xnDateValuation);
            return xnRealEstate;
        }

        private static void AddXnEvaluativeFactors(XmlDocument xmlFile, OMGroup subgroup, OMUnit unit, int? factorReestrId, OMModel model,
            List<OMMarkCatalog> marks, XmlNode xnRealEstate)
        {
            XmlNode xnCEvaluativeFactors = xmlFile.CreateElement("Evaluative_Factors");

            //Получаем список факторов группы
            var factorValuesGroup = new List<CalcItem>();
            DataTable data;
            using (Log.TimeOperation("Получение атрибутов"))
            {
                data = CoreGetAttributesTrimmed.GetAttributes((int) unit.Id, factorReestrId.Value);
            }

            if (data != null)
            {
                Log.Verbose("Data rows {RowCount}",data.Rows.Count);
                foreach (DataRow row in data.Rows)
                {
                    factorValuesGroup.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString(),
                        row.ItemArray[7].ParseToString()));
                }
            }

            if (model != null)
            {
                if (model.ModelFactor.Count == 0)
                    model.ModelFactor = OMModelFactor
                        .Where(x => x.ModelId == model.Id && x.AlgorithmType_Code == model.AlgoritmType_Code).SelectAll()
                        .Execute();
                foreach (var factorModel in model.ModelFactor)
                {
                    var findf = false;
                    var valueItem = string.Empty; //TODO: значение фактора для данного объекта
                    var factorItem = factorValuesGroup.Find(x => x.FactorId == factorModel.FactorId);
                    if (factorItem != null)
                    {
                        findf = true;
                        valueItem = factorItem.Value;
                    }

                    if (findf) // если фактор найден
                    {
                        XmlNode xnCEvaluativeFactor = xmlFile.CreateElement("Evaluative_Factor");
                        DataExportCommon.AddAttribute(xmlFile, xnCEvaluativeFactor, "ID_Factor",
                            factorModel.FactorId + "_" + subgroup.Id);

                        factorModel.FillMarkCatalogsFromList(marks, model.GroupId ?? 0);

                        var addf = false;
                        if (factorModel.SignMarket)
                        {
                            var mark = factorModel.MarkCatalogs.Find(x => x.ValueFactor == valueItem);
                            if (mark != null)
                            {
                                XmlNode xnQualitativeId = xmlFile.CreateElement("Qualitative_Id");
                                xnQualitativeId.InnerText = mark.Id.ToString();
                                xnCEvaluativeFactor.AppendChild(xnQualitativeId);
                                addf = true;
                            }
                        }
                        else
                        {
                            XmlNode xnQuantitativeValue = xmlFile.CreateElement("Quantitative_Value");
                            xnQuantitativeValue.InnerText = valueItem.ToUpper();
                            xnCEvaluativeFactor.AppendChild(xnQuantitativeValue);
                            addf = true;
                        }

                        if (addf)
                            xnCEvaluativeFactors.AppendChild(xnCEvaluativeFactor);
                    }
                }
            }

            xnRealEstate.AppendChild(xnCEvaluativeFactors);
        }

        private static void AddPropertyTypeInfo(XmlDocument xmlFile, List<GbuObjectAttribute> allAttributes, OMUnit unit, XmlNode xnRealEstate)
        {
            if (unit.PropertyType_Code == PropertyTypes.Stead)
            {
                if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 3, out var valueAttr3)
                ) //Код категории земель из ГКН
                {
                    XmlNode xnAssignation = xmlFile.CreateElement("Category");
                    DataExportCommon.AddAttribute(xmlFile, xnAssignation, "Name", valueAttr3);
                    xnRealEstate.AppendChild(xnAssignation);
                }

                if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 4, out var valueAttr4)
                ) //Код вида использования по документам
                {
                    XmlNode xnUtilization = xmlFile.CreateElement("Utilization");
                    DataExportCommon.AddAttribute(xmlFile, xnUtilization, "Name_doc", valueAttr4);
                    xnRealEstate.AppendChild(xnUtilization);
                }
            }
            else
            {
                #region Assignation

                XmlNode xnAssignation = xmlFile.CreateElement("Assignation");
                if (DataExportCommon.GetObjectAttribute(allAttributes, unit, 5, out var valueAttr5)
                ) //Код Вид использования по классификатору
                {
                    switch (unit.PropertyType_Code)
                    {
                        case PropertyTypes.Building:
                        {
                            XmlNode xnAssignationBuilding = xmlFile.CreateElement("Assignation_Building");
                            XmlNode xnAssBuilding = xmlFile.CreateElement("Ass_Building");
                            xnAssBuilding.InnerText = valueAttr5;
                            xnAssignationBuilding.AppendChild(xnAssBuilding);
                            xnAssignation.AppendChild(xnAssignationBuilding);
                            break;
                        }
                        case PropertyTypes.Pllacement:
                        {
                            XmlNode xnAssignationFlat = xmlFile.CreateElement("Assignation_Flat");
                            XmlNode xnAssFlat = xmlFile.CreateElement("Ass_Flat");
                            xnAssFlat.InnerText = valueAttr5;
                            xnAssignationFlat.AppendChild(xnAssFlat);
                            xnAssignation.AppendChild(xnAssignationFlat);
                            break;
                        }
                        case PropertyTypes.Construction:
                        case PropertyTypes.UncompletedBuilding:
                        {
                            XmlNode xnFormalizedConstrUncompleted = xmlFile.CreateElement("Formalized_Constr_Uncompleted");
                            xnFormalizedConstrUncompleted.InnerText = valueAttr5;
                            xnAssignation.AppendChild(xnFormalizedConstrUncompleted);
                            break;
                        }
                    }

                    if (valueAttr5 != "005002002999")
                        xnRealEstate.AppendChild(xnAssignation);
                }

                #endregion
            }
        }
    }
}