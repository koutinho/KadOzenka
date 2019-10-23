using System;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

using ObjectModel.Market;
using KadOzenka.Dal.WebRequest;

namespace KadOzenka.Dal.RestAppParser
{

    public class Data
    {

        List<OMCoreObject> AllObjects = new List<OMCoreObject>();
        DateTime LastUpdateDate = OMCoreObject
            .Where(x => true)
            .Select(x => x.ParserTime)
            .OrderByDescending(x => x.ParserTime)
            .ExecuteFirstOrDefault()
            .ParserTime
            .GetValueOrDefault();
        int restData = new JSONParser.RestApp().GetRestData(new RestApp().GetMetaInfoDataValues());

        public void Detect()
        {
            string[] regionIDs = ConfigurationManager.AppSettings["restAppRegionIDs"].Split(',');
            string[] dealTypes = ConfigurationManager.AppSettings["restAppDealType"].Split(',');
            int delta = int.Parse(ConfigurationManager.AppSettings["restAppMinuteLimits"]);
            List<string> links = new List<string>();
            int RACOR = 0, RAERR = 0;
            bool EXCEPTION = false;
            while (LastUpdateDate < DateTime.Today)
            {
                DateTime currentTime = LastUpdateDate.AddSeconds(1);
                LastUpdateDate = LastUpdateDate.AddMinutes(delta);
                foreach (string region in regionIDs)
                {
                    foreach (string deal in dealTypes)
                    {
                        //links.Add(new RestApp().FormLink(region, deal, currentTime, LastUpdateDate));
                        List<OMCoreObject> coreObjs = 
                            new JSONParser.RestApp().ParseCoreObject(
                                new RestApp().GetDataByMultipleValues(region, deal, currentTime, LastUpdateDate), 
                                ref RACOR, 
                                ref RAERR, 
                                ref EXCEPTION
                            );
                        if (!EXCEPTION) AllObjects.AddRange(coreObjs);
                        else break;
                        Logger.ConsoleLog.WriteData("Получение данных из сторонних источников", restData, AllObjects.Count, RACOR, RAERR);
                    }
                    if (EXCEPTION) break;
                }
                if (EXCEPTION) break;
            }
            Logger.ConsoleLog.WriteFotter("Получение данных из сторонних завершено");
            Console.WriteLine(AllObjects.Count);
        }

    }

}
