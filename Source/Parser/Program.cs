using System;

using KadOzenka.Dal.RestAppParser;
using KadOzenka.Dal.AddressChecker;
using KadOzenka.Dal.KadNumberChecker;
using KadOzenka.Dal.Selenium.PriceChecker;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===Старт парсинга ЦИАНа===");
            Console.WriteLine("1. Получение данных с RestApp");
            new Data().Detect();
            //Console.WriteLine("2. Присвоение формализованных адресов");
            //new Addresses().Detect();
            //Console.WriteLine("3. Присвоение кадастровых номеров по формализованным адресам");
            //new KadNumbers().Detect();
            //Console.WriteLine("4. Парсинг объявлений со снятием скриншотов");
            //new Cian().RefreshAllData(15000, false);
        }
    }
}
