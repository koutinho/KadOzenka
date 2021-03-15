using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using DevExpress.Data.Extensions;
using Ionic.Zip;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.Register;
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
        public static List<ResultKoUnloadSettings> ExportToXml(OMUnloadResultQueue unloadResultQueue,
            KOUnloadSettings setting, SetProgress setProgress)
        {
            Log.ForContext("InputParameters", setting, true)
                .Debug("Начата выгрузка в XML результатов Кадастровой оценки по группам");

            const string progressMessage = "Выгрузка в XML результатов Кадастровой оценки по группам";
            var progress = 0;
            if (unloadResultQueue != null)
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);

            // Подгруппы тура
            var tourGroups = OMTourGroup
                .Where(x => x.TourId == setting.IdTour)
                .Select(x => x.GroupId).Execute()
                .Select(x => x.GroupId).ToList();

            // Основные группы ОКС/ЗУ
            var mainGroupType = setting.UnloadParcel ? KoGroupAlgoritm.MainParcel : KoGroupAlgoritm.MainOKS;
            var mainGroups = OMGroup.Where(x => x.ParentId == -1 && x.GroupAlgoritm_Code == mainGroupType)
                .Select(x=>x.Id).Execute()
                .Select(x=>(long?)x.Id).ToList();

            // Выбор подгрупп тура с разделением по ОКС\ЗУ
            var koGroups = OMGroup.Where(x => x.ParentId != -1 && mainGroups.Contains(x.ParentId) && tourGroups.Contains(x.Id)).SelectAll().Execute();
            var res = new List<ResultKoUnloadSettings>();
            var countCurr = 0;
            var countAll = koGroups.Count;
            Log.Debug("Найдено {countAll} подгрупп", countAll);

            foreach (var subgroup in koGroups)
            {
                Log.ForContext("SubGroupId", subgroup.Id)
                    .Debug("Начата работа с подгруппой '{GroupName}', №{countCurr} из {countAll}", subgroup.GroupName,
                        countCurr, countAll);

                countCurr++;
                var strMessage = "Выгружается группа " + countCurr + " (Id=" + subgroup.Id + ") из " + countAll;
                Console.WriteLine(strMessage);
                var taskCounter = 0;
                var syncRoot = new object();
                ParallelOptions options = new ParallelOptions {MaxDegreeOfParallelism = 8};
                var curr = countCurr;
                Parallel.ForEach(setting.TaskFilter, options, taskId =>
                    //foreach (var taskId in setting.TaskFilter)
                {
                    Log.Debug("Начата работа с ЗнО {TaskId}", taskId);
                    var export = new DEKOGroupExport(setting, subgroup, taskId, unloadResultQueue.Id);
                    export.Export(curr, res);

                    lock (syncRoot)
                    {
                        taskCounter++;
                        progress = (taskCounter * 100 / setting.TaskFilter.Count + (curr - 1) * 100) /
                                   koGroups.Count;
                        setProgress?.Invoke(progress, progressMessage: progressMessage);
                        if (unloadResultQueue != null)
                            KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                });
            }

            setProgress?.Invoke(100, true, progressMessage);
            if (unloadResultQueue != null)
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);

            Log.Debug("Закончена выгрузка в XML результатов Кадастровой оценки по группам");

            return res;
        }

        internal class DEKOGroupExportBase
        {
            private const long UnitThresholdForMemoryStream = 1_000_000;
            private readonly bool _singleQueryMode;
            private readonly KOUnloadSettings _setting;
            private readonly OMGroup _subgroup;
            private readonly long _taskId;
            private readonly XmlDocument _xmlFile;
            private readonly OMModel _model;
            private readonly Stopwatch _stopwatch;
            private long _unloadId;
            private long _unitCount;
            private Dictionary<long?, List<OMMarkCatalog>> _marks;
            private CancellationToken _token;

            protected DEKOGroupExportBase(KOUnloadSettings setting, OMGroup subgroup, long taskId, long unloadId) : this(subgroup,
                taskId, unloadId)
            {
                _setting = setting;
            }

            protected DEKOGroupExportBase(OMGroup subgroup, long taskId, long unloadId)
            {
                _unloadId = unloadId;
                _subgroup = subgroup;
                _taskId = taskId;
                _singleQueryMode = true;
                _xmlFile = new XmlDocument();
                //_calcItemDict = new Dictionary<long, List<CalcItem>>();
                _stopwatch = new Stopwatch();
                _model = OMModel.Where(x => x.GroupId == _subgroup.Id && x.IsActive.Coalesce(true) == true)
                    .SelectAll()
                    .ExecuteFirstOrDefault();
                _marks = new Dictionary<long?, List<OMMarkCatalog>>();
            }

            protected void FullExport(int countCurr, List<ResultKoUnloadSettings> res)
            {
                var factorRegisterId = OMGroup.GetFactorReestrId(_subgroup);
                Log.Debug("FactorReestrId {factorReestrId} GetGroupAttributes", factorRegisterId);

                _unitCount = OMUnit
                    .Where(x => x.GroupId == _subgroup.Id && x.TaskId == _taskId && x.CadastralCost > 0)
                    .OrderBy(x => x.Id)
                    .ExecuteCount();
                Log.Debug(
                    "Найдено {subgroupUnitCount} ЕО с группой {subgroupId} и ЗнО {taskId}, у которых положительная Кадастровая стоимость",
                    _unitCount, _subgroup.Id, _taskId);

                if (_unitCount <= 0) return;

                Stream resultFile;
                using (Log.TimeOperation("Формирование файла для подгруппы {GroupName}", _subgroup.GroupName))
                {
                    resultFile = SaveXmlDocument();
                }

                using (resultFile)
                {
                    var parentGroup = OMGroup.Where(x => x.Id == _subgroup.ParentId).SelectAll()
                        .ExecuteFirstOrDefault();
                    var fullGroupNum = GetFullGroupNum(parentGroup, _subgroup);
                    var fileName = GetFdFileName(_taskId, fullGroupNum, _subgroup, countCurr);

                    if (_setting.IsDataComparingUnload)
                    {
                        using (Log.TimeOperation("Копирование файла для сравнения '{fileName}'", fileName))
                        {
                            var fullFileName = Path.Combine(_setting.DirectoryName, $"{fileName}.xml");
                            using var fs = File.Create(fullFileName);
                            fs.Seek(0, SeekOrigin.Begin);
                            resultFile.CopyTo(fs);
                        }
                    }
                    else
                    {
                        long id;
                        using (Log.TimeOperation("Сохранение файла '{fileName}'", fileName))
                        {
                            var zf = new ZipFile();
                            zf.AddEntry(fileName+".xml", resultFile);
                            using (var fs = new FileStream(Path.GetTempFileName(), FileMode.Open, FileAccess.ReadWrite,
                                FileShare.None, 4096, FileOptions.DeleteOnClose))
                            {
                                zf.Save(fs);
                                fs.Seek(0, SeekOrigin.Begin);
                                id = SaveUnloadResult.SaveResult(fileName, fs, _unloadId, "zip", KoUnloadResultType.UnloadXML2);
                            }
                        }

                        var fileResult = new ResultKoUnloadSettings
                        {
                            FileName = fileName,
                            FileId = id,
                            TaskId = _taskId
                        };
                        res.Add(fileResult);
                    }
                }
            }

            private string GetFullGroupNum(OMGroup parentGroup, OMGroup subgroup)
            {
                var fullGroupNum = (parentGroup.Number ?? parentGroup.Id.ToString()) + "." +
                                   (subgroup.Number ?? subgroup.Id.ToString());
                fullGroupNum = fullGroupNum.Replace("\n", "");
                return fullGroupNum;
            }

            private string GetFdFileName(long taskId, string fullGroupNum, OMGroup subgroup, int countCurr)
            {
                return "Task_" + taskId + "_" + fullGroupNum
                       + "_FD_State_Cadastral_Valuation_"
                       + subgroup.Id.ToString().PadLeft(5, '0')
                       + countCurr.ToString().PadLeft(5, '0');
            }

            /// <summary>
            /// Экспорт в Xml - КОценка по группам.
            /// </summary>
            protected Stream SaveXmlDocument()
            {
                //var xmlFile = new XmlDocument();
                XmlNode xnLandValuation = _xmlFile.CreateElement("FD_State_Cadastral_Valuation");
                DataExportCommon.AddAttribute(_xmlFile, xnLandValuation, "Version", "02");
                _xmlFile.AppendChild(xnLandValuation);

                using (Log.TimeOperation("Добавление общей информации в файл"))
                {
                    var xnGeneralInformation = GetXmlGeneralInfo();
                    xnLandValuation.AppendChild(xnGeneralInformation);
                }

                var dictNodes = new Dictionary<long, XmlNode>();
                using (Log.TimeOperation("Добавление основной информации в файл"))
                {
                    AddXmlPackage(xnLandValuation, dictNodes);
                }

                _xmlFile.AppendChild(xnLandValuation);

                Stream stream;
                if (_unitCount < UnitThresholdForMemoryStream)
                {
                    stream = new MemoryStream();
                }
                else
                {
                    stream = new FileStream(Path.GetTempFileName(), FileMode.Open, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose);
                }

                _xmlFile.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }

            private XmlNode GetXmlGeneralInfo()
            {
                XmlNode xnGeneralInformation = _xmlFile.CreateElement("General_Information");

                #region RegionsRF

                XmlNode xnRegionsRf = _xmlFile.CreateElement("RegionsRF");
                XmlNode xnRegionRf = _xmlFile.CreateElement("RegionRF");
                xnRegionRf.InnerText = ConfigurationManager.AppSettings["3XML_RegionRF"];
                xnRegionsRf.AppendChild(xnRegionRf);
                xnGeneralInformation.AppendChild(xnRegionsRf);

                #endregion

                #region Contract_Evaluation

                XmlNode xnContractEvaluation = _xmlFile.CreateElement("Contract_Evaluation");

                #region Details

                XmlNode xnDetails = _xmlFile.CreateElement("Details");
                XmlNode xnDateDoc = _xmlFile.CreateElement("Date_Doc");
                xnDateDoc.InnerText = ConfigurationManager.AppSettings["3XML_Detail_Date_Doc"].ParseToDateTime()
                    .ToString(DataExportCommon.Culture);
                xnDetails.AppendChild(xnDateDoc);

                XmlNode xnNDoc = _xmlFile.CreateElement("N_Doc");
                xnNDoc.InnerText = ConfigurationManager.AppSettings["3XML_Detail_N_Doc"];
                xnDetails.AppendChild(xnNDoc);

                XmlNode xnDocName = _xmlFile.CreateElement("Name");
                xnDocName.InnerText = ConfigurationManager.AppSettings["3XML_Detail_DocName"];
                xnDetails.AppendChild(xnDocName);

                xnContractEvaluation.AppendChild(xnDetails);

                #endregion

                #region Customer

                XmlNode xnCustomer = _xmlFile.CreateElement("Customer");
                XmlNode xnCustomerName = _xmlFile.CreateElement("Name");
                xnCustomerName.InnerText = ConfigurationManager.AppSettings["3XML_Customer_Name"];
                xnCustomer.AppendChild(xnCustomerName);

                XmlNode xnCustomerCodeOgrn = _xmlFile.CreateElement("Code_OGRN");
                xnCustomerCodeOgrn.InnerText = ConfigurationManager.AppSettings["3XML_Customer_Code_OGRN"];
                xnCustomer.AppendChild(xnCustomerCodeOgrn);

                XmlNode xnCustomerAddress = _xmlFile.CreateElement("Address");
                xnCustomerAddress.InnerText = ConfigurationManager.AppSettings["3XML_Customer_Address"];
                xnCustomer.AppendChild(xnCustomerAddress);

                xnContractEvaluation.AppendChild(xnCustomer);

                #endregion

                #region Administrant

                XmlNode xnAdministrant = _xmlFile.CreateElement("Administrant");
                XmlNode xnJuridic = _xmlFile.CreateElement("Juridic");
                XmlNode xnAdministrantName = _xmlFile.CreateElement("Name");
                xnAdministrantName.InnerText = ConfigurationManager.AppSettings["3XML_Administrant_Name"];
                xnJuridic.AppendChild(xnAdministrantName);

                XmlNode xnAdministrantCodeOgrn = _xmlFile.CreateElement("Code_OGRN");
                xnAdministrantCodeOgrn.InnerText = ConfigurationManager.AppSettings["3XML_Administrant_Code_OGRN"];
                xnJuridic.AppendChild(xnAdministrantCodeOgrn);

                XmlNode xnAdministrantAddress = _xmlFile.CreateElement("Address");
                xnAdministrantAddress.InnerText = ConfigurationManager.AppSettings["3XML_Administrant_Address"];
                xnJuridic.AppendChild(xnAdministrantAddress);

                xnAdministrant.AppendChild(xnJuridic);
                xnContractEvaluation.AppendChild(xnAdministrant);

                #endregion

                xnGeneralInformation.AppendChild(xnContractEvaluation);

                #endregion

                #region Report_Details

                XmlNode xnReportDetails = _xmlFile.CreateElement("Report_Details");
                DataExportCommon.AddAttribute(_xmlFile, xnReportDetails, "Date",
                    ConfigurationManager.AppSettings["3XML_Report_Details_Date"].ParseToDateTime()
                        .ToString(DataExportCommon.Culture));
                DataExportCommon.AddAttribute(_xmlFile, xnReportDetails, "Number",
                    ConfigurationManager.AppSettings["3XML_Report_Details_Number"]);

                #region Appraisers

                XmlNode xnAppraisers = _xmlFile.CreateElement("Appraisers");

                #region App_1

                XmlNode xnAppraiser = _xmlFile.CreateElement("Appraiser");
                XmlNode xnAppraiserFio = _xmlFile.CreateElement("FIO");
                XmlNode xnAppraiserF = _xmlFile.CreateElement("Surname");
                xnAppraiserF.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserF"];
                xnAppraiserFio.AppendChild(xnAppraiserF);

                XmlNode xnAppraiserI = _xmlFile.CreateElement("First");
                xnAppraiserI.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserI"];
                xnAppraiserFio.AppendChild(xnAppraiserI);

                XmlNode xnAppraiserO = _xmlFile.CreateElement("Patronymic");
                xnAppraiserO.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserO"];
                xnAppraiserFio.AppendChild(xnAppraiserO);

                xnAppraiser.AppendChild(xnAppraiserFio);

                XmlNode xnAppraiserNameOrg = _xmlFile.CreateElement("Name_Org");
                xnAppraiserNameOrg.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserName_Org"];
                xnAppraiser.AppendChild(xnAppraiserNameOrg);

                xnAppraisers.AppendChild(xnAppraiser);

                #endregion

                #region App_2

                XmlNode xnAppraiser1 = _xmlFile.CreateElement("Appraiser");
                XmlNode xnAppraiserFio1 = _xmlFile.CreateElement("FIO");
                XmlNode xnAppraiserF1 = _xmlFile.CreateElement("Surname");
                xnAppraiserF1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserF1"];
                xnAppraiserFio1.AppendChild(xnAppraiserF1);

                XmlNode xnAppraiserI1 = _xmlFile.CreateElement("First");
                xnAppraiserI1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserI1"];
                xnAppraiserFio1.AppendChild(xnAppraiserI1);

                XmlNode xnAppraiserO1 = _xmlFile.CreateElement("Patronymic");
                xnAppraiserO1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserO1"];
                xnAppraiserFio1.AppendChild(xnAppraiserO1);

                xnAppraiser1.AppendChild(xnAppraiserFio1);

                XmlNode xnAppraiserNameOrg1 = _xmlFile.CreateElement("Name_Org");
                xnAppraiserNameOrg1.InnerText = ConfigurationManager.AppSettings["3XML_AppraiserName_Org1"];
                xnAppraiser1.AppendChild(xnAppraiserNameOrg1);

                xnAppraisers.AppendChild(xnAppraiser1);

                #endregion

                xnReportDetails.AppendChild(xnAppraisers);

                #endregion

                xnGeneralInformation.AppendChild(xnReportDetails);

                #endregion

                return xnGeneralInformation;
            }

            private void AddXmlPackage(XmlNode parentNode, Dictionary<long, XmlNode> dictNodes)
            {
                XmlNode xnPackage = _xmlFile.CreateElement("Package");

                var xnGroupsRealEstates = GetXnGroupsRealEstates();
                xnPackage.AppendChild(xnGroupsRealEstates);

                EvaluativeFactors(dictNodes, xnPackage);

                AddAppraise(xnPackage);

                parentNode.AppendChild(xnPackage);
            }

            private void EvaluativeFactors(IDictionary<long, XmlNode> dictNodes, XmlNode xnPackage)
            {
                if (dictNodes.ContainsKey(_subgroup.Id))
                {
                    var temp = _xmlFile.ImportNode(dictNodes[_subgroup.Id], true);
                    xnPackage.AppendChild(temp);
                }
                else
                {
                    XmlNode xnEvaluativeFactors = _xmlFile.CreateElement("Evaluative_Factors");
//                    model = OMModel.Where(x => x.GroupId == _subgroup.Id && x.IsActive.Coalesce(false) == true)
//                        .SelectAll()
//                        .ExecuteFirstOrDefault();
                    Log.ForContext("ModelId", _model?.Id).ForContext("SubgroupId", _subgroup.Id)
                        .Debug(
                            "Поиск активной модели для подгруппы {GroupName}. Модель найдена - {modelFound}",
                            _subgroup.GroupName, _model != null);
                    if (_model != null)
                    {
                        if (_model.ModelFactor.Count == 0)
                        {
                            _model.ModelFactor = OMModelFactor
                                .Where(x => x.ModelId == _model.Id && x.AlgorithmType_Code == _model.AlgoritmType_Code)
                                .SelectAll()
                                .Execute();
                            Log.Debug(
                                "Найдено {ModelFactorCount} факторов типа {AlgorithmTypeCode} для модели",
                                _model.ModelFactor.Count, _model.AlgoritmType_Code.GetEnumDescription());
                        }

                        _marks = GetMarks();
                        foreach (var factor in _model.ModelFactor)
                        {
                            _token.ThrowIfCancellationRequested();
                            var attributeFactor = RegisterCache.GetAttributeData(factor.FactorId.GetValueOrDefault());
                            factor.FillMarkCatalogsFromList(_marks);

                            XmlNode xnEvaluativeFactor = _xmlFile.CreateElement("Evaluative_Factor");
                            DataExportCommon.AddAttribute(_xmlFile, xnEvaluativeFactor, "Id_Factor",
                                factor.FactorId + "_" + _subgroup.Id);
                            DataExportCommon.AddAttribute(_xmlFile, xnEvaluativeFactor, "Type",
                                factor.SignMarket ? "1" : "2");
                            XmlNode xnNameFactor = _xmlFile.CreateElement("Name_Factor");
                            xnNameFactor.InnerText = attributeFactor.Name;
                            xnEvaluativeFactor.AppendChild(xnNameFactor);

                            XmlNode xnNameFactorDesc = _xmlFile.CreateElement("Name_Factor_Desc");
                            xnNameFactorDesc.InnerText = attributeFactor.Description;
                            xnEvaluativeFactor.AppendChild(xnNameFactorDesc);
                            var addfactor = false;
                            if (attributeFactor.Type == RegisterAttributeType.STRING && factor.MarkCatalogs.Count > 0)
                            {
                                XmlNode xnQualitativeValues = _xmlFile.CreateElement("QualitativeValues");
                                foreach (var mark in factor.MarkCatalogs)
                                {
                                    _token.ThrowIfCancellationRequested();
                                    XmlNode xnQualitativeValueRoot = _xmlFile.CreateElement("QualitativeValue");

                                    XmlNode xnQualitativeId = _xmlFile.CreateElement("Qualitative_Id");
                                    xnQualitativeId.InnerText = mark.Id.ToString();
                                    xnQualitativeValueRoot.AppendChild(xnQualitativeId);

                                    XmlNode xnQualitativeValue = _xmlFile.CreateElement("Qualitative_Value");
                                    xnQualitativeValue.InnerText = mark.ValueFactor.ToUpper();
                                    xnQualitativeValueRoot.AppendChild(xnQualitativeValue);

                                    xnQualitativeValues.AppendChild(xnQualitativeValueRoot);
                                    addfactor = true;
                                }

                                xnEvaluativeFactor.AppendChild(xnQualitativeValues);
                            }
                            else
                            {
                                XmlNode xnQuantitativeDimension = _xmlFile.CreateElement("Quantitative_Dimension");
                                xnQuantitativeDimension.InnerText = "Метр";
                                xnEvaluativeFactor.AppendChild(xnQuantitativeDimension);
                                addfactor = true;
                            }

                            if (addfactor)
                                xnEvaluativeFactors.AppendChild(xnEvaluativeFactor);
                        }
                    }

                    xnPackage.AppendChild(xnEvaluativeFactors);
                    dictNodes.Add(_subgroup.Id, xnEvaluativeFactors);
                }
            }

            private Dictionary<long?, List<OMMarkCatalog>> GetMarks()
            {
                List<OMMarkCatalog> omMarkCatalogs;
                using (Log.TimeOperation("Получение меток"))
                {
                    omMarkCatalogs = OMMarkCatalog.Where(x => x.GroupId == _model.GroupId)
                        .SelectAll().Execute();
                }

                using (Log.TimeOperation("Группировка {marksCount} меток по факторам", omMarkCatalogs.Count))
                {
                    return omMarkCatalogs.GroupBy(x => x.FactorId).ToDictionary(x => x.Key, x => x.ToList());
                }
            }

            private void AddAppraise(XmlNode xnPackage)
            {
                XmlNode xnAppraise = _xmlFile.CreateElement("Appraise");
                var regionRf = ConfigurationManager.AppSettings["3XML_RegionRF"];

                Log.Debug("Механизм группировки подгруппы - '{GroupAlgoritm}'", _subgroup.GroupAlgoritm_Code);

                //_attributesByObject = GetAttributesByObject();

                if (_subgroup.GroupAlgoritm_Code == KoGroupAlgoritm.Model)
                {
                    AddXnStatisticModelling(regionRf, xnAppraise);
                }
                else
                {
                    AddXnOther(regionRf, xnAppraise);
                }

                xnPackage.AppendChild(xnAppraise);
            }

            private Dictionary<long, List<GbuObjectAttribute>> GetAttributesByObject(List<long> objectIds = null)
            {
                if (objectIds == null)
                    objectIds = OMUnit.Where(x => x.GroupId == _subgroup.Id && x.TaskId == _taskId && x.CadastralCost > 0)
                        .Select(x=>x.ObjectId).Execute().Select(x => x.ObjectId.GetValueOrDefault()).ToList();
                var allAttributes = new List<GbuObjectAttribute>();
                using (Log.TimeOperation("Получение ГБУ атрибутов"))
                {
                    new GbuObjectService().GetAllAttributes(objectIds, null, new List<long> {3, 4, 5, 8, 600});
                }
                Log.Verbose("Получено {attributesCount} атрибутов для {objectsCount} объектов", allAttributes.Count,
                    objectIds.Count);
                var res = allAttributes.GroupBy(x => x.ObjectId).ToDictionary(x => x.Key, x => x.ToList());
                return res;
            }

            private XmlNode GetXnGroupsRealEstates()
            {
                XmlNode xnGroupsRealEstates = _xmlFile.CreateElement("Groups_Real_Estates");

                XmlNode xnGroupRealEstate = _xmlFile.CreateElement("Group_Real_Estate");
                XmlNode xnIdGroup = _xmlFile.CreateElement("ID_Group");
                xnIdGroup.InnerText = _subgroup.Id.ToString();
                xnGroupRealEstate.AppendChild(xnIdGroup);

                XmlNode xnNameGroup = _xmlFile.CreateElement("Name_Group");
                xnNameGroup.InnerText = _subgroup.GroupName;
                xnGroupRealEstate.AppendChild(xnNameGroup);

                xnGroupsRealEstates.AppendChild(xnGroupRealEstate);
                return xnGroupsRealEstates;
            }

            private void AddXnOther(string regionRf, XmlNode xnAppraise)
            {
                XmlNode xnOther = _xmlFile.CreateElement("Other");
                XmlNode xnEvaluationGroup = _xmlFile.CreateElement("Evaluation_Group");

                XmlNode xnDescription = _xmlFile.CreateElement("Description");
                xnDescription.InnerText = _subgroup.GroupName; //TODO???    // Desc_SubGroup;
                xnEvaluationGroup.AppendChild(xnDescription);

                XmlNode xnRealEstates = _xmlFile.CreateElement("Real_Estates");

                var currentUnitsCount = 0;

                _stopwatch.Restart();
                foreach (var unitInfo in GetUnitInfos(false))
                {
                    _token.ThrowIfCancellationRequested();
                    var xnRealEstate = GetXnRealEstate(regionRf, unitInfo,
                        ref currentUnitsCount);

                    xnRealEstates.AppendChild(xnRealEstate);
                    if (currentUnitsCount % 1000 == 0)
                    {
                        Log.Debug("Обработка 1000 ГБУ атрибутов: {elapsed} ms, длина словаря: {dictLength}",
                            _stopwatch.ElapsedMilliseconds, _unitInfoSize); //_attributesByObject.Count);
                        _stopwatch.Restart();
                    }
                }

                _stopwatch.Reset();

                xnEvaluationGroup.AppendChild(xnRealEstates);
                xnOther.AppendChild(xnEvaluationGroup);
                xnAppraise.AppendChild(xnOther);
            }

            private void AddXnStatisticModelling(string regionRf, XmlNode xnAppraise)
            {
//                var model = OMModel.Where(x => x.GroupId == _subgroup.Id && x.IsActive.Coalesce(false) == true)
//                    .SelectAll()
//                    .ExecuteFirstOrDefault();

                XmlNode xnStatisticalModelling = _xmlFile.CreateElement("Statistical_Modelling");
                XmlNode xnGroupRealEstateModelling = _xmlFile.CreateElement("Group_Real_Estate_Modelling");
                DataExportCommon.AddAttribute(_xmlFile, xnGroupRealEstateModelling, "ID_Group",
                    _subgroup.Id.ToString());

                XmlNode xnRatingModel = _xmlFile.CreateElement("Rating_Model");
                if (_model != null) xnRatingModel.InnerText = _model.AlgoritmType;
                xnGroupRealEstateModelling.AppendChild(xnRatingModel);

                XmlNode xnRealEstates = _xmlFile.CreateElement("Real_Estates");

                var currentUnitsCount = 0;
                Log.Debug("Начата обработка ЕО подгруппы");

//                var marks = new Dictionary<long?,List<OMMarkCatalog>>();
//                if (_model != null)
//                    marks = GetMarks();
                var factorRegisterId = OMGroup.GetFactorReestrId(_subgroup).ParseToInt();

//                if (_singleQueryMode)
//                    _calcItemDict = GetGroupFactors(factorRegisterId, _subgroup.Id, _taskId);

                _stopwatch.Restart();
                foreach (var unitInfo in GetUnitInfos(true))
                {
                    _token.ThrowIfCancellationRequested();
                    var xnRealEstate = GetXnRealEstate(regionRf, unitInfo,
                        ref currentUnitsCount);
                    if (_singleQueryMode)
                        AddXnEvaluativeFactors(_marks, xnRealEstate, unitInfo.CalcItems);
                    else
                        AddXnEvaluativeFactors(_marks, xnRealEstate, unitInfo.Unit, factorRegisterId);
                    xnRealEstates.AppendChild(xnRealEstate);

                    if (currentUnitsCount % 1000 == 0)
                    {
                        Log.Debug("Обработка 1000 ГБУ атрибутов: {elapsed} ms, длина словаря: {dictLength}",
                            _stopwatch.ElapsedMilliseconds, _unitInfoSize); //_attributesByObject.Count);
                        _stopwatch.Restart();
                    }
                }

                _stopwatch.Reset();

                Log.Debug("Закончена обработка ЕО подгруппы");
                xnGroupRealEstateModelling.AppendChild(xnRealEstates);

                AddXnEvaluativeFactorsModelling(xnGroupRealEstateModelling);

                xnStatisticalModelling.AppendChild(xnGroupRealEstateModelling);
                xnAppraise.AppendChild(xnStatisticalModelling);
            }

            private void AddXnEvaluativeFactorsModelling(XmlNode xnGroupRealEstateModelling)
            {
                XmlNode xnEvaluativeFactorsModelling = _xmlFile.CreateElement("Evaluative_Factors_Modelling");

                if (_model != null)
                {
                    foreach (var factorModel in _model.ModelFactor)
                    {
                        _token.ThrowIfCancellationRequested();
                        factorModel.FillMarkCatalogsFromList(_marks);

                        XmlNode xnEvaluativeFactorModelling = _xmlFile.CreateElement("Evaluative_Factor_Modelling");
                        DataExportCommon.AddAttribute(_xmlFile, xnEvaluativeFactorModelling, "Id_factor",
                            factorModel.Id + "_" + _subgroup.Id);
                        if (factorModel.SignMarket)
                        {
                            if (factorModel.MarkCatalogs.Count > 0)
                            {
                                XmlNode xnCastAccounts = _xmlFile.CreateElement("Cast_Accounts");
                                foreach (var mark in factorModel.MarkCatalogs)
                                {
                                    _token.ThrowIfCancellationRequested();
                                    XmlNode xnCastAccount = _xmlFile.CreateElement("Cast_Account");

                                    XmlNode xnQualitativeId = _xmlFile.CreateElement("Qualitative_Id");
                                    xnQualitativeId.InnerText = mark.Id.ToString();
                                    xnCastAccount.AppendChild(xnQualitativeId);

                                    XmlNode xnDimension = _xmlFile.CreateElement("Dimension");
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

            private XmlNode GetXnRealEstate(string regionRf, UnitInfo unitInfo, ref int currentUnitsCount)
            {
                if (++currentUnitsCount % 1000 == 0 || currentUnitsCount == 1)
                {
                    Log.Debug(
                        "Начата обработка ЕО №{currentUnitsCount} из {subgroupUnitCount} (Заполнение информации по ГБУ-атрибутам)",
                        currentUnitsCount, _unitCount);
                }

                XmlNode xnRealEstate = _xmlFile.CreateElement("Real_Estate");
                DataExportCommon.AddAttribute(_xmlFile, xnRealEstate, "ID_Group", _subgroup.Id.ToString());

                XmlNode xnCadastralNumber = _xmlFile.CreateElement("CadastralNumber");
                xnCadastralNumber.InnerText = unitInfo.Unit.CadastralNumber;
                xnRealEstate.AppendChild(xnCadastralNumber);

                XmlNode xnCadastralType = _xmlFile.CreateElement("Type");
                xnCadastralType.InnerText = unitInfo.Unit.PropertyType_Code.ToString();
                xnRealEstate.AppendChild(xnCadastralType);

                if (_subgroup.GroupAlgoritm_Code != KoGroupAlgoritm.Model)
                {
                    XmlNode xnSpecificCadastralCost = _xmlFile.CreateElement("Specific_CadastralCost");
                    DataExportCommon.AddAttribute(_xmlFile, xnSpecificCadastralCost, "Value",
                        unitInfo.Unit.Upks.ToString());
                    DataExportCommon.AddAttribute(_xmlFile, xnSpecificCadastralCost, "Unit", "1002");
                    xnRealEstate.AppendChild(xnSpecificCadastralCost);
                }

                XmlNode xnCadastralArea = _xmlFile.CreateElement("Area");
                xnCadastralArea.InnerText = unitInfo.Unit.Square.ToString();
                xnRealEstate.AppendChild(xnCadastralArea);

                AddPropertyTypeInfo(unitInfo, xnRealEstate);

                XmlNode xnCadastralLocation = _xmlFile.CreateElement("Location");
                XmlNode xnCadastralRegion = _xmlFile.CreateElement("Region");
                xnCadastralRegion.InnerText = regionRf;
                xnCadastralLocation.AppendChild(xnCadastralRegion);

                if (DataExportCommon.GetObjectAttribute(unitInfo, 600, out var valueAttr600)
                ) //Код 600 - Адрес
                {
                    XmlNode xnCadastralNote = _xmlFile.CreateElement("Note");
                    xnCadastralNote.InnerText = valueAttr600;
                    xnCadastralLocation.AppendChild(xnCadastralNote);
                }

                if (DataExportCommon.GetObjectAttribute(unitInfo, 8, out var valueAttr8)
                ) //Код 8 - Местоположение
                {
                    XmlNode xnCadastralOther = _xmlFile.CreateElement("Other");
                    xnCadastralOther.InnerText = valueAttr8;
                    xnCadastralLocation.AppendChild(xnCadastralOther);
                }

                xnRealEstate.AppendChild(xnCadastralLocation);

                XmlNode xnDateValuation = _xmlFile.CreateElement("Date_valuation");
                xnDateValuation.InnerText =
                    ConfigurationManager.AppSettings["ucAppDate"].ParseToDateTime().ToString("yyyy-MM-dd");
                xnRealEstate.AppendChild(xnDateValuation);
                return xnRealEstate;
            }

            private void AddXnEvaluativeFactors(Dictionary<long?, List<OMMarkCatalog>> marks, XmlNode xnRealEstate,
                List<CalcItem> calcItems)
            {
                XmlNode xnCEvaluativeFactors = _xmlFile.CreateElement("Evaluative_Factors");

                //Получаем список факторов группы
//            var factorValuesGroup = new List<CalcItem>();
//            DataTable data;

                Modelling(marks, calcItems, xnCEvaluativeFactors);

                xnRealEstate.AppendChild(xnCEvaluativeFactors);
            }

            private void AddXnEvaluativeFactors(Dictionary<long?, List<OMMarkCatalog>> marks, XmlNode xnRealEstate,
                OMUnit unit,
                int factorReestrId)
            {
                XmlNode xnCEvaluativeFactors = _xmlFile.CreateElement("Evaluative_Factors");

                //Получаем список факторов группы
//            var factorValuesGroup = new List<CalcItem>();
//            DataTable data;
                List<CalcItem> calcItems;
                //                // data = CoreGetAttributesTrimmed.GetAttributes((int) unit.Id, factorReestrId.Value);
                calcItems = GetFactors(factorReestrId, unit.Id).OrderBy(x => x.FactorId).ToList();

                Modelling(marks, calcItems, xnCEvaluativeFactors);

                xnRealEstate.AppendChild(xnCEvaluativeFactors);
            }

            private void Modelling(Dictionary<long?, List<OMMarkCatalog>> marks, List<CalcItem> calcItems,
                XmlNode xnCEvaluativeFactors)
            {
                if (_model == null) return;
                foreach (var factorModel in _model.ModelFactor)
                {
                    _token.ThrowIfCancellationRequested();
                    string valueItem; //TODO: значение фактора для данного объекта
                    var factorItem = calcItems.Find(x => x.FactorId == factorModel.FactorId);
                    if (factorItem == null) continue;

                    valueItem = factorItem.Value;
                    XmlNode xnCEvaluativeFactor = _xmlFile.CreateElement("Evaluative_Factor");
                    DataExportCommon.AddAttribute(_xmlFile, xnCEvaluativeFactor, "ID_Factor",
                        factorModel.FactorId + "_" + _subgroup.Id);

                    factorModel.FillMarkCatalogsFromList(marks);

                    if (factorModel.SignMarket)
                    {
                        var mark = factorModel.MarkCatalogs.Find(x => x.ValueFactor == valueItem);
                        if (mark == null) continue;

                        XmlNode xnQualitativeId = _xmlFile.CreateElement("Qualitative_Id");
                        xnQualitativeId.InnerText = mark.Id.ToString();
                        xnCEvaluativeFactor.AppendChild(xnQualitativeId);
                        xnCEvaluativeFactors.AppendChild(xnCEvaluativeFactor);
                    }
                    else
                    {
                        XmlNode xnQuantitativeValue = _xmlFile.CreateElement("Quantitative_Value");
                        xnQuantitativeValue.InnerText = valueItem.ToUpper();
                        xnCEvaluativeFactor.AppendChild(xnQuantitativeValue);
                        xnCEvaluativeFactors.AppendChild(xnCEvaluativeFactor);
                    }
                }
            }

            private void AddPropertyTypeInfo(UnitInfo unitInfo, XmlNode xnRealEstate)
            {
                if (unitInfo.Unit.PropertyType_Code == PropertyTypes.Stead)
                {
                    if (DataExportCommon.GetObjectAttribute(unitInfo, 3, out var valueAttr3)
                    ) //Код категории земель из ГКН
                    {
                        XmlNode xnAssignation = _xmlFile.CreateElement("Category");
                        DataExportCommon.AddAttribute(_xmlFile, xnAssignation, "Name", valueAttr3);
                        xnRealEstate.AppendChild(xnAssignation);
                    }

                    if (DataExportCommon.GetObjectAttribute(unitInfo, 4, out var valueAttr4)
                    ) //Код вида использования по документам
                    {
                        XmlNode xnUtilization = _xmlFile.CreateElement("Utilization");
                        DataExportCommon.AddAttribute(_xmlFile, xnUtilization, "Name_doc", valueAttr4);
                        xnRealEstate.AppendChild(xnUtilization);
                    }
                }
                else
                {
                    #region Assignation

                    XmlNode xnAssignation = _xmlFile.CreateElement("Assignation");
                    if (DataExportCommon.GetObjectAttribute(unitInfo, 5, out var valueAttr5)
                    ) //Код Вид использования по классификатору
                    {
                        switch (unitInfo.Unit.PropertyType_Code)
                        {
                            case PropertyTypes.Building:
                            {
                                XmlNode xnAssignationBuilding = _xmlFile.CreateElement("Assignation_Building");
                                XmlNode xnAssBuilding = _xmlFile.CreateElement("Ass_Building");
                                xnAssBuilding.InnerText = valueAttr5;
                                xnAssignationBuilding.AppendChild(xnAssBuilding);
                                xnAssignation.AppendChild(xnAssignationBuilding);
                                break;
                            }
                            case PropertyTypes.Pllacement:
                            {
                                XmlNode xnAssignationFlat = _xmlFile.CreateElement("Assignation_Flat");
                                XmlNode xnAssFlat = _xmlFile.CreateElement("Ass_Flat");
                                xnAssFlat.InnerText = valueAttr5;
                                xnAssignationFlat.AppendChild(xnAssFlat);
                                xnAssignation.AppendChild(xnAssignationFlat);
                                break;
                            }
                            case PropertyTypes.Construction:
                            case PropertyTypes.UncompletedBuilding:
                            {
                                XmlNode xnFormalizedConstrUncompleted =
                                    _xmlFile.CreateElement("Formalized_Constr_Uncompleted");
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

            private Dictionary<long, List<CalcItem>> GetGroupFactors(long factorRegisterId, long groupId, long taskId, List<long> objectsId = null)
            {
                using (Log.TimeOperation("Выгрузка атрибутов для группы {groupId} из реестра {factorRegisterId}",
                    groupId, factorRegisterId))
                {
                    OMGroup group = OMGroup.Where(x => x.Id == groupId).SelectAll().ExecuteFirstOrDefault();
                    if (group.GroupAlgoritm_Code != KoGroupAlgoritm.Model)
                        return new Dictionary<long, List<CalcItem>>();

                    var allAttr = GetTourFactorAttributesByRegisterId(factorRegisterId);
                    var attr = allAttr
                        .Where(x => !x.IsPrimaryKey.GetValueOrDefault(false))
                        //.Where(x=>modelFactors.Contains(x.Id))
                        .ToList();
                    var primary = allAttr.FirstOrDefault(x => x.IsPrimaryKey.GetValueOrDefault(false));

                    if (primary == null) return new Dictionary<long, List<CalcItem>>();
                    var registerPrimaryKeyColumn = new QSColumnSimple(primary.Id);

                    if (objectsId == null)
                        objectsId = OMUnit
                            .Where(x => x.GroupId == groupId && x.TaskId == taskId)
                            .Select(x => x.Id).Execute()
                            .Select(x => x.Id).ToList();
                    if (objectsId.Count == 0)
                    {
                        return new Dictionary<long, List<CalcItem>>();
                    }

                    var query = new QSQuery
                    {
                        MainRegisterID = (int) factorRegisterId,
                        Condition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.In,
                            LeftOperand = registerPrimaryKeyColumn,
                            RightOperand = new QSColumnConstant(objectsId)
                        },
                        OrderBy = new List<QSOrder>
                        {
                            new QSOrder {Column = registerPrimaryKeyColumn}
                        }
                    };

                    attr.ForEach(attribute => { query.AddColumn(attribute.Id, attribute.Id.ToString()); });

                    DataTable dt;
                    try
                    {
                        dt = query.ExecuteQuery();
                    }
                    catch (Exception e)
                    {
                        Log.Warning(e, "Ошибка выгрузки из реестра №{factorRegisterId}", factorRegisterId);
                        return new Dictionary<long, List<CalcItem>>();
                    }

                    var calcItemCount = 0;
                    var dict = new Dictionary<long, List<CalcItem>>();
                    using (Log.TimeOperation("Парсинг DataTable в Dictionary<long,List<CalcItem>>"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            _token.ThrowIfCancellationRequested();
                            var calcItems = new List<CalcItem>();
                            foreach (DataColumn column in dt.Columns)
                            {
                                var ordinal = column.Ordinal;
                                if (ordinal == 0) continue;

                                var x = row.ItemArray[ordinal];
                                if (x is DBNull) continue;

                                calcItemCount++;
                                calcItems.Add(new CalcItem(column.ColumnName.ParseToLong(), x.ToString(), null));
                            }

                            dict.Add(row.ItemArray[0].ParseToLong(), calcItems);
                        }

                        Log.Verbose(
                            "Количество записей в словаре: {dictRecordsCount}, CalcItems: {calcItemCount}. Исходная таблица - строк: {Rows}, столбцов: {Columns}"
                            , dict.Count, calcItemCount, dt.Rows.Count, dt.Columns.Count);
                    }

                    return dict;
                }
            }

            private List<CalcItem> GetFactors(long factorRegisterId, long objectId)
            {
                using (Log.TimeOperation("Выгрузка атрибутов для объекта {objectId} из реестра {factorRegisterId}",
                    objectId, factorRegisterId))
                {
                    var allAttr = GetTourFactorAttributesByRegisterId(factorRegisterId);
                    var attr = allAttr.Where(x => !x.IsPrimaryKey.GetValueOrDefault(false)).ToList();
                    var primary = allAttr.FirstOrDefault(x => x.IsPrimaryKey.GetValueOrDefault(false));

                    if (primary == null) return new List<CalcItem>();
                    var registerPrimaryKeyColumn = new QSColumnSimple(primary.Id);

                    var query = new QSQuery
                    {
                        MainRegisterID = (int) factorRegisterId,
                        Condition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.In,
                            LeftOperand = registerPrimaryKeyColumn,
                            RightOperand = new QSColumnConstant(objectId)
                        }
                    };

                    attr.ForEach(attribute => { query.AddColumn(attribute.Id, attribute.Id.ToString()); });

                    //var sql = query.GetSql();
                    var dt = query.ExecuteQuery();

                    var calcItems = new List<CalcItem>();
                    using (Log.TimeOperation("Парсинг DataTable в CalcItem"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            _token.ThrowIfCancellationRequested();
                            foreach (DataColumn column in dt.Columns)
                            {
                                var ordinal = column.Ordinal;
                                if (ordinal == 0) continue;

                                var x = row.ItemArray[ordinal];
                                if (x is DBNull) continue;

                                calcItems.Add(new CalcItem(column.ColumnName.ParseToLong(), x.ToString(), null));
                            }
                        }
                    }

                    Log.Verbose(
                        "Количество записей в списке: {calcItemCount}. Исходная таблица - строк: {Rows}, столбцов: {Columns}",
                        calcItems.Count, dt.Rows.Count, dt.Columns.Count);
                    return calcItems;
                }
            }

            private List<OMAttribute> GetTourFactorAttributesByRegisterId(long registerId)
            {
                var registers = OMTourFactorRegister.Where(x => x.RegisterId == registerId).SelectAll().Execute();
                var registerIds = registers.Select(x => x.RegisterId).Distinct().ToList();

                return OMAttribute
                    .Where(x => registerIds.Contains(x.RegisterId)
                        //&& x.IsDeleted.Coalesce(false) == false
                    )
                    .OrderBy(x => x.Name).SelectAll().Execute();
            }

            private const int BatchSize = 100_000;
            private int _unitInfoSize;

            protected IEnumerable<UnitInfo> GetUnitInfos(bool loadGroupFactors)
            {
                List<UnitInfo> unitInfos = new List<UnitInfo>();
                var unitInfosCounter = 0;
                var factorRegisterId = OMGroup.GetFactorReestrId(_subgroup).ParseToInt();
                var batchNum = 0;
                var unitsQuery = OMUnit
                    .Where(x => x.GroupId == _subgroup.Id && x.TaskId == _taskId && x.CadastralCost > 0)
                    .OrderBy(x => x.Id)
                    .Select(x => new
                        {x.Id, x.CadastralNumber, x.PropertyType_Code, x.Square, x.Upks, x.ObjectId, x.CreationDate});
                var unitsCount = unitsQuery.ExecuteCount();
                for (var i = 0; i < unitsCount; i++)
                {
                    // Подгрузка данных
                    if (i % BatchSize == 0)
                    {
                        var sw = new Stopwatch();
                        sw.Start();
                        unitInfos.Clear();
                        unitInfosCounter = 0;
                        var query = unitsQuery.SetPackageSize(BatchSize).SetPackageIndex(batchNum);
                        _unitInfoSize = query.ExecuteCount();
                        var units = query.Execute();
                        //var sql = query.GetSql();
                        var objectIds = units.Select(x => x.ObjectId.GetValueOrDefault()).ToList();
                        var unitIds = units.Select(x=> x.Id).ToList();
                        var attributes = GetAttributesByObject(objectIds);
                        Dictionary<long, List<CalcItem>> factors = new Dictionary<long, List<CalcItem>>();
                        if (loadGroupFactors)
                        {
                            factors = GetGroupFactors(factorRegisterId, _subgroup.Id, _taskId, unitIds);
                        }

                        foreach (var unit in units)
                        {
                            _token.ThrowIfCancellationRequested();
                            attributes.TryGetValue(unit.ObjectId.GetValueOrDefault(), out var attributeList);
                            var info = new UnitInfo();
                            info.Unit = unit;
                            info.Attributes = attributeList;
                            if (loadGroupFactors)
                            {
                                factors.TryGetValue(unit.Id, out var factorList);
                                info.CalcItems = factorList;
                            }

                            unitInfos.Add(info);
                        }

                        batchNum++;
                        sw.Stop();
                        Log.Verbose("Подгружена информация по {unitInfoSize} объектов за {unitInfoLoadMilliseconds}",
                            unitsCount, sw.ElapsedMilliseconds);
                    }

                    unitInfos.TryGetValue(unitInfosCounter, out var res);
                    if (res == null) yield break;
                    yield return res;
                    unitInfosCounter++;
                }
            }
        }

        internal class DEKOGroupExport : DEKOGroupExportBase
        {
            public DEKOGroupExport(KOUnloadSettings setting, OMGroup subgroup, long taskId, long unloadId)
                : base(setting, subgroup, taskId, unloadId)
            {
            }

            public void Export(int countCurr, List<ResultKoUnloadSettings> res)
            {
                FullExport(countCurr, res);
            }
        }

        internal class DEKOGroupExportForVuon : DEKOGroupExportBase
        {
            public DEKOGroupExportForVuon(OMGroup subgroup, long taskId, long unloadId)
                : base(subgroup, taskId, unloadId)
            {
            }

            public Stream Export()
            {
                return SaveXmlDocument();
            }
        }

        public class UnitInfo
        {
            public OMUnit Unit { get; set; }
            public List<CalcItem> CalcItems { get; set; }
            public List<GbuObjectAttribute> Attributes { get; set; }
        }
    }
}