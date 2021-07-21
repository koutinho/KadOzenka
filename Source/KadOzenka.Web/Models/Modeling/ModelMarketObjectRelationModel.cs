using System.Collections.Generic;
using Core.Shared.Extensions;
using ModelingBusiness.Objects;
using ModelingBusiness.Objects.Entities;
using ObjectModel.Directory;
using ObjectModel.Modeling;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelMarketObjectRelationModel
	{
		public long Id { get; set; }
		public string CadastralNumber { get; set; }
		public long? MarketObjectId { get; set; }
		public long? UnitId { get; set; }
		public string UnitPropertyType { get; set; }
		public decimal Price { get; set; }
        public decimal? PriceFromModel { get; set; }
        public decimal? ModelingPrice { get; set; }
        public decimal? DeviationFromPredictablePrice { get; set; }
        public decimal? Percent { get; set; }
		public bool IsExcluded { get; set; }
        public bool IsForTraining { get; set; }
        public bool IsForControl { get; set; }
        public bool IsDirty { get; set; }
        public List<CoefficientForObject> Coefficients { get; set; }


        public static ModelMarketObjectRelationModel ToModel(OMModelToMarketObjects entity)
        {
	        return new ModelMarketObjectRelationModel
			{
				Id = entity.Id,
				CadastralNumber = entity.CadastralNumber,
				MarketObjectId = entity.MarketObjectId,
				UnitId = entity.UnitId,
				UnitPropertyType = entity.UnitPropertyType_Code == PropertyTypes.None ? null : entity.UnitPropertyType_Code.GetEnumDescription(),
				Price = entity.Price,
                PriceFromModel = entity.PriceFromModel,
				//ModelingPrice = entity.ModelingPrice,
				DeviationFromPredictablePrice = ModelObjectsService.CalculatePercent(entity.PriceFromModel, entity.Price),
				//Percent = entity.Percent,
                IsExcluded = entity.IsExcluded.GetValueOrDefault(),
                IsForTraining = entity.IsForTraining.GetValueOrDefault(),
                IsForControl = entity.IsForControl.GetValueOrDefault(),
                Coefficients = entity.DeserializedCoefficients
			};
		}

		public static ModelMarketObjectRelationDto FromModel(ModelMarketObjectRelationModel model)
		{
			return new ModelMarketObjectRelationDto
			{
				Id = model.Id,
				IsExcluded = model.IsExcluded,
				IsForTraining = model.IsForTraining,
				IsForControl = model.IsForControl
			};
		}
	}
}
