using System.Collections.Generic;
using MarketPlaceBusiness.Dto.Corrections;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Corrections
{
	public interface IMarketObjectsForCorrectionByFirstFloor 
	{
		List<OMCoreObject> GetFirstFloorsForCorrectionByFirstFloor(List<MarketSegment> segments, MarketSegment? segment = null);
		
		List<FloorStatsForCorrectionByFirstFloor> GetFloorStatsForCorrectionByFirstFloor(bool includeCorrectionByRooms, bool firstFloor = false);
	}
}