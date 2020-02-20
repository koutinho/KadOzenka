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
using ObjectModel.Gbu.ExportAttribute;
using ObjectModel.Core.Shared;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Перенос атрибутов из ГБУ в КО
    /// </summary>
    public class ExportAttributeToKO
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
        /// Выполнение операции переноса атрибутов
        /// </summary>
        public static void Run(GbuExportAttributeSettings setting)
        {
            locked = new object();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };

            List<long> lstIds = new List<long>();
            foreach (ExportAttributeItem item in setting.Attributes)
            {
                lstIds.Add(item.IdAttributeGBU);
            }


            if (setting.TaskFilter.Count > 0)
            {
                List<ObjectModel.KO.OMUnit> Objs = new List<ObjectModel.KO.OMUnit>();
                foreach (long taskId in setting.TaskFilter)
                {
                    Objs.AddRange(ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute());
                }
                MaxCount = Objs.Count;
                CurrentCount = 0;
                Parallel.ForEach(Objs, options, item => { RunOneUnit(item, setting, lstIds); });
                CurrentCount = 0;
                MaxCount = 0;
            }
        }

        public static void RunOneUnit(ObjectModel.KO.OMUnit unit, GbuExportAttributeSettings setting, List<long> lstIds)
        {
            lock (locked)
            {
                CurrentCount++;
            }
            List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(unit.ObjectId.Value, null, lstIds, unit.CreationDate);

            foreach (GbuObjectAttribute attrib in attribs)
            {
                ExportAttributeItem current = setting.Attributes.Find(x => x.IdAttributeGBU == attrib.AttributeId);
                //if (current != null)
                {
                    long id_factor = current.IdAttributeKO;
                    long RegId = id_factor / 100000;
                    object value = attrib.StringValue;


                    RegisterObject registerObject = new RegisterObject((int)RegId, (int)unit.Id);
                    var attributeData = RegisterCache.GetAttributeData((int)id_factor);
                    int referenceItemId = -1;
                    if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
                    {
                        OMReferenceItem item = OMReferenceItem.Where(x => x.ReferenceId == attributeData.ReferenceId && x.Value == value.ToString()).ExecuteFirstOrDefault();
                        if (item != null) referenceItemId = (int)item.ItemId;
                    }

                    switch (attributeData.Type)
                    {
                        case RegisterAttributeType.INTEGER:
                            value = value.ParseToLongNullable();
                            break;
                        case RegisterAttributeType.DECIMAL:
                            value = value.ParseToDecimalNullable();
                            break;
                        case RegisterAttributeType.BOOLEAN:
                            value = value.ParseToBooleanNullable();
                            break;
                        case RegisterAttributeType.STRING:
                            value = value.ToString();
                            break;
                        case RegisterAttributeType.DATE:
                            value = value.ParseToDateTimeNullable();
                            break;
                    }
                    registerObject.SetAttributeValue((int)id_factor, value, referenceItemId);
                    RegisterStorage.Save(registerObject);
                }
            }



        }

    }

}
