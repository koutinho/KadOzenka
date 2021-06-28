using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling
{
	public class MarkBuilder : AMarkBuilder
	{
		public override OMMarkCatalog Build()
		{
			return _mark;
		}
	}
}