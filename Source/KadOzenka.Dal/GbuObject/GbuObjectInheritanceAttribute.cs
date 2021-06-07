using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.GbuObject.Entities;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using Serilog;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Наследование атрибутов ГБУ по объектам
    /// </summary>
    public class GbuObjectInheritanceAttribute
    {
	    private static readonly ILogger _log = Log.ForContext<GbuObjectInheritanceAttribute>();
        private GbuObjectService GbuObjectService { get; }
        private GbuInheritanceAttributeSettings Settings { get; }
        private List<AttributeMappingInternal> AttributesMapping { get; }
        private int ErrorColumnIndex = 4;

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

        public static string ErrorMessageForNotSupporterType = "Неподдерживаемый тип атрибута";
        public static string ErrorMessageForChildConverting = "Невозможно привести значение";
        public static string ErrorMessageForParentConverting = "Невозможно привести значение родительского фактора";


        public GbuObjectInheritanceAttribute(GbuInheritanceAttributeSettings settings)
        {
	        GbuObjectService = new GbuObjectService();
	        Settings = settings;
	        AttributesMapping = Settings.Attributes.Where(x => x.IdFrom > 0 && x.IdTo > 0)
		        .Select(x => new AttributeMappingInternal
		        {
			        From = RegisterCache.GetAttributeData(x.IdFrom),
			        To = RegisterCache.GetAttributeData(x.IdTo)
		        }).ToList();
        }


        /// <summary>
        /// Выполнение операции наследования атрибутов
        /// </summary>
        public string Run()
        {
	        _log.ForContext("InputParameters", JsonConvert.SerializeObject(Settings)).Debug("Входные данные для Наследования");

            using var reportService = new GbuReportService("Отчет наследование");
			reportService.AddHeaders(new List<string>{ "КН", "КН наследуемого объекта", "Имя наследуемого атрибута", "Значение атрибута", "Ошибка" });
			reportService.SetIndividualWidth(1, 4);
			reportService.SetIndividualWidth(0, 4);
			reportService.SetIndividualWidth(2, 6);
			reportService.SetIndividualWidth(3, 4);
			reportService.SetIndividualWidth(ErrorColumnIndex, 6);

            locked = new object();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };

            if (Settings.TaskFilter?.Count > 0)
            {
	           _log.ForContext("TasksIds", Settings.TaskFilter, true).Debug("Начато скачивание ЕО по {TasksCount} ЗнО", Settings.TaskFilter.Count);
	           var unitsGetter = new InheritanceUnitsGetter(_log, Settings) as AItemsGetter<InheritanceUnitPure>;
	           unitsGetter = new GbuObjectStatusFilterDecorator<InheritanceUnitPure>(unitsGetter, _log, Settings.ObjectChangeStatus, DateTime.Now.GetEndOfTheDay());
	           var allUnits = unitsGetter.GetItems();
	           MaxCount = allUnits.Count;
	           CurrentCount = 0;
	           _log.Debug("Скачено {UnitsCount} ЕО по ЗнО", MaxCount);


				var unitsGroupedByCreationDate = allUnits.GroupBy(x => x.CreationDate ?? DateTime.Now.Date)
					.ToDictionary(k => k.Key, v => v.ToList());
				_log.Debug("Найдено {UnitsGroupCount} групп ЕО с одинаковой датой создания", unitsGroupedByCreationDate.Count);

                unitsGroupedByCreationDate.ForEach(group =>
				{
					var units = group.Value;
					var unitsCreationDate = group.Key;

					var parentInfo = GetInfoAboutParentObjects(units, unitsCreationDate);

					_log.Debug("Начата обработка Единиц Оценки");
					Parallel.ForEach(units, options, unit => {
						ProcessOneUnit(unit, parentInfo, reportService);
					});
					_log.Debug("Закончена обработка Единиц Оценки");
                });


                CurrentCount = 0;
                MaxCount = 0;
                allUnits.Clear();
                _log.Debug("Переменная очищена. Записей {Count}", allUnits.Count);
            }

            var reportId = reportService.SaveReport();

            return reportService.GetUrlToDownloadFile(reportId);
        }


        #region Support Methods

        private ParentInfo GetInfoAboutParentObjects(List<InheritanceUnitPure> units, DateTime unitsCreationDate)
        {
	        _log.Debug("Начато скачивание атрибутов с КН-родителя для {UnitsCount} ЕО в группе", units.Count);
	        var parentCadastralNumberAttributeIdList = new List<long>(1) { Settings.ParentCadastralNumberAttribute };
	        var parentCadastralNumberAttributes = GbuObjectService.GetAllAttributes(
		        units.Select(x => x.ObjectId).ToList(),
		        null,
		        parentCadastralNumberAttributeIdList, unitsCreationDate,
		        attributesToDownload: new List<GbuColumnsToDownload> {GbuColumnsToDownload.Value});
	        _log.Debug("Найдено {ParentCadastralNumberAttributesCount} атрибутов с КН-родителя", parentCadastralNumberAttributes.Count);

	        var parents = GetParentGbuMainObjects(parentCadastralNumberAttributes);

	        var attributeIdsFromCopy = AttributesMapping.Select(x => x.From.Id).ToList();
	        var parentIds = parents.Select(x => x.Id).ToList();
            var parentAttributes = GbuObjectService.GetAllAttributes(parentIds, null, attributeIdsFromCopy, unitsCreationDate,
		        attributesToDownload: new List<GbuColumnsToDownload>
		        {
			        GbuColumnsToDownload.DocumentId,
			        GbuColumnsToDownload.S,
			        GbuColumnsToDownload.Ot,
			        GbuColumnsToDownload.Value
		        }).GroupBy(x => x.ObjectId).ToDictionary(x => x.Key, x => x.ToList());

            return new ParentInfo
	        {
		        ParentCadastralNumberAttributes = parentCadastralNumberAttributes.ToDictionary(key => key.ObjectId),
		        Parents = parents,
                ParentAttributes = parentAttributes
            };
        }

        private List<OMMainObject> GetParentGbuMainObjects(List<GbuObjectAttribute> parentCadastralNumberAttributes)
        {
	        var uniqueCadastralNumbers = parentCadastralNumberAttributes.Select(x => x.StringValue)
		        .Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
	        _log.Debug("Найдено {UniqueParentCadastralNumberAttributesCount} уникальных атрибутов с КН-родителя", uniqueCadastralNumbers.Count);

	        var parents = new List<OMMainObject>();
	        if (uniqueCadastralNumbers.Count > 0)
	        {
		        var possibleTypes = new List<PropertyTypes>
		        {
			        PropertyTypes.Building, PropertyTypes.Stead, PropertyTypes.CadastralQuartal
		        };

		        _log.Debug("Начато скачивание ОН-родителей по уникальным КН");
		        parents = OMMainObject.Where(x =>
				        uniqueCadastralNumbers.Contains(x.CadastralNumber) && possibleTypes.Contains(x.ObjectType_Code))
			        .Select(x => new
			        {
				        x.CadastralNumber,
				        x.ObjectType_Code
			        }).Execute();
                _log.Debug("Найдено {ParentCadastralNumberAttributesCount} ОН-родителей по уникальным КН", parents.Count);
            }

	        return parents;
        }

		private void Inheritance(InheritanceUnitPure unit, PropertyTypes typecode, ParentInfo parentInfo,
	        GbuReportService reportService)
        {
	        parentInfo.ParentCadastralNumberAttributes.TryGetValue(unit.ObjectId, out var parentCadastralNumberAttribute);
	        var parentCadastralNumber = parentCadastralNumberAttribute?.StringValue;
	        if (!string.IsNullOrWhiteSpace(parentCadastralNumber))
            {
	            var parent = parentInfo.Parents.FirstOrDefault(x => x.CadastralNumber == parentCadastralNumber && x.ObjectType_Code == typecode);
                if (parent != null)
                {
	                parentInfo.ParentAttributes.TryGetValue(parent.Id, out var parentAttributes);
	                if (parentAttributes == null)
		                return;
                    
                    var rowsReport = new List<GbuReportService.Row>();
                    if (parentAttributes.Count > 0)
                    {
	                    lock (locked)
	                    {
		                    rowsReport = reportService.GetRangeRows(parentAttributes.Count);
	                    }
                    }

                    int counter = 0;
                    foreach (var pattrib in parentAttributes)
                    {
	                    var attributeTo = AttributesMapping.First(x => x.From.Id == pattrib.AttributeId).To;
                        var attributeValue = new GbuObjectAttribute
                        {
                            Id = -1,
                            AttributeId = attributeTo.Id,
                            ObjectId = unit.ObjectId,
                            ChangeDocId = pattrib.ChangeDocId,
                            S = pattrib.S,
                            ChangeUserId = SRDSession.Current.UserID,
                            ChangeDate = DateTime.Now,
                            Ot = pattrib.Ot
                        };

                        var parentAttributeValue = pattrib.GetValue();
                        var result = ProcessAttributeValueToCopy(parentAttributeValue, pattrib.AttributeData.Type, attributeTo.Type);
                        if (result.IsSuccess)
                        {
	                        attributeValue.SetValue(result.Value);
                        }

                        GbuObjectService.SaveAttributeValueWithCheck(attributeValue);

                        lock (locked)
                        {
	                        if (rowsReport != null && rowsReport.Count >= counter)
	                        {
		                        AddRowToReport(rowsReport[counter], unit.CadastralNumber, parentCadastralNumber,
			                        pattrib.AttributeId, parentAttributeValue, result.ErrorMessages?.ToString(), reportService);
		                        counter++;
	                        }
                        }
                    }
                }
                else
                {
	                AddErrorToReport(unit.CadastralNumber, $"Не найден объект по кадастровому номеру '{parentCadastralNumber}'", reportService);
                }
            }
            else
	        {
		        AddErrorToReport(unit.CadastralNumber, "Не найдено значение родительского кадастрового номера", reportService);
	        }
        }

		public static ConvertingResult ProcessAttributeValueToCopy(object parentAttributeValue,
			RegisterAttributeType parentAttributeType, RegisterAttributeType childAttributeType)
        {
            object result = null;
            var errorMessages = new StringBuilder();
            var parentAttributeStr = parentAttributeValue?.ToString();

            if (!string.IsNullOrWhiteSpace(parentAttributeStr) && parentAttributeType != childAttributeType)
            {
	            _log.Warning("Несопадение типов атрибутов {AttributeFromType} => {AttributeToType}, идет попытка конвертации", parentAttributeType.GetEnumDescription(), childAttributeType.GetEnumDescription());

                switch (parentAttributeType)
                {
                    case RegisterAttributeType.STRING:
                        switch (childAttributeType)
                        {
                            case RegisterAttributeType.DATE:
	                            if (parentAttributeStr.TryParseToDateTime(out var dateResult))
                                    result = dateResult;
                                else
                                    errorMessages.AppendLine(GetErrorMessageForNotConvertedChildValue(parentAttributeStr, childAttributeType));
                                break;
                            case RegisterAttributeType.DECIMAL:
                            case RegisterAttributeType.INTEGER:
                                if (parentAttributeValue.TryParseToDecimal(out var doubleResult))
                                    result = doubleResult;
                                else
                                    errorMessages.AppendLine(GetErrorMessageForNotConvertedChildValue(parentAttributeStr, childAttributeType));
                                break;
                            case RegisterAttributeType.BOOLEAN:
	                            if (parentAttributeValue.TryParseToBoolean(out var boolResult))
                                    result = boolResult;
                                else
                                    errorMessages.AppendLine(GetErrorMessageForNotConvertedChildValue(parentAttributeStr, childAttributeType));
                                break;
                            default:
                            {
	                            errorMessages.AppendLine($"{ErrorMessageForNotSupporterType} {childAttributeType.GetEnumDescription()}");
	                            break;
                            }
                        }
                        break;


                    case RegisterAttributeType.DATE:
	                    switch (childAttributeType)
	                    {
		                    case RegisterAttributeType.STRING:
			                    result = parentAttributeStr;
			                    break;
		                    case RegisterAttributeType.DECIMAL:
		                    case RegisterAttributeType.INTEGER:
			                    errorMessages.AppendLine(GetErrorMessageForNotConvertedChildValue(parentAttributeStr, childAttributeType));
			                    break;
		                    case RegisterAttributeType.BOOLEAN:
			                    errorMessages.AppendLine(GetErrorMessageForNotConvertedChildValue(parentAttributeStr, childAttributeType));
			                    break;
		                    default:
		                    {
			                    errorMessages.AppendLine($"{ErrorMessageForNotSupporterType} {childAttributeType.GetEnumDescription()}");
			                    break;
		                    }
	                    }
                        break;


                    case RegisterAttributeType.DECIMAL:
                    case RegisterAttributeType.INTEGER:
	                    switch (childAttributeType)
	                    {
		                    case RegisterAttributeType.STRING:
			                    result = parentAttributeStr;
			                    break;
		                    case RegisterAttributeType.DATE:
			                    errorMessages.AppendLine(GetErrorMessageForNotConvertedChildValue(parentAttributeStr, childAttributeType));
			                    break;
		                    case RegisterAttributeType.BOOLEAN:
			                    errorMessages.AppendLine(GetErrorMessageForNotConvertedChildValue(parentAttributeStr, childAttributeType));
			                    break;
		                    default:
		                    {
			                    errorMessages.AppendLine($"{ErrorMessageForNotSupporterType} {childAttributeType.GetEnumDescription()}");
			                    break;
		                    }
	                    }
                        break;


                    case RegisterAttributeType.BOOLEAN:
	                    switch (childAttributeType)
	                    {
		                    case RegisterAttributeType.STRING:
			                    result = parentAttributeStr;
			                    break;
		                    case RegisterAttributeType.DATE:
			                    errorMessages.AppendLine(GetErrorMessageForNotConvertedChildValue(parentAttributeStr, childAttributeType));
			                    break;
		                    case RegisterAttributeType.DECIMAL:
		                    case RegisterAttributeType.INTEGER:
			                    errorMessages.AppendLine(GetErrorMessageForNotConvertedChildValue(parentAttributeStr, childAttributeType));
			                    break;
		                    default:
		                    {
			                    errorMessages.AppendLine($"{ErrorMessageForNotSupporterType} {childAttributeType.GetEnumDescription()}");
			                    break;
		                    }
	                    }
                        break;

                    default:
                    {
	                    errorMessages.AppendLine($"{ErrorMessageForNotSupporterType} {childAttributeType.GetEnumDescription()}");
	                    break;
                    }
                }
            }
            else
            {
	            return new ConvertingResult
	            {
		            Value = parentAttributeValue
                };
            }

            return new ConvertingResult
            {
                ErrorMessages = errorMessages,
                Value = result
            };
        }

        private static string GetErrorMessageForNotConvertedChildValue(string initialValue, RegisterAttributeType typeToCast)
        {
	        //TODO get type
	        return $"{ErrorMessageForChildConverting} '{initialValue}' к типу '{typeToCast.GetEnumDescription()}'";
        }

        private static string GetErrorMessageForNotConvertedParentValue(string initialValue, RegisterAttributeType typeToCast)
        {
	        //TODO get type
	        return $"{ErrorMessageForParentConverting} '{initialValue}' к типу '{typeToCast.GetEnumDescription()}'";
        }

        public void ProcessOneUnit(InheritanceUnitPure unit, ParentInfo parentInfo, GbuReportService reportService)
        {
            lock (locked)
            {
                CurrentCount++;
            }

            PropertyTypes? type = null;
            //Тип наследования: Здание -> Помещение
            if (Settings.BuildToFlat && unit.PropertyType_Code == PropertyTypes.Pllacement)
            {
	            type = PropertyTypes.Building;
            }
            //Тип наследования: Земельный участок -> Объект незавершенного строительства
            if (Settings.ParcelToUncomplited && unit.PropertyType_Code == PropertyTypes.UncompletedBuilding)
            {
	            type = PropertyTypes.Stead;
            }
            //Тип наследования: Земельный участок -> Сооружение
            if (Settings.ParcelToConstruction && unit.PropertyType_Code == PropertyTypes.Construction)
            {
	            type = PropertyTypes.Stead;
            }
            //Тип наследования: Земельный участок -> Здание
            if (Settings.ParcelToBuilding && unit.PropertyType_Code == PropertyTypes.Building)
            {
	            type = PropertyTypes.Stead;
            }
            //Тип наследования: Кадастровый квартал -> Объект незавершенного строительства
            if (Settings.CadastralBlockToUncomplited && unit.PropertyType_Code == PropertyTypes.UncompletedBuilding)
            {
	            type = PropertyTypes.CadastralQuartal;
            }
            //Тип наследования: Кадастровый квартал -> Сооружение
            if (Settings.CadastralBlockToConstruction && unit.PropertyType_Code == PropertyTypes.Construction)
            {
	            type = PropertyTypes.CadastralQuartal;
            }
            //Тип наследования: Кадастровый квартал -> Здание
            if (Settings.CadastralBlockToBuilding && unit.PropertyType_Code == PropertyTypes.Building)
            {
	            type = PropertyTypes.CadastralQuartal;
            }
            //Тип наследования: Кадастровый квартал -> Земельный участок
            if (Settings.CadastralBlockToParcel && unit.PropertyType_Code == PropertyTypes.Stead)
            {
	            type = PropertyTypes.CadastralQuartal;
            }

            if (type != null)
            {
	            Inheritance(unit, type.Value, parentInfo, reportService);
            }
        }

        private void AddRowToReport(GbuReportService.Row rowNumber, string kn, string knInh, long sourceAttribute, object value, string errorMessage, GbuReportService reportService)
        {
	        string sourceName = GbuObjectService.GetAttributeNameById(sourceAttribute);
	        reportService.AddValue(kn, 0, rowNumber);
	        reportService.AddValue(knInh, 1, rowNumber);
	        reportService.AddValue(sourceName, 2, rowNumber);
			reportService.AddValue(value?.ToString(), 3, rowNumber);
			reportService.AddValue(errorMessage, ErrorColumnIndex, rowNumber);
        }

        private void AddErrorToReport(string unitCadastralNumber, string message, GbuReportService reportService)
        {
	        lock (locked)
	        {
		        var rowReport = reportService.GetCurrentRow();
		        reportService.AddValue(unitCadastralNumber, 0, rowReport);
		        reportService.AddValue(message, ErrorColumnIndex, rowReport);
	        }
        }
    }

    #endregion


    #region Entities

    public class InheritanceUnitPure : ItemBase
    {
	    public string CadastralNumber { get; set; }
	    public DateTime? CreationDate { get; set; }
	    public PropertyTypes PropertyType_Code { get; set; }
    }

    public class InheritanceUnitsGetter : AItemsGetter<InheritanceUnitPure>
    {
	    public GbuInheritanceAttributeSettings Settings { get; set; }

	    public InheritanceUnitsGetter(ILogger logger, GbuInheritanceAttributeSettings setting) : base(logger)
	    {
		    Settings = setting;
	    }


	    public override List<InheritanceUnitPure> GetItems()
	    {
            if(Settings.TaskFilter == null)
                return new List<InheritanceUnitPure>();

		    return ObjectModel.KO.OMUnit.Where(x => Settings.TaskFilter.Contains((long) x.TaskId) && x.ObjectId != null)
			    .Select(x => new
			    {
				    x.ObjectId,
				    x.CadastralNumber,
				    x.CreationDate,
				    x.PropertyType_Code
			    })
			    .Execute()
			    .Select(x => new InheritanceUnitPure
			    {
				    Id = x.Id,
				    ObjectId = x.ObjectId.GetValueOrDefault(),
				    CadastralNumber = x.CadastralNumber,
				    CreationDate = x.CreationDate,
				    PropertyType_Code = x.PropertyType_Code
			    }).ToList();
	    }
    }

    public class ParentInfo
    {
	    public Dictionary<long, GbuObjectAttribute> ParentCadastralNumberAttributes { get; set; }
	    public List<OMMainObject> Parents { get; set; }
	    public Dictionary<long, List<GbuObjectAttribute>> ParentAttributes { get; set; }
    }

    internal class AttributeMappingInternal
    {
        public RegisterAttribute From { get; set; }
        public RegisterAttribute To { get; set; }
    }

    public class ConvertingResult
    {
	    public object Value { get; set; }
	    public StringBuilder ErrorMessages { get; set; }
	    public bool IsSuccess => ErrorMessages == null || ErrorMessages.Length == 0;
    }

    #endregion
}
