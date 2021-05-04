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
    }
}
