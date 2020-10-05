using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Registers;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Shared;
using Serilog;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Перенос атрибутов из ГБУ в КО
    /// </summary>
    public class ExportAttributeToKO
    {
        private static readonly ILogger _log = Log.ForContext<ExportAttributeToKO>();
        private RosreestrRegisterService RosreestrRegisterService { get; }
        private GbuObjectService GbuObjectService { get; }

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

        public ExportAttributeToKO()
        {
	        RosreestrRegisterService = new RosreestrRegisterService();
	        GbuObjectService = new GbuObjectService();
        }
        
        
        public void Run(GbuExportAttributeSettings setting, OMQueue processQueue)
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
			Log.Verbose("Найдено {AttributesCount} пар атрибутов.", setting.Attributes.Count);
            Log.Verbose("Найдено {TaskCount} заданий на оценку.", setting.TaskFilter.Count);

            if (setting.TaskFilter.Count > 0)
            {
	            var units = GetUnits(setting);
	            MaxCount = units.Count;
                CurrentCount = 0;
				WorkerCommon.LogState(processQueue, $"Найдено {units.Count} единиц оценки.");
				Log.Verbose("Найдено {UnitsCount} единиц оценки.", units.Count);
				Parallel.ForEach(units, options, item => { RunOneUnit(item, setting, lstIds); });
				CurrentCount = 0;
                MaxCount = 0;
            }
        }

        public void RunOneUnit(UnitPure unit, GbuExportAttributeSettings setting, List<long> lstIds)
        {
            lock (locked)
            {
                CurrentCount++;
            }
            var attributes = GbuObjectService.GetAllAttributes(unit.ObjectId, null, lstIds, unit.CreationDate);

            foreach (GbuObjectAttribute attrib in attributes)
            {
                ExportAttributeItem current = setting.Attributes.Find(x => x.IdAttributeGBU == attrib.AttributeId);

                try
                {
                    _log.ForContext("IdAttributeGBU", current.IdAttributeGBU)
                        .ForContext("AttributeId", current.IdAttributeKO)
                        .Verbose("ExportAttributeToKO.RunOneUnit");
                    
                //if (current != null)
                //{
	                var attributeData = RegisterCache.GetAttributeData((int)current.IdAttributeKO);
					long id_factor = current.IdAttributeKO;
                    long RegId = attributeData.RegisterId;
                    object value = attrib.GetValueInString();

                    RegisterObject registerObject = new RegisterObject((int)RegId, (int)unit.Id);
                    int referenceItemId = -1;
                    if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
                    {
	                    var valueStr = value?.ToString();
	                    OMReferenceItem item = OMReferenceItem
		                    .Where(x => x.ReferenceId == attributeData.ReferenceId && x.Value == valueStr)
		                    .ExecuteFirstOrDefault();
                        if (item != null) referenceItemId = (int)item.ItemId;
                    }

                    switch (attributeData.Type)
                    {
                        case RegisterAttributeType.INTEGER:
                            //из-за несовпадения типов данных в КО и ГБУ нужно попробовать сделать дополнительный парсинг
                            //например, площадь в КО - целое число, а в ГБУ - вещественное
                            //если сделать приведение вещественного числа к long, то будет null
                            value = value.ParseToLongNullable() ?? value.ParseToDecimalNullable();
	                        break;
                        case RegisterAttributeType.DECIMAL:
                            value = value.ParseToDecimalNullable();
                            break;
                        case RegisterAttributeType.BOOLEAN:
                            value = value.ParseToBooleanNullable();
                            break;
                        case RegisterAttributeType.STRING:
                            value = value.ParseToStringNullable();
                            break;
                        case RegisterAttributeType.DATE:
                            value = value.ParseToDateTimeNullable();
                            break;
                    }
                    registerObject.SetAttributeValue((int)id_factor, value, referenceItemId);
                    RegisterStorage.Save(registerObject);
                //}
                }
                catch (System.Exception ex)
                {
                    _log.ForContext("IdAttributeGBU", current.IdAttributeGBU)
                        .ForContext("AttributeId", current.IdAttributeKO)
                        .ForContext("attrib", JsonConvert.SerializeObject(attrib)).Error(ex, "ExportAttributeToKO.RunOneUnit");
                }
            }
        }


        #region Support Methods

        private List<UnitPure> GetUnits(GbuExportAttributeSettings settings)
        {
	        var query = new QSQuery
	        {
		        MainRegisterID = OMUnit.GetRegisterId(),
		        Condition = new QSConditionGroup
		        {
			        Type = QSConditionGroupType.And,
			        Conditions = new List<QSCondition>
			        {
				        new QSConditionSimple
				        {
					        ConditionType = QSConditionType.IsNotNull, LeftOperand = OMUnit.GetColumn(x => x.ObjectId)
				        },
				        new QSConditionSimple(OMUnit.GetColumn(x => x.TaskId), QSConditionType.In,
					        settings.TaskFilter.Select(x => (double) x).ToList()),
				        GetConditionForObjectType(settings)
			        }
		        }
	        };
	        query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, nameof(UnitPure.ObjectId)));
            query.AddColumn(OMUnit.GetColumn(x => x.CreationDate, nameof(UnitPure.CreationDate)));
            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType_Code, nameof(UnitPure.ObjectType)));

            var allUnits = query.ExecuteQuery<UnitPure>();
            if (settings.OksAdditionalFilters.IsPlacements)
            {
	            return FilterPlacementObjects(allUnits, settings);
            }

            return allUnits;
        }

        private List<UnitPure> FilterPlacementObjects(List<UnitPure> allUnits, GbuExportAttributeSettings settings)
        {
	        if (settings.OksAdditionalFilters.PlacementPurpose == PlacementPurpose.None)
		        return allUnits;

	        var placements = allUnits.Where(x => x.ObjectType == PropertyTypes.Pllacement).ToList();
	        var placementPurposeAttribute = RosreestrRegisterService.GetPlacementPurposeAttribute();

	        var placementsAttributes = GbuObjectService.GetAllAttributes(
		        placements.Select(x => x.ObjectId).ToList(),
		        new List<long> { placementPurposeAttribute.RegisterId },
		        new List<long> { placementPurposeAttribute.Id },
		        DateTime.Now.GetEndOfTheDay());

	        var possibleValues = new List<string>();
	        switch (settings.OksAdditionalFilters.PlacementPurpose)
	        {
		        case PlacementPurpose.Live:
			        possibleValues.Add("Жилое");
			        break;
		        case PlacementPurpose.NotLive:
			        possibleValues.Add("Нежилое");
			        break;
		        case PlacementPurpose.LiveAndNotLive:
			        possibleValues.Add("Жилое");
			        possibleValues.Add("Нежилое");
			        break;
            }

            var resultObjectIds = allUnits.Where(x => x.ObjectType != PropertyTypes.Pllacement).Select(x => x.ObjectId).ToList();
	        placementsAttributes.ForEach(x =>
	        {
		        var placementPurpose = x.GetValueInString();
		        if (!string.IsNullOrWhiteSpace(placementPurpose) && possibleValues.Contains(placementPurpose))
		        {
			        resultObjectIds.Add(x.ObjectId);
		        }
	        });

	        return allUnits.Where(x => resultObjectIds.Contains(x.ObjectId)).ToList();
        }

        private static QSCondition GetConditionForObjectType(GbuExportAttributeSettings settings)
        {
	        var propertyTypeColumn = OMUnit.GetColumn(x => x.PropertyType_Code);
	        var additionalFiltersForOks = settings.OksAdditionalFilters.ObjectTypes;
	        
            if (settings.ObjType == ObjectTypeExtended.Oks)
            {
	            if (additionalFiltersForOks.Count == 0)
		            return new QSConditionSimple(propertyTypeColumn, QSConditionType.NotEqual, (double)PropertyTypes.Stead);

	            return new QSConditionSimple(propertyTypeColumn, QSConditionType.In, additionalFiltersForOks.Select(x => (double)x));
            }

	        if (settings.ObjType == ObjectTypeExtended.Zu)
	        {
		        return new QSConditionSimple(propertyTypeColumn, QSConditionType.Equal, (double)PropertyTypes.Stead);
            }

	        if (settings.ObjType == ObjectTypeExtended.Both)
	        {
		        if (additionalFiltersForOks.Count == 0)
			        return null;

		        var objectTypes = new List<PropertyTypes> {PropertyTypes.Stead};
                objectTypes.AddRange(additionalFiltersForOks);

                return new QSConditionSimple(propertyTypeColumn, QSConditionType.In, objectTypes.Select(x => (double)x));
            }

	        throw new ArgumentException("Не указан тип объекта");
        }

        #endregion

       
        #region Entities

        public class UnitPure
        {
	        public long Id { get; set; }
	        public long ObjectId { get; set; }
	        public DateTime? CreationDate { get; set; }
	        public PropertyTypes? ObjectType { get; set; }
        }

        #endregion
    }
}
