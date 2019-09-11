using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OuterMarketParser.Model;
using OuterMarketParser.DatabaseReader;

namespace OuterMarketParser.LinksGenerator
{
    class LinkGenerator : ILinkGenerator
    {
        public List<string> GenerateCianLinks(OuterMarketSettings settings)
        {
            List<string> result = new List<string>();
            var lastUpdateDate = settings.LastSuccesfulUpdateDate;
            while (lastUpdateDate < DateTime.Today)
            {
                DateTime currentTime = lastUpdateDate.AddSeconds(1);
                lastUpdateDate = lastUpdateDate.AddMinutes(settings.TimeDelta);
                foreach(int region in settings.RegionIDs)
                {
                    result.Add(string.Format(settings.Link,
                                             settings.Login,
                                             settings.Token,
                                             settings.DealId,
                                             $"region_id={region}",
                                             currentTime.ToString(settings.Template),
                                             lastUpdateDate.ToString(settings.Template)));
                    //Console.WriteLine(string.Format(settings.Link,
                    //                                settings.Login,
                    //                                settings.Token,
                    //                                settings.DealId,
                    //                                $"region_id={region}",
                    //                                currentTime.ToString(settings.Template),
                    //                                lastUpdateDate.ToString(settings.Template)));
                }
            }
            return result.ToList();
        }
    }
}
