using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class TrainingResponse
    {
        [JsonProperty("coef")]
        public Coefficients Coefficients { get; set; }

        [JsonProperty("accuracy_score")]
        public AccuracyScore AccuracyScore { get; set; }

        [JsonProperty("data")]
        public TrainingGeneralResult Data { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("quality_control_info")]
        public QualityControlInfo QualityControlInfo { get; set; }

        public TrainingResponse()
        {
	        QualityControlInfo = new QualityControlInfo();
        }
    }

    public class Coefficients
    {
	    [JsonProperty("factors")]
	    public Dictionary<string, decimal> CoefficientsForAttributes { get; set; }

	    [JsonProperty("free_member")]
        public decimal A0 { get; set; }
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

    public class QualityControlInfo
    {
	    [JsonProperty("student")]
	    public QualityControlGeneral Student { get; set; }
	    [JsonProperty("mse")]
        public QualityControlGeneral MeanSquaredError { get; set; }
        [JsonProperty("r2")]
        public QualityControlGeneral R2 { get; set; }
        [JsonProperty("fisher")]
        public QualityControlGeneral Fisher { get; set; }

        public QualityControlInfo()
        {
	        Student = new QualityControlGeneral();
	        MeanSquaredError = new QualityControlGeneral();
	        R2 = new QualityControlGeneral();
	        Fisher = new QualityControlGeneral();
        }
    }

    public class QualityControlGeneral
    {
	    [JsonProperty("criterion")]
	    public string Criterion { get; set; }
	    [JsonProperty("conclusion")]
	    public bool Conclusion { get; set; }
    }
}
