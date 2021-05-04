using System.Collections.Generic;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectsForCorrectionsService : IMarketObjectsForCorrectionsService
	{
		public List<OMCoreObject> GetObjectsForCorrectionByDate()
		{
			return OMCoreObject
				.Where(x => x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal &&
					(x.ParserTime != null || x.LastDateUpdate != null) &&
					x.PropertyMarketSegment != null)
				.SelectAll().Execute();
		}

		public List<OMCoreObject> GetObjectsForCorrectionByRoom()
		{ 
			return OMCoreObject.Where(x => x.RoomsCount == 1 || x.RoomsCount == 3).SelectAll().Execute();
		}
    }
}
