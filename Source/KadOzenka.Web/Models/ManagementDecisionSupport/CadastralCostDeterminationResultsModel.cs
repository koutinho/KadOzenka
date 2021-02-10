using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
	public class CadastralCostDeterminationResultsModel : IValidatableObject
	{
		public long[] TaskIds { get; set; }
		[Display(Name = "Тип")]
		public ReportType ReportType { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (TaskIds == null || TaskIds.Length == 0)
			{
				yield return new ValidationResult("Не указаны задания на оценку", new[] { nameof(TaskIds) });
			}
		}
	}
}
