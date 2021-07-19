namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class HiredHouse
	{
		public string UseHiredHouseAttributeId { get; set; }
		public long? UseHiredHouseAttributeIdValue => long.TryParse(UseHiredHouseAttributeId, out var val) ? val : (long?)null;

		public string ActBuildingAttributeId { get; set; }
		public long? ActBuildingAttributeIdValue => long.TryParse(ActBuildingAttributeId, out var val) ? val : (long?)null;

		public string ActDevelopmentAttributeId { get; set; }
		public long? ActDevelopmentAttributeIdValue => long.TryParse(ActDevelopmentAttributeId, out var val) ? val : (long?)null;

		public string ContractBuildingAttributeId { get; set; }
		public long? ContractBuildingAttributeIdValue => long.TryParse(ContractBuildingAttributeId, out var val) ? val : (long?)null;

		public string ContractDevelopmentAttributeId { get; set; }
		public long? ContractDevelopmentAttributeIdValue => long.TryParse(ContractDevelopmentAttributeId, out var val) ? val : (long?)null;

		public string OwnerDecisionAttributeId { get; set; }
		public long? OwnerDecisionAttributeIdValue => long.TryParse(OwnerDecisionAttributeId, out var val) ? val : (long?)null;

		public string ContractSupportAttributeId { get; set; }
		public long? ContractSupportAttributeIdValue => long.TryParse(ContractSupportAttributeId, out var val) ? val : (long?)null;

		public Document DocHiredHouse { get; set; }
	}
}
