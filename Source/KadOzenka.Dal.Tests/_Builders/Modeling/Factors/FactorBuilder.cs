using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling.Factors
{
	public class FactorBuilder : AFactorBuilder
	{
		public override OMModelFactor Build()
		{
			return _factor;
		}
	}
}
