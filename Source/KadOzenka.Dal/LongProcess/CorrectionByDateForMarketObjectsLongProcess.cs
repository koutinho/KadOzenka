using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using KadOzenka.Dal.Correction;
using ObjectModel.Market;

namespace KadOzenka.Dal.LongProcess
{
    public class CorrectionByDateForMarketObjectsLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(CorrectionByDateForMarketObjectsLongProcess);

        public static void AddProcessToQueue()
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, registerId: OMCoreObject.GetRegisterId());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);

            var correctionByDateService = new CorrectionByDateService();

            var marketObjects = correctionByDateService.GetMarketObjectsForUpdate();

            var consumerIndexes = OMIndexesForDateCorrection.Where(x => true).SelectAll().Execute();

            var nextPercent = PercentageInterval;
            for (var i = 0; i < marketObjects.Count; i++)
            {
                LogProgress(processQueue, i, marketObjects.Count, ref nextPercent);

                var obj = marketObjects[i];
                var priceWithCorrection = correctionByDateService.CalculatePriceAfterCorrectionByDate(obj, consumerIndexes);
                obj.PriceAfterCorrectionByDate = priceWithCorrection;
                obj.Save();
            }

            WorkerCommon.SetProgress(processQueue, 100);
        }
    }
}
