using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling
{
	public class ModelBuilder : AModelBuilder
	{
		public override OMModel Build()
		{
			return _model;
		}
	}
}