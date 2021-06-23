using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders.Model
{
	public class ModelFactorBuilder : AFactorBuilder
	{
		public override OMModelFactor Build()
		{
			_factor.Save();
			return _factor;
		}
	}
}