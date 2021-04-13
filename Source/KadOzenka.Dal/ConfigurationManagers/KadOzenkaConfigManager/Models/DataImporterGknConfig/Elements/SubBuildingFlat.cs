namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class SubBuildingFlat
	{
		public string AreaAttributeId { get; set; }
		public long? AreaAttributeIdValue => long.TryParse(AreaAttributeId, out var val) ? val : (long?)null;

		public Encumbrance[] Encumbrances { get; set; }

		public string NumberRecordAttributeId { get; set; }
		public long? NumberRecordAttributeIdValue => long.TryParse(NumberRecordAttributeId, out var val) ? val : (long?)null;

		public string DateCreatedAttributeId { get; set; }
		public long? DateCreatedAttributeIdValue => long.TryParse(DateCreatedAttributeId, out var val) ? val : (long?)null;
	}
}