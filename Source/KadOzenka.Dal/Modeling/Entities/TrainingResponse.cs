using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class TrainingResponse
    {
        [JsonProperty("coef")]
        public Dictionary<string, decimal> CoefficientsForAttributes { get; set; }

        [JsonProperty("accuracy_score")]
        public AccuracyScore AccuracyScore { get; set; }

        [JsonProperty("data")]
        public TrainingGeneralResult Data { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }
    }

    public class AccuracyScore
    {
        [JsonProperty("mse")]
        public TrainingGeneralResult MeanSquaredError { get; set; }
        [JsonProperty("fisher")]
        public TrainingGeneralResult FisherCriterion { get; set; }
        [JsonProperty("r2")]
        public TrainingGeneralResult R2 { get; set; }
    }

    public class TrainingGeneralResult
    {
        [JsonProperty("train")]
        public string Train { get; set; }
        [JsonProperty("test")]
        public string Test { get; set; }
    }

    public class Images
    {
        [JsonProperty("scatter")]
        public string ScatterLink { get; set; }
        [JsonProperty("correlation")]
        public string CorrelationLink { get; set; }
    }
}
