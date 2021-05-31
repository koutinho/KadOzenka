using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.GbuObject.Entities;
using KadOzenka.Dal.GbuObject.Exceptions;
using KadOzenka.Dal.Models.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.Gbu.GroupingAlgoritm;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.GbuObject
{
    public class PriorityItemFinal
    {
        private readonly ILogger _log;

        public PriorityItemFinal(ILogger log)
        {
            _log = log;
        }

        #region Методы

        private ValueItem GetValueFactor(List<GbuObjectAttribute> objectAttributes, long? idFactor)
        {
            var res = new ValueItem
            {
                Value = string.Empty,
                IdDocument = null,
                AttributeName = string.Empty
            };

            var attribute = objectAttributes.FirstOrDefault(x => x.AttributeId == idFactor);
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.GetValueInString()))
                return res;

            res.Value = attribute.GetValueInString();
            res.AttributeName = attribute.AttributeData.Name;
            res.IdDocument = attribute.ChangeDocId;

            return res;
        }

        private void AddValueFactor(long objectId, long? idFactor, long? idDoc, DateTime date, string value)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idFactor.Value,
                ObjectId = objectId,
                ChangeDocId = (idDoc == null) ? -1 : idDoc.Value,
                S = date,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = date,
                StringValue = value,
            };

            attributeValue.Save();
        }


        string CleanUp(string x)
        {
            if (x == null) return null;
            // Оставляем только значимые символы в строках и убираем мусор из значения
            return Regex
                .Replace(x, "\\s+", "")
                .Replace("_x000D_", "")
                .Replace("-", "")
                .ToLower();
        }

        private void LogNotFoundDictionaryValues(ValueItem valueLevel, DataLevel dataLevel, List<OMCodDictionary> list,
            GroupingItem item)
        {
            var codId = list?[0].IdCodjob;
            _log.ForContext("ValueLevel", valueLevel, true)
                .ForContext("DataLevel", dataLevel, true)
                .ForContext("DictionaryId", codId)
                .Verbose(
                    "[Нормализация] {CadastralNumber}: {AttributeName}. Значение: {Value} отсутствует в классификаторе",
                    item.CadastralNumber, valueLevel.AttributeName, valueLevel.Value);
        }

        private static string ResolveCode(string[] orderedCodes, Filters[] orderedFilters, GbuObjectAttribute attr)
        {
            static bool? ConvertDbNumericToBoolNullable(decimal? val)
            {
                return val switch
                {
                    0 => false,
                    1 => true,
                    null => null,
                    _ => null
                };
            }

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
                        FilteringTypeString.EndsWithIgnoreCase or
                        FilteringTypeString.NotEndsWith or
                        FilteringTypeString.NotEndsWithIgnoreCase
                        when filter.Value == null => false,


                    FilteringTypeString.Equal => value == filter.Value,
                    FilteringTypeString.EqualIgnoreCase => string.Equals(value, filter.Value, StringComparison.CurrentCultureIgnoreCase),
                    FilteringTypeString.NotEqual => value != filter.Value,
                    FilteringTypeString.NotEqualIgnoreCase => !string.Equals(value, filter.Value, StringComparison.CurrentCultureIgnoreCase),
                    FilteringTypeString.BeginsFrom => value.StartsWith(filter.Value),
                    FilteringTypeString.BeginsFromIgnoreCase => value.ToLower().StartsWith(filter.Value.ToLower()),
                    FilteringTypeString.NotBeginsFrom => !value.StartsWith(filter.Value),
                    FilteringTypeString.NotBeginsFromIgnoreCase => !value.ToLower().StartsWith(filter.Value.ToLower()),
                    FilteringTypeString.EndsWith => value.EndsWith(filter.Value),
                    FilteringTypeString.EndsWithIgnoreCase => value.ToLower().EndsWith(filter.Value.ToLower()),
                    FilteringTypeString.NotEndsWith => !value.EndsWith(filter.Value),
                    FilteringTypeString.NotEndsWithIgnoreCase => !value.ToLower().EndsWith(filter.Value.ToLower()),
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

            for (var i=0;i<orderedCodes.Length;i++)
            {
                bool valResolved = orderedFilters[i].Type switch
                {
                    FilteringType.Boolean => ResolveBoolean(orderedFilters[i].BoolFilter, ConvertDbNumericToBoolNullable(attr?.NumValue)),
                    FilteringType.Date => ResolveDate(orderedFilters[i].DateFilter, attr?.DtValue),
                    FilteringType.Number => ResolveNumber(orderedFilters[i].NumberFilter, attr?.NumValue),
                    FilteringType.Reference => false, // Нет поддержки референсов
                    FilteringType.String => ResolveString(orderedFilters[i].StringFilter, attr?.StringValue),
                    FilteringType.None => false,
                    _ => false
                };
                if (!valResolved) continue;
                Log.ForContext<PriorityItemFinal>()
                    .ForContext("FilterType", orderedFilters[i].Type)
                    .Verbose("Найден код для финализации нормализации: {resolvedValue}, Массив кодов: {valueArray}, Значения атрибута: {numValue}, {dtValue}, {stringValue}",
                        orderedCodes[i], orderedCodes, attr?.NumValue, attr?.DtValue, attr?.StringValue);

                return orderedCodes[i];
            }

            Log.ForContext<PriorityGroupingFinal>().Verbose("Совпадения не найдено, записываем прочерк");
            return "-";
        }

        public void SetPriorityGroup(GroupingSettingsFinal setting,
            List<long> allAttributeIds, GroupingItem inputItem, DateTime dateActual,
            List<GbuObjectAttribute> objectAttributes, GbuReportService reportService)
        {
            GbuReportService.Row currentRow;
            lock (PriorityGroupingFinal.Locked)
            {
                currentRow = reportService.GetCurrentRow();
                PriorityGroupingFinal.CurrentCount++;
                reportService.AddValue(inputItem.CadastralNumber, PriorityGroupingFinal.KnColumn, currentRow);
            }

            bool errorCOD = false;
            string errorCODStr = string.Empty;

            try
            {
                string resGroup = "";
                var valueSource = GetValueFactor(objectAttributes, setting.IdAttributeSource);
                var values = valueSource.Value.Split("/");

                if (values.Length == 0 || valueSource.Value.IsNullOrEmpty())
                {
                    errorCOD = true;
                    errorCODStr = "Не найдено кодов для проставления";
                }

                switch (values.Length)
                {
                    case 1:
                        resGroup = values.First();
                        break;
                    case 2:
                    {
                        var attr2 = objectAttributes.FirstOrDefault(x => x.AttributeId == setting.IdAttributeFor2Selections);
                        resGroup = ResolveCode(values, new []{setting.Filter1ForSelectionBetween2,
                            setting.Filter2ForSelectionBetween2},attr2);
                        break;
                    }
                    case 3:
                    {
                        var attr3 = objectAttributes.FirstOrDefault(x => x.AttributeId == setting.IdAttributeFor3Selections);
                        resGroup = ResolveCode(values, new []{setting.Filter1ForSelectionBetween3,
                            setting.Filter2ForSelectionBetween3, setting.Filter3ForSelectionBetween3},attr3);
                        break;
                    }
                }

                #region Результат

                if (!errorCOD)
                {
                    if (setting.IdAttributeResult != null)
                    {
                        try
                        {
                            //todo iddoc
                            AddValueFactor(inputItem.ObjectId, setting.IdAttributeResult, 0, dateActual,
                                resGroup);
                        }
                        catch (Exception e)
                        {
                            throw new GroupingAttributeSavingException(
                                $"Ошибка при сохрании значения '{resGroup}' в Характеристику", e);
                        }

                        lock (PriorityGroupingFinal.Locked)
                        {
                            reportService.AddValue(
                                GbuObjectService.GetAttributeNameById(setting.IdAttributeResult
                                    .GetValueOrDefault()),
                                PriorityGroupingFinal.ResultColumn, currentRow);
                             reportService.AddValue(resGroup, PriorityGroupingFinal.ValueColumn, currentRow);
                            //AddValueItemsToReport(reportService, valueItems, currentRow, PriorityGroupingFinal.ValueColumnOffset);
                        }

                        lock (PriorityGroupingFinal.Locked)
                        {
                            //reportService.AddValue(errorCODStr, PriorityGroupingFinal.ErrorColumn, currentRow);
                        }
                    }
                }
                else
                {
                    lock (PriorityGroupingFinal.Locked)
                    {
                        //reportService.AddValue(errorCODStr, PriorityGroupingFinal.ErrorColumn, currentRow);
                    }
                }

                #endregion
            }
            catch (GroupingAttributeSavingException exception)
            {
                var message = exception.Message;
                LogException(message, inputItem, reportService, exception.InnerException, currentRow);
            }
            catch (Exception ex)
            {
                var message = "В ходе обработки оъекта возникла ошибка";
                LogException(message, inputItem, reportService, ex, currentRow);
            }

            if (PriorityGroupingFinal.CurrentCount % 1000 == 0)
            {
                _log.Debug("Обработан объект {CurrentCount} из {MaxCount}", PriorityGroupingFinal.CurrentCount,
                    PriorityGroupingFinal.MaxCount);
            }
        }

        private void AddValueItemsToReport(IGbuReportService reportService, List<ValueItem> list, GbuReportService.Row currRow, int columnOffset)
        {
            foreach (var item in list)
            {
                if (!(item.Value == "" && item.AttributeName == ""))
                    reportService.AddValue(item.Value, columnOffset, currRow);
                columnOffset++;
            }
        }

        private void LogException(string message, GroupingItem inputItem, GbuReportService reportService, Exception ex,
            GbuReportService.Row currentRow)
        {
            lock (PriorityGroupingFinal.Locked)
            {
                //var errorId = ErrorManager.LogError(ex);
                var fullMessage = $"{message}. {ex.Message}.";
                reportService.AddValue(fullMessage, PriorityGroupingFinal.GeneralErrorColumn, currentRow);
            }

            _log.ForContext("Item", JsonConvert.SerializeObject(inputItem))
                .ForContext("HumanMessage", message)
                .Error(ex, "Ошибка группировки по КН {CadastralNumber}", inputItem.CadastralNumber);
        }

        #endregion
    }

    /// <summary>
    /// Приоритет группировки
    /// </summary>
    public class PriorityGroupingFinal
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<PriorityGroupingFinal>();

        #region Номера колонок отчета

        public static int KnColumn = 0;

        public static int ResultColumn = 1;

        public static int ValueColumn = 2;

        public static int SourceColumn = 3;

        public static int ErrorColumn = 3;

        public static int GeneralErrorColumn = 4;

        public static int ValueColumnOffset = 5;

        #endregion

        /// <summary>
        /// Объект для блокировки счетчика в многопоточке
        /// </summary>
        public static object Locked;

        /// <summary>
        /// Общее число объектов
        /// </summary>
        public static int MaxCount;

        /// <summary>
        /// Индекс текущего объекта
        /// </summary>
        public static int CurrentCount;

        /// <summary>
        /// Количество объектов прошедших процедуру
        /// </summary>
        public static int SuccessCount = 0;

        /// <summary>
        /// Справочник приоритетов
        /// </summary>
        public static PriorityGroupList PrioritetList;

        public static List<string> ErrorMessages;

        // TODO: заменить на SRDSession.SetThreadCurrentPrincipal после обновления платформы
        public static void SetThreadCurrentPrincipal(long userId)
        {
            SRDUserBase userData = SRDCache.Users[(int) userId];
            GenericIdentity genericIdentity = new GenericIdentity(userData.Username);
            Thread.CurrentPrincipal = new GenericPrincipal(genericIdentity, new string[] { });
        }

        /// <summary>
        /// Выполнение операции группировки
        /// </summary>
        public string SetPriorityGroup(GroupingSettingsFinal setting, CancellationToken processCancellationToken)
        {
            Log.ForContext("InputParameters", setting).Debug("Старт нормализации. Входные параметры.");
            ValidateInputParameters(setting);


            using var reportService = new GbuReportService("Отчет нормализации");
            var dataHeaderAndColumnNumber = GenerateReportHeaderWithColumnNumber(setting);

            Log.Debug("Заголовки отчета и номера столбцов ${DictionaryColumns} ${Headers}",
                dataHeaderAndColumnNumber.DictionaryColumns, dataHeaderAndColumnNumber.Headers);

            reportService.AddHeaders(dataHeaderAndColumnNumber.Headers);
            try
            {
                Log.Verbose("Применение стилей SetStyle");
                reportService.SetIndividualWidth(KnColumn, 4);
                reportService.SetIndividualWidth(ResultColumn, 6);
                reportService.SetIndividualWidth(ValueColumn, 3);
                //reportService.SetIndividualWidth(SourceColumn, 6);
                reportService.SetIndividualWidth(ErrorColumn, 5);
                reportService.SetIndividualWidth(GeneralErrorColumn, 5);

                foreach (var dictionaryColumn in dataHeaderAndColumnNumber.DictionaryColumns)
                {
                    reportService.SetIndividualWidth((int) dictionaryColumn.Value, 3);
                    //reportService.SetIndividualWidth((int) dictionaryColumn.Value + 1, 3);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Форматирование отчета завершилось с ошибкой");
                throw;
            }

            long reportId;

            ErrorMessages = new List<string>();
            Locked = new object();
            PrioritetList = new PriorityGroupList();

            var itemsGetter = new PriorityGroupingItemsFinalGetter(Log, setting) as AItemsGetter<GroupingItem>;
            var actualDate = setting.DateActual?.Date ?? DateTime.Now.Date;
            itemsGetter =
                new GbuObjectStatusFilterDecorator<GroupingItem>(itemsGetter, Log, setting.ObjectChangeStatus,
                    actualDate);

            bool useTask = false;
            if (setting.TaskFilter != null) useTask = setting.TaskFilter.Count > 0;

            ////TODO для тестирования
            //var objectIdsForTesting = new List<long> { 11614530, 13445766, 13664618, 11657698 };
            //var items = itemsGetter.GetItems().Where(x => objectIdsForTesting.Contains(x.ObjectId)).ToList();
            var items = itemsGetter.GetItems();
            MaxCount = items.Count;
            CurrentCount = 0;
            Log.ForContext("useTask", useTask)
                .ForContext("Objs_0", JsonConvert.SerializeObject(items.ElementAtOrDefault(0)))
                .Debug(useTask
                    ? "Нормализация по Заданиям на оценку. Всего {Count} единиц оценки"
                    : "Нормализация по Объектам Недвижимости. Всего {Count} объектов", MaxCount);

            var allAttributeIds = GetAttributes(setting);
            var queryManager = new QueryManager();
            queryManager.SetBaseToken(processCancellationToken);
            var gbuObjectService = new GbuObjectService(queryManager);
            //TODO если будут еще различия - вынести в интрефейс/класс
            if (useTask)
            {
                ProcessUnits(setting, items, allAttributeIds, gbuObjectService, reportService,
                    dataHeaderAndColumnNumber, processCancellationToken);
            }
            else
            {
                ProcessObjects(setting, items, allAttributeIds, actualDate, gbuObjectService,
                    reportService,
                    dataHeaderAndColumnNumber, processCancellationToken);
            }

            Log.Debug("Закончена обработка всех элементов");
            //попытка принудительно освободить память
            items = null;
            GC.Collect();

            Log.Debug("Старт генерации отчета");
            reportId = reportService.SaveReport();

            Log.Debug("Финиш нормализации");

            return reportService.GetUrlToDownloadFile(reportId);
        }

        private static void ValidateInputParameters(GroupingSettingsFinal setting)
        {
            var message = new StringBuilder();

            CheckAttributeIsStringType(setting.IdAttributeResult, "Характеристика", ref message);

            if (message.Length <= 0)
                return;

            //убираем последную запятую
            message.Length--;
            throw new Exception($"Атрибут(ы): '{message}' должны быть типа строка.");
        }

        private static void CheckAttributeIsStringType(long? attributeId, string name, ref StringBuilder message)
        {
            var attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == attributeId);
            if (attribute != null && attribute.Type != RegisterAttributeType.STRING)
            {
                message.Append($"{name} ({attribute.Name}),");
            }
        }

        private static void ProcessUnits(GroupingSettingsFinal setting, List<GroupingItem> units, List<long> allAttributeIds,
            GbuObjectService gbuObjectService, GbuReportService reportService,
            ReportHeaderWithColumnDic dataHeaderAndColumnNumber, CancellationToken processCancellationToken)
        {
            var config = GetProcessConfigFromSettings();
            var localCancelTokenSource = new CancellationTokenSource();
            var options = new ParallelOptions
            {
                CancellationToken = localCancelTokenSource.Token,
                MaxDegreeOfParallelism = config.ThreadsCountForUnits
            };

            var documents = new ConcurrentDictionary<long, OMInstance>();
            var unitsGroupedByCreationDate = units.GroupBy(x => x.CreationDate ?? DateTime.Now.Date)
                .ToDictionary(k => k.Key, v => v.ToList());
            Parallel.ForEach(unitsGroupedByCreationDate, options, groupedUnits =>
            {
                CheckCancellationToken(processCancellationToken, localCancelTokenSource, options);

                var localActualDate = groupedUnits.Key;
                Log.Debug(
                    "Начата работа с группой, у которой дата актуальности = '{ActualDate}'. Всего групп: {GroupsCount}",
                    localActualDate, unitsGroupedByCreationDate.Count);

                //TODO если в группе будет много юнитов, сделать пагинацию внутри группы
                //if (groupedUnits.Value.Count > config.PackageSize)
                //{
                //	_log.Debug("Количество ЕО в группе ({CurrentUnitsCount}) больше размера пакета ({PackageSize}), дополнительно отправляем их в пакеты",
                //		groupedUnits.Value.Count, config.PackageSize);
                //}

                var objectIds = groupedUnits.Value.Select(x => x.ObjectId).ToList();
                Log.Debug("Отобрано {ObjectsCount} объектов группы, у которой дата актуальности = '{ActualDate}'",
                    objectIds.Count, localActualDate.ToShortDateString());

                var objectAttributes = gbuObjectService.GetAllAttributes(objectIds, null, allAttributeIds,
                        localActualDate,
                        attributesToDownload: new List<GbuColumnsToDownload>
                            {GbuColumnsToDownload.Value, GbuColumnsToDownload.DocumentId})
                    .GroupBy(x => x.ObjectId).ToList();
                Log.Debug(
                    "Найдено {AttributesCount} атрибутов для объектов группы, у которой дата актуальности = '{ActualDate}'",
                    objectAttributes.Count, localActualDate.ToShortDateString());

                ProcessItems(new ProcessItemInputParametersFinal(setting, groupedUnits.Value,
                    objectAttributes, allAttributeIds, true, localActualDate, reportService,
                    processCancellationToken, config));
            });
        }

        private static void ProcessObjects(GroupingSettingsFinal setting,
            List<GroupingItem> objects, List<long> allAttributeIds, DateTime actualDate,
            GbuObjectService gbuObjectService, GbuReportService reportService,
            ReportHeaderWithColumnDic dataHeaderAndColumnNumber, CancellationToken processCancellationToken)
        {
            var config = GetProcessConfigFromSettings();
            var localCancelTokenSource = new CancellationTokenSource();
            var options = new ParallelOptions
            {
                CancellationToken = localCancelTokenSource.Token,
                MaxDegreeOfParallelism = config.ThreadsCountForObjects
            };

            var packageSize = config.PackageSize;
            var numberOfPackages = MaxCount / packageSize + 1;
            var documents = new ConcurrentDictionary<long, OMInstance>();
            Parallel.For(0, numberOfPackages, options, (i, _) =>
            {
                CheckCancellationToken(processCancellationToken, localCancelTokenSource, options);

                Log.Debug("Начата работа с пакетом №{PackageNumber} из {MaxPackagesCount}", i, numberOfPackages);

                var objectsPage = objects.Skip(i * packageSize).Take(packageSize).ToList();
                Log.Debug("Отобрано {ObjectsCount} объектов для пакета №{PackageNumber} из {MaxPackagesCount}",
                    objectsPage.Count, i, numberOfPackages);

                var objectAttributes = gbuObjectService.GetAllAttributes(objectsPage.Select(x => x.ObjectId).ToList(),
                        null, allAttributeIds, actualDate,
                        attributesToDownload: new List<GbuColumnsToDownload>
                            {GbuColumnsToDownload.Value, GbuColumnsToDownload.DocumentId})
                    .GroupBy(x => x.ObjectId).ToList();
                Log.Debug("Найдено {AttributesCount} атрибутов для пакета №{PackageNumber} из {MaxPackagesCount}",
                    objectAttributes.Count, i, numberOfPackages);

                ProcessItems(new ProcessItemInputParametersFinal(setting, objectsPage,
                    objectAttributes, allAttributeIds, false, actualDate, reportService,
                    processCancellationToken, config));
            });
        }

        private static void ProcessItems(ProcessItemInputParametersFinal inputParameters)
        {
            var localCancelTokenSource = new CancellationTokenSource();
            var options = new ParallelOptions
            {
                CancellationToken = localCancelTokenSource.Token,
                MaxDegreeOfParallelism = inputParameters.ProcessConfig.ThreadsCountForItemsHandling
            };

            var objectAttributesDictionary = inputParameters.ObjectAttributes.ToDictionary(k => k.Key, v => v.ToList());
            var defaultAttributes = inputParameters.AllAttributeIds
                .Select(x => new GbuObjectAttribute {AttributeId = x}).ToList();
            var userId = SRDSession.GetCurrentUserId().GetValueOrDefault();
            //Parallel.ForEach(inputParameters.Items, options, item =>
            foreach (var item in inputParameters.Items)
            {
                CheckCancellationToken(inputParameters.ProcessCancellationToken, localCancelTokenSource, options);

                //если работаем с единицами оценки, дата актуальности должна браться из них
                var localActualDate = inputParameters.UseTask
                    ? item.CreationDate?.Date ?? DateTime.Now.Date
                    : inputParameters.ActualDate;

                SetThreadCurrentPrincipal(userId);

                var currentObjectAttributes = objectAttributesDictionary.TryGetValue(item.ObjectId, out var attributes)
                    ? attributes
                    : defaultAttributes;

                new PriorityItemFinal(Log).SetPriorityGroup(inputParameters.Setting,
                    inputParameters.AllAttributeIds, item, localActualDate,
                    currentObjectAttributes, inputParameters.ReportService);
            }
            //);
        }

        private static ProcessConfig GetProcessConfigFromSettings()
        {
            var fileName = "appsettings.json";
            Log.Debug("Поиск настроек конфигурации из файла {FileName}", fileName);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: fileName, optional: false, reloadOnChange: true)
                .Build();

            var config = new ProcessConfig();
            var sectionName = "MainOperations:Grouping";
            configuration.GetSection(sectionName).Bind(config);
            Log.ForContext("Configs", config, true)
                .Debug("Полученные настройки конфигурации для секции {SectionName}", sectionName);

            var packageSize = config.PackageSize == 0 ? 100000 : config.PackageSize;
            var threadsCountForObjects = config.ThreadsCountForObjects == 0 ? 20 : config.ThreadsCountForObjects;
            var threadsCountForUnits = config.ThreadsCountForUnits == 0 ? 20 : config.ThreadsCountForUnits;
            var threadsCountForItemsHandling =
                config.ThreadsCountForItemsHandling == 0 ? 20 : config.ThreadsCountForItemsHandling;

            config.PackageSize = packageSize;
            config.ThreadsCountForObjects = threadsCountForObjects;
            config.ThreadsCountForUnits = threadsCountForUnits;
            config.ThreadsCountForItemsHandling = threadsCountForItemsHandling;

            Log.ForContext("ResultConfigs", config, true)
                .Debug("Итоговые настройки конфигурации для секции {SectionName}", sectionName);

            return config;
        }

        private static void CheckCancellationToken(CancellationToken processCancellationToken,
            CancellationTokenSource localCancellationToken, ParallelOptions options)
        {
            if (!processCancellationToken.IsCancellationRequested)
                return;

            localCancellationToken.Cancel();
            options.CancellationToken.ThrowIfCancellationRequested();
        }

        private static List<long> GetAttributes(GroupingSettingsFinal setting)
        {
            return new()
            {
                setting.IdAttributeSource.GetValueOrDefault(),
                setting.IdAttributeFor2Selections.GetValueOrDefault(),
                setting.IdAttributeFor3Selections.GetValueOrDefault()
            };
        }

        public static ReportHeaderWithColumnDic GenerateReportHeaderWithColumnNumber(GroupingSettingsFinal setting)
        {
            List<string> resHeaderList = new List<string>
            {
                "КН", "Поле в которое вносилось значение", "Внесенное значение",
                //"Источник внесенного значения",
                "Ошибка внесенного значения", "Ошибка"
            };
            ReportHeaderWithColumnDic res = new ReportHeaderWithColumnDic();

            var dicColumns = new Dictionary<long, long>();
            int lastColumn = GeneralErrorColumn;
            foreach (FieldInfo propertyInfo in typeof(GroupingSettingsFinal).GetFields(BindingFlags.Instance |
                                                                                  BindingFlags.NonPublic |
                                                                                  BindingFlags.Public))
            {
                if (propertyInfo.Name.IndexOf("Level", StringComparison.Ordinal) != -1)
                {
                    if (propertyInfo.GetValue(setting) is LevelItem &&
                        ((LevelItem) propertyInfo.GetValue(setting)).IdFactor != null)
                    {
                        lastColumn++;
                        var lItem = (LevelItem) propertyInfo.GetValue(setting);
                        resHeaderList.AddRange(new List<string>
                        {
                            GbuObjectService.GetAttributeNameById(lItem.IdFactor.GetValueOrDefault()),
                            //$"(Уровень - {levelTitle}) Источник информации"
                        });

                        Serilog.Log.Verbose("Атрибут ОН. {FactorName}, Id {FactorID}",
                            lItem.IdFactor.GetValueOrDefault(),
                            lItem.IdFactor);

                        dicColumns.Add(lItem.IdFactor.GetValueOrDefault(), lastColumn);
                        lastColumn++;
                    }
                }
            }

            res.Headers = resHeaderList;
            res.DictionaryColumns = dicColumns;
            return res;
        }

        public static void AddInfoToReport(List<DataLevel> dataLevels, GbuReportService.Row rowNumber,
            Dictionary<long, long> dictionaryColumns, GbuReportService reportService)
        {
            lock (Locked)
            {
                foreach (var dataLevel in dataLevels)
                {
                    if (!dataLevel.Code.IsNullOrEmpty())
                    {
                        string registerName = GbuObjectService.GetRegisterNameByAttributeId(dataLevel.FactorId);
                        long column = dictionaryColumns.FirstOrDefault(x => x.Key == dataLevel.FactorId).Value;
                        reportService.AddValue(dataLevel.Code, (int) column, rowNumber);
                        reportService.AddValue(registerName, (int) column + 1, rowNumber);
                    }
                }
            }
        }
    }

    public class PriorityGroupingItemsFinalGetter : AItemsGetter<GroupingItem>
    {
        private GroupingSettingsFinal Settings { get; }

        public PriorityGroupingItemsFinalGetter(ILogger logger, GroupingSettingsFinal setting) : base(logger)
        {
            Settings = setting;
        }


        public override List<GroupingItem> GetItems()
        {
            return Settings.TaskFilter?.Count > 0
                ? GetUnits()
                : GetObjects();
        }

        private List<GroupingItem> GetUnits()
        {
            return OMUnit.Where(x =>
                    x.PropertyType_Code == PropertyTypes.Stead && Settings.TaskFilter.Contains((long) x.TaskId) &&
                    x.ObjectId != null)
                .Select(x => new
                {
                    x.ObjectId,
                    x.CadastralNumber,
                    x.CreationDate
                })
                .Execute()
                .Select(x => new GroupingItem
                {
                    Id = x.Id,
                    ObjectId = x.ObjectId.GetValueOrDefault(),
                    CadastralNumber = x.CadastralNumber,
                    CreationDate = x.CreationDate
                }).ToList();
        }

        private List<GroupingItem> GetObjects()
        {
            return OMMainObject.Where(x => x.ObjectType_Code == PropertyTypes.Stead)
                .Select(x => x.CadastralNumber)
                .Execute()
                .Select(x => new GroupingItem
                {
                    Id = x.Id,
                    ObjectId = x.Id,
                    CadastralNumber = x.CadastralNumber
                }).ToList();
        }
    }

    #region Entities

    internal class ProcessItemInputParametersFinal
    {
        public ProcessItemInputParametersFinal(GroupingSettingsFinal setting, List<GroupingItem> items,
            List<IGrouping<long, GbuObjectAttribute>> objectAttributes, List<long> allAttributeIds, bool useTask,
            DateTime actualDate, GbuReportService reportService,
            CancellationToken processCancellationToken, ProcessConfig processConfig)
        {
            Setting = setting;
            Items = items;
            ObjectAttributes = objectAttributes;
            AllAttributeIds = allAttributeIds;
            UseTask = useTask;
            ActualDate = actualDate;
            ReportService = reportService;
            ProcessCancellationToken = processCancellationToken;
            ProcessConfig = processConfig;
        }

        public GroupingSettingsFinal Setting { get; }
        public List<GroupingItem> Items { get; }
        public List<IGrouping<long, GbuObjectAttribute>> ObjectAttributes { get; }
        public List<long> AllAttributeIds { get; }
        public bool UseTask { get; }
        public DateTime ActualDate { get; }
        public GbuReportService ReportService { get; }
        public CancellationToken ProcessCancellationToken { get; }
        public ProcessConfig ProcessConfig { get; }
    }

    #endregion
}