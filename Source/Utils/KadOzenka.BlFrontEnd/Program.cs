﻿using Platform.Shared;
using GemBox.Spreadsheet;
using KadOzenka.Dal.AddressChecker;
using KadOzenka.Dal.DuplicateCleaner;
using KadOzenka.Dal.KadNumberChecker;
using Core.Register.LongProcessManagment;
using KadOzenka.BlFrontEnd.DataExport;
using KadOzenka.BlFrontEnd.ObjectReplicationExcel;
using KadOzenka.Dal.RestAppParser;
using KadOzenka.Dal.Selenium.PriceChecker;
using KadOzenka.BlFrontEnd.ExportKO;
using KadOzenka.BlFrontEnd.ExportSud;
using KadOzenka.BlFrontEnd.ExportMSSQL;
using KadOzenka.BlFrontEnd.ExportCommission;
using KadOzenka.BlFrontEnd.SudTests;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.IO;
using KadOzenka.Dal.XmlParser;
using KadOzenka.BlFrontEnd.PostgresToMongo;
using System;
using KadOzenka.BlFrontEnd.GbuTest;
using KadOzenka.BlFrontEnd.DataImport;
using KadOzenka.Dal.AvitoParsing;
using KadOzenka.Dal.CadastralInfoFillingForMarketObjects;
using KadOzenka.Dal.YandexParser;
using KadOzenka.Dal.ExcelParser;
using KadOzenka.Dal.DataImport;
using KadOzenka.WebClients.ReonClient.Api;
using System.Linq;
using System.Threading;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Entities;
using ObjectModel.Core.LongProcess;
using ObjectModel.SPD;
using System.Data;
using System.Text;
using KadOzenka.BlFrontEnd.ExpressScore;
using KadOzenka.Dal.AddingMissingDataFromGbuPart;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Selenium.FillingAdditionalFields;
using KadOzenka.Dal.YandexParsing;
using ObjectModel.Directory.Core.LongProcess;
using Platform.Web.Services.BackgroundExporterScheduler;

namespace KadOzenka.BlFrontEnd
{

	class Program
	{

		static void Main(string[] args)
		{
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
				LongProcessManagementService service = new LongProcessManagementService();
				service.Start();
			});

			/*Загрузка информации по сделкам росреестра из excel*/
			consoleHelper.AddCommand("1001", "Загрузка объектов росреестра из Excel", ObjectReplicationExcelProcess.UploadRosreestrObjectsToDatabase);
			consoleHelper.AddCommand("1002", "Присвоение координат объектам росреестра из базы данных", () => { ObjectReplicationExcelProcess.SetRRFDBCoordinatesByYandex(); });

            /*Загрузка информации по предложениям из ЦИАН-а через RestApp*/
            consoleHelper.AddCommand("1101", "Запуск выгрузки объявлений объектов-аналогов из RestApp", () => {
				string[] logins = ConfigurationManager.AppSettings["restAppLogins"].Split(','),
						 tokens = ConfigurationManager.AppSettings["restAppTokens"].Split(',');
				for (int i = 0; i < logins.Length; i++) new Data(logins[i], tokens[i]).Detect();
			});
            consoleHelper.AddCommand("1102", "Запуск выгрузки объявлений объектов-аналогов с сайта Яндекс-Недвижимость", () => { new YandexParser().FormMarketObjects(); });
		    consoleHelper.AddCommand("194", "Запуск выгрузки объявлений объектов-аналогов с Avito", () => { new AvitoParsingService().ParseAllObjects(); });

            consoleHelper.AddCommand("1103", "Присвоение адресов не обработанным объектам сторонних маркетов", () => { new Addresses().Detect(); });
            consoleHelper.AddCommand("1104", "Присвоение кадастровых номеров объектам сторонних маркетов", () => { new KadNumbers().Detect(); });

            consoleHelper.AddCommand("11041", "Парсинг дополнительных данных для Циан", () =>
	            {
		            new CianFilling().FillAdditionalData(false);
            });

