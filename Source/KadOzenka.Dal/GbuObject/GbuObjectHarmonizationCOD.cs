using System;
using System.Collections.Generic;
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
                Parallel.ForEach(Objs, options, item => { RunOneUnit(item, setting, DictionaryItem); });
                CurrentCount = 0;
                MaxCount = 0;
            }
            else
            {
                List<ObjectModel.Gbu.OMMainObject> Objs = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == setting.PropertyType && x.IsActive == true).SelectAll().Execute();
                MaxCount = Objs.Count;
                CurrentCount = 0;
                Parallel.ForEach(Objs, options, item => { RunOneGbu(item, setting, DictionaryItem); });
                CurrentCount = 0;
                MaxCount = 0;
            }

            _reportService.SetStyle();
            _reportService.SetIndividualWidth(1, 6);
            _reportService.SetIndividualWidth(0, 4);
            _reportService.SetIndividualWidth(2, 3);
            _reportService.SetIndividualWidth(3, 6);
            _reportService.SetIndividualWidth(4, 5);
            long reportId = _reportService.SaveReport("Отчет гормонизации ЦОД");
            _reportService = null;

            return reportId;

        }

        public static void RunOneGbu(ObjectModel.Gbu.OMMainObject obj, HarmonizationCODSettings setting, List<ObjectModel.KO.OMCodDictionary> dictionaryItem)
        {
            lock (locked)
            {
                CurrentCount++;
            }

            DateTime dt = (setting.DateActual == null) ? DateTime.Now : setting.DateActual.Value;
            List<long> lstIds = new List<long>();
            if (setting.Level1Attribute != null) lstIds.Add(setting.Level1Attribute.Value);
            if (setting.Level2Attribute != null) lstIds.Add(setting.Level2Attribute.Value);
            if (setting.Level3Attribute != null) lstIds.Add(setting.Level3Attribute.Value);
            if (setting.Level4Attribute != null) lstIds.Add(setting.Level4Attribute.Value);
            if (setting.Level5Attribute != null) lstIds.Add(setting.Level5Attribute.Value);
            if (setting.Level6Attribute != null) lstIds.Add(setting.Level6Attribute.Value);
            if (setting.Level7Attribute != null) lstIds.Add(setting.Level7Attribute.Value);
            if (setting.Level8Attribute != null) lstIds.Add(setting.Level8Attribute.Value);
            if (setting.Level9Attribute != null) lstIds.Add(setting.Level9Attribute.Value);
            if (setting.Level10Attribute != null) lstIds.Add(setting.Level10Attribute.Value);

            List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(obj.Id, null, lstIds, dt);

            if (!GetLevelData(obj, setting.Level1Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                if (!GetLevelData(obj, setting.Level2Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                    if (!GetLevelData(obj, setting.Level3Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                        if (!GetLevelData(obj, setting.Level4Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                            if (!GetLevelData(obj, setting.Level5Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                if (!GetLevelData(obj, setting.Level6Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                    if (!GetLevelData(obj, setting.Level7Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                        if (!GetLevelData(obj, setting.Level8Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                            if (!GetLevelData(obj, setting.Level9Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                                if (!GetLevelData(obj, setting.Level10Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                                {
	                                                int rowReport;

	                                                lock (locked)
	                                                {
		                                                rowReport = _reportService.GetCurrentRow();
	                                                }

	                                                string message = "Для текущего объекта было установлено значение по умолчанию.";
	                                                AddRowToReport(rowReport,obj.CadastralNumber, 0, setting.DefaultValue, setting.IdAttributeResult.Value, message);

													var attributeValue = new GbuObjectAttribute
                                                    {
                                                        Id = -1,
                                                        AttributeId = setting.IdAttributeResult.Value,
                                                        ObjectId = obj.Id,
                                                        ChangeDocId = (setting.IdDocument==null)?-1: setting.IdDocument.Value,
                                                        S = dt,
                                                        ChangeUserId = SRDSession.Current.UserID,
                                                        ChangeDate = DateTime.Now,
                                                        Ot = dt,
                                                        StringValue=setting.DefaultValue
                                                    };
                                                    attributeValue.Save();

                                                }
        }
        public static void RunOneUnit(ObjectModel.KO.OMUnit unit, HarmonizationCODSettings setting, List<ObjectModel.KO.OMCodDictionary> dictionaryItem)
        {
            lock (locked)
            {
                CurrentCount++;
            }
            if (unit.ObjectId != null)
            {

                List<long> lstIds = new List<long>();
                if (setting.Level1Attribute != null) lstIds.Add(setting.Level1Attribute.Value);
                if (setting.Level2Attribute != null) lstIds.Add(setting.Level2Attribute.Value);
                if (setting.Level3Attribute != null) lstIds.Add(setting.Level3Attribute.Value);
                if (setting.Level4Attribute != null) lstIds.Add(setting.Level4Attribute.Value);
                if (setting.Level5Attribute != null) lstIds.Add(setting.Level5Attribute.Value);
                if (setting.Level6Attribute != null) lstIds.Add(setting.Level6Attribute.Value);
                if (setting.Level7Attribute != null) lstIds.Add(setting.Level7Attribute.Value);
                if (setting.Level8Attribute != null) lstIds.Add(setting.Level8Attribute.Value);
                if (setting.Level9Attribute != null) lstIds.Add(setting.Level9Attribute.Value);
                if (setting.Level10Attribute != null) lstIds.Add(setting.Level10Attribute.Value);

                List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(unit.ObjectId.Value, null, lstIds, unit.CreationDate);

                if (!GetLevelData(unit, setting.Level1Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                    if (!GetLevelData(unit, setting.Level2Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                        if (!GetLevelData(unit, setting.Level3Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                            if (!GetLevelData(unit, setting.Level4Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                if (!GetLevelData(unit, setting.Level5Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                    if (!GetLevelData(unit, setting.Level6Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                        if (!GetLevelData(unit, setting.Level7Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                            if (!GetLevelData(unit, setting.Level8Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                                if (!GetLevelData(unit, setting.Level9Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                                    if (!GetLevelData(unit, setting.Level10Attribute, setting.IdAttributeResult, attribs, dictionaryItem))
                                                    {
	                                                    int rowReport;

	                                                    lock (locked)
	                                                    {
		                                                    rowReport = _reportService.GetCurrentRow();
	                                                    }

	                                                    string message = "Для текущего объекта было установлено значение по умолчанию.";
	                                                    AddRowToReport(rowReport, unit.CadastralNumber, 0, setting.DefaultValue, setting.IdAttributeResult.Value, message);

														var attributeValue = new GbuObjectAttribute
                                                        {
                                                            Id = -1,
                                                            AttributeId = setting.IdAttributeResult.Value,
                                                            ObjectId = unit.ObjectId.Value,
                                                            ChangeDocId = (setting.IdDocument == null) ? -1 : setting.IdDocument.Value,
                                                            S = unit.CreationDate.Value,
                                                            ChangeUserId = SRDSession.Current.UserID,
                                                            ChangeDate = DateTime.Now,
                                                            Ot = unit.CreationDate.Value,
                                                            StringValue = setting.DefaultValue
                                                        };
                                                        attributeValue.Save();

                                                    }
            }
        }

        public static bool GetLevelData(ObjectModel.Gbu.OMMainObject obj, long? idSourceAttrib, long? idResultAttrib, List<GbuObjectAttribute> attribs, List<ObjectModel.KO.OMCodDictionary> dictionaryItem)
        {
            bool res = false;
            if (idSourceAttrib != null && idResultAttrib != null)
            {
                GbuObjectAttribute attrib = attribs.Find(x => x.AttributeId == idSourceAttrib.Value);
                if (attrib != null)
                {
                    string resValue = string.Empty;
                    if (attrib.GetValueInString() != string.Empty && attrib.GetValueInString() != null)
                    {
                        ObjectModel.KO.OMCodDictionary dictionaryRecord = dictionaryItem.Find(x => x.Value == attrib.GetValueInString());
                        if (dictionaryRecord != null)
                        {
                            string code = dictionaryRecord.Code.Replace(" ", "");
                            if (code != "0")
                            {
                                resValue = code;
                            }
                        }
                        if (resValue != string.Empty)
                        {
	                        int rowReport;

	                        lock (locked)
	                        {
		                        rowReport = _reportService.GetCurrentRow();
	                        }

	                        AddRowToReport(rowReport, obj.CadastralNumber, idSourceAttrib.Value, resValue, idResultAttrib.Value, "");
							res = true;
                            var attributeValue = new GbuObjectAttribute
                            {
                                Id = -1,
                                AttributeId = idResultAttrib.Value,
                                ObjectId = obj.Id,
                                ChangeDocId = attrib.ChangeDocId,
                                S = attrib.S,
                                ChangeUserId = SRDSession.Current.UserID,
                                ChangeDate = DateTime.Now,
                                Ot = attrib.Ot,
                                StringValue = resValue,
                            };
                            attributeValue.Save();
                        }
                    }
                }
            }
            return res;
        }
        public static bool GetLevelData(ObjectModel.KO.OMUnit unit, long? idSourceAttrib, long? idResultAttrib, List<GbuObjectAttribute> attribs, List<ObjectModel.KO.OMCodDictionary> dictionaryItem)
        {
            bool res = false;
            if (idSourceAttrib != null && idResultAttrib != null)
            {
                GbuObjectAttribute attrib = attribs.Find(x => x.AttributeId == idSourceAttrib.Value);
                if (attrib != null)
                {
                    string resValue = string.Empty;
                    if (attrib.GetValueInString() != string.Empty && attrib.GetValueInString() != null)
                    {
                        ObjectModel.KO.OMCodDictionary dictionaryRecord = dictionaryItem.Find(x => x.Value == attrib.GetValueInString());
                        if (dictionaryRecord != null)
                        {
                            string code = dictionaryRecord.Code.Replace(" ", "");
                            if (code != "0")
                            {
                                resValue = code;
                            }
                        }
                        if (resValue != string.Empty)
                        {
	                        int rowReport;
	                        lock (locked)
	                        {
		                        rowReport = _reportService.GetCurrentRow();
	                        }
	                        AddRowToReport(rowReport, unit.CadastralNumber, idSourceAttrib.Value, attrib.GetValueInString(), idResultAttrib.Value, "");

							res = true;
                            var attributeValue = new GbuObjectAttribute
                            {
                                Id = -1,
                                AttributeId = idResultAttrib.Value,
                                ObjectId = unit.ObjectId.Value,
                                ChangeDocId = attrib.ChangeDocId,
                                S = attrib.S,
                                ChangeUserId = SRDSession.Current.UserID,
                                ChangeDate = DateTime.Now,
                                Ot = attrib.Ot,
                                StringValue = resValue,
                            };
                            attributeValue.Save();
                        }
                    }
                }
            }
            return res;
        }

        public static void AddRowToReport(int rowNumber, string kn, long sourceAttribute, string value, long resultAttribute, string errorMessage)
        {
	        string sourceName = GbuObjectService.GetAttributeNameById(sourceAttribute);
	        string resultName = GbuObjectService.GetAttributeNameById(resultAttribute);
	        _reportService.AddValue(kn, 0, rowNumber);
	        _reportService.AddValue(sourceName, 3, rowNumber);
	        _reportService.AddValue(value, 2, rowNumber);
	        _reportService.AddValue(resultName, 1, rowNumber);
	        _reportService.AddValue(errorMessage, 4, rowNumber);
        }

	}

}
