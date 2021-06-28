using KadOzenka.Common.Tests.Builders;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders
{
	public class GroupBuilder : AGroupBuilder
	{
		public override OMGroup Build()
		{
			_group.Save();
			return _group;
		}
	}
}
