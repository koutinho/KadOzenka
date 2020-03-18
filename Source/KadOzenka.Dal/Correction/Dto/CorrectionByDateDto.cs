using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.Correction.Dto
{
    public class CorrectionByDateDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        //public decimal? ConsumerPriceChange { get; set; }
        //public decimal? ConsumerPriceIndex { get; set; }
        public decimal? ConsumerPriceIndexRosstat { get; set; }
    }
}
