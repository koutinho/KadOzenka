using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults.Entities;
using Kendo.Mvc.UI;

namespace KadOzenka.Web.Models.ManagementDecisionSupport.QualityPricingFactorsEncodingResults
{
	public class DataCompositionWithCrviForOksConfigurationModel : IValidatableObject
	{
		public long[] TaskIds { get; set; }
		
		[Display(Name = "КН родителя (для помещения)")]
		public long? ParentKnAttributeId { get; set; }

		[Display(Name = "Наименование вида использования")]
		public long? TypeOfUsingNameAttributeId { get; set; }

		[Display(Name = "Код вида использования")]
		public long? TypeOfUsingCodeAttributeId { get; set; }

		[Display(Name = "Источник информации кода вида использования")]
		public long? TypeOfUsingCodeSourceAttributeId { get; set; }

		[Display(Name = "Код подгруппы вида использования")]
		public long? TypeOfUsingGroupCodeAttributeId { get; set; }

		[Display(Name = "Наименование функциональной подгруппы")]
		public long? FunctionalGroupNameAttributeId { get; set; }

		[Display(Name = "Сегмент")]
		public long? SegmentAttributeId { get; set; }

		public List<DropDownTreeItemModel> GbuAttributes { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (TaskIds == null || TaskIds.Length == 0)
			{
				yield return new ValidationResult("Не указаны задания на оценку", new[] { nameof(TaskIds) });
			}
			if (ParentKnAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'КН родителя (для помещения)'", new[] { nameof(ParentKnAttributeId) });
			}
			if (TypeOfUsingNameAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Наименование вида использования'", new[] { nameof(TypeOfUsingNameAttributeId) });
			}
			if (TypeOfUsingCodeAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Код вида использования'", new[] { nameof(TypeOfUsingCodeAttributeId) });
			}
			if (TypeOfUsingCodeSourceAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Источник информации кода вида использования'", new[] { nameof(TypeOfUsingCodeSourceAttributeId) });
			}
			if (TypeOfUsingGroupCodeAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Код подгруппы вида использования'", new[] { nameof(TypeOfUsingGroupCodeAttributeId) });
			}
			if (FunctionalGroupNameAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Наименование функциональной подгруппы'", new[] { nameof(FunctionalGroupNameAttributeId) });
			}
			if (SegmentAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Сегмент'", new[] { nameof(SegmentAttributeId) });
			}
		}


		public InputParametersForOks MapToInputParameters()
		{
			return new InputParametersForOks
			{
				TaskIds = TaskIds.ToList(),
				ParentKnAttributeId = ParentKnAttributeId.GetValueOrDefault(),
				TypeOfUsingNameAttributeId = TypeOfUsingNameAttributeId.GetValueOrDefault(),
				TypeOfUsingCodeAttributeId = TypeOfUsingCodeAttributeId.GetValueOrDefault(),
				TypeOfUsingCodeSourceAttributeId = TypeOfUsingCodeSourceAttributeId.GetValueOrDefault(),
				TypeOfUsingGroupCodeAttributeId = TypeOfUsingGroupCodeAttributeId.GetValueOrDefault(),
				SegmentAttributeId = SegmentAttributeId.GetValueOrDefault(),
				FunctionalGroupNameAttributeId = FunctionalGroupNameAttributeId.GetValueOrDefault()
			};
		}
	}
}
