namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Сведения об устранении выявленного нарушения
	/// </summary>
	public class xmlElimination
	{
		/// <summary>
		/// Отметка об устранении выявленного нарушения: 1(true)-устранено
		/// </summary>
		public bool EliminationMark;
		/// <summary>
		/// Наименование органа, принявшего решение об устранении правонарушения
		/// </summary>
		public string EliminationAgency;
	}
}