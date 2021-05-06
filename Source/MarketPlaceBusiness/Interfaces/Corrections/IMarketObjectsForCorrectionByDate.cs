﻿using System;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Corrections
{
	public interface IMarketObjectsForCorrectionByDate
	{
		List<OMCoreObject> GetObjects();

		List<IGrouping<MarketSegment, OMCoreObject>> GetObjectsGroupedBySegment(DateTime startDate, DateTime endDate);
	}
}