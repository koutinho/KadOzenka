using Platform.Shared;
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
using KadOzenka.Dal.YandexParser;

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
			consoleHelper.AddCommand("1001", "Загрузка объектов росреестра из Excel",
				ObjectReplicationExcelProcess.UploadRosreestrObjectsToDatabase);
			consoleHelper.AddCommand("1002", "Присвоение координат объектам росреестра из базы данных",
				() => { ObjectReplicationExcelProcess.SetRRFDBCoordinatesByYandex(); });

            /*Загрузка информации по предложениям из ЦИАН-а через RestApp*/
            consoleHelper.AddCommand("1101", "Запуск выгрузки объявлений объектов-аналогов из RestApp", () => { new Data().Detect(); });
            consoleHelper.AddCommand("1102", "Присвоение адресов не обработанным объектам сторонних маркетов", () => { new Addresses().Detect(); });
            consoleHelper.AddCommand("1103", "Присвоение кадастровых номеров объектам сторонних маркетов", () => { new KadNumbers().Detect(); });
            consoleHelper.AddCommand("1104", "Процедура обновления цен объектов-аналогов с ЦИАН-а", () => { new Cian().RefreshAllData(15000, true); });
            consoleHelper.AddCommand("1106", "Процедура обновления цен объектов-аналогов с Яндекс недвижимость", () => { new Yandex().RefreshAllData(testBoot: true); });
            consoleHelper.AddCommand("1105", "Процедура проверки данных на дублирование", () => { new Duplicates().Detect(); });

			/*Генерация тайлов для карт яндекс*/
			consoleHelper.AddCommand("1901", "Генерация JSON файлов с пиксельными координатами",
				() => { new CoordinatesConverter().GenerateInitialCoordinates(); });
			consoleHelper.AddCommand("1902", "Генерация тайлов для карты",
				() => { new CoordinatesConverter().GenerateInitialImages(); });

			consoleHelper.AddCommand("5", "Загрузка словаря с кадастровыми номерами из Excel",
				ObjectReplicationExcelProcess.StartImport);

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
			consoleHelper.AddCommand("17", "Сформировать файл с выгрузкой адресов росреестра",
				() =>
				{
					ObjectReplicationExcelProcess.FormFile(ConfigurationManager.AppSettings["GroupedAddressesFile"]);
				});
			consoleHelper.AddCommand("18", "Присвоение координат объектам росреестра из файла",
				() =>
				{
					ObjectReplicationExcelProcess.SetRRCoordinatesByYandex(
						ConfigurationManager.AppSettings["GroupedAddressesFile"]);
				});
			consoleHelper.AddCommand("19", "Парсинг XML файла", () => { XMLToJSPolyLine.parseDistricts(); });
			consoleHelper.AddCommand("20", "Тест конвертации из Postgres в Mongo",
				() => { ConvertToMongo.Convert(20000); });

			consoleHelper.AddCommand("30", "Тест получения значения атрибутов ГБУ", GbuTests.TestGetDataFromAllpri);

			consoleHelper.AddCommand("100",
				"Тест Парсинга объектов-аналогов с сайта Яндекс-Недвижимость (тестовая категория - Покупка офисного помещения)",
				() => { new YandexChecker().FormMarketObjects(); });

			consoleHelper.AddCommand("200", "Импорт данных KO (БД) Модель 2016", MSExporter.DoLoadBd2016Model);
			consoleHelper.AddCommand("201", "Импорт данных KO (БД) Объекты и факторы 2016 ОНС",
				MSExporter.DoLoadBd2016Unit_Uncomplited);
			consoleHelper.AddCommand("202", "Импорт данных KO (БД) Объекты и факторы 2016 Сооружения",
				MSExporter.DoLoadBd2016Unit_Construction);
			consoleHelper.AddCommand("203", "Импорт данных KO (БД) Объекты и факторы 2016 Здания",
				MSExporter.DoLoadBd2016Unit_Building);
			consoleHelper.AddCommand("204", "Импорт данных KO (БД) Объекты и факторы 2016 Помещения",
				MSExporter.DoLoadBd2016Unit_Flat);
			consoleHelper.AddCommand("205", "Импорт данных KO (БД) Объекты и факторы 2016 Участки",
				MSExporter.DoLoadBd2016Unit_Parcel);
			consoleHelper.AddCommand("206", "Импорт данных KO (XML) 2016", MSExporter.DoLoadXml2016);
			consoleHelper.AddCommand("207", "Импорт данных KO (БД) Объекты и факторы 2016 Помещения (отсутствующие)",
				MSExporter.DoLoadBd2016Unit_FlatSkip);

			consoleHelper.AddCommand("210", "Импорт данных KO (БД) Модель 2018", MSExporter.DoLoadBd2018Model);
			consoleHelper.AddCommand("211", "Импорт данных KO (БД) Объекты и факторы 2018 ОНС",
				MSExporter.DoLoadBd2018Unit_Uncomplited);
			consoleHelper.AddCommand("212", "Импорт данных KO (БД) Объекты и факторы 2018 Сооружения",
				MSExporter.DoLoadBd2018Unit_Construction);
			consoleHelper.AddCommand("213", "Импорт данных KO (БД) Объекты и факторы 2018 Здания",
				MSExporter.DoLoadBd2018Unit_Building);
			consoleHelper.AddCommand("214", "Импорт данных KO (БД) Объекты и факторы 2018 Помещения",
				MSExporter.DoLoadBd2018Unit_Flat);
			consoleHelper.AddCommand("215", "Импорт данных KO (БД) Объекты и факторы 2018 Участки",
				MSExporter.DoLoadBd2018Unit_Parcel);
			consoleHelper.AddCommand("216", "Импорт данных KO (XML) 2018", MSExporter.DoLoadXml2018);
			consoleHelper.AddCommand("217", "Импорт данных KO (БД) Модель 2018 (метки, модель)",
				MSExporter.DoLoadBd2018ModelAdd);
			consoleHelper.AddCommand("218", "Импорт данных KO (БД) Эталонные объекты",
				MSExporter.LoadGroupEtalonParcel_2018);
			consoleHelper.AddCommand("219", "Импорт данных KO (БД) ВУОН Земля",
				MSExporter.DoLoadBd2018Unit_Parcel_VUON);
			consoleHelper.AddCommand("220", "Импорт данных KO (БД) ВУОН Здания",
				MSExporter.DoLoadBd2018Unit_Build_VUON);
			consoleHelper.AddCommand("221", "Импорт данных KO (БД) ВУОН Сооружения",
				MSExporter.DoLoadBd2018Unit_Construction_VUON);
			consoleHelper.AddCommand("222", "Импорт данных KO (БД) ВУОН Помещения",
				MSExporter.DoLoadBd2018Unit_Flat_VUON);
			consoleHelper.AddCommand("223", "Импорт данных KO (БД) ВУОН ОНС",
				MSExporter.DoLoadBd2018Unit_Uncomplited_VUON);

			consoleHelper.AddCommand("250", "Формула 2016", MSExporter.GetFormulaText);
			consoleHelper.AddCommand("251", "Рассчет", MSExporter.GetCalcGroup);
			consoleHelper.AddCommand("252", "История", () =>
			{
				List<ObjectModel.KO.HistoryUnit> histories = ObjectModel.KO.HistoryUnit.GetHistory("77:17:0100302:62");
				foreach (ObjectModel.KO.HistoryUnit history in histories) Console.WriteLine(history.ToString());
			});

			consoleHelper.AddCommand("300", "Импорт данных судебной подсистемы (БД)", SudExporter.DoLoadBd);
			consoleHelper.AddCommand("301", "Импорт данных судебной подсистемы (Excel)", SudExporter.DoLoadExcel);
			consoleHelper.AddCommand("302", "Экспорт данных судебной подсистемы в Xml", SudExporter.ExportXml);
			consoleHelper.AddCommand("303", "Экспорт данных судебной подсистемы в Excel", SudExporter.ExportExcel);
			consoleHelper.AddCommand("304", "Статистика сводная в Excel", SudExporter.ExportStatExcel);
			consoleHelper.AddCommand("305", "Статискика по объектам недвидимости в Excel",
				SudExporter.ExportStatObjectExcel);
			consoleHelper.AddCommand("306", "Статистика по положительным судебным решениям в Excel",
				SudExporter.ExportStatCheckExcel);
			consoleHelper.AddCommand("350", "Импорт данных решений комиссий (БД)", CommissionExporter.DoLoadBd);
			consoleHelper.AddCommand("351", "Импорт данных решений комиссий (Excel)", CommissionExporter.DoLoadExcel);
			consoleHelper.AddCommand("390", "Тест API судебной подсистемы", SudTestApi.TestAll);

			consoleHelper.AddCommand("360", "Экспорт в Xml - результаты определения КС.", ExporterKO.ExportXml1);
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
			consoleHelper.AddCommand("501", "Импорт данных деклараций (Excel)",
				() => { new DataImporterDeclarationsTest().ImportData(); });
		}
	}
}