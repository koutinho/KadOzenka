using System;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class AnalogDto
	{
		public long Id { get; set; }

		/// <summary>
		/// Кадастровый номер
		/// </summary>
		public string Kn { get; set; }
		/// <summary>
		/// Цена
		/// </summary>
		public decimal Price { get; set; }
		/// <summary>
		/// Площадь
		/// </summary>
		public decimal Square { get; set; }
		/// <summary>
		/// Дата добавления объекта или дата последнего обновления, для росреестра всегда дата добавления
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// Этаж помещения
		/// </summary>
		public long Floor { get; set; }

		/// <summary>
		/// Количество этажей
		/// </summary>
		public long FloorsCount { get; set; }

		/// <summary>
		/// Год постройки
		/// </summary>
		public long YearBuild { get; set; }

		/// <summary>
		/// Адрес аналога
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Тип сделки
		/// </summary>
		public DealType DealType { get; set; }
	}
}