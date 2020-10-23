using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class ModelingModelDto
	{
        public long ModelId { get; set; }
        public KoModelType Type { get; set; }
        public KoAlgoritmType AlgorithmType { get; set; }
        public KoCalculationType CalculationType { get; set; }
        public KoCalculationMethod CalculationMethod { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public decimal? A0 { get; set; }
        public long TourId { get; set; }
		public long TourYear { get; set; }
        public long? GroupId { get; set; }
        public string GroupName { get; set; }
        public string LinearTrainingResult { get; set; }
        public string ExponentialTrainingResult { get; set; }
        public string MultiplicativeTrainingResult { get; set; }
        public bool IsOksObjectType { get; set; }
        public List<ModelAttributeRelationDto> Attributes { get; set; }

        public ModelingModelDto()
		{
			Attributes = new List<ModelAttributeRelationDto>();
		}
	}
}
