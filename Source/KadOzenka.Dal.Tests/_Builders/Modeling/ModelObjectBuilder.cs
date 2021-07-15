using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling
{
	public class ModelObjectBuilder : AModelObjectBuilder
	{
		public override OMModelToMarketObjects Build()
		{
			_modelObject.Id = RandomGenerator.GenerateRandomId();
			return _modelObject;
		}
	}
}
