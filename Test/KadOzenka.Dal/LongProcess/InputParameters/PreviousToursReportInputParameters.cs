using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.InputParameters
{
    public class PreviousToursReportInputParameters
    {
        public List<long> TaskIds { get; set; }
        public long GroupId { get; set; }
    }
}
