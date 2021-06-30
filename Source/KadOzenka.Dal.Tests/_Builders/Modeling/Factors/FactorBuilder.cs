using System;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling.Factors
{
	public class FactorBuilder : AFactorBuilder
	{
		public override OMModelFactor Build()
		{
			_factor.Id = RandomGenerator.GenerateRandomInteger(maxNumber: int.MaxValue);
			return _factor;
		}
	}
}
