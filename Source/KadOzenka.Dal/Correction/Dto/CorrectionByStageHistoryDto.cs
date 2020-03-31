using ObjectModel.Directory;
using System;

namespace KadOzenka.Dal.Correction.Dto
{
    public class CorrectionByStageHistoryDto
	{
		public long Id { get; set; }
		public string BuildingCadastralNumber { get; set; }						
		public DateTime Date { get; set; }
        public decimal StageCoefficient { get; set; }
		public MarketSegment MarketSegment { get; set; }
		public bool IsExcludedFromCalculation { get; set; }
	}
}
