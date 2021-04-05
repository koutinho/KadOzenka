namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Ограничения (обременения)
	/// </summary>
	public class xmlEncumbrance
	{
		/// <summary>
		/// Содержание ограничения (обременения)
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Код по справочнику
		/// </summary>
		public xmlCodeName Type { get; set; }
		/// <summary>
		/// Реестровый номер границы зоны, территории
		/// </summary>
		public string AccountNumber { get; set; }
		/// <summary>
		/// Кадастровый номер ЗУ, в пользу которого установлен сервитут
		/// </summary>
		public string CadastralNumberRestriction { get; set; }
		/// <summary>
		/// Площадь
		/// </summary>
		public double Area { get; set; }
		/// <summary>
		/// Государственная регистрация ограничения (обременения)
		/// </summary>
		public xmlNumberDate Registration { get; set; }
		/// <summary>
		/// Реквизиты документа, на основании которого возникает ограничение (обременение)
		/// </summary>
		public xmlDocument Document { get; set; }
	}
}