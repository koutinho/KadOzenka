using System.Collections.Generic;
using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Oks;

namespace KadOzenka.Web.Models.Modeling
{
	public class AttributeAndDictionary
	{
		public long AttributeId { get; set; }
		public long DictionaryId { get; set; }
	}

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

		public List<AttributeAndDictionary> Attributes { get; set; }


		public static ModelingModelDto FromModel(ModelingModel model)
		{
			return new ModelingModelDto
			{
				Name = model.Name
			};
		}
	}
}
