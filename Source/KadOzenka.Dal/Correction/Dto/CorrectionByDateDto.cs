using System;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Correction.Dto
{
    public class CorrectionByDateDto
    {
        public long Id { get; set; }
        public string BuildingCadastralNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal Coefficient { get; set; }
        public MarketSegment MarketSegment { get; set; }
        public bool IsExcludeFromCalculation { get; set; }
    }
}
