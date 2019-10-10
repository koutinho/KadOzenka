using System;

using DebugApplication.RosreestrParser;
using DebugApplication.YandexFiller;

namespace DebugApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Запуск службы выполнения фоновых процессов
             */
            //LongProcessManagementService service = new LongProcessManagementService();
            //service.Start();

            /*
             * Запуск выгрузки объявлений объектов-аналогов из сторонних источников
             */
            //new OuterMarketParser.Launcher.OuterMarketParser().StartProcess();

            /*
             * Запуск выгрузки адресов из geocode-maps.yandex.ru
             */
            //new YandexFiller.AddressesFiller().GetAddresses();

            /*
             * Запуск парсинга excele файла с объектами-аналогами из росреестра
             */
            //new OuterMarketParser.Launcher.OuterMarketParser().ParseExcele();
            new RosreestrParser.ExcelParser().LoadRosreestrDeals();

            return;
        }

    }
}
