using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.LongProcess.Reports.ResultsByCadastralDistrict.Entities;
using Kendo.Mvc.UI;

namespace KadOzenka.Web.Models.ManagementDecisionSupport.ResultsByCadastralDistrictReport
{
	public class UncompletedBuildingsConfigurationModel : IValidatableObject
	{
		public long[] TaskIds { get; set; }
		
		[Display(Name = "Сегмент")]
		public long? SegmentAttributeId { get; set; }

		[Display(Name = "Наименование вида использования")]
		public long? UsageTypeNameAttributeId { get; set; }

		[Display(Name = "Код вида использования")]
		public long? UsageTypeCodeAttributeId { get; set; }

		[Display(Name = "Источник информации кода вида использования")]
		public long? UsageTypeCodeSourceAttributeId { get; set; }

		[Display(Name = "Код подгруппы вида использования")]
		public long? SubGroupUsageTypeCodeAttributeId { get; set; }

		[Display(Name = "Наименование функциональной подгруппы")]
		public long? FunctionalSubGroupNameAttributeId { get; set; }

		public List<DropDownTreeItemModel> GbuAttributes { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (TaskIds == null || TaskIds.Length == 0)
			{
				yield return new ValidationResult("Не указаны задания на оценку", new[] { nameof(TaskIds) });
			}
			if (SegmentAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Сегмент'", new[] { nameof(SegmentAttributeId) });
			}
			if (UsageTypeNameAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указано 'Наименование вида использования'", new[] { nameof(UsageTypeNameAttributeId) });
			}
			if (UsageTypeCodeAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Код вида использования'", new[] { nameof(UsageTypeCodeAttributeId) });
			}
			if (UsageTypeCodeSourceAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Источник информации кода вида использования'", new[] { nameof(UsageTypeCodeSourceAttributeId) });
			}
			if (SubGroupUsageTypeCodeAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указан 'Код подгруппы вида использования'", new[] { nameof(SubGroupUsageTypeCodeAttributeId) });
			}
			if (FunctionalSubGroupNameAttributeId.GetValueOrDefault() == 0)
			{
				yield return new ValidationResult("Не указано 'Наименование функциональной подгруппы'", new[] { nameof(FunctionalSubGroupNameAttributeId) });
			}
		}


		public InputParametersForUncompletedBuildings MapToInputParameters()
		{
			return new InputParametersForUncompletedBuildings
			{
				TaskIds = TaskIds.ToList(),
				SegmentAttributeId = SegmentAttributeId.GetValueOrDefault(),
				UsageTypeNameAttributeId = UsageTypeNameAttributeId.GetValueOrDefault(),
				UsageTypeCodeAttributeId = UsageTypeCodeAttributeId.GetValueOrDefault(),
				UsageTypeCodeSourceAttributeId = UsageTypeCodeSourceAttributeId.GetValueOrDefault(),
				SubGroupUsageTypeCodeAttributeId = SubGroupUsageTypeCodeAttributeId.GetValueOrDefault(),
				FunctionalSubGroupNameAttributeId = FunctionalSubGroupNameAttributeId.GetValueOrDefault()
			};
		}
	}
}
