using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Correction.Dto;

namespace KadOzenka.Web.Models.MarketObject
{
    public class CorrectionByRoomModel
    {
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Коэффициент на 1-но комнатную квартиру")]
        public decimal OneRoomCoefficient { get; set; }

        [Display(Name = "Коэффициент на 2-х комнатную квартиру")]
        public decimal TwoRoomsCoefficient { get; set; }


        public static CorrectionByRoomModel Map(CorrectionByRoomHistoryDto history)
        {
            return new CorrectionByRoomModel
            {
               Date = history.Date,
               OneRoomCoefficient = history.OneRoomCoefficient,
               TwoRoomsCoefficient = history.TwoRoomsCoefficient
            };
        }
    }
}
