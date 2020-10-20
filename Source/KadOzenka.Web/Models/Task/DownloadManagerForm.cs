using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Task
{
	public class DownloadManagerForm
	{
		/// <summary>
		/// Идентификатор раскладки
		/// </summary>
		public long? LayoutId { get; set; }

		/// <summary>
		/// Имя раскладки
		/// </summary>
		[Required(ErrorMessage = "Имя обязательно")]
		public string Name { get; set; }

		/// <summary>
		/// Тип генерируемого файла
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Список колонок реестров
		/// </summary>
		public List<ColumnSetting> ColumnSettings { get; set; }

		/// <summary>
		/// Фильтр для раскладки
		/// </summary>
		public List<long> Filters { get; set; }

		/// <summary>
		/// Имя представления реестра
		/// </summary>
		[Required(ErrorMessage = "Имя представления не заполнено")]
		public string RegisterViewId { get; set; }

		/// <summary>
		/// Ид реестра
		/// </summary>
		[Required(ErrorMessage = "Ид представления не заполнено")]
		public int? RegisterId { get; set; }

	}

	public class ColumnSetting
	{
		public long AttributeId { get; set; }

		public int Ordinal { get; set; }

		public string HeaderName { get; set; }
	}
}