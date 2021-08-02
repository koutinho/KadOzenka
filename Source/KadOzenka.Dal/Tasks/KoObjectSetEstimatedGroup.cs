using System;
using System.Collections.Generic;
using System.Linq;
using CommonSdks.Excel;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.Exceptions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Models.Filters;
using KadOzenka.Dal.Units;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ModelingBusiness.Model;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.KoObject
{
    #region TypeStructure

    public enum ReportColumns : int
    {
        KnColumn = 0,
        ValueColumn = 1,
        ObjectTypeColumn = 2,
        ErrorColumn = 3,
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
        private IGbuObjectService GbuObjectService { get; }
        private IGroupService GroupService { get; }

        private IModelService ModelService { get; }

        private IUnitService UnitService { get; }

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
            ModelService = new ModelService();
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

            // Note: ToLookup предположительно более производителен вместо GroupBy().ToDictionary()
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

        public string Run(EstimatedGroupModel param)
        {
            Logger.ForContext("InputParameters", JsonConvert.SerializeObject(param))
                .Information("Входные данные для Присвоения оценочной группы");

            using var reportService = new GbuReportService("Отчет проставления оценочной группы");
            reportService.AddHeaders(new List<string>
            {
                "КН", "Тип объекта", "Внесённое значение", "Ошибка"
            });
            reportService.SetIndividualWidth((int) ReportColumns.KnColumn, 4);
            reportService.SetIndividualWidth((int) ReportColumns.ObjectTypeColumn, 4);
            reportService.SetIndividualWidth((int) ReportColumns.ValueColumn, 3);
            reportService.SetIndividualWidth((int) ReportColumns.ErrorColumn, 5);
            _locked = new object();

            var unitsGetter =
                new EstimatedGroupAffixingUnitsGetter(Logger, param) as AItemsGetter<SetEstimatedGroupUnitPure>;
            unitsGetter = new GbuObjectStatusFilterDecorator<SetEstimatedGroupUnitPure>(unitsGetter, Logger,
                param.ObjectChangeStatus, DateTime.Now.GetEndOfTheDay());

            CountAllUnits = unitsGetter.GetItemsCount();
            Logger.Debug("Всего в БД {MaxUnitsCount} ЕО", CountAllUnits);

            var tourId = OMTask.Where(x => x.Id == param.IdTask).Select(x => x.TourId).ExecuteFirstOrDefault().TourId;
            var allUnitsQuery = param.OverwriteGroups
                ? OMUnit.Where(x => x.TaskId == param.IdTask)
                : OMUnit.Where(x => x.TaskId == param.IdTask && x.GroupId == -1);


            // Трекинг юнитов для отчета
            var allUnits = allUnitsQuery
                .Select(unit => new {unit.Id, unit.CadastralNumber, unit.PropertyType, unit.PropertyType_Code})
                .Execute();
            Logger.Information("Выбрано {UnitsCount} юнитов для проставления оценочной группы");

            // Сбор данных по группам и приоритету рассчета
            var groupsInfo = GroupService.GetTourGroupsInfo(tourId.GetValueOrDefault(), ObjectTypeExtended.Both);
            Logger.Information("Собраны данные по группам и приоритету рассчета");

            CheckActiveModels(groupsInfo);

            // ОКС
            var calcSettingsOks =
                GroupCalculationSettingsService.GetCalculationSettings(tourId.GetValueOrDefault(), false);
            var OksGroups = groupsInfo.OksSubGroups.Select(x => x.Id);
            var OksSettings = OMTourGroupGroupingSettings.Where(x => OksGroups.Contains(x.GroupId)).SelectAll()
                .Execute();
            var convertedOksSettings =
                EnrichGroupSettings(ConvertToEstimateSetting(OksSettings), calcSettingsOks, groupsInfo.OksSubGroups)
                    .OrderBy(x => x.Priority).ToList();
            Logger.Information("Собраны данные по группировке для ОКС");

            // ЗУ
            var calcSettingsZu =
                GroupCalculationSettingsService.GetCalculationSettings(tourId.GetValueOrDefault(), true);
            var ZuGroups = groupsInfo.ZuSubGroups.Select(x => x.Id);
            var ZuSettings = OMTourGroupGroupingSettings.Where(x => ZuGroups.Contains(x.GroupId)).SelectAll().Execute();
            var convertedZuSettings =
                EnrichGroupSettings(ConvertToEstimateSetting(ZuSettings), calcSettingsZu, groupsInfo.ZuSubGroups)
                    .OrderBy(x => x.Priority).ToList();
            Logger.Information("Собраны данные по группировке для ЗУ");

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
                        new QSConditionSimple(OMUnit.GetColumn(x => x.TaskId), QSConditionType.Equal, param.IdTask)
                    }
                }
            };

            // Шаблон для ЗУ
            var queryTemplateZu = queryTemplate.GetCopy();
            queryTemplateZu.Condition = queryTemplateZu.Condition.And(new QSConditionSimple(
                OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.Equal, (int) PropertyTypes.Stead));
            var unitsZu = allUnits.Where(x => x.PropertyType_Code == PropertyTypes.Stead);
            var unitsZuIds = unitsZu.Select(x => x.Id).ToList();

            // Шаблон для ОКС
            var propertyTypesOksForQsQuery = new[]
            {
                (double) PropertyTypes.Building, (double) PropertyTypes.Construction,
                (double) PropertyTypes.Pllacement, (double) PropertyTypes.UncompletedBuilding,
                (double) PropertyTypes.Parking
            }.AsEnumerable();
            var propertyTypesOks = new[]
            {
                PropertyTypes.Building, PropertyTypes.Construction,
                PropertyTypes.Pllacement, PropertyTypes.UncompletedBuilding, PropertyTypes.Parking
            };
            var queryTemplateOks = queryTemplate.GetCopy();
            queryTemplateOks.Condition = queryTemplateOks.Condition.And(
                new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.In,
                    propertyTypesOksForQsQuery));
            var unitsOks = allUnits.Where(x => propertyTypesOks.Contains(x.PropertyType_Code));
            var unitsOksIds = unitsOks.Select(x => x.Id).ToList();


            // Итерации по группам вместо юнитов, однопроходные
            Logger.Information("Начало группировки по ОКС");
            var assignmentReportOks = AssignGroups(convertedOksSettings, queryTemplateOks, unitsOksIds);
            Logger.Information("Начало группировки по ЗУ");
            var assignmentReportZu = AssignGroups(convertedZuSettings, queryTemplateZu, unitsZuIds);

            var report = new List<GroupingInfo>();
            if (assignmentReportOks.Count > 0)
                report.AddRange(assignmentReportOks);
            if (assignmentReportZu.Count > 0)
                report.AddRange(assignmentReportZu);

            Logger.Information("Генерация отчёта");
            foreach (var groupingInfo in report)
            {
                var unitsForGroup = allUnits.Where(x => groupingInfo.UnitIds.Contains(x.Id));
                foreach (var unit in unitsForGroup)
                {
                    if (groupingInfo.GroupId == -1)
                        AddErrorRow(unit.CadastralNumber, $"Не найдено подходящей группы по условиям", reportService);
                    else
                    {
                        AddRowToReport(unit.CadastralNumber, unit.PropertyType, groupingInfo.GroupNumber,
                            reportService);
                    }
                }
            }

            var reportId = reportService.SaveReport();

            Logger.Information("Закончена операция присвоения оценочной группы");

            return reportService.GetUrlToDownloadFile(reportId);
        }


        private bool CheckActiveModels(TourGroupsInfo groupsInfo)
        {
            var oksWithModelCheck = groupsInfo.OksSubGroups.Where(x => x.CheckModelFactorsValues).ToList();
            var zuWithModelCheck = groupsInfo.ZuSubGroups.Where(x => x.CheckModelFactorsValues).ToList();
            List<GroupTreeDto> modelCheck = new List<GroupTreeDto>();
            if (oksWithModelCheck.Count > 0)
                modelCheck.AddRange(oksWithModelCheck);
            if (zuWithModelCheck.Count > 0)
                modelCheck.AddRange(zuWithModelCheck);
            var dictGroupModel = new List<(GroupTreeDto, OMModel)>();
            foreach (var groupTreeDto in modelCheck)
            {
                var model = ModelService.GetActiveModelEntityByGroupId(groupTreeDto.Id);
                dictGroupModel.Add((groupTreeDto, model));
            }

            var groupsWithEmptyActiveModels = dictGroupModel.Where(x => x.Item2 == null).ToList();
            if (groupsWithEmptyActiveModels.Count == 0)
                return true;
            var stringListOfGroupsWithEmptyActiveModels = groupsWithEmptyActiveModels
                .Select(x => x.Item1.GroupName)
                .Aggregate((acc, str) => acc + "<br>" + str);
            throw new EmptyActiveModelForGroupWithFactorValueCheckException(
                "Операция прервана. Найдены группы с флагом проверки наличия значений факторов модели, но с пустыми активными моделями:<br> " +
                stringListOfGroupsWithEmptyActiveModels);
        }

        private List<GroupingInfo> AssignGroups(List<EstimatedGroupSettings> convertedSettings, QSQuery queryTemplate,
            List<long> unitIds)
        {
            QSQuery FormQueryWithConditions(EstimatedGroupSettings setting)
            {
                var query = queryTemplate.GetCopy();
                var conditions = setting.GetConditions();
                if (conditions.Count > 0)
                {
                    query.Condition =
                        query.Condition.And(new QSConditionGroup
                        {
                            Type = QSConditionGroupType.And,
                            Conditions = conditions
                        });
                }

                query.ClearSqlCache();
                return query;
            }

            var result = new List<GroupingInfo>();
            if (convertedSettings.Count == 0)
            {
                Logger.Information("Не найдено ни одного условия для группировки");
                return result;
            }

            foreach (var setting in convertedSettings)
            {
                if (unitIds.Count == 0) return result;

                var query = FormQueryWithConditions(setting);

                ModelFactorCheck(setting, query);

                var sqlString = query.GetSql();
                var toAssign = query.ExecuteQuery<IdHolder>().Select(x => x.Id);
                var filterConditionMatchingUnitIds = unitIds.Intersect(toAssign).ToList();
                if (filterConditionMatchingUnitIds.Count == 0) continue;

                var commaSeparatedIdList = filterConditionMatchingUnitIds.Select(x => x.ToString())
                    .Aggregate((acc, item) => acc + "," + item);
                unitIds = unitIds.Except(filterConditionMatchingUnitIds).ToList();

                Logger.Information("Проставление юнитам группы {GroupName} ({GroupId})", setting.GroupDesc,
                    setting.GroupId);
                var sql =
                    $"update ko_unit set change_date = now(), group_id = {setting.GroupId} where id in ({commaSeparatedIdList})";
                var updateGroupsCommand = DBMngr.Main.GetSqlStringCommand(sql);
                DBMngr.Main.ExecuteNonQuery(updateGroupsCommand);

                result.Add(new GroupingInfo
                {
                    GroupId = setting.GroupId, GroupNumber = setting.GroupNumber,
                    UnitIds = filterConditionMatchingUnitIds
                });
            }

            if (unitIds.Count > 0)
            {
                var commaSeparatedIdList = unitIds.Select(x => x.ToString()).Aggregate((acc, item) => acc + "," + item);
                var sql = $"update ko_unit set change_date = now(), group_id = -1 where id in ({commaSeparatedIdList})";
                var updateGroupsCommand = DBMngr.Main.GetSqlStringCommand(sql);
                DBMngr.Main.ExecuteNonQuery(updateGroupsCommand);

                result.Add(new GroupingInfo {GroupId = -1, UnitIds = unitIds});
            }

            return result;
        }

        private void ModelFactorCheck(EstimatedGroupSettings setting, QSQuery query)
        {
            // Model factor check
            var group = GroupService.GetGroupsByIds(new List<long> {setting.GroupId}).FirstOrDefault();
            var checkFactorValues = group?.CheckModelFactorsValues;
            if (!(checkFactorValues ?? false)) return;

            Logger.Information("Проверка наличия значений факторов у группы {GroupName} ({GroupId})", setting.GroupDesc,
                setting.GroupId);
            var model = ModelService.GetActiveModelEntityByGroupId(setting.GroupId);
            if (model == null) return;

            var modelFactorsObjectModel =
                OMModelFactor.Where(x => x.ModelId == model.Id).SelectAll().Execute();
            var modelFactorsIds = modelFactorsObjectModel.Select(x => x.FactorId)
                .ToList();
            var conditions = modelFactorsIds?.Select(x =>
                    (QSCondition) new QSConditionSimple(new QSColumnSimple(x), QSConditionType.IsNotNull))
                .ToList();
            if (conditions.Count <= 0) return;
            query.Condition = query.Condition.And(new QSConditionGroup
            {
                Type = QSConditionGroupType.And,
                Conditions = conditions
            });
            query.ClearSqlCache();
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

        private void AddErrorRow(string kn, string value, GbuReportService reportService)
        {
            lock (_locked)
            {
                var rowReport = reportService.GetCurrentRow();
                reportService.AddValue(kn, (int) ReportColumns.KnColumn, rowReport);
                reportService.AddValue(value, (int) ReportColumns.ErrorColumn, rowReport);
            }
        }

        private void AddRowToReport(string kn, string value, string type,
            GbuReportService reportService)
        {
            lock (_locked)
            {
                var rowReport = reportService.GetCurrentRow();
                reportService.AddValue(kn, (int) ReportColumns.KnColumn, rowReport);
                reportService.AddValue(type, (int) ReportColumns.ObjectTypeColumn, rowReport);
                reportService.AddValue(value, (int) ReportColumns.ValueColumn, rowReport);
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
                    var dictValues = OMGroupingDictionariesValues.Where(x => x.DictionaryId == setting.DictionaryId
                                                                             && x.GroupingValue != null).SelectAll()
                        .Execute();
                    var dictStringValues = setting.DictionaryValues.Split("\n");
                    var dictFilterValues = dictStringValues.Distinct().Where(x => x.IsNotEmpty()).ToList();
                    var dictFilter = dictValues.Where(x => dictFilterValues.Contains(x.GroupingValue))
                        .Select(x => x.Value).ToList();
                    listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId),
                        QSConditionType.In, dictFilter));
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
                        FilteringTypeString.In => QSConditionType.In,
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

                if (setting.Filters.Type == FilteringType.String
                    && setting.Filters.StringFilter.FilteringType == FilteringTypeString.Equal
                    && setting.Filters.StringFilter.Value.Split(",").Distinct().Count(x => x.IsNotEmpty()) > 1)
                {
                    condition = QSConditionType.In;
                }

                if (condition is QSConditionType.GreaterSysdate or QSConditionType.LessSysdate)
                {
                    bool includeBoundary = condition == QSConditionType.GreaterSysdate;
                    var cond1 = includeBoundary ? QSConditionType.GreaterOrEqual : QSConditionType.Greater;
                    var cond2 = includeBoundary ? QSConditionType.LessOrEqual : QSConditionType.Less;
                    switch (setting.Filters.Type)
                    {
                        case FilteringType.Date:
                        {
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), cond1,
                                setting.Filters.DateFilter.Value ?? DateTime.MinValue));
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), cond2,
                                setting.Filters.DateFilter.Value2 ?? DateTime.MaxValue));
                        }
                            break;
                        case FilteringType.Number:
                        {
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), cond1,
                                setting.Filters.NumberFilter.Value.ParseToDouble()));
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId), cond2,
                                setting.Filters.NumberFilter.Value2.ParseToDouble()));
                        }
                            break;
                    }
                }
                else
                {
                    switch (setting.Filters.Type)
                    {
                        case FilteringType.String:
                        {
                            if (condition == QSConditionType.In)
                            {
                                listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId),
                                    condition,
                                    setting.Filters.StringFilter.ValueMulti.Replace("\r", "").Split('\n').Distinct()
                                        .Where(x => x.IsNotEmpty())));
                            }
                            else
                            {
                                listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId),
                                    condition, setting.Filters.StringFilter.Value));
                            }
                        }

                            break;
                        case FilteringType.Boolean:
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId),
                                condition, setting.Filters.BoolFilter.Value.GetValueOrDefault() ? 1 : 0));
                            break;
                        case FilteringType.Number:
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId),
                                condition, setting.Filters.NumberFilter.Value.ParseToDouble()));
                            break;
                        case FilteringType.Date:
                            listConditions.Add(new QSConditionSimple(new QSColumnSimple(setting.KoAttributeId),
                                condition, setting.Filters.DateFilter.Value ?? DateTime.MinValue));
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