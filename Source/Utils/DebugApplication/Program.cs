using System;
using System.Collections.Generic;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using DebugApplication.ObjectReplicationExcel;
using DebugApplication.RosreestrParser;
using DebugApplication.TestsAndExamples;
using DebugApplication.YandexFiller;
using GemBox.Spreadsheet;
using Platform.Shared;

namespace DebugApplication
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
			consoleHelper.AddCommand("1", "Tests.TestGetAllAttributes", Tests.TestGetAllAttributes);

			consoleHelper.AddCommand("2", "RosreestrParser.ExcelParser().LoadRosreestrDeals", () => { new RosreestrParser.ExcelParser().LoadRosreestrDeals(); });

			consoleHelper.AddCommand("3", "Запуск службы выполнения фоновых процессов", () => {
				LongProcessManagementService service = new LongProcessManagementService();
				service.Start();
			});

			consoleHelper.AddCommand("4", "Запуск выгрузки объявлений объектов-аналогов из сторонних источников", () => { new OuterMarketParser.Launcher.OuterMarketParser().StartProcess(); });

			consoleHelper.AddCommand("5", "Запуск выгрузки адресов из geocode-maps.yandex.ru", () => { new YandexFiller.AddressesFiller().GetAddresses(); });

			consoleHelper.AddCommand("6", "Запуск парсинга excele файла с объектами-аналогами из росреестра", () => {
				//new OuterMarketParser.Launcher.OuterMarketParser().ParseExcele();
			});

			consoleHelper.AddCommand("7", "Загрузка объектов ГБУ из Excel", ObjectReplicationExcelProcess.StartImport);
		}
	}
}
