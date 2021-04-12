namespace KadOzenka.Dal.XmlParser.GknParserXmlElements
{
	public class xmlEncumbranceOks
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
		/// Государственная регистрация ограничения (обременения)
		/// </summary>
		public xmlNumberDate Registration { get; set; }
		/// <summary>
		/// Реквизиты документа, на основании которого возникает ограничение (обременение)
		/// </summary>
		public xmlDocument Document { get; set; }

		public xmlEncumbranceOks() { }
	}
}
