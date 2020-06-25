namespace KadOzenka.Dal.Modeling
{
    public class ModelingProcessConfig
    {
        public static ModelingProcessConfig Current => Core.ConfigParam.Configuration.GetParam<ModelingProcessConfig>("ModelingProcessConfig");

        public string TrainingBaseUrl { get; set; }
        public string TrainingLinearTypeUrl { get; set; }
        public string TrainingExponentialTypeUrl { get; set; }
        public string TrainingMultiplicativeTypeUrl { get; set; }

        public string PredictionUrl { get; set; }

        public string CorrelationUrl { get; set; }
    }
}
