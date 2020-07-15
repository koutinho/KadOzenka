using System;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

using ObjectModel.Market;
using KadOzenka.Dal.WebRequest;
using Core.Register.QuerySubsystem;

namespace KadOzenka.Dal.RestAppParser
{

    public class Data
    {

        private static string login { get; set; }
        private static string token { get; set; }
        private static List<OMCoreObject> AllObjects { get; set; }
        private static DateTime LastUpdateDate { get; set; }
        private static int restData { get; set; }

        public Data(string login, string token)
        {
            Console.WriteLine(login);
            Console.WriteLine(token);
            Data.login = login;
            Data.token = token;
            AllObjects = new List<OMCoreObject>();
            LastUpdateDate =
                   OMCoreObject.Where(x => x.Market_Code == ObjectModel.Directory.MarketTypes.Cian && x.ParserTime != null)
                               .Select(x => x.ParserTime)
                               .OrderByDescending(x => x.ParserTime)
                               .ExecuteFirstOrDefault()
                               .ParserTime
                               .GetValueOrDefault();
            restData = new JSONParser.RestApp().GetRestData(new RestApp().GetMetaInfoDataValues(login, token));
        }

        public void Detect()
        {
            Console.WriteLine($"Дата последнего обновления: {LastUpdateDate}");
            string[] regionIDs = ConfigurationManager.AppSettings["restAppRegionIDsCIAN"].Split(',');
            string[] dealTypes = ConfigurationManager.AppSettings["restAppDealTypeCIAN"].Split(',');
            int delta = int.Parse(ConfigurationManager.AppSettings["restAppMinuteLimits"]);
            List<string> links = new List<string>();
            int RACOR = 0, RAERR = 0, SCUR = 0, SCOR = 0, SERR = 0, SDUB = 0;
            bool EXCEPTION = false;
            while (LastUpdateDate < DateTime.Today)
            {
                DateTime currentTime = LastUpdateDate.AddSeconds(1);
                LastUpdateDate = LastUpdateDate.AddMinutes(delta);
                foreach (string region in regionIDs)
                {
                    foreach (string deal in dealTypes)
                    {
                        try
                        {
                            //links.Add(new RestApp().FormLink(region, deal, currentTime, LastUpdateDate));
                            List<OMCoreObject> coreObjs = 
                                new JSONParser.RestApp().ParseCoreObject(new RestApp().GetCIANDataByMultipleValues(region, deal, currentTime, LastUpdateDate, login, token), ref RACOR, ref RAERR);
                            AllObjects.AddRange(coreObjs);
                            Logger.ConsoleLog.WriteData("Получение данных из сторонних источников", restData, AllObjects.Count, RACOR, RAERR);
                        }
                        catch (Exception ex){ Console.WriteLine(ex); Console.WriteLine(ex.StackTrace); EXCEPTION = true; }
                    }
                    if (EXCEPTION) break;
                }
                if (EXCEPTION) break;
            }
            Logger.ConsoleLog.WriteFotter("Получение данных из сторонних источников завершено");
            AllObjects = AllObjects.GroupBy(x => x.Url).Select(x => x.First()).ToList();
            Console.WriteLine($"Полученные данные проверены на дублирование. Осталось записей: {AllObjects.Count}");
            AllObjects.ForEach(x =>
            {
                try
                {
                    if (OMCoreObject.Where(y => y.Url == x.Url).Execute().Count == 0)
                    {
                        x.Save();
                        SCOR++;
                    }
                    else SDUB++;
                    SCUR++;
                }
                catch (Exception) { SERR++; }
                Logger.ConsoleLog.WriteData("Запись данных в Postgres", AllObjects.Count, SCUR, SCOR, SERR, SDUB);
            });
            Logger.ConsoleLog.WriteFotter("Запись данных в Postgres завершена");
        }

    }

}