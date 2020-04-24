using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;

namespace CIPJS.Models.ExpressScore
{
	public class NearestObjectViewModel
	{
		/// <summary>
		/// Площадь
		/// </summary>
		[Required(ErrorMessage = "Заполните площадь")]
		[Range(1, 5000, ErrorMessage = "Диапозон значений площади от 1 до 5000 кв. м")]
		public decimal? Square { get; set; }

		/// <summary>
		/// Сегмент рынка
		/// </summary>
		[Required(ErrorMessage = "Заполните сегмент рынка.")]
		public MarketSegment? Segment { get; set; }

		/// <summary>
		/// Количество аналогов для вывода
		/// </summary>
		[Required(ErrorMessage = "Не указано количество аналогов. Обратитесь к администратору.")]
		public int? Quality { get; set; }

		/// <summary>
		/// Широта точки от которой ищем объекты
		/// </summary>
		[Required(ErrorMessage = "Не указана широта. Обратитесь к администратору.")]
		public decimal? SelectedLat { get; set; }

		/// <summary>
		/// Долгота точки от которой ищем объекты
		/// </summary>
		[Required(ErrorMessage = "Не указана долгота. Обратитесь к администратору.")]
		public decimal? SelectedLng { get; set; }

		/// <summary>
		/// Адрес объекта 
		/// </summary>
		[Required(ErrorMessage = "Не указан адрес")]
		public string Address { get; set; }

		/// <summary>
		/// Тип сделаки
		/// </summary>
		[Required(ErrorMessage = "Не указан тип сделки")]
		public DealType DealType { get; set; }

		/// <summary>
		/// Дата актуальности
		/// </summary>
		[Required(ErrorMessage = "Не указана дата актуальности")]
		public DateTime? ActualDate { get; set; }

	}
}