using System;

namespace KadOzenka.Dal.Correction.Dto
{
    public class CorrectionByDateDto
    {
        public DateTime Date { get; set; }
        public decimal? AvaragePricePerMeter { get; set; }
        public decimal? ConsumerPriceIndex { get; set; }
    }
}
