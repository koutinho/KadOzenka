using GemBox.Spreadsheet;
using KadOzenka.Dal.XmlParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks.Excel;
using KadOzenka.Dal.DataImport.DataImporterGknNew;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.GbuObject;
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

        private readonly object _locked;
        private Dictionary<long, List<long>> _updatedObjectsAttributes;


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

        private GknAllAttributes AllGknAttributes { get; set; }
        private xmlImportGkn XmlImportGkn { get; }
        private GbuReportService GbuReportService { get; }

		public DataImporterGkn(GbuReportService gbuReportService)
		{
			GbuReportService = gbuReportService;
			XmlImportGkn = new xmlImportGkn(GbuReportService);

	        _updatedObjectsAttributes = new Dictionary<long, List<long>>();
	        _locked = new object();
			AllGknAttributes = new GknAllAttributes();
        }


        /// <summary>
        /// Импорт данных ГКН из Xml
        /// xmlFile - файл xml
        /// pathSchema - путь к каталогу где хранится схема
        /// task - ссылка на задание на оценку
        /// </summary>
        public void ImportDataGknFromXml(Stream xmlFile, OMTask task, CancellationToken cancellationToken)
        {
			xmlObjectList GknItems = null;
			using (Operation.Time("Импорт задания на оценку: парсинг xml"))
			{
				xmlImportGkn.FillDictionary();
				GknItems = XmlImportGkn.GetXmlObject(xmlFile, task.GetAssessmentDateForUnit());
			}

			using (Operation.Time("Импорт задания на оценку: импорт распарсенных объектов"))
			{
				ImportDataGkn(task.EstimationDate.Value, task, cancellationToken, GknItems);
			}
		}

  //      /// <summary>
  //      /// Импорт данных ГКН из Excel для Обращений
  //      /// excelFile - файл Excel
  //      /// pathSchema - путь к каталогу где хранится схема
  //      /// task - ссылка на задание на оценку
  //      /// </summary>
  //      public void ImportGknPetitionFromExcel(ExcelFile excelFile, string pathSchema, OMTask task, CancellationToken cancellationToken)
  //      {
		//	xmlObjectList GknItems = null;
		//	using (Operation.Time("Импорт задания на оценку (Обращения): парсинг excel"))
		//	{
		//		xmlImportGkn.FillDictionary(pathSchema);
		//		GknItems = XmlImportGkn.GetExcelObjectForPetition(excelFile, task.GetAssessmentDateForUnit());
		//	}

		//	using (Operation.Time("Импорт задания на оценку (Обращения): импорт распарсенных объектов"))
		//	{
		//		ImportDataGkn(task.CreationDate.Value, task, cancellationToken, GknItems);
		//	}
		//}

        /// <summary>
        /// Импорт данных ГКН из Excel для всех типов кроме Обращений
        /// excelFile - файл Excel
        /// pathSchema - путь к каталогу где хранится схема
        /// task - ссылка на задание на оценку
        /// </summary>
        public void ImportGknFromExcel(ExcelFile excelFile, OMTask task,
	        List<ColumnToAttributeMapping> columnsMapping, CancellationToken cancellationToken)
        {
	        xmlObjectList gknItems;
	        using (Operation.Time("Импорт задания на оценку: парсинг excel"))
	        {
		        xmlImportGkn.FillDictionary();
		        gknItems = XmlImportGkn.GetExcelObject(excelFile, columnsMapping, AllGknAttributes);
	        }

	        using (Operation.Time("Импорт задания на оценку: импорт распарсенных объектов"))
	        {
		        ImportDataGkn(task.EstimationDate.Value, task, cancellationToken, gknItems);
	        }
        }

		private void ImportDataGkn(DateTime unitDate, OMTask task, CancellationToken cancellationToken, xmlObjectList GknItems)
        {
	        if (cancellationToken.IsCancellationRequested)
	        {
		        Log.Warning("Импорт данных ГКН был отменен");
		        return;
	        }

	        CountImportBuildings = 0;
	        CountImportParcels = 0;
	        CountImportConstructions = 0;
	        CountImportUncompliteds = 0;
	        CountImportFlats = 0;
	        CountImportCarPlaces = 0;
	        AreCountersInitialized = true;
	        Log.ForContext("TaskId", task.Id)
		        .ForContext("CountXmlBuildings", GknItems.Buildings.Count)
		        .ForContext("CountXmlParcels", GknItems.Parcels.Count)
		        .ForContext("CountXmlConstructions", GknItems.Constructions.Count)
		        .ForContext("CountXmlUncompliteds", GknItems.Uncompliteds.Count)
		        .ForContext("CountXmlFlats", GknItems.Flats.Count)
		        .ForContext("CountXmlCarPlaces", GknItems.CarPlaces.Count)
		        .Debug("Получены данные для импорта из ГКН");

	        var objectsImporters = new List<object>
	        {
		        new ImportObjectBuild(AllGknAttributes.Building, unitDate, task, IncreaseImportedBuildingsCount,
			        UpdateObjectsAttributes, GbuReportService, _locked),
		        new ImportObjectParcel(AllGknAttributes.Parcel, unitDate, task, IncreaseImportedParcelsCount,
			        UpdateObjectsAttributes, GbuReportService, _locked),
		        new ImportObjectConstruction(AllGknAttributes.Construction, unitDate, task,
			        IncreaseImportedConstructionsCount, UpdateObjectsAttributes, GbuReportService, _locked),
		        new ImportObjectUncomplited(AllGknAttributes.Uncompleted, unitDate, task,
			        IncreaseImportedUncomplitedsCount, UpdateObjectsAttributes, GbuReportService, _locked),
		        new ImportObjectFlat(AllGknAttributes.Flat, unitDate, task, IncreaseImportedFlatsCount,
			        UpdateObjectsAttributes, GbuReportService, _locked),
		        new ImportObjectCarPlace(AllGknAttributes.CarPlace, unitDate, task, IncreaseImportedCarPlacesCount,
			        UpdateObjectsAttributes, GbuReportService, _locked)
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
	        lock (_locked)
	        {
		        if (!_updatedObjectsAttributes.ContainsKey(idObject))
		        {
			        _updatedObjectsAttributes[idObject] = new List<long>();
		        }
		        _updatedObjectsAttributes[idObject].Add(attributeId);
	        }
        }

        public int GetTotalObjectsCount()
        {
	        return XmlImportGkn.TotalObjectCounter;
        }

        public void IncreaseImportedBuildingsCount()
        {
	        lock (_locked)
	        {
		        CountImportBuildings++;
            }
        }

        public void IncreaseImportedParcelsCount()
        {
	        lock (_locked)
	        {
                CountImportParcels++;
	        }
        }

        public void IncreaseImportedConstructionsCount()
        {
	        lock (_locked)
	        {
                CountImportConstructions++;
	        }
        }

        public void IncreaseImportedUncomplitedsCount()
        {
	        lock (_locked)
	        {
                CountImportUncompliteds++;
	        }
        }

        public void IncreaseImportedFlatsCount()
        {
	        lock (_locked)
	        {
		        CountImportFlats++;
	        }
        }

        public void IncreaseImportedCarPlacesCount()
        {
	        lock (_locked)
	        {
                CountImportCarPlaces++;
	        }
        }
    }
}
