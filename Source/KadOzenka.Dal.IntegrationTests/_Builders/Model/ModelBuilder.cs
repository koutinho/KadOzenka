using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders.Model
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