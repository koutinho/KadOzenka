using System;
using System.Threading;
using ObjectModel.Core.LongProcess;
using Core.Register.LongProcessManagment;

namespace OuterMarketParser.Launcher
{

    public class OuterMarketParser : ILongProcess
    {

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 100);
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            throw new NotImplementedException();
        }

        public bool Test() => true;

    }

}
