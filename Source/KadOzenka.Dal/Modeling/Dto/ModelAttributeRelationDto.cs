﻿using Core.Register;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class ModelAttributeRelationDto
	{
		/// <summary>
		/// ID строки из ko_model_factor
		/// </summary>
		public long Id { get; set; }
		public long RegisterId { get; set; }
		public long AttributeId { get; set; }
		public string AttributeName { get; set; }
		public long AttributeType { get; set; }
		public RegisterAttributeType AttributeTypeCode => (RegisterAttributeType) AttributeType;
		public long? DictionaryId { get; set; }
		public string DictionaryName { get; set; }
		public bool IsNormalized => DictionaryId != null;

		
		public KoAlgoritmType Type { get; set; }
		public decimal? Coefficient { get; set; }
		public decimal? PreviousWeight { get; set; }
        public decimal B0 { get; set; }
        public bool SignDiv { get; set; }
        public bool SignAdd { get; set; }
        public bool SignMarket { get; set; }
    }
}
