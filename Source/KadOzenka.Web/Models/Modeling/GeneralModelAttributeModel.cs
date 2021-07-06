using System.Collections.Generic;
using Core.Register;
using KadOzenka.Dal.Modeling.Dto;

namespace KadOzenka.Web.Models.Modeling
{
	public class GeneralModelAttributeModel
	{
		public bool IsReadOnly { get; set; }
		public List<ModelAttributeModel> GeneralModelAttributes { get; set; }
	}

	public class ModelAttributeModel
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


		public static ModelAttributeModel ToModel(ModelAttributeRelationPure dto)
		{
			return new ModelAttributeModel
			{
				Id = dto.Id,
				RegisterId = dto.RegisterId,
				AttributeId = dto.AttributeId,
				AttributeName = dto.AttributeName,
				AttributeType = dto.AttributeType,
				DictionaryId = dto.DictionaryId,
				DictionaryName = dto.DictionaryName,
				IsActive = dto.IsActive
			};
		}
	}
}
