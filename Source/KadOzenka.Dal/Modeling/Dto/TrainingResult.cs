using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Dto
{
    public class TrainingResult
    {
        [JsonProperty("coef")]
        public Dictionary<string, decimal> CoefficientsForAttributes { get; set; }

        [JsonProperty("accuracy_score")]
        public AccuracyScore AccuracyScore { get; set; }

        [JsonProperty("data")]
        public TrainingGeneralResult Data { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("link_to_graph")]
        public string LinkToGraph { get; set; }
    }

    public class AccuracyScore
    {
        [JsonProperty("mean_squared_error")]
        public TrainingGeneralResult MeanSquaredError { get; set; }
        [JsonProperty("Fisher_criterion")]
        public TrainingGeneralResult FisherCriterion { get; set; }
        [JsonProperty("R2")]
        public TrainingGeneralResult R2 { get; set; }
    }

    public class TrainingGeneralResult
    {
        [JsonProperty("train")]
        public string Train { get; set; }
        [JsonProperty("test")]
        public string Test { get; set; }
    }
}
