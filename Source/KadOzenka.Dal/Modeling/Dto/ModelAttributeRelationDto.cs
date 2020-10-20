using Core.Register;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class ModelAttributeRelationDto
	{
		/// <summary>
		/// ID строки из KO_MODEL_ATTRIBUTES
		/// </summary>
		public long Id { get; set; }
		public long RegisterId { get; set; }
		/// <summary>
		/// ID Атрибута
		/// </summary>
		public long AttributeId { get; set; }
		public string AttributeName { get; set; }
		public int AttributeType { get; set; }
		public RegisterAttributeType AttributeTypeCode => (RegisterAttributeType) AttributeType;
		public long? DictionaryId { get; set; }
		public string DictionaryName { get; set; }
		public bool IsNormalized => DictionaryId != null;

		/// <summary>
		/// ID строки из ko_model_factor (для редактирования фактора из карточки тура)
		/// </summary>
		public long FactorId { get; set; }
		public decimal? Coefficient { get; set; }
        public decimal B0 { get; set; }
        public bool SignDiv { get; set; }
        public bool SignAdd { get; set; }
        public bool SignMarket { get; set; }
    }
}
