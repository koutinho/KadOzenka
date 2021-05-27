using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Modeling.Builders
{
	public class ModelBuilder : AModelBuilder
	{
		public override OMModel Build()
		{
			_model.Save();
			return _model;
		}
	}
}