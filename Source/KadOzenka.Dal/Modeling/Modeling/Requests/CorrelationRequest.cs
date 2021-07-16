using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Modeling.Requests
{
    public class CorrelationRequest
    {
        [JsonProperty("columns")]
        public List<string> AttributeNames { get; set; }
        [JsonProperty("x")]
        public List<List<decimal?>> Coefficients { get; set; }

        public CorrelationRequest()
        {
            AttributeNames = new List<string>();
            Coefficients = new List<List<decimal?>>();
        }
    }
}
