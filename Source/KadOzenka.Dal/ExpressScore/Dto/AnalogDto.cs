using System;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class AnalogDto
	{
		public long Id { get; set; }
		public string Kn { get; set; }
		/// <summary>
		/// Кадастровый номер
		/// </summary>
		public decimal Price { get; set; }
		/// <summary>
		/// Площадь
		/// </summary>
		public decimal Square { get; set; }
		/// <summary>
		/// Дата псоледнего обновления
		/// </summary>
		public DateTime LastDateUpdate { get; set; }
		/// <summary>
		/// Этаж помещения
		/// </summary>
		public long Floor { get; set; }

		/// <summary>
		/// Количество этажей
		/// </summary>
		public long FloorsCount { get; set; }



	}
}