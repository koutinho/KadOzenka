using System;
using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Correction.Requests
{
    public class CorrectionByBargainRequest
    {
        public HashSet<MarketSegment> MarketSegments { get; set; }
        public CoefficientCoverageAreaType CoverageAreaType { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
