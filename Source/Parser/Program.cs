using System;
using System.Configuration;
using System.Linq;

using Microsoft.Extensions.Configuration;

using Serilog;
using System.Threading;
using System.Collections.Generic;

namespace Parser
{
    class Program
    {

        static readonly ILogger _log = Log.ForContext<Program>();

        public static readonly Hash _hash = new Hash();

        static void InitializeSeq()
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

            InitializeSeq();

            _log.Debug("Запуск парсера");

            Console.WriteLine("CIAN: " + _hash.CIANHashCombined.Count);
            Console.WriteLine("Avito: " + _hash.AvitoHashCombined.Count);
            Console.WriteLine("YandexProperty: " + _hash.YandexPropertyHashCombined.Count);

            Console.WriteLine();

            Console.WriteLine(_hash.CIANHashCombined.First().Url);
            Console.WriteLine(_hash.AvitoHashCombined.First().Url);
            Console.WriteLine(_hash.YandexPropertyHashCombined.First().Url);

            Console.WriteLine();

            //CIAN private
            HashCombined cianFlatsShapovskoye = _hash.CIANHashCombined.Where(x => x.Id == 1 && x.CustomRegions.Id == 151).First();
            HashCombined cianFlatPartsChertanovoYuzhnoe = _hash.CIANHashCombined.Where(x => x.Id == 2 && x.CustomRegions.Id == 87).First();
            HashCombined ciansSteadsSokol = _hash.CIANHashCombined.Where(x => x.Id == 6 && x.CustomRegions.Id == 23).First();
            HashCombined cianFlatsPresnenskiy = _hash.CIANHashCombined.Where(x => x.Id == 1 && x.CustomRegions.Id == 6).First();
            HashCombined cianHousePartsNoElements = _hash.CIANHashCombined.Where(x => x.Id == 4 && x.CustomRegions.Id == 13).First();
            HashCombined cianOrdinar = _hash.CIANHashCombined.First();
            //CIAN commercial
            HashCombined cianTradingYzhnoportoviy = _hash.CIANHashCombined.Where(x => x.Id == 8 && x.CustomRegions.Id == 71).First();
            HashCombined cianTradingSnyatVoronovskoye = _hash.CIANHashCombined.Where(x => x.Id == 8 && x.CustomRegions.Id == 142 && x.DealTypes.Id == 2).First();
            HashCombined cianFreeYasenevo = _hash.CIANHashCombined.Where(x => x.Id == 10 && x.CustomRegions.Id == 99).First();
            HashCombined cianGarageYasenevo = _hash.CIANHashCombined.Where(x => x.Id == 12 && x.CustomRegions.Id == 99).First();
            HashCombined cianTradingPresnenskiy = _hash.CIANHashCombined.Where(x => x.Id == 8 && x.CustomRegions.Id == 6).First();
            HashCombined cianCarServiceNoElements = _hash.CIANHashCombined.Where(x => x.Id == 14 && x.CustomRegions.Id == 151).First();

            //Avito commercial
            HashCombined avitoOrdinar = _hash.AvitoHashCombined.Where(x => x.Id == 7 && x.CustomRegions.Id == 6).First();
            HashCombined avitoNoElements = _hash.AvitoHashCombined.Where(x => x.Id == 18 && x.CustomRegions.Id == 123).First();
            HashCombined avitoObchepitYzhnoportoviy = _hash.AvitoHashCombined.Where(x => x.Id == 11 && x.CustomRegions.Id == 71).First();
            HashCombined avitoOfficePresnenskiy = _hash.AvitoHashCombined.Where(x => x.Id == 7 && x.CustomRegions.Id == 6).First();

            //YandexProperty commercial
            HashCombined yandexOrdinar = _hash.YandexPropertyHashCombined.First();
            HashCombined yandexNoElements = _hash.YandexPropertyHashCombined.Where(x => x.Id == 18 && x.CustomRegions.Id == 123).First();
            HashCombined yandexObchepitYzhnoportoviy = _hash.YandexPropertyHashCombined.Where(x => x.Id == 11 && x.CustomRegions.Id == 71).First();
            HashCombined yandexOfficePresnenskiy = _hash.YandexPropertyHashCombined.Where(x => x.Id == 7 && x.CustomRegions.Id == 6).First();

            new ParseData(yandexNoElements).Parse();
            new ParseData(yandexOrdinar).Parse();
            new ParseData(yandexObchepitYzhnoportoviy).Parse();
            new ParseData(yandexOfficePresnenskiy).Parse();

