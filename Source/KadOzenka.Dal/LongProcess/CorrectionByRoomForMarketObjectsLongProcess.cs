using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using KadOzenka.Dal.Correction;
using ObjectModel.Market;

namespace KadOzenka.Dal.LongProcess
{
    public class CorrectionByRoomForMarketObjectsLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(CorrectionByRoomForMarketObjectsLongProcess);

        public static void AddProcessToQueue()
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, registerId: OMCoreObject.GetRegisterId());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            var correctionByRoomService = new CorrectionByRoomService();
            correctionByRoomService.UpdateMarketObjectsPrice();
        }
    }
}
