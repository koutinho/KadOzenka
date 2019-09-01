using CIPJS.DAL.StrahNach;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Misc;
using ObjectModel.Core.LongProcess;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CIPJS.DAL.Fsp
{
    /// <summary>
    /// Процесс перерасчета балансов в фсп, у которых была изменена площадь.
    /// </summary>
    public class FspOplDifferentKodoplUpdateProcess : ILongProcess
    {
        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {

        }

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            FspService fspService = new FspService();
            StrahNachService strahNachService = new StrahNachService();

            // Получение ФСП у которых была изменена площадь.
            var fspIds = fspService.SetGbuInputNachWithFspByPeriod();
            // Выгружаем занчения.
            List<OMFsp> fspList = OMFsp.Where(x => fspIds.Contains(x.EmpId) && x.SpecialActual == 1)
                .Select(x => x.OplKodpl)
                .Select(x => x.Kodpl)
                .Select(x => x.DateOpen)
                .Execute();

            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 10
            };
            bool error = false;
            // Параллельная обработка фсп. 
            Parallel.ForEach(fspList, options, fsp =>
            {
                try
                {
                    // Если нет даты открытия фсп, берем стандартное значение.
                    DateTime periodRegDate = fsp.DateOpen.HasValue ? fsp.DateOpen.Value : new DateTime(2018, 1, 1);
                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {
                        fspService.AccountFsp(fsp.EmpId, periodRegDate);
                        fspService.CalcBalanceSumFromPeriod(fsp.EmpId, periodRegDate);

                        ts.Complete();
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    error = true;
                }
            });

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
