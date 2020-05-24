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
using Core.SRD;
using KadOzenka.Dal.LongProcess;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.BlFrontEnd
{

	class Program
	{

		static void Main(string[] args)
		{
			BuildQsXml.BuildSudApproveStatus();
			SpreadsheetInfo.SetLicense("ERDD-TNCL-YKZ5-3ZTU");
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
            consoleHelper.AddCommand("1101", "Запуск выгрузки объявлений объектов-аналогов из RestApp", () => { new Data().Detect(); });
            consoleHelper.AddCommand("1102", "Запуск выгрузки объявлений объектов-аналогов с сайта Яндекс-Недвижимость", () => { new YandexChecker().FormMarketObjects(); });
		    consoleHelper.AddCommand("194", "Запуск выгрузки объявлений объектов-аналогов с Avito", () => { new AvitoParsingService().ParseAllObjects(); });

            consoleHelper.AddCommand("1103", "Присвоение адресов не обработанным объектам сторонних маркетов", () => { new Addresses().Detect(); });
            consoleHelper.AddCommand("1104", "Присвоение кадастровых номеров объектам сторонних маркетов", () => { new KadNumbers().Detect(); });

            consoleHelper.AddCommand("1105", "Процедура обновления цен объектов-аналогов с ЦИАН-а", () => { new Cian().RefreshAllData(15000, true); });
            consoleHelper.AddCommand("1106", "Процедура обновления цен объектов-аналогов с Яндекс недвижимость", () => { new Yandex().RefreshAllData(testBoot: true); });
		    consoleHelper.AddCommand("194-2", "Процедура обновления цен объектов-аналогов с Avito", () => { new Avito().RefreshAllData(testBoot: false); });
            consoleHelper.AddCommand("1107", "Процедура проверки данных на дублирование", () => { new Duplicates().Detect(); });

            /*Вспомогательные функции*/
            consoleHelper.AddCommand("1108", "Присвоение кадастровых кварталов, районов и зон", () => { new Cian().SetCadastralNumbers(); });

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
			consoleHelper.AddCommand("305", "Статискика по объектам недвидимости в Excel", SudExporter.ExportStatObjectExcel);
			consoleHelper.AddCommand("306", "Статистика по положительным судебным решениям в Excel", SudExporter.ExportStatCheckExcel);
			consoleHelper.AddCommand("350", "Импорт данных решений комиссий (БД)", CommissionExporter.DoLoadBd);
			consoleHelper.AddCommand("351", "Импорт данных решений комиссий (Excel)", CommissionExporter.DoLoadExcel);
            consoleHelper.AddCommand("362", "Экспорт в Xml - КОценка по исходящим документам.", ExporterKO.ExportXmlRD);
            consoleHelper.AddCommand("363", "Экспорт в Xml - КОценка для ВУОН.", ExporterKO.ExportXmlVUON);
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
				
				List<IO.Swagger.Model.RRDataLoadModel> result = service.RosreestrDataGetRRData(DateTime.Today.AddDays(-1), DateTime.Today);

                Console.WriteLine($"Из РЕОН получено заданий: {result.Count}");
				Console.WriteLine(String.Join("\n", result.Select(x => $"{x.DocNumber} от {x.DocDate.Value.ToShortDateString()}")));
			});

            consoleHelper.AddCommand("902", "Тест Сервиса для создания задач на основе данных из РЕОН", () =>
            {
                new KoTaskFromReon().StartProcess(null,
                    new OMQueue { UserId = SRDSession.Current.UserID },
                    new System.Threading.CancellationToken());
            });

            consoleHelper.AddCommand("903", "Тест Сервиса для получения графических факторов из РЕОН", () =>
            {
                var taskId = 44354853;
                new KoFactorsFromReon().StartProcess(null,
                    new OMQueue {ObjectId = taskId, UserId = SRDSession.Current.UserID }, 
                    new System.Threading.CancellationToken());
            });

            consoleHelper.AddCommand("554", "эксель импорт", 
				() => new DataImporterCommon().StartProcess(null, 
					new ObjectModel.Core.LongProcess.OMQueue { ObjectId = 41980095 }, 
					new System.Threading.CancellationToken()));
						
			consoleHelper.AddCommand("5551", "Корректировка на этажность",
				() => 
				{
					var q = ObjectModel.Core.LongProcess.OMQueue.Where(x => x.Id == 42661016).SelectAll().ExecuteFirstOrDefault();
					new Dal.LongProcess.CorrectionByStageForMarketObjectsLongProcess().StartProcess(null,
					 q,
					 new System.Threading.CancellationToken());
				});


			//consoleHelper.AddCommand("555", "Корректировка на этажность", () => new Dal.Correction.CorrectionByStageService().MakeCorrection(new DateTime(2020, 3, 1)));
		}

	}

}