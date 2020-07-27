using System.Collections.Generic;
using KadOzenka.Dal.Enum;
using ObjectModel.Directory;
using ObjectModel.Directory.ES;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class InputCalculateDto
	{
		public List<AnalogDto> Analogs { get; set; }

		public int TargetObjectId { get; set; }

		public int Floor { get; set; }

		public decimal Square { get; set; }

		public ScenarioType ScenarioType { get; set; }

		public MarketSegment Segment { get; set; }

		public string Address { get; set; }

		public string Kn { get; set; }

		public DealTypeShort DealType { get; set; }
	}
}