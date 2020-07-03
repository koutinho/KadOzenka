namespace KadOzenka.Dal.Modeling.Dto
{
	public class ModelAttributeRelationDto
	{
        public long Id { get; set; }
		public long RegisterId { get; set; }
		public long AttributeId { get; set; }
		public string AttributeName { get; set; }
		public int AttributeType { get; set; }
		public long? DictionaryId { get; set; }
		public string DictionaryName { get; set; }
		public bool IsNormalized => DictionaryId != null;
        public decimal? Coefficient { get; set; }
    }
}
