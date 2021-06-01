using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.GbuObject.Entities;
using Newtonsoft.Json;
using ObjectModel.Directory;
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
        private List<AttributeMapping> AttributesMapping { get; }
        public List<long> AttributeIdsFromCopy { get; }

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

        
        public GbuObjectInheritanceAttribute(GbuInheritanceAttributeSettings settings)
        {
	        GbuObjectService = new GbuObjectService();
	        Settings = settings;
	        AttributesMapping = Settings.Attributes.Where(x => x.IdFrom > 0 && x.IdTo > 0).ToList();
	        AttributeIdsFromCopy = AttributesMapping.Select(x => x.IdFrom).ToList();
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
			reportService.SetIndividualWidth(4, 6);

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
	           var units = unitsGetter.GetItems();
	           MaxCount = units.Count;
	           CurrentCount = 0;
	           _log.Debug("Скачено {UnitsCount} ЕО по ЗнО", MaxCount);


                _log.Debug("Начата обработка Единиц Оценки");
                Parallel.ForEach(units, options, item => { 
                    RunOneUnit(item, reportService);
                });
                _log.Debug("Закончена обработка Единиц Оценки");


                CurrentCount = 0;
                MaxCount = 0;
                units.Clear();
                _log.Debug("Переменная очищена. Записей {Count}", units.Count);
            }

            var reportId = reportService.SaveReport();

            return reportService.GetUrlToDownloadFile(reportId);
        }

        public void Inheritance(InheritanceUnitPure unit, PropertyTypes typecode, GbuReportService reportService)
        {
	        var parentCadastralNumberAttributeIdList = new List<long>(1) { Settings.ParentCadastralNumberAttribute };
	        var parentCadastralNumberAttribute = GbuObjectService.GetAllAttributes(unit.ObjectId, null, parentCadastralNumberAttributeIdList, unit.CreationDate,
		        attributesToDownload: new List<GbuColumnsToDownload>
		        {
			        GbuColumnsToDownload.Value
		        });
	        var parentCadastralNumber = parentCadastralNumberAttribute.ElementAtOrDefault(0)?.StringValue;

            if (!string.IsNullOrWhiteSpace(parentCadastralNumber))
            {
	            var parent = ObjectModel.Gbu.OMMainObject
		            .Where(x => x.CadastralNumber == parentCadastralNumber && x.ObjectType_Code == typecode)
		            .ExecuteFirstOrDefault();

                if (parent != null)
                {
	                var parentAttributes = GbuObjectService.GetAllAttributes(parent.Id, null, AttributeIdsFromCopy, unit.CreationDate,
					 attributesToDownload: new List<GbuColumnsToDownload>
					 {
						 GbuColumnsToDownload.DocumentId,
						 GbuColumnsToDownload.S,
						 GbuColumnsToDownload.Ot,
						 GbuColumnsToDownload.Value
					 });

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
	                    var attributeTo = AttributesMapping.First(x => x.IdFrom == pattrib.AttributeId);
                        var attributeValue = new GbuObjectAttribute
                        {
                            Id = -1,
                            AttributeId = attributeTo.IdTo,
                            ObjectId = unit.ObjectId,
                            ChangeDocId = pattrib.ChangeDocId,
                            S = pattrib.S,
                            ChangeUserId = SRDSession.Current.UserID,
                            ChangeDate = DateTime.Now,
                            Ot = pattrib.Ot,
                            StringValue = pattrib.StringValue,
                        };

                        GbuObjectService.SaveAttributeValueWithCheck(attributeValue);

                        lock (locked)
                        {
	                        if (rowsReport != null && rowsReport.Count >= counter)
	                        {
		                        AddRowToReport(rowsReport[counter], unit.CadastralNumber, parentCadastralNumberAttribute[0].StringValue,
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
                        reportService.AddValue($"Не найден объект по кадастровому номеру {parentCadastralNumberAttribute[0].StringValue}", 4, rowReport);
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

        public void RunOneUnit(InheritanceUnitPure unit, GbuReportService reportService)
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
	            Inheritance(unit, type.Value, reportService);
            }
        }

        public static void AddRowToReport(GbuReportService.Row rowNumber, string kn, string knInh, long sourceAttribute, string value,  string errorMessage, GbuReportService reportService)
        {
	        string sourceName = GbuObjectService.GetAttributeNameById(sourceAttribute);
	        reportService.AddValue(kn, 0, rowNumber);
	        reportService.AddValue(knInh, 1, rowNumber);
	        reportService.AddValue(sourceName, 2, rowNumber);
			reportService.AddValue(value, 3, rowNumber);
			reportService.AddValue(errorMessage, 4, rowNumber);
        }
    }


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

    #endregion
}
