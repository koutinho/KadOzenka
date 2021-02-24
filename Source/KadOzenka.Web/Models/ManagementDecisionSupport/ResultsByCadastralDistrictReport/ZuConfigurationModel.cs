using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.LongProcess.Reports.ResultsByCadastralDistrict.Entities;
using Kendo.Mvc.UI;

namespace KadOzenka.Web.Models.ManagementDecisionSupport.ResultsByCadastralDistrictReport
{
	public class ZuConfigurationModel : IValidatableObject
	{
		public long[] TaskIds { get; set; }
		
		[Display(Name = "Сведения о нахождении на земельном участке других связанных с ним объектов недвижимости")]
		public long? InfoAboutExistenceOfOtherObjectsAttributeId { get; set; }

		[Display(Name = "Источник информации")]
		public long? InfoSourceAttributeId { get; set; }

		[Display(Name = "Сегмент")]
		public long? SegmentAttributeId { get; set; }

		[Display(Name = "Код вида использования")]
		public long? UsageTypeCodeAttributeId { get; set; }

		[Display(Name = "Наименование вида использования")]
		public long? UsageTypeNameAttributeId { get; set; }

		[Display(Name = "Источник информации кода вида использования")]
		public long? UsageTypeCodeSourceAttributeId { get; set; }

		public List<DropDownTreeItemModel> GbuAttributes { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (TaskIds == null || TaskIds.Length == 0)
			{
				yield return new ValidationResult("Не указаны задания на оценку", new[] { nameof(TaskIds) });
			}
			if (InfoAboutExistenceOfOtherObjectsAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указаны 'Сведения о нахождении на земельном участке других связанных с ним объектов недвижимости'", new[] { nameof(InfoAboutExistenceOfOtherObjectsAttributeId) });
			}
			if (InfoSourceAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Источник информации'", new[] { nameof(InfoSourceAttributeId) });
			}
			if (SegmentAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Сегмент'", new[] { nameof(SegmentAttributeId) });
			}
			if (UsageTypeCodeAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Код вида использования'", new[] { nameof(UsageTypeCodeAttributeId) });
			}
			if (UsageTypeNameAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указано 'Наименование вида использования'", new[] { nameof(UsageTypeNameAttributeId) });
			}
			if (UsageTypeCodeSourceAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Источник информации кода вида использования'", new[] { nameof(UsageTypeCodeSourceAttributeId) });
			}
		}


		public InputParametersForZu MapToInputParameters()
		{
			return new InputParametersForZu
			{
				TaskIds = TaskIds.ToList(),
				InfoAboutExistenceOfOtherObjectsAttributeId = InfoAboutExistenceOfOtherObjectsAttributeId.GetValueOrDefault(),
				InfoSourceAttributeId = InfoSourceAttributeId.GetValueOrDefault(),
				SegmentAttributeId = SegmentAttributeId.GetValueOrDefault(),
				UsageTypeCodeAttributeId = UsageTypeCodeAttributeId.GetValueOrDefault(),
				UsageTypeNameAttributeId = UsageTypeNameAttributeId.GetValueOrDefault(),
				UsageTypeCodeSourceAttributeId = UsageTypeCodeSourceAttributeId.GetValueOrDefault()
			};
		}
	}
}
