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

        //обновляется через UI
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
        public TrainingSpecificResult FisherCriterion { get; set; }
        [JsonProperty("r2")]
        public TrainingGeneralResult R2 { get; set; }
        [JsonProperty("student")]
        public TrainingSpecificResult Student { get; set; }
    }

    public class TrainingGeneralResult
    {
        [JsonProperty("train")]
        public string Train { get; set; }
        [JsonProperty("test")]
        public string Test { get; set; }
    }

    public class TrainingSpecificResult
    {
	    [JsonProperty("estimated")]
	    public string Estimated { get; set; }
	    [JsonProperty("tabular")]
	    public string Tabular { get; set; }
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
	    public QualityControlSpecial Student { get; set; }
	    [JsonProperty("mse")]
        public QualityControlSpecial MeanSquaredError { get; set; }
        [JsonProperty("r2")]
        public QualityControlSpecial R2 { get; set; }
        [JsonProperty("fisher")]
        public QualityControlSpecial Fisher { get; set; }

        public QualityControlInfo()
        {
	        Student = new QualityControlSpecial();
	        MeanSquaredError = new QualityControlSpecial();
	        R2 = new QualityControlSpecial();
	        Fisher = new QualityControlSpecial();
        }

        public void UpdateStudent(string criterion, bool conclusion)
        {
	        Student.Criterion = criterion;
	        Student.Conclusion = conclusion;
        }

        public void UpdateMse(string criterion, bool conclusion)
        {
	        MeanSquaredError.Criterion = criterion;
	        MeanSquaredError.Conclusion = conclusion;
        }

        public void UpdateR2(string criterion, bool conclusion)
        {
	        R2.Criterion = criterion;
	        R2.Conclusion = conclusion;
        }

        public void UpdateFisher(string criterion, bool conclusion)
        {
	        Fisher.Criterion = criterion;
	        Fisher.Conclusion = conclusion;
        }
    }

    public class QualityControlSpecial
    {
	    [JsonProperty("estimated")]
	    public string Estimated { get; set; }
	    [JsonProperty("tabular")]
	    public string Tabular { get; set; }
        [JsonProperty("criterion")]
	    public string Criterion { get; set; }
	    [JsonProperty("conclusion")]
	    public bool Conclusion { get; set; }
    }
}
