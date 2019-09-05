using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using DebugApplication.Model;

namespace DebugApplication.LinksGenerator
{
    class LinkGenerator : ILinkGenerator
    {
        private readonly JSONSettings _settings = JsonConvert.DeserializeObject<JSONSettings>(JObject.Parse(File.ReadAllText(@"Settings\ConnectionsSettings.json"))["api"].ToString());

        public List<string> GenerateCianLinks()
        {
            List<string> result = new List<string>();
            var yesterday = DateTime.Today.AddDays(-1);
            while (yesterday < DateTime.Today)
            {
                DateTime currentTime = yesterday.AddSeconds(1);
                yesterday = yesterday.AddMinutes(_settings.MinutesDelta);
                result.Add(_settings.ToString(currentTime.ToString(_settings.Cian.DateTimeTemplate), yesterday.ToString(_settings.Cian.DateTimeTemplate)));
            }
            return result.Take(1).ToList();
        }
    }
}
