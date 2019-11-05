using System;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

using ObjectModel.Market;
using KadOzenka.Dal.WebRequest;

namespace KadOzenka.Dal.RestAppParser
{
    public class AvitoChecker
    {
        DateTime LastUpdateDate =
            OMCoreObject.Where(x => true).Select(x => x.ParserTime).OrderByDescending(x => x.ParserTime).ExecuteFirstOrDefault().ParserTime.GetValueOrDefault();
        int restData = new JSONParser.RestApp().GetRestData(new RestApp().GetMetaInfoDataValues());

        public void Detect()
        {
            string[] regionIDs = ConfigurationManager.AppSettings["restAppRegionIDsAvito"].Split(',');
            string[] categories = ConfigurationManager.AppSettings["restAppCategoriesAvito"].Split(',');
            int delta = int.Parse(ConfigurationManager.AppSettings["restAppMinuteLimits"]);
            while (LastUpdateDate < DateTime.Today)
            {
                DateTime currentTime = LastUpdateDate.AddSeconds(1);
                LastUpdateDate = LastUpdateDate.AddMinutes(delta);
                foreach (string region in regionIDs)
                {
                    foreach (string category in categories)
                    {
                        Console.WriteLine($"{new RestApp().GetAvitoDataByMultipleValues(region, category, currentTime, LastUpdateDate)}");
                    }
                }
            }
            Console.WriteLine(string.Join(";", regionIDs));
            Console.WriteLine(string.Join(";", categories));
        }
    }
}
