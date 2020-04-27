using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Correction.Dto;

namespace KadOzenka.Web.Models.MarketObject
{
    public class CorrectionForFirstFloorModel
    {
        public long Id { get; set; }
        public bool IsDirty { get; set; }

        [Display(Name = "Дата")]
        public DateTime StatsDate { get; set; }

        [Display(Name = "Кадастровый номер здания")]
        public string BuildingCadastralNumber { get; set; }

        [Display(Name = "Коэффициент для первого этажа")]
        public decimal FirstFloorCoefficient { get; set; }

        [Display(Name = "Исключено из расчета")]
        public bool IsExcludedFromCalculation { get; set; }
        public bool IsCoefIncludedInCalculationLimit { get; set; }


        public static CorrectionForFirstFloorModel Map(CorrectionForFirstFloorDto coefficients, Func<decimal?, bool> isCoefIncludedInCalculationLimit)
        {
            return new CorrectionForFirstFloorModel
            {
                Id = coefficients.Id,
                BuildingCadastralNumber = coefficients.BuildingCadastralNumber,
                StatsDate = coefficients.StatsDate,
                FirstFloorCoefficient = coefficients.FirstFloorCoefficient,
                IsExcludedFromCalculation = coefficients.IsExcludedFromCalculation,
                IsCoefIncludedInCalculationLimit = isCoefIncludedInCalculationLimit(coefficients.FirstFloorCoefficient)
            };
        }

        public static CorrectionForFirstFloorDto UnMap(CorrectionForFirstFloorModel model)
        {
            return new CorrectionForFirstFloorDto
            {
                Id = model.Id,
                IsExcludedFromCalculation = model.IsExcludedFromCalculation
            };
        }
    }
}
