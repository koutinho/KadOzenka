using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;

namespace KadOzenka.Web.Models.Modeling
{
    public class TrainingQualityInfoModel
    {
	    public string StudentCriterionForCalculation { get; set; }
        public string StudentCriterionForTable { get; set; }
        public string MeanSquaredError { get; set; }
        public string R2 { get; set; }
        public string FisherCriterionForCalculation { get; set; }
        public string FisherCriterionForTable { get; set; }
        
        public string CriterionForStudent { get; set; }
        public string CriterionForMeanSquaredError { get; set; }
        public string CriterionForR2 { get; set; }
        public string CriterionForFisher { get; set; }

        public bool? ConclusionForStudent { get; set; }
        public bool? ConclusionForMeanSquaredError { get; set; }
        public bool? ConclusionForR2 { get; set; }
        public bool? ConclusionForFisher { get; set; }


        public static TrainingQualityInfoModel ToModel(TrainingDetailsDto trainingResult)
        {
	        return new TrainingQualityInfoModel
            {
	            StudentCriterionForCalculation = trainingResult.MeanSquaredErrorTest,
                StudentCriterionForTable = trainingResult.MeanSquaredErrorTest,
                MeanSquaredError = trainingResult.MeanSquaredErrorTest,
                R2 = trainingResult.R2Test,
                FisherCriterionForCalculation = trainingResult.FisherCriterionTest,
                FisherCriterionForTable = trainingResult.FisherCriterionTest,
                CriterionForStudent = trainingResult.QualityControlInfo.Student.Criterion,
                CriterionForMeanSquaredError = trainingResult.QualityControlInfo.MeanSquaredError.Criterion,
                CriterionForR2 = trainingResult.QualityControlInfo.R2.Criterion,
                CriterionForFisher = trainingResult.QualityControlInfo.Fisher.Criterion,
                ConclusionForStudent = trainingResult.QualityControlInfo.Student.Conclusion,
                ConclusionForMeanSquaredError = trainingResult.QualityControlInfo.MeanSquaredError.Conclusion,
                ConclusionForR2 = trainingResult.QualityControlInfo.R2.Conclusion,
                ConclusionForFisher = trainingResult.QualityControlInfo.Fisher.Conclusion
            };
        }

        public QualityControlInfo FromModel()
        {
	        return new QualityControlInfo
            {
		        Student = new QualityControlGeneral
		        {
			        Criterion = CriterionForStudent, 
			        Conclusion = ConclusionForStudent.GetValueOrDefault()
		        },
                MeanSquaredError = new QualityControlGeneral
                {
	                Criterion = CriterionForMeanSquaredError,
	                Conclusion = ConclusionForMeanSquaredError.GetValueOrDefault()
                },
                R2 = new QualityControlGeneral
                {
	                Criterion = CriterionForR2,
	                Conclusion = ConclusionForR2.GetValueOrDefault()
                },
                Fisher = new QualityControlGeneral
                {
	                Criterion = CriterionForFisher,
	                Conclusion = ConclusionForFisher.GetValueOrDefault()
                }
            };
        }
    }
}
