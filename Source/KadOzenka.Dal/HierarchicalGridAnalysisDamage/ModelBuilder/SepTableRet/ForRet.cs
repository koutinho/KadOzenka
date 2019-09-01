using CIPJS.DAL.Helpers;
using CIPJS.DAL.HierarchicalGridAnalysisDamage.Models;
using Core.SRD;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTableRet
{
    class ForRet : IModelBuilder<SepTableRetEl, SRDUser>
    {
        public List<GroupEl<SepTableRetEl>> Create(SRDUser user)
        {
            var ret = new List<GroupEl<SepTableRetEl>>();

            var commandText =
                $@"
                select
                p.num as izn_num,
                p.date as izn_date,
                d.sum_damage as sum_damage,
                i.sum_opl as sum_opl,
                d.strah_plat as strah_plat,
                s.fio_adm as fio,
                i.num_invoice as num_invoice,
                povt.povt_num,
                povt.povt_date
                from insur_reestr_pay p
                join insur_invoice i on i.link_reestr_pay = p.emp_id
                join insur_damage d on i.link_damage = d.emp_id
                left join insur_subject s on s.emp_id = i.subject_id
                join (select 
                d2.emp_id,
                i2.emp_id as id,
                p2.num as povt_num,
                p2.date as povt_date
                from insur_invoice i2
                join insur_damage d2 on i2.link_damage = d2.emp_id
                join insur_reestr_pay p2 on i2.link_reestr_pay = p2.emp_id
                where not(i2.status_code = 12170004)
                ) povt on d.emp_id = povt.emp_id and not(povt.id = i.emp_id)
                where type_code = 12168003 and i.status_code = 12170004";
            //where (d.AGREEMENT_ID_2 = {user.ID} or d.MAIN_AGREEMENT_ID = {user.ID}) and type_code = 12168003 and i.status_code = 12170004

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            var result = DBMngr.Realty.ExecuteDataSet(command).Tables[0];
            var objs = result.GetModelsByDataReader<SepTableRetEl>();

            ret.Add(VozvrIzKaz(objs));

            return ret;
        }

        object IModelBuilder.Create(object objs) => Create((SRDUser)objs);

        private GroupEl<SepTableRetEl> VozvrIzKaz(List<SepTableRetEl> objs)
        {
            var result = new GroupEl<SepTableRetEl>();

            result.GroupVal = "Возврат из Казначейства (Ошибка в реквизитах)";
            result.Value = objs;
            result.Count = objs.Count;

            return result;
        }
    }
}
