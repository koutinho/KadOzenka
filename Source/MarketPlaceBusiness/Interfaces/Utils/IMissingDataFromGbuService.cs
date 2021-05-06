using System.Collections.Generic;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Utils
{
	public interface IMissingDataFromGbuService
	{
		List<OMCoreObject> GetInitialObjects();
		List<OMCoreObject> GetExistingObjects();
	}
}