using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders.Model
{
	public class MarkBuilder : AMarkBuilder
	{
		public override OMMarkCatalog Build()
		{
			_mark.Save();
			return _mark;
		}
	}
}