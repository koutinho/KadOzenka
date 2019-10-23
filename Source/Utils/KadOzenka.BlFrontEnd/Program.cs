using System;
using System.Collections.Generic;

using Platform.Shared;
using GemBox.Spreadsheet;
using KadOzenka.Dal.AddressChecker;
using KadOzenka.Dal.DuplicateCleaner;
using KadOzenka.Dal.KadNumberChecker;
using Core.Register.LongProcessManagment;
using KadOzenka.BlFrontEnd.GetSeleniumScreens;
using KadOzenka.BlFrontEnd.ObjectReplicationExcel;
using KadOzenka.Dal.RestAppParser;

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
        }

	}
}