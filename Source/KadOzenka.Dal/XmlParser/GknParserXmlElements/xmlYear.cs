namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Эксплуатационные характеристики
	/// </summary>
	public class xmlYear
	{
		/// <summary>
		/// Год завершения строительства
		/// </summary>
		public string Year_Built { get; set; }
		/// <summary>
		/// Год ввода в эксплуатацию по завершении строительства
		/// </summary>
		public string Year_Used { get; set; }
	}
}