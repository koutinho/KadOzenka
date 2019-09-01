using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder;
using CIPJS.DAL.HierarchicalGridAnalysisDamage.Models;
using ObjectModel.Insur;
using ObjectModel.Directory;
using Core.SRD;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTable
{
    class ForInjectors : BaseSepTable
    {
        public override List<GroupEl<SepTableEl>> Create(SRDUser objs)
        {
            var result = new List<GroupEl<SepTableEl>>();
            var data = GetData("AGREEMENT_ID_1", objs);

            result.Add(GetCreate(data));
            result.Add(GetRascetUscherba(data));
            result.Add(GetRashojdenia(data));
            result.Add(GetPeredanoNaViplatu(data));
            result.Add(GetPeredanoNaProvOtkaz(data));

            return result;
        }

        private GroupEl<SepTableEl> GetCreate(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Создано";
            var filtered = objs.Where(x => x.DamageStatus == (long)StatusDamageAmount.Created).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }

        private GroupEl<SepTableEl> GetRascetUscherba(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Расчёт ущерба совпадает с данными СК";
            var filtered = objs.Where(x => x.DamageStatus == (long)StatusDamageAmount.DamageAmountCoincides).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }

        private GroupEl<SepTableEl> GetRashojdenia(List<SepTableEl> objs)
        {
            var result = new GroupEl<SepTableEl>();

            result.GroupVal = "Расхождения со СК в расчёте ущерба";
            var filtered = objs.Where(x => x.DamageStatus == (long)StatusDamageAmount.DamageAmountDiscrepancies).ToList();
            result.Value = filtered;
            result.Count = result.Value.GroupBy(x => x.DamageId).Count(x => x.Key != null);

            return result;
        }        
    }
}
