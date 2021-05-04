using System.Collections.Generic;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public interface IMarketObjectsForCorrectionsService
	{
		List<OMCoreObject> GetObjectsForCorrectionByDate();
	}
}