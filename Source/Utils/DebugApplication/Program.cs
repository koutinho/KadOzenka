using System;
using System.Collections.Generic;
using System.Linq;

using Core.ErrorManagment;
using Core.SRD;
using ObjectModel.Market;

using DebugApplication.LinksGenerator;
using DebugApplication.Parser.Cian;

namespace DebugApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> links = new LinkGenerator().GenerateCianLinks();
            Parser.Cian.Client client = new Parser.Cian.Client(new CianDataParser(links));
            Console.WriteLine(string.Join("\n", client.Parser.GetProperty().Select(x => x.ToString())));
            client.Parser.GetProperty().ForEach(
                element => 
                {
                    OMCoreObject obj = new OMCoreObject
                    {
                        Url = element.Url,
                        Price = element.Price,
                        MarketId = 1,
                        Code = "1"
                    };
                    OMCianObject cianObj = new OMCianObject
                    {
                        Id = obj.Id,
                        Code = "145"
                    };

                    obj.Save();
                    cianObj.Save();
                }
            );
           
        }
    }
}
