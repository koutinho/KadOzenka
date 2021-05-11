using System.Collections.Generic;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectsForOutliersCheckingService
	{
		Dictionary<MarketSegment, List<OMCoreObject>> GetObjectsBySegments(List<PropertyTypesCIPJS> types,
			MarketSegment? segment);
	}
}