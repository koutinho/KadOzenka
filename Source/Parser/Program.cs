using System;
using System.Configuration;

using KadOzenka.Dal.RestAppParser;
using KadOzenka.Dal.AddressChecker;
using KadOzenka.Dal.KadNumberChecker;
using KadOzenka.Dal.Selenium.PriceChecker;
using Microsoft.Extensions.Configuration;

using Serilog;
using System.Threading;

namespace Parser
{
    class Program
    {

        static readonly ILogger _log = Log.ForContext<Program>();

        static void initializeSeq()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build())
                .Enrich
                .FromLogContext()
                .CreateLogger();
        }

        static void Main(string[] args)
        {
            initializeSeq();

            _log.Debug("Запуск парсера");

            _log.Verbose("Тестовое сообщение из парсера");

            Console.WriteLine(args[0]);
            Console.WriteLine("===Старт парсинга ЦИАНа===");
            if(args[0] == "1")
            {
                Console.WriteLine("1. Получение данных с RestApp");
                string[] logins = ConfigurationManager.AppSettings["restAppLogins"].Split(','),
                tokens = ConfigurationManager.AppSettings["restAppTokens"].Split(',');
                for (int i = 0; i < logins.Length; i++) new Data(logins[i], tokens[i]).Detect();
            }
            else if(args[0] == "2")
            {
                Console.WriteLine("2. Присвоение формализованных адресов");
                new Addresses().Detect();
                Console.WriteLine("3. Присвоение кадастровых номеров по формализованным адресам");
                new KadNumbers().Detect();
                Console.WriteLine("4. Парсинг объявлений со снятием скриншотов");
                new Cian().RefreshAllData(15000, false);
            }
        }
    }
}
