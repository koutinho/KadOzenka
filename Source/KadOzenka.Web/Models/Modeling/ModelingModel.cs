using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Modeling.Dto;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingModel
	{
		public long? Id { get; set; }

		[Display(Name = "Имя")]
		[Required(ErrorMessage = "Не заполнено Имя")]
		public string Name { get; set; }

		[Display(Name = "Сегмент")]
		public MarketSegment? PropertyMarketSegment { get; set; }


		public static ModelingModelDto FromModel(ModelingModel model)
		{
			return new ModelingModelDto
			{
				Name = model.Name
			};
		}
	}
}
