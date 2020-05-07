namespace KadOzenka.Dal.Modeling.Dto
{
	public class AttributeDto
	{
		public long RegisterId { get; set; }
		public long AttributeId { get; set; }
		public string AttributeName { get; set; }
		public long? DictionaryId { get; set; }
		public string DictionaryName { get; set; }
		public bool IsNormalized => DictionaryId != null;
		public decimal? Coefficient { get; set; }

		//временное решение (если юзер выбрал строковый аттрибут, но не выбрал для него справочник, то нужно залогировать ошибку).
		//в будущем будем обрабатывать этот кейс раньше
		public string Message { get; set; }
	}
}
