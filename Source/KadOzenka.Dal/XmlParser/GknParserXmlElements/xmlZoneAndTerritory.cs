namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Сведения о расположении земельного участка в границах зоны или территории
	/// </summary>
	public class xmlZoneAndTerritory
	{
		/// <summary>
		/// Наименование
		/// </summary>
		public string Description;
		/// <summary>
		/// Вид или наименование по документу
		/// </summary>
		public string CodeZoneDoc;
		/// <summary>
		/// Реестровый номер границы
		/// </summary>
		public string AccountNumber;
		/// <summary>
		/// Содержание ограничения
		/// </summary>
		public string ContentRestrictions;
		/// <summary>
		/// Полностью входит в зону
		/// </summary>
		public bool FullPartly;
		/// <summary>
		/// Реквизиты решения
		/// </summary>
		public xmlDocument Document;
	}
}