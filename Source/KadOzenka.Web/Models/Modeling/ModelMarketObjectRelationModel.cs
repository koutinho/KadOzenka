using System.Collections.Generic;
using KadOzenka.Dal.Modeling.Dto;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelMarketObjectRelationModel
	{
		public long Id { get; set; }
		public string CadastralNumber { get; set; }
		public decimal Price { get; set; }
        public decimal? PriceFromModel { get; set; }
        public decimal? ModelingPrice { get; set; }
        public decimal? Percent { get; set; }
		public bool IsExcluded { get; set; }
        public bool IsForTraining { get; set; }
        public bool IsDirty { get; set; }
        public List<CoefficientForObject> Coefficients { get; set; }


        public static ModelMarketObjectRelationModel ToModel(ModelMarketObjectRelationDto entity)
        {
            return new ModelMarketObjectRelationModel
			{
				Id = entity.Id,
				CadastralNumber = entity.CadastralNumber,
				Price = entity.Price,
                PriceFromModel = entity.PriceFromModel,
				ModelingPrice = entity.ModelingPrice,
				Percent = entity.Percent,
                IsExcluded = entity.IsExcluded,
                IsForTraining = entity.IsForTraining,
                Coefficients = entity.Coefficients
            };
		}

		public static ModelMarketObjectRelationDto FromModel(ModelMarketObjectRelationModel model)
		{
			return new ModelMarketObjectRelationDto
			{
				Id = model.Id,
				CadastralNumber = model.CadastralNumber,
				Price = model.Price,
				IsExcluded = model.IsExcluded,
				IsForTraining = model.IsForTraining
			};
		}
	}
}
