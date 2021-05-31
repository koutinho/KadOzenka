using KadOzenka.Common.Tests.Builders.GbuObject;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.Integration._Builders.GbuObject
{
	public class GbuObjectBuilder : AGbuObjectBuilder
	{
		public override OMMainObject Build()
		{
			_gbuObject.Save();
			return _gbuObject;
		}
	}
}
