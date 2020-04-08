using System;
using ObjectModel.Directory;

namespace KadOzenka.Dal.LongProcess.InputParameters
{
    public class CorrectionForFirstFloorRequest
    {
        public DateTime? Date { get; set; }
        public MarketSegment? Segment { get; set; }
    }
}
