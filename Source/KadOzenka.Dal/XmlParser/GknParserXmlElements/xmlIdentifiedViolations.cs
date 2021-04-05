namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Выявленное правонарушение
	/// </summary>
	public class xmlIdentifiedViolations
	{
		/// <summary>
		/// Площадь (в кв. м)
		/// </summary>
		public double Area { get; set; }
		/// <summary>
		/// Вид выявленного правонарушения
		/// </summary>
		public string TypeViolations { get; set; }
		/// <summary>
		/// Признаки выявленного правонарушения
		/// </summary>
		public string SignViolations { get; set; }
	}
}