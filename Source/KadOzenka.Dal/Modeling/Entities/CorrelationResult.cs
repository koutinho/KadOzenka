using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class CorrelationResult
    {
        [JsonProperty("coef")]
        public Dictionary<string, decimal> CoefficientsForAttributes { get; set; }
    }
}
