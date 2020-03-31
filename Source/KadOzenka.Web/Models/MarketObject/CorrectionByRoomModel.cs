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


        public static CorrectionByRoomModel Map(CorrectionByRoomHistoryDto history)
        {
            return new CorrectionByRoomModel
            {
                Id = history.Id,
                BuildingCadastralNumber = history.BuildingCadastralNumber,
                Date = history.Date,
                OneRoomCoefficient = history.OneRoomCoefficient,
                ThreeRoomsCoefficient = history.ThreeRoomsCoefficient,
                IsExcludeFromCalculation = history.IsExcludeFromCalculation
            };
        }

        public static CorrectionByRoomHistoryDto UnMap(CorrectionByRoomModel model)
        {
            return new CorrectionByRoomHistoryDto
            {
                Id = model.Id,
                IsExcludeFromCalculation = model.IsExcludeFromCalculation
            };
        }
    }
}
