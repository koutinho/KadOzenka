using System;
using System.Collections.Generic;
using System.Text;

using OuterMarketParser.Model;

namespace OuterMarketParser.Parser
{
    interface IParser
    {
        void GetProperty(string link);
        void GetProperty(List<string> links);
        List<PropertyObject> GetProperty();
    }
}
