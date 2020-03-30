using System;

namespace KadOzenka.Dal.Correction.Dto
{
    public class CorrectionByRoomHistoryDto
    {
        public DateTime Date { get; set; }
        public decimal OneRoomCoefficient { get; set; }
        public decimal TwoRoomsCoefficient { get; set; }
    }
}
