using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Correction.Dto;
using KadOzenka.Dal.Correction.Dto.CorrectionSettings;

namespace KadOzenka.Web.Models.MarketObject
{
    public class CorrectionByDateModel
    {
        public long Id { get; set; }
        public bool IsDirty { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Кадастровый номер здания")]
        public string BuildingCadastralNumber { get; set; }

        [Display(Name = "Коэффициент")]
        public decimal Coefficient { get; set; }

        [Display(Name = "Исключить из расчета")]
        public bool IsExcludeFromCalculation { get; set; }
        public bool IsExclusionEnabled { get; set; }


        public static CorrectionByDateModel Map(CorrectionByDateDto coefficient, CorrectionSettings settings, Func<decimal?, CorrectionSettings, bool> isCoefIncludedInCalculationLimit)
        {
            var model =  new CorrectionByDateModel
            {
                Id = coefficient.Id,
                BuildingCadastralNumber = coefficient.BuildingCadastralNumber,
                Date = coefficient.Date,
                Coefficient = coefficient.Coefficient,
                IsExcludeFromCalculation = coefficient.IsExcludeFromCalculation,
                IsExclusionEnabled = isCoefIncludedInCalculationLimit(coefficient.Coefficient, settings)
            };

            return model;
        }

        public static CorrectionByDateDto UnMap(CorrectionByDateModel model)
        {
            return new CorrectionByDateDto
            {
                Id = model.Id,
                IsExcludeFromCalculation = model.IsExcludeFromCalculation
            };
        }
    }
}
