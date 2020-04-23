using System;
using System.Configuration;

using KadOzenka.Dal.AddressChecker;
using KadOzenka.Dal.KadNumberChecker;
using KadOzenka.Dal.RestAppParser;

namespace StartProject
{
    class Program
    {
        static void Main(string[] args)
        {

            //string[] regionIDs = ConfigurationManager.AppSettings["restAppRegionIDsCIAN"].Split(',');
            //string[] dealTypes = ConfigurationManager.AppSettings["restAppDealTypeCIAN"].Split(',');
            //int delta = int.Parse(ConfigurationManager.AppSettings["restAppMinuteLimits"]);
            //string login = ConfigurationManager.AppSettings["restAppLogin001"];
            //string token = ConfigurationManager.AppSettings["restAppToken001"];
            //string metaInfoLink = ConfigurationManager.AppSettings["restAppMetaLink"];
            //string restAppCIANLink = ConfigurationManager.AppSettings["restAppCIANLink"];
            //string restAppAvitoLink = ConfigurationManager.AppSettings["restAppAvitoLink"];
            //string restAppTimeTemplate = ConfigurationManager.AppSettings["restAppTimeTmp"];


            Console.WriteLine("===Старт парсинга ЦИАНа===");
            Console.WriteLine("1. Получение данных с RestApp");
            new Data().Detect();
            Console.WriteLine("2. Присвоение формализованных адресов");
            new Addresses().Detect();
            Console.WriteLine("3. Присвоение кадастровых номеров по формализованным адресам");
            new KadNumbers().Detect();
            Console.WriteLine("4. Парсинг объявлений со снятием скриншотов");


        }
    }
}
