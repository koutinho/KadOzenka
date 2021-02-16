using System;
using System.Collections.Generic;

namespace ScreenshotParser
{

    class Program
    {

        static void Main(string[] args)
        {
            
            GetData getDataYandexProperty = new GetData(ObjectModel.Directory.MarketTypes.YandexProterty), getDataCian = new GetData(ObjectModel.Directory.MarketTypes.Cian);
            Parser parser = new Parser();

            int commonCount = getDataYandexProperty.AllObjects.Count;
            int currentCount = 0;
            getDataYandexProperty.AllObjects.ForEach(x => 
            {
                currentCount++;
                try { parser.ParseUrl(x, commonCount, currentCount, "YandexProperty"); }
                catch(Exception ex) 
                { 
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            });

            commonCount = getDataCian.AllObjects.Count;
            currentCount = 0;
            getDataCian.AllObjects.ForEach(x =>
            {
                currentCount++;
                try { parser.ParseUrl(x, commonCount, currentCount, "Cian"); }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            });

            parser.QuitDriver();

        }

    }

}
