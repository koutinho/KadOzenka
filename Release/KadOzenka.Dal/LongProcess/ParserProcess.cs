﻿using System;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Collections.Generic;

using KadOzenka.Dal.RestAppParser;
using ObjectModel.Core.LongProcess;
using KadOzenka.Dal.AddressChecker;
using KadOzenka.Dal.KadNumberChecker;
using KadOzenka.Dal.Selenium.PriceChecker;

namespace KadOzenka.Dal.LongProcess
{
    public class ParserProcess : LongProcess
    {
        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            Console.WriteLine("===Старт парсинга ЦИАНа===");
            Console.WriteLine("1. Получение данных с RestApp");
            string[] logins = ConfigurationManager.AppSettings["restAppLogin001"].Split(','),
                     tokens = ConfigurationManager.AppSettings["restAppToken001"].Split(',');
            for (int i = 0; i < logins.Length; i++) new Data(logins[i], tokens[i]).Detect();
            //Console.WriteLine("2. Присвоение формализованных адресов");
            //new Addresses().Detect();
            //Console.WriteLine("3. Присвоение кадастровых номеров по формализованным адресам");
            //new KadNumbers().Detect();
            //Console.WriteLine("4. Парсинг объявлений со снятием скриншотов");
            //new Cian().RefreshAllData(15000, false);
        }
    }
}
