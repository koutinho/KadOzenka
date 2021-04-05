using System;

namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Сведения о кадастровой стоимости
	/// </summary>
	public class xmlCost
	{
		/// <summary>
		/// Значение
		/// </summary>
		public double Value { get; set; }
		/// <summary>
		/// Дата определения кадастровой стоимости
		/// </summary>
		public DateTime DateValuation { get; set; }
		/// <summary>
		/// Дата внесения сведений о кадастровой стоимости в ЕГРН
		/// </summary>
		public DateTime DateEntering { get; set; }
		/// <summary>
		/// Дата акта об утверждении кадастровой стоимости
		/// </summary>
		public DateTime DocDate { get; set; }
		/// <summary>
		/// Дата начала применения кадастровой стоимости
		/// </summary>
		public DateTime ApplicationDate { get; set; }
		/// <summary>
		/// Номер акта об утверждении кадастровой стоимости
		/// </summary>
		public string DocNumber { get; set; }
		/// <summary>
		/// Наименование документа об утверждении кадастровой стоимости
		/// </summary>
		public string DocName { get; set; }
		/// <summary>
		/// Дата утверждения кадастровой стоимости
		/// </summary>
		public DateTime DateApproval { get; set; }
		/// <summary>
		/// Дата подачи заявления о пересмотре кадастровой стоимости
		/// </summary>
		public DateTime RevisalStatementDate { get; set; }
	}
}