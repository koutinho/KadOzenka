using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Shared;
using Serilog;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Dal.GbuObject
{
	/// <summary>
    /// Перенос атрибутов из ГБУ в КО
    /// </summary>
    public class ExportAttributeToKO
    {
        private static readonly ILogger _log = Log.ForContext<ExportAttributeToKO>();
        private GbuObjectService GbuObjectService { get; }
        private string ReportName => "Отчет по переносу атрибутов";
        private int ColumnWidth => 8;
        private int CadastralNumberColumnIndex => 0;
        private int AttributesStartColumnIndex => 1;

        private enum AttributeSettingType
        {
	        UseParentAttributeForLivingPlacements,
	        UseParentAttributeForNotLivingPlacements,
	        UseParentAttributeForCarPlace
        }

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
	        GbuObjectService = new GbuObjectService();
        }
        
        
        public string Run(GbuExportAttributeSettings setting, OMQueue processQueue)
        {
	        var reportService = new GbuReportService();
	        GenerateReportHeaders(setting.Attributes, reportService);

	        locked = new object();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };
            //если используется "Перенос с созданием атрибутов", нужно обновить кеш, т.к. нового атрибута может не быть в нем
            RegisterCache.UpdateCache(0, null);
            var gbuAttributeSettings = OMAttributeSettings.Where(x => true).SelectAll().Execute();
            var gbuAttributes = new List<Tuple<RegisterAttribute, OMAttributeSettings>>();
            foreach (ExportAttributeItem item in setting.Attributes)
            {
	            var gbuAttribute = RegisterCache.GetAttributeData((int)item.IdAttributeGBU);
	            var gbuAttributeSetting =
		            gbuAttributeSettings.FirstOrDefault(x => x.AttributeId == item.IdAttributeGBU);
                gbuAttributes.Add(new Tuple<RegisterAttribute, OMAttributeSettings>(gbuAttribute, gbuAttributeSetting));
            }
            WorkerCommon.LogState(processQueue, $"Найдено {setting.Attributes.Count} пар атрибутов.");
            WorkerCommon.LogState(processQueue, $"Найдено {setting.TaskFilter.Count} заданий на оценку.");
       
            if (setting.TaskFilter.Count > 0)
            {
				//добавление фильтров на лету через декоратор
				var unitsGetter = new TransferAttributeUnitsGetter(setting, _log) as AItemsGetter<UnitPure>;
				unitsGetter = new GbuObjectStatusFilterDecorator<UnitPure>(unitsGetter, _log, setting.ObjectChangeStatus, DateTime.Now.GetEndOfTheDay());

				var units = unitsGetter.GetItems();
	            MaxCount = units.Count;
                CurrentCount = 0;
				WorkerCommon.LogState(processQueue, $"Найдено {units.Count} единиц оценки.");

                var gbuAttributeIds = gbuAttributes.Select(x => x.Item1.Id).ToList();
				Parallel.ForEach(units, options, item => { RunOneUnit(item, setting, gbuAttributeIds, gbuAttributes, reportService); });
				CurrentCount = 0;
                MaxCount = 0;
            }

            reportService.SetStyle();
            reportService.SaveReport(ReportName);

            return reportService.UrlToDownload;
        }


        #region Support Methods

        private void GenerateReportHeaders(List<ExportAttributeItem> attributesFromSettings, GbuReportService reportService)
        {
	        var cadastralNumberColumn = new GbuReportService.Column
	        {
		        Index = CadastralNumberColumnIndex,
		        Header = "КН",
		        Width = 4
	        };
	        reportService.SetIndividualWidth(cadastralNumberColumn.Index, cadastralNumberColumn.Width);

	        var numberOfAttributes = attributesFromSettings?.Count ?? 0;
	        var copiedColumns = new List<GbuReportService.Column>(numberOfAttributes);
	        for (var i = 0; i < numberOfAttributes; i++)
	        {
		        var currentAttribute = attributesFromSettings[i];
		        var koAttribute = RegisterCache.GetAttributeData((int)currentAttribute.IdAttributeKO);
		        var gbuAttribute = RegisterCache.GetAttributeData((int)currentAttribute.IdAttributeGBU);
		        var gbuRegister = RegisterCache.GetRegisterData(gbuAttribute.RegisterId);

		        var column = new GbuReportService.Column
		        {
			        Header = $"{gbuAttribute.Name} ({gbuRegister.Description}) -> {koAttribute.Name}",
			        //+1 чтобы учесть колонку с КН
			        Index = i + AttributesStartColumnIndex,
			        Width = ColumnWidth
		        };
		        copiedColumns.Add(column);
		        reportService.SetIndividualWidth(column.Index, column.Width);
	        }

	        var headers = copiedColumns.Select(x => x.Header).ToList();
	        headers.Insert(0, cadastralNumberColumn.Header);
	        reportService.AddHeaders(headers);
        }

        private void RunOneUnit(UnitPure unit, GbuExportAttributeSettings setting, List<long> lstIds,
	        List<Tuple<RegisterAttribute, OMAttributeSettings>> gbuAttributesWithSettings, GbuReportService reportService)
        {
	        GbuReportService.Row row;
            lock (locked)
            {
                CurrentCount++;
                row = reportService.GetCurrentRow();
                reportService.AddValue(unit.CadastralNumber, CadastralNumberColumnIndex, row);
            }

            var attributes = GetUnitGbuAttributes(unit, lstIds, gbuAttributesWithSettings);
            _log.ForContext("Attributes", JsonConvert.SerializeObject(attributes.Select(x => new { AttributeId = x.AttributeId, ObjectId = x.ObjectId, Value = x.GetValueInString()}).ToList()))
	            .Verbose("Для юнита {UnitId}({UnitCadastralNumber}) получено {AttributesCount} гбу атрибута", unit.Id, unit.CadastralNumber, attributes.Count);
            foreach (GbuObjectAttribute attrib in attributes)
            {
                ExportAttributeItem current = setting.Attributes.Find(x => x.IdAttributeGBU == attrib.AttributeId);
                var columnIndexInReport = setting.Attributes.IndexOf(current) + AttributesStartColumnIndex;

                var koAttributeData = RegisterCache.GetAttributeData((int)current.IdAttributeKO);
                var gbuAttributeData = gbuAttributesWithSettings.FirstOrDefault(x => x.Item1.Id == current.IdAttributeGBU).Item1;

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
                   
                    lock (locked)
                    {
	                    var columnValue = value == null ? "''" : value.ToString();
	                    if (koAttributeData.Type != gbuAttributeData.Type)
	                    {
		                    columnValue = $"{columnValue}\n{GetWarningMessage(koAttributeData, gbuAttributeData)}";
		                    reportService.AddValue(columnValue, columnIndexInReport, row, reportService.WarningCellStyle);
                        }
	                    else
	                    {
		                    reportService.AddValue(columnValue, columnIndexInReport, row);
                        }
                    }
                }
                catch (Exception ex)
                {
	                var errorId = ErrorManager.LogError(ex);

	                lock (locked)
	                {
		                reportService.AddValue($"Ошибка обработки (журнал: {errorId}).", columnIndexInReport, row,
			                reportService.ErrorCellStyle);
	                }
                }
            }
        }

        private List<GbuObjectAttribute> GetUnitGbuAttributes(UnitPure unit, List<long> lstIds, List<Tuple<RegisterAttribute, OMAttributeSettings>> gbuAttributesWithSettings)
        {
	        List<GbuObjectAttribute> result = null;

	        var objectIds = new List<long> { unit.ObjectId };
	        if (unit.ParentPlacementObjectId.HasValue)
	        {
		        objectIds.Add(unit.ParentPlacementObjectId.Value);
	        }
	        //Получаем все атрибуты (в т.ч. атрибуты родительского объекта для юнитов с типом 'Помещение' или 'Машино-место')
	        var allAttributes = GbuObjectService.GetAllAttributes(objectIds, null, lstIds, DateTime.Now.GetEndOfTheDay());

	        if (unit.ObjectType == PropertyTypes.Pllacement && unit.PlacementPurpose == "Жилое")
	        {
		        //Для атрибутов с настройками 'Использовать родительский атрибут для жилых помещений' необходимо использовать значение родительского атрибута
		        result = GetUnitGbuAttributesForPlacements(unit, lstIds, gbuAttributesWithSettings, allAttributes, AttributeSettingType.UseParentAttributeForLivingPlacements);
	        }
	        else if (unit.ObjectType == PropertyTypes.Pllacement && unit.PlacementPurpose == "Нежилое")
	        {
                //Для атрибутов с настройками 'Использовать родительский атрибут для нежилых помещений' необходимо использовать значение родительского атрибута
                result = GetUnitGbuAttributesForPlacements(unit, lstIds, gbuAttributesWithSettings, allAttributes, AttributeSettingType.UseParentAttributeForNotLivingPlacements);
	        }
	        else if (unit.ObjectType == PropertyTypes.Parking)
	        {
                //Для атрибутов с настройками 'Использовать родительский атрибут для машино-мест' необходимо использовать значение родительского атрибута
                result = GetUnitGbuAttributesForPlacements(unit, lstIds, gbuAttributesWithSettings, allAttributes, AttributeSettingType.UseParentAttributeForCarPlace);
            }
            else
	        {
		        result = allAttributes;
	        }

	        return result;
        }

        private List<GbuObjectAttribute> GetUnitGbuAttributesForPlacements(UnitPure unit, List<long> lstIds, List<Tuple<RegisterAttribute, OMAttributeSettings>> gbuAttributesWithSettings, 
	        List<GbuObjectAttribute> allAttributes, AttributeSettingType attributeSettingType)
        {
	        var result = new List<GbuObjectAttribute>();

	        foreach (var attributeId in lstIds)
	        {
		        var attributeSettings = gbuAttributesWithSettings
			        .FirstOrDefault(x => x.Item1.Id == attributeId)?.Item2;
		        bool? useParentAttribute = null;
		        switch (attributeSettingType)
		        {
                    case AttributeSettingType.UseParentAttributeForLivingPlacements:
	                    useParentAttribute = attributeSettings?.UseParentAttributeForLivingPlacements;
	                    break;
                    case AttributeSettingType.UseParentAttributeForNotLivingPlacements:
	                    useParentAttribute = attributeSettings?.UseParentAttributeForNotLivingPlacements;
	                    break;
                    case AttributeSettingType.UseParentAttributeForCarPlace:
	                    useParentAttribute = attributeSettings?.UseParentAttributeForCarPlace;
	                    break;
                }

		        var attribute = useParentAttribute.GetValueOrDefault()
			        ? allAttributes.FirstOrDefault(x => x.AttributeId == attributeId && x.ObjectId == unit.ParentPlacementObjectId)
			        : allAttributes.FirstOrDefault(x => x.AttributeId == attributeId && x.ObjectId == unit.ObjectId);
		        if (attribute != null)
			        result.Add(attribute);
	        }

	        return result;
        }

        private static string GetWarningMessage(RegisterAttribute koAttributeData, RegisterAttribute gbuAttributeData)
        {
	        return $"Типы данных не совпадают. Тип КО - '{koAttributeData.Type.GetEnumDescription()}', тип ГБУ - '{gbuAttributeData.Type.GetEnumDescription()}'";
        }

        #endregion
    }

    #region Entities

    public class UnitPure : ItemBase
    {
	    //public long Id { get; set; }
	    //public long ObjectId { get; set; }
	    public long? ParentPlacementObjectId { get; set; }
	    public string CadastralNumber { get; set; }
	    //TODO раньше использовалась для получения атрибута, в качестве хотфикса поставили текущую дату
	    //TODO если хотфикс будет заапрувлен, нужно убрать
	    //public DateTime? CreationDate { get; set; }
	    public PropertyTypes ObjectType { get; set; }
	    public string PlacementPurpose { get; set; }
    }

	public class TransferAttributeUnitsGetter : AItemsGetter<UnitPure>
	{
		private GbuExportAttributeSettings Settings { get; }

		public TransferAttributeUnitsGetter(GbuExportAttributeSettings settings, ILogger logger) : base(logger)
		{
			Settings = settings;
		}


		public override List<UnitPure> GetItems()
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
							Settings.TaskFilter.Select(x => (double) x).ToList()),
						GetConditionForObjectType(Settings)
					}
				}
			};
			query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, nameof(UnitPure.ObjectId)));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(UnitPure.CadastralNumber)));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType_Code, nameof(UnitPure.ObjectType)));

			var parentObjectIsSubQuery = new QSQuery(OMMainObject.GetRegisterId())
			{
				Columns = new List<QSColumn>
				{
					OMMainObject.GetColumn(x => x.Id)
				},
				Condition = new QSConditionGroup(QSConditionGroupType.And)
				{
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(OMMainObject.GetColumn(x => x.ObjectType_Code), QSConditionType.In,
							new List<double> {(double) PropertyTypes.Building, (double) PropertyTypes.Construction}),
						new QSConditionSimple(OMMainObject.GetColumn(x => x.CadastralNumber), QSConditionType.Equal,
							OMUnit.GetColumn(x => x.BuildingCadastralNumber))
						{
							RightOperandLevel = 1
						},
						new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.In,
							new List<double> {(double) PropertyTypes.Pllacement, (double) PropertyTypes.Parking})
						{
							LeftOperandLevel = 1
						}
					}
				},
				PackageSize = 1
			};
			query.AddColumn(parentObjectIsSubQuery, nameof(UnitPure.ParentPlacementObjectId));

			var allUnits = new List<UnitPure>();
			var resultTable = query.ExecuteQuery();
			foreach (DataRow row in resultTable.Rows)
			{
				allUnits.Add(new UnitPure
				{
					Id = row[nameof(UnitPure.Id)].ParseToLong(),
					ObjectId = row[nameof(UnitPure.ObjectId)].ParseToLong(),
					ParentPlacementObjectId = row[nameof(UnitPure.ParentPlacementObjectId)].ParseToLongNullable(),
					CadastralNumber = row[nameof(UnitPure.CadastralNumber)].ParseToString(),
					ObjectType = (PropertyTypes)row[nameof(UnitPure.ObjectType)].ParseToLong()
				});
			}

			FillPlacementPurpose(allUnits);

			if (Settings.OksAdditionalFilters.IsPlacements)
			{
				return FilterPlacementObjects(allUnits, Settings);
			}

			return allUnits;
		}


		#region Support Methods

		private void FillPlacementPurpose(List<UnitPure> allUnits)
		{
			var placements = allUnits.Where(x => x.ObjectType == PropertyTypes.Pllacement).ToList();
			var placementPurposeAttribute = new RosreestrRegisterService().GetPlacementPurposeAttribute();
			var placementsAttributes = new GbuObjectService().GetAllAttributes(
					placements.Select(x => x.ObjectId).Distinct().ToList(),
					new List<long> { placementPurposeAttribute.RegisterId },
					new List<long> { placementPurposeAttribute.Id },
					DateTime.Now.GetEndOfTheDay(), isLight: true);

			foreach (var placement in placements)
			{
				var attr = placementsAttributes.FirstOrDefault(x => x.ObjectId == placement.ObjectId);
				if (attr != null)
				{
					placement.PlacementPurpose = attr.GetValueInString();
				}
			}
		}

		private List<UnitPure> FilterPlacementObjects(List<UnitPure> allUnits, GbuExportAttributeSettings settings)
		{
			if (settings.OksAdditionalFilters.PlacementPurpose == PlacementPurpose.None)
				return allUnits;

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
			allUnits.Where(x => x.ObjectType == PropertyTypes.Pllacement).ToList().ForEach(x =>
			{
				if (!string.IsNullOrWhiteSpace(x.PlacementPurpose) && possibleValues.Contains(x.PlacementPurpose))
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

				var objectTypes = new List<PropertyTypes> { PropertyTypes.Stead };
				objectTypes.AddRange(additionalFiltersForOks);

				return new QSConditionSimple(propertyTypeColumn, QSConditionType.In, objectTypes.Select(x => (double)x));
			}

			throw new ArgumentException("Не указан тип объекта");
		}

		#endregion
	}

	#endregion
}
