using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Tour.EstimateGroup
{
	public class ImportGroupViewModel
	{
		/// <summary>
		/// Ид тура
		/// </summary>
		[Required(ErrorMessage = "Не заполнен тур")]
		public long? TourId { get; set; }
		/// <summary>
		/// Имя столбца где берем код
		/// </summary>
		[Required(ErrorMessage = "Выберите тмя столбца для значения кода")]
		public string CodeColumnName { get; set; }

		/// <summary>
		/// Имя столбца где берем код группы
		/// </summary>
		[Required(ErrorMessage = "Выберите тмя столбца для значения группы")]
		public string GroupColumnName { get; set; }


		/// <summary>
		/// Имя столбца где берем тип территории
		/// </summary>
		public string TerritoryTypeColumnName { get; set; }

		/// <summary>
		/// Имя столбца где берем тип жилое/нежилое
		/// </summary>
		public string RoomTypeColumnName { get; set; }

		/// <summary>
		/// Тип объекта недвижимости
		/// </summary>
		[Required(ErrorMessage = "Выберите тип объекта недвижимости")]
		public PropertyTypes ObjectType { get; set; }
	}
}