            new ParseData(avitoNoElements).Parse();
            new ParseData(avitoOrdinar).Parse();
            new ParseData(avitoObchepitYzhnoportoviy).Parse();
            new ParseData(avitoOfficePresnenskiy).Parse();

            new ParseData(cianCarServiceNoElements).Parse();
            new ParseData(cianOrdinar).Parse();
            new ParseData(cianTradingYzhnoportoviy).Parse();
            new ParseData(cianTradingPresnenskiy).Parse();

            new ParseData(cianHousePartsNoElements).Parse();
            new ParseData(cianFlatsPresnenskiy).Parse();
            new ParseData(cianFlatsShapovskoye).Parse();

            //new ParseData(cianOrdinar.AgregatorDataExtra, cianOrdinar.AgregatorData).Parse("https://www.cian.ru/kupit-chast-doma/", cianOrdinar.AgregatorDataExtra);
            //new ParseData(cianOrdinar.AgregatorDataExtra, cianOrdinar.AgregatorData).Parse("https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&object_type%5B0%5D=2&offer_type=suburban&p=2&region=1", cianOrdinar.AgregatorDataExtra);

            //Console.WriteLine($"{cianOrdinar.AgregatorData.Value} {cianOrdinar.Comment} {cianOrdinar.CustomRegions.Value}: ");
            //new ParseData(cianOrdinar.AgregatorDataExtra, cianOrdinar.AgregatorData).Parse(cianOrdinar.Url, _hash.AgregatorsDataExtra.Where(x => x.Agregator_id == 1).First());
            //Console.WriteLine($"{cianNoElements.AgregatorData.Value} {cianNoElements.Comment} {cianNoElements.CustomRegions.Value}: ");
            //new ParseData(cianOrdinar.AgregatorDataExtra, cianOrdinar.AgregatorData).Parse(cianNoElements.Url, _hash.AgregatorsDataExtra.Where(x => x.Agregator_id == 1).First());
            //Console.Write($"{cianOfficePresnenskiy.AgregatorData.Name} {cianOfficePresnenskiy.Comment} {cianOfficePresnenskiy.CustomRegions.Value}: ");
            //new ParseData(cianOfficePresnenskiy.AgregatorDataExtra, cianOfficePresnenskiy.AgregatorData).Parse(cianOfficePresnenskiy.Url, _hash.AgregatorsDataExtra.Where(x => x.Agregator_id == 1).First());
            //Console.Write("ЦИАН Капча: ");
            //new ParseData(cianOrdinar.AgregatorDataExtra, cianOrdinar.AgregatorData).Parse("https://www.cian.ru/captcha/?redirect_url=https://www.cian.ru/", _hash.AgregatorsDataExtra.Where(x => x.Agregator_id == 1).First());

            //Console.Write($"{avitoOrdinar.AgregatorData.Name} {avitoOrdinar.Comment} {avitoOrdinar.CustomRegions.Value}: ");
            //new ParseData(avitoOrdinar.AgregatorDataExtra, avitoOrdinar.AgregatorData).Parse(avitoOrdinar.Url, _hash.AgregatorsDataExtra.Where(x => x.Agregator_id == 2).First());
            //Console.Write($"{avitoNoElements.AgregatorData.Name} {avitoNoElements.Comment} {avitoNoElements.CustomRegions.Value}: ");
            //new ParseData(avitoOrdinar.AgregatorDataExtra, avitoOrdinar.AgregatorData).Parse(avitoNoElements.Url, _hash.AgregatorsDataExtra.Where(x => x.Agregator_id == 2).First());

            //Console.Write($"{yandexOrdinar.AgregatorData.Name} {yandexOrdinar.Comment} {yandexOrdinar.CustomRegions.Value}: ");
            //new ParseData(yandexOrdinar.AgregatorDataExtra, yandexOrdinar.AgregatorData).Parse(yandexOrdinar.Url, _hash.AgregatorsDataExtra.Where(x => x.Agregator_id == 3).First());
            //Console.Write($"{yandexNoElements.AgregatorData.Name} {yandexNoElements.Comment} {yandexNoElements.CustomRegions.Value}: ");
            //new ParseData(yandexNoElements.AgregatorDataExtra, yandexNoElements.AgregatorData).Parse(yandexNoElements.Url, _hash.AgregatorsDataExtra.Where(x => x.Agregator_id == 3).First());

            Console.ReadLine();

        }

    }

}
