using CIPJS.DAL.HierarchicalGridAnalysisDamage.Models;
using Core.SRD;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTable
{
    class ForInspector : BaseSepTable
    {
        public override List<GroupEl<SepTableEl>> Create(SRDUser objs)
        {
            var result = new List<GroupEl<SepTableEl>>();
            var data = GetData(null, null);

            result.Add(GetPeredanoNaViplatu(data));
            result.Add(GetPeredanoNaProvOtkaz(data));
            result.Add(GetProverenoNaViplatu(data));
            result.Add(GetProverenoOtkaz(data));
            result.Add(GetPerevipuskZakluch(data));

            return result;
        }
    }
}
