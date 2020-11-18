using System;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

using ObjectModel.Market;
using KadOzenka.Dal.WebRequest;
using Core.Register.QuerySubsystem;
using Serilog;

namespace KadOzenka.Dal.RestAppParser
{

    public class Data
    {

        private static string login { get; set; }
        private static string token { get; set; }
        private static List<OMCoreObject> AllObjects { get; set; }
        private static DateTime LastUpdateDate { get; set; }
        private static int restData { get; set; }
        static readonly ILogger _log = Log.ForContext<Data>();

        public Data(string login, string token)
        {
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
            _log
                .ForContext("Логин", login)
                .ForContext("Токен", token)
                .ForContext("Дата последнего обновления", LastUpdateDate.ToString())
                .Debug("Обращение к RestApp");
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
                            _log
                                .ForContext("RestObjects", restData)
                                .ForContext("RegionId", region)
                                .ForContext("DealId", deal)
                                .ForContext("StartTime", currentTime)
                                .ForContext("EndTime", LastUpdateDate)
                                .ForContext("Login", login)
                                .ForContext("Token", token)
                                .ForContext("Link", link)
                                .Debug("Получение данных с RestApp. Корректно {CorrenctObjectsCount}. С ошибкой {ErrorObjectsCount}.", RACOR, RAERR);
                        }
                        catch (Exception ex)
                        {
                            _log
                                .ForContext("RestObjects", restData)
                                .ForContext("RegionId", region)
                                .ForContext("DealId", deal)
                                .ForContext("StartTime", currentTime)
                                .ForContext("EndTime", LastUpdateDate)
                                .ForContext("Login", login)
                                .ForContext("Token", token)
                                .ForContext("Link", link)
                                .Error(ex, "Ошибка при получении данных с RestApp");
                            EXCEPTION = true; 
                        }
                    }
                    if (EXCEPTION) break;
                }
                if (EXCEPTION) break;
            }
            _log
                .ForContext("Login", login)
                .ForContext("Token", token)
                .Debug("Получение данных из сторонних источников завершено");
            AllObjects = AllObjects.GroupBy(x => x.Url).Select(x => x.First()).ToList();
            _log
                .ForContext("Login", login)
                .ForContext("Token", token)
                .ForContext("UniqueCount", AllObjects.Count)
                .Debug("Проверка полученных данных на дублирование");
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
                catch (Exception ex) 
                {
                    SERR++;
                    _log
                        .ForContext("Id", x.Id)
                        .Error(ex, "Ошибка при сохранении объекта в БД");
                }
            });
            _log
                .ForContext("AllObjectsCount", AllObjects.Count)
                .ForContext("WrittenObjectsCount", SCUR)
                .ForContext("CorrectObjectsCount", SCOR)
                .ForContext("ErrorObjectsCount", SERR)
                .ForContext("DuplicateObjectsCount", SDUB)
                .Debug("Запись данных в БД завершена");
        }

    }

}