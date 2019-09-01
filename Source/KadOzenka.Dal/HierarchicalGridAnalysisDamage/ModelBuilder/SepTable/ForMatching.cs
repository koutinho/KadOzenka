using CIPJS.DAL.HierarchicalGridAnalysisDamage.Models;
using Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTable
{
    class ForMatching : BaseSepTable
    {
        public override List<GroupEl<SepTableEl>> Create(SRDUser objs)
        {
            var result = new List<GroupEl<SepTableEl>>();
            var data = GetData(null, null);

            result.Add(GetProverenoNaViplatu(data));
            result.Add(GetProverenoOtkaz(data));
            result.Add(GetSoglasovanoNaViplatu(data));
            result.Add(GetOtkazVViplateSoglas(data));
            result.Add(GetPerevipuskZakluch(data));

            return result;
        }

        private GroupEl<SepTableEl> GetSoglasovanoNaViplatu(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Согласовано \"На Выплату\"";
            var filtered = objs.Where(x => x.DamageStatus == (long)StatusDamageAmount.Agreed
            && x.InvoiceStatus == (long)InvoiceStatus.TransferredPayment).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }

        private GroupEl<SepTableEl> GetOtkazVViplateSoglas(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Отказ в Выплате\"/Согласовано\"";
            var filtered = objs.Where(x => x.InvoiceStatus == (long)InvoiceStatus.DeniedAgreed).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }
    }
}
