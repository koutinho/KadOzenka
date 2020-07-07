using System;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Gbu.Harmonization;
using Core.Shared.Extensions;
using ObjectModel.Gbu;
using System.Threading;
using System.Threading.Tasks;
using ObjectModel.KO;

namespace KadOzenka.Dal.GbuObject
{
    public abstract class GbuObjectHarmonizationBase
    {
        protected object locked;
        public int MaxObjectsCount;
        public int CurrentCount;

        private const int KnColumnNumber = 0;
        private const int ResultColumnNumber = 1;
        private const int ValueColumnNumber = 2;
        private const int SourceColumnNUmber = 3;
        private const int ErrorColumnNumber = 4;

        private static ABaseHarmonizationSettings BaseSetting { get; set; }
        protected static GbuReportService ReportService { get; set; }
        private static GbuObjectService GbuObjectService { get; set; }


        protected GbuObjectHarmonizationBase(ABaseHarmonizationSettings setting)
        {
            BaseSetting = setting;
            ReportService = new GbuReportService();
            GbuObjectService = new GbuObjectService();
        }


        public long Run()
        {
            ValidateInputParameters();

            ReportService.AddHeaders(0, new List<string> { "КН", "Поле в которое производилась запись", "Внесенное значение", "Источник внесенного значения", "Ошибка" });

            var objects = GetObjects();
            var notNullAttributeIds = GetAllLevelsAttributeIds();
            MaxObjectsCount = objects.Count;
            CurrentCount = 0;

            locked = new object();
            var cancelTokenSource = new CancellationTokenSource();
            var options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };
            
            Parallel.ForEach(objects, options, obj =>
            {
                ProcessOneObject(obj, notNullAttributeIds);
            });

            ReportService.SetStyle();
            ReportService.SetIndividualWidth(KnColumnNumber, 4);
            ReportService.SetIndividualWidth(ResultColumnNumber, 6);
            ReportService.SetIndividualWidth(ValueColumnNumber, 3);
            ReportService.SetIndividualWidth(SourceColumnNUmber, 6);
            ReportService.SetIndividualWidth(ErrorColumnNumber, 5);

            var reportId = ReportService.SaveReport("Отчет гармонизации");

            return reportId;
        }

        protected abstract bool CopyLevelData(Item item, long sourceAttributeId, long resultAttributeId, List<GbuObjectAttribute> allAttributes);

        protected abstract void SaveFailResult(Item item);

        protected static void AddRowToReport(int rowNumber, string kn, long sourceAttribute, string value, long resultAttribute, string errorMessage)
        {
            var sourceName = GbuObjectService.GetAttributeNameById(sourceAttribute);
            var resultName = GbuObjectService.GetAttributeNameById(resultAttribute);
            ReportService.AddValue(kn, KnColumnNumber, rowNumber);
            ReportService.AddValue(resultName, ResultColumnNumber, rowNumber);
            ReportService.AddValue(value, ValueColumnNumber, rowNumber);
            ReportService.AddValue(sourceName, SourceColumnNUmber, rowNumber);
            ReportService.AddValue(errorMessage, ErrorColumnNumber, rowNumber);
        }

        #region Support Methods

        private static void ValidateInputParameters()
        {
            if(BaseSetting.IdAttributeResult.GetValueOrDefault() == 0)
                throw new Exception("Не выбрана результирующая характеристика");
            if(BaseSetting.DateActual != null && BaseSetting.IdAttributeFilter.GetValueOrDefault() == 0)
                throw new Exception("Выбрана только дата актуальности, но не выбрана характеристика");
        }

        private static List<Item> GetObjects()
        {
            var byTasks = false;
            if (BaseSetting.TaskFilter != null)
                byTasks = BaseSetting.TaskFilter.Count > 0;

            return byTasks 
                ? GetObjectsByTasks() 
                : GetObjectsWithoutTasks();
        }

        private static List<Item> GetObjectsByTasks()
        {
            if (BaseSetting.TaskFilter == null || BaseSetting.TaskFilter.Count == 0)
            {
                throw new Exception("Была выбрана фильтрация по Заданиям на оценку, но не были выбраны задания.");
            }

            var objects = new List<Item>();
            BaseSetting.TaskFilter.ForEach(taskId =>
            {
                var units = OMUnit
                    .Where(x => x.PropertyType_Code == BaseSetting.PropertyType && x.TaskId == taskId && x.ObjectId != null)
                    .Select(x => x.ObjectId)
                    .Select(x => x.CadastralNumber)
                    .Select(x => x.CreationDate)
                    .Execute();

                objects.AddRange(units.Select(x => new Item
                {
                    CadastralNumber = x.CadastralNumber,
                    ObjectId = x.ObjectId.Value,
                    Date = x.CreationDate ?? DateTime.Now
                }));
            });

            return FilterObjects(objects);
        }

        private static List<Item> GetObjectsWithoutTasks()
        {
            var allObjects = OMMainObject.Where(x => x.ObjectType_Code == BaseSetting.PropertyType && x.IsActive == true)
                .Select(x => x.Id)
                .Select(x => x.CadastralNumber)
                .Execute()
                .Select(x => new Item
                {
                    CadastralNumber = x.CadastralNumber,
                    ObjectId = x.Id,
                    Date = DateTime.Now
                }).ToList();

            if (BaseSetting.SelectAllObject)
                return allObjects;

            return FilterObjects(allObjects);
        }

        private static List<Item> FilterObjects(List<Item> allObjects)
        {
            if (BaseSetting.IdAttributeFilter.GetValueOrDefault() == 0 || allObjects.Count == 0)
                return allObjects;

            if (BaseSetting.ValuesFilter == null || BaseSetting.ValuesFilter.Count == 0)
                throw new Exception("Была выбрана фильтрация по Значению, но не были выбраны значения.");

            var date = BaseSetting.DateActual ?? DateTime.Now.GetEndOfTheDay();
            var allObjectsAttributes = GbuObjectService.GetAllAttributes(
                allObjects.Select(x => x.ObjectId).Distinct().ToList(),
                null,
                new List<long> { BaseSetting.IdAttributeFilter.Value},
                date);

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

            return allObjects.Where(x => resultObjectIds.Contains(x.ObjectId)).ToList();
        }

        private static List<long> GetAllLevelsAttributeIds()
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

        private void ProcessOneObject(Item item, List<long> levelsAttributeIds)
        {
            lock (locked)
            {
                CurrentCount++;
            }

            var attributes = GbuObjectService.GetAllAttributes(item.ObjectId, null, levelsAttributeIds, item.Date);
            foreach (var attribute in levelsAttributeIds)
            {
                var isDataSaved = CopyLevelData(item, attribute, BaseSetting.IdAttributeResult.Value, attributes);
                if (isDataSaved)
                    return;
            }

            SaveFailResult(item);
        }

        #endregion


        #region Entities

        protected class Item
        {
            public string CadastralNumber { get; set; }
            public long ObjectId { get; set; }
            public DateTime Date { get; set; }
        }

        #endregion
    }
}
