﻿using System.Collections.Generic;
using KadOzenka.Dal.Modeling.Factors.Entities;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling.Model.Entities
{
	public class ModelDto
	{
        public long ModelId { get; set; }
        public KoModelType Type { get; set; }
        public KoAlgoritmType AlgorithmTypeForCadastralPriceCalculation { get; set; }
        public KoAlgoritmType AlgorithmType { get; set; }
        public KoCalculationType CalculationType { get; set; }
        public KoCalculationMethod CalculationMethod { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public string Formula { get; set; }
		public decimal? A0 { get; set; }
        public long TourId { get; set; }
		public long TourYear { get; set; }
        public long? GroupId { get; set; }
        public string GroupName { get; set; }
        public string LinearTrainingResult { get; set; }
        public string ExponentialTrainingResult { get; set; }
        public string MultiplicativeTrainingResult { get; set; }
        public bool IsOksObjectType { get; set; }
        public bool IsActive { get; set; }
        public List<ModelAttributeRelationDto> Attributes { get; set; }

        public ModelDto()
		{
			Attributes = new List<ModelAttributeRelationDto>();
		}
	}
}
