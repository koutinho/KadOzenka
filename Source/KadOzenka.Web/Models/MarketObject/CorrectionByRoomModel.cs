using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Correction.Dto;

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
        public decimal OneRoomCoefficient { get; set; }

        [Display(Name = "Коэффициент на 3-х комнатную квартиру")]
        public decimal ThreeRoomsCoefficient { get; set; }

        [Display(Name = "Исключить из расчета")]
        public bool IsExcludeFromCalculation { get; set; }


        public static CorrectionByRoomModel Map(CorrectionByRoomCoefficientsDto coefficients)
        {
            return new CorrectionByRoomModel
            {
                Id = coefficients.Id,
                BuildingCadastralNumber = coefficients.BuildingCadastralNumber,
                Date = coefficients.Date,
                OneRoomCoefficient = coefficients.OneRoomCoefficient,
                ThreeRoomsCoefficient = coefficients.ThreeRoomsCoefficient,
                IsExcludeFromCalculation = coefficients.IsExcludeFromCalculation
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
