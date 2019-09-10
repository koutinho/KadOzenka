using System;
using System.Collections.Generic;
using System.Text;

using OuterMarketParser.DatabaseReader;

namespace OuterMarketParser.LinksGenerator
{
    interface ILinkGenerator
    {
        List<string> GenerateCianLinks(OuterMarketSettings settings);
    }
}
