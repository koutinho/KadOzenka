using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.GbuObject
{
	public class UnloadingFromDicViewModel
	{
		/// <summary>
		/// Идентификатор задания ЦОД
		/// </summary>
		[Display(Name = "Задание ЦОД")]
		[Required(ErrorMessage = "Выберете Задание ЦОД")]
		public long? IdCodJob { get; set; }

		/// <summary>
		/// Идентификатор атрибута, куда будет записан результат 
		/// </summary>
		[Display(Name = "Характеристика")]
		[Required(ErrorMessage = "Выберете результирующую Характеристику")]
		public long? IdAttributeResult { get; set; }

		/// <summary>
		/// Тип объекта 
		/// </summary>
		[Required(ErrorMessage = "Выберете Тип объекта")]
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
		/// Документ для значения по умолчанию 
		/// </summary>
		[Display(Name = "Документ")]
		[Required(ErrorMessage = "Выберете документ")]
		public long? IdDocument { get; set; }

	}
}