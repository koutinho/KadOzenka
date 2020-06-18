using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class CorrelationResponse
    {
        [JsonProperty("coef")]
        public List<Result> CoefficientsForAttributes { get; set; }
    }

    public class Result
    {
        [JsonProperty("column_1")]
        public string FirstColumn { get; set; }
        [JsonProperty("column_2")]
        public string SecondColumn { get; set; }
        [JsonProperty("coefficient")]
        public decimal Coefficient { get; set; }
    }
}
