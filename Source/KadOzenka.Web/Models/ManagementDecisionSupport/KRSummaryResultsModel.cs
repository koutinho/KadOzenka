using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
	public class KRSummaryResultsOksModel : IValidatableObject
	{
		public long[] TaskIds { get; set; }
		[Required]
		[Display(Name = "КЛАДР")]
		public long KladrAttributeId { get; set; }
		[Required]
		[Display(Name = "КН родителя")]
		public long ParentKnAttributeId { get; set; }

		public List<DropDownTreeItemModel> GbuAttributes { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (TaskIds == null || TaskIds.Length == 0)
			{
				yield return new ValidationResult("Не указаны задания на оценку", new[] { nameof(TaskIds) });
			}
		}
	}
}
