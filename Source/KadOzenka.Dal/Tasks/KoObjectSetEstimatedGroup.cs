using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using EP.Ner.Measure.Internal;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.GbuObject.Entities;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Models.Filters;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Units;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.KoObject
{
    #region TypeStructure

    public enum ReportColumns : int
    {
        KnColumn = 0,
        InputFieldColumn = 1,
        ValueColumn = 2,
        OutputFieldColumn = 3,
        ErrorColumn = 4
    }

    public struct ValueItem
    {
        public string Value { get; set; }
        public long? IdDocument { get; set; }
    }

    public struct ComplianceGuid
    {
        public string Group { get; set; }

        public string Code { get; set; }

        //public string TypeRoom { get; set; }
        public long SubGroup { get; set; }

        public override string ToString()
        {
            return $"Код - {Code}, Группа - '{Group}', Подгруппа - {SubGroup}.";
        }
    }

    public class EstimatedGroupModel
    {
        public long IdTask { get; set; }

        public bool OverwriteGroups { get; set; }
        public long IdCodeGroup { get; set; }
        public List<ObjectChangeStatus> ObjectChangeStatus { get; set; }

        /// <summary>
        /// Result parameter.
        /// </summary>
        public long IdEstimatedSubGroup { get; set; }
    }

    #endregion


    public class KoObjectSetEstimatedGroup
    {
        private static readonly ILogger Logger = Log.ForContext<KoObjectSetEstimatedGroup>();
        private GbuObjectService GbuObjectService { get; }
        private GroupService GroupService { get; }

        private UnitService UnitService { get; }

        private GroupCalculationSettingsService GroupCalculationSettingsService { get; }
        private object _locked;

        public int CountAllUnits { get; private set; }
        public int CurrentCount { get; private set; }

        public KoObjectSetEstimatedGroup()
        {
            UnitService = new UnitService();
            GroupCalculationSettingsService = new GroupCalculationSettingsService();
            GbuObjectService = new GbuObjectService();
            GroupService = new GroupService();
        }


        private List<EstimatedGroupSettings> ConvertToEstimateSetting(List<OMTourGroupGroupingSettings> settings)
        {
            GroupingSetting convertToGroupSetting(OMTourGroupGroupingSettings setting)
            {
                return new()
                {
                    KoAttributeId = setting.KoAttributeId.GetValueOrDefault(),
                    Filters = setting.Filter.DeserializeFromXml<Filters>(),
                    DictionaryId = setting.DictionaryId,
                    DictionaryValues = setting.DictionaryValues
                };
            }

            // TODO: ToLookup предположительно более производителен вместо GroupBy().ToDictionary()
            var groupedSettings = settings.GroupBy(x => x.GroupId)
                .ToDictionary(x => x.Key, x => x.ToList());
            var estGroupSetting = groupedSettings.Select(x => new EstimatedGroupSettings
            {
                Priority = long.MaxValue,
                GroupId = x.Key.GetValueOrDefault(),
                GroupingSettings = x.Value.Select(convertToGroupSetting).ToList()
            }).ToList();

            return estGroupSetting;
        }

        private List<EstimatedGroupSettings> EnrichGroupSettings(
            List<EstimatedGroupSettings> estimatedGroupSettingsList,
            List<GroupCalculationSettingsDto> calculationSettingsDtos, List<GroupTreeDto> groupDtos)
        {
            estimatedGroupSettingsList.ForEach(est =>
            {
                var calcGroupDto = calculationSettingsDtos.FirstOrDefault(x => x.GroupId == est.GroupId);
                est.Priority = calcGroupDto?.Priority ?? est.Priority;
                var groupDto = groupDtos.FirstOrDefault(x => x.Id == est.GroupId);
                est.GroupNumber = groupDto?.CombinedNumber ?? "";
                est.GroupDesc = groupDto?.GroupName ?? "";
            });
            return estimatedGroupSettingsList;
        }

        private List<long> GatherFactorsForGrouping(List<EstimatedGroupSettings> settings)
        {
            List<GroupingSetting> groupingSettings = new List<GroupingSetting>();
            settings.ForEach(outerSetting =>
                outerSetting.GroupingSettings.ForEach(innerSetting => groupingSettings.Add(innerSetting)));
            return groupingSettings.Select(x => x.KoAttributeId).Distinct().ToList();
        }

        public string Run(EstimatedGroupModel param)
        {
            Logger.ForContext("InputParameters", JsonConvert.SerializeObject(param))
                .Debug("Входные данные для Присвоения оценочной группы");

            using var reportService = new GbuReportService("Отчет проставления оценочной группы");
            reportService.AddHeaders(new List<string>
            {
                "КН", "Поле в которое производилась запись", "Внесенное значение", "Источник внесенного значения",
                "Ошибка"
            });
            reportService.SetIndividualWidth((int) ReportColumns.KnColumn, 4);
            reportService.SetIndividualWidth((int) ReportColumns.InputFieldColumn, 6);
            reportService.SetIndividualWidth((int) ReportColumns.ValueColumn, 3);
            reportService.SetIndividualWidth((int) ReportColumns.OutputFieldColumn, 6);
            reportService.SetIndividualWidth((int) ReportColumns.ErrorColumn, 5);
            _locked = new object();

            var unitsGetter =
                new EstimatedGroupAffixingUnitsGetter(Logger, param) as AItemsGetter<SetEstimatedGroupUnitPure>;
            unitsGetter = new GbuObjectStatusFilterDecorator<SetEstimatedGroupUnitPure>(unitsGetter, Logger,
                param.ObjectChangeStatus, DateTime.Now.GetEndOfTheDay());

            CountAllUnits = unitsGetter.GetItemsCount();
            Logger.Debug("Всего в БД {MaxUnitsCount} ЕО", CountAllUnits);

            //var estimatedSubGroupAttribute = RegisterCache.GetAttributeData((int) param.IdEstimatedSubGroup);
            //var codeGroupAttribute = RegisterCache.GetAttributeData((int) param.IdCodeGroup);
            var tourId = OMTask.Where(x => x.Id == param.IdTask).Select(x => x.TourId).ExecuteFirstOrDefault().TourId;
            //var allComplianceGuidesInTour = GetAllComplianceGuidesInTour(tourId);

            // var packageSize = 100000;
            // var numberOfPackages = CountAllUnits / packageSize + 1;
            // var generalCancelTokenSource = new CancellationTokenSource();
            // var generalOptions = new ParallelOptions
            // {
            //     CancellationToken = generalCancelTokenSource.Token,
            //     MaxDegreeOfParallelism = 1
            // };

            var allUnitsQuery = param.OverwriteGroups
                ? OMUnit.Where(x => x.TaskId == param.IdTask)
                : OMUnit.Where(x => x.TaskId == param.IdTask && x.GroupId == -1);

            // Трекинг юнитов для отчета
            var allUnits = allUnitsQuery.Select(unit => new {unit.Id, unit.CadastralNumber, unit.PropertyType, unit.PropertyType_Code}).Execute();

            // Сбор данных по группам и приоритету рассчета
            var groupsInfo = GroupService.GetTourGroupsInfo(tourId.GetValueOrDefault(), ObjectTypeExtended.Both);

            // ОКС
            var calcSettingsOks =
                GroupCalculationSettingsService.GetCalculationSettings(tourId.GetValueOrDefault(), false);
            var OksGroups = groupsInfo.OksSubGroups.Select(x => x.Id);
            var OksSettings = OMTourGroupGroupingSettings.Where(x => OksGroups.Contains(x.GroupId)).SelectAll()
                .Execute();
            var convertedOksSettings =
                EnrichGroupSettings(ConvertToEstimateSetting(OksSettings), calcSettingsOks, groupsInfo.OksSubGroups)
                    .OrderBy(x => x.Priority).ToList();


            // ЗУ
            var calcSettingsZu =
                GroupCalculationSettingsService.GetCalculationSettings(tourId.GetValueOrDefault(), true);
            var ZuGroups = groupsInfo.ZuSubGroups.Select(x => x.Id);
            var ZuSettings = OMTourGroupGroupingSettings.Where(x => ZuGroups.Contains(x.GroupId)).SelectAll().Execute();
            var convertedZuSettings =
                EnrichGroupSettings(ConvertToEstimateSetting(ZuSettings), calcSettingsZu, groupsInfo.ZuSubGroups)
                    .OrderBy(x => x.Priority).ToList();

            // Шаблон
            var queryTemplate = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Columns = new List<QSColumn>(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMUnit.GetColumn(x=>x.TaskId), QSConditionType.Equal, param.IdTask)
                    }
                }
            };

            // Шаблон для ЗУ
            var queryTemplateZu = queryTemplate.GetCopy();
            queryTemplateZu.Condition = queryTemplateZu.Condition.And(new QSConditionSimple(OMUnit.GetColumn(x=>x.PropertyType_Code), QSConditionType.Equal, (int) PropertyTypes.Stead));
            var unitsZu = allUnits.Where(x =>x.PropertyType_Code == PropertyTypes.Stead);
            var unitsZuIds = unitsZu.Select(x => x.Id).ToList();

            // Шаблон для ОКС
            var propertyTypesOksForQsQuery = new[] {(double) PropertyTypes.Building,(double) PropertyTypes.Construction,
                (double) PropertyTypes.Pllacement, (double) PropertyTypes.UncompletedBuilding, (double) PropertyTypes.Parking}.AsEnumerable();
            var propertyTypesOks = new[] { PropertyTypes.Building, PropertyTypes.Construction,
                 PropertyTypes.Pllacement,  PropertyTypes.UncompletedBuilding,  PropertyTypes.Parking};
            var queryTemplateOks = queryTemplate.GetCopy();
            queryTemplateOks.Condition = queryTemplateOks.Condition.And(new QSConditionSimple(OMUnit.GetColumn(x=>x.PropertyType_Code), QSConditionType.In, propertyTypesOksForQsQuery));
            var unitsOks = allUnits.Where(x => propertyTypesOks.Contains(x.PropertyType_Code));
            var unitsOksIds = unitsOks.Select(x => x.Id).ToList();


            // Итерации по группам вместо юнитов, однопроходные
            var assignmentReportOks = AssignGroups(convertedOksSettings, queryTemplateOks, unitsOksIds);
            var assignmentReportZu = AssignGroups(convertedZuSettings, queryTemplateZu, unitsZuIds);

            var report = new List<GroupingInfo>();
            report.AddRange(assignmentReportOks);
            report.AddRange(assignmentReportZu);

            foreach (var groupingInfo in report)
            {
                var unitsForGroup = allUnits.Where(x => groupingInfo.UnitIds.Contains(x.Id));
                foreach (var unit in unitsForGroup)
                {
                    if (groupingInfo.GroupId == -1)
                        AddErrorRow(unit.CadastralNumber, $"Не найдено подходящей группы по условиям", reportService);
                    else
                    {
                        AddRowToReport(unit.CadastralNumber, groupingInfo.GroupNumber, reportService);
                    }
                }
            }

            // for (int i = 0; i < numberOfPackages; i++)
            //     //Parallel.For(0, numberOfPackages, generalOptions, (i, s) =>
            // {
            //     ////TODO для тестирования
            //     //var cadasterNumbersForTesting = new List<string> { "77:02:0023003:88", "50:21:0110114:855", "50:26:0150506:743" };
            //     //var currentUnitsPartition = unitsGetter.GetItems(i, packageSize).Where(x => cadasterNumbersForTesting.Contains(x.CadastralNumber)).ToList();
            //     var currentUnitsPartition = unitsGetter.GetItems(i, packageSize);
            //     var gbuObjectIds = currentUnitsPartition.Select(x => x.ObjectId).ToList();
            //     Logger.ForContext("CurrentHandledCount", CurrentCount)
            //         .ForContext("UnitPartitionCount", currentUnitsPartition.Count)
            //         .ForContext("CountAllUnits", CountAllUnits)
            //         .Debug("Начата обработка пакета юнитов №{PackageIndex} из {MaxPackageIndex}", i, numberOfPackages);
            //
            //     //var codeGroups = GetValueFactors(gbuObjectIds, codeGroupAttribute.RegisterId, codeGroupAttribute.Id);
            //     //Logger.Debug("Найдено {CodeGroupsCount} атрибутов с кодом группы для пакета №{PackageIndex}", codeGroups.Count, i);
            //
            //     foreach (var unitPure in currentUnitsPartition)
            //     {
            //         var unit = OMUnit.Where(x => x.Id == unitPure.Id).SelectAll().Execute().FirstOrDefault();
            //         if (unit == null) continue;
            //
            //         List<EstimatedGroupSettings> settingsList = new List<EstimatedGroupSettings>();
            //         if (unit.PropertyType_Code == PropertyTypes.Stead)
            //         {
            //             settingsList = convertedZuSettings;
            //         }
            //         else if (unit.PropertyType_Code is PropertyTypes.Building or PropertyTypes.Construction or
            //             PropertyTypes.Pllacement or PropertyTypes.UncompletedBuilding or PropertyTypes.Parking)
            //         {
            //             settingsList = convertedOksSettings;
            //         }
            //
            //         var unitFactors = UnitService.GetUnitFactors(unit, GatherFactorsForGrouping(settingsList));
            //
            //         bool groupForReport = false;
            //         foreach (var estSetting in settingsList)
            //         {
            //             bool assignToGroup = true;
            //             foreach (var groupingSetting in estSetting.GroupingSettings)
            //             {
            //                 var factor =
            //                     unitFactors.FirstOrDefault(x => x.AttributeId == groupingSetting.KoAttributeId);
            //                 assignToGroup &= ResolveGroup(groupingSetting.Filters, factor);
            //             }
            //
            //             if (!assignToGroup) continue;
            //
            //             groupForReport = true;
            //             AddRowToReport(unitPure.CadastralNumber, estSetting.GroupNumber, reportService);
            //             unit.GroupId = estSetting.GroupId;
            //             unit.Save();
            //             break;
            //         }
            //
            //         if (!groupForReport)
            //         {
            //             AddErrorRow(unitPure.CadastralNumber, $"Не найдено подходящей группы по условиям", reportService);
            //         }
            //     }
            // }
            // //);

            var reportId = reportService.SaveReport();

            Logger.Debug("Закончена операция присвоения оценочной группы");

            return reportService.GetUrlToDownloadFile(reportId);
        }

        private static List<GroupingInfo> AssignGroups(List<EstimatedGroupSettings> convertedSettings, QSQuery queryTemplate, List<long> unitIds)
        {
            var result = new List<GroupingInfo>();
            QSQuery FormQueryWithConditions(EstimatedGroupSettings setting)
            {
                var query = queryTemplate.GetCopy();
                var conditions = setting.GetConditions();
                query.Condition =
                    query.Condition.And(new QSConditionGroup
                    {
                        Type = QSConditionGroupType.And,
                        Conditions = conditions
                    });
                query.ClearSqlCache();
                return query;
            }

            if (convertedSettings.Count == 0) return result;
            foreach (var setting in convertedSettings)
            {
                if (unitIds.Count==0) return result;

                var query = FormQueryWithConditions(setting);

                var toAssign = query.ExecuteQuery<IdHolder>().Select(x => x.Id);
                var intersect = unitIds.Intersect(toAssign).ToList();

                if (intersect.Count == 0) continue;

                var sqlString = query.GetSql();
                var commaSeparatedIdList = intersect.Select(x => x.ToString()).Aggregate((acc, item) => acc + "," + item);
                unitIds = unitIds.Except(intersect).ToList();

                var sql = $"update ko_unit set change_date = now(), group_id = {setting.GroupId} where id in ({commaSeparatedIdList})";
                var updateGroupsCommand = DBMngr.Main.GetSqlStringCommand(sql);
                DBMngr.Main.ExecuteNonQuery(updateGroupsCommand);

                result.Add(new GroupingInfo { GroupId = setting.GroupId, GroupNumber = setting.GroupNumber, UnitIds = intersect});
            }

            if (unitIds.Count > 0)
            {
                result.Add(new GroupingInfo { GroupId = -1, UnitIds = unitIds});
            }

            return result;
        }

        public class IdHolder
        {
            public long Id { get; set; }
        }

        public class GroupingInfo
        {
            public long GroupId { get; set; }

            public string GroupNumber { get; set; }
            public List<long> UnitIds { get; set; } = new();
        }

        #region Help Methods

        private List<OMComplianceGuide> GetAllComplianceGuidesInTour(long? tourId)
        {
            var allComplianceGuidesInTour = OMComplianceGuide.Where(x => x.TourId == tourId).Select(x => new
            {
                x.SubGroup,
                x.TypeProperty,
                x.Code
            }).Execute();

            Logger.Debug("Найдено {ComplianceCount} строк из Таблицы соответствия кода и группы",
                allComplianceGuidesInTour.Count);

            return allComplianceGuidesInTour;
        }

        private void AddValueFactor(long objectId, long? idFactor, long? idDoc, DateTime date, string value)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idFactor.Value,
                ObjectId = objectId,
                ChangeDocId = (idDoc == null) ? -1 : idDoc.Value,
                S = date.Date,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = date.Date.Date,
                StringValue = value,
            };
            attributeValue.Save();
        }

        private Dictionary<long, ValueItem> GetValueFactors(List<long> objectIds, long idRegister, long idFactor)
        {
            var result = new Dictionary<long, ValueItem>();

            var attributes = GbuObjectService.GetAllAttributes(objectIds,
                new List<long> {idRegister}, new List<long> {idFactor}, DateTime.Now.Date,
                attributesToDownload: new List<GbuColumnsToDownload>
                    {GbuColumnsToDownload.Value, GbuColumnsToDownload.DocumentId});

            foreach (var id in objectIds)
            {
                ValueItem res = new ValueItem
                {
                    Value = string.Empty,
                    IdDocument = null,
                };

                var objAttr = attributes.FirstOrDefault(x => x.ObjectId == id);
                if (objAttr != null)
                {
                    var valueInString = objAttr.GetValueInString();
                    if (!string.IsNullOrEmpty(valueInString))
                    {
                        res.Value = valueInString;
                        res.IdDocument = objAttr.ChangeDocId;
                    }
                }

                result.Add(id, res);
            }

            return result;
        }

        private bool ResolveGroup(Filters filter, UnitFactor attr)
        {
            static bool ResolveBoolean(BoolFilter filter, bool? value)
            {
                return filter.FilteringType switch
                {
                    FilteringTypeBool.Equal => value == filter.Value,
                    FilteringTypeBool.NotEqual => value != filter.Value,
                    FilteringTypeBool.IsNull => value == null,
                    FilteringTypeBool.IsNotNull => value != null,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            static bool ResolveDate(DateFilter filter, DateTime? value)
            {
                return filter.FilteringType switch
                {
                    FilteringTypeDate.Before => value < filter.Value,
                    FilteringTypeDate.BeforeIncludingBoundary => value <= filter.Value,
                    FilteringTypeDate.After => value > filter.Value,
                    FilteringTypeDate.AfterIncludingBoundary => value >= filter.Value,
                    FilteringTypeDate.InRange => filter.Value < value && value < filter.Value2,
                    FilteringTypeDate.InRangeIncludingBoundaries => filter.Value <= value && value <= filter.Value2,
                    FilteringTypeDate.IsNull => value == null,
                    FilteringTypeDate.IsNotNull => value != null,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            static bool ResolveString(StringFilter filter, string value)
            {
                return filter.FilteringType switch
                {
                    // Обработка пустых/не найденных значений атрибутов без выброса ошибок
                    FilteringTypeString.Contains or
                        FilteringTypeString.ContainsIgnoreCase or
                        FilteringTypeString.NotContains or
                        FilteringTypeString.NotContainsIgnoreCase or
                        FilteringTypeString.BeginsFrom or
                        FilteringTypeString.BeginsFromIgnoreCase or
                        FilteringTypeString.NotBeginsFrom or
                        FilteringTypeString.NotBeginsFromIgnoreCase or
                        FilteringTypeString.EndsWith or
                        //FilteringTypeString.EndsWithIgnoreCase or
                        FilteringTypeString.NotEndsWith
                        //FilteringTypeString.NotEndsWithIgnoreCase
                        when filter.Value == null => false,


                    FilteringTypeString.Equal => value == filter.Value,
                    FilteringTypeString.EqualIgnoreCase => string.Equals(value, filter.Value,
                        StringComparison.CurrentCultureIgnoreCase),
                    FilteringTypeString.NotEqual => value != filter.Value,
                    FilteringTypeString.NotEqualIgnoreCase => !string.Equals(value, filter.Value,
                        StringComparison.CurrentCultureIgnoreCase),
                    FilteringTypeString.BeginsFrom => value.StartsWith(filter.Value),
                    FilteringTypeString.BeginsFromIgnoreCase => value.ToLower().StartsWith(filter.Value.ToLower()),
                    FilteringTypeString.NotBeginsFrom => !value.StartsWith(filter.Value),
                    FilteringTypeString.NotBeginsFromIgnoreCase => !value.ToLower().StartsWith(filter.Value.ToLower()),
                    FilteringTypeString.EndsWith => value.EndsWith(filter.Value),
                    //FilteringTypeString.EndsWithIgnoreCase => value.ToLower().EndsWith(filter.Value.ToLower()),
                    FilteringTypeString.NotEndsWith => !value.EndsWith(filter.Value),
                    //FilteringTypeString.NotEndsWithIgnoreCase => !value.ToLower().EndsWith(filter.Value.ToLower()),
                    FilteringTypeString.Contains => value.Contains(filter.Value),
                    FilteringTypeString.ContainsIgnoreCase => value.Contains(filter.Value.ToLower()),
                    FilteringTypeString.NotContains => !value.Contains(filter.Value),
                    FilteringTypeString.NotContainsIgnoreCase => !value.Contains(filter.Value.ToLower()),
                    FilteringTypeString.IsNull => value.IsNullOrEmpty(),
                    FilteringTypeString.IsNotNull => !value.IsNullOrEmpty(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            static bool ResolveNumber(NumberFilter filter, decimal? value)
            {
                return filter.FilteringType switch
                {
                    FilteringTypeNumber.Equal => value == filter.Value,
                    FilteringTypeNumber.NotEqual => value != filter.Value,
                    FilteringTypeNumber.Less => value < filter.Value,
                    FilteringTypeNumber.LessOrEqual => value <= filter.Value,
                    FilteringTypeNumber.Greater => value > filter.Value,
                    FilteringTypeNumber.GreaterOrEqual => value >= filter.Value,
                    FilteringTypeNumber.InRange => filter.Value < value && value < filter.Value2,
                    FilteringTypeNumber.InRangeIncludingBoundaries => filter.Value <= value && value <= filter.Value2,
                    FilteringTypeNumber.IsNull => value == null,
                    FilteringTypeNumber.IsNotNull => value != null,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            bool valResolved = filter.Type switch
            {
                FilteringType.Boolean => ResolveBoolean(filter.BoolFilter, attr?.BoolValue),
                FilteringType.Date => ResolveDate(filter.DateFilter, attr?.DateTimeValue),
                FilteringType.Number => ResolveNumber(filter.NumberFilter, attr?.DecimalValue),
                FilteringType.Reference => false, // Нет поддержки референсов
                FilteringType.String => ResolveString(filter.StringFilter, attr?.StringValue),
                FilteringType.None => false,
                _ => false
            };
            return valResolved;
        }

        private List<ComplianceGuid> GetComplianceGuides(List<OMComplianceGuide> complianceGuides)
        {
            var res = new List<ComplianceGuid>();

            foreach (var complianceGuide in complianceGuides)
            {
                long.TryParse(complianceGuide.SubGroup?.Split('.')[1], out var sGroup);
                if (complianceGuide.SubGroup != null)
                    res.Add(new ComplianceGuid
                        {Group = complianceGuide.SubGroup, Code = complianceGuide.Code, SubGroup = sGroup});
            }

            return res;
        }

        private void AddErrorRow(string kn, string value, GbuReportService reportService)
        {
            lock (_locked)
            {
                var rowReport = reportService.GetCurrentRow();
                reportService.AddValue(kn, (int) ReportColumns.KnColumn, rowReport);
                reportService.AddValue(value, (int) ReportColumns.ErrorColumn, rowReport);
            }
        }

        private void AddRowToReport(string kn, string value,
            GbuReportService reportService)
        {
            lock (_locked)
            {
                var rowReport = reportService.GetCurrentRow();
                //var inputAttributeName = GbuObjectService.GetAttributeNameById(inputAttributeId);
                //var sourceAttributeName = GbuObjectService.GetAttributeNameById(sourceAttributeId);
                reportService.AddValue(kn, (int) ReportColumns.KnColumn, rowReport);
                //reportService.AddValue(inputAttributeName, (int) ReportColumns.InputFieldColumn, rowReport);
                reportService.AddValue(value, (int) ReportColumns.ValueColumn, rowReport);
                //reportService.AddValue(sourceAttributeName, (int) ReportColumns.OutputFieldColumn, rowReport);
                reportService.AddValue(string.Empty, (int) ReportColumns.ErrorColumn, rowReport);
            }
        }

        #endregion
    }


    #region Entities

    public class SetEstimatedGroupUnitPure : ItemBase
    {
        public string CadastralNumber { get; set; }
        public string PropertyType { get; set; }
    }

    public class EstimatedGroupSettings
    {
        public long GroupId { get; set; }

        public long Priority { get; set; }

        public string GroupNumber { get; set; }
        public string GroupDesc { get; set; }

        public List<GroupingSetting> GroupingSettings { get; set; }

        public List<QSCondition> GetConditions()
        {
            var listConditions = new List<QSCondition>();

            foreach (var setting in GroupingSettings)
            {
                if (setting.DictionaryId != null)
                {
                    var dictValues = OMModelingDictionariesValues.Where(x => x.DictionaryId == setting.DictionaryId
                        && x.CalculationValue != null).SelectAll()
                        .Execute();
                    var dictStringValues = setting.DictionaryValues.Split(",");
                    var dictFilterValues = dictStringValues.Select(x => x.ParseToDecimal()).Distinct().ToList();
                    var dictFilter = dictValues.Where(x => dictFilterValues.Contains(x.CalculationValue)).Select(x=>x.Value).ToList();
                    listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), QSConditionType.In, dictFilter));
                    continue;
                }

                var condition = setting.Filters.Type switch
                {
                    FilteringType.Boolean => setting.Filters.BoolFilter.FilteringType switch
                    {
                        FilteringTypeBool.Equal => QSConditionType.Equal,
                        FilteringTypeBool.NotEqual => QSConditionType.NotEqual,
                        FilteringTypeBool.IsNull => QSConditionType.IsNull,
                        FilteringTypeBool.IsNotNull => QSConditionType.IsNotNull,
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    FilteringType.Date => setting.Filters.DateFilter.FilteringType switch
                    {
                        FilteringTypeDate.Before => QSConditionType.Less,
                        FilteringTypeDate.BeforeIncludingBoundary => QSConditionType.LessOrEqual,
                        FilteringTypeDate.After => QSConditionType.Greater,
                        FilteringTypeDate.AfterIncludingBoundary => QSConditionType.GreaterOrEqual,
                        FilteringTypeDate.InRange => QSConditionType.LessSysdate,
                        FilteringTypeDate.InRangeIncludingBoundaries => QSConditionType.GreaterSysdate,
                        FilteringTypeDate.IsNull => QSConditionType.IsNull,
                        FilteringTypeDate.IsNotNull => QSConditionType.IsNotNull,
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    FilteringType.String => setting.Filters.StringFilter.FilteringType switch
                    {
                        FilteringTypeString.Equal => QSConditionType.Equal,
                        FilteringTypeString.EqualIgnoreCase => QSConditionType.EqualNonCaseSensitive,
                        FilteringTypeString.NotEqual => QSConditionType.NotEqual,
                        FilteringTypeString.NotEqualIgnoreCase => QSConditionType.NotEqualNonCaseSensitive,
                        FilteringTypeString.BeginsFrom => QSConditionType.BeginFrom,
                        FilteringTypeString.BeginsFromIgnoreCase => QSConditionType.BeginFromNonCaseSensitive,
                        FilteringTypeString.NotBeginsFrom => QSConditionType.NotBeginFrom,
                        FilteringTypeString.NotBeginsFromIgnoreCase => QSConditionType.NotBeginFromNonCaseSensitive,
                        FilteringTypeString.EndsWith => QSConditionType.EndTo,
                        //FilteringTypeString.EndsWithIgnoreCase => expr, // Не поддерживается платформой
                        FilteringTypeString.NotEndsWith => QSConditionType.NotEndTo,
                        //FilteringTypeString.NotEndsWithIgnoreCase => expr, // Не поддерживается платформой
                        FilteringTypeString.Contains => QSConditionType.Contains,
                        FilteringTypeString.ContainsIgnoreCase => QSConditionType.ContainsNonCaseSensitive,
                        FilteringTypeString.NotContains => QSConditionType.NotContains,
                        FilteringTypeString.NotContainsIgnoreCase => QSConditionType.NotContainsNonCaseSensitive,
                        FilteringTypeString.IsNull => QSConditionType.IsNull,
                        FilteringTypeString.IsNotNull => QSConditionType.IsNotNull,
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    FilteringType.Number => setting.Filters.NumberFilter.FilteringType switch
                    {
                        FilteringTypeNumber.Equal => QSConditionType.Equal,
                        FilteringTypeNumber.NotEqual => QSConditionType.NotEqual,
                        FilteringTypeNumber.Less => QSConditionType.Less,
                        FilteringTypeNumber.LessOrEqual => QSConditionType.LessOrEqual,
                        FilteringTypeNumber.Greater => QSConditionType.Greater,
                        FilteringTypeNumber.GreaterOrEqual => QSConditionType.GreaterOrEqual,
                        FilteringTypeNumber.InRange => QSConditionType.LessSysdate,
                        FilteringTypeNumber.InRangeIncludingBoundaries => QSConditionType.GreaterSysdate,
                        FilteringTypeNumber.IsNull => QSConditionType.IsNull,
                        FilteringTypeNumber.IsNotNull => QSConditionType.IsNotNull,
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (condition is QSConditionType.GreaterSysdate or QSConditionType.LessSysdate)
                {
                    bool includeBoundary = condition == QSConditionType.GreaterSysdate;
                    var cond1 = includeBoundary ? QSConditionType.GreaterOrEqual : QSConditionType.Greater;
                    var cond2 = includeBoundary ? QSConditionType.LessOrEqual : QSConditionType.Less;
                    switch (setting.Filters.Type)
                    {
                        case FilteringType.Date:
                        {
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), cond1, setting.Filters.DateFilter.Value ?? DateTime.MinValue));
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), cond2, setting.Filters.DateFilter.Value2 ?? DateTime.MaxValue));
                        }
                            break;
                        case FilteringType.Number:
                        {
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), cond1, setting.Filters.NumberFilter.Value.ParseToDouble()));
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), cond2, setting.Filters.NumberFilter.Value2.ParseToDouble()));
                        }
                            break;
                    }
                }
                else
                {
                    switch (setting.Filters.Type)
                    {
                        case FilteringType.String:
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), condition, setting.Filters.StringFilter.Value));
                            break;
                        case FilteringType.Boolean:
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), condition, setting.Filters.BoolFilter.Value.GetValueOrDefault() ? 1 : 0));
                            break;
                        case FilteringType.Number:
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), condition, setting.Filters.NumberFilter.Value.ParseToDouble()));
                            break;
                        case FilteringType.Date:
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), condition, setting.Filters.DateFilter.Value ?? DateTime.MinValue));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                }
            }

            return listConditions;
        }
    }

    public class GroupingSetting
    {
        public Filters Filters { get; set; }
        public long KoAttributeId { get; set; }

        public long? DictionaryId { get; set; }

        public string DictionaryValues { get; set; }
    }

    public class EstimatedGroupAffixingUnitsGetter : AItemsGetter<SetEstimatedGroupUnitPure>
    {
        public EstimatedGroupModel Settings { get; set; }
        private string BaseUnitsCondition { get; set; }

        public EstimatedGroupAffixingUnitsGetter(ILogger logger, EstimatedGroupModel setting) : base(logger)
        {
            Settings = setting;

            BaseUnitsCondition = $@" where unit.TASK_ID = {Settings.IdTask}";
        }


        public override List<SetEstimatedGroupUnitPure> GetItems(int packageIndex, int packageSize)
        {
            if (Settings.IdTask == 0)
                return new List<SetEstimatedGroupUnitPure>();

            var sql = $@"select ID as {nameof(SetEstimatedGroupUnitPure.Id)},
								OBJECT_ID as {nameof(SetEstimatedGroupUnitPure.ObjectId)}, 
								CADASTRAL_NUMBER as {nameof(SetEstimatedGroupUnitPure.CadastralNumber)}, 
								PROPERTY_TYPE as {nameof(SetEstimatedGroupUnitPure.PropertyType)}
										from ko_unit unit
										{BaseUnitsCondition}
										order by unit.id 
										limit {packageSize} offset {packageIndex * packageSize}";

            return QSQuery.ExecuteSql<SetEstimatedGroupUnitPure>(sql);
        }

        public override int GetItemsCount()
        {
            var columnName = "count";
            var countSql = $@"select count(*) as {columnName} from ko_unit unit {BaseUnitsCondition}";
            var command = DBMngr.Main.GetSqlStringCommand(countSql);
            var dataSet = DBMngr.Main.ExecuteDataSet(command);

            var unitCount = 0;
            var row = dataSet.Tables[0]?.Rows[0];
            if (row != null)
            {
                unitCount = row[columnName].ParseToInt();
            }

            return unitCount;
        }
    }

    #endregion
}