using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling
{
	public class ModelBuilder : AModelBuilder
	{
		public override OMModel Build()
		{
			_model.Id = RandomGenerator.GenerateRandomInteger(maxNumber: int.MaxValue);
			return _model;
		}
	}
}