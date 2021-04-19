using GemBox.Spreadsheet;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using KadOzenka.Dal.DataImport.DataImporterGknNew;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Support;
using ObjectModel.KO;
using Serilog;
using SerilogTimings.Extensions;
using SerilogTimings;

namespace KadOzenka.Dal.DataImport
{
    public class DataImporterGkn
    {
	    private static readonly ILogger Log = Serilog.Log.ForContext<DataImporterGkn>();

        //Объект-блокиратор для многопоточки
        private static object locked = new object();
        private Dictionary<long, List<long>> _updatedObjectsAttributes;

        /// <summary>
        /// Колличество зданий в Xml
        /// </summary>
        public Int32 CountXmlBuildings { get; private set; }
        /// <summary>
        /// Колличество участков в Xml
        /// </summary>
        public Int32 CountXmlParcels { get; private set; }
        /// <summary>
        /// Колличество сооружений в Xml
        /// </summary>
        public Int32 CountXmlConstructions { get; private set; }
        /// <summary>
        /// Колличество объектов незавершенного строительства в Xml
        /// </summary>
        public Int32 CountXmlUncompliteds { get; private set; }
        /// <summary>
        /// Колличество помещений в Xml
        /// </summary>
        public Int32 CountXmlFlats { get; private set; }
        /// <summary>
        /// Колличество машиномест в Xml
        /// </summary>
        public Int32 CountXmlCarPlaces { get; private set; }

        /// <summary>
        /// Колличество загруженных зданий из Xml
        /// </summary>
        public Int32 CountImportBuildings { get; private set; }
        /// <summary>
        /// Колличество загруженных участков из Xml
        /// </summary>
        public Int32 CountImportParcels { get; private set; }
        /// <summary>
        /// Колличество загруженных сооружений из Xml
        /// </summary>
        public Int32 CountImportConstructions { get; private set; }
        /// <summary>
        /// Колличество загруженных объектов незавершенного строительства из Xml
        /// </summary>
        public Int32 CountImportUncompliteds { get; private set; }
        /// <summary>
        /// Колличество загруженных помещений из Xml
        /// </summary>
        public Int32 CountImportFlats { get; private set; }
        /// <summary>
        /// Колличество загруженных машиномест из Xml
        /// </summary>
        public Int32 CountImportCarPlaces { get; private set; }

        public bool AreCountersInitialized { get; private set; }

        public DataImporterGkn()
        {
	        _updatedObjectsAttributes = new Dictionary<long, List<long>>();
        }


        /// <summary>
        /// Импорт данных ГКН из Xml
        /// xmlFile - файл xml
        /// pathSchema - путь к каталогу где хранится схема
        /// task - ссылка на задание на оценку
        /// </summary>
        public void ImportDataGknFromXml(Stream xmlFile, string pathSchema, ObjectModel.KO.OMTask task, CancellationToken cancellationToken)
        {
            ImportDataGknFromXml(xmlFile, pathSchema, task.EstimationDate.Value, task.TourId.Value, task, task.NoteType_Code, task.EstimationDate.Value, task.EstimationDate.Value, task.DocumentId.Value, cancellationToken);
        }

        /// <summary>
        /// Импорт данных ГКН из Excel
        /// excelFile - файл Excel
        /// pathSchema - путь к каталогу где хранится схема
        /// task - ссылка на задание на оценку
        /// </summary>
        public void ImportDataGknFromExcel(ExcelFile excelFile, string pathSchema, ObjectModel.KO.OMTask task, CancellationToken cancellationToken)
        {
            ImportDataGknFromExcel(excelFile, pathSchema, task.CreationDate.Value, task.TourId.Value, task, task.NoteType_Code, task.EstimationDate.Value, task.EstimationDate.Value, task.DocumentId.Value, cancellationToken);
        }

        private void ImportDataGknFromExcel(ExcelFile excelFile, string pathSchema, DateTime unitDate, long idTour,
            OMTask task, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument,
            CancellationToken cancellationToken)
        {
	        xmlObjectList GknItems = null;
	        using (Operation.Time("Импорт задания на оценку: парсинг xml"))
	        {
		        xmlImportGkn.FillDictionary(pathSchema);
		        GknItems = xmlImportGkn.GetExcelObject(excelFile);
	        }

	        using (Operation.Time("Импорт задания на оценку: импорт распарсенных объектов"))
	        {
                ImportDataGkn(unitDate, idTour, task, koNoteType, sDate, otDate, idDocument, cancellationToken,
			        GknItems);
	        }
        }
        
        private void ImportDataGknFromXml(Stream xmlFile, string pathSchema, DateTime unitDate, long idTour, OMTask task, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument, CancellationToken cancellationToken)
        {
	        xmlObjectList GknItems = null;
	        using (Operation.Time("Импорт задания на оценку: парсинг xml"))
	        {
		        xmlImportGkn.FillDictionary(pathSchema);
		        GknItems = xmlImportGkn.GetXmlObject(xmlFile);
	        }

	        using (Operation.Time("Импорт задания на оценку: импорт распарсенных объектов"))
	        {
		        ImportDataGkn(unitDate, idTour, task, koNoteType, sDate, otDate, idDocument, cancellationToken,
			        GknItems);
	        }
        }

