using KadOzenka.Dal.Modeling.Entities;

namespace KadOzenka.Web.Models.Modeling
{
    public class TrainingQualityInfoModel
    {
	    public string StudentForEstimated { get; set; }
        public string StudentForTabular { get; set; }
        public string MeanSquaredError { get; set; }
        public string R2 { get; set; }
        public string FisherCriterionForEstimated { get; set; }
        public string FisherCriterionForTabular { get; set; }
        
        public string CriterionForStudent { get; set; }
        public string CriterionForMeanSquaredError { get; set; }
        public string CriterionForR2 { get; set; }
        public string CriterionForFisher { get; set; }

        public bool? ConclusionForStudent { get; set; }
        public bool? ConclusionForMeanSquaredError { get; set; }
        public bool? ConclusionForR2 { get; set; }
        public bool? ConclusionForFisher { get; set; }


        public static TrainingQualityInfoModel ToModel(QualityControlInfo qualityControlInfo)
        {
	        return new TrainingQualityInfoModel
            {
	            StudentForEstimated = qualityControlInfo.Student.Estimated,
                StudentForTabular = qualityControlInfo.Student.Tabular,
                MeanSquaredError = qualityControlInfo.MeanSquaredError.Tabular,
                R2 = qualityControlInfo.R2.Tabular,
                FisherCriterionForEstimated = qualityControlInfo.Fisher.Estimated,
                FisherCriterionForTabular = qualityControlInfo.Fisher.Tabular,
                CriterionForStudent = qualityControlInfo.Student.Criterion,
                CriterionForMeanSquaredError = qualityControlInfo.MeanSquaredError.Criterion,
                CriterionForR2 = qualityControlInfo.R2.Criterion,
                CriterionForFisher = qualityControlInfo.Fisher.Criterion,
                ConclusionForStudent = qualityControlInfo.Student.Conclusion,
                ConclusionForMeanSquaredError = qualityControlInfo.MeanSquaredError.Conclusion,
                ConclusionForR2 = qualityControlInfo.R2.Conclusion,
                ConclusionForFisher = qualityControlInfo.Fisher.Conclusion
            };
        }

        public QualityControlInfo FromModel()
        {
	        return new QualityControlInfo
            {
		        Student = new QualityControlSpecial
		        {
			        Criterion = CriterionForStudent, 
			        Conclusion = ConclusionForStudent.GetValueOrDefault()
		        },
                MeanSquaredError = new QualityControlSpecial
                {
	                Criterion = CriterionForMeanSquaredError,
	                Conclusion = ConclusionForMeanSquaredError.GetValueOrDefault()
                },
                R2 = new QualityControlSpecial
                {
	                Criterion = CriterionForR2,
	                Conclusion = ConclusionForR2.GetValueOrDefault()
                },
                Fisher = new QualityControlSpecial
                {
	                Criterion = CriterionForFisher,
	                Conclusion = ConclusionForFisher.GetValueOrDefault()
                }
            };
        }
    }
}
