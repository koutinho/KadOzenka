using Platform.Shared;
using GemBox.Spreadsheet;
using Core.Register.LongProcessManagment;
using KadOzenka.BlFrontEnd.ExportKO;
using KadOzenka.BlFrontEnd.ExportSud;
using KadOzenka.BlFrontEnd.ExportMSSQL;
using KadOzenka.BlFrontEnd.ExportCommission;
using KadOzenka.BlFrontEnd.SudTests;
using System.Collections.Generic;
using System.IO;
using KadOzenka.Dal.XmlParser;
using System;
using KadOzenka.BlFrontEnd.GbuTest;
using KadOzenka.Dal.ExcelParser;
using KadOzenka.WebClients.ReonClient.Api;
using System.Linq;
using System.Threading;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using ObjectModel.Core.LongProcess;
using ObjectModel.SPD;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using CommonSdks.RecycleBin;
using Core.Main.FileStorages;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.GbuObject.Entities;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess.DataImport;
using KadOzenka.Dal.LongProcess.Modeling;
using KadOzenka.Dal.LongProcess.Modeling.InputParameters;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports.ResultComposition;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Support;
using KadOzenka.Dal.LongProcess.TaskLongProcesses;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Tours.Repositories;
using ObjectModel.Directory;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using ObjectModel.Common;
using ObjectModel.Directory.Core.LongProcess;
using Platform.Web.Services.BackgroundExporterScheduler;
using Microsoft.Extensions.Configuration;
using ModelingBusiness.Modeling.Entities;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.BlFrontEnd
{
	class Program
	{
		static void Main(string[] args)
		{
			var configuration = new ConfigurationBuilder()
			   .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
			   .AddEnvironmentVariables()
			   .Build();

			Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(configuration)
			.CreateLogger();

			Log.Information("Starting KadOzenka.BlFrontEnd");

			BuildQsXml.BuildSudApproveStatus();
			SpreadsheetInfo.SetLicense("ERDD-TNCL-YKZ5-3ZTU");

			//Добавляет поддержку кодировок 1251, 866
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			var consoleHelper = new BlFrontEndConsoleHelper();
			InitCommands(consoleHelper);
			consoleHelper.Run();
		}

		private static void InitCommands(BlFrontEndConsoleHelper consoleHelper)
		{
			consoleHelper.AddCommand("2", "Запуск службы выполнения фоновых процессов", () =>
			{

				Log.Information("Запуск службы выполнения фоновых процессов");
				LongProcessManagementService service = new LongProcessManagementService();
				service.Start();
			});

			/*Загрузка информации по сделкам росреестра из excel*/
			//consoleHelper.AddCommand("1001", "Загрузка объектов росреестра из Excel", ObjectReplicationExcelProcess.UploadRosreestrObjectsToDatabase);
			//consoleHelper.AddCommand("1002", "Присвоение координат объектам росреестра из базы данных", () => { new ObjectReplicationExcelProcess().SetRRFDBCoordinatesByYandex(); });

            //consoleHelper.AddCommand("1103", "Присвоение адресов не обработанным объектам сторонних маркетов", () => { new Addresses().Detect(); });
            //consoleHelper.AddCommand("1104", "Присвоение кадастровых номеров объектам сторонних маркетов", () => { new KadNumbers().Detect(); });

   //         consoleHelper.AddCommand("11044", "Получение дополнительных данных из ГБУ части", () =>
			//{
			//	new AddingMissingDataFromGbuPartProc().PerformProc(false);
			//});

            //consoleHelper.AddCommand("1107", "Процедура проверки данных на дублирование", () => { new Duplicates().Detect(); });

            /*Вспомогательные функции*/
            consoleHelper.AddCommand("1109", "Сгенерировать перечисления (источник данных)", () => { new InsertGenerator().GenerateInsertData("INSERT INTO core_reference_item (itemid, referenceid, code, value, name) VALUES ({0}, {1}, {2}, '{3}', '{4}');", 1514, 5, 101); });

			consoleHelper.AddCommand("19", "Парсинг XML файла", () => { XMLToJSPolyLine.parseXMLMapGeoData(); });
            consoleHelper.AddCommand("21", "Парсинг excele файла", () => { FormRegionTable.parseExcelRegionsData(); });

            /*Генерация тайлов для карт яндекс*/
            consoleHelper.AddCommand("1901", "Генерация JSON файлов с пиксельными координатами", () => { new CoordinatesConverter().GenerateInitialCoordinates(); });
			consoleHelper.AddCommand("1902", "Генерация тайлов для карты", () => { new CoordinatesConverter().GenerateInitialImages(); });

			//consoleHelper.AddCommand("5", "Загрузка словаря с кадастровыми номерами из Excel", ObjectReplicationExcelProcess.StartImport);

			//consoleHelper.AddCommand("16", "Выгрузка кад. номеров в excel по первоначальным адресам",
			//	() =>
			//	{
			//		ObjectReplicationExcelProcess.SetCadastralNumber(
			//			ConfigurationManager.AppSettings["InitialAddressFile"],
			//			ConfigurationManager.AppSettings["DefaultExceleValue"]);
			//	});
			//consoleHelper.AddCommand("17", "Сформировать файл с выгрузкой адресов росреестра", () => { new ObjectReplicationExcelProcess().FormFile(ConfigurationManager.AppSettings["GroupedAddressesFile"]); });
			//consoleHelper.AddCommand("18", "Присвоение координат объектам росреестра из файла", () => { new ObjectReplicationExcelProcess().SetRRCoordinatesByYandex(ConfigurationManager.AppSettings["GroupedAddressesFile"]); });

			consoleHelper.AddCommand("30", "Тест получения значения атрибутов ГБУ", GbuTests.TestGetDataFromAllpri);

			//consoleHelper.AddCommand("291", "Рассчет", MSExporter.GetCalcGroup);
			consoleHelper.AddCommand("292", "История", () =>
			{ //36855837

				ObjectModel.KO.OMUnit tmp = ObjectModel.KO.OMUnit.Where(x => x.Id == 36855837).SelectAll().ExecuteFirstOrDefault();

				List<ObjectModel.KO.HistoryUnit> histories = ObjectModel.KO.HistoryUnit.GetHistory(tmp);
				//List<ObjectModel.KO.HistoryUnit> histories = ObjectModel.KO.HistoryUnit.GetHistory("77:17:0100302:62");
				foreach (ObjectModel.KO.HistoryUnit history in histories) Console.WriteLine(history.ToString());
			});

			consoleHelper.AddCommand("300", "Импорт данных судебной подсистемы (БД)", SudExporter.DoLoadBd);
			consoleHelper.AddCommand("301", "Импорт данных судебной подсистемы (Excel)", SudExporter.DoLoadExcel);
			consoleHelper.AddCommand("302", "Экспорт данных судебной подсистемы в Xml", SudExporter.ExportXml);
			consoleHelper.AddCommand("303", "Экспорт данных судебной подсистемы в Excel", SudExporter.ExportExcel);
			consoleHelper.AddCommand("304", "Статистика сводная в Excel", SudExporter.ExportStatExcel);
			consoleHelper.AddCommand("305", "Статискика по объектам недвижимости в Excel", SudExporter.ExportStatObjectExcel);
			consoleHelper.AddCommand("306", "Статистика по положительным судебным решениям в Excel", SudExporter.ExportStatCheckExcel);
			consoleHelper.AddCommand("350", "Импорт данных решений комиссий (БД)", CommissionExporter.DoLoadBd);
			consoleHelper.AddCommand("351", "Импорт данных решений комиссий (Excel)", CommissionExporter.DoLoadExcel);
            consoleHelper.AddCommand("362", "Экспорт в Xml - КОценка по исходящим документам.", ExporterKO.ExportXmlRD);
            consoleHelper.AddCommand("363", "Экспорт в Xml - КОценка для ВУОН.", ExporterKO.ExportXmlVUON);
            consoleHelper.AddCommand("364", "Экспорт в Word - Ответные документы по объектам.", ExporterKO.ExportDocOtvet);
            consoleHelper.AddCommand("365", "ExportXlsTable4", ExporterKO.ExportXlsTable4);
            consoleHelper.AddCommand("390", "Тест API судебной подсистемы", SudTestApi.TestAll);

            //consoleHelper.AddCommand("161-1", "Привязка к объектам аналогам кадастровых кварталов", () =>
            //{
	           // var filler = new MarketObjectsCadastralInfoFiller();
	           // filler.PerformFillingCadastralQuarterProc();
            //});
            //consoleHelper.AddCommand("161-2",
	           // "Привязка к объектам аналогам информации о зонах, округах и районах по кадастровому кварталу", () =>
	           // {
		          //  var filler = new MarketObjectsCadastralInfoFiller();
		          //  filler.PerformFillingCadastralInfoByQuarterProc();
	           // });

			consoleHelper.AddCommand("900", "Test Configuration.GetFileStream", () =>
			{
				FileStream fileStream = Core.ConfigParam.Configuration.GetFileStream("CCCReport", ".frx", "Reports");

			});

			consoleHelper.AddCommand("901", "Тест API РЕОН", () =>
			{
				var service = new RosreestrDataApi();

				//Console.WriteLine("Введите дату, на которую нужно загрузить задания");
				//DateTime date = Console.ReadLine().ParseToDateTime();
				var date = DateTime.Today;

				List<IO.Swagger.Model.RRDataLoadModel> result = service.RosreestrDataGetRRData(date, date.AddDays(1));

                Console.WriteLine($"Из РЕОН получено заданий: {result.Count}");
				Console.WriteLine(String.Join("\n", result.Select(x => $"{x.DocNumber} от {x.DocDate.Value.ToShortDateString()}")));
			});

			consoleHelper.AddCommand("901-2", "Загрузка заданий из РЕОН", () =>
			{
				//Console.WriteLine("Введите дату, на которую нужно загрузить задания");
				//DateTime date = Console.ReadLine().ParseToDateTime();

				var date = DateTime.Today;

				var processType = OMProcessType.Where(x => x.ProcessName == "KoTaskFromReon").SelectAll().ExecuteFirstOrDefault();

				processType.Parameters = date.ToString("dd.MM.yyyy HH:mm:ss");

				OMQueue omQueue = new OMQueue
				{
					Status_Code = ObjectModel.Directory.Core.LongProcess.Status.Added,
					ProcessTypeId = processType.Id,
					CreateDate = DateTime.Now,
					LastCheckDate = DateTime.Now,
					UserId = SRDSession.GetCurrentUserId()
				};

				new KoTaskFromReon().StartProcess(processType, omQueue, new CancellationToken());
			});

			consoleHelper.AddCommand("902", "Тест Сервиса для создания задач на основе данных из РЕОН", () =>
            {
                new KoTaskFromReon().StartProcess(null,
                    new OMQueue
                    {
                        UserId = SRDSession.Current.UserID,
                        Status_Code = Status.Added
                    },
                    new CancellationToken());
            });

            consoleHelper.AddCommand("903", "Тест Сервиса для получения графических факторов из РЕОН", () =>
            {
                var taskId = 44354853;
				var attributes = new RegisterAttributeService()
					.GetActiveRegisterAttributes(KoFactorsFromReon.ReonSourceRegisterId)
					.Select(x => x.Id).ToList();

				var inputParameters = new KoFactorsFromReonInputParameters
				{
					TaskId = taskId,
					AttributeIds = attributes
				};

				new KoFactorsFromReon().StartProcess(null,
                    new OMQueue
                    {
                        ObjectId = taskId,
                        UserId = SRDSession.Current.UserID,
                        Status_Code = Status.Added,
						Parameters = inputParameters.SerializeToXml()
					},
                    new CancellationToken());
            });

            consoleHelper.AddCommand("904", "Моделирование (обучение)", () =>
            {
                var trainingInputParameters = new GeneralModelingInputParameters
                {
                    ModelId = 46847140,
                    ModelType = KoAlgoritmType.Line
                };
                var inputRequest = new ModelingInputParameters
                {
                    Mode = ModelingMode.Training,
                    InputParametersXml = trainingInputParameters.SerializeToXml()
                };
                new ModelingProcess().StartProcess(new OMProcessType(), new OMQueue
                {
                    UserId = SRDSession.GetCurrentUserId(),
                    Parameters = inputRequest.SerializeToXml()
                }, new CancellationToken());
            });

			consoleHelper.AddCommand("905", "Тест получения данных по заявителю из заявки СПД", () =>
			{
				var id = 106941603;
				var spdRequest = OMRequestRegistration.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

				DataSet dataSet = new DataSet();

				System.IO.StringReader xmlSR = new System.IO.StringReader(spdRequest.CustomXML);

				dataSet.ReadXml(xmlSR, XmlReadMode.ReadSchema);

				var row = dataSet.Tables["AppApplicants"].Rows[0];

				string name = row["NAME"].ToString();
				string SNAME = row["SNAME"].ToString();
				string FNAME = row["FNAME"].ToString();
				string MNAME = row["MNAME"].ToString();
			});

			consoleHelper.AddCommand("554", "эксель импорт",
				() => new DataImporterByTemplateLongProcess().StartProcess(null,
					new ObjectModel.Core.LongProcess.OMQueue { ObjectId = 41980095 },
					new System.Threading.CancellationToken()));

			//consoleHelper.AddCommand("554", "эксель импорт", 
			//	() => new DataImporterCommon().StartProcess(null, 
			//		new ObjectModel.Core.LongProcess.OMQueue { ObjectId = 41980095 }, 
			//		new System.Threading.CancellationToken()));

			consoleHelper.AddCommand("557", "Фоновая выгрузка отчетов/раскладок по кастомному пути (процесс из платформы)", () =>
            {
                new BackgroundExporterLongProcess().StartProcess(new OMProcessType(), new OMQueue
                {
                    Status_Code = Status.Added,
                    UserId = SRDSession.GetCurrentUserId()
                }, new CancellationToken());
            });

            consoleHelper.AddCommand("558", "Перенос атрибутов", () =>
            {
	            var queue = new OMQueue
	            {
		            Status_Code = Status.Added,
		            UserId = SRDSession.GetCurrentUserId()
	            };
	            var tasks = new List<long> {15534573};
	            var attributes = new List<ExportAttributeItem>
		            {new ExportAttributeItem {IdAttributeGBU = 600, IdAttributeKO = 25118600}};
            

	            new ExportAttributeToKO().Run(new GbuExportAttributeSettings
	            {
		            TaskFilter = tasks,
		            Attributes = attributes,
		            ObjType = ObjectTypeExtended.Zu,
		            OksAdditionalFilters = new OksAdditionalFilters
		            {
			            IsBuildings = true
		            }
	            }, queue);
	            new ExportAttributeToKO().Run(new GbuExportAttributeSettings
				{
					TaskFilter = tasks,
					Attributes = attributes,
					ObjType = ObjectTypeExtended.Oks
				}, queue);
	            new ExportAttributeToKO().Run(new GbuExportAttributeSettings
				{
					TaskFilter = tasks,
					Attributes = attributes,
					ObjType = ObjectTypeExtended.Oks,
					OksAdditionalFilters = new OksAdditionalFilters
					{
						IsBuildings = true
					}
				}, queue);
	            new ExportAttributeToKO().Run(new GbuExportAttributeSettings
				{
					TaskFilter = tasks,
					Attributes = attributes,
					ObjType = ObjectTypeExtended.Both,
					OksAdditionalFilters = new OksAdditionalFilters
					{
						IsBuildings = true,
						IsPlacements = true
					}
				}, queue);
	            new ExportAttributeToKO().Run(new GbuExportAttributeSettings
				{
					TaskFilter = tasks,
					Attributes = attributes,
					ObjType = ObjectTypeExtended.Both
				}, queue);
			});

            consoleHelper.AddCommand("560", "Тест сервиса отчетов", () =>
            {
	            long reportId = -1;
	            using (var reportService = new GbuReportService("Test"))
	            {
		            var numberOfColumns = 2;
		            var numberOfRows = 200;
		            var columns = Enumerable.Range(0, numberOfColumns).Select(x => new GbuReportService.Column
		            {
			            Header = $"Header {x}",
			            Index = x,
			            Width = 2
		            }).ToList();

		            Enumerable.Range(0, numberOfRows).ForEach(x =>
		            {
			            var row = reportService.GetCurrentRow();
			            columns.ForEach(column =>
			            {
				            reportService.AddValue($"value {x}.{column.Index}", column.Index, row);
			            });
		            });

		            reportId = reportService.SaveReport();
				} 


				var export = OMExportByTemplates
					.Where(x => x.Id == reportId)
					.SelectAll()
					.Execute()
					.FirstOrDefault();

				var pathToFile = FileStorageManager.GetFullFileName(DataExporterCommon.FileStorageName,
					export.DateFinished.Value, export.ResultFileName);
            });


            consoleHelper.AddCommand("561", "Тест проставления оценочной группы", () =>
            {
	            var taskId = 15534573;
				new UpdateEvaluativeGroupLongProcess().StartProcess(new OMProcessType(), new OMQueue
				{
					Status_Code = Status.Added,
					UserId = SRDSession.GetCurrentUserId(),
					ObjectId = taskId
				}, new CancellationToken());
			});

            consoleHelper.AddCommand("562", "Тест длительного процесса для начального наполнения данными отчетов с характеристиками объектов", () =>
            {
				new InitialReportTableFiller().StartProcess(new OMProcessType(), new OMQueue
				{
					Status_Code = Status.Added,
					UserId = SRDSession.GetCurrentUserId()
				}, new CancellationToken());

				////TODO тестирование отмены процесса
				//var cancelSource = new CancellationTokenSource();
				//var cancelToken = cancelSource.Token;
				//Task.Factory.StartNew(() =>
				//{
				//	Thread.Sleep(300000);
				//	cancelSource.Cancel();
				//});
				//new DataCompositionByCharacteristicsReportsLongProcessViaTables().StartProcess(new OMProcessType(), new OMQueue
				//{
				//	Status_Code = Status.Added,
				//	UserId = SRDSession.GetCurrentUserId()
				//}, cancelToken);
			});


            consoleHelper.AddCommand("18122020", "Исправление типа ГБУ объектов на кадастровый квартал", () =>
            {
	            ChangeTypeToCadastralQuarter.Perform("C:\\Genix\\data1.xlsx");
            });


            consoleHelper.AddCommand("563", "Создание триггеров для поддержания актуальности кеш-таблицы для отчетов с составом данных", () =>
            {
				TriggerCreationForDataCompositionReports.Start();
            });

            consoleHelper.AddCommand("564", "Удаление моделей, которые остались после некорректного удаления групп и туров", () =>
            {
	            var groupIds = OMGroup.Where(x => true).Execute().Select(x => x.Id).ToList();

	            var tourService = new TourService(new TourFactorService(), new GroupService(),  new RecycleBinService(), new TourRepository());
	            var groupService = new GroupService();

				var modelsWithNotExistedGroups = OMModel.Where(x => !groupIds.Contains((long) x.GroupId)).SelectAll().Execute().ToList();
				//var modelIdsToDelete = modelsWithNotExistedGroups.Select(x => x.Id).ToList();
				var notExistedGroupIds = modelsWithNotExistedGroups.Select(x => x.GroupId.Value).Distinct().ToList();
				if (notExistedGroupIds.Count > 0)
				{
					var tourToGroupRelations = OMTourGroup.Where(x => notExistedGroupIds.Contains(x.GroupId)).SelectAll().Execute();
					var possibleDeletedTourIds = tourToGroupRelations.Select(x => x.TourId).Distinct().ToList();
					if (possibleDeletedTourIds.Count > 0)
					{
						possibleDeletedTourIds.ForEach(x =>
						{
							tourService.DeleteTour(x);
						});
					}

					notExistedGroupIds.ForEach(x =>
					{
						groupService.DeleteGroup(x);
					});
				}

				var tours = OMTour.Where(x => true).Execute().Select(x => x.Id).ToList();
				var deletedTourIds = OMTourGroup.Where(x => !tours.Contains(x.TourId)).SelectAll().Execute().Select(x => x.TourId).Distinct().ToList();
				deletedTourIds.ForEach(x =>
				{
					tourService.DeleteTour(x);
				});
            });


            consoleHelper.AddCommand("565", "Тест длительного процесса для начального наполнения данными отчетов с характеристиками объектов", () =>
            {
				//TODO тестирование отмены процесса
				var cancelSource = new CancellationTokenSource();
				var cancelToken = cancelSource.Token;
				Task.Factory.StartNew(() =>
				{
					Thread.Sleep(5000);
					cancelSource.Cancel();
				});
				new UniformReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
				{
					Status_Code = Status.Added,
					UserId = SRDSession.GetCurrentUserId(),
					Parameters = new ReportLongProcessOnlyTasksInputParameters {TaskIds = new List<long>{ 15534573 }}.SerializeToXml()
				}, cancelToken);
			});

            consoleHelper.AddCommand("566", "Тестирование отмены нормализации", () =>
            {
	            //TODO тестирование отмены процесса
	            var cancelSource = new CancellationTokenSource();
	            var cancelToken = cancelSource.Token;
	            Task.Factory.StartNew(() =>
	            {
		            Thread.Sleep(5000);
		            cancelSource.Cancel();
	            });
	            new SetPriorityGroupProcess().StartProcess(new OMProcessType(), new OMQueue
	            {
		            Status_Code = Status.Added,
		            UserId = SRDSession.GetCurrentUserId(),
		            Parameters = new GroupingSettings().SerializeToXml()
	            }, cancelToken);
            });


            consoleHelper.AddCommand("567", "Тестирование сервиса для получения ГБУ-атрибутов", () =>
            {
	            var objectIds = new List<long> {53556156};
	            var date = DateTime.Now.GetEndOfTheDay();
	            var selectedColumns = new List<GbuColumnsToDownload>();
				//a.Add(GbuObjectService.GbuObjectAttributeToDownload.Ot);

				var attributes1 = new GbuObjectService().GetAllAttributes(objectIds, sources: null, inputAttributes: new List<long> {8}, date);

				var attributes2 = new GbuObjectService().GetAllAttributes(objectIds, sources: null, inputAttributes: new List<long> {8, 545}, date, attributesToDownload: selectedColumns);

				selectedColumns.Add(GbuColumnsToDownload.DocumentId);
				selectedColumns.Add(GbuColumnsToDownload.S);
				selectedColumns.Add(GbuColumnsToDownload.Ot);
				selectedColumns.Add(GbuColumnsToDownload.Value);
				var attributes3 = new GbuObjectService().GetAllAttributes(objectIds, sources: null, inputAttributes: new List<long> {8, 545 }, date, attributesToDownload: selectedColumns);
            });


			//consoleHelper.AddCommand("555", "Корректировка на этажность", () => new Dal.Correction.CorrectionByStageService().MakeCorrection(new DateTime(2020, 3, 1)));
		}
    }
}