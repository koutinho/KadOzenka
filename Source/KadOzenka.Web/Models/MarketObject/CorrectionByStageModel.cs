using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Correction.Dto;

namespace KadOzenka.Web.Models.MarketObject
{
    public class CorrectionByStageModel
    {
        public long Id { get; set; }        

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Кадастровый номер здания")]
        public string BuildingCadastralNumber { get; set; }

        [Display(Name = "Коэффициент на этажность")]
        public decimal StageCoefficient { get; set; }        

        [Display(Name = "Исключить из расчета")]
        public bool IsExcludedFromCalculation { get; set; }

        public static CorrectionByStageHistoryDto UnMap(CorrectionByStageModel model)
        {
            return new CorrectionByStageHistoryDto
            {
                Id = model.Id,
                IsExcludedFromCalculation = model.IsExcludedFromCalculation
            };
        }
    }
}
