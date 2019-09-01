using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CIPJS.DAL.Fsp
{
    public class FspPolisUpdateBalancesProcess : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            FspService fspService = new FspService();

            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 10
            };

            int package = 0;

            while (true)
            {
                List<OMFsp> fspList = OMFsp.Where(x => x.FspType_Code == FSPType.Polis && x.DateOpen != null)
                    .SelectAll()
                    .SetPackageSize(1000)
                    .SetPackageIndex(package)
                    .Execute();

                if (fspList.Count == 0)
                {
                    break;
                }

                Parallel.ForEach(fspList, options, fsp =>
                {
                    if (fsp.DateOpen.HasValue)
                    {
                        fspService.AccountFsp(fsp.EmpId, fsp.DateOpen.Value, fsp: fsp);
                        fspService.CalcBalanceSumFromPeriod(fsp.EmpId, fsp.DateOpen.Value, fsp: fsp);
                    }
                });

                package++;
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
