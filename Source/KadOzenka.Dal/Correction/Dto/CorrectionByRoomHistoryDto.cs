using System;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Correction.Dto
{
    public class CorrectionByRoomHistoryDto
    {
        public long Id { get; set; }
        public string BuildingCadastralNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal OneRoomCoefficient { get; set; }
        public decimal ThreeRoomsCoefficient { get; set; }
        public MarketSegment MarketSegment { get; set; }
        public bool IsExcludeFromCalculation { get; set; }
    }
}
