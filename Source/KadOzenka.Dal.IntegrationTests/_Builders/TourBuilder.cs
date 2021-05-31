using KadOzenka.Common.Tests.Builders;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders
{
	public class TourBuilder : ATourBuilder
	{
		public override OMTour Build()
		{
			_tour.Save();
			return _tour;
		}
	}
}
