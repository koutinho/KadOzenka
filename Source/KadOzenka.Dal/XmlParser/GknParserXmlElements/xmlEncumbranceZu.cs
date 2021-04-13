using KadOzenka.Dal.XmlParser.GknParserXmlElements;

namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Ограничения (обременения)
	/// </summary>
	public class xmlEncumbranceZu : xmlEncumbranceOks
	{
		/// <summary>
		/// Реестровый номер границы зоны, территории
		/// </summary>
		public string AccountNumber { get; set; }
		/// <summary>
		/// Кадастровый номер ЗУ, в пользу которого установлен сервитут
		/// </summary>
		public string CadastralNumberRestriction { get; set; }
	}
}