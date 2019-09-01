using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIPJS.DAL.Fsp
{
    public class FspLinkObjectProcess : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            if (processQueue == null)
            {
                return;
            }

            FspService fspService = new FspService();
			
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 10
            };

            //Предварительно очищаем obj_reestr_id, чтобы мы могли выгружать ФСП пакетно
            DbCommand prepareCommand = DBMngr.Realty.GetSqlStringCommand(@"update insur_fsp_q f
set obj_reestr_id = null
where f.obj_id is null and obj_reestr_id = 317;");
            DBMngr.Realty.ExecuteNonQuery(prepareCommand);

			ConcurrentBag<string> log = new ConcurrentBag<string>();

			int linked = 0;
			int notLinked = 0;

			List<OMFsp> fspList = OMFsp.Where(x => x.ObjReestrId == null && x.ObjId == null) // && x.FspNumber == "2610774715–6895-27-001"
					.Select(x => x.ObjReestrId)
					.Select(x => x.ObjId)
					.Select(x => x.Kodpl)
					.Select(x => x.FlagManyObj)
					.Select(x => x.NumObj)
					.SetPackageSize(50000) // Лимит, чтоб не возникло SystemOutOfMemory
					.Execute();
			
			Parallel.ForEach(fspList, options, fsp =>
			{
				OMInputNach inputNach = OMInputNach.Where(x => x.FspId == fsp.EmpId && x.TypeSource_Code == InsuranceSourceType.Mfc)
					.Select(x => x.Unom)
					.Select(x => x.Kvnom)
					.Select(x => x.Kodpl)
					.OrderByDescending(x => x.PeriodRegDate)
					.SetPackageSize(1)
					.ExecuteFirstOrDefault();

				//CIPJS-746 TODO рефакторинг fspService для использования валидации из CreateNachFsp
				if (inputNach != null)
				{
					List<string> linkErrors;
					fspService.LinkFspFlat(fsp, inputNach.Unom, inputNach.Kvnom, out linkErrors);

					if (linkErrors.Count > 0)
					{
						foreach (string linkError in linkErrors)
						{
							log.Add(linkError);
						}
					}
				}

				if (fsp.ObjId != null && fsp.ObjReestrId != null)
				{
					linked++;
					fsp.Save();
				}
				else
				{
					notLinked++;
				}
			});

			processQueue.Message = $"Получено: {fspList.Count}; Связано: {linked}; Не связано: {notLinked}";
			
			processQueue.Log = string.Join(Environment.NewLine, log).Truncate(10000);
			processQueue.Save();
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
