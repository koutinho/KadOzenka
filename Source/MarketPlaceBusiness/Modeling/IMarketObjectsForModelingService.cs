using System.Collections.Generic;
using MarketPlaceBusiness.Modeling.Entities;
using ObjectModel.Core.Register;

namespace MarketPlaceBusiness.Modeling
{
	public interface IMarketObjectsForModelingService
	{
		List<MarketObjectPure> GetObjectsForFormation(bool isOks, long segment);

		CorrelationDto GetObjectsForCorrelation(List<long> objectIds, List<OMAttribute> attributes);
	}
}