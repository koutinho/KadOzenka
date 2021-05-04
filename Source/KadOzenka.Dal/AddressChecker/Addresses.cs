using System;
using System.Linq;
using ObjectModel.Market;
using System.Configuration;
using System.Linq.Expressions;
using KadOzenka.Dal.Logger;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces;

namespace KadOzenka.Dal.AddressChecker
{
    /// <summary>
    /// Присвоение адресов необработанным объектам сторонних маркетов
    /// </summary>
    public class Addresses
    {
	    private IMarketObjectService MarketObjectService { get; }

	    public Addresses()
        {
	        MarketObjectService = new MarketObjectService();
        }

        public void Detect()
        {
	        Expression<Func<OMCoreObject, bool>> whereExpression = x => x.ProcessType_Code == ObjectModel.Directory.ProcessStep.DoNotProcessed;
	        Expression<Func<OMCoreObject, object>> selectExpression = x => new
	        {
		        x.Market_Code,
		        x.ProcessType_Code,
		        x.Address,
		        x.Lng,
		        x.Lat,
		        x.ExclusionStatus_Code
            };

	        var allObjects = MarketObjectService.GetObjectsByCondition(whereExpression, selectExpression)
		        .Take(Int32.Parse(ConfigurationManager.AppSettings["YandexLimit"]))
		        .ToList();

            int YCur = 0, YCor = 0, YErr = 0, ICtr = allObjects.Count;
            int PCur = 0, PCor = 0, PErr = 0;

            /*Процедура получения формализованного адреса из яндекса*/
            allObjects.ForEach(x =>
            {
                try
                {
                    OMYandexAddress address = new JSONParser.YandexGeocoder().ParseYandexAddress(new WebRequest.YandexGeocoder().GetDataByGeocode(x.Lng, x.Lat));
                    x.Address = address.FormalizedAddress;
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
            allObjects.ForEach(x =>
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