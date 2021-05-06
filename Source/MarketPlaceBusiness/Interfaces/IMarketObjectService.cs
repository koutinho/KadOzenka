using System.Collections.Generic;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectService
	{
		List<OMCoreObject> GetObjectsForDuplicatesChecking();
		List<OMCoreObject> GetObjectsToAssignDistrictsRegionsAndZones();
		List<OMCoreObject> GetObjectsToAssignCoordinates();
	}
}