using System.Collections.Generic;
using MarketPlaceBusiness.Dto.Corrections;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Corrections
{
	public interface IMarketObjectsForCorrectionByStage
	{
		List<GeneralInfoForCorrectionByStage> GetObjects(bool isForStage, List<MarketSegment> segments);
		
		List<OMCoreObject> GetBasementObjects(List<MarketSegment> segments);
	}
}
