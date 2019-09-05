using System;
using System.Collections.Generic;
using System.Linq;

using DebugApplication.LinksGenerator;
using DebugApplication.Parser.Cian;
using DebugApplication.Model.DatabaseOperations;

namespace DebugApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> links = new LinkGenerator().GenerateCianLinks();
            Parser.Cian.Client client = new Parser.Cian.Client(new CianDataParser(links));
            Model.DatabaseOperations.Client dbCient = new Model.DatabaseOperations.Client(new DataToPostgreSQL(client.Parser.GetProperty()));
        }
    }
}