			consoleHelper.AddCommand("11042", "Парсинг дополнительных данных для Яндекс недвижимость", () =>
            {
	            new YandexFilling().FillAdditionalData(false);
			});

			consoleHelper.AddCommand("11044", "Получение дополнительных данных из ГБУ части", () =>
			{
				new AddingMissingDataFromGbuPartProc().PerformProc(false);
			});

			consoleHelper.AddCommand("1105", "Процедура обновления цен объектов-аналогов с ЦИАН-а", () => { new Cian().RefreshAllData(15000, true); });
            consoleHelper.AddCommand("1106", "Процедура обновления цен объектов-аналогов с Яндекс недвижимость", () => { new Yandex().RefreshAllData(testBoot: true); });
		    consoleHelper.AddCommand("194-2", "Процедура обновления цен объектов-аналогов с Avito", () => { new Avito().RefreshAllData(testBoot: false); });
            consoleHelper.AddCommand("1107", "Процедура проверки данных на дублирование", () => { new Duplicates().Detect(); });

			consoleHelper.AddCommand("1111", "", () => { new ParserProcess().StartProcess(null, null, new System.Threading.CancellationToken()); });

            /*Вспомогательные функции*/
            consoleHelper.AddCommand("1108", "Присвоение кадастровых кварталов, районов и зон", () => { new Cian().SetCadastralNumbers(); });

			consoleHelper.AddCommand("1109", "Сгенерировать перечисления (источник данных)", () => { new InsertGenerator().GenerateInsertData("INSERT INTO core_reference_item (itemid, referenceid, code, value, name) VALUES ({0}, {1}, {2}, '{3}', '{4}');", 1514, 5, 101); });

			consoleHelper.AddCommand("19", "Парсинг XML файла", () => { XMLToJSPolyLine.parseXMLMapGeoData(); });
            consoleHelper.AddCommand("21", "Парсинг excele файла", () => { FormRegionTable.parseExcelRegionsData(); });

            /*Генерация тайлов для карт яндекс*/
            consoleHelper.AddCommand("1901", "Генерация JSON файлов с пиксельными координатами", () => { new CoordinatesConverter().GenerateInitialCoordinates(); });
			consoleHelper.AddCommand("1902", "Генерация тайлов для карты", () => { new CoordinatesConverter().GenerateInitialImages(); });

			consoleHelper.AddCommand("5", "Загрузка словаря с кадастровыми номерами из Excel", ObjectReplicationExcelProcess.StartImport);

			consoleHelper.AddCommand("10", "Экспорт данных в Excel на основе шаблона", DataExportConsole.ExportData);
			consoleHelper.AddCommand("11", "Импорт данных в Excel из шаблона", DataImportConsole.ImportData);
			consoleHelper.AddCommand("14", "Тест скриншот", () => { new Cian().Test(100); });

			consoleHelper.AddCommand("16", "Выгрузка кад. номеров в excel по первоначальным адресам",
				() =>
				{
					ObjectReplicationExcelProcess.SetCadastralNumber(
						ConfigurationManager.AppSettings["InitialAddressFile"],
						ConfigurationManager.AppSettings["DefaultExceleValue"]);
				});
			consoleHelper.AddCommand("17", "Сформировать файл с выгрузкой адресов росреестра", () => { ObjectReplicationExcelProcess.FormFile(ConfigurationManager.AppSettings["GroupedAddressesFile"]); });
			consoleHelper.AddCommand("18", "Присвоение координат объектам росреестра из файла", () => { ObjectReplicationExcelProcess.SetRRCoordinatesByYandex(ConfigurationManager.AppSettings["GroupedAddressesFile"]); });
			consoleHelper.AddCommand("20", "Тест конвертации из Postgres в Mongo", () => { ConvertToMongo.Convert(20000); });

			consoleHelper.AddCommand("30", "Тест получения значения атрибутов ГБУ", GbuTests.TestGetDataFromAllpri);

