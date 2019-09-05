using System;
using System.Collections.Generic;
using System.Text;

using DebugApplication.Model;

namespace DebugApplication.Model.DatabaseOperations
{
    interface IDataToPostgreSQL
    {
        void SaveObject(PropertyObject element);
    }
}
