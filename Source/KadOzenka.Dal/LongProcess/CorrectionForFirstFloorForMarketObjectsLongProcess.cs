using System;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using KadOzenka.Dal.Correction;
using ObjectModel.Market;

namespace KadOzenka.Dal.LongProcess
{
    public class CorrectionForFirstFloorForMarketObjectsLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(CorrectionForFirstFloorForMarketObjectsLongProcess);

        public static void AddProcessToQueue()
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, registerId: OMCoreObject.GetRegisterId());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var correctionForFirstFloorService = new CorrectionForFirstFloorService();
            //correctionForFirstFloorService.UpdateMarketObjectsPrice(date);
        }
    }
}