			consoleHelper.AddCommand("200", "Импорт данных KO (БД) Модель 2016", MSExporter.DoLoadBd2016Model);
			consoleHelper.AddCommand("201", "Импорт данных KO (БД) Объекты и факторы 2016 ОНС", MSExporter.DoLoadBd2016Unit_Uncomplited);
			consoleHelper.AddCommand("202", "Импорт данных KO (БД) Объекты и факторы 2016 Сооружения", MSExporter.DoLoadBd2016Unit_Construction);
			consoleHelper.AddCommand("203", "Импорт данных KO (БД) Объекты и факторы 2016 Здания", MSExporter.DoLoadBd2016Unit_Building);
			consoleHelper.AddCommand("204", "Импорт данных KO (БД) Объекты и факторы 2016 Помещения", MSExporter.DoLoadBd2016Unit_Flat);
			consoleHelper.AddCommand("205", "Импорт данных KO (БД) Объекты и факторы 2016 Участки", MSExporter.DoLoadBd2016Unit_Parcel);
			consoleHelper.AddCommand("206", "Импорт данных KO (XML) 2016", MSExporter.DoLoadXml2016);
			//consoleHelper.AddCommand("207", "Импорт данных KO (БД) Объекты и факторы 2016 Помещения (отсутствующие)", MSExporter.DoLoadBd2016Unit_FlatSkip);

			consoleHelper.AddCommand("210", "Импорт данных KO (БД) Модель 2018", MSExporter.DoLoadBd2018Model);
			consoleHelper.AddCommand("211", "Импорт данных KO (БД) Объекты и факторы 2018 ОНС", MSExporter.DoLoadBd2018Unit_Uncomplited);
			consoleHelper.AddCommand("212", "Импорт данных KO (БД) Объекты и факторы 2018 Сооружения", MSExporter.DoLoadBd2018Unit_Construction);
			consoleHelper.AddCommand("213", "Импорт данных KO (БД) Объекты и факторы 2018 Здания", MSExporter.DoLoadBd2018Unit_Building);
			consoleHelper.AddCommand("214", "Импорт данных KO (БД) Объекты и факторы 2018 Помещения", MSExporter.DoLoadBd2018Unit_Flat);
			consoleHelper.AddCommand("215", "Импорт данных KO (БД) Объекты и факторы 2018 Участки", MSExporter.DoLoadBd2018Unit_Parcel);
			consoleHelper.AddCommand("216", "Импорт данных KO (XML) 2018", MSExporter.DoLoadXml2018);

			//consoleHelper.AddCommand("217", "Импорт данных KO (БД) Модель 2018 (метки, модель)", MSExporter.DoLoadBd2018ModelAdd);

			consoleHelper.AddCommand("218", "Импорт данных KO (БД) Эталонные объекты", MSExporter.LoadGroupEtalonParcel_2018);
			consoleHelper.AddCommand("219", "Импорт данных KO (БД) ВУОН Земля", MSExporter.DoLoadBd2018Unit_Parcel_VUON);
			consoleHelper.AddCommand("220", "Импорт данных KO (БД) ВУОН Здания", MSExporter.DoLoadBd2018Unit_Build_VUON);
			consoleHelper.AddCommand("221", "Импорт данных KO (БД) ВУОН Сооружения", MSExporter.DoLoadBd2018Unit_Construction_VUON);
			consoleHelper.AddCommand("222", "Импорт данных KO (БД) ВУОН Помещения", MSExporter.DoLoadBd2018Unit_Flat_VUON);
			consoleHelper.AddCommand("223", "Импорт данных KO (БД) ВУОН ОНС", MSExporter.DoLoadBd2018Unit_Uncomplited_VUON);

