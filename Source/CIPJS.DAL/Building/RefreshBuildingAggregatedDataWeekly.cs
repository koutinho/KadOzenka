using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;

namespace CIPJS.DAL.Building
{
    public class RefreshBuildingAggregatedDataWeekly : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            const string cmd =
@"create temp table tmp_insur_svod_data_calculated_nach_plat as
select *
from v_insur_svod_data_calculated_nach_plat
;
update insur_svod_data_calculated
set nach_mfc_tek_period = coalesce(tmp.nach, 0),
    nach_mfc_tek_period_1 = coalesce(tmp.nach1, 0),
    nach_mfc_tek_period_2 = coalesce(tmp.nach2, 0),
    plat_mfc_tek_period = coalesce(tmp.plat, 0),
    plat_mfc_tek_period_1 = coalesce(tmp.plat1, 0),
    plat_mfc_tek_period_2 = coalesce(tmp.plat2, 0)
from tmp_insur_svod_data_calculated_nach_plat as tmp
where insur_svod_data_calculated.link_mkd=tmp.link_mkd
    and insur_svod_data_calculated.link_bti=tmp.link_bti
;
drop table tmp_insur_svod_data_calculated_nach_plat";
            DBMngr.Realty.ExecuteNonQuery(DBMngr.Realty.GetSqlStringCommand(cmd), false);
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null) { }

        public bool Test() => true;
    }
}
