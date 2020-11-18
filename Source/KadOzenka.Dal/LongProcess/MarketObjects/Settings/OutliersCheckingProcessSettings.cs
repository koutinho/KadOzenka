﻿using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.LongProcess.MarketObjects.Settings
{
	public class OutliersCheckingProcessSettings
	{
		public MarketSegment? Segment { get; set; }
		public List<PropertyTypesCIPJS> PropertyTypes { get; set; }
	}
}
