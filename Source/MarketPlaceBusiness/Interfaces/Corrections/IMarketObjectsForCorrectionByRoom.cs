using System.Collections.Generic;
using System.Linq;
using MarketPlaceBusiness.Dto.Corrections;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Corrections
{
	public interface IMarketObjectsForCorrectionByRoom
	{
		List<OMCoreObject> GetObjectsForCorrectionByRoom();

		List<IGrouping<ObjectsGroupedBySegmentForCorrectionByRoom, OMCoreObject>> GetObjectsGroupedBySegmentForCorrectionByRoom(List<MarketSegment> calculatedMarketSegments, long?[] numberOfRooms);
	}
}