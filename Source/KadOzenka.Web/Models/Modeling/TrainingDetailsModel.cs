using KadOzenka.Dal.Modeling.Entities;

namespace KadOzenka.Web.Models.Modeling
{
    public class TrainingDetailsModel
    {
        public string MeanSquaredErrorTrain { get; set; }
        public string MeanSquaredErrorTest { get; set; }

        public string FisherCriterionTrain { get; set; }
        public string FisherCriterionTest { get; set; }

        public string R2Train { get; set; }
        public string R2Test { get; set; }

        public string ScatterImageLink { get; set; }
        public string CorrelationImageLink { get; set; }


        public static TrainingDetailsModel ToModel(TrainingResult trainingResult)
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
                CorrelationImageLink = trainingResult?.Images?.CorrelationLink
            };
        }
    }
}
