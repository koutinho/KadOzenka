using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Misc;
using ObjectModel.Core.LongProcess;
using ObjectModel.MV;

namespace CIPJS.DAL.Mv
{
    /// <summary>
    /// Процесс обновления MATERIALIZED VIEW.
    /// CIPJS-465: Настроить обновление аналитических показателей на основной панели ОПС
    /// </summary>
    public class MvUpdateProcess : ILongProcess
    {
        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
           
        }

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            MvService mvService = new MvService();
            
            var mViews = OMRefreshList.Where(x => x).SelectAll().OrderBy(x => x.Id).Execute();

            // Проверка наличия записей. 
            if(mViews == null || mViews.Count == 0)
            {
                if (processQueue != null)
                {
                    processQueue.Status = (long)OMQueueStatus.Faulted;
                    processQueue.Save();
                }

                return;
            }
            bool error = false;

            // Обработка views. 
            foreach(var mView in mViews)
            {
                OMRefreshLog log = new OMRefreshLog()
                {
                    RefreshDate = DateTime.Now,
                    MvName = mView.MvName
                };

                try
                {
                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {
                        // Обновление.
                        mvService.UpdateMvRefreshList(mView);
                        log.MsgEvent = "Success";
                        log.Save();
                        ts.Complete();
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    log.MsgEvent = "Error";
                    log.ErrMessage = ex.Message;
                    log.Save();
                    error = true;
                }
            }
       
            if (error)
            {
                throw new Exception("При обработке представлений возникла ошибка");
            }
        }

        public bool Test()
        {
            return true;
        }
    }
}
