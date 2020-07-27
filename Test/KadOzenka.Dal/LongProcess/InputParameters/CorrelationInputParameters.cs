using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.InputParameters
{
    public class CorrelationInputParameters
    {
        public List<long> AttributeIds { get; set; }
        public string QsQueryStr { get; set; }
    }
}
