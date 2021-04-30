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
		public string Description { get; set; }
		/// <summary>
		/// Вид или наименование по документу
		/// </summary>
		public string CodeZoneDoc { get; set; }
		/// <summary>
		/// Реестровый номер границы
		/// </summary>
		public string AccountNumber { get; set; }
		/// <summary>
		/// Содержание ограничения
		/// </summary>
		public string ContentRestrictions { get; set; }
		/// <summary>
		/// Полностью входит в зону
		/// </summary>
		public bool? FullPartly { get; set; }
		/// <summary>
		/// Реквизиты решения
		/// </summary>
		public xmlDocument Document { get; set; }

		public xmlZoneAndTerritory()
		{
			Document = new xmlDocument();
		}
	}
}