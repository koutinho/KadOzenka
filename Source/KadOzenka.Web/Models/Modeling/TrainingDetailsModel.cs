using KadOzenka.Dal.Modeling.Entities;

namespace KadOzenka.Web.Models.Modeling
{
    public class TrainingDetailsModel
    {
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

        public string ConclusionForStudent { get; set; }
        public string ConclusionForMeanSquaredError { get; set; }
        public string ConclusionForR2 { get; set; }
        public string ConclusionForFisher { get; set; }



        public string MeanSquaredErrorTrain { get; set; }
        public string MeanSquaredErrorTest { get; set; }

        public string FisherCriterionTrain { get; set; }
        public string FisherCriterionTest { get; set; }

        public string R2Train { get; set; }
        public string R2Test { get; set; }

        public string ScatterImageLink { get; set; }
        public string CorrelationImageLink { get; set; }


        public static TrainingDetailsModel ToModel(TrainingResponse trainingResult)
        {
            return new TrainingDetailsModel
            {
                MeanSquaredErrorTrain = trainingResult?.AccuracyScore?.MeanSquaredError?.Train,
                MeanSquaredErrorTest = trainingResult?.AccuracyScore?.MeanSquaredError?.Test,
                FisherCriterionTrain = trainingResult?.AccuracyScore?.FisherCriterion?.Train,
                FisherCriterionTest = trainingResult?.AccuracyScore?.FisherCriterion?.Test,
                R2Train = trainingResult?.AccuracyScore?.R2?.Train,
                R2Test = trainingResult?.AccuracyScore?.R2?.Test,
                ScatterImageLink = trainingResult?.Images?.ScatterLink,
                CorrelationImageLink = trainingResult?.Images?.CorrelationLink,
                StudentCriterionForCalculation = -1,
                StudentCriterionForTable = -2,
                MeanSquaredError = -3,
                R2 = -4,
                FisherCriterionForCalculation = -5,
                FisherCriterionForTable = -6,
                CriterionForStudent = "Критерий для Стьюдента",
                CriterionForMeanSquaredError = "Критерий для ошибки",
                CriterionForR2 = "Критерий для R2",
                CriterionForFisher = "Критерий для Фишера",
                ConclusionForStudent = "Вывод для Стьюдента",
                ConclusionForMeanSquaredError = "Вывод для ошибки",
                ConclusionForR2 = "Вывод для R2",
                ConclusionForFisher = "Вывод для Фишера"
            };
        }
    }
}
