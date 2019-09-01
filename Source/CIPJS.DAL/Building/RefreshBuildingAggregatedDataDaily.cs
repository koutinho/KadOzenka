using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;

namespace CIPJS.DAL.Building
{
    public class RefreshBuildingAggregatedDataDaily : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            const string cmd =
@"insert into insur_svod_data_calculated
(unom, link_mkd, link_bti, kol_gp_egrn, kol_gp_bti, kol_gp_mfc, nach_mfc_tek_period, nach_mfc_tek_period_1, nach_mfc_tek_period_2, plat_mfc_tek_period, plat_mfc_tek_period_1, plat_mfc_tek_period_2)
select distinct
	b.unom,
	b.emp_id AS link_mkd,
	coalesce(lbb.id_bti_fsks,-1) AS link_bti,
    0 AS kol_gp_egrn,
    0 AS kol_gp_bti,
    0 AS kol_gp_mfc,
	0 AS nach_mfc_tek_period,
	0 AS nach_mfc_tek_period_1,
	0 AS nach_mfc_tek_period_2,
	0 AS plat_mfc_tek_period,
	0 AS plat_mfc_tek_period_1,
	0 AS plat_mfc_tek_period_2
from insur_building_q b
left join insur_link_build_bti lbb on b.emp_id = lbb.id_insur_build
where b.actual = 1 and not exists (
	select 1
	from insur_svod_data_calculated s
	where s.link_mkd=b.emp_id and s.link_bti=coalesce(lbb.id_bti_fsks,-1)
)
;
create temp table tmp_insur_svod_data_calculated as
select *
from v_insur_svod_data_calculated
;
update insur_svod_data_calculated
set kol_gp_egrn = tmp.kol_gp_egrn,
    kol_gp_bti = tmp.kol_gp_bti,
    kol_gp_mfc=tmp.kol_gp_mfc
from tmp_insur_svod_data_calculated as tmp
where insur_svod_data_calculated.link_mkd=tmp.link_mkd
    and insur_svod_data_calculated.link_bti=tmp.link_bti
;
drop table tmp_insur_svod_data_calculated";
            DBMngr.Realty.ExecuteNonQuery(DBMngr.Realty.GetSqlStringCommand(cmd), false);
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null) { }

        public bool Test() => true;
    }
}
