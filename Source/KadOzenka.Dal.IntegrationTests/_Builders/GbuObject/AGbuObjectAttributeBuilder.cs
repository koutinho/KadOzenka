using KadOzenka.Common.Tests.Builders.GbuObject;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Dal.Integration._Builders.GbuObject
{
	public class GbuObjectAttributeBuilder : AGbuObjectAttributeBuilder
	{
		public override GbuObjectAttribute Build()
		{
			_gbuObjectAttribute.Save();
			return _gbuObjectAttribute;
		}
	}
}
