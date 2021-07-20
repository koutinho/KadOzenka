using Core.Register;
using ObjectModel.Directory;

namespace ModelingBusiness.Factors.Entities
{
	public class ModelFactorRelationPure
	{
		/// <summary>
		/// ID строки из ko_model_factor
		/// </summary>
		public long Id { get; set; }
		public long RegisterId { get; set; }
		public long AttributeId { get; set; }
		public string AttributeName { get; set; }
		public long AttributeType { get; set; }
		public RegisterAttributeType AttributeTypeCode => (RegisterAttributeType)AttributeType;
		public long? DictionaryId { get; set; }
		public string DictionaryName { get; set; }
		public bool IsNormalized => DictionaryId != null;
		public bool IsActive { get; set; }
	}

	public class ModelFactorRelationDto : ModelFactorRelationPure
	{
		public KoAlgoritmType Type { get; set; }
		public decimal? Correction { get; set; }
		public decimal Coefficient { get; set; }
		public bool SignMarket { get; set; }
        public string MarkType { get; set; }
        public decimal? CorrectingTerm { get; set; }
        public decimal? K { get; set; }
	}
}
