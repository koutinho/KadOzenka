using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ObjectModel.Market;

namespace KadOzenka.BlFrontEnd.ClearDuplicates
{
    class DuplicateCleaner
    {
        readonly List<OMCoreObject> AllObjects =
            OMCoreObject
                .Where(x => x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr &&
                            x.ProcessType_Code == ObjectModel.Directory.ProcessStep.CadastralNumberStep)
                .Select(x => new 
                { 
                    x.CadastralNumber, 
                    x.DealType_Code, 
                    x.PropertyType_Code 
                })
                .Execute()
                .ToList();

        public void Launch()
        {
            List<List<OMCoreObject>> objs = AllObjects
                .GroupBy(x => new 
                { 
                    x.CadastralNumber, 
                    x.DealType_Code, 
                    x.PropertyType_Code 
                })
                .Select(grp => grp.ToList())
                .ToList();
            Console.WriteLine(objs.Count);
        }


    }
}
