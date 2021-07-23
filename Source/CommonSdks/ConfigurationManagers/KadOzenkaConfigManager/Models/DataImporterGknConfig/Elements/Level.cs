namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class Level
	{
		public string NumberAttributeId { get; set; }
		public long? NumberAttributeIdValue => long.TryParse(NumberAttributeId, out var val) ? val : (long?)null;

		public string TypeAttributeId { get; set; }
		public long? TypeAttributeIdValue => long.TryParse(TypeAttributeId, out var val) ? val : (long?)null;

		public string PositionNumberOnPlanAttributeId { get; set; }
		public long? PositionNumberOnPlanAttributeIdValue => long.TryParse(PositionNumberOnPlanAttributeId, out var val) ? val : (long?)null;

		public string PositionDescriptionAttributeId { get; set; }
		public long? PositionDescriptionAttributeIdValue => long.TryParse(PositionDescriptionAttributeId, out var val) ? val : (long?)null;

	}
}
