using KadOzenka.Dal.Modeling.Entities;

namespace KadOzenka.Dal.LongProcess.InputParameters
{
    public class PredictionInputParameters
    {
        public long ModelId { get; set; }
        public PredictionType PredictionType { get; set; }
    }
}
