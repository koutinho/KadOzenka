using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling.Dto
{
    public class TrainingDetailsDto
    {
        public long ModelId { get; set; }
        public string ModelName { get; set; }
        public KoAlgoritmType Type { get; set; }

        public decimal StudentCriterionForCalculation { get; set; }
        public decimal StudentCriterionForTable { get; set; }
        public decimal MeanSquaredError { get; set; }
        public decimal R2 { get; set; }
        public decimal FisherCriterionForCalculation { get; set; }
        public decimal FisherCriterionForTable { get; set; }
        
        public string CriterionForStudent { get; set; }
        public string CriterionForMeanSquaredError { get; set; }
        public string CriterionForR2 { get; set; }
        public string CriterionForFisher { get; set; }

        public bool ConclusionForStudent { get; set; }
        public bool ConclusionForMeanSquaredError { get; set; }
        public bool ConclusionForR2 { get; set; }
        public bool ConclusionForFisher { get; set; }



        public string MeanSquaredErrorTrain { get; set; }
        public string MeanSquaredErrorTest { get; set; }

        public string FisherCriterionTrain { get; set; }
        public string FisherCriterionTest { get; set; }

        public string R2Train { get; set; }
        public string R2Test { get; set; }

        public string ScatterImageLink { get; set; }
        public string CorrelationImageLink { get; set; }
    }
}
