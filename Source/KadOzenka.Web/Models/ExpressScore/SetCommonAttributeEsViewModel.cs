using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.ExpressScore
{
	public class SetCommonAttributeEsViewModel
	{
		[Display(Name = "Атрибут кадастрового номера строения")]
		[Required(ErrorMessage = "Атрибут кадастрового номера строения обязательное")]
		public long? CadastralNumbeGbuAttributeId { get; set; }

	}
}