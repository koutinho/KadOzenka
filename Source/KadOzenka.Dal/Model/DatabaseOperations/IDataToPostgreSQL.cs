using System;
using System.Collections.Generic;
using System.Text;

using OuterMarketParser.Model;

namespace OuterMarketParser.Model.DatabaseOperations
{
    interface IDataToPostgreSQL
    {
        void SaveObject(PropertyObject element);
    }
}
