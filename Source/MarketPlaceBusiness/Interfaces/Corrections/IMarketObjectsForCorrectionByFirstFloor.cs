using System.Collections.Generic;
using MarketPlaceBusiness.Dto.Corrections;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Corrections
{
	public interface IMarketObjectsForCorrectionByFirstFloor 
	{
		List<OMCoreObject> GetFirstFloors(List<MarketSegment> segments, MarketSegment? segment = null);
		
		List<FloorStatsForCorrectionByFirstFloor> GetFloorStats(bool includeCorrectionByRooms, bool firstFloor = false);
	}
}