using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;

namespace CIPJS.Models.ExpressScore
{
	public class NearestObjectViewModel
	{
		/// <summary>
		/// Этажей в здании
		/// </summary>
		public int? Floor { get; set; }

		/// <summary>
		/// Площадь
		/// </summary>
		[Required(ErrorMessage = "Заполните площадь")]
		public decimal? Square { get; set; }
		
		/// <summary>
		/// Сегмент рынка
		/// </summary>
		public MarketSegment? Segment { get; set; }

		/// <summary>
		/// Количество аналогов для вывода
		/// </summary>
		[Required(ErrorMessage = "Не указано количество аналогов. Обратитесь к администратору")]
		public int? Quality { get; set; }

		/// <summary>
		/// Широта точки от которой ищем объекты
		/// </summary>
		[Required(ErrorMessage = "Не указана широта. Обратитесь к администратору")]
		public decimal? SelectedLat { get; set; }

		/// <summary>
		/// Долгота точки от которой ищем объекты
		/// </summary>
		[Required(ErrorMessage = "Не указана долгота. Обратитесь к администратору")]
		public decimal? SelectedLng { get; set; }

	}
}