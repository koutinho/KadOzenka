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
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.GbuObject.Entities;
using KadOzenka.Dal.GbuObject.Exceptions;
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
    public struct ReportHeaderWithColumnDic
    {
        public List<string> Headers;

        public Dictionary<long, long> DictionaryColumns;
    }

    public struct DataLevel
    {
        public string Code;
        public long FactorId;
    }

    public class PriorityItem
    {
        private readonly ILogger _log;

        public PriorityItem(ILogger log)
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
            var otDate = GbuObjectService.GetNextOtFromAttributeIdPartition(objectId, idFactor.GetValueOrDefault(), date);
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idFactor.Value,
                ObjectId = objectId,
                ChangeDocId = (idDoc == null) ? -1 : idDoc.Value,
                S = date,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = otDate,
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


        public void SetPriorityGroup(GroupingSettings setting, List<DictRecord> dict,
            List<long> allAttributeIds, GroupingItem inputItem, DateTime dateActual,
            List<GbuObjectAttribute> objectAttributes, GbuReportService reportService)
        {
            string ExtractValue(ValueItem item)
            {
                if (item.AttributeName == "" && item.Value == "") return null;
                return item.Value;
            }
            GbuReportService.Row currentRow;
            lock (PriorityGrouping.Locked)
            {
                currentRow = reportService.GetCurrentRow();
                PriorityGrouping.CurrentCount++;
                reportService.AddValue(inputItem.CadastralNumber, PriorityGrouping.KnColumn, currentRow);
            }

            bool errorCOD = false;
            string errorCODStr = string.Empty;

            try
            {

                //по начальному порядку находим значение ГБУ-атрибутов всех уровней
                var valueItems = new List<ValueItem>();
                allAttributeIds.ForEach(x =>
                {
                    ////TODO для тестирования
                    //var attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(c => c.Id == x)?.Name;
                    valueItems.Add(GetValueFactor(objectAttributes, x));
                });

                var record = new DictRecord
                {
                    Value1 = CleanUp(ExtractValue(valueItems[0])),
                    Value2 = CleanUp(ExtractValue(valueItems[1])),
                    Value3 = CleanUp(ExtractValue(valueItems[2])),
                    Value4 = CleanUp(ExtractValue(valueItems[3])),
                    Value5 = CleanUp(ExtractValue(valueItems[4])),
                    Value6 = CleanUp(ExtractValue(valueItems[5])),
                    Value7 = CleanUp(ExtractValue(valueItems[6])),
                    Value8 = CleanUp(ExtractValue(valueItems[7])),
                    Value9 = CleanUp(ExtractValue(valueItems[8])),
                };

                //string resGroup = GetGroupCode(out string source);
                string resGroup = dict.FirstOrDefault(x => x == record)?.Code ?? "14:000";


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

                        lock (PriorityGrouping.Locked)
                        {
                            reportService.AddValue(
                                GbuObjectService.GetAttributeNameById(setting.IdAttributeResult
                                    .GetValueOrDefault()),
                                PriorityGrouping.ResultColumn, currentRow);
                            reportService.AddValue(resGroup, PriorityGrouping.ValueColumn, currentRow);
                            AddValueItemsToReport(reportService, valueItems, currentRow,
                                PriorityGrouping.ValueColumnOffset);
                        }

                        lock (PriorityGrouping.Locked)
                        {
                            reportService.AddValue(errorCODStr, PriorityGrouping.ErrorColumn, currentRow);
                        }
                    }
                }
                else
                {
                    lock (PriorityGrouping.Locked)
                    {
                        reportService.AddValue(errorCODStr, PriorityGrouping.ErrorColumn, currentRow);
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

            if (PriorityGrouping.CurrentCount % 1000 == 0)
            {
                _log.Debug("Обработан объект {CurrentCount} из {MaxCount}", PriorityGrouping.CurrentCount,
                    PriorityGrouping.MaxCount);
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
            lock (PriorityGrouping.Locked)
            {
                //var errorId = ErrorManager.LogError(ex);
                var fullMessage = $"{message}. {ex.Message}.";
                reportService.AddValue(fullMessage, PriorityGrouping.GeneralErrorColumn, currentRow);
            }

            _log.ForContext("Item", JsonConvert.SerializeObject(inputItem))
                .ForContext("HumanMessage", message)
                .Error(ex, "Ошибка группировки по КН {CadastralNumber}", inputItem.CadastralNumber);
        }

        #endregion
    }


    public class PriorityGroupingItemsGetter : AItemsGetter<GroupingItem>
    {
        private GroupingSettings Settings { get; }

        public PriorityGroupingItemsGetter(ILogger logger, GroupingSettings setting) : base(logger)
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

    /// <summary>
    /// Приоритет группировки
    /// </summary>
    public class PriorityGrouping
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<PriorityGrouping>();
        private static ICodDictionaryService _dictionaryService;

        public PriorityGrouping(ICodDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

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
        public string SetPriorityGroup(GroupingSettings setting, CancellationToken processCancellationToken)
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

            var itemsGetter = new PriorityGroupingItemsGetter(Log, setting) as AItemsGetter<GroupingItem>;
            var actualDate = setting.DateActual?.Date ?? DateTime.Now.Date;
            itemsGetter =
                new GbuObjectStatusFilterDecorator<GroupingItem>(itemsGetter, Log, setting.ObjectChangeStatus,
                    actualDate);

            var dictionaryValues = _dictionaryService.GetDictionaryValuesByDictId(setting.IdCodJob.GetValueOrDefault());
            //var dictionaryItems = GetDictionaryItems(setting);

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
                ProcessUnits(setting, dictionaryValues, items, allAttributeIds, gbuObjectService, reportService,
                    dataHeaderAndColumnNumber, processCancellationToken);
            }
            else
            {
                ProcessObjects(setting, dictionaryValues, items, allAttributeIds, actualDate, gbuObjectService,
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

        private static void ValidateInputParameters(GroupingSettings setting)
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

        private static void ProcessUnits(GroupingSettings setting, List<CodDictionaryValue> dictionaryItems,
            List<GroupingItem> units, List<long> allAttributeIds,
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

                ProcessItems(new ProcessItemInputParameters(setting, dictionaryItems, groupedUnits.Value,
                    objectAttributes, allAttributeIds, true, localActualDate, reportService,
                    processCancellationToken, config));
            });
        }

        private static void ProcessObjects(GroupingSettings setting, List<CodDictionaryValue> dictionaryItems,
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

                ProcessItems(new ProcessItemInputParameters(setting, dictionaryItems, objectsPage,
                    objectAttributes, allAttributeIds, false, actualDate, reportService,
                    processCancellationToken, config));
            });
        }

        private static DictRecord ConvertCodDictValueToRecord(CodDictionaryValue val, GroupingSettings settings)
        {
            return new()
            {
                Value1 = CleanUp(val.GetValueByAttributeId(settings.Level1.CodValueId.GetValueOrDefault())),
                Value2 = CleanUp(val.GetValueByAttributeId(settings.Level2.CodValueId.GetValueOrDefault())),
                Value3 = CleanUp(val.GetValueByAttributeId(settings.Level3.CodValueId.GetValueOrDefault())),
                Value4 = CleanUp(val.GetValueByAttributeId(settings.Level4.CodValueId.GetValueOrDefault())),
                Value5 = CleanUp(val.GetValueByAttributeId(settings.Level5.CodValueId.GetValueOrDefault())),
                Value6 = CleanUp(val.GetValueByAttributeId(settings.Level6.CodValueId.GetValueOrDefault())),
                Value7 = CleanUp(val.GetValueByAttributeId(settings.Level7.CodValueId.GetValueOrDefault())),
                Value8 = CleanUp(val.GetValueByAttributeId(settings.Level8.CodValueId.GetValueOrDefault())),
                Value9 = CleanUp(val.GetValueByAttributeId(settings.Level9.CodValueId.GetValueOrDefault())),
                Code = val.Code
            };
        }

        private static string CleanUp(string x)
        {
            if (x == null) return null;
            // Оставляем только значимые символы в строках и убираем мусор из значения
            return Regex
                .Replace(x, "\\s+", "")
                .Replace("_x000D_", "")
                .Replace("-", "")
                .ToLower();
        }

        private static void ProcessItems(ProcessItemInputParameters inputParameters)
        {
            var localCancelTokenSource = new CancellationTokenSource();
            var options = new ParallelOptions
            {
                CancellationToken = localCancelTokenSource.Token,
                MaxDegreeOfParallelism = inputParameters.ProcessConfig.ThreadsCountForItemsHandling
            };

            var dict = inputParameters.DictionaryValues
                .Select(x => ConvertCodDictValueToRecord(x, inputParameters.Setting)).ToList();

            var objectAttributesDictionary = inputParameters.ObjectAttributes.ToDictionary(k => k.Key, v => v.ToList());
            var defaultAttributes = inputParameters.AllAttributeIds
                .Select(x => new GbuObjectAttribute {AttributeId = x}).ToList();
            var userId = SRDSession.GetCurrentUserId().GetValueOrDefault();
            Parallel.ForEach(inputParameters.Items, options, item =>
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

                new PriorityItem(Log).SetPriorityGroup(inputParameters.Setting,
                    dict,
                    inputParameters.AllAttributeIds, item, localActualDate,
                    currentObjectAttributes, inputParameters.ReportService);
            });
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

        private static List<long> GetAttributes(GroupingSettings setting)
        {
            return new()
            {
                setting.Level1.IdFactor.GetValueOrDefault(), setting.Level2.IdFactor.GetValueOrDefault(),
                setting.Level3.IdFactor.GetValueOrDefault(), setting.Level4.IdFactor.GetValueOrDefault(),
                setting.Level5.IdFactor.GetValueOrDefault(), setting.Level6.IdFactor.GetValueOrDefault(),
                setting.Level7.IdFactor.GetValueOrDefault(), setting.Level8.IdFactor.GetValueOrDefault(),
                setting.Level9.IdFactor.GetValueOrDefault()
            };
        }

        public static ReportHeaderWithColumnDic GenerateReportHeaderWithColumnNumber(GroupingSettings setting)
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
            foreach (FieldInfo propertyInfo in typeof(GroupingSettings).GetFields(BindingFlags.Instance |
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

    #region Entities

    public class GroupingItem : ItemBase
    {
        public string CadastralNumber { get; set; }
        public DateTime? CreationDate { get; set; }
    }

    internal class ProcessConfig
    {
        public int PackageSize { get; set; }
        public int ThreadsCountForObjects { get; set; }
        public int ThreadsCountForUnits { get; set; }
        public int ThreadsCountForItemsHandling { get; set; }
    }

    internal class ProcessItemInputParameters
    {
        public ProcessItemInputParameters(GroupingSettings setting, List<CodDictionaryValue> dictionaryValues, List<GroupingItem> items,
            List<IGrouping<long, GbuObjectAttribute>> objectAttributes, List<long> allAttributeIds, bool useTask,
            DateTime actualDate, GbuReportService reportService,
            CancellationToken processCancellationToken, ProcessConfig processConfig)
        {
            Setting = setting;
            DictionaryValues = dictionaryValues;
            Items = items;
            ObjectAttributes = objectAttributes;
            AllAttributeIds = allAttributeIds;
            UseTask = useTask;
            ActualDate = actualDate;
            ReportService = reportService;
            ProcessCancellationToken = processCancellationToken;
            ProcessConfig = processConfig;
        }

        public GroupingSettings Setting { get; }
        public List<CodDictionaryValue> DictionaryValues { get; }
        public List<GroupingItem> Items { get; }
        public List<IGrouping<long, GbuObjectAttribute>> ObjectAttributes { get; }
        public List<long> AllAttributeIds { get; }
        public bool UseTask { get; }
        public DateTime ActualDate { get; }
        public GbuReportService ReportService { get; }
        public CancellationToken ProcessCancellationToken { get; }
        public ProcessConfig ProcessConfig { get; }
    }

    public record DictRecord
    {
        public string Value1 { get; init; }
        public string Value2 { get; init; }
        public string Value3 { get; init; }
        public string Value4 { get; init; }
        public string Value5 { get; init; }
        public string Value6 { get; init; }
        public string Value7 { get; init; }
        public string Value8 { get; init; }
        public string Value9 { get; init; }
        public string Code { get; init; }

        public virtual bool Equals(DictRecord dictRecord)
        {
            bool equal = true;
            equal &= Value1 == dictRecord?.Value1;
            equal &= Value2 == dictRecord?.Value2;
            equal &= Value3 == dictRecord?.Value3;
            equal &= Value4 == dictRecord?.Value4;
            equal &= Value5 == dictRecord?.Value5;
            equal &= Value6 == dictRecord?.Value6;
            equal &= Value7 == dictRecord?.Value7;
            equal &= Value8 == dictRecord?.Value8;
            equal &= Value9 == dictRecord?.Value9;
            return equal;
        }

        public override int GetHashCode()
        {
            return Value1.GetHashCode() +
                   Value2.GetHashCode() +
                   Value3.GetHashCode() +
                   Value4.GetHashCode() +
                   Value5.GetHashCode() +
                   Value6.GetHashCode() +
                   Value7.GetHashCode() +
                   Value8.GetHashCode() +
                   Value9.GetHashCode();
        }
    }

    #endregion
}