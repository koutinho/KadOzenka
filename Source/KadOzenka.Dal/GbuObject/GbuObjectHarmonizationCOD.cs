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
    /// Гармонизация с использованием справочника ЦОД
    /// </summary>
    public class HarmonizationCOD
    {

	    #region Номера столбцов отчетов

	    private static int KnColumnNumber = 0;
	    private static int ResultColumnNumber = 1;
	    private static int ValueColumnNumber = 2;
	    private static int SourceColumnNUmber = 3;
	    private static int ErrorColumnNumber = 4;

	    #endregion

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
		public static long Run(HarmonizationCODSettings setting)
        {
	        _reportService = new GbuReportService();

	        _reportService.AddHeaders(0, new List<string> { "КН", "Поле в которое производилась запись", "Внесенное значение", "Источник внесенного значения", "Ошибка" });

			locked = new object();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };

            List<ObjectModel.KO.OMCodDictionary> DictionaryItem = new List<ObjectModel.KO.OMCodDictionary>();
            if (setting.IdCodJob != null)
                DictionaryItem = ObjectModel.KO.OMCodDictionary.Where(x => x.IdCodjob == setting.IdCodJob).SelectAll().Execute();

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
                //Parallel.ForEach(Objs.Take(1), options, item => { RunOneUnit(item, setting, DictionaryItem); });
                Parallel.ForEach(Objs, options, item => { RunOneUnit(item, setting, DictionaryItem); });
                CurrentCount = 0;
                MaxCount = 0;
            }
            else
            {
                List<ObjectModel.Gbu.OMMainObject> Objs = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == setting.PropertyType && x.IsActive == true).SelectAll().Execute();
                MaxCount = Objs.Count;
                CurrentCount = 0;
                ////TODO для тестирования
                //Parallel.ForEach(Objs.Take(1), options, item => { RunOneGbu(item, setting, DictionaryItem); });
                Parallel.ForEach(Objs, options, item => { RunOneGbu(item, setting, DictionaryItem); });
                CurrentCount = 0;
                MaxCount = 0;
            }

            if (_reportService == null)
            {
				throw new Exception("Сервис отчетов не создан");
            }
            _reportService.SetStyle();
            _reportService.SetIndividualWidth(KnColumnNumber, 4);
			_reportService.SetIndividualWidth(ResultColumnNumber, 6);
			_reportService.SetIndividualWidth(ValueColumnNumber, 3);
            _reportService.SetIndividualWidth(SourceColumnNUmber, 6);
            _reportService.SetIndividualWidth(ErrorColumnNumber, 5);

            var reportId = _reportService.SaveReport("Отчет гармонизации ЦОД");
            _reportService = null;

            return reportId;
        }


        #region Support Methods

        private static void RunOneGbu(ObjectModel.Gbu.OMMainObject obj, HarmonizationCODSettings setting, List<ObjectModel.KO.OMCodDictionary> dictionaryItem)
        {
            var dt = setting.DateActual ?? DateTime.Now;
            RunOneItem(obj.CadastralNumber, obj.Id, dt, setting, dictionaryItem);
        }

        private static void RunOneUnit(ObjectModel.KO.OMUnit unit, HarmonizationCODSettings setting, List<ObjectModel.KO.OMCodDictionary> dictionaryItem)
        {
            RunOneItem(unit.CadastralNumber, unit.ObjectId, unit.CreationDate, setting, dictionaryItem);
        }

        private static void RunOneItem(string cadastralNumber, long? objectId, DateTime? date, HarmonizationCODSettings setting, List<ObjectModel.KO.OMCodDictionary> dictionaryItem)
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
                var isDataSaved = SaveLevelData(cadastralNumber, objectId.Value, attribute, setting.IdAttributeResult, attributes, dictionaryItem);
                if (isDataSaved)
                    return;
            }

            SaveFailResult(cadastralNumber, objectId, date, setting);
        }

        private static List<long?> GetAllLevelsAttributeIds(HarmonizationCODSettings setting)
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

        public static bool SaveLevelData(string cadastralNumber, long objectId, long? sourceAttributeId, long? resultAttributeId, List<GbuObjectAttribute> allAttributes, List<ObjectModel.KO.OMCodDictionary> dictionaryItems)
        {
            if (sourceAttributeId == null || resultAttributeId == null)
                return false;

            var sourceAttribute = allAttributes.Find(x => x.AttributeId == sourceAttributeId.Value);
            if (sourceAttribute == null)
                return false; 

            var sourceAttributeValueInString = sourceAttribute.GetValueInString();
            if (string.IsNullOrEmpty(sourceAttributeValueInString))
                return false;

            var resValue = string.Empty;
            var dictionaryRecord = dictionaryItems.Find(x => x.Value == sourceAttributeValueInString);
            if (dictionaryRecord != null)
            {
                var code = dictionaryRecord.Code.Replace(" ", "");
                if (code != "0")
                {
                    resValue = code;
                }
            }
            if (resValue == string.Empty)
                return false;

            lock (locked)
            {
                var rowReport = _reportService.GetCurrentRow();
                AddRowToReport(rowReport, cadastralNumber, sourceAttributeId.Value, resValue, resultAttributeId.Value, string.Empty);
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
                StringValue = resValue
            };

            attributeValue.Save();

            return true;
        }

        private static void SaveFailResult(string cadastralNumber, long? objectId, DateTime? date,
            HarmonizationCODSettings setting)
        {
            lock (locked)
            {
                var rowReport = _reportService.GetCurrentRow();
                var message = "Для текущего объекта было установлено значение по умолчанию.";
                AddRowToReport(rowReport, cadastralNumber, 0, setting.DefaultValue, setting.IdAttributeResult.Value, message);
            }

            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = setting.IdAttributeResult.Value,
                ObjectId = objectId.Value,
                ChangeDocId = setting.IdDocument ?? -1,
                S = date ?? DateTime.Now,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = date ?? DateTime.Now,
                StringValue = setting.DefaultValue
            };

            attributeValue.Save();
        }

        private static void AddRowToReport(int rowNumber, string kn, long sourceAttribute, string value, long resultAttribute, string errorMessage)
        {
            string sourceName = GbuObjectService.GetAttributeNameById(sourceAttribute);
            string resultName = GbuObjectService.GetAttributeNameById(resultAttribute);
            _reportService.AddValue(kn, KnColumnNumber, rowNumber);
            _reportService.AddValue(resultName, ResultColumnNumber, rowNumber);
            _reportService.AddValue(value, ValueColumnNumber, rowNumber);
            _reportService.AddValue(sourceName, SourceColumnNUmber, rowNumber);
            _reportService.AddValue(errorMessage, ErrorColumnNumber, rowNumber);
        }

        #endregion
    }
}
