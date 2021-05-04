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
		List<OMCoreObject> GetBasementObjectsForCorrectionByStage(List<MarketSegment> segments);
		List<OMCoreObject> GetFirstFloorsForCorrectionByFirstFloor(List<MarketSegment> segments, MarketSegment? segment = null);
		List<FloorStatsForCorrectionByFirstFloor> GetFloorStatsForCorrectionByFirstFloor(bool includeCorrectionByRooms, bool firstFloor = false);
	}
}