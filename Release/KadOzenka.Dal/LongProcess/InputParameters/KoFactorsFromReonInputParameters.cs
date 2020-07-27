using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.InputParameters
{
    public class KoFactorsFromReonInputParameters
    {
        public long TaskId { get; set; }
        public List<long> AttributeIds { get; set; }
    }
}
