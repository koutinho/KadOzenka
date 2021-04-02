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
		public string Name;
		/// <summary>
		/// Код по справочнику
		/// </summary>
		public xmlCodeName Type;
		/// <summary>
		/// Реестровый номер границы зоны, территории
		/// </summary>
		public string AccountNumber;
		/// <summary>
		/// Кадастровый номер ЗУ, в пользу которого установлен сервитут
		/// </summary>
		public string CadastralNumberRestriction;
		/// <summary>
		/// Площадь
		/// </summary>
		public double Area;
		/// <summary>
		/// Государственная регистрация ограничения (обременения)
		/// </summary>
		public xmlNumberDate Registration;
		/// <summary>
		/// Реквизиты документа, на основании которого возникает ограничение (обременение)
		/// </summary>
		public xmlDocument Document;
	}
}