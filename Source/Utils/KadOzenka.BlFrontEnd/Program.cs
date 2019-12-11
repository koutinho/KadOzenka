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
using KadOzenka.BlFrontEnd.ExportMSSQL;
using KadOzenka.BlFrontEnd.ExportCommission;
using KadOzenka.Dal.DataExport;
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
            consoleHelper.AddCommand("2", "Запуск службы выполнения фоновых процессов", () =>
            {
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
            //consoleHelper.AddCommand("12", "Процедура обновления цен", () => { new Cian().TakePrice(); });
            consoleHelper.AddCommand("12", "Процедура обновления цен", () => { new Cian().RefreshAllData(); });
            consoleHelper.AddCommand("13", "Check Avito", () => { new AvitoChecker().Detect(); });
            consoleHelper.AddCommand("14", "Тест скриншот", () => { new Cian().Test(); });
            consoleHelper.AddCommand("15", "Тест автоматического формирования исключений", () => { new TestAutoExclusions().TryParse(); });
            consoleHelper.AddCommand("16", "Тест API судебной подсистемы", SudTestApi.TestAll);

            consoleHelper.AddCommand("200", "Импорт данных KO (БД) Модель 2016", MSExporter.DoLoadBd2016Model);
            consoleHelper.AddCommand("201", "Импорт данных KO (БД) Объекты и факторы 2016 ОНС", MSExporter.DoLoadBd2016Unit_Uncomplited);
            consoleHelper.AddCommand("202", "Импорт данных KO (БД) Объекты и факторы 2016 Сооружения", MSExporter.DoLoadBd2016Unit_Construction);
            consoleHelper.AddCommand("203", "Импорт данных KO (БД) Объекты и факторы 2016 Здания", MSExporter.DoLoadBd2016Unit_Building);
            consoleHelper.AddCommand("204", "Импорт данных KO (БД) Объекты и факторы 2016 Помещения", MSExporter.DoLoadBd2016Unit_Flat);
            consoleHelper.AddCommand("205", "Импорт данных KO (БД) Объекты и факторы 2016 Участки", MSExporter.DoLoadBd2016Unit_Parcel);
            consoleHelper.AddCommand("206", "Импорт данных KO (XML) 2016", MSExporter.DoLoadXml2016);

            consoleHelper.AddCommand("210", "Импорт данных KO (БД) Модель 2018", MSExporter.DoLoadBd2018Model);
            consoleHelper.AddCommand("211", "Импорт данных KO (БД) Объекты и факторы 2018 ОНС", MSExporter.DoLoadBd2018Unit_Uncomplited);
            consoleHelper.AddCommand("212", "Импорт данных KO (БД) Объекты и факторы 2018 Сооружения", MSExporter.DoLoadBd2018Unit_Construction);
            consoleHelper.AddCommand("213", "Импорт данных KO (БД) Объекты и факторы 2018 Здания", MSExporter.DoLoadBd2018Unit_Building);
            consoleHelper.AddCommand("214", "Импорт данных KO (БД) Объекты и факторы 2018 Помещения", MSExporter.DoLoadBd2018Unit_Flat);
            consoleHelper.AddCommand("215", "Импорт данных KO (БД) Объекты и факторы 2018 Участки", MSExporter.DoLoadBd2018Unit_Parcel);
            consoleHelper.AddCommand("216", "Импорт данных KO (XML) 2018", MSExporter.DoLoadXml2018);

            consoleHelper.AddCommand("220", "Формула 2016", MSExporter.GetFormulaText);
            consoleHelper.AddCommand("221", "Рассчет", MSExporter.GetCalcGroup);


            consoleHelper.AddCommand("300", "Импорт данных судебной подсистемы", SudExporter.DoLoadBd);
            consoleHelper.AddCommand("301", "Импорт данных судебной подсистемы", SudExporter.DoLoadExcel);
            consoleHelper.AddCommand("302", "Экспорт данных судебной подсистемы в Xml", SudExporter.ExportXml);
            consoleHelper.AddCommand("303", "Экспорт данных судебной подсистемы в Excel", SudExporter.ExportExcel);
            consoleHelper.AddCommand("304", "Статистика сводная в Excel", SudExporter.ExportStatExcel);
            consoleHelper.AddCommand("305", "Статискика по объектам недвидимости в Excel", SudExporter.ExportStatObjectExcel);
            consoleHelper.AddCommand("350", "Импорт данных решений комиссий (БД)", CommissionExporter.DoLoadBd);
            consoleHelper.AddCommand("351", "Импорт данных решений комиссий (Excel)", CommissionExporter.DoLoadExcel);

            consoleHelper.AddCommand("400", "Выгрузка кад. номеров в excel по первоначальным адресам", () => { ObjectReplicationExcelProcess.GAF(); });
            consoleHelper.AddCommand("100", "Контрольная проверка механизма отбора дублей", () => { new DetectDuplicatesTest.DetectDuplicatesTest().Test(); });
        }

    }
}