using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using KadOzenka.Dal.Correction;
using ObjectModel.Market;

namespace KadOzenka.Dal.LongProcess
{
    public class CorrectionByFloorForMarketObjectsLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(CorrectionByFloorForMarketObjectsLongProcess);

        public static void AddProcessToQueue()
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, registerId: OMCoreObject.GetRegisterId());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            var correctionByFloorService = new CorrectionForFirstFloorService();
            //Todo
            //correctionByFloorService.UpdateMarketObjectsPrice(DateTime date);
        }
    }
}
