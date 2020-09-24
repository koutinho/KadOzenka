using System.Collections.Generic;
using KadOzenka.Dal.Enum;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class AnalogResultDto : AnalogDto
	{
		public string Address { get; set; }

		public string Source { get; set; }

		public string TypeOfRoom { get; set; }

		public decimal SquarePrice { get; set; }
	}
	public class ResultCalculateDto
	{
		public int Id { get; set; }
		public decimal SummaryCost { get; set; }
		public decimal SquareCost { get; set; }
		public long ReportId { get; set; }
		public List<AnalogResultDto> Analogs { get; set; }
		public DealTypeShort DealType { get; set; }
		public string Address { get; set; }
		public decimal Area { get; set; }
		public MarketSegment MarketSegment { get; set; }
	}
}