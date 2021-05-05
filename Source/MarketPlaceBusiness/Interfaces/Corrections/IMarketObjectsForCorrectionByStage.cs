using System.Collections.Generic;
using MarketPlaceBusiness.Dto.Corrections;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Corrections
{
	public interface IMarketObjectsForCorrectionByStage
	{
		List<GeneralInfoForCorrectionByStage> GetObjectsForCorrectionByStage(bool isForStage, List<MarketSegment> segments);
		List<OMCoreObject> GetBasementObjectsForCorrectionByStage(List<MarketSegment> segments);
	}
}