			consoleHelper.AddCommand("231", "Импорт данных ГБУ(БД) ВУОН Земля", MSExporter.DoLoadBd2018Unit_Parcel_VUON_GKN);
			consoleHelper.AddCommand("232", "Импорт данных ГБУ(БД) ВУОН Здания", MSExporter.DoLoadBd2018Unit_Build_VUON_GKN);
			consoleHelper.AddCommand("233", "Импорт данных ГБУ (БД) ВУОН Сооружения", MSExporter.DoLoadBd2018Unit_Construction_VUON_GKN);
			consoleHelper.AddCommand("234", "Импорт данных ГБУ (БД) ВУОН ОНС", MSExporter.DoLoadBd2018Unit_Uncomplited_VUON_GKN);
			consoleHelper.AddCommand("235", "Импорт данных ГБУ (БД) ВУОН Помещения", MSExporter.DoLoadBd2018Unit_Flat_VUON_GKN);

			consoleHelper.AddCommand("240", "Импорт данных ГБУ(БД) Земля TXT", MSExporter.DoLoadBd_Parcel_GBU_TEXT);
			consoleHelper.AddCommand("241", "Импорт данных ГБУ(БД) Здания TXT", MSExporter.DoLoadBd_Build_GBU_TEXT);
			consoleHelper.AddCommand("242", "Импорт данных ГБУ(БД) Сооружения TXT", MSExporter.DoLoadBd_Construction_GBU_TEXT);
			consoleHelper.AddCommand("243", "Импорт данных ГБУ(БД) ОНС TXT", MSExporter.DoLoadBd_Uncomplited_GBU_TEXT);
			consoleHelper.AddCommand("244", "Импорт данных ГБУ(БД) Помещения TXT", MSExporter.DoLoadBd_Flat_GBU_TEXT);

			consoleHelper.AddCommand("245", "Импорт данных ГБУ(БД) Земля Numeric", MSExporter.DoLoadBd_Parcel_GBU_Numeric);
			consoleHelper.AddCommand("246", "Импорт данных ГБУ(БД) Здания Numeric", MSExporter.DoLoadBd_Build_GBU_Numeric);
			consoleHelper.AddCommand("247", "Импорт данных ГБУ(БД) Сооружения Numeric", MSExporter.DoLoadBd_Construction_GBU_Numeric);
			consoleHelper.AddCommand("248", "Импорт данных ГБУ(БД) ОНС Numeric", MSExporter.DoLoadBd_Uncomplited_GBU_Numeric);
			consoleHelper.AddCommand("249", "Импорт данных ГБУ(БД) Помещения Numeric", MSExporter.DoLoadBd_Flat_GBU_Numeric);

			consoleHelper.AddCommand("250", "Импорт данных ГБУ(БД) Земля Date", MSExporter.DoLoadBd_Parcel_GBU_Date);
			consoleHelper.AddCommand("251", "Импорт данных ГБУ(БД) Здания Date", MSExporter.DoLoadBd_Build_GBU_Date);
			consoleHelper.AddCommand("252", "Импорт данных ГБУ(БД) Сооружения Date", MSExporter.DoLoadBd_Construction_GBU_Date);
			consoleHelper.AddCommand("253", "Импорт данных ГБУ(БД) ОНС Date", MSExporter.DoLoadBd_Uncomplited_GBU_Date);
			consoleHelper.AddCommand("254", "Импорт данных ГБУ(БД) Помещения Date", MSExporter.DoLoadBd_Flat_GBU_Date);

			consoleHelper.AddCommand("255", "Импорт данных ГБУ(БД) Новые факторы Текст (Земля)", MSExporter.DoLoadBd2018Unit_Dop_TEXT_P);
			consoleHelper.AddCommand("256", "Импорт данных ГБУ(БД) Новые факторы Текст (Здания)", MSExporter.DoLoadBd2018Unit_Dop_TEXT_B);
			consoleHelper.AddCommand("257", "Импорт данных ГБУ(БД) Новые факторы Текст (Сооружения)", MSExporter.DoLoadBd2018Unit_Dop_TEXT_C);
			consoleHelper.AddCommand("258", "Импорт данных ГБУ(БД) Новые факторы Текст (ОНС)", MSExporter.DoLoadBd2018Unit_Dop_TEXT_U);
			consoleHelper.AddCommand("259", "Импорт данных ГБУ(БД) Новые факторы Текст (Помещения)", MSExporter.DoLoadBd2018Unit_Dop_TEXT_F);

