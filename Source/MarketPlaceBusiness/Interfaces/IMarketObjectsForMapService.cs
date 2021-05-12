using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Dto;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectsForMapService : IAMarketObjectBaseService
	{
		QSQuery<OMCoreObject> GetBaseQuery();

		void UpdateInfoFromCard(MarketObjectDto dto);
	}
}