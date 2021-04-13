namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class Document
	{
		public string CodeAttributeId { get; set; }
		public long? CodeAttributeIdValue => long.TryParse(CodeAttributeId, out var val) ? val : (long?)null;

		public string NameAttributeId { get; set; }
		public long? NameAttributeIdValue => long.TryParse(NameAttributeId, out var val) ? val : (long?)null;

		public string SeriesAttributeId { get; set; }
		public long? SeriesAttributeIdValue => long.TryParse(SeriesAttributeId, out var val) ? val : (long?)null;

		public string NumberAttributeId { get; set; }
		public long? NumberAttributeIdValue => long.TryParse(NumberAttributeId, out var val) ? val : (long?)null;

		public string DateAttributeId { get; set; }
		public long? DateAttributeIdValue => long.TryParse(DateAttributeId, out var val) ? val : (long?)null;

		public string IssueOrganAttributeId { get; set; }
		public long? IssueOrganAttributeIdValue => long.TryParse(IssueOrganAttributeId, out var val) ? val : (long?)null;

		public string DescAttributeId { get; set; }
		public long? DescAttributeIdValue => long.TryParse(DescAttributeId, out var val) ? val : (long?)null;
	}
}
