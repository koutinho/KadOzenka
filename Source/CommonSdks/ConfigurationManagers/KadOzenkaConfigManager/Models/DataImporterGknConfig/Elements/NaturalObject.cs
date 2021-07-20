namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class NaturalObject
	{
		public string KindAttributeId { get; set; }
		public long? KindAttributeIdValue => long.TryParse(KindAttributeId, out var val) ? val : (long?)null;

		public string ForestryAttributeId { get; set; }
		public long? ForestryAttributeIdValue => long.TryParse(ForestryAttributeId, out var val) ? val : (long?)null;

		public string ForestUseAttributeId { get; set; }
		public long? ForestUseAttributeIdValue => long.TryParse(ForestUseAttributeId, out var val) ? val : (long?)null;

		public string QuarterNumbersAttributeId { get; set; }
		public long? QuarterNumbersAttributeIdValue => long.TryParse(QuarterNumbersAttributeId, out var val) ? val : (long?)null;

		public string TaxationSeparationsAttributeId { get; set; }
		public long? TaxationSeparationsAttributeIdValue => long.TryParse(TaxationSeparationsAttributeId, out var val) ? val : (long?)null;

		public string ProtectiveForestAttributeId { get; set; }
		public long? ProtectiveForestAttributeIdValue => long.TryParse(ProtectiveForestAttributeId, out var val) ? val : (long?)null;

		public ForestEncumbrance[] ForestEncumbrances { get; set; }

		public string WaterObjectAttributeId { get; set; }
		public long? WaterObjectAttributeIdValue => long.TryParse(WaterObjectAttributeId, out var val) ? val : (long?)null;

		public string NameOtherAttributeId { get; set; }
		public long? NameOtherAttributeIdValue => long.TryParse(NameOtherAttributeId, out var val) ? val : (long?)null;

		public string CharOtherAttributeId { get; set; }
		public long? CharOtherAttributeIdValue => long.TryParse(CharOtherAttributeId, out var val) ? val : (long?)null;
	}

	public class ForestEncumbrance
	{
		public string ForestEncumbranceAttributeId { get; set; }
		public long? ForestEncumbranceAttributeIdValue => long.TryParse(ForestEncumbranceAttributeId, out var val) ? val : (long?)null;
	}
}
