using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class PredictionResponse
    {
        [JsonProperty("y_predict")]
        public List<decimal> Prices { get; set; }
    }
}
