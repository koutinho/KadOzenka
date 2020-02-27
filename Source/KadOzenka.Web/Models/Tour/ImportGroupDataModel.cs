using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Tour
{
	public class ImportGroupDataModel
	{
		[Display(Name = "Тур")]
		public long? TourId { get; set; }

		[Display(Name = "Статус единицы оценки")]
		public KoUnitStatus? UnitStatus { get; set; }
	}
}
