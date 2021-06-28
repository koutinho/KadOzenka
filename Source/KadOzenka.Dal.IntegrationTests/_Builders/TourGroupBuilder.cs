using KadOzenka.Common.Tests.Builders;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders
{
	public class TourGroupBuilder : ATourGroupBuilder
	{
		public override OMTourGroup Build()
		{
			_tourGroup.Save();
			return _tourGroup;
		}
	}
}
