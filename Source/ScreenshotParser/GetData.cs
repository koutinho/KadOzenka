using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ObjectModel.Directory;
using ObjectModel.Market;

namespace ScreenshotParser
{

    class GetData
    {

        public List<OMCoreObject> AllObjects { get; set; }

        public GetData(MarketTypes marketType)
        {
            AllObjects = OMCoreObject
                .Where(x => 
                    x.Url != null && 
                    x.Market_Code == marketType && 
                    x.ProcessType_Code != ProcessStep.Excluded)
                .Select(x => new
                {
                    x.Url,
                    x.Price,
                    x.ExclusionStatus,
                    x.ExclusionStatus_Code,
                    x.ProcessType,
                    x.ProcessType_Code,
                    x.PropertyMarketSegment_Code
                })
                .Execute();
            List<OMScreenshots> screenshots = OMScreenshots
                .Where(x => true)
                .Select(x => new { x.Id, x.InitialId })
                .Execute()
                .ToList();
            List<long?> screenshotIds = screenshots.Select(x => x.InitialId).OrderBy(x => x.Value).ToList();
            AllObjects = AllObjects.Where(x => screenshotIds.BinarySearch(x.Id) < 0).ToList();
            Console.WriteLine(AllObjects.Count);
        }

    }

}
