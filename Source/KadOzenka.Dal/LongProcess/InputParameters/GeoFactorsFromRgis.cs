using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.InputParameters
{
	public class GeoFactorsFromRgis
	{
		public long TaskId { get; set; }
		public List<long> IdFactors { get; set; }
	}
}