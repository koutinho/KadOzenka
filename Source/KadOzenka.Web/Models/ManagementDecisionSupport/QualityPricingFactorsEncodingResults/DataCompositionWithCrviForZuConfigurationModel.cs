using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults.Entities;
using Kendo.Mvc.UI;

namespace KadOzenka.Web.Models.ManagementDecisionSupport.QualityPricingFactorsEncodingResults
{
	public class DataCompositionWithCrviForZuConfigurationModel : IValidatableObject
	{
		public long[] TaskIds { get; set; }
		
		[Display(Name = "Сведения о нахождении на земельном участке других связанных с ним объектов недвижимости")]
		public long? LinkedObjectsInfoAttributeId { get; set; }

		[Display(Name = "Источник информации о нахождении на земельном участке других связанных с ним объектов недвижимости")]
		public long? LinkedObjectsInfoSourceAttributeId { get; set; }

		[Display(Name = "Наименование вида использования")]
		public long? TypeOfUsingNameAttributeId { get; set; }

		[Display(Name = "Код вида использования")]
		public long? TypeOfUsingCodeAttributeId { get; set; }

		[Display(Name = "Источник информации кода вида использования")]
		public long? TypeOfUsingCodeSourceAttributeId { get; set; }

		[Display(Name = "Сегмент")]
		public long? SegmentAttributeId { get; set; }

		public List<DropDownTreeItemModel> GbuAttributes { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (TaskIds == null || TaskIds.Length == 0)
			{
				yield return new ValidationResult("Не указаны задания на оценку", new[] { nameof(TaskIds) });
			}
			if (LinkedObjectsInfoAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указаны 'Сведения о нахождении на земельном участке других связанных с ним объектов недвижимости'", new[] { nameof(LinkedObjectsInfoAttributeId) });
			}
			if (LinkedObjectsInfoSourceAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Источник информации о нахождении на земельном участке других связанных с ним объектов недвижимости'", new[] { nameof(LinkedObjectsInfoSourceAttributeId) });
			}
			if (TypeOfUsingNameAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указано 'Наименование вида использования'", new[] { nameof(TypeOfUsingNameAttributeId) });
			}
			if (TypeOfUsingCodeAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Код вида использования'", new[] { nameof(TypeOfUsingCodeAttributeId) });
			}
			if (TypeOfUsingCodeSourceAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Источник информации кода вида использования'", new[] { nameof(TypeOfUsingCodeSourceAttributeId) });
			}
			if (SegmentAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Сегмент'", new[] { nameof(SegmentAttributeId) });
			}
		}


		public InputParametersForZu MapToInputParameters()
		{
			return new InputParametersForZu
			{
				TaskIds = TaskIds.ToList(),
				LinkedObjectsInfoAttributeId = LinkedObjectsInfoAttributeId.GetValueOrDefault(),
				LinkedObjectsInfoSourceAttributeId = LinkedObjectsInfoSourceAttributeId.GetValueOrDefault(),
				TypeOfUsingNameAttributeId = TypeOfUsingNameAttributeId.GetValueOrDefault(),
				TypeOfUsingCodeAttributeId = TypeOfUsingCodeAttributeId.GetValueOrDefault(),
				TypeOfUsingCodeSourceAttributeId = TypeOfUsingCodeSourceAttributeId.GetValueOrDefault(),
				SegmentAttributeId = SegmentAttributeId.GetValueOrDefault(),
			};
		}
	}
}