			consoleHelper.AddCommand("260", "Импорт данных ГБУ(БД) Новые факторы Число (Земля)", MSExporter.DoLoadBd2018Unit_Dop_NUM_P);
			consoleHelper.AddCommand("261", "Импорт данных ГБУ(БД) Новые факторы Число (Здания)", MSExporter.DoLoadBd2018Unit_Dop_NUM_B);
			consoleHelper.AddCommand("262", "Импорт данных ГБУ(БД) Новые факторы Число (Сооружения)", MSExporter.DoLoadBd2018Unit_Dop_NUM_C);
			consoleHelper.AddCommand("263", "Импорт данных ГБУ(БД) Новые факторы Число (ОНС)", MSExporter.DoLoadBd2018Unit_Dop_NUM_U);
			consoleHelper.AddCommand("264", "Импорт данных ГБУ(БД) Новые факторы Число (Помещения)", MSExporter.DoLoadBd2018Unit_Dop_NUM_F);

			consoleHelper.AddCommand("270", "Импорт данных ГБУ(БД) Новые факторы Дата (Земля)", MSExporter.DoLoadBd2018Unit_Dop_Data_P);
			consoleHelper.AddCommand("271", "Импорт данных ГБУ(БД) Новые факторы Дата (Здания)", MSExporter.DoLoadBd2018Unit_Dop_Data_B);
			consoleHelper.AddCommand("272", "Импорт данных ГБУ(БД) Новые факторы Дата (Сооружения)", MSExporter.DoLoadBd2018Unit_Dop_Data_C);
			consoleHelper.AddCommand("273", "Импорт данных ГБУ(БД) Новые факторы Дата (ОНС)", MSExporter.DoLoadBd2018Unit_Dop_Data_U);
			consoleHelper.AddCommand("274", "Импорт данных ГБУ(БД) Новые факторы Дата (Помещения)", MSExporter.DoLoadBd2018Unit_Dop_Data_F);

