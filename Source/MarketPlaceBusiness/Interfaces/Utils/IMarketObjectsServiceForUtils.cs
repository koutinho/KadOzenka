using System.Collections.Generic;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Utils
{
	/// <summary>
	/// Интерфейс для методов, которые используются только в BlFrontend
	/// и когда нет смысла выделять один метод в интерфейс
	/// </summary>
	public interface IMarketObjectsServiceForUtils : IMissingDataFromGbuService, IMarketObjectForCadastralInfoFiller
	{
		List<OMCoreObject> GetNotRosreestrObjectsWithAddressProceed();

		List<OMCoreObject> GetObjectsToSetAddress(int objectsCount);
	}
}