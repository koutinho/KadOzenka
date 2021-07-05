using KadOzenka.Common.Tests;
using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling.Factors
{
	public class ManualFactorDtoBuilder
	{
		private readonly ManualModelFactorDto _factor;

		public ManualFactorDtoBuilder()
		{
			_factor = new ManualModelFactorDto
			{
				Id = RandomGenerator.GenerateRandomId(),
				GeneralModelId = RandomGenerator.GenerateRandomId(),
				Type = KoAlgoritmType.Line,
				FactorId = RandomGenerator.GenerateRandomId(),
				MarkerId = RandomGenerator.GenerateRandomInteger(),
				DictionaryId = RandomGenerator.GenerateRandomId(),
				Weight = RandomGenerator.GenerateRandomDecimal(),
				B0 = RandomGenerator.GenerateRandomDecimal(),
				SignDiv = true,
				SignAdd = true,
				MarkType = MarkType.Default,
				CorrectItem = RandomGenerator.GenerateRandomDecimal(),
				K = RandomGenerator.GenerateRandomDecimal()
			};
		}


		public ManualFactorDtoBuilder Type(MarkType type)
		{
			_factor.MarkType = type;
			return this;
		}

		public ManualFactorDtoBuilder CorrectItem(decimal? correctItem)
		{
			_factor.CorrectItem = correctItem;
			return this;
		}

		public ManualFactorDtoBuilder K(decimal? k)
		{
			_factor.K = k;
			return this;
		}

		public ManualFactorDtoBuilder Dictionary(long? dictionaryId)
		{
			_factor.DictionaryId = dictionaryId;
			return this;
		}


		public ManualModelFactorDto Build()
		{
			return _factor;
		}
	}
}