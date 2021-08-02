using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling.Factors
{
	public class ModelFactorBuilder : AFactorBuilder
	{
		public override OMModelFactor Build()
		{
			_factor.Id = RandomGenerator.GenerateRandomId();
			return _factor;
		}
	}
}
