using System;
using System.Text;
using System.Collections.Generic;

using OuterMarketParser.DatabaseReader;

namespace OuterMarketParser.LinksGenerator
{

    interface ILinkGenerator
    {

        List<string> GenerateCianLinks(OuterMarketSettings settings);

    }

}
