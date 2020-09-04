using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using ObjectModel.Gbu.InheritanceAttribute;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Наследование атрибутов ГБУ по объектам
    /// </summary>
    public class GbuObjectInheritanceAttribute
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
        /// Выполнение операции наследования атрибутов
        /// </summary>
        public static long Run(GbuInheritanceAttributeSettings setting)
        {
			var reportService = new GbuReportService();
			reportService.AddHeaders(0, new List<string>{ "КН", "КН наследуемого объекта", "Имя наследуемого атрибута", "Значение атрибута", "Ошибка" });

            locked = new object();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };

            if (setting.TaskFilter.Count > 0)
            {
                List<ObjectModel.KO.OMUnit> Objs = new List<ObjectModel.KO.OMUnit>();
                foreach (long taskId in setting.TaskFilter)
                {
                    Objs.AddRange(ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute());
                }
                MaxCount = Objs.Count;
                CurrentCount = 0;
                Parallel.ForEach(Objs, options, item => { RunOneUnit(item, setting, reportService); });
                CurrentCount = 0;
                MaxCount = 0;
            }

            reportService.SetStyle();
            reportService.SetIndividualWidth(1, 4);
            reportService.SetIndividualWidth(0, 4);
            reportService.SetIndividualWidth(2, 6);
            reportService.SetIndividualWidth(3, 4);
            reportService.SetIndividualWidth(4, 6);
            long reportId = reportService.SaveReport("Отчет наследование");
            return reportId;
        }

        public static void Inheritance(ObjectModel.KO.OMUnit unit, GbuInheritanceAttributeSettings setting, ObjectModel.Directory.PropertyTypes typecode, GbuReportService reportService)
        {
            List<long> lstIds = new List<long>();
            lstIds.Add(setting.ParentCadastralNumberAttribute);
            List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(unit.ObjectId.Value, null, lstIds, unit.CreationDate);
            if (attribs.Count > 0)
            {
                ObjectModel.Gbu.OMMainObject parent = ObjectModel.Gbu.OMMainObject.Where(x => x.CadastralNumber == attribs[0].StringValue && x.ObjectType_Code == typecode).SelectAll().ExecuteFirstOrDefault();
                if (parent != null)
                {
                    List<long> lstPIds = new List<long>();
                    if (setting.Attributes != null)
                    {
                        foreach (long id in setting.Attributes)
                        {
                            if (id > 0) lstPIds.Add(id);
                        }
                    }
                    List<GbuObjectAttribute> pattribs = new GbuObjectService().GetAllAttributes(parent.Id, null, lstPIds, unit.CreationDate);

					List<int> rowsReport = new List<int>();
                    if (pattribs.Count > 0)
                    {
	                    lock (locked)
	                    {
		                    rowsReport = reportService.GetRangeRows(pattribs.Count);
	                    }
                    }

                    int counter = 0;
                    foreach (GbuObjectAttribute pattrib in pattribs)
                    {
                        var attributeValue = new GbuObjectAttribute
                        {
                            Id = -1,
                            AttributeId = pattrib.AttributeId,
                            ObjectId = unit.ObjectId.Value,
                            ChangeDocId = pattrib.ChangeDocId,
                            S = pattrib.S,
                            ChangeUserId = SRDSession.Current.UserID,
                            ChangeDate = DateTime.Now,
                            Ot = pattrib.Ot,
                            StringValue = pattrib.StringValue,
                        };
                        attributeValue.Save();

                        lock (locked)
                        {
                            if (rowsReport != null && rowsReport.Count >= counter)
                            {
                                AddRowToReport(rowsReport[counter], unit.CadastralNumber, attribs[0].StringValue,
                                    pattrib.AttributeId, pattrib.StringValue, "", reportService);
                                counter++;
                            }
                        }
                    }
                }
                else
                {
                    lock (locked)
					{
						var rowReport = reportService.GetCurrentRow();
                        reportService.AddValue(unit.CadastralNumber, 0, rowReport);
                        reportService.AddValue($"Не найден объект по кадастровому номеру {attribs[0].StringValue}", 4, rowReport);
                    }
                }
            }
            else
            {
                lock (locked)
	            {
		            var rowReport = reportService.GetCurrentRow();
                    reportService.AddValue(unit.CadastralNumber, 0, rowReport);
                    reportService.AddValue("Не найдено значение родительского кадастрового номера", 4, rowReport);
                }
            }
        }
        public static void RunOneUnit(ObjectModel.KO.OMUnit unit, GbuInheritanceAttributeSettings setting, GbuReportService reportService)
        {
            lock (locked)
            {
                CurrentCount++;
            }

            //Тип наследования: Здание -> Помещение
            if (setting.BuildToFlat && unit.PropertyType_Code==ObjectModel.Directory.PropertyTypes.Pllacement)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Building, reportService);
            }
            //Тип наследования: Земельный участок -> Объект незавершенного строительства
            if (setting.ParcelToUncomplited && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.UncompletedBuilding)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Stead, reportService);
            }
            //Тип наследования: Земельный участок -> Сооружение
            if (setting.ParcelToConstruction && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Construction)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Stead, reportService);
            }
            //Тип наследования: Земельный участок -> Здание
            if (setting.ParcelToBuilding && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Building)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Stead, reportService);
            }
            //Тип наследования: Кадастровый квартал -> Объект незавершенного строительства
            if (setting.CadastralBlockToUncomplited && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.UncompletedBuilding)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.CadastralQuartal, reportService);
            }
            //Тип наследования: Кадастровый квартал -> Сооружение
            if (setting.CadastralBlockToConstruction && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Construction)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.CadastralQuartal, reportService);
            }
            //Тип наследования: Кадастровый квартал -> Здание
            if (setting.CadastralBlockToBuilding && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Building)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.CadastralQuartal, reportService);
            }
            //Тип наследования: Кадастровый квартал -> Земельный участок
            if (setting.CadastralBlockToParcel && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Stead)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.CadastralQuartal, reportService);
            }
        }

        public static void AddRowToReport(int rowNumber, string kn, string knInh, long sourceAttribute, string value,  string errorMessage, GbuReportService reportService)
        {
	        string sourceName = GbuObjectService.GetAttributeNameById(sourceAttribute);
	        reportService.AddValue(kn, 0, rowNumber);
	        reportService.AddValue(knInh, 1, rowNumber);
	        reportService.AddValue(sourceName, 2, rowNumber);
			reportService.AddValue(value, 3, rowNumber);
			reportService.AddValue(errorMessage, 4, rowNumber);
        }
    }
}
