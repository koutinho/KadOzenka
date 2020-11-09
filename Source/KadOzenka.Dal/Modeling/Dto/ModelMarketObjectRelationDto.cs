﻿using System.Collections.Generic;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class ModelMarketObjectRelationDto
	{
		public long Id { get; set; }
		public string CadastralNumber { get; set; }
		public decimal Price { get; set; }
        public decimal? PriceFromModel { get; set; }
        public decimal? ModelingPrice { get; set; }
        public decimal? Percent { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsForTraining { get; set; }
        public List<CoefficientForObject> Coefficients { get; set; }
    }
}
