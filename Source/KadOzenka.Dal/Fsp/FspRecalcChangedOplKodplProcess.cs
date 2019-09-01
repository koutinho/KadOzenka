using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CIPJS.DAL.Fsp
{
    public class FspRecalcChangedOplKodplProcess : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            //перерасчитываем на актуальный период
            DateTime actualPeriod = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            FspService fspService = new FspService();

            int packageIndex = 0;

            while (true)
            {
                List<OMFsp> fspList = OMFsp.Where(x => x.OplKodplUpdateDate != null)
                    .SelectAll()
                    .SetPackageSize(1000)
                    .SetPackageIndex(packageIndex)
                    .Execute();

                if (fspList.Count == 0)
                {
                    break;
                }

                ParallelOptions options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 10
                };

                Parallel.ForEach(fspList, options, fsp =>
                {
                    fspService.AccountFsp(fsp.EmpId, actualPeriod);
                    fspService.CalcBalanceSumFromPeriod(fsp.EmpId, actualPeriod);
                });

                packageIndex++;
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
