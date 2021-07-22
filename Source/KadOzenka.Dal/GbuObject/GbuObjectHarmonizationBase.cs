using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using ObjectModel.Gbu;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks.Excel;
using ObjectModel.KO;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.SRD;
using DevExpress.Data.Extensions;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.GbuObject.Entities;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using ObjectModel.Directory;
using Serilog;

namespace KadOzenka.Dal.GbuObject
{
	public abstract class GbuObjectHarmonizationBase
    {
        #region Fields
        private ILogger _log;
        protected object Locked;
        public int MaxObjectsCount;
        public int CurrentCount;

        private const int KnColumnNumber = 0;
        private const int ResultColumnNumber = 1;
        private const int ValueColumnNumber = 2;
        private const int SourceColumnNUmber = 3;
        private const int ErrorColumnNumber = 4;
        private long UserId { get; set; }

        protected abstract string ReportName { get; }
        private static ABaseHarmonizationSettings BaseSetting { get; set; }
        private static GbuObjectService GbuObjectService { get; set; }

        private RegisterAttribute _registerAttribute;
        private RegisterAttribute ResultAttribute
        {
            get
            {
                if (_registerAttribute != null)
                    return _registerAttribute;

                //если во время гармонизации был создан новый атрибут, его может еще не быть в кеше
                RegisterCache.UpdateCache(0, null);
                _registerAttribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == BaseSetting.IdAttributeResult);

                return _registerAttribute;
            }
        }

        #endregion


        protected GbuObjectHarmonizationBase(ABaseHarmonizationSettings setting, long? userId, ILogger logger)
        {
            BaseSetting = setting;
            _log = logger;
            GbuObjectService = new GbuObjectService();

            UserId = userId ?? SRDSession.Current.UserID;
            _log.ForContext("UserIdFromProcess", userId).Debug("ИД Юзера - {ResultUserId}", UserId);
        }


        public string Run()
        {
	        _log.Debug("Валидация входных параметров");
            ValidateInputParameters();

            using var reportService = new GbuReportService(ReportName);
            reportService.AddHeaders(new List<string> { "КН", "Поле в которое производилась запись", "Внесенное значение", "Источник внесенного значения", "Ошибка" });
            reportService.SetIndividualWidth(KnColumnNumber, 4);
            reportService.SetIndividualWidth(ResultColumnNumber, 6);
            reportService.SetIndividualWidth(ValueColumnNumber, 3);
            reportService.SetIndividualWidth(SourceColumnNUmber, 6);
            reportService.SetIndividualWidth(ErrorColumnNumber, 5);

            _log.Debug("Получение объектов для обработки");
            //добавление фильтров на лету через декоратор
            var itemsGetter = new HarmonizationItemsGetter(BaseSetting, _log) as AItemsGetter<Item>;
            itemsGetter = new HarmonizationBuildingFilterDecorator<Item>(itemsGetter, _log, BaseSetting);
            itemsGetter = new HarmonizationPlacementFilterDecorator<Item>(itemsGetter, _log, BaseSetting);
            itemsGetter = new GbuObjectStatusFilterDecorator<Item>(itemsGetter, _log, BaseSetting.ObjectChangeStatus,
	            BaseSetting.DateActual ?? DateTime.Now.GetEndOfTheDay());

            var objects = itemsGetter.GetItems();
            MaxObjectsCount = objects.Count;
            _log.Debug("Получено {ObjectsCount} объектов для дальнейшей обработки", MaxObjectsCount);

            var levelsAttributesIds = GetLevelsAttributesIds();
            _log.ForContext("InputParameters", levelsAttributesIds, true).Debug("Получен список атрибутов-уровней");

            Locked = new object();
            ProcessItems(objects, levelsAttributesIds, reportService);

            _log.Debug("Сохранение отчета");
            var reportId = reportService.SaveReport();

            return reportService.GetUrlToDownloadFile(reportId);
        }

