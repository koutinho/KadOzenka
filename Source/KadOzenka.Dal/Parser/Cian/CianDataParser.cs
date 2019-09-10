using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OuterMarketParser.Model;
using OuterMarketParser.Parser;
using OuterMarketParser.Exceptions;

namespace OuterMarketParser.Parser.Cian
{
    class CianDataParser : IParser
    {
        public List<PropertyObject> Property { get; set; } = new List<PropertyObject>();

        public CianDataParser(List<string> links) => GetProperty(links);

        public void GetProperty(string link)
        {
            try
            {
                string data = new StreamReader(WebRequest.Create(link).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();
                List<PropertyObject> initArray = JsonConvert.DeserializeObject<List<RestAPICianPropertyObject>>(((JObject)JsonConvert.DeserializeObject(data))["data"].ToString())
                                                            .Select(x => new PropertyObject(x)).ToList();
                Property.AddRange(initArray);
                if (initArray.Count >= 50) throw new ParserFullFillException($"Переполнение в запросе: {link}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetProperty(List<string> links)
        {
            foreach (string link in links) GetProperty(link);
        }

        public List<PropertyObject> GetProperty() => Property;
    }
}
