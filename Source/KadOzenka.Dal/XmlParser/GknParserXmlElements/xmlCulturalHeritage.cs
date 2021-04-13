namespace KadOzenka.Dal.XmlParser.GknParserXmlElements
{
	/// <summary>
	/// Сведения об объектах культурного наследия (памятниках истории и культуры) народов Российской Федерации
	/// </summary>
	public class xmlCulturalHeritage
	{
		/// <summary>
		/// Регистрационный номер
		/// </summary>
		public string EgroknRegNum { get; set; }
		/// <summary>
		/// Вид объекта
		/// </summary>
		public xmlCodeName EgroknObjCultural { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		public string EgroknNameCultural { get; set; }
		/// <summary>
		/// Требования к сохранению, содержанию и использованию, обеспечению доступа
		/// </summary>
		public string RequirementsEnsure { get; set; }
		/// <summary>
		/// Реквизиты соответствующего решения
		/// </summary>
		public xmlDocument Document { get; set; }
	}
}
