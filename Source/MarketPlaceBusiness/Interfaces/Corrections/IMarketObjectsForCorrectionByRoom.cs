using System.Collections.Generic;
using System.Linq;
using MarketPlaceBusiness.Dto.Corrections;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Corrections
{
	public interface IMarketObjectsForCorrectionByRoom
	{
		List<OMCoreObject> GetObjects();

		List<IGrouping<ObjectsGroupedBySegmentForCorrectionByRoom, OMCoreObject>> GetObjectsGroupedBySegment(List<MarketSegment> calculatedMarketSegments, long?[] numberOfRooms);
	}
}