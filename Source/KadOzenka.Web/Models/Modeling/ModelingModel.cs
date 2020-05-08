using System.Collections.Generic;
using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Oks;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingModel
	{
		public long Id { get; set; }
        public bool IsModelWasTrained { get; set; }

        [Display(Name = "Имя")]
		[Required(ErrorMessage = "Не заполнено Имя")]
		public string Name { get; set; }

		[Display(Name = "Тур")]
		[Required(ErrorMessage = "Не выбран Тур")]
		public long TourId { get; set; }

		[Display(Name = "Тур")]
		public long TourYear { get; set; }

		[Display(Name = "Сегмент")]
		[Required(ErrorMessage = "Не выбран Сегмент")]
		public MarketSegment MarketSegmentCode { get; set; }

		[Display(Name = "Сегмент")]
		public string MarketSegment => MarketSegmentCode.GetEnumDescription();

		[Display(Name = "Тип")]
		public ObjectType ObjectType { get; set; }

		public List<ModelAttributeDto> Attributes { get; set; }


		public static ModelingModel ToModel(ModelingModelDto entity)
		{
			return new ModelingModel
			{
				Id = entity.ModelId,
				MarketSegmentCode = entity.MarketSegment,
				TourId = entity.TourId,
				TourYear = entity.TourYear,
				Name = entity.Name,
				Attributes = entity.Attributes,
                IsModelWasTrained = entity.WasTrained
			};
		}

		public static ModelingModelDto FromModel(ModelingModel model)
		{
			return new ModelingModelDto
			{
				ModelId =  model.Id,
				Name = model.Name,
				TourId = model.TourId,
				MarketSegment = model.MarketSegmentCode,
				Attributes = model.Attributes
			};
		}
	}
}
