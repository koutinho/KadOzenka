using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace KadOzenka.Web.Models.Tour
{
	public class ImportGroupDataModel
	{
		[Display(Name = "Тур")]
		[Required(ErrorMessage = "Не выбран Тур")]
		public long? TourId { get; set; }

		[Display(Name = "Статус единицы оценки")]
		[Required(ErrorMessage = "Не выбран Статус единицы оценки")]
		public KoUnitStatus? UnitStatus { get; set; }

		[Required(ErrorMessage = "Файл не выбран")]
		public IFormFile File { get; set; }
	}
}
