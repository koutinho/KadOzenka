using System;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Gbu.Harmonization;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Простая гармонизация
    /// </summary>
    public class Harmonization
    {
        /// <summary>
        /// Объект для блокировки счетчика в многопоточке
        /// </summary>
        private static object locked;
        /// <summary>
        /// Общее число объектов
        /// </summary>
        public static int MaxCount = 0;
        /// <summary>
        /// Индекс текущего объекта
        /// </summary>
        public static int CurrentCount = 0;

        private static GbuReportService _reportService;

        /// <summary>
        /// Выполнение операции гармонизации
        /// </summary>
        public static long Run(HarmonizationSettings setting)
        {
			 _reportService = new GbuReportService();

			_reportService.AddHeaders(0, new List<string>{"КН", "Поле в которое производилась запись", "Внесенное значение", "Источник внесенного значения", "Ошибка"});

            locked = new object();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };


            bool useTask = false;
            if (setting.TaskFilter != null) useTask = setting.TaskFilter.Count > 0;
            if (useTask)
            {
                List<ObjectModel.KO.OMUnit> Objs = new List<ObjectModel.KO.OMUnit>();
                foreach (long taskId in setting.TaskFilter)
                {
                    Objs.AddRange(ObjectModel.KO.OMUnit.Where(x => x.PropertyType_Code == setting.PropertyType && x.TaskId == taskId).SelectAll().Execute());
                }
                MaxCount = Objs.Count;
                CurrentCount = 0;
                ////TODO для тестирования
                //Parallel.ForEach(Objs.Take(1), options, item => { RunOneUnit(item, setting); });
                Parallel.ForEach(Objs, options, item => { RunOneUnit(item, setting); });
                CurrentCount = 0;  
                MaxCount = 0;
            }
            else
            {
                List<ObjectModel.Gbu.OMMainObject> Objs = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == setting.PropertyType && x.IsActive == true).SelectAll().Execute();
                MaxCount = Objs.Count;
                CurrentCount = 0;
                ////TODO для тестирования
                //Parallel.ForEach(Objs.Take(1), options, item => { RunOneGbu(item, setting); });
                Parallel.ForEach(Objs, options, item => { RunOneGbu(item, setting); });
                CurrentCount = 0;
                MaxCount = 0;
            }

            _reportService.SetStyle();
            _reportService.SetIndividualWidth(1, 6);
            _reportService.SetIndividualWidth(0, 4);
            _reportService.SetIndividualWidth(2, 3);
            _reportService.SetIndividualWidth(3, 6);
            _reportService.SetIndividualWidth(4, 5);
            var reportId = _reportService.SaveReport("Отчет гармонизации");
			_reportService = null;

			return reportId;
        }


        #region Support Methods

        public static void RunOneGbu(ObjectModel.Gbu.OMMainObject obj, HarmonizationSettings setting)
        {
            var dt = setting.DateActual ?? DateTime.Now;
            RunOneItem(obj.CadastralNumber, obj.Id, dt, setting);
        }

        public static void RunOneUnit(ObjectModel.KO.OMUnit obj, HarmonizationSettings setting)
        {
            RunOneItem(obj.CadastralNumber, obj.ObjectId, obj.CreationDate, setting);
        }

        private static void RunOneItem(string cadastralNumber, long? objectId, DateTime? date, HarmonizationSettings setting)
        {
            ////TODO для тестирования
            //objectId = 11188991;

            lock (locked)
            {
                CurrentCount++;
            }

            if (objectId == null)
                return;

            var allLevelsAttributeIds = GetAllLevelsAttributeIds(setting);
            var notNullAttributeIds = allLevelsAttributeIds.Where(x => x != null).Select(x => x.Value).ToList();
            var attributes = new GbuObjectService().GetAllAttributes(objectId.Value, null, notNullAttributeIds, date);

            foreach (var attribute in allLevelsAttributeIds)
            {
                var isDataSaved = SaveLevelData(cadastralNumber, objectId.Value, attribute, setting.IdAttributeResult, attributes);
                if(isDataSaved)
                    return;
            }
            
            SaveFailResult(cadastralNumber, objectId.Value, date ?? DateTime.Now, setting);
        }

        private static List<long?> GetAllLevelsAttributeIds(HarmonizationSettings setting)
        {
            var allLevelsAttributeIds = new List<long?>
            {
                setting.Level1Attribute,
                setting.Level2Attribute,
                setting.Level3Attribute,
                setting.Level4Attribute,
                setting.Level5Attribute,
                setting.Level6Attribute,
                setting.Level7Attribute,
                setting.Level8Attribute,
                setting.Level9Attribute,
                setting.Level10Attribute
            };
            if (setting.AdditionalLevels != null && allLevelsAttributeIds.Count != 0)
                allLevelsAttributeIds.AddRange(setting.AdditionalLevels.Select(x => x.AttributeId));

            return allLevelsAttributeIds;
        }

        private static bool SaveLevelData(string cadastralNumber, long objectId, long? sourceAttributeId, long? resultAttributeId, List<GbuObjectAttribute> allAttributes)
        {
            if (sourceAttributeId == null || resultAttributeId == null)
                return false;

            var sourceAttribute = allAttributes.FirstOrDefault(x => x.AttributeId == sourceAttributeId.Value);
            if (sourceAttribute == null)
                return false;

            var attributeValueInString = sourceAttribute.GetValueInString();
            if (string.IsNullOrEmpty(attributeValueInString))
                return false;

            lock (locked)
            {
                var rowReport = _reportService.GetCurrentRow();
                AddRowToReport(rowReport, cadastralNumber, sourceAttributeId.Value, attributeValueInString, resultAttributeId.Value, string.Empty);
            }

            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = resultAttributeId.Value,
                ObjectId = objectId,
                ChangeDocId = sourceAttribute.ChangeDocId,
                S = sourceAttribute.S,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = sourceAttribute.Ot,
                StringValue = attributeValueInString
            };

            attributeValue.Save();

            return true;
        }

        private static void SaveFailResult(string cadastralNumber, long objectId, DateTime date,
            HarmonizationSettings setting)
        {
            lock (locked)
            {
                var rowReport = _reportService.GetCurrentRow();
                var message = "Для текущего объекта не было записанно значение, т.к не было найдено.";
                AddRowToReport(rowReport, cadastralNumber, 0, "", setting.IdAttributeResult.Value, message);
            }

            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = setting.IdAttributeResult.Value,
                ObjectId = objectId,
                ChangeDocId = -1,
                S = date,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = date
            };

            attributeValue.Save();
        }

        private static void AddRowToReport(int rowNumber, string kn, long sourceAttribute, string value, long resultAttribute, string errorMessage)
        {
            string sourceName = GbuObjectService.GetAttributeNameById(sourceAttribute);
            string resultName = GbuObjectService.GetAttributeNameById(resultAttribute);
            _reportService.AddValue(kn,0, rowNumber);
            _reportService.AddValue(sourceName,3, rowNumber);
            _reportService.AddValue(value,2, rowNumber);
            _reportService.AddValue(resultName,1, rowNumber);
            _reportService.AddValue(errorMessage,4, rowNumber);
        }

        #endregion
    }
}
