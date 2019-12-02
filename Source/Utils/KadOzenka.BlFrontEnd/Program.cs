﻿using System;
using System.Collections.Generic;

using Platform.Shared;
using GemBox.Spreadsheet;
using KadOzenka.Dal.AddressChecker;
using KadOzenka.Dal.DuplicateCleaner;
using KadOzenka.Dal.KadNumberChecker;
using Core.Register.LongProcessManagment;
using KadOzenka.BlFrontEnd.DataExport;
using KadOzenka.BlFrontEnd.GetSeleniumScreens;
using KadOzenka.BlFrontEnd.ObjectReplicationExcel;
using KadOzenka.Dal.RestAppParser;
using KadOzenka.Dal.Selenium.PriceChecker;
using KadOzenka.Dal.Test;
using KadOzenka.BlFrontEnd.ExportSud;
using KadOzenka.BlFrontEnd.SudTests;

namespace KadOzenka.BlFrontEnd
{
    class Program
    {

		static void Main(string[] args)
        {
			SpreadsheetInfo.SetLicense("ERDD-TNCL-YKZ5-3ZTU");
			var consoleHelper = new BlFrontEndConsoleHelper();
			InitCommands(consoleHelper);
			consoleHelper.Run();
		}
		
		private static void InitCommands(BlFrontEndConsoleHelper consoleHelper)
		{
			consoleHelper.AddCommand("1", "Запуск парсинга excele файла с объектами-аналогами из росреестра", () => { new RosreestrParser.ExcelParser().LoadRosreestrDeals(); });
			consoleHelper.AddCommand("2", "Запуск службы выполнения фоновых процессов", () => {
				LongProcessManagementService service = new LongProcessManagementService();
				service.Start();
			});
            //consoleHelper.AddCommand("3", "Запуск выгрузки объявлений объектов-аналогов из сторонних источников", () => { new OuterMarketParser.Launcher.OuterMarketParser().StartProcess(); });
            consoleHelper.AddCommand("3", "Запуск выгрузки объявлений объектов-аналогов из сторонних источников", () => { new Data().Detect(); });
            consoleHelper.AddCommand("4", "Загрузка объектов ГБУ из Excel", ObjectReplicationExcelProcess.StartImport);
            consoleHelper.AddCommand("5", "Загрузка словаря с кадастровыми номерами из Excel", ObjectReplicationExcelProcess.StartImport);
            consoleHelper.AddCommand("6", "Присвоение адресов не обработанным объектам сторонних маркетов", () => { new Addresses().Detect(); });
            consoleHelper.AddCommand("7", "Присвоение кадастровых номеров объектам сторонних маркетов", () => { new KadNumbers().Detect(); });
            consoleHelper.AddCommand("8", "Процедура проверки данных на дублирование", () => { new Duplicates().Detect(); });
            consoleHelper.AddCommand("9", "Процедура создания тестовых скриншотов", () => { new Selenium().MakeScreenshot(); });
            consoleHelper.AddCommand("10", "Экспорт данных в Excel на основе шаблона", DataExportConsole.ExportData);
            consoleHelper.AddCommand("11", "Импорт данных в Excel из шаблона", DataImportConsole.ImportData);
            consoleHelper.AddCommand("12", "Процедура обновления цен", () => { new Cian().TakePrice(); });
            consoleHelper.AddCommand("13", "Check Avito", () => { new AvitoChecker().Detect(); });
            consoleHelper.AddCommand("14", "Тест скриншот", () => { new Cian().Test(); });
            consoleHelper.AddCommand("15", "Тест автоматического формирования исключений", () => { new TestAutoExclusions().TryParse(); });
			consoleHelper.AddCommand("16", "Тест API судебной подсистемы", SudTestApi.TestAll);

			consoleHelper.AddCommand("300", "Импорт данных судебной подсистемы", SudExporter.DoLoad);
        }
	}
}