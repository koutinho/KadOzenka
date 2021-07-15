using KadOzenka.Dal.Modeling.Dictionaries.Entities;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Modeling.Dictionaries
{
	public class MarkDtoBuilder
	{
		private DictionaryMarkDto _markDto;

		public MarkDtoBuilder()
		{
			_markDto = new DictionaryMarkDto
			{
				Id = RandomGenerator.GenerateRandomId(),
				DictionaryId = RandomGenerator.GenerateRandomId(),
				Value = RandomGenerator.GetRandomString(),
				CalculationValue = RandomGenerator.GenerateRandomDecimal()
			};
		}


		public MarkDtoBuilder Dictionary(long dictionaryId)
		{
			_markDto.DictionaryId = dictionaryId;
			return this;
		}

		public MarkDtoBuilder Dictionary(OMModelingDictionary dictionary)
		{
			_markDto.DictionaryId = dictionary.Id;
			return this;
		}

		public MarkDtoBuilder Value(string value)
		{
			_markDto.Value = value;
			return this;
		}

		public MarkDtoBuilder CalculationValue(decimal? value)
		{
			_markDto.CalculationValue = value;
			return this;
		}

		public DictionaryMarkDto Build()
		{
			return _markDto;
		}
	}
}
