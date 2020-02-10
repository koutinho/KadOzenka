using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Gbu.Harmonization;

namespace KadOzenka.Web.Models.GbuObject
{
	public class HarmonizationCODViewModel
	{
		/// <summary>
		/// Идентификатор задания ЦОД
		/// </summary>
		[Display(Name = "Задание ЦОД")]
		public long? IdCodJob { get; set; }

		/// <summary>
		/// Идентификатор атрибута, куда будет записан результат 
		/// </summary>
		[Display(Name = "Характеристика")]
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

		/// <summary>
		/// Значение по умолчанию 
		/// </summary>
		[Display(Name = "Значение по умолчанию")]
		public string DefaultValue { get; set; }

		/// <summary>
		/// Документ для значения по умолчанию 
		/// </summary>
		[Display(Name = "Документ")]
		public long? IdDocument { get; set; }

		public HarmonizationCODSettings ToHarmonizationCODSettings()
		{
			var settings = new HarmonizationCODSettings
			{
				IdCodJob = IdCodJob,
				IdAttributeResult = IdAttributeResult,
				PropertyType = (PropertyTypes) PropertyType.GetValueOrDefault(),
				SelectAllObject = SelectAllObject,
				IdAttributeFilter = IdAttributeFilter,
				ValuesFilter = ValuesFilter,
				Level1Attribute = Level1Attribute,
				Level2Attribute = Level2Attribute,
				Level3Attribute = Level3Attribute,
				Level4Attribute = Level4Attribute,
				Level5Attribute = Level5Attribute,
				Level6Attribute = Level6Attribute,
				Level7Attribute = Level7Attribute,
				Level8Attribute = Level8Attribute,
				Level9Attribute = Level9Attribute,
				Level10Attribute = Level10Attribute,
				DefaultValue = DefaultValue,
				IdDocument = IdDocument
			};

			return settings;
		}
	}
}