        private void ProcessItems(List<Item> allItems, List<long> levelsAttributesIds, GbuReportService reportService)
        {
	        var localCancelTokenSource = new CancellationTokenSource();
            var options = new ParallelOptions
            {
                CancellationToken = localCancelTokenSource.Token,
                MaxDegreeOfParallelism = 5
            };

            var packageSize = 100000;
            var groupedItems = allItems.GroupBy(x => x.Date).ToDictionary(k => k.Key, v => v.ToList());
            groupedItems.ForEach(group =>
            {
	            var actualDate = group.Key;
	            var items = group.Value;
	            var numberOfPackages = group.Value.Count / packageSize + 1;
                Parallel.For(0, numberOfPackages, options, (i, s) =>
				{
					_log.Debug("Начата работа с пакетом №{PackageNumber} из {MaxPackagesCount}", i, numberOfPackages);

					var itemsPage = items.Skip(i * packageSize).Take(packageSize).ToList();
					_log.Debug("Отобрано {ObjectsCount} объектов для пакета №{PackageNumber} из {MaxPackagesCount}", itemsPage.Count, i, numberOfPackages);

					var objectAttributes = GbuObjectService.GetAllAttributes(itemsPage.Select(x => x.ObjectId).ToList(), null, levelsAttributesIds, actualDate,
							attributesToDownload: new List<GbuColumnsToDownload>
							{
								GbuColumnsToDownload.Value, GbuColumnsToDownload.DocumentId, GbuColumnsToDownload.S,
								GbuColumnsToDownload.Ot
							}).GroupBy(x => x.ObjectId).ToList();
					_log.Debug("Найдено {AttributesCount} атрибутов для пакета №{PackageNumber} из {MaxPackagesCount}", objectAttributes.Count, i, numberOfPackages);

					DoHarmonization(itemsPage, levelsAttributesIds, objectAttributes, reportService);
				});
			});
        }

        private void DoHarmonization(List<Item> objects, List<long> levelsAttributesIds,
	        List<IGrouping<long, GbuObjectAttribute>> objectAttributes, GbuReportService reportService)
        {
	        _log.Debug("Старт обработки объектов");

	        var cancelTokenSource = new CancellationTokenSource();
	        var options = new ParallelOptions
	        {
		        CancellationToken = cancelTokenSource.Token,
		        MaxDegreeOfParallelism = 20
	        };

	        var objectAttributesDictionary = objectAttributes.ToDictionary(k => k.Key, v => v.ToList());
	        var defaultAttributes = levelsAttributesIds.Select(x => new GbuObjectAttribute {AttributeId = x}).ToList();
	        Parallel.ForEach(objects, options, obj =>
	        {
		        var currentObjectAttributes = objectAttributesDictionary.TryGetValue(obj.ObjectId, out var attributes)
			        ? attributes
			        : defaultAttributes;

		        ProcessOneObject(obj, levelsAttributesIds, currentObjectAttributes, reportService);

		        lock (Locked)
		        {
			        CurrentCount++;

			        if (CurrentCount % 1000 == 0)
			        {
				        _log.Debug("Завершена обработка объекта №{CurrentCount} из {MaxCount}", CurrentCount, MaxObjectsCount);
			        }
		        }
	        });

	        _log.Debug("Финиш обработки объектов ");
        }

        protected abstract bool CopyLevelData(Item item, GbuObjectAttribute sourceAttribute, GbuReportService reportService);

        protected abstract void SaveFailResult(Item item, GbuReportService reportService);

        protected void SaveGbuAttribute(Item item, long changeDocId, DateTime s, DateTime ot, string value, long? sourceAttributeId, GbuReportService reportService, string errorMessageForReport = "")
        {
            var gbuAttribute = new GbuObjectAttribute
            {
                AttributeId = BaseSetting.IdAttributeResult,
                ObjectId = item.ObjectId,
                ChangeDocId = changeDocId,
                S = s,
                ChangeUserId = UserId,
                ChangeDate = DateTime.Now,
                Ot = ot
            };

            var convertingErrorMessage = string.Empty;
            if (value != null)
            {
                switch (ResultAttribute.Type)
                {
                    case RegisterAttributeType.STRING:
                        gbuAttribute.StringValue = value;
                        break;
                    case RegisterAttributeType.DECIMAL:
                    case RegisterAttributeType.INTEGER:
                        if (decimal.TryParse(value, out var number))
                        {
                            gbuAttribute.NumValue = number;
                        }
                        else
                        {
                            convertingErrorMessage = GetErrorMessage(value, RegisterAttributeType.DECIMAL);
                        }
                        break;
                    case RegisterAttributeType.DATE:
                        if (DateTime.TryParse(value, out var date))
                        {
                            gbuAttribute.DtValue = date;
                        }
                        else
                        {
                            convertingErrorMessage = GetErrorMessage(value, RegisterAttributeType.DATE);
                        }

                        break;
                }
            }

            //для атрибутов Росреестра есть индекс уникальности на object_id и ot
            if (GbuObjectService.CheckExistsValueFromAttributeIdPartition(gbuAttribute.ObjectId, gbuAttribute.AttributeId, gbuAttribute.Ot) != null)
            {
                lock (Locked)
                {
                    gbuAttribute.Ot = GbuObjectService.GetNextOtFromAttributeIdPartition(gbuAttribute.ObjectId, gbuAttribute.AttributeId, gbuAttribute.Ot);
                }
            }

            gbuAttribute.Save();

            lock (Locked)
            {
                var row = reportService.GetCurrentRow();
                var savedValue = string.IsNullOrWhiteSpace(convertingErrorMessage) ? value : string.Empty;
                var resultErrorMessage = $"{convertingErrorMessage} {errorMessageForReport}";

                AddRowToReport(reportService, row, item.CadastralNumber, sourceAttributeId ?? 0, savedValue,
                    BaseSetting.IdAttributeResult, resultErrorMessage);
            }
        }


