using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class CorrelationRequest
    {
        [JsonProperty("y")]
        public List<List<decimal>> Prices { get; set; }
        [JsonProperty("columns")]
        public List<string> AttributeNames { get; set; }
        [JsonProperty("x")]
        public List<List<decimal?>> Coefficients { get; set; }

        public CorrelationRequest()
        {
            Prices = new List<List<decimal>>();
            AttributeNames = new List<string>();
            Coefficients = new List<List<decimal?>>();
        }
    }
}
