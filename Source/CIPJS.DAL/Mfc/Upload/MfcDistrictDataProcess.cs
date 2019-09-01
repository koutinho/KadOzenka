using CIPJS.DAL.Fsp;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using ObjectModel.Core.LongProcess;
using Platform.Main.ConfigParam.Mfc;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CIPJS.DAL.Mfc.Upload
{
    public class MfcDistrictDataProcess : ILongProcess
    {
        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {

        }

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            if (processQueue != null)
            {
                processQueue.Status = (long)OMQueueStatus.Running;
                processQueue.Save();
            }

            FspService fspService = new FspService();
            bool error = false;
            try
            {
                var parameters = processType.Parameters.DeserializeFromXml<MfcDistrictDataProcessParam>();
                if (parameters == null)
                {
                    return;
                }
                // Устанавливаем дату начала обработки.
                DateTime periodRegDate = parameters.PeriodRegDate.HasValue ? parameters.PeriodRegDate.Value : new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var okrugIds = parameters.OkrugId;
                // Получаем ФСП округов.
                var fspIds = fspService.GetFspByOkrug(okrugIds);

                ParallelOptions options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 10
                };

                // Проверяем на полученные значения 
                if (fspIds.Count > 0)
                {
                    // Параллельная обработка фсп. 
                    Parallel.ForEach(fspIds, options, fsp =>
                    {
                        using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                        {
                            fspService.AccountFsp(fsp, periodRegDate);
                            fspService.CalcBalanceSumFromPeriod(fsp, periodRegDate);

                            ts.Complete();
                        }
                    });
                }
                else
                {
                    throw new Exception("ФСП округов не найдены!");
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                error = true;
            }


            if (processQueue != null)
            {
                processQueue.Status = error ? (long)OMQueueStatus.Faulted : (long)OMQueueStatus.Completed;
                processQueue.Save();
            }


        }

        public bool Test()
        {
            return true;
        }
    }
}
