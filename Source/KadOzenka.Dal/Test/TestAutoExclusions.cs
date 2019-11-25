using ObjectModel.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace KadOzenka.Dal.Test
{
    public class TestAutoExclusions
    {

        readonly List<OMCoreObject> AllObjects =
            OMCoreObject.Where(x => x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr && 
                                    x.DealType_Code == ObjectModel.Directory.DealType.SaleSuggestion && 
                                    (x.Subcategory == "Офисная" || x.Subcategory == "Торговая"))
                        .Select(x => new { x.CadastralNumber, x.Description, x.ExclusionStatus_Code, x.ProcessType_Code, x.Url, x.Subcategory })
                        .Execute()
                        .ToList();

        public void TryParse()
        {
            IEnumerable<OMCoreObject> rentList = AllObjects.Where(x => Regex.IsMatch(x.Description, @"([^А-Яа-я]ппа)|(прав аренды)|(права аренды)", RegexOptions.IgnoreCase));
            IEnumerable<OMCoreObject> furnitureis = AllObjects.Where(x => Regex.IsMatch(x.Description, @"(мебель[^(ный)])", RegexOptions.IgnoreCase));
            Console.WriteLine(rentList.Count());
            Console.WriteLine(furnitureis.Count());
            Console.WriteLine(AllObjects.Count);
        }

    }
}
