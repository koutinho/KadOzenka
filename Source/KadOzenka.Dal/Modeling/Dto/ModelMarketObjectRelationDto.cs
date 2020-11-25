﻿using System.Collections.Generic;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class ModelMarketObjectRelationDto
	{
		public long Id { get; set; }
		public string CadastralNumber { get; set; }
		public long? MarketObjectId { get; set; }
		public long? UnitId { get; set; }
		public decimal Price { get; set; }
        public decimal? PriceFromModel { get; set; }
        public decimal? DeviationFromPredictablePrice => ModelingService.CalculatePercent(PriceFromModel, Price);
        public decimal? ModelingPrice { get; set; }
        public decimal? Percent { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsForTraining { get; set; }
        public bool IsForControl { get; set; }
        public List<CoefficientForObject> Coefficients { get; set; }
    }
}
