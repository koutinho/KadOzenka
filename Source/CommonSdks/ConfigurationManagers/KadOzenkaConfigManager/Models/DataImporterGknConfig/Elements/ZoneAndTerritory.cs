namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class ZoneAndTerritory
	{
		public string DescriptionAttributeId { get; set; }
		public long? DescriptionAttributeIdValue => long.TryParse(DescriptionAttributeId, out var val) ? val : (long?)null;
		
		public string CodeZoneDocAttributeId { get; set; }
		public long? CodeZoneDocAttributeIdValue => long.TryParse(CodeZoneDocAttributeId, out var val) ? val : (long?)null;

		public string AccountNumberAttributeId { get; set; }
		public long? AccountNumberAttributeIdValue => long.TryParse(AccountNumberAttributeId, out var val) ? val : (long?)null;

		public string ContentRestrictionsAttributeId { get; set; }
		public long? ContentRestrictionsAttributeIdValue => long.TryParse(ContentRestrictionsAttributeId, out var val) ? val : (long?)null;


		public string FullPartlyAttributeId { get; set; }
		public long? FullPartlyAttributeIdValue => long.TryParse(FullPartlyAttributeId, out var val) ? val : (long?)null;

		public Document Document { get; set; }
	}
}
