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
            //ManageData(processQueue, "364a064a-ad89-403c-9e2b-abe8c3b8715b");
        }

        private static void ManageData(OMQueue processQueue, string key)
        {
            List<OMCoreObject> AllObjects = OMCoreObject
                .Where(x => x.ProcessType_Code == ObjectModel.Directory.ProcessStep.Dealed && x.Market_Code == ObjectModel.Directory.MarketTypes.Rosreestr && x.Lng == null && x.Lat == null)
                .Select(x => new { x.ProcessType_Code, x.Address, x.Lng, x.Lat, x.ExclusionStatus_Code }).Execute().ToList().Take(20000).ToList();
            int goods = 0, errors = 0, current = 0;
            AllObjects.ForEach(x =>
            {
                try
                {
                    OMYandexAddress address = new YandexGeocoder().ParseYandexAddress(new WebRequest.YandexGeocoder().GetDataByAddress(YandexGeocoder.getFormalizedAddress(x.Address), key));
                    x.Lng = address.Lng;
                    x.Lat = address.Lat;
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Dealed;
                    goods++;
                }
                catch (Exception)
                {
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Excluded;
                    x.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.NoAddress;
                    errors++;
                }
                x.Save();
                int currentPercent = (int)((double)++current / allCount * 100);
                Console.WriteLine($"С координатами: {goods}; С ошибкой: {errors}");
                WorkerCommon.SetProgress(processQueue, currentPercent);
            });
        }

    }

}
