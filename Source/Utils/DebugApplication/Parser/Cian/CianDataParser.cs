using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using DebugApplication.Model;

namespace DebugApplication.Parser.Cian
{
    class CianDataParser : IParser
    {
        public List<PropertyObject> Property { get; set; } = new List<PropertyObject>();

        public CianDataParser(List<string> links) => GetProperty(links);

        public void GetProperty(string link)
        {
            string data = new StreamReader(WebRequest.Create(link).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();
            Property.AddRange(
                JsonConvert.DeserializeObject<List<RestAPICianPropertyObject>>(
                    ((JObject)JsonConvert.DeserializeObject(data))["data"].ToString()
                ).Select(x => new PropertyObject(x))
            );
        }

        public void GetProperty(List<string> links)
        {
            foreach (string link in links)
            {
                GetProperty(link);
                Console.WriteLine($"===>{Property.Count}");
            }
        }

        public List<PropertyObject> GetProperty() => Property;
    }
}
