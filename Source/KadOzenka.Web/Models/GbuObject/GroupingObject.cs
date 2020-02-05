using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Gbu.GroupingAlgoritm;

namespace KadOzenka.Web.Models.GbuObject
{
	public class GroupingObject
	{
		/// <summary>
		/// Идентификатор задания ЦОД
		/// </summary>
		[Display(Name = "Справочник ЦОД")]
		public int? IdCodJob { get; set; }

		/// <summary>
		/// Выборка по всем объектам
		/// </summary>
		public bool SelectAllObject { get; set; }

		/// <summary>
		/// Идентификатор аттрибута - фильтра
		/// </summary>
		[Display(Name = "Характеристика")]
		public int? IdAttributeFilter { get; set; }

		/// <summary>
		/// Список значений фильтра
		/// </summary>
		[Display(Name = "Значения")]
		public List<string> ValuesFilter { get; set; }

		/// <summary>
		/// Настройки 1 уровня группировки
		/// </summary>
		public LevelItem Level1 { get; set; }

		/// <summary>
		/// Настройки 2 уровня группировки
		/// </summary>
		public LevelItem Level2 { get; set; }

		/// <summary>
		/// Настройки 3 уровня группировки
		/// </summary>
		public LevelItem Level3 { get; set; }

		/// <summary>
		/// Настройки 4 уровня группировки
		/// </summary>
		public LevelItem Level4 { get; set; }

		/// <summary>
		/// Настройки 5 уровня группировки
		/// </summary>
		public LevelItem Level5 { get; set; }

		/// <summary>
		/// Настройки 6 уровня группировки
		/// </summary>
		public LevelItem Level6 { get; set; }

		/// <summary>
		/// Настройки 7 уровня группировки
		/// </summary>
		public LevelItem Level7 { get; set; }

		/// <summary>
		/// Настройки 8 уровня группировки
		/// </summary>
		public LevelItem Level8 { get; set; }

		/// <summary>
		/// Настройки 9 уровня группировки
		/// </summary>
		public LevelItem Level9 { get; set; }

		/// <summary>
		/// Настройки 10 уровня группировки
		/// </summary>
		public LevelItem Level10 { get; set; }

		/// <summary>
		/// Настройки 11 уровня группировки
		/// </summary>
		public LevelItem Level11 { get; set; }

		/// <summary>
		/// Идентификатор атрибута, куда будет записан результат 
		/// </summary>
		[Display(Name = "Характеристика")]
		public int? IdAttributeResult { get; set; }
		/// <summary>
		/// Идентификатор атрибута, куда будут записаны источники 
		/// </summary>
		[Display(Name = "Источник")]
		public int? IdAttributeSource { get; set; }
		/// <summary>
		/// Идентификатор атрибута, куда будут записаны документы 
		/// </summary>
		[Display(Name = "Документ")]
		public int? IdAttributeDocument { get; set; }
	}
}