﻿using System;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Core.Shared.Extensions;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.LongProcess.InputParameters;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.LongProcess
{
    public class CorrectionForFirstFloorForMarketObjectsLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(CorrectionForFirstFloorForMarketObjectsLongProcess);

        public static void AddProcessToQueue(CorrectionForFirstFloorRequest request)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, registerId: OMCoreObject.GetRegisterId(), parameters: request.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            DateTime date;
            MarketSegment? segment = null;
            var firstMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                var request = processQueue.Parameters.DeserializeFromXml<CorrectionForFirstFloorRequest>();
                date = request?.Date ?? firstMonthDate;
                segment = request?.Segment;
            }
            else
            {
                date = firstMonthDate;
            }

            var correctionForFirstFloorService = new CorrectionForFirstFloorService();
            
            correctionForFirstFloorService.MakeCorrections(date, segment);
        }
    }
}
