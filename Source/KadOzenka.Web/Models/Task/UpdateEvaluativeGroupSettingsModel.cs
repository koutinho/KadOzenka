using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Task
{
	public class UpdateEvaluativeGroupSettingsModel : IValidatableObject
	{
		[Display(Name = "Атрибут оценочной группы")]
		public long? EvaluativeGroupGbuAttributeId { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!EvaluativeGroupGbuAttributeId.HasValue)
			{
				yield return new ValidationResult("Не выбран атрибут");
			}
		}
	}
}
