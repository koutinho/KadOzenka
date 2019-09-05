using System;
using System.Collections.Generic;
using System.Text;

using DebugApplication.Model;

namespace DebugApplication.Parser
{
    interface IParser
    {
        void GetProperty(string link);
        void GetProperty(List<string> links);
        List<PropertyObject> GetProperty();
    }
}
