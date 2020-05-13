using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.DataProcessing;
using KadOzenka.Dal.Modeling.Dto;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelMarketObjectRelationModel
	{
		public long Id { get; set; }
		public string CadastralNumber { get; set; }
		public decimal Price { get; set; }
        public decimal? PriceFromModel { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsForTraining { get; set; }
        public bool IsDirty { get; set; }
        public List<CoefficientForObject> Coefficients { get; set; }
        public string Warnings { get; set; }


        public static ModelMarketObjectRelationModel ToModel(ModelMarketObjectRelationDto entity)
        {
            var warnings = new StringBuilder();
            entity.Coefficients?.Where(x => !string.IsNullOrWhiteSpace(x.Message)).ForEach(x => warnings.AppendLine(x.Message));
            
            return new ModelMarketObjectRelationModel
			{
				Id = entity.Id,
				CadastralNumber = entity.CadastralNumber,
				Price = entity.Price,
                PriceFromModel = entity.PriceFromModel,
                IsExcluded = entity.IsExcluded,
                IsForTraining = entity.IsForTraining,
                Coefficients = entity.Coefficients,
                Warnings = warnings.ToString()
            };
		}

		public static ModelMarketObjectRelationDto FromModel(ModelMarketObjectRelationModel model)
		{
			return new ModelMarketObjectRelationDto
			{
				Id = model.Id,
				CadastralNumber = model.CadastralNumber,
				Price = model.Price,
				IsExcluded = model.IsExcluded
			};
		}
	}
}
