using System.Collections.Generic;
using MarketPlaceBusiness.Dto;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectService : IAMarketObjectBaseService
	{
		//List<OMCoreObject> GetObjectsForDuplicatesChecking();
		//List<List<OMCoreObject>> SplitListByPersentForDuplicates(List<OMCoreObject> list, double areaDelta, double priceDelta,
		//	int selectedMarket);

		//List<OMCoreObject> GetObjectsToAssignDistrictsRegionsAndZones();

		//List<OMCoreObject> GetObjectsToAssignCoordinates();
	}
}