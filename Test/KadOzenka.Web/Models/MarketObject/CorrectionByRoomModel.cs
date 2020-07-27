using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Correction.Dto;
using KadOzenka.Dal.Correction.Dto.CorrectionSettings;

namespace KadOzenka.Web.Models.MarketObject
{
    public class CorrectionByRoomModel
    {
        public long Id { get; set; }
        public bool IsDirty { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Кадастровый номер здания")]
        public string BuildingCadastralNumber { get; set; }

        [Display(Name = "Коэффициент на 1-но комнатную квартиру")]
        public decimal? OneRoomCoefficient { get; set; }

        [Display(Name = "Коэффициент на 3-х комнатную квартиру")]
        public decimal? ThreeRoomsCoefficient { get; set; }

        [Display(Name = "Исключить из расчета")]
        public bool IsExcludeFromCalculation { get; set; }
        public bool IsOneRoomCoefIncludedInCalculationLimit { get; set; }
        public bool IsThreeRoomsCoefIncludedInCalculationLimit { get; set; }


        public static CorrectionByRoomModel Map(CorrectionByRoomCoefficientsDto coefficient,
            Func<decimal?, bool> isOneRoomCoefIncludedInCalculationLimit,
            Func<decimal?, bool> isThreeRoomsCoefIncludedInCalculationLimit)
        {
            return new CorrectionByRoomModel
            {
                Id = coefficient.Id,
                BuildingCadastralNumber = coefficient.BuildingCadastralNumber,
                Date = coefficient.Date,
                OneRoomCoefficient = coefficient.OneRoomCoefficient,
                ThreeRoomsCoefficient = coefficient.ThreeRoomsCoefficient,
                IsExcludeFromCalculation = coefficient.IsExcludeFromCalculation,
                IsOneRoomCoefIncludedInCalculationLimit = isOneRoomCoefIncludedInCalculationLimit(coefficient.OneRoomCoefficient),
                IsThreeRoomsCoefIncludedInCalculationLimit = isThreeRoomsCoefIncludedInCalculationLimit(coefficient.ThreeRoomsCoefficient)
            };
        }

        public static CorrectionByRoomCoefficientsDto UnMap(CorrectionByRoomModel model)
        {
            return new CorrectionByRoomCoefficientsDto
            {
                Id = model.Id,
                IsExcludeFromCalculation = model.IsExcludeFromCalculation
            };
        }
    }
}
