using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Correction.Dto;

namespace KadOzenka.Web.Models.MarketObject
{
    public class CorrectionByDateModel
    {
        public long Id { get; set; }
        public bool IsDateReadOnly { get; set; }
        public DateTime? MaxIndexDate { get; set; }

        [Required(ErrorMessage = "Дата не может быть пустой")]
        [Display(Name = "Дата")]
        public DateTime? IndexDate { get; set; }

        [Display(Name = "Изменение потребительских цен к предыдущему месяцу, %")]
        public decimal? ConsumerPriceIndexChange { get; set; }

        public decimal? ConsumerPriceIndex { get; set; }

        [Required(ErrorMessage = "Индекс не может быть пустым")]
        [Display(Name = "Индекс потребительских цен тов. и усл. Росстат")]
        public decimal? ConsumerPriceIndexRosstat { get; set; }


        public static CorrectionByDateModel Map(CorrectionByDateDto correction)
        {
            return new CorrectionByDateModel
            {
                Id = correction.Id,
                IndexDate = correction.Date,
                ConsumerPriceIndex = correction.ConsumerPriceIndex,
                ConsumerPriceIndexRosstat = correction.ConsumerPriceIndexRosstat
            };
        }

        public static CorrectionByDateDto UnMap(CorrectionByDateModel model)
        {
            if(model.IndexDate == null)
                throw new ArgumentException("Дата не может быть пустой");

            return new CorrectionByDateDto
            {
                Id = model.Id,
                Date = model.IndexDate.Value,
                ConsumerPriceIndexRosstat = model.ConsumerPriceIndexRosstat
            };
        }
    }
}
