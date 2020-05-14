using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;

namespace KadOzenka.Dal.LongProcess.InputParameters
{
    public class ModelingInputParameters
    {
        public long ModelId { get; set; }
        public bool IsTrainingMode { get; set; }
        public PredictionType PredictionType { get; set; }
    }
}