			consoleHelper.AddCommand("290", "Формула 2016", MSExporter.GetFormulaText);
			consoleHelper.AddCommand("291", "Рассчет", MSExporter.GetCalcGroup);
			consoleHelper.AddCommand("292", "История", () =>
			{
				List<ObjectModel.KO.HistoryUnit> histories = ObjectModel.KO.HistoryUnit.GetHistory("77:17:0100302:62");
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
            consoleHelper.AddCommand("401", "Импорт данных ЦОД из Xml", () =>
			{
				using (FileStream fstream = File.OpenRead("c:\\WORK\\cod1.xml"))
				{
					byte[] array = new byte[fstream.Length];
					fstream.Read(array, 0, array.Length);
					fstream.Seek(0, SeekOrigin.Begin);
					KadOzenka.Dal.DataImport.DataImporterCod.ImportDataCodFromXml(fstream, 2, true);
				}
			});
			consoleHelper.AddCommand("402", "Импорт данных ЦОД из Xml", () =>
			{
				XmlDocument xml = new XmlDocument();
				xml.Load("c:\\WORK\\cod2.xml");
				KadOzenka.Dal.DataImport.DataImporterCod.ImportDataCodFromXml(xml, 2, true);
			});
			consoleHelper.AddCommand("501", "Импорт данных деклараций (Excel)", () => { new DataImporterDeclarationsTest().ImportData(); });

            consoleHelper.AddCommand("161-1", "Привязка к объектам аналогам кадастровых кварталов", () =>
            {
                var filler = new MarketObjectsCadastralInfoFiller();
                filler.PerformFillingCadastralQuarterProc();
            });
		    consoleHelper.AddCommand("161-2", "Привязка к объектам аналогам информации о зонах, округах и районах по кадастровому кварталу", () =>
		    {
		        var filler = new MarketObjectsCadastralInfoFiller();
		        filler.PerformFillingCadastralInfoByQuarterProc();
		    });

			consoleHelper.AddCommand("900", "Test Configuration.GetFileStream", () =>
			{
				FileStream fileStream = Core.ConfigParam.Configuration.GetFileStream("CCCReport", ".frx", "Reports");

			});

			consoleHelper.AddCommand("901", "Тест API РЕОН", () =>
			{
				var service = new RosreestrDataApi();

				Console.WriteLine("Введите дату, на которую нужно загрузить задания");
				DateTime date = Console.ReadLine().ParseToDateTime();

				List<IO.Swagger.Model.RRDataLoadModel> result = service.RosreestrDataGetRRData(date, date.AddDays(1));

                Console.WriteLine($"Из РЕОН получено заданий: {result.Count}");
				Console.WriteLine(String.Join("\n", result.Select(x => $"{x.DocNumber} от {x.DocDate.Value.ToShortDateString()}")));
			});

			consoleHelper.AddCommand("901-2", "Загрузка заданий из РЕОН", () =>
			{
				Console.WriteLine("Введите дату, на которую нужно загрузить задания");
				DateTime date = Console.ReadLine().ParseToDateTime();



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
                new KoFactorsFromReon().StartProcess(null,
                    new OMQueue
                    {
                        ObjectId = taskId,
                        UserId = SRDSession.Current.UserID,
                        Status_Code = Status.Added
                    },
                    new CancellationToken());
            });

            consoleHelper.AddCommand("904", "Моделирование (обучение)", () =>
            {
                var trainingInputParameters = new GeneralModelingInputParameters
                {
                    ModelId = 46847140,
                    ModelType = ModelType.Linear
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
				() => new DataImporterByTemplate().StartProcess(null,
					new ObjectModel.Core.LongProcess.OMQueue { ObjectId = 41980095 },
					new System.Threading.CancellationToken()));

			//consoleHelper.AddCommand("554", "эксель импорт", 
			//	() => new DataImporterCommon().StartProcess(null, 
			//		new ObjectModel.Core.LongProcess.OMQueue { ObjectId = 41980095 }, 
			//		new System.Threading.CancellationToken()));
						
			consoleHelper.AddCommand("5551", "Корректировка на этажность",
				() => 
				{
					var q = ObjectModel.Core.LongProcess.OMQueue.Where(x => x.Id == 42661016).SelectAll().ExecuteFirstOrDefault();
					new Dal.LongProcess.CorrectionByStageForMarketObjectsLongProcess().StartProcess(null,
					 q,
					 new System.Threading.CancellationToken());
				});


            consoleHelper.AddCommand("556", "Корректировка на дату", () =>
            {
                new CorrectionByDateForMarketObjectsLongProcess().StartProcess(new OMProcessType(), new OMQueue
                {
                    Status_Code = Status.Added,
                    UserId = SRDSession.GetCurrentUserId()
                }, new CancellationToken());
            });

            consoleHelper.AddCommand("557", "Фоновая выгрузка отчетов/раскладок по кастомному пути (процесс из платформы)", () =>
            {
                new BackgroundExporterLongProcess().StartProcess(new OMProcessType(), new OMQueue
                {
                    Status_Code = Status.Added,
                    UserId = SRDSession.GetCurrentUserId()
                }, new CancellationToken());
            });

			consoleHelper.AddCommand("559", "Проверка получения данных для грида результатов ЭО", TestServiceES.TestDataResultGrid);
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


			//consoleHelper.AddCommand("555", "Корректировка на этажность", () => new Dal.Correction.CorrectionByStageService().MakeCorrection(new DateTime(2020, 3, 1)));
		}
    }
}