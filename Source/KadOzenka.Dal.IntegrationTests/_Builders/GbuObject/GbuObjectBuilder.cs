using KadOzenka.Common.Tests.Builders.GbuObject;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.Integration._Builders.GbuObject
{
	public class GbuObjectBuilder : AGbuObjectBuilder
	{
		public GbuObjectBuilder()
		{
		}

		private GbuObjectBuilder(OMMainObject gbuObject) : base(gbuObject)
		{
		}


		public override AGbuObjectBuilder ShallowCopy()
		{
			return new GbuObjectBuilder(_gbuObject);
		}

		public override OMMainObject Build()
		{
			_gbuObject.Save();
			return _gbuObject;
		}
	}
}
