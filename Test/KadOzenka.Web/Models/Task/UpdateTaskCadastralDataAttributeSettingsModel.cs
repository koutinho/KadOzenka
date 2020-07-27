using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Task
{
	public class UpdateTaskCadastralDataAttributeSettingsModel
	{
		[Display(Name = "Атрибут кадастрового квартала")]
		[Required(ErrorMessage = "Поле Атрибут кадастрового квартала обязательное")]
		public long? CadastralQuarterGbuAttributeId { get; set; }

		[Display(Name = "Атрибут кадастрового номера здания")]
		[Required(ErrorMessage = "Поле Атрибут кадастрового номера здания обязательное")]
		public long? BuildingCadastralNumberGbuAttributeId { get; set; }
	}
}
