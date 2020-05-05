namespace KadOzenka.Dal.Modeling.Dto
{
	public class AttributeDto
	{
		public long? AttributeId { get; set; }
		public string AttributeName { get; set; }
		public long? DictionaryId { get; set; }
		public string DictionaryName { get; set; }
		public bool IsNormalized => DictionaryId != null;
	}
}
