using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
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
        private List<OperationResult> ExportResults { get; set; }

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
        
        
        public List<OperationResult> Run(GbuExportAttributeSettings setting, OMQueue processQueue)
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

            var gbuAttributes = new List<RegisterAttribute>();
            foreach (ExportAttributeItem item in setting.Attributes)
            {
	            var gbuAttribute = RegisterCache.GetAttributeData((int)item.IdAttributeGBU);
	            var gbuRegister = RegisterCache.GetRegisterData(gbuAttribute.RegisterId);
	            gbuAttribute.RegisterName = gbuRegister.Description;
	            gbuAttributes.Add(gbuAttribute);
            }
            WorkerCommon.LogState(processQueue, $"Найдено {setting.Attributes.Count} пар атрибутов.");
            WorkerCommon.LogState(processQueue, $"Найдено {setting.TaskFilter.Count} заданий на оценку.");
       
            if (setting.TaskFilter.Count > 0)
            {
	            var units = GetUnits(setting);
	            ExportResults = new List<OperationResult>(units?.Count ?? 0);
                MaxCount = units.Count;
                CurrentCount = 0;
				WorkerCommon.LogState(processQueue, $"Найдено {units.Count} единиц оценки.");

                var gbuAttributeIds = gbuAttributes.Select(x => x.Id).ToList();
				Parallel.ForEach(units, options, item => { RunOneUnit(item, setting, gbuAttributeIds, gbuAttributes); });
				CurrentCount = 0;
                MaxCount = 0;
            }

            return ExportResults;
        }

        #region Support Methods

        private void RunOneUnit(UnitPure unit, GbuExportAttributeSettings setting, List<long> lstIds,
	        List<RegisterAttribute> gbuAttributes)
        {
            lock (locked)
            {
                CurrentCount++;
            }

			var operationResult = new OperationResult
			{
                CadastralNumber = unit.CadastralNumber
			};
            var attributes = GbuObjectService.GetAllAttributes(unit.ObjectId, null, lstIds, DateTime.Now.GetEndOfTheDay());

            foreach (GbuObjectAttribute attrib in attributes)
            {
                ExportAttributeItem current = setting.Attributes.Find(x => x.IdAttributeGBU == attrib.AttributeId);

                var koAttributeData = RegisterCache.GetAttributeData((int)current.IdAttributeKO);
                var gbuAttributeData = gbuAttributes.FirstOrDefault(x => x.Id == current.IdAttributeGBU);

                try
                {
                    long id_factor = current.IdAttributeKO;
                    long RegId = koAttributeData.RegisterId;
                    object value = attrib.GetValueInString();

                    RegisterObject registerObject = new RegisterObject((int)RegId, (int)unit.Id);
                    int referenceItemId = -1;
                    if (koAttributeData.CodeField.IsNotEmpty() && koAttributeData.ReferenceId > 0)
                    {
	                    var valueStr = value?.ToString();
	                    OMReferenceItem item = OMReferenceItem
		                    .Where(x => x.ReferenceId == koAttributeData.ReferenceId && x.Value == valueStr)
		                    .ExecuteFirstOrDefault();
                        if (item != null) referenceItemId = (int)item.ItemId;
                    }

                    switch (koAttributeData.Type)
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

                    operationResult.Atributes.Add(new Attribute
                    {
                        Index = setting.Attributes.IndexOf(current),
                        KoAttributeName = koAttributeData.Name,
                        GbuAttributeName = gbuAttributeData.Name,
                        GbuRegisterName = gbuAttributeData.RegisterName,
                        Value = value,
                        Warning = koAttributeData.Type != gbuAttributeData.Type ? GetWarningMessage(koAttributeData, gbuAttributeData) : ""
                    });
                }
                catch (System.Exception ex)
                {
	                var errorId = ErrorManager.LogError(ex);
                    
                    operationResult.Atributes.Add(new Attribute
                    {
	                    Index = setting.Attributes.IndexOf(current),
                        KoAttributeName = koAttributeData.Name,
	                    GbuAttributeName = gbuAttributeData.Name,
                        GbuRegisterName = gbuAttributeData.RegisterName,
                        Error = $"Ошибка обработки (журнал: {errorId})."
                    });
                }
            }
            lock (locked)
            {
	            ExportResults.Add(operationResult);
            }
        }

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
	        query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(UnitPure.CadastralNumber)));
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

        private static string GetWarningMessage(RegisterAttribute koAttributeData, RegisterAttribute gbuAttributeData)
        {
	        return $"Типы данных не совпадают. Тип КО - '{koAttributeData.Type.GetEnumDescription()}', тип ГБУ - '{gbuAttributeData.Type.GetEnumDescription()}'";
        }

        #endregion


        #region Entities

        public class UnitPure
        {
	        public long Id { get; set; }
	        public long ObjectId { get; set; }
	        public string CadastralNumber { get; set; }
            //TODO раньше использовалась для получения атрибута, в качестве хотфикса поставили текущую дату
            //TODO если хотфикс будет заапрувлен, нужно убрать
            public DateTime? CreationDate { get; set; }
	        public PropertyTypes? ObjectType { get; set; }
        }

        public class OperationResult
		{
			public string CadastralNumber { get; set; }
            public List<Attribute> Atributes { get; set; }

            public OperationResult()
            {
	            Atributes = new List<Attribute>();
            }
		}

        public class Attribute
        {
            public int Index { get; set; }
            public string KoAttributeName { get; set; }
            public string GbuAttributeName { get; set; }
            public string GbuRegisterName { get; set; }
            public object Value { get; set; }
            public string Warning { get; set; }
            public string Error { get; set; }
        }

        #endregion
    }
}
