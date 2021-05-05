using System.Collections.Generic;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.ForBlFrontendApp
{
	public interface IMarketObjectForCadastralInfoFiller
	{
		List<OMCoreObject> GetObjectsWithCadastralNumber();

		List<OMCoreObject> GetObjectsWithCadastralQuartal();
	}
}