using System.Linq;
using System.Collections.Generic;
using ObjectModel.Market;
using KadOzenka.Dal.Logger;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces;
using MarketPlaceBusiness.Interfaces.ForBlFrontendApp;

namespace KadOzenka.Dal.KadNumberChecker
{
    /// <summary>
    /// Присвоение кадастровых номеров объектам сторонних маркетов
    /// </summary>
    public class KadNumbers
    {
        private IMarketObjectsServiceForBlFrontendApp MarketObjectService { get; set; }

        readonly List<OMYandexAddress> YandexObjects =
            OMYandexAddress.Where(x => true)
                           .Select(x => new { x.FormalizedAddress, x.CadastralNumber, x.Lng, x.Lat, x.InitialId })
                           .Execute();


        public KadNumbers()
        {
	        MarketObjectService = new MarketObjectService();
        }


        public void Detect()
        {
	        var allObjects = MarketObjectService.GetNotRosreestrObjectsWithAddressProceed();
	        int KCtr = allObjects.Count, KCur = 0, KKad = 0, KErr = 0;
            allObjects.ForEach(x =>
            {
                OMYandexAddress address = YandexObjects.FirstOrDefault(y => y.FormalizedAddress.Equals(x.Address));
                if (address != null)
                {
                    x.CadastralNumber = address.CadastralNumber;
                    x.CadastralQuartal = address.CadastralNumber.Substring(0, address.CadastralNumber.LastIndexOf(":"));
                    x.BuildingCadastralNumber = address.CadastralNumber;
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.CadastralNumberStep;
                    address.InitialId = x.Id;
                    address.Lng = x.Lng;
                    address.Lat = x.Lat;
                    address.Save();
                    KKad++;
                }
                else
                {
                    x.CadastralNumber = null;
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Excluded;
                    x.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.NoCadastralNumber;
                    KErr++;
                }
                KCur++;
                x.Save();
                ConsoleLog.WriteData("Получение кадастровых номеров", KCtr, KCur, KKad, KErr);
            });
            ConsoleLog.WriteFotter("Получение кадастровых номеров завершено");
        }

    }
}
