using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System;
using System.Linq;
using System.Threading;

namespace CIPJS.DAL.Bti.Import
{
    public class BtiImportProcess : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            if(processQueue != null)
            {
                processQueue.StartDate = DateTime.Now;
                processQueue.Status = (long)OMQueueStatus.Running;
                processQueue.Save();
            }
            try
            {
                Importer importer = new Importer(true, cancellationToken);
                importer.Import();
            }
            catch(Exception ex)
            {
                int errorId = ErrorManager.LogError(ex);
                if(processQueue != null)
                {
                    processQueue.ErrorId = errorId;
                    processQueue.Status = (long)OMQueueStatus.Faulted;
                    processQueue.EndDate = DateTime.Now;
                    processQueue.Save();
                }

                throw;
            }

            if (processQueue != null)
            {
                processQueue.Status = (long)OMQueueStatus.Completed;
                processQueue.EndDate = DateTime.Now;
                processQueue.Save();
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
