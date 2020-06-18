using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class CorrelationResponse
    {
        [JsonProperty("coef")]
        public Dictionary<string, decimal> CoefficientsForAttributes { get; set; }
    }
}
