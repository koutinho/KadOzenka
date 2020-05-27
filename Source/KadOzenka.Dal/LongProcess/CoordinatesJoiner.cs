using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

using Core.Register.LongProcessManagment;
using KadOzenka.Dal.JSONParser;
using ObjectModel.Core.LongProcess;
using ObjectModel.Market;

namespace KadOzenka.Dal.LongProcess
{

    public class CoordinatesJoiner : LongProcess
    {

        private static int allCount = 0;
        private static int current = 0;

        public const string LongProcessName = "CoordinatesJoiner";

        public static void AddProcessToQueue() => LongProcessManager.AddTaskToQueue(LongProcessName, null, null);

        public static bool IsProcessAdditable() => OMQueue.Where(x => x.ProcessTypeId == 39 && x.EndDate == null).ExecuteCount() == 0;

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            int reqCount = OMCoreObject
                .Where(x => x.ProcessType_Code == ObjectModel.Directory.ProcessStep.Dealed && x.Market_Code == ObjectModel.Directory.MarketTypes.Rosreestr && x.Lng == null && x.Lat == null)
                .Select(x => new { x.ProcessType_Code, x.Address, x.Lng, x.Lat, x.ExclusionStatus_Code }).ExecuteCount();
            allCount = reqCount < 250000 ? reqCount : 250000;
            WorkerCommon.SetProgress(processQueue, 0);
            //ManageData(processQueue, "aae90424-d9b9-4fbb-b505-2d512fbc9495");
            ManageData(processQueue, "8ee708c5-f0dd-4b49-8077-82c143f3a03d");
            ManageData(processQueue, "2956b9ed-b374-4efc-a0ee-26598db7a569");
            ManageData(processQueue, "b6904267-d27b-4235-8c5e-a797c251ca82");
            ManageData(processQueue, "25934766-8ae1-4845-b856-3650ceb36995");
            ManageData(processQueue, "31656d59-9d52-4639-b329-98557c812d2a");
            ManageData(processQueue, "f3ec39cb-1e54-4eb8-ad2a-a37ff8e188d3");
            ManageData(processQueue, "45aeed3f-13b0-481f-8a89-ccee9e212958");
            ManageData(processQueue, "664bab45-742f-4754-8c68-f480d00d387e");
            ManageData(processQueue, "5f702dd4-999a-45eb-8200-505af36a7a05");
            ManageData(processQueue, "bc813f20-7caa-4990-a2c9-5a5460283880");
        }

        private static void ManageData(OMQueue processQueue, string key)
        {
            List<OMCoreObject> AllObjects = OMCoreObject
                .Where(x => x.ProcessType_Code == ObjectModel.Directory.ProcessStep.Dealed && x.Market_Code == ObjectModel.Directory.MarketTypes.Rosreestr && x.Lng == null && x.Lat == null)
                .Select(x => new { x.ProcessType_Code, x.Address, x.Lng, x.Lat, x.ExclusionStatus_Code }).Execute().ToList().Take(25000).ToList();
            AllObjects.ForEach(x =>
            {
                try
                {
                    OMYandexAddress address = new YandexGeocoder().ParseYandexAddress(new WebRequest.YandexGeocoder().GetDataByAddress(YandexGeocoder.getFormalizedAddress(x.Address), key));
                    x.Lng = address.Lng;
                    x.Lat = address.Lat;
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Dealed;
                }
                catch (Exception)
                {
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Excluded;
                    x.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.NoAddress;
                }
                x.Save();
                int currentPercent = (int)((double)++current / allCount * 100);
                WorkerCommon.SetProgress(processQueue, currentPercent);
            });
        }

    }

}
