using System;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Correction.Dto
{
    public class CorrectionByBargainDto
    {
        public string CadastralNumber { get; set; }
        public string Address { get; set; }
        public MarketSegment MarketSegment { get; set; }
        public decimal? Price { get; set; }
        public decimal? BargainCoefficient { get; set; }
        public decimal? PriceAfterCorrectionByBargain { get; set; }
        public DateTime? Date { get; set; }
    }
}
