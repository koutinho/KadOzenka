using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser.GknParserXmlElements
{
	/// <summary>
	/// Природные объекты на участке
	/// </summary>
	public class xmlNaturalObject
	{
		/// <summary>
		/// Вид объекта
		/// </summary>
		public xmlCodeName Kind { get; set; }
		/// <summary>
		/// Наименование лесничества (лесопарка), участкового лесничества
		/// </summary>
		public string Forestry { get; set; }
		/// <summary>
		/// Целевое назначение (категория) лесов
		/// </summary>
		public xmlCodeName ForestUse { get; set; }
		/// <summary>
		/// Номера лесных кварталов
		/// </summary>
		public string QuarterNumbers { get; set; }
		/// <summary>
		/// Номера лесотаксационных выделов
		/// </summary>
		public string TaxationSeparations { get; set; }
		/// <summary>
		/// Категория защитных лесов
		/// </summary>
		public xmlCodeName ProtectiveForest { get; set; }
		/// <summary>
		/// Виды разрешенного использования лесов
		/// </summary>
		public List<xmlCodeName> ForestEncumbrances { get; set; }
		/// <summary>
		/// Вид водного объекта
		/// </summary>
		public string WaterObject { get; set; }
		/// <summary>
		/// Наименование водного объекта, иного природного объекта
		/// </summary>
		public string NameOther { get; set; }
		/// <summary>
		/// Характеристика иного природного объекта
		/// </summary>
		public string CharOther { get; set; }

		public xmlNaturalObject()
		{
			ForestEncumbrances = new List<xmlCodeName>();
			Kind = new xmlCodeName();
			ForestUse = new xmlCodeName();
			ProtectiveForest = new xmlCodeName();
		}
	}
}