        #region Support Methods

        private static void ValidateInputParameters()
        {
	        if (BaseSetting.DateActual != null && BaseSetting.IdAttributeFilter.GetValueOrDefault() == 0)
                throw new Exception("Выбрана только дата актуальности, но не выбрана характеристика");
        }

        private static List<long> GetLevelsAttributesIds()
        {
            var allLevelsAttributeIds = new List<long?>
            {
                BaseSetting.Level1Attribute,
                BaseSetting.Level2Attribute,
                BaseSetting.Level3Attribute,
                BaseSetting.Level4Attribute,
                BaseSetting.Level5Attribute,
                BaseSetting.Level6Attribute,
                BaseSetting.Level7Attribute,
                BaseSetting.Level8Attribute,
                BaseSetting.Level9Attribute,
                BaseSetting.Level10Attribute
            };
            if (BaseSetting.AdditionalLevels != null && allLevelsAttributeIds.Count != 0)
                allLevelsAttributeIds.AddRange(BaseSetting.AdditionalLevels.Select(x => x.AttributeId));

            return allLevelsAttributeIds.Where(x => x != null).Select(x => x.Value).ToList();
        }

        private void ProcessOneObject(Item item, List<long> levelsAttributeIds, List<GbuObjectAttribute> gbuAttributes,
	        GbuReportService reportService)
        {
	        foreach (var sourceAttributeId in levelsAttributeIds)
	        {
		        var sourceAttribute = gbuAttributes.FirstOrDefault(x => x.AttributeId == sourceAttributeId);
		        if (sourceAttribute == null)
			        continue;

		        var isDataSaved = CopyLevelData(item, sourceAttribute, reportService);
		        if (isDataSaved)
			        return;
	        }

	        SaveFailResult(item, reportService);
        }

        private string GetErrorMessage(string value, RegisterAttributeType type)
        {
            return $"Не удалось преобразовать значение '{value}' к типу '{type.GetEnumDescription()}'.Cохранено пустое значение.";
        }

        private static void AddRowToReport(GbuReportService reportService, GbuReportService.Row rowNumber, string kn, long sourceAttribute, string value, long resultAttribute, string errorMessage)
        {
            var sourceName = GbuObjectService.GetAttributeNameById(sourceAttribute);
            var resultName = GbuObjectService.GetAttributeNameById(resultAttribute);
            reportService.AddValue(kn, KnColumnNumber, rowNumber);
            reportService.AddValue(resultName, ResultColumnNumber, rowNumber);
            reportService.AddValue(value, ValueColumnNumber, rowNumber);
            reportService.AddValue(sourceName, SourceColumnNUmber, rowNumber);
            reportService.AddValue(errorMessage, ErrorColumnNumber, rowNumber);
        }

