namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Разрешенное использование участка
	/// </summary>
	public class xmlUtilization
	{
		/// <summary>
		/// Вид разрешенного использования в соответствии с ранее использовавшимся классификатором
		/// </summary>
		public xmlCodeName Utilization { get; set; }
		/// <summary>
		/// Вид разрешенного использования земельного участка в соответствии с классификатором, утвержденным приказом Минэкономразвития России от 01.09.2014 № 540
		/// </summary>
		public xmlCodeName LandUse { get; set; }
		/// <summary>
		/// Вид использования участка по документу
		/// </summary>
		public string ByDoc { get; set; }
		/// <summary>
		/// Разрешенное использование (текстовое описание)
		/// </summary>
		public string PermittedUseText { get; set; }

		public xmlUtilization()
		{
			Utilization = new xmlCodeName();
			LandUse = new xmlCodeName();
		}
	}
}