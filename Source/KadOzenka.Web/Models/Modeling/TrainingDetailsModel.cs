using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Modeling
{
    public class TrainingDetailsModel
    {
        public long ModelId { get; set; }
        public string ModelName { get; set; }
        public KoAlgoritmType Type { get; set; }


        public TrainingQualityInfoModel TrainingQualityInfoModel { get; set; }


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
                TrainingQualityInfoModel = TrainingQualityInfoModel.ToModel(trainingResult)
            };
        }
    }
}