        #endregion
    }

    #region Entities

    public class Item : ItemBase
    {
	    public string CadastralNumber { get; set; }
	    public DateTime Date { get; set; }
    }

    public class HarmonizationItemsGetter : AItemsGetter<Item>
    {
        private static GbuObjectService GbuObjectService { get; set; }
        private static ABaseHarmonizationSettings BaseSetting { get; set; }

        public HarmonizationItemsGetter(ABaseHarmonizationSettings settings, ILogger log) : base(log)
        {
            BaseSetting = settings;
            GbuObjectService = new GbuObjectService();
        }

        public override List<Item> GetItems()
        {
            var byTasks = false;
            if (BaseSetting.TaskFilter != null)
                byTasks = BaseSetting.TaskFilter.Count > 0;

            var objectType = BaseSetting.PropertyType;
            if (BaseSetting.PropertyType == PropertyTypes.Pllacement &&
                BaseSetting.PlacementPurpose == PlacementPurpose.ParkingPlace)
            {
                objectType = PropertyTypes.Parking;
            }

            var objects = byTasks ? GetUnits(objectType) : GetObjects(objectType);

            return objects;
        }

        private List<Item> GetUnits(PropertyTypes objectType)
        {
            Logger.Debug("Начато получение ЕО по списку заданий на оценку");

            if (BaseSetting.TaskFilter == null || BaseSetting.TaskFilter.Count == 0)
                throw new Exception("Была выбрана фильтрация по Заданиям на оценку, но не были выбраны задания.");

            var objects = new List<Item>();
            BaseSetting.TaskFilter.ForEach(taskId =>
            {
	            var units = OMUnit.Where(x => x.PropertyType_Code == objectType && x.TaskId == taskId && x.ObjectId != null)
		            .Select(x => new
		            {
			            x.ObjectId,
			            x.CadastralNumber,
			            x.CreationDate
		            })
		            .Execute();

	            objects.AddRange(units.Select(x => new Item
	            {
		            CadastralNumber = x.CadastralNumber,
		            ObjectId = x.ObjectId.GetValueOrDefault(),
		            Date = x.CreationDate ?? DateTime.Now.GetEndOfTheDay()
	            }));

                Logger.Debug("Загружено {UnitsCount} ЕО для задания на оценку {TaskId}", units.Count, taskId);
            });

            Logger.Debug("Общее количество загруженных ЕО: {ResultObjectsCount}", objects.Count);

            return FilterObjects(objects);
        }

        private List<Item> GetObjects(PropertyTypes objectType)
        {
            Logger.Debug("Начато получение ОН по типу");

            var allObjects = OMMainObject.Where(x => x.ObjectType_Code == objectType && x.IsActive == true)
                .Select(x => x.CadastralNumber)
                .Execute()
                .Select(x => new Item
                {
                    CadastralNumber = x.CadastralNumber,
                    ObjectId = x.Id,
                    Date = DateTime.Now.GetEndOfTheDay()
                }).ToList();

            Logger.Debug("Общее количество загруженных ОН: {AllObjectsCount}", allObjects.Count);

            if (BaseSetting.SelectAllObject)
                return allObjects;

            return FilterObjects(allObjects);
        }

        private List<Item> FilterObjects(List<Item> allObjects)
        {
            List<Item> result;

            if (BaseSetting.IdAttributeFilter.GetValueOrDefault() == 0 || allObjects.Count == 0)
            {
                result = allObjects;
            }
            else
            {
                if (BaseSetting.ValuesFilter == null || BaseSetting.ValuesFilter.Count == 0)
                    throw new Exception("Была выбрана фильтрация по Значению, но не были выбраны значения.");

                Logger.Debug("Начата фильтрация по Значению");

                var date = BaseSetting.DateActual ?? DateTime.Now.GetEndOfTheDay();
                var allObjectsAttributes = GbuObjectService.GetAllAttributes(
                    allObjects.Select(x => x.ObjectId).ToList(),
                    null,
                    new List<long> { BaseSetting.IdAttributeFilter.Value },
                    date,
                    attributesToDownload: new List<GbuColumnsToDownload> { GbuColumnsToDownload.Value });

                var resultObjectIds = new List<long?>();
                var lowerFilterValues = BaseSetting.ValuesFilter.Select(x => x.ToLower()).ToList();
                allObjectsAttributes.ForEach(x =>
                {
                    var lowerAttributeValue = x.GetValueInString()?.ToLower();
                    if (lowerAttributeValue != null && lowerFilterValues.Contains(lowerAttributeValue))
                    {
                        resultObjectIds.Add(x.ObjectId);
                    }
                });

                result = allObjects.Where(x => resultObjectIds.Contains(x.ObjectId)).ToList();
            }

            Logger.Debug("Выбрано {ResultCount} объектов после фильтрации по Значению", result.Count);

            return result;
        }
    }


    public class HarmonizationBuildingFilterDecorator<T> : ADecorator<T> where T : ItemBase
    {
	    private ABaseHarmonizationSettings BaseSetting { get; }

        public HarmonizationBuildingFilterDecorator(AItemsGetter<T> comp, ILogger log, ABaseHarmonizationSettings settings)
            : base(comp, log)
        {
            BaseSetting = settings;
        }


        public override List<T> GetItems()
        {
            var allItems = base.GetItems();

            if (BaseSetting.PropertyType == PropertyTypes.Building)
            {
                allItems = FilterBuildingObjects(allItems);
            }

            return allItems;
        }

        private List<T> FilterBuildingObjects(List<T> allObjects)
        {
            List<T> result;

            if (BaseSetting.PropertyType != PropertyTypes.Building ||
                BaseSetting.BuildingPurpose == BuildingPurpose.None)
            {
                result = allObjects;
            }
            else
            {
                Logger.Debug("Начата фильтрация по атрибуту 'Назначение здания'");

                var buildingPurposeAttribute = RosreestrRegisterService.GetBuildingPurposeAttribute();

                var allObjectsAttributes = GbuObjectService.GetAllAttributes(
                    allObjects.Select(x => x.ObjectId).ToList(),
                    new List<long> { buildingPurposeAttribute.RegisterId },
                    new List<long> { buildingPurposeAttribute.Id },
                    BaseSetting.DateActual ?? DateTime.Now.GetEndOfTheDay(),
                    attributesToDownload: new List<GbuColumnsToDownload> { GbuColumnsToDownload.Value });

                var possibleValues = new List<string>();
                switch (BaseSetting.BuildingPurpose)
                {
                    case BuildingPurpose.Live:
                        possibleValues.Add("Жилое");
                        break;
                    case BuildingPurpose.NotLive:
                        possibleValues.Add("Нежилое");
                        break;
                    case BuildingPurpose.ApartmentHouse:
                        possibleValues.Add("Многоквартирный дом");
                        break;
                    case BuildingPurpose.LiveAndApartmentHouse:
                        possibleValues.Add("Жилое");
                        possibleValues.Add("Многоквартирный дом");
                        break;
                }

                var resultObjectIds = new List<long?>();
                allObjectsAttributes.ForEach(x =>
                {
                    var buildingPurpose = x.GetValueInString();
                    if (!string.IsNullOrWhiteSpace(buildingPurpose) && possibleValues.Contains(buildingPurpose))
                    {
                        resultObjectIds.Add(x.ObjectId);
                    }
                });

                result = allObjects.Where(x => resultObjectIds.Contains(x.ObjectId)).ToList();
            }

            Logger.Debug("Выбрано {ResultCount} объектов после фильтрации по атрибуту 'Назначение здания'", result.Count);

            return result;
        }
    }

    public class HarmonizationPlacementFilterDecorator<T> : ADecorator<T> where T : ItemBase
    {
	    private ABaseHarmonizationSettings BaseSetting { get; }

        public HarmonizationPlacementFilterDecorator(AItemsGetter<T> comp, ILogger log, ABaseHarmonizationSettings settings)
            : base(comp, log)
        {
            BaseSetting = settings;
        }

        public override List<T> GetItems()
        {
            var allItems = base.GetItems();

            if (BaseSetting.PropertyType == PropertyTypes.Pllacement)
            {
                allItems = FilterPlacementObjects(allItems);
            }

            return allItems;
        }

        private List<T> FilterPlacementObjects(List<T> allObjects)
        {
            List<T> result;

            if (BaseSetting.PropertyType != PropertyTypes.Pllacement ||
                BaseSetting.PlacementPurpose == PlacementPurpose.None ||
                BaseSetting.PlacementPurpose == PlacementPurpose.ParkingPlace)
            {
                result = allObjects;
            }
            else
            {
                Logger.Debug("Начата фильтрации по атрибуту 'Назначение помещения'");

                var placementPurposeAttribute = RosreestrRegisterService.GetPlacementPurposeAttribute();

                var allObjectsAttributes = GbuObjectService.GetAllAttributes(
                    allObjects.Select(x => x.ObjectId).ToList(),
                    new List<long> { placementPurposeAttribute.RegisterId },
                    new List<long> { placementPurposeAttribute.Id },
                    BaseSetting.DateActual ?? DateTime.Now.GetEndOfTheDay(),
                    attributesToDownload: new List<GbuColumnsToDownload> { GbuColumnsToDownload.Value });

                var possibleValues = new List<string>();
                switch (BaseSetting.PlacementPurpose)
                {
                    case PlacementPurpose.Live:
                        possibleValues.Add("Жилое");
                        break;
                    case PlacementPurpose.NotLive:
                        possibleValues.Add("Нежилое");
                        break;
                }

                var resultObjectIds = new List<long?>();
                allObjectsAttributes.ForEach(x =>
                {
                    var buildingPurpose = x.GetValueInString();
                    if (!string.IsNullOrWhiteSpace(buildingPurpose) && possibleValues.Contains(buildingPurpose))
                    {
                        resultObjectIds.Add(x.ObjectId);
                    }
                });

                result = allObjects.Where(x => resultObjectIds.Contains(x.ObjectId)).ToList();
            }

            Logger.Debug("Выбрано {ResultCount} объектов после фильтрации по атрибуту 'Назначение помещения'", result.Count);

            return result;
        }
    }

    #endregion
}
