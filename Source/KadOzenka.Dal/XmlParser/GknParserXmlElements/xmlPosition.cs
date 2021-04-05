using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Местоположение в объекте недвижимости
	/// </summary>
	public class xmlPosition
	{
		/// <summary>
		/// Уровень (этаж)
		/// </summary>
		public xmlCodeNameValue Position { get; set; }
		/// <summary>
		/// Номер на плане
		/// </summary>
		public List<string> NumbersOnPlan { get; set; }
		public xmlPosition()
		{
			Position = new xmlCodeNameValue();
			NumbersOnPlan = new List<string>();
		}
	}
}