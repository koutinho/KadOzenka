using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class PredictionResult
    {
        [JsonProperty("y_predict")]
        public List<decimal> Prices { get; set; }
    }
}
