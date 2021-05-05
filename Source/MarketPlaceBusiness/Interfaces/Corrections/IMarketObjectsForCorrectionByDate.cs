using System;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Corrections
{
	public interface IMarketObjectsForCorrectionByDate
	{
		List<OMCoreObject> GetObjectsForCorrectionByDate();

		List<IGrouping<MarketSegment, OMCoreObject>> GetObjectsGroupedBySegmentForCorrectionByDate(DateTime startDate, DateTime endDate);
	}
}