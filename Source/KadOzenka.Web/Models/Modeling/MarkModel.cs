using KadOzenka.Dal.Modeling.Dictionaries.Entities;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class MarkModel
	{
		public long Id { get; set; }
		public long? GroupId { get; set; }
		public long? FactorId { get; set; }
		public string Value { get; set; }
		public decimal? Metka { get; set; }


		public static MarkModel ToModel(OMModelingDictionariesValues entity)
		{
			return new MarkModel
			{
				Id = entity.Id,
				Value = entity.Value,
				Metka = entity.CalculationValue
			};
		}

		public DictionaryMarkDto ToDto(long dictionaryId)
		{
			return new DictionaryMarkDto
			{
				Id = Id,
				DictionaryId = dictionaryId,
				CalculationValue = Metka,
				Value = Value
			};
		}
	}
}
