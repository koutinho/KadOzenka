using CIPJS.DAL.HierarchicalGridAnalysisDamage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectModel.Directory;
using Core.SRD;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CIPJS.DAL.Helpers;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTablePay
{
    class ForPay : IModelBuilder<SepTablePayEl, SRDUser>
    {
        public List<GroupEl<SepTablePayEl>> Create(SRDUser user)
        {
            var ret = new List<GroupEl<SepTablePayEl>>();

            var commandText =
                $@"
                select
                p.num as num,
                p.date as date,
                p.status_code as status_code,
                (select
                count(*)
                from insur_invoice i where i.link_reestr_pay = p.emp_id) as count
                from insur_reestr_pay p
                where (p.status_code = 12169001 or p.status_code = 12169002 or p.status_code = 12169003 or p.status_code = 12169006) and p.type_code = 12168001
                group by p.emp_id, p.num, p.date, p.status_code";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            var result = DBMngr.Realty.ExecuteDataSet(command).Tables[0];
            var objs = result.GetModelsByDataReader<SepTablePayEl>();

            ret.Add(GetSform(objs));
            ret.Add(GetPeredanVDgi(objs));
            ret.Add(GetYtverjVDgi(objs));
            ret.Add(GetPeredanNaOplatu(objs));

            return ret;
        }

        object IModelBuilder.Create(object objs) => Create((SRDUser)objs);

        private GroupEl<SepTablePayEl> GetSform(List<SepTablePayEl> objs)
        {
            var result = new GroupEl<SepTablePayEl>();

            result.GroupVal = "Сформирован";
            var h = objs.Where(x => x.Status_code != null && x.Status_code.Value == (int)ReestrPayStatus.Formed).ToList();
            result.Value = h;
            result.Count = h.Count;

            return result;
        }

        private GroupEl<SepTablePayEl> GetPeredanVDgi(List<SepTablePayEl> objs)
        {
            var result = new GroupEl<SepTablePayEl>();

            result.GroupVal = "Передан в ДГИ";
            var h = objs.Where(x => x.Status_code != null && x.Status_code.Value == (int)ReestrPayStatus.TransferredDGI).ToList();
            result.Value = h;
            result.Count = h.Count;

            return result;
        }

        private GroupEl<SepTablePayEl> GetYtverjVDgi(List<SepTablePayEl> objs)
        {
            var result = new GroupEl<SepTablePayEl>();

            result.GroupVal = "Утвержден в ДГИ";
            var h = objs.Where(x => x.Status_code != null && x.Status_code.Value == (int)ReestrPayStatus.ApprovedDGI).ToList();
            result.Value = h;
            result.Count = h.Count;

            return result;
        }

        private GroupEl<SepTablePayEl> GetPeredanNaOplatu(List<SepTablePayEl> objs)
        {
            var result = new GroupEl<SepTablePayEl>();

            result.GroupVal = "Передан на оплату";
            var h = objs.Where(x => x.Status_code != null && x.Status_code.Value == (int)ReestrPayStatus.TransferredPayment).ToList();
            result.Value = h;
            result.Count = h.Count;

            return result;
        }
    }
}
