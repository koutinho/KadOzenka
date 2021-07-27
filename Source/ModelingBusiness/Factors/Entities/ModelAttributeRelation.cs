using System;
using Core.Register;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;

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
		public MarkType MarkTypeCode { get; set; }
		public string MarkTypeStr => MarkTypeCode.GetEnumDescription();
		public long? DictionaryId { get; set; }
		public string DictionaryName { get; set; }
		public bool IsNormalized => DictionaryId != null;
		public bool IsActive { get; set; }
	}

	public class ModelFactorRelation : ModelFactorRelationPure
	{
		public KoAlgoritmType Type { get; set; }
		public decimal? Correction { get; set; }
		public decimal? Coefficient { get; set; }
		public decimal? CoefficientForLinear { get; set; }
		public decimal? CoefficientForExponential { get; set; }
		public decimal? CoefficientForMultiplicative { get; set; }
		public bool SignMarket { get; set; }
        public decimal? CorrectingTerm { get; set; }
        public decimal? K { get; set; }

		public decimal? GetCoefficient(KoAlgoritmType type)
		{
			switch (type)
			{
				case KoAlgoritmType.Exp:
					return CoefficientForExponential;
				case KoAlgoritmType.Line:
					return CoefficientForLinear;
				case KoAlgoritmType.Multi:
					return CoefficientForMultiplicative;
				default:
					throw new Exception($"Передан неизвестный тип алгоритма '{type.GetEnumDescription()}'");
			}
		}
	}
}
