using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Corrections
{
	public interface IMarketObjectsForCorrectionByBargain
	{
		QSQuery<OMCoreObject> GetBaseQueryForCorrectionByBargain();

		List<OMCoreObject> GetObjectsForCorrectionByBargain(QSQuery<OMCoreObject> marketObjectsQuery);
	}
}