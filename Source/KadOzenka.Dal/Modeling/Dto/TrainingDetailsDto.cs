using KadOzenka.Dal.Modeling.Entities;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling.Dto
{
    public class TrainingDetailsDto
    {
        public long ModelId { get; set; }
        public string ModelName { get; set; }
        public KoAlgoritmType Type { get; set; }


        public string MeanSquaredErrorTrain { get; set; }
        public string MeanSquaredErrorTest { get; set; }

        public string FisherCriterionTrain { get; set; }
        public string FisherCriterionTest { get; set; }

        public string R2Train { get; set; }
        public string R2Test { get; set; }

        public string ScatterImageLink { get; set; }
        public string CorrelationImageLink { get; set; }


        public QualityControlInfo QualityControlInfo { get; set; }
    }
}
