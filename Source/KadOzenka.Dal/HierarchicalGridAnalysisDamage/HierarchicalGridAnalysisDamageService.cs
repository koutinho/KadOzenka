using System;
using CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder;
using CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTable;
using CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTablePay;
using CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTableRet;
using ObjectModel.SRD;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage
{
    public class HierarchicalGridAnalysisDamageService
    {
        public object GetDataST(Core.SRD.SRDUser user, string[] strFunc, int index, int? AdminIndex)
        {
            var h = new Func<IModelBuilder>[][]
            {
                new Func<IModelBuilder>[]{ () => new ForInjectors() },
                new Func<IModelBuilder>[]{ () => new ForInspector(), () => new ForPay(), () => new ForRet() },
                new Func<IModelBuilder>[]{ () => new ForMatching(), () => new ForPay(), () => new ForRet() }
            };

            var builder = InitGroupSelect(new Func<IModelBuilder>[][]
            {
                h[0],
                h[1],
                h[2],
                h[AdminIndex??0],
            });

            var mB = builder.Serch(strFunc)[index]();

            var res = mB.Create(user);

            return res;
        }

        public GroupSelect<string, T> InitGroupSelect<T>(T[] values)
        {
            var res = new GroupSelect<string, T>();

            res.Add(new GroupSelectEl<string>(
                new string[]
                {
                    SRDCoreFunctions.INSUR_HIERARCHICALGRIDANALYSISDAMAGE_INJECTORS
                },
                new int[]
                {
                    1
                }), values[0]);

            res.Add(new GroupSelectEl<string>(
                new string[]
                {
                    SRDCoreFunctions.INSUR_HIERARCHICALGRIDANALYSISDAMAGE_INSPECTOR
                },
                new int[]
                {
                    2
                }), values[1]);

            res.Add(new GroupSelectEl<string>(
                new string[]
                {
                    SRDCoreFunctions.INSUR_HIERARCHICALGRIDANALYSISDAMAGE_MATCHING
                },
                new int[]
                {
                    3
                }), values[2]);

            res.Add(new GroupSelectEl<string>(
               new string[]
               {
                    "Admin"
               },
               new int[]
               {
                    4
               }), values[3]);

            return res;
        }
    }
}
