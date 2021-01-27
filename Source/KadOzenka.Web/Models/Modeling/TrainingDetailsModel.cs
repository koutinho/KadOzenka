using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Modeling
{
    public class TrainingDetailsModel
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

        public bool? ConclusionForStudent { get; set; }
        public bool? ConclusionForMeanSquaredError { get; set; }
        public bool? ConclusionForR2 { get; set; }
        public bool? ConclusionForFisher { get; set; }



        public string MeanSquaredErrorTrain { get; set; }
        public string MeanSquaredErrorTest { get; set; }

        public string FisherCriterionTrain { get; set; }
        public string FisherCriterionTest { get; set; }

        public string R2Train { get; set; }
        public string R2Test { get; set; }

        public string ScatterImageLink { get; set; }
        public string CorrelationImageLink { get; set; }


        public static TrainingDetailsModel ToModel(TrainingDetailsDto trainingResult)
        {
	        return new TrainingDetailsModel
            {
                ModelId = trainingResult.ModelId,
                ModelName = trainingResult.ModelName,
                Type = trainingResult.Type,
				MeanSquaredErrorTrain = trainingResult.MeanSquaredErrorTrain,
                MeanSquaredErrorTest = trainingResult.MeanSquaredErrorTest,
                FisherCriterionTrain = trainingResult.FisherCriterionTrain,
                FisherCriterionTest = trainingResult.FisherCriterionTest,
                R2Train = trainingResult.R2Train,
                R2Test = trainingResult.R2Test,
                ScatterImageLink = trainingResult.ScatterImageLink,
                CorrelationImageLink = trainingResult.CorrelationImageLink,
                StudentCriterionForCalculation = trainingResult.StudentCriterionForCalculation,
                StudentCriterionForTable = trainingResult.StudentCriterionForTable,
                MeanSquaredError = trainingResult.MeanSquaredError,
                R2 = trainingResult.R2,
                FisherCriterionForCalculation = trainingResult.FisherCriterionForCalculation,
                FisherCriterionForTable = trainingResult.FisherCriterionForTable,
                CriterionForStudent = trainingResult.CriterionForStudent,
                CriterionForMeanSquaredError = trainingResult.CriterionForMeanSquaredError,
                CriterionForR2 = trainingResult.CriterionForR2,
                CriterionForFisher = trainingResult.CriterionForFisher,
                ConclusionForStudent = trainingResult.ConclusionForStudent,
                ConclusionForMeanSquaredError = trainingResult.ConclusionForMeanSquaredError,
                ConclusionForR2 = trainingResult.ConclusionForR2,
                ConclusionForFisher = trainingResult.ConclusionForFisher
            };
        }
    }
}
