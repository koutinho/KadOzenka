using System.Collections.Generic;
using KadOzenka.Dal.Enum;
using ObjectModel.Directory;
using ObjectModel.Directory.ES;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class CalculateSquareCostDto
	{
		public List<AnalogDto> Analogs { get; set; }
		public int TargetObjectId { get; set; }
		public int TargetObjectFloor { get; set; }
		public MarketSegment MarketSegment { get; set; }
		public DealTypeShort DealTypeShort { get; set; }
		public long? TargetMarketObjectId { get; set; }
		public ScenarioType? ScenarioType { get; set; }

		public string Kn { get; set; }
	}
}