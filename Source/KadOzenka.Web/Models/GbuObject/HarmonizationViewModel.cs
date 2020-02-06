using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.GbuObject
{
	public class HarmonizationViewModel
	{
		/// <summary>
		/// Идентификатор атрибута, куда будет записан результат 
		/// </summary>
		[Display(Name = "Характеристика")]
		public long? IdAttributeResult { get; set; }

		/// <summary>
		/// Тип объекта 
		/// </summary>
		public long? PropertyType { get; set; }

		/// <summary>
		/// Выборка по всем объектам
		/// </summary>
		public bool SelectAllObject { get; set; }

		/// <summary>
		/// Идентификатор аттрибута - фильтра
		/// </summary>
		[Display(Name = "Характеристика")]
		public long? IdAttributeFilter { get; set; }

		/// <summary>
		/// Список значений фильтра
		/// </summary>
		[Display(Name = "Значение")]
		public List<string> ValuesFilter { get; set; }

		/// <summary>
		/// Фактор 1 уровня 
		/// </summary>
		public long? Level1Attribute { get; set; }

		/// <summary>
		/// Фактор 2 уровня 
		/// </summary>
		public long? Level2Attribute { get; set; }

		/// <summary>
		/// Фактор 3 уровня 
		/// </summary>
		public long? Level3Attribute { get; set; }

		/// <summary>
		/// Фактор 4 уровня 
		/// </summary>
		public long? Level4Attribute { get; set; }

		/// <summary>
		/// Фактор 5 уровня 
		/// </summary>
		public long? Level5Attribute { get; set; }

		/// <summary>
		/// Фактор 6 уровня 
		/// </summary>
		public long? Level6Attribute { get; set; }

		/// <summary>
		/// Фактор 7 уровня 
		/// </summary>
		public long? Level7Attribute { get; set; }

		/// <summary>
		/// Фактор 8 уровня 
		/// </summary>
		public long? Level8Attribute { get; set; }

		/// <summary>
		/// Фактор 9 уровня 
		/// </summary>
		public long? Level9Attribute{get; set; }

		/// <summary>
		/// Фактор 10 уровня 
		/// </summary>
		public long? Level10Attribute { get; set; }
	}
}
