using System;
using System.Collections.Generic;
using System.Text;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Correction.Dto
{
    public class CorrectionForFirstFloorDto
    {
        public long Id { get; set; }
        public DateTime StatsDate { get; set; }
        public string BuildingCadastralNumber { get; set; }
        public MarketSegment MarketSegmentCode { get; set; }
        public decimal FirstFloorCoefficient { get; set; }
        public bool IsExcludedFromCalculation { get; set; }
    }
}
