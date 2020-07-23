using System.Collections.Generic;
using Core.Register;
using Core.Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
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
        public static void Run(GbuExportAttributeSettings setting, OMQueue processQueue)
        {
            locked = new object();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };
            //если используется "Перенос с созданием атрибутов", нужно обновить кеш, т.к. нового атрибута может не быть в нем
            RegisterCache.UpdateCache(0, null);

            List<long> lstIds = new List<long>();
            foreach (ExportAttributeItem item in setting.Attributes)
            {
                lstIds.Add(item.IdAttributeGBU);
            }
            WorkerCommon.LogState(processQueue, $"Найдено {setting.Attributes.Count} пар атрибутов.");
            WorkerCommon.LogState(processQueue, $"Найдено {setting.TaskFilter.Count} заданий на оценку.");

            if (setting.TaskFilter.Count > 0)
            {
                List<ObjectModel.KO.OMUnit> Objs = new List<ObjectModel.KO.OMUnit>();
                foreach (long taskId in setting.TaskFilter)
                {
                    Objs.AddRange(ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute());
                }
                MaxCount = Objs.Count;
                CurrentCount = 0;
                WorkerCommon.LogState(processQueue, $"Найдено {Objs.Count} единиц оценки.");
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
	                var attributeData = RegisterCache.GetAttributeData((int)current.IdAttributeKO);
					long id_factor = current.IdAttributeKO;
                    long RegId = attributeData.RegisterId;
                    object value = attrib.GetValueInString();


                    RegisterObject registerObject = new RegisterObject((int)RegId, (int)unit.Id);
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
