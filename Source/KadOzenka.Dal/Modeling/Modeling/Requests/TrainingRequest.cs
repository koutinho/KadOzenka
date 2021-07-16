using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Modeling.Requests
{
    public class TrainingRequest : GeneralRequest
    {
        [JsonIgnore]
        public List<long> AttributeIds { get; set; }

        [JsonProperty("y")]
        public List<List<decimal>> PricesForControl { get; set; }
        [JsonProperty("test_y")]
        public List<List<decimal>> PricesForTraining { get; set; }
        [JsonProperty("columns")]
        public List<string> AttributeNames { get; set; }
        [JsonProperty("x")]
        public List<List<decimal?>> CoefficientsForControl { get; set; }
        [JsonProperty("test_x")]
        public List<List<decimal?>> CoefficientsForTraining { get; set; }

        public TrainingRequest()
        {
            PricesForControl = new List<List<decimal>>();
            PricesForTraining = new List<List<decimal>>();
            AttributeNames = new List<string>();
            CoefficientsForControl = new List<List<decimal?>>();
            CoefficientsForTraining = new List<List<decimal?>>();
            AttributeIds = new List<long>();
        }
    }
}
