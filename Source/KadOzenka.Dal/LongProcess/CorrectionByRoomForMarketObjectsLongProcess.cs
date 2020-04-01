using System;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.LongProcess.InputParameters;
using ObjectModel.Market;
using Core.Shared.Extensions;


namespace KadOzenka.Dal.LongProcess
{
    public class CorrectionByRoomForMarketObjectsLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(CorrectionByRoomForMarketObjectsLongProcess);

        public static void AddProcessToQueue(CorrectionByRoomRequest request)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, registerId: OMCoreObject.GetRegisterId(), parameters: request.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            DateTime date;
            var firstMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                var request = processQueue.Parameters.DeserializeFromXml<CorrectionByRoomRequest>();
                date = request?.Date ?? firstMonthDate;
            }
            else
            {
                date = firstMonthDate;
            }

            var correctionByRoomService = new CorrectionByRoomService();
            correctionByRoomService.UpdateMarketObjectsPrice(date);
        }
    }
}
