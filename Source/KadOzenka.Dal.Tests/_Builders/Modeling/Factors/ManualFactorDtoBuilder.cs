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
				Id = RandomGenerator.GenerateRandomInteger(),
				GeneralModelId = RandomGenerator.GenerateRandomInteger(),
				Type = KoAlgoritmType.Line,
				FactorId = RandomGenerator.GenerateRandomInteger(),
				MarkerId = RandomGenerator.GenerateRandomInteger(),
				Weight = RandomGenerator.GenerateRandomDecimal(),
				B0 = RandomGenerator.GenerateRandomDecimal(),
				SignDiv = true,
				SignAdd = true,
				SignMarket = true,
				MarkType = MarkType.Default
			};
		}

		public ManualFactorDtoBuilder Type(MarkType type)
		{
			_factor.MarkType = type;
			return this;
		}


		public ManualModelFactorDto Build()
		{
			return _factor;
		}
	}
}