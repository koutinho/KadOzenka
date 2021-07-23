using ModelingBusiness.Factors.Entities;

namespace KadOzenka.Web.Models.Modeling
{
	public class DictionaryModel
	{
		/// <summary>
		/// ID строки из ko_model_factor
		/// </summary>
		public long Id { get; set; }
		public string AttributeName { get; set; }
		public long AttributeType { get; set; }
		public long? DictionaryId { get; set; }
		public string DictionaryName { get; set; }
		public bool IsNormalized => DictionaryId != null;


		public static DictionaryModel ToModel(ModelAttributeRelationPure dto)
		{
			return new DictionaryModel
			{
				Id = dto.Id,
				AttributeName = dto.AttributeName,
				AttributeType = dto.AttributeType,
				DictionaryId = dto.DictionaryId,
				DictionaryName = dto.DictionaryName,
			};
		}
	}
}
