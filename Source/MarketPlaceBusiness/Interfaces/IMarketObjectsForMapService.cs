using Core.Register.QuerySubsystem;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectsForMapService : IAMarketObjectBaseService
	{
		QSQuery<OMCoreObject> GetBaseQuery();
	}
}