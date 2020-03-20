using System;

namespace KadOzenka.Dal.Correction.Dto
{
    public class CorrectionByDateDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal? ConsumerPriceChange { get; }
        public decimal? ConsumerPriceIndex { get; }
        public decimal? ConsumerPriceIndexRosstat { get; set; }

        public CorrectionByDateDto()
        {
            
        }

        public CorrectionByDateDto(decimal? consumerPriceIndex, decimal? consumerPriceChange)
        {
            ConsumerPriceIndex = consumerPriceIndex;
            ConsumerPriceChange = consumerPriceChange;
        }
    }
}
