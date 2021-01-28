using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;

using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public abstract class ImportObjectBase<T> where T : xmlObjectParticular
    {
	    protected static object locked = new object();

        private static readonly ILogger Log = Serilog.Log.ForContext<ImportObjectBase<T>>();

        private Dictionary<long, List<long>> _updatedObjectsAttributes;


        public abstract string ErrorMessage { get; }
        public abstract string CancelMessage { get; }
        public abstract string SuccessMessage { get; }

        public int ImportCounter { get; protected set; }

        public void ImportObjects(List<T> objectsList, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate,
	        DateTime otDate, long idDocument, CancellationToken cancellationToken, Dictionary<long, List<long>> updatedObjectsAttributes)
        {
	        _updatedObjectsAttributes = updatedObjectsAttributes;

            ParallelOptions options = new ParallelOptions
	        {
		        CancellationToken = cancellationToken,
		        MaxDegreeOfParallelism = 10
	        };

            try
	        {
		        Parallel.ForEach(objectsList, options, item =>
		        {
			        try
			        {
				        ImportObjectCurrent(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument);
			        }
			        catch (Exception ex)
			        {
				        var errorId = ErrorManager.LogError(ex);
				        Log.ForContext("ErrorId", errorId).Error(ex, ErrorMessage);
			        }
		        });
	        }
	        catch (OperationCanceledException)
	        {
		        Log.Warning(CancelMessage);
	        }
	        Log.ForContext("TaskId", idTask).Debug(SuccessMessage);
        }

        protected abstract void ImportObject(T current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument);

        public void ImportObjectCurrent(T current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
        {
            ImportObject(current, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument);

            lock (locked)
            {
                ImportCounter++;
            }
        }

        private void SaveAttributeValueWithCheckInternal(GbuObjectAttribute attributeValue)
        {
            SaveAttributeValueWithCheck(attributeValue);

            lock (locked)
            {
                if (!_updatedObjectsAttributes.ContainsKey(attributeValue.ObjectId))
                {
                    _updatedObjectsAttributes[attributeValue.ObjectId] = new List<long>();
                }

                _updatedObjectsAttributes[attributeValue.ObjectId].Add(attributeValue.AttributeId);
            }
        }

        //TODO перенести в общий сервис, например, GbuObjectService
        public void SaveAttributeValueWithCheck(GbuObjectAttribute attributeValue)
        {
            // TODO: разобраться почему перехваченное исключение все равно приводит к "25P02: текущая транзакция прервана, команды до конца блока транзакции игнорируются"
            //try
            //{
            //    attributeValue.Save();
            //}
            //catch (Exception ex)
            //{
            //    ErrorManager.LogError(ex);

            //    var existsAttributeValue = GbuObjectService.CheckExistsValueFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot);

            //    // Проблема не в наличии значения на ту же дату ОТ
            //    if (existsAttributeValue == null) throw ExceptionInitializer.Create("Ошибка сохранения знчения показателя", $"Значение: {attributeValue.SerializeToXml()}", ex);

            //    lock (locked)
            //    {
            //        // Проблема в наличии значения на ту же дату ОТ
            //        attributeValue.Ot = GbuObjectService.GetNextOtFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot);

            //        attributeValue.Save();
            //    }
            //}

            if (GbuObjectService.CheckExistsValueFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot) != null)
            {
                lock (locked)
                {
                    // Проблема в наличии значения на ту же дату ОТ
                    attributeValue.Ot = GbuObjectService.GetNextOtFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot);
                }
            }

            attributeValue.Save();
        }

        public void SetAttributeValue_String(long idAttribute, string value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = DateTime.Now,
                Ot = otDate,
                StringValue = value,
            };

            SaveAttributeValueWithCheckInternal(attributeValue);
        }

        public void SetAttributeValue_Numeric(long idAttribute, decimal? value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = DateTime.Now,
                Ot = otDate,
                NumValue = value,
            };

            SaveAttributeValueWithCheckInternal(attributeValue);
        }

        public void SetAttributeValue_Boolean(long idAttribute, bool value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = DateTime.Now,
                Ot = otDate,
                NumValue = (value ? 1 : 0)
            };

            SaveAttributeValueWithCheckInternal(attributeValue);
        }

        public void SetAttributeValue_Date(long idAttribute, DateTime? value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = DateTime.Now,
                Ot = otDate,
                DtValue = value,
            };

            SaveAttributeValueWithCheckInternal(attributeValue);
        }

        public KoStatusRepeatCalc GetNewtatusRepeatCalc(List<ObjectModel.KO.OMUnit> units)
        {
            KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.New;
            if (units.Count > 0)
            {
                foreach (ObjectModel.KO.OMUnit unit in units)
                {
                    if (unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.Initial || unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.RepeatedInitial)
                        koStatusRepeatCalc = KoStatusRepeatCalc.RepeatedInitial;
                    if (unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.New || unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.Repeated)
                        koStatusRepeatCalc = KoStatusRepeatCalc.Repeated;
                }
            }
            return koStatusRepeatCalc;
        }

        public bool CheckChange(ObjectModel.KO.OMUnit unit, long idAttribute, KoChangeStatus status, List<GbuObjectAttribute> prevAttribs, List<GbuObjectAttribute> curAttribs)
        {
            bool res = true;
            string oldValue = string.Empty;
            string newValue = string.Empty;
            GbuObjectAttribute prevAttrib = prevAttribs.Find(x => x.AttributeId == idAttribute);
            GbuObjectAttribute curAttrib = curAttribs.Find(x => x.AttributeId == idAttribute);
            if (prevAttrib != null) oldValue = prevAttrib.GetValueInString();
            if (curAttrib != null) newValue = curAttrib.GetValueInString();
            if (oldValue != newValue)
            {
                res = false;
                ObjectModel.KO.OMUnitChange unitChange = new ObjectModel.KO.OMUnitChange
                {
                    Id = -1,
                    ChangeStatus_Code = status,
                    NewValue = newValue,
                    OldValue = oldValue,
                    UnitId = unit.Id
                };
                unitChange.Save();
            }
            return res;
        }

        public static void SaveHistoryForNewObject(OMUnit unit)
        {
	        new OMUnitChange
	        {
		        Id = -1,
		        ChangeStatus_Code = KoChangeStatus.NewObjectAddition,
		        UnitId = unit.Id
	        }.Save();
        }
    }
}
