using System.Collections.Generic;
using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Oks;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingModel
	{
		public long? Id { get; set; }

		[Display(Name = "Имя")]
		[Required(ErrorMessage = "Не заполнено Имя")]
		public string Name { get; set; }

		[Display(Name = "Тур")]
		public long? TourId { get; set; }

		[Display(Name = "Сегмент")]
		public MarketSegment? PropertyMarketSegment { get; set; }

		[Display(Name = "Тип")]
		public ObjectType ObjectType { get; set; }

		public List<Attributes> Attributes { get; set; }


		public static ModelingModel ToModel(ModelingModelDto entity)
		{
			return new ModelingModel
			{
				Id = entity.ModelId,
				PropertyMarketSegment = entity.MarketSegment,
				TourId = entity.TourId,
				Name = entity.Name,
				Attributes = entity.Attributes
			};
		}

		public static ModelingModelDto FromModel(ModelingModel model)
		{
			return new ModelingModelDto
			{
				Name = model.Name
			};
		}
	}
}
