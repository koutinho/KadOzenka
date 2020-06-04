﻿using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Выполнение операции гармонизации
        /// </summary>

        private static GbuReportService _reportService;

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
                Parallel.ForEach(Objs, options, item => { RunOneUnit(item, setting); });
                CurrentCount = 0;  
                MaxCount = 0;
            }
            else
            {
                List<ObjectModel.Gbu.OMMainObject> Objs = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == setting.PropertyType && x.IsActive == true).SelectAll().Execute();
                MaxCount = Objs.Count;
                CurrentCount = 0;
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
            long reportId = _reportService.SaveReport("Отчет гормонизации");
			_reportService = null;

			return reportId;
        }

        public static bool GetLevelData(ObjectModel.Gbu.OMMainObject obj, long? idSourceAttrib, long? idResultAttrib, List<GbuObjectAttribute> attribs)
        {
            bool res = false;
            if (idSourceAttrib != null && idResultAttrib != null)
            {
                GbuObjectAttribute attrib = attribs.Find(x => x.AttributeId == idSourceAttrib.Value);
                if (attrib != null)
                {
                    if (attrib.GetValueInString() != string.Empty && attrib.GetValueInString() != null)
                    {
	                    int rowReport;
	                    lock (locked)
	                    {
		                    rowReport = _reportService.GetCurrentRow();
	                    }
	                    AddRowToReport(rowReport, obj.CadastralNumber, idSourceAttrib.Value, attrib.GetValueInString(), idResultAttrib.Value, "");
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
                            StringValue = attrib.GetValueInString(),
                        };
                        attributeValue.Save();
                    }
                }
            }
            return res;
        }
        public static bool GetLevelData(ObjectModel.KO.OMUnit obj, long? idSourceAttrib, long? idResultAttrib, List<GbuObjectAttribute> attribs)
        {
			bool  res = false;
            if (idSourceAttrib != null && idResultAttrib != null)
            {
                GbuObjectAttribute attrib = attribs.Find(x => x.AttributeId == idSourceAttrib.Value);
                if (attrib != null)
                {
                    if (attrib.GetValueInString() != string.Empty && attrib.GetValueInString() != null)
                    {
	                    int rowReport;
	                    lock (locked)
	                    {
							rowReport = _reportService.GetCurrentRow();
						}
						AddRowToReport(rowReport, obj.CadastralNumber, idSourceAttrib.Value, attrib.GetValueInString(), idResultAttrib.Value, "");
                        res = true;
                        var attributeValue = new GbuObjectAttribute
                        {
                            Id = -1,
                            AttributeId = idResultAttrib.Value,
                            ObjectId = obj.ObjectId.Value,
                            ChangeDocId = attrib.ChangeDocId,
                            S = attrib.S,
                            ChangeUserId = SRDSession.Current.UserID,
                            ChangeDate = DateTime.Now,
                            Ot = attrib.Ot,
                            StringValue = attrib.GetValueInString(),
                        };
                        attributeValue.Save();
                    }
                }
            }
            return res;
        }

        public static void RunOneGbu(ObjectModel.Gbu.OMMainObject obj, HarmonizationSettings setting)
        {
            lock (locked)
            {
                CurrentCount++;
            }
            DateTime dt = (setting.DateActual==null)?DateTime.Now:setting.DateActual.Value;
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

            if (!GetLevelData(obj, setting.Level1Attribute, setting.IdAttributeResult, attribs))
                if (!GetLevelData(obj, setting.Level2Attribute, setting.IdAttributeResult, attribs))
                    if (!GetLevelData(obj, setting.Level3Attribute, setting.IdAttributeResult, attribs))
                        if (!GetLevelData(obj, setting.Level4Attribute, setting.IdAttributeResult, attribs))
                            if (!GetLevelData(obj, setting.Level5Attribute, setting.IdAttributeResult, attribs))
                                if (!GetLevelData(obj, setting.Level6Attribute, setting.IdAttributeResult, attribs))
                                    if (!GetLevelData(obj, setting.Level7Attribute, setting.IdAttributeResult, attribs))
                                        if (!GetLevelData(obj, setting.Level8Attribute, setting.IdAttributeResult, attribs))
                                            if (!GetLevelData(obj, setting.Level9Attribute, setting.IdAttributeResult, attribs))
                                                if (!GetLevelData(obj, setting.Level10Attribute, setting.IdAttributeResult, attribs))
                                                {
	                                                int rowReport;

	                                                lock (locked)
	                                                {
		                                                rowReport = _reportService.GetCurrentRow();
	                                                }

	                                                string message = "Для текущего объекта не было записанно значение, т.к не было найдено.";
	                                                AddRowToReport(rowReport, obj.CadastralNumber, 0, "", setting.IdAttributeResult.Value, message);

													var attributeValue = new GbuObjectAttribute
                                                    {
                                                        Id = -1,
                                                        AttributeId = setting.IdAttributeResult.Value,
                                                        ObjectId = obj.Id,
                                                        ChangeDocId = -1,
                                                        S = dt,
                                                        ChangeUserId = SRDSession.Current.UserID,
                                                        ChangeDate = DateTime.Now,
                                                        Ot = dt,
                                                    };
                                                    attributeValue.Save();

                                                }
        }
        public static void RunOneUnit(ObjectModel.KO.OMUnit obj, HarmonizationSettings setting)
        {
            lock (locked)
            {
                CurrentCount++;
            }

            if (obj.ObjectId != null)
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

                List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(obj.ObjectId.Value, null, lstIds, obj.CreationDate);

                if (!GetLevelData(obj, setting.Level1Attribute, setting.IdAttributeResult, attribs))
                    if (!GetLevelData(obj, setting.Level2Attribute, setting.IdAttributeResult, attribs))
                        if (!GetLevelData(obj, setting.Level3Attribute, setting.IdAttributeResult, attribs))
                            if (!GetLevelData(obj, setting.Level4Attribute, setting.IdAttributeResult, attribs))
                                if (!GetLevelData(obj, setting.Level5Attribute, setting.IdAttributeResult, attribs))
                                    if (!GetLevelData(obj, setting.Level6Attribute, setting.IdAttributeResult, attribs))
                                        if (!GetLevelData(obj, setting.Level7Attribute, setting.IdAttributeResult, attribs))
                                            if (!GetLevelData(obj, setting.Level8Attribute, setting.IdAttributeResult, attribs))
                                                if (!GetLevelData(obj, setting.Level9Attribute, setting.IdAttributeResult, attribs))
                                                    if (!GetLevelData(obj, setting.Level10Attribute, setting.IdAttributeResult, attribs))
                                                    {
	                                                    int rowReport;

														lock (locked)
	                                                    {
		                                                    rowReport = _reportService.GetCurrentRow();
														}
	                                                   
	                                                    string message = "Для текущего объекта не было записанно значение, т.к не было найдено.";
	                                                    AddRowToReport(rowReport, obj.CadastralNumber, 0, "", setting.IdAttributeResult.Value, message);
														var attributeValue = new GbuObjectAttribute
                                                        {
                                                            Id = -1,
                                                            AttributeId = setting.IdAttributeResult.Value,
                                                            ObjectId = obj.ObjectId.Value,
                                                            ChangeDocId = -1,
                                                            S = obj.CreationDate.Value,
                                                            ChangeUserId = SRDSession.Current.UserID,
                                                            ChangeDate = DateTime.Now,
                                                            Ot = obj.CreationDate.Value,
                                                        };
                                                        attributeValue.Save();

                                                    }
            }
        }

        public static void AddRowToReport(int rowNumber, string kn, long sourceAttribute, string value, long resultAttribute, string errorMessage)
        {
	        string sourceName = GbuObjectService.GetAttributeNameById(sourceAttribute);
	        string resultName = GbuObjectService.GetAttributeNameById(resultAttribute);
			_reportService.AddValue(kn,0, rowNumber);
			_reportService.AddValue(sourceName,1, rowNumber);
			_reportService.AddValue(value,2, rowNumber);
			_reportService.AddValue(resultName,3, rowNumber);
			_reportService.AddValue(errorMessage,4, rowNumber);
        }

    }

}
