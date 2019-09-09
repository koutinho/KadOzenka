using System;
using System.Collections.Generic;
using System.Text;

namespace OuterMarketParser.LinksGenerator
{
    interface ILinkGenerator
    {
        List<string> GenerateCianLinks();
    }
}
