using System.Collections.Generic;
using MarketPlaceBusiness.Dto.Modeling;
using ObjectModel.Core.Register;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectsForModelingService
	{
		List<MarketObjectPureOutSide> GetObjectsForFormation(bool isOks, long segment);
		CorrelationDto GetObjectsForCorrelation(List<long> objectIds, List<OMAttribute> attributes);
	}
}