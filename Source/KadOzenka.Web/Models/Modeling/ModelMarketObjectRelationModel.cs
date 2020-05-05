﻿using KadOzenka.Dal.Modeling.Dto;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelMarketObjectRelationModel
	{
		public long Id { get; set; }
		public string CadastralNumber { get; set; }
		public decimal Price { get; set; }
		public bool IsExcluded { get; set; }
		public bool IsDirty { get; set; }


		public static ModelMarketObjectRelationModel ToModel(ModelMarketObjectRelationDto entity)
		{
			return new ModelMarketObjectRelationModel
			{
				Id = entity.Id,
				CadastralNumber = entity.CadastralNumber,
				Price = entity.Price,
				IsExcluded = entity.IsExcluded
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
