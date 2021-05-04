using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public interface IMarketObjectsForCorrectionsService
	{
		List<OMCoreObject> GetObjectsForCorrectionByDate();
		List<OMCoreObject> GetObjectsForCorrectionByRoom();
		QSQuery<OMCoreObject> GetBaseQueryForCorrectionByBargain();
		List<OMCoreObject> GetMarketObjectsForCorrectionByBargain(QSQuery<OMCoreObject> marketObjectsQuery);
	}
}