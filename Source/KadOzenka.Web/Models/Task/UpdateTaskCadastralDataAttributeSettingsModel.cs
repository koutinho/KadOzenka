using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Task
{
	public class UpdateTaskCadastralDataAttributeSettingsModel : IValidatableObject
	{
		[Display(Name = "Атрибут кадастрового квартала")]
		public long? CadastralQuarterGbuAttributeId { get; set; }

		[Display(Name = "Атрибут кадастрового номера здания")]
		public long? BuildingCadastralNumberGbuAttributeId { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!CadastralQuarterGbuAttributeId.HasValue && !BuildingCadastralNumberGbuAttributeId.HasValue)
			{
				yield return
					new ValidationResult("Должен быть заполнен хотя бы один из атрибутов");
			}
		}
	}
}
