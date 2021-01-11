using System;
using System.Threading;
using ObjectModel.Core.LongProcess;
using Core.Register.LongProcessManagment;
using Serilog;

namespace OuterMarketParser.Launcher
{

    public class OuterMarketParser : ILongProcess
    {
	    private readonly ILogger _log = Log.ForContext<OuterMarketParser>();

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 100);
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
	        _log.ForContext("ErrorId", errorId).Error(ex, "Ошибка фонового процесса. ID объекта {objectId}", objectId);
        }

        public bool Test() => true;

    }

}
