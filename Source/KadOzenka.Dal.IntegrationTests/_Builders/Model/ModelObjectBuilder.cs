using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Integration._Builders.Model
{
	public class ModelObjectBuilder : AModelObjectBuilder
	{
		public override OMModelToMarketObjects Build()
		{
			_modelObject.Save();
			return _modelObject;
		}
	}
}