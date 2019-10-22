using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ObjectModel.Market;
using KadOzenka.BlFrontEnd.Logger;

namespace KadOzenka.BlFrontEnd.KadNumberChecker
{
    class ParseCadastral
    {
        readonly List<OMCoreObject> AllObjects =
            OMCoreObject.Where(x => x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr && x.ProcessType_Code == ObjectModel.Directory.ProcessStep.AddressStep)
                        .Select(x => new { x.Address, x.CadastralNumber, x.Lng, x.Lat, x.ExclusionStatus_Code, x.ProcessType_Code })
                        .Execute()
                        .ToList();
        readonly List<OMYandexAddress> YandexObjects =
            OMYandexAddress.Where(x => true)
                           .Select(x => new { x.FormalizedAddress, x.CadastralNumber, x.Lng, x.Lat, x.InitialId })
                           .Execute();

        public void Launch()
        {
            int KCtr = AllObjects.Count, KCur = 0, KKad = 0, KErr = 0;
            AllObjects.ForEach(x => 
            {
                OMYandexAddress address = YandexObjects.FirstOrDefault(y => y.FormalizedAddress.Equals(x.Address));
                if (address != null)
                {
                    x.CadastralNumber = address.CadastralNumber;
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.CadastralNumberStep;
                    address.InitialId = x.Id;
                    address.Lng = x.Lng;
                    address.Lat = x.Lat;
                    address.Save();
                    KKad++;
                }
                else
                {
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Dealed;
                    x.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.NoCadastralNumber;
                    KErr++;
                }
                KCur++;
                x.Save();
                LogData.WriteData("Получение кадастровых номеров", KCtr, KCur, KKad, KErr);
            });
            LogData.WriteFotter("Получение кадастровых номеров завершено");
        }
    }
}
