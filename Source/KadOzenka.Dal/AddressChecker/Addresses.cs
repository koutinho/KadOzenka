using System;
using System.Text;
using System.Linq;
using ObjectModel.Market;
using System.Configuration;
using System.Collections.Generic;

using KadOzenka.Dal.Logger;
using KadOzenka.Dal.WebRequest;
using KadOzenka.Dal.JSONParser;

namespace KadOzenka.Dal.AddressChecker
{

    public class Addresses
    {

        List<OMCoreObject> AllObjects =
            OMCoreObject.Where(x => x.ProcessType_Code == ObjectModel.Directory.ProcessStep.DoNotProcessed)
                        .Select(x => new { x.Market_Code, x.ProcessType_Code, x.Address, x.Lng, x.Lat, x.ExclusionStatus_Code })
                        .Execute()
                        .Take(Int32.Parse(ConfigurationManager.AppSettings["YandexLimit"]))
                        .ToList();

        public void Detect()
        {
            int YCur = 0, YCor = 0, YErr = 0, ICtr = AllObjects.Count;
            int PCur = 0, PCor = 0, PErr = 0;
            /*Процедура получения формализованного адреса из яндекса*/
            AllObjects.ForEach(x =>
            {
                try
                {
                    OMYandexAddress address = new JSONParser.YandexGeocoder().ParseYandexAddress(new WebRequest.YandexGeocoder().GetDataByGeocode(x.Lng, x.Lat));
                    x.Address = address.FormalizedAddress;
                    x.District = address.District;
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.AddressStep;
                    YCor++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Excluded;
                    x.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.NoAddress;
                    YErr++;
                }
                YCur++;
                ConsoleLog.WriteData("Получение адресов при помощи Yandex API", ICtr, YCur, YCor, YErr);
            });
            ConsoleLog.WriteFotter("Получение адресов при помощи Yandex API завершено");
            /*Процедура записи данных в Postgres*/
            AllObjects.ForEach(x =>
            {
                try
                {
                    x.Save();
                    PCor++;
                }
                catch (Exception){ PErr++; }
                PCur++;
                ConsoleLog.WriteData("Запись полученных адресов в Postgres", ICtr, PCur, PCor, PErr);
            });
            ConsoleLog.WriteFotter("Запись полученных адресов в Postgres завершена");
        }

    }

}