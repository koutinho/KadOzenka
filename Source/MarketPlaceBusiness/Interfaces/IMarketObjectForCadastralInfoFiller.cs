using System.Collections.Generic;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectForCadastralInfoFiller
	{
		List<OMCoreObject> GetObjectsWithCadastralNumber();
		List<OMCoreObject> GetObjectsWithCadastralQuartal();
	}
}