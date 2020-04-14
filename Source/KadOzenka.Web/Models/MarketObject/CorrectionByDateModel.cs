using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Correction.Dto;

namespace KadOzenka.Web.Models.MarketObject
{
    public class CorrectionByDateModel
    {
        [Display(Name = "Дата")]
        public DateTime? IndexDate { get; set; }

        [Display(Name = "Средняя цена за кв.м.")]
        public decimal? AveragePricePerMeter { get; set; }

        [Display(Name = "Индекс цен")]
        public decimal? ConsumerPriceIndex { get; set; }


        public static CorrectionByDateModel Map(CorrectionByDateDto index)
        {
            return new CorrectionByDateModel
            {
                IndexDate = index.Date,
                AveragePricePerMeter = index.AvaragePricePerMeter,
                ConsumerPriceIndex = index.ConsumerPriceIndex
            };
        }
    }
}
