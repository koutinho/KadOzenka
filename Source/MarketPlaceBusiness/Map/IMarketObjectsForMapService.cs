using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Common.Dto;
using MarketPlaceBusiness.Common.Interfaces;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Map
{
	public interface IMarketObjectsForMapService : IAMarketObjectBaseService
	{
		QSQuery<OMCoreObject> GetBaseQuery();

		void UpdateInfoFromCard(MarketObjectDto dto);
	}
}