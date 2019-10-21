using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using ObjectModel.Market;
using KadOzenka.BlFrontEnd.Logger;

namespace KadOzenka.BlFrontEnd.YandexFiller
{

    class MarketCoreObject
    {

        List<OMCoreObject> AllObjects = 
            OMCoreObject.Where(x => x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr && 
                                    x.ProcessType_Code == ObjectModel.Directory.ProcessStep.DoNotProcessed)
                        .Select(x => new { x.Market_Code, x.ProcessType_Code, x.Address, x.Lng, x.Lat, x.ExclusionStatus_Code })
                        .Execute();

        public void Launch()
        {
            YandexAddressParser parser = new YandexAddressParser();
            int YCur = 0, YCor = 0, YErr = 0, ICtr = AllObjects.Count;
            int PCur = 0, PCor = 0, PErr = 0;
            LogData.StartLogger();
            /*Процедура получения формализованного адреса из яндекса*/
            AllObjects.ForEach(x =>
            {
                try
                {
                    OMYandexAddress address = parser.ParseYandexAddress(parser.GetDataByGeocode(x.Lng, x.Lat));
                    x.Address = address.FormalizedAddress;
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.AddressStep;
                    YCor++;
                }
                catch(Exception ex)
                {
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Dealed;
                    x.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.NoAddress;
                    LogData.LogError(string.Format(ConfigurationManager.AppSettings["geocodeLink"],
                                                   ConfigurationManager.AppSettings["GeocodeTest00"],
                                                   x.Lng.ToString().Replace(",", "."),
                                                   x.Lat.ToString().Replace(",", ".")), 
                                     ex.Message);
                    YErr++;
                }
                YCur++;
                LogData.WriteData("Получение адресов при помощи Yandex API", ICtr, YCur, YCor, YErr);
            });
            LogData.WriteFotter("Получение адресов при помощи Yandex API завершено");
            /*Процедура записи данных в Postgres*/
            AllObjects.ForEach(x =>
            {
                try
                {
                    x.Save();
                    PCor++;
                }
                catch (Exception ex)
                {
                    LogData.LogError(x.Id.ToString(), ex.Message);
                    PErr++;
                }
                PCur++;
                LogData.WriteData("Запись полученных адресов в Postgres", ICtr, PCur, PCor, PErr);
            });
            LogData.WriteFotter("Запись полученных адресов в Postgres завершена");
        }

    }
}
