using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Npgsql;

using Core.Register.LongProcessManagment;
using ObjectModel.Market;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DebugApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            LongProcessManagementService service = new LongProcessManagementService();
            service.Start();
            return;
        }
    }
}
