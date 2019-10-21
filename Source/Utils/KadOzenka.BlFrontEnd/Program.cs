﻿using System;
using System.Collections.Generic;

using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.BlFrontEnd.ObjectReplicationExcel;
using KadOzenka.BlFrontEnd.RosreestrParser;
using KadOzenka.BlFrontEnd.TestsAndExamples;
using KadOzenka.BlFrontEnd.YandexFiller;
using KadOzenka.BlFrontEnd.KadNumberChecker;
using KadOzenka.BlFrontEnd.GetSeleniumScreens;
using KadOzenka.BlFrontEnd.ClearDuplicates;
using GemBox.Spreadsheet;
using Platform.Shared;

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
			consoleHelper.AddCommand("1", "Tests.TestGetAllAttributes", Tests.TestGetAllAttributes);

			consoleHelper.AddCommand("2", "Запуск парсинга excele файла с объектами-аналогами из росреестра", () => { new RosreestrParser.ExcelParser().LoadRosreestrDeals(); });

			consoleHelper.AddCommand("3", "Запуск службы выполнения фоновых процессов", () => {
				LongProcessManagementService service = new LongProcessManagementService();
				service.Start();
			});

			consoleHelper.AddCommand("4", "Запуск выгрузки объявлений объектов-аналогов из сторонних источников", () => { new OuterMarketParser.Launcher.OuterMarketParser().StartProcess(); });

			consoleHelper.AddCommand("5", "Загрузка объектов ГБУ из Excel", ObjectReplicationExcelProcess.StartImport);

            consoleHelper.AddCommand("6", "Загрузка словаря с кадастровыми номерами из Excel", ObjectReplicationExcelProcess.StartImport);

            consoleHelper.AddCommand("7", "Присвоение адресов не обработанным объектам сторонних маркетов", () => { new YandexFiller.MarketCoreObject().Launch(); });

            consoleHelper.AddCommand("8", "Присвоение кадастровых номеров объектам сторонних маркетов", () => { new KadNumberChecker.ParseCadastral().Launch(); });

            consoleHelper.AddCommand("9", "Процедура проверки данных на дублирование", () => { new DuplicateCleaner().Launch(); });

            consoleHelper.AddCommand("10", "Процедура создания тестовых скриншотов", () => { new Selenium().MakeScreenshot(); });

        }
	}
}
