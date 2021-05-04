using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Dto.Corrections;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectsForCorrectionsService
	{
		List<OMCoreObject> GetObjectsForCorrectionByDate();
		List<OMCoreObject> GetObjectsForCorrectionByRoom();
		QSQuery<OMCoreObject> GetBaseQueryForCorrectionByBargain();
		List<OMCoreObject> GetObjectsForCorrectionByBargain(QSQuery<OMCoreObject> marketObjectsQuery);
		List<GeneralInfoForCorrectionByStage> GetObjectsForCorrectionByStage(bool isForStage, List<MarketSegment> segments);
	}
}