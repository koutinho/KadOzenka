using KadOzenka.Common.Tests;
using ModelingBusiness.Factors.Entities;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling.Factors
{
	public class AutomaticFactorDtoBuilder
	{
		private readonly AutomaticModelFactorDto _factor;

		public AutomaticFactorDtoBuilder()
		{
			_factor = new AutomaticModelFactorDto
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				ModelId = RandomGenerator.GenerateRandomInteger(),
				Type = KoAlgoritmType.Line,
				FactorId = RandomGenerator.GenerateRandomInteger(),
				DictionaryId = RandomGenerator.GenerateRandomInteger(),
				IsActive = true,
				MarkType = MarkType.Default
			};
		}

		public AutomaticFactorDtoBuilder Type(MarkType type)
		{
			_factor.MarkType = type;
			return this;
		}


		public AutomaticModelFactorDto Build()
		{
			return _factor;
		}
	}
}