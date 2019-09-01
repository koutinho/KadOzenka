using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CIPJS.DAL.Mfc.Upload
{
    public class MfcUploadLoadProcess : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            try
            {
                if (processQueue.ObjectId.HasValue)
                {
                    MfcUploadService mfcUploadService = new MfcUploadService();
                    List<long?> filePackageIds = mfcUploadService.Load(processQueue.ObjectId.Value);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                throw;
            }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            if (!objectId.HasValue)
            {
                return;
            }

            OMLogFile logFile = OMLogFile.Where(x => x.EmpId == objectId.Value)
                .SelectAll()
                .ExecuteFirstOrDefault();

            if (logFile == null)
            {
                return;
            }

            logFile.GeneralStatus_Code = MfcGeneralUploadStatus.Loaded;
            logFile.Status_Code = MfcUploadFileStatus.Error;
            logFile.Save();

            List<OMInputFile> inputFiles = OMInputFile.Where(x => x.LogFileId == logFile.EmpId 
                && (x.Status_Code == null || x.Status_Code == UFKFileProcessingStatus.None))
                .Execute();
            foreach(OMInputFile inputFile in inputFiles)
            {
                inputFile.Destroy();
            }
        }

        public bool Test()
        {
            return true;
        }
    }
}
