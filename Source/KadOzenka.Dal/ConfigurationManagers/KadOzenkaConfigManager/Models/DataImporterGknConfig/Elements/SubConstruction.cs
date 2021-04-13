namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class SubConstruction
	{
		public KeyParameter KeyParameter { get; set; }

		public Encumbrance[] Encumbrances { get; set; }

		public string NumberRecordAttributeId { get; set; }
		public long? NumberRecordAttributeIdValue => long.TryParse(NumberRecordAttributeId, out var val) ? val : (long?)null;

		public string DateCreatedAttributeId { get; set; }
		public long? DateCreatedAttributeIdValue => long.TryParse(DateCreatedAttributeId, out var val) ? val : (long?)null;
	}
}