        private void ImportDataGkn(DateTime unitDate, long idTour, OMTask idTask, KoNoteType koNoteType, DateTime sDate,
	        DateTime otDate, long idDocument, CancellationToken cancellationToken, xmlObjectList GknItems)
        {
	        if (cancellationToken.IsCancellationRequested)
	        {
		        Log.Warning("Импорт данных ГКН был отменен");
		        return;
	        }

	        CountXmlBuildings = GknItems.Buildings.Count;
	        CountXmlParcels = GknItems.Parcels.Count;
	        CountXmlConstructions = GknItems.Constructions.Count;
	        CountXmlUncompliteds = GknItems.Uncompliteds.Count;
	        CountXmlFlats = GknItems.Flats.Count;
	        CountXmlCarPlaces = GknItems.CarPlaces.Count;
	        CountImportBuildings = 0;
	        CountImportParcels = 0;
	        CountImportConstructions = 0;
	        CountImportUncompliteds = 0;
	        CountImportFlats = 0;
	        CountImportCarPlaces = 0;
	        AreCountersInitialized = true;
	        Log.ForContext("TaskId", idTask)
		        .ForContext("CountXmlBuildings", CountXmlBuildings)
		        .ForContext("CountXmlParcels", CountXmlParcels)
		        .ForContext("CountXmlConstructions", CountXmlConstructions)
		        .ForContext("CountXmlUncompliteds", CountXmlUncompliteds)
		        .ForContext("CountXmlFlats", CountXmlFlats)
		        .ForContext("CountXmlCarPlaces", CountXmlCarPlaces)
		        .Debug("Получены данные для импорта из ГКН");

	        var objectsImporters = new List<object>
	        {
		        new ImportObjectBuild(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, IncreaseImportedBuildingsCount, UpdateObjectsAttributes),
		        new ImportObjectParcel(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, IncreaseImportedParcelsCount, UpdateObjectsAttributes),
		        new ImportObjectConstruction(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, IncreaseImportedConstructionsCount, UpdateObjectsAttributes),
		        new ImportObjectUncomplited(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, IncreaseImportedUncomplitedsCount, UpdateObjectsAttributes),
		        new ImportObjectFlat(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, IncreaseImportedFlatsCount, UpdateObjectsAttributes),
		        new ImportObjectCarPlace(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, IncreaseImportedCarPlacesCount, UpdateObjectsAttributes)
	        };

	        ParallelOptions options = new ParallelOptions
	        {
		        CancellationToken = cancellationToken,
		        MaxDegreeOfParallelism = 10
	        };

	        try
	        {
		        Parallel.ForEach(objectsImporters, options, item =>
		        {
			        if (item is ImportObjectBuild build)
			        {
						build.Init();
				        build.ImportObjects(GknItems.Buildings, cancellationToken);
			        }
			        else if (item is ImportObjectParcel parcel)
			        {
						parcel.Init();
				        parcel.ImportObjects(GknItems.Parcels, cancellationToken);
			        }
			        else if (item is ImportObjectConstruction construction)
			        {
						construction.Init();
				        construction.ImportObjects(GknItems.Constructions, cancellationToken);
			        }
			        else if (item is ImportObjectUncomplited uncomplited)
			        {
						uncomplited.Init();
				        uncomplited.ImportObjects(GknItems.Uncompliteds, cancellationToken);
			        }
			        else if (item is ImportObjectFlat flat)
			        {
						flat.Init();
				        flat.ImportObjects(GknItems.Flats, cancellationToken);
			        }
			        else if (item is ImportObjectCarPlace place)
			        {
						place.Init();
				        place.ImportObjects(GknItems.CarPlaces, cancellationToken);
			        }
		        });
	        }
	        catch (OperationCanceledException)
	        {
		        Log.Warning("Импорт данных ГКН был отменен");
	        }

            try
            {
	            using (Log.TimeOperation(
		            "Добавление задачи на актуализацию данных для отчетов с характеристиками объектов"))
	            {
		            var id = TmpTableFiller.AddProcessToQueue(_updatedObjectsAttributes);
	            }
            }
            catch (Exception e)
            {
	            Log.ForContext("MappedInfoToUpdate",
			            TmpTableFiller.MapDictionary(_updatedObjectsAttributes),
			            destructureObjects: true)
		            .ForContext("InfoToUpdateAsDictionary", _updatedObjectsAttributes, destructureObjects: true)
		            .Fatal(e,
			            "Не удалось добавить в очередь процесс для актуализации данных для отчетов с характеристиками обектов");
            }
        }

        private void UpdateObjectsAttributes(long idObject, long attributeId)
        {
	        lock (locked)
	        {
		        if (!_updatedObjectsAttributes.ContainsKey(idObject))
		        {
			        _updatedObjectsAttributes[idObject] = new List<long>();
		        }
		        _updatedObjectsAttributes[idObject].Add(attributeId);
	        }
        }

        public void IncreaseImportedBuildingsCount()
        {
	        lock (locked)
	        {
		        CountImportBuildings++;
            }
        }

        public void IncreaseImportedParcelsCount()
        {
	        lock (locked)
	        {
                CountImportParcels++;
	        }
        }

        public void IncreaseImportedConstructionsCount()
        {
	        lock (locked)
	        {
                CountImportConstructions++;
	        }
        }

        public void IncreaseImportedUncomplitedsCount()
        {
	        lock (locked)
	        {
                CountImportUncompliteds++;
	        }
        }

        public void IncreaseImportedFlatsCount()
        {
	        lock (locked)
	        {
		        CountImportFlats++;
	        }
        }

        public void IncreaseImportedCarPlacesCount()
        {
	        lock (locked)
	        {
                CountImportCarPlaces++;
	        }
        }
    }
}
