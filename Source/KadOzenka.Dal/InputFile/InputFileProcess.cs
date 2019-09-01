using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;

namespace CIPJS.DAL.InputFile
{
    public class InputFileProcess : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            if (processQueue.ObjectId.HasValue)
            {
                new InputFileService().Process(processQueue.ObjectId.Value, processQueue);
            }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
        }

        public bool Test()
        {
            return true;
        }
    }
}
