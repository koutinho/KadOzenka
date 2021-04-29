using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.XmlParser;
using Microsoft.CodeAnalysis;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public abstract class ImportObjectBase<T> where T : xmlObjectParticular
    {
	    private static readonly ILogger Log = Serilog.Log.ForContext<ImportObjectBase<T>>();

        public abstract PropertyTypes PropertyType { get; }
        public abstract string ErrorMessage { get; }
        public abstract string CancelMessage { get; }
        public abstract string SuccessMessage { get; }

        protected readonly List<ImportedAttributeGkn> GknDataAttributes;
		protected Dictionary<KoChangeStatus, ImportedAttribute> AttributeChangeStatuses { get; set; }
		protected List<ImportedAttribute> TaskFormingAttributes { get; set; }

		protected DateTime UnitDate { get; }
        protected long IdTour { get; }
        protected OMTask Task { get; }
        protected KoNoteType KoNoteType { get; }
        protected DateTime SDate { get; }
        protected DateTime OtDate { get; }
        protected long IdDocument { get; }

        private Action IncreaseImportedObjectsCountAction { get; }
        private Action<long, long> UpdateObjectsAttributesAction { get; }

		private OMTour Tour { get; }
        private GbuReportService GbuReportService { get; }
        private readonly object _locker;

		protected ImportObjectBase(DateTime unitDate, OMTask task, 
			Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction, 
			List<ImportedAttributeGkn> gknDataAttributes, GbuReportService gbuReportService, object locked)
        {
	        UnitDate = unitDate;
	        IdTour = task.TourId.Value;
	        Task = task;
	        KoNoteType = task.NoteType_Code;
	        SDate = task.EstimationDate.Value;
	        OtDate = task.EstimationDate.Value;
	        IdDocument = task.DocumentId.Value;
	        IncreaseImportedObjectsCountAction = increaseImportedObjectsCountAction;
	        UpdateObjectsAttributesAction = updateObjectsAttributesAction;
	        GknDataAttributes = gknDataAttributes;
	        GbuReportService = gbuReportService;

	        _locker = locked;
			Tour = OMTour.Where(x => x.Id == IdTour).Select(x => x.Year).ExecuteFirstOrDefault();
        }

		public virtual void Init()
		{
			InitAttributeChangeStatusList();
			InitTaskFormingAttributes();
		}

        protected virtual void InitAttributeChangeStatusList()
        {
	        AttributeChangeStatuses = new Dictionary<KoChangeStatus, ImportedAttribute>
	        {
		        {KoChangeStatus.Place, new ImportedAttribute(Consts.LocationAttributeId)},
		        {KoChangeStatus.Adress, new ImportedAttribute(Consts.AddressAttributeId)},
		        {KoChangeStatus.CadastralBlock, new ImportedAttribute(Consts.CadastralQuarterAttributeId)},
	        };
        }

		protected virtual void InitTaskFormingAttributes()
        {
			TaskFormingAttributes = new List<ImportedAttribute> { new ImportedAttribute(Consts.P1GroupAttributeId), new ImportedAttribute(Consts.P2FsAttributeId) };
        }


        public void ImportObjects(List<T> objectsList, CancellationToken cancellationToken)
        {
	        ParallelOptions options = new ParallelOptions
	        {
		        CancellationToken = cancellationToken,
		        MaxDegreeOfParallelism = 10
	        };

            try
	        {
		        // обрабатываем юниты порциями по 1000 для уменьшения числа запросов к бд
                var partitionSize = 1000;
		        var partitionCount = objectsList.Count / partitionSize + 1;

		        var objectPartitions = new Dictionary<int, List<T>>();
		        for (var i = 0; i < partitionCount; i++)
		        {
			        objectPartitions.Add(i, objectsList.Skip(i * partitionSize).Take(partitionSize).ToList());
		        }

		        Parallel.ForEach(objectPartitions, options, item =>
		        {
			        ImportPartition(item.Value, options);
		        });
	        }
	        catch (OperationCanceledException)
	        {
		        Log.Warning(CancelMessage);
		        throw;
	        }
	        Log.ForContext("TaskId", Task.Id).Debug(SuccessMessage);
        }

        private void ImportPartition(List<T> objectsPartition, ParallelOptions options)
        {
	        var cadastralNumbersPartition = objectsPartition.Select(x => x.CadastralNumber).Distinct().ToList();
	        var prevUnits = new List<OMUnit>();
	        var prevUnitTasks = new List<OMTask>();
	        var gbuObjectsByPrevUnits = new List<OMMainObject>();
	        var gbuObjectsByCadastralNumbers = new List<OMMainObject>();
	        var existedUnits = new List<OMUnit>();
	        var existedUnitCosts = new List<OMCostRosreestr>();
	        if (objectsPartition.IsNotEmpty())
	        {
		        prevUnits = OMUnit
			        .Where(x => cadastralNumbersPartition.Contains(x.CadastralNumber) && x.TourId == IdTour)
			        .Select(x => new
			        {
				        x.CadastralNumber, x.ObjectId, x.StatusRepeatCalc_Code, x.StatusResultCalc_Code, x.TaskId, x.TourId,
				        x.ResponseDocId, x.CreationDate, x.CadastralCost, x.PropertyType_Code, x.GroupId
			        })
			        .Execute();
		        if (prevUnits.Count > 0)
		        {
			        var prevGbuObjectIds = prevUnits.Select(x => x.ObjectId).Distinct().ToList();
			        gbuObjectsByPrevUnits = OMMainObject
				        .Where(x => prevGbuObjectIds.Contains(x.Id))
				        .Select(x => new {x.ObjectType_Code, x.CadastralNumber}).Execute();

			        var taskIds = prevUnits.Select(x => x.TaskId).Distinct().ToList();
			        prevUnitTasks = OMTask.Where(x => taskIds.Contains(x.Id))
				        .Select(x => new {x.DocumentId, x.NoteType, x.NoteType_Code}).Execute();
		        }

		        gbuObjectsByCadastralNumbers = OMMainObject
			        .Where(x => cadastralNumbersPartition.Contains(x.CadastralNumber))
			        .Select(x => new {x.ObjectType_Code, x.CadastralNumber}).Execute();

		        if (gbuObjectsByPrevUnits.IsNotEmpty() || gbuObjectsByCadastralNumbers.IsNotEmpty())
		        {
			        var gbuObjectIds = gbuObjectsByPrevUnits.Select(x => (long?) x.Id).Distinct().ToList();
			        gbuObjectIds = gbuObjectsByCadastralNumbers.Select(x => (long?) x.Id).Distinct().ToList();

			        existedUnits = OMUnit
				        .Where(x => gbuObjectIds.Contains(x.ObjectId) && x.TaskId == Task.Id && x.TourId == IdTour)
				        .Select(x => new {x.ObjectId, x.TaskId, x.TourId, x.CreationDate, x.PropertyType_Code, x.GroupId})
				        .Execute();
			        if (existedUnits.IsNotEmpty())
			        {
				        var existedUnitIds = existedUnits.Select(x => x.Id).ToList();
				        existedUnitCosts = OMCostRosreestr
					        .Where(x => existedUnitIds.Contains(x.IdObject))
					        .Select(x => new {x.IdObject}).Execute();
			        }
		        }
	        }

	        Parallel.ForEach(objectsPartition, options, item =>
	        {
		        try
		        {
			        ImportObjectCurrent(item, prevUnits, prevUnitTasks, gbuObjectsByPrevUnits, gbuObjectsByCadastralNumbers, existedUnits, existedUnitCosts);
		        }
		        catch (Exception ex)
		        {
					Log.ForContext("Object", item, destructureObjects: true)
						.Error(ex, ErrorMessage + ". Полная информация об объекте {CadastralNumber}", item.CadastralNumber);

			        lock(_locker)
			        {
						var reportRow = GbuReportService.GetCurrentRow();
						GbuReportService.AddValue(item.CadastralNumber, BaseImporter.CadastralNumberColumnIndex, reportRow);
						GbuReportService.AddValue(ex.Message, BaseImporter.ErrorMessageColumnIndex, reportRow);
			        }
		        }
	        });
        }

        public void ImportObjectCurrent(T current, List<OMUnit> prevUnits, List<OMTask> prevUnitTasks,
            List<OMMainObject> gbuObjectsByPrevUnits, List<OMMainObject> gbuObjectsByCadastralNumbers, List<OMUnit> existedUnits, List<OMCostRosreestr> existedUnitCosts)
        {
            using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Required))
            {
                KoUnitStatus koUnitStatus = KoUnitStatus.New;
                KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.New;
                switch (KoNoteType)
                {
                    case KoNoteType.Day:
                        koUnitStatus = KoUnitStatus.New;
                        break;
                    case KoNoteType.Petition:
                        koUnitStatus = KoUnitStatus.New;
                        break;
                    case KoNoteType.Year:
                        koUnitStatus = KoUnitStatus.Annual;
                        break;
                    case KoNoteType.Initial:
                        koUnitStatus = KoUnitStatus.Initial;
                        koStatusRepeatCalc = KoStatusRepeatCalc.Initial;
                        break;
                }

                #region Получение данных о прошлой оценке данного объекта
                List<OMUnit> prev = prevUnits.Where(x => x.CadastralNumber == current.CadastralNumber).ToList();
                #endregion

                //Если данные о прошлой оценке найдены
                if (prev.Count > 0)
                {
	                OMMainObject gbuObject = gbuObjectsByPrevUnits.FirstOrDefault(x => x.Id == prev[0].ObjectId);
                    var koUnit = SaveUnit(current, existedUnits, existedUnitCosts, koUnitStatus, GetNewtatusRepeatCalc(prev), ref gbuObject);

                    #region Анализ данных
                    //Признак было ли по данному объекту обращение?
                    bool prCheckObr = false;
                    //получить историю по объекту и узнать был ли он получен в рамках обращения (по типу входящего документа)
                    //надо перебрать все документы и узнать это
                    List<HistoryUnit> olds = HistoryUnit.GetPrevHistoryTour(koUnit.Id, prev, prevUnitTasks, Tour);
                    foreach (HistoryUnit old in olds)
                    {
                        if (old.Task.NoteType_Code == KoNoteType.Petition && old.Unit.CreationDate < koUnit.CreationDate && koUnit.Id != old.Unit.Id)
                            prCheckObr = true;
                    }
                    OMUnit lastUnit = null;
                    if (olds.Count > 0) lastUnit = HistoryUnit.GetPrevUnit(olds).Unit;
                    if (lastUnit != null)
                    {
                        List<long> sourceIds = new List<long>
                         {
                             Consts.RosreestrRegisterId
                         };
                        var attrIds = AttributeChangeStatuses.Values.Select(x => x.AttributeId).ToList();

                        List<GbuObjectAttribute> prevAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attrIds, lastUnit.CreationDate);
                        List<GbuObjectAttribute> curAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attrIds, koUnit.CreationDate);
                        var unitChangesDictionary = new Dictionary<KoChangeStatus, bool>();
                        foreach (var attributeChangeStatuse in AttributeChangeStatuses)
                        {
	                        SetUnitChange(attributeChangeStatuse, koUnit.Id, prevAttrib, curAttrib, unitChangesDictionary);
                        }

                        if (!prCheckObr)
                        {
	                        DoInheritanceFromPrevUnit(lastUnit, koUnit, unitChangesDictionary);
                        }
                        SetCODTasksFormingAttributesWithChecking(gbuObject.Id, unitChangesDictionary);
                    }
                    #endregion
                }
                else
                {
	                OMMainObject gbuObject = gbuObjectsByCadastralNumbers.FirstOrDefault(x => x.CadastralNumber == current.CadastralNumber);
                    var koUnit = SaveUnit(current,existedUnits, existedUnitCosts, koUnitStatus, koStatusRepeatCalc, ref gbuObject);

                    SaveHistoryForNewObject(koUnit.Id);
                    SetCODTasksFormingAttributes(gbuObject.Id);
                }

                ts.Complete();
            }

            IncreaseImportedObjectsCountAction();
        }

        private OMUnit SaveUnit(T current, List<OMUnit> existedUnits, List<OMCostRosreestr> existedUnitCosts, KoUnitStatus koUnitStatus,
	        KoStatusRepeatCalc koStatusRepeatCalc, ref OMMainObject gbuObject)
        {
	        var isNewGbuObject = false;
	        if (gbuObject == null)
	        {
		        gbuObject = new OMMainObject
		        {
			        Id = -1,
			        CadastralNumber = current.CadastralNumber,
			        IsActive = true,
			        ObjectType_Code = PropertyType,
		        };
		        gbuObject.Save();
		        isNewGbuObject = true;
	        }
	        else
	        {
		        if (gbuObject.ObjectType_Code != PropertyType)
		        {
			        gbuObject.ObjectType_Code = PropertyType;
			        gbuObject.Save();
		        }
	        }

	        SaveGknData(current, gbuObject.Id);
	        //Задание на оценку
	        OMUnit koUnit = SaveUnit(current, gbuObject.Id, koUnitStatus, koStatusRepeatCalc,
		        isNewGbuObject, existedUnits, existedUnitCosts);

	        return koUnit;
        }

        protected virtual void DoInheritanceFromPrevUnit(OMUnit lastUnit, OMUnit koUnit, Dictionary<KoChangeStatus, bool> unitChangesDictionary) { }

        protected abstract void SetCODTasksFormingAttributesWithChecking(long gbuObjectId, Dictionary<KoChangeStatus, bool> unitChangesDictionary);

        private void SetCODTasksFormingAttributes(long gbuObjectId)
        {
	        foreach (var taskFormingAttribute in TaskFormingAttributes)
	        {
		        SaveDataAttribute(taskFormingAttribute, true, gbuObjectId);
	        }
        }

        protected virtual void SaveGknData(T current, long gbuObjectId)
        {
	        foreach (var gknDataAttribute in GknDataAttributes)
	        {
		        SaveGknDataAttribute(gknDataAttribute, current, gbuObjectId);
	        }
        }

        private void SaveGknDataAttribute(ImportedAttributeGkn gknDataAttribute, T current, long gbuObjectId)
        {
	        gknDataAttribute.SaveAttributeValue(current, gbuObjectId, IdDocument, SDate, OtDate, SRDSession.Current.UserID);
	        UpdateObjectsAttributesAction(gbuObjectId, gknDataAttribute.AttributeId);
        }

        protected void SaveDataAttribute(ImportedAttribute gknDataAttribute, object value, long gbuObjectId)
        {
	        gknDataAttribute.SetAttributeValue(value, gbuObjectId, IdDocument, SDate, OtDate, SRDSession.Current.UserID);
	        UpdateObjectsAttributesAction(gbuObjectId, gknDataAttribute.AttributeId);
        }

        protected OMUnit SaveUnit(T current, long gbuObjectId, KoUnitStatus unitStatus, KoStatusRepeatCalc calcStatus, bool isNewGbuObject,
	        List<OMUnit> existedUnits, List<OMCostRosreestr> existedUnitCosts)
        {
	        OMUnit koUnit = null;

            OMUnit existedUnit = null;
	        if (!isNewGbuObject)
		        existedUnit = existedUnits.FirstOrDefault(x =>
			        x.ObjectId == gbuObjectId && x.TaskId == Task.Id && x.TourId == IdTour);

            if (isNewGbuObject || existedUnit == null)
	        {
		        koUnit = new OMUnit
		        {
			        Id = -1,
			        TourId = IdTour,
			        TaskId = Task.Id,
			        GroupId = -1,
			        Status_Code = unitStatus,
			        ObjectId = gbuObjectId,
			        CreationDate = UnitDate,
			        CadastralNumber = current.CadastralNumber,
			        CadastralBlock = current.CadastralNumberBlock,
			        PropertyType_Code = PropertyType,
			        StatusRepeatCalc_Code = calcStatus,
			        StatusResultCalc_Code = KoStatusResultCalc.None,
			        CadastralCost = 0,
			        CadastralCostPre = 0,
			        Upks = 0,
			        UpksPre = 0,
					AssessmentDate = current.AssessmentDate,
					Square = current.Area?.ParseToDecimal()
				};

		        SetAdditionalUnitProperties(koUnit, current);

		        koUnit.Save();
			}
	        else
	        {
		        koUnit = existedUnit;
	        }

            OMCostRosreestr existedUnitCost = null;
	        if (existedUnit != null)
		        existedUnitCost = existedUnitCosts.FirstOrDefault(x => x.IdObject == existedUnit.Id);

            if (current.CadastralCost != null && (isNewGbuObject || existedUnitCost == null))
	        {
		        var cost = new OMCostRosreestr
                {
			        Id = -1,
			        Applicationdate = current.CadastralCost.ApplicationDate,
			        Dateapproval = current.CadastralCost.DateApproval,
			        Dateentering = current.CadastralCost.DateEntering,
			        Datevaluation = current.CadastralCost.DateValuation,
			        Docdate = current.CadastralCost.DocDate,
			        Docnumber = current.CadastralCost.DocNumber,
			        Docname = current.CadastralCost.DocName,
			        IdObject = koUnit.Id,
			        Costvalue = current.CadastralCost.Value?.ParseToDecimal(),
			        Revisalstatementdate = current.CadastralCost.RevisalStatementDate,
		        };
		        cost.Save();
	        }

            return koUnit;
        }

        protected virtual void SetAdditionalUnitProperties(OMUnit koUnit, T current) { }

        private KoStatusRepeatCalc GetNewtatusRepeatCalc(List<OMUnit> units)
        {
            KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.New;
            if (units.Count > 0)
            {
                foreach (OMUnit unit in units)
                {
                    if (unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.Initial || unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.RepeatedInitial)
                        koStatusRepeatCalc = KoStatusRepeatCalc.RepeatedInitial;
                    if (unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.New || unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.Repeated)
                        koStatusRepeatCalc = KoStatusRepeatCalc.Repeated;
                }
            }
            return koStatusRepeatCalc;
        }

        private void SetUnitChange(KeyValuePair<KoChangeStatus, ImportedAttribute> attribute, long unitId, List<GbuObjectAttribute> prevAttribs, List<GbuObjectAttribute> curAttribs, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
        {
	        bool res = true;
	        string oldValue = string.Empty;
	        string newValue = string.Empty;
	        GbuObjectAttribute prevAttrib = prevAttribs.Find(x => x.AttributeId == attribute.Value.AttributeId);
	        GbuObjectAttribute curAttrib = curAttribs.Find(x => x.AttributeId == attribute.Value.AttributeId);
	        if (prevAttrib != null) oldValue = prevAttrib.GetValueInString();
	        if (curAttrib != null) newValue = curAttrib.GetValueInString();
	        if (oldValue != newValue)
	        {
		        res = false;
                OMUnitChange unitChange = new OMUnitChange
                {
			        Id = -1,
			        ChangeStatus_Code = attribute.Key,
			        NewValue = newValue,
			        OldValue = oldValue,
			        UnitId = unitId
		        };
		        unitChange.Save();
	        }

	        unitChangesDictionary.TryAdd(attribute.Key, res);
        }

        private void SaveHistoryForNewObject(long unitId)
        {
	        new OMUnitChange
	        {
		        Id = -1,
		        ChangeStatus_Code = KoChangeStatus.NewObjectAddition,
		        UnitId = unitId
	        }.Save();
        }
    }
}
