using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.LongProcess.Reports.CalculationParams.Entities;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
	public class ModelingResultsModel : IValidatableObject
	{
		public long[] TaskIds { get; set; }
		
		[Display(Name = "Группа")]
		public long GroupId { get; set; }

		public List<DropDownTreeItemModel> PossibleGroups { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (TaskIds == null || TaskIds.Length == 0)
			{
				yield return new ValidationResult("Не указаны задания на оценку", new[] { nameof(TaskIds) });
			}
			if (GroupId == 0)
			{
				yield return new ValidationResult("Не указана группа", new[] { nameof(GroupId) });
			}
		}

		public ReportInputParameters MapToInputParameters()
		{
			return new ReportInputParameters
			{
				TaskIds = TaskIds.ToList(),
				GroupId = GroupId
			};
		}
	}
}
