namespace KadOzenka.Dal.XmlParser.GknParserXmlElements
{
	public class xmlLevel
	{
		/// <summary>
		/// Расположение в пределах этажа (части этажа)
		/// </summary>
		public xmlPos Position { get; set; }
		/// <summary>
		/// Номер этажа
		/// </summary>
		public string Number { get; set; }
		/// <summary>
		/// Тип этажа
		/// </summary>
		public xmlCodeName Type { get; set; }

		public xmlLevel()
		{
			Position = new xmlPos();
			Type = new xmlCodeName();
		}
	}
}
