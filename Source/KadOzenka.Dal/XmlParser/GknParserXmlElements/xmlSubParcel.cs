using System;
using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser.GknParserXmlElements
{
	/// <summary>
	/// Сведения о части земельного участка
	/// </summary>
	public class xmlSubParcel
	{
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public double? Area { get; set; }
		/// <summary>
		/// Погрешность измерения
		/// </summary>
		public double? AreaInaccuracy { get; set; }
		/// <summary>
		/// Сведения об ограничениях (обременениях)
		/// </summary>
		public List<xmlEncumbranceZu> Encumbrances { get; set; }
		/// <summary>
		/// Учетный номер части
		/// </summary>
		public string NumberRecord { get; set; }
		/// <summary>
		/// Дата внесения в ЕГРН сведений о части
		/// </summary>
		public DateTime? DateCreated { get; set; }

		public xmlSubParcel()
		{
			Encumbrances = new List<xmlEncumbranceZu>();
		}
	}
}
