using System.Collections.Generic;
using KadOzenka.Dal.OutliersChecking;
using ObjectModel.Directory;

namespace KadOzenka.Dal.LongProcess.MarketObjects.Settings
{
	public class OutliersCheckingProcessSettings
	{
		public MarketSegment? Segment { get; set; }
		public List<ObjectPropertyTypeDivision> PropertyTypes { get; set; }
	}
}
