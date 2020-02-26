using System;
using System.Text;
using System.Data;
using System.Globalization;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

using Core.Register;
using Core.Numerator;
using Core.Shared.Misc;
using Core.Shared.Extensions;
using Core.Register.RegisterEntities;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using ObjectModel.Gbu.InheritanceAttribute;
using ObjectModel.Core.Shared;

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
        public static void Run(GbuInheritanceAttributeSettings setting)
        {
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
                Parallel.ForEach(Objs, options, item => { RunOneUnit(item, setting); });
                CurrentCount = 0;
                MaxCount = 0;
            }
        }

        public static void Inheritance(ObjectModel.KO.OMUnit unit, GbuInheritanceAttributeSettings setting, ObjectModel.Directory.PropertyTypes typecode)
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
                    }
                }
            }
        }
        public static void RunOneUnit(ObjectModel.KO.OMUnit unit, GbuInheritanceAttributeSettings setting)
        {
            lock (locked)
            {
                CurrentCount++;
            }

            //Тип наследования: Здание -> Помещение
            if (setting.BuildToFlat && unit.PropertyType_Code==ObjectModel.Directory.PropertyTypes.Pllacement)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Building);
            }
            //Тип наследования: Земельный участок -> Объект незавершенного строительства
            if (setting.ParcelToUncomplited && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.UncompletedBuilding)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Stead);
            }
            //Тип наследования: Земельный участок -> Сооружение
            if (setting.ParcelToConstruction && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Construction)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Stead);
            }
            //Тип наследования: Земельный участок -> Здание
            if (setting.ParcelToBuilding && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Building)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Stead);
            }
            //Тип наследования: Кадастровый квартал -> Объект незавершенного строительства
            if (setting.CadastralBlockToUncomplited && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.UncompletedBuilding)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Other);
            }
            //Тип наследования: Кадастровый квартал -> Сооружение
            if (setting.CadastralBlockToConstruction && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Construction)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Other);
            }
            //Тип наследования: Кадастровый квартал -> Здание
            if (setting.CadastralBlockToBuilding && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Building)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Other);
            }
            //Тип наследования: Кадастровый квартал -> Земельный участок
            if (setting.CadastralBlockToParcel && unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Stead)
            {
                Inheritance(unit, setting, ObjectModel.Directory.PropertyTypes.Other);
            }
        }

    }

}
