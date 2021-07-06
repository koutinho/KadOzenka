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

		public static MarkModel ToModel(OMMarkCatalog entity)
		{
			return new MarkModel
			{
				Id = entity.Id,
				FactorId = entity.FactorId,
				GroupId = entity.GroupId,
				Value = entity.ValueFactor,
				Metka = entity.MetkaFactor
			};
		}

		public static MarkModel ToModel(OMModelingDictionariesValues entity)
		{
			return new MarkModel
			{
				Id = entity.Id,
				Value = entity.Value,
				Metka = entity.CalculationValue
			};
		}

		public DictionaryValueDto ToDto(long dictionaryId)
		{
			return new DictionaryValueDto
			{
				Id = Id,
				DictionaryId = dictionaryId,
				CalcValue = Metka,
				Value = Value
			};
		}
	}
}
