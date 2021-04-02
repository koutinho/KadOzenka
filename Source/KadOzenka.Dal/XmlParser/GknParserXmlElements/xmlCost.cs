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
		public double Value;
		/// <summary>
		/// Дата определения кадастровой стоимости
		/// </summary>
		public DateTime DateValuation;
		/// <summary>
		/// Дата внесения сведений о кадастровой стоимости в ЕГРН
		/// </summary>
		public DateTime DateEntering;
		/// <summary>
		/// Дата акта об утверждении кадастровой стоимости
		/// </summary>
		public DateTime DocDate;
		/// <summary>
		/// Дата начала применения кадастровой стоимости
		/// </summary>
		public DateTime ApplicationDate;
		/// <summary>
		/// Номер акта об утверждении кадастровой стоимости
		/// </summary>
		public string DocNumber;
		/// <summary>
		/// Наименование документа об утверждении кадастровой стоимости
		/// </summary>
		public string DocName;
		/// <summary>
		/// Дата утверждения кадастровой стоимости
		/// </summary>
		public DateTime DateApproval;
		/// <summary>
		/// Дата подачи заявления о пересмотре кадастровой стоимости
		/// </summary>
		public DateTime RevisalStatementDate;
	}
}