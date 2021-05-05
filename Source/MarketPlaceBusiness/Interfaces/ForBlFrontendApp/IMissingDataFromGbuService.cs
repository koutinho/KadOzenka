using System.Collections.Generic;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.ForBlFrontendApp
{
	public interface IMissingDataFromGbuService
	{
		List<OMCoreObject> GetInitialObjects();
		List<OMCoreObject> GetExistingObjects();
	}
}