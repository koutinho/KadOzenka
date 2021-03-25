namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models
{
	public class ModelingProcessConfig
	{
		public string TrainingBaseUrl { get; set; }
		public string TrainingAllTypesUrl { get; set; }
		public string TrainingLinearTypeUrl { get; set; }
		public string TrainingExponentialTypeUrl { get; set; }
		public string TrainingMultiplicativeTypeUrl { get; set; }

		public string PredictionBaseUrl { get; set; }
		public string PredictionLinearTypeUrl { get; set; }
		public string PredictionExponentialTypeUrl { get; set; }
		public string PredictionMultiplicativeTypeUrl { get; set; }

		public string CorrelationUrl { get; set; }
    }
}