using ObjectModel.Directory;
using ObjectModel.Directory.ES;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class SaveExpressScoreDto
	{
		public int TargetObjectId { get; set; }

		public long? TargetMarketObjectId { get; set; }

		public decimal SummaryCost { get; set; }

		public decimal CostSquareMeter { get; set; }

		public int? ExpressScoreId { get; set; }

		public ScenarioType? ScenarioType { get; set; }

		public MarketSegment? SegmentType { get; set; }

		public DealType? DealType { get; set; }

		public string Address { get; set; }
	}
}