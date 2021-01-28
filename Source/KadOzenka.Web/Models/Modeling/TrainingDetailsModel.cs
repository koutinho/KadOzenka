using System;
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

        public string ScatterImageData { get; set; }
        public string CorrelationImageData { get; set; }


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
                ScatterImageData = ConvertByteToImage(trainingResult.ScatterImage),
                CorrelationImageData = ConvertByteToImage(trainingResult.CorrelationImage),
                TrainingQualityInfoModel = TrainingQualityInfoModel.ToModel(trainingResult)
            };
        }

        
        #region Support Methods

        private static string ConvertByteToImage(byte[] image)
        {
	        if (image == null || image.Length == 0)
		        return string.Empty;

	        var imageStr = Convert.ToBase64String(image);
	        return $"data:image/png;base64,{imageStr}";
        }

        #endregion
    }
}
