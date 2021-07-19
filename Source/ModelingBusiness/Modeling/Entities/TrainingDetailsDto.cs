using ModelingBusiness.Modeling.Responses;
using ObjectModel.Directory;

namespace ModelingBusiness.Modeling.Entities
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

        public byte[] ScatterImage { get; set; }
        public byte[] CorrelationImage { get; set; }

        public QualityControlInfo QualityControlInfo { get; set; }
    }
}
