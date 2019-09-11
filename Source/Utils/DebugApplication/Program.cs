using System;
using System.Collections.Generic;
using System.Linq;

using Core.Register.LongProcessManagment;

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